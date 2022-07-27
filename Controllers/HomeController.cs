using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PrettyWorld.Models;
using System.Diagnostics;


namespace PrettyWorld.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly PrettyWorldContext _db = new();

        public HomeController(ILogger<HomeController> logger, PrettyWorldContext context)
        {
            _logger = logger;
            _db = context;
        }

        public IActionResult Index(string id)
        {
            id = "L124614391";

            IQueryable<MyProfile> ProfileData = from _profileTable in _db.MyProfiles
                                                where _profileTable.Id == id
                                                select _profileTable;

            //寫法二
            //SqlParameter _id = new SqlParameter("id", id);

            //var ProfileData = _db.MyProfiles
            //                      .FromSqlRaw("SELECT * FROM MyProfile with(nolock) WHERE ID = @id", _id)
            //                      .OrderBy(d => d.Id)  
            //                      .FirstOrDefault();
            

            if (ProfileData == null)
            {
                return NotFound();
            }
            else
            {
                return View(ProfileData.FirstOrDefault());
            }
        }

        [HttpGet]
        public IActionResult Edit()
        {

            string id = "L124614391";
            if (id == null)
            {
                return new StatusCodeResult((int)System.Net.HttpStatusCode.BadRequest);
            }

            IQueryable<MyProfile> ProfileData = from _profileTable in _db.MyProfiles
                                                where _profileTable.Id == id
                                                select _profileTable;

            if (ProfileData == null)
            {
                return NotFound();
            }
            else
            {
                return View(ProfileData.FirstOrDefault());
            }
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]  // 避免CSRF攻擊  https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/implementing-basic-crud-functionality-with-the-entity-framework-in-asp-net-mvc-application#overpost
        public async Task<IActionResult> EditConfirm([Bind("Id, FullName, Email, Phone, Mobile, Address")] MyProfile _myProfile)
        {
            if(_myProfile == null)
            {
                return new StatusCodeResult((int)System.Net.HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var profile = new MyProfile() { 
                    Id = _myProfile.Id,
                    FullName = _myProfile.FullName, 
                    Email = _myProfile.Email, 
                    Phone = _myProfile.Phone, 
                    Mobile = _myProfile.Mobile, 
                    Address = _myProfile.Address 
                };   

                //只修改部分欄位
                _db.MyProfiles.Attach(profile);
                _db.Entry(profile).Property(p => p.FullName).IsModified = true;
                _db.Entry(profile).Property(p => p.Email).IsModified = true;
                _db.Entry(profile).Property(p => p.Phone).IsModified = true;
                _db.Entry(profile).Property(p => p.Mobile).IsModified = true;
                _db.Entry(profile).Property(p => p.Address).IsModified = true;

                //修改所有欄位
                //_db.Entry(_myProfile).State = EntityState.Modified;  // 確認被修改（狀態：Modified）

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Content(" *** 更新失敗！！*** ");
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}