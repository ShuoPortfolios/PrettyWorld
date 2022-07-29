using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PrettyWorld.Models;
using System.Diagnostics;


namespace PrettyWorld.Controllers
{
    public class MovieController : Controller
    {
        private readonly ILogger<MovieController> _logger;

        private readonly PrettyWorldContext _db = new();

        public MovieController(ILogger<MovieController> logger, PrettyWorldContext context)
        {
            _logger = logger;
            _db = context;
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
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MovieController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MovieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MovieController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MovieController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MovieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
