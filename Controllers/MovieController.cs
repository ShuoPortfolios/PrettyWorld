using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PrettyWorld.Models;
using PrettyWorld.ViewModel;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace PrettyWorld.Controllers
{
    public static class Extensions
    {
        public static string Filter(this string str, List<char> charsToRemove)
        {
            foreach (char c in charsToRemove)
            {
                str = str.Replace(c.ToString(), String.Empty);
            }

            return str;
        }
    }

    public class MovieController : Controller
    {
        private readonly ILogger<MovieController> _logger;

        private readonly PrettyWorldContext _db = new();

        private readonly IWebHostEnvironment WebHostEnvironment;

        public MovieController(ILogger<MovieController> logger, PrettyWorldContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _db = context;
            WebHostEnvironment = webHostEnvironment;
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

        private const string YoutubeLinkRegex = "(?:.+?)?(?:\\/v\\/|watch\\/|\\?v=|\\&v=|youtu\\.be\\/|\\/v=|^youtu\\.be\\/)([a-zA-Z0-9_-]{11})+";
        private static Regex regexExtractId = new Regex(YoutubeLinkRegex, RegexOptions.Compiled);
        private static string[] validAuthorities = { "youtube.com", "www.youtube.com", "youtu.be", "www.youtu.be" };

        public string ExtractVideoIdFromUri(Uri uri)
        {
            string authority = new UriBuilder(uri).Uri.Authority.ToLower();

            //check if the url is a youtube url
            if (validAuthorities.Contains(authority))
            {
                //and extract the id
                var regRes = regexExtractId.Match(uri.ToString());
                if (regRes.Success)
                {
                    return regRes.Groups[1].Value;
                }
            }

            return null;
        }

        // GET: MovieController
        public async Task<IActionResult> MovieList(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "Name";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var movies = from m in _db.Movies
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(m => m.MovieName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Name":
                    movies = movies.OrderBy(m => m.MovieName);
                    break;
                case "name_desc":
                    movies = movies.OrderByDescending(m => m.MovieName);
                    break;
                case "Date":
                    movies = movies.OrderBy(m => m.WatchDate);
                    break;
                case "date_desc":
                    movies = movies.OrderByDescending(m => m.WatchDate);
                    break;
                default:
                    movies = movies.OrderBy(m => m.WatchDate);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Movie>.CreateAsync(movies.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: MovieController/Details/5
        [HttpGet]
        public ActionResult MovieDetails(string movieName)
        {
            if (string.IsNullOrEmpty(movieName))
            {
                return new StatusCodeResult((int)System.Net.HttpStatusCode.BadRequest);
            }

            // 第一種寫法：========================================
            IQueryable<Movie> movieData = from _movieList in _db.Movies
                                            where _movieList.MovieName == movieName   
                                            select _movieList;


            //也可以寫成下面這樣：
            //var ListOne = from m in _db.UserTables
            //              where m.UserId == id
            //              select m;

            if (movieData == null)
            {    // 找不到這一筆記錄
                return NotFound();
            }
            else
            {
                return View(movieData.FirstOrDefault());
            }

        }

        // GET: MovieController/Create
        public ActionResult MovieCreate()
        {

            List<MovieTypeList> tl = new();
            tl = (from t in _db.MovieTypeLists select t).ToList();
            tl.Insert(0, new MovieTypeList { TypeId = 0, TypeName = "選擇電影類型" });

            ViewBag.MovieTypeLists = tl;    
            return View();
        }

        [HttpPost, ActionName("MovieCreate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MovieCreate([Bind("MovieName, WatchDate, MovieType, MoviePicture, Trailer, Director, TopCast, Review, Rating, Plot, Scene, Sound, Immersion, Acting")] MovieViewModel movieVM)
        {
            
            if (movieVM == null)
            {
                return new StatusCodeResult((int)System.Net.HttpStatusCode.BadRequest);
            }

            Uri myUri;
            if (!string.IsNullOrEmpty(movieVM.Trailer))
            {
                if (Uri.IsWellFormedUriString(movieVM.Trailer!, UriKind.Absolute))
                {
                    myUri = new Uri(movieVM.Trailer!, UriKind.Absolute);
                    movieVM.Trailer = ExtractVideoIdFromUri(myUri);
                }
            }

            if (ModelState.IsValid)
            {
                string stringFileName = UploadFile(movieVM);
                var movie = new Movie
                {
                    MovieName = movieVM.MovieName,
                    WatchDate = movieVM.WatchDate,
                    MovieType = movieVM.MovieType,
                    MoviePicture = stringFileName,
                    Trailer = movieVM.Trailer,
                    Director = movieVM.Director,
                    TopCast = movieVM.TopCast,
                    Review = movieVM.Review,
                    Rating = movieVM.Rating,
                    Plot = movieVM.Plot,
                    Scene = movieVM.Scene,
                    Sound = movieVM.Sound,
                    Immersion = movieVM.Immersion,
                    Acting = movieVM.Acting
                };

                try
                {
                    _db.Entry(movie).State = EntityState.Added;
                    await _db.SaveChangesAsync();

                    return RedirectToAction(nameof(MovieList));  // 提升程式的維護性，常用在"字串"上。
                }
                catch
                {
                    return View(movieVM);  // 若沒有新增成功，則列出原本畫面
                }
            }
            else
            {
                return View(movieVM);  // 若沒有新增成功，則列出原本畫面
            }
        }

        private string UploadFile(MovieViewModel movieVM)
        {
            string fileName = string.Empty;
            if(movieVM.MovieName != null)
            {
                string uploadDir = Path.Combine(WebHostEnvironment.WebRootPath, "MoviePictures");

                List<char> charsToRemove = new List<char>() { '/', '\'', ':', '*', '?', '"', '<', '>', '|', '@', '_', ',', '.' };
                fileName = movieVM.MovieName.Filter(charsToRemove) + ".jpg";

                string filePath = Path.Combine(uploadDir, fileName);    
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    movieVM.MoviePicture.CopyTo(fileStream);   
                }
            }

            return fileName;
        }

        public ActionResult MovieEdit(string movieName)
        {
            if (string.IsNullOrEmpty(movieName))
            {
                return new StatusCodeResult((int)System.Net.HttpStatusCode.BadRequest);
            }

            IQueryable<Movie> movieData = from _movieList in _db.Movies
                                        where _movieList.MovieName == movieName
                                        select _movieList;


            List<MovieTypeList> tl = new();
            tl = (from t in _db.MovieTypeLists select t).ToList();
            tl.Insert(0, new MovieTypeList { TypeId = 0, TypeName = "選擇電影類型" });

            ViewBag.MovieTypeLists = tl;


            if (movieData == null)
            {    // 找不到這一筆記錄
                return NotFound();
            }
            else
            {
                return View(movieData.FirstOrDefault());
            }

        }

        [HttpPost, ActionName("MovieEdit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MovieEdit([Bind("MovieName, WatchDate, MovieType, Trailer, Director, TopCast, Review, Rating, Plot, Scene, Sound, Immersion, Acting")] Movie _movie)
        {
            if (_movie == null)
            {
                return new StatusCodeResult((int)System.Net.HttpStatusCode.BadRequest);
            }

            Uri myUri;
            if (!string.IsNullOrEmpty(_movie.Trailer))
            {
                if(Uri.IsWellFormedUriString(_movie.Trailer!, UriKind.Absolute))
                {
                    myUri = new Uri(_movie.Trailer!, UriKind.Absolute);
                    _movie.Trailer = ExtractVideoIdFromUri(myUri);
                }           
            }

            ModelState.Remove("MovieName");
            ModelState.Remove("MoviePicture");

            if (ModelState.IsValid)
            {
                var movie = new Movie()
                {
                    MovieId = _movie.MovieId,
                    MovieName = _movie.MovieName,
                    WatchDate = _movie.WatchDate,
                    MovieType = _movie.MovieType,   
                    Trailer = _movie.Trailer,   
                    Director = _movie.Director, 
                    TopCast = _movie.TopCast,
                    Review = _movie.Review,
                    Rating = _movie.Rating,
                    Plot = _movie.Plot, 
                    Scene = _movie.Scene,
                    Sound = _movie.Sound,
                    Immersion = _movie.Immersion,
                    Acting = _movie.Acting
                };

                //只修改部分欄位
                _db.Movies.Attach(movie);
                _db.Entry(movie).Property(m => m.WatchDate).IsModified = true;
                _db.Entry(movie).Property(m => m.MovieType).IsModified = true;
                _db.Entry(movie).Property(m => m.Trailer).IsModified = true;
                _db.Entry(movie).Property(m => m.Director).IsModified = true;
                _db.Entry(movie).Property(m => m.TopCast).IsModified = true;
                _db.Entry(movie).Property(m => m.Review).IsModified = true;
                _db.Entry(movie).Property(m => m.Rating).IsModified = true;
                _db.Entry(movie).Property(m => m.Plot).IsModified = true;
                _db.Entry(movie).Property(m => m.Scene).IsModified = true;
                _db.Entry(movie).Property(m => m.Sound).IsModified = true;
                _db.Entry(movie).Property(m => m.Immersion).IsModified = true;
                _db.Entry(movie).Property(m => m.Acting).IsModified = true;

                //修改所有欄位
                //_db.Entry(_myProfile).State = EntityState.Modified;  // 確認被修改（狀態：Modified）

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(MovieList));
            }
            else
            {
                return Content(" *** 更新失敗！！*** ");
            }
        }

        // GET: MovieController/Delete/5
        public ActionResult MovieDelete(string movieName)
        {
            if (string.IsNullOrEmpty(movieName))
            {
                return new StatusCodeResult((int)System.Net.HttpStatusCode.BadRequest);
            }

            // 第一種寫法：========================================
            IQueryable<Movie> ListOne = from _movieList in _db.Movies
                                        where _movieList.MovieName == movieName
                                        select _movieList;
            //也可以寫成下面這樣：
            //var ListOne = from m in _db.UserTables
            //              where m.UserId == id
            //              select m;

            if (ListOne == null)
            {    // 找不到這一筆記錄
                return NotFound();
            }
            else
            {
                return View(ListOne.FirstOrDefault());
            }
        }

        // POST: MovieController/Delete/5
        [HttpPost, ActionName("MovieDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MovieDelete([Bind("MovieName")] Movie _movie)
        {
            if (_movie == null)
            {
                return new StatusCodeResult((int)System.Net.HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                _db.Entry(_movie).State = EntityState.Deleted;  // 確認被修改（狀態：Modified）
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(MovieList));  // 提升程式的維護性，常用在"字串"上。
            }
            else
            {
                ModelState.AddModelError("1", "刪除失敗");
                //return View(_userTable);  // 若沒有新增成功，則列出原本畫面
                return Content(" *** 刪除失敗！！*** ");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
