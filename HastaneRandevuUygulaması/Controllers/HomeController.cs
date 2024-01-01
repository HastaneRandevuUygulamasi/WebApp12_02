using HastaneRandevuUygulaması.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using HastaneRandevuUygulaması.Services;
using Microsoft.AspNetCore.Localization;

namespace HastaneRandevuUygulaması.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private LanguageService _localization;

        public HomeController(ILogger<HomeController> logger, LanguageService localization)
        {
            _logger = logger;
            _localization = localization;
        }

        public IActionResult Index()
        {
            ViewBag.Welcome = _localization.Getkey("Welcome").Value;
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
            return View();
        }
        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                });
            return Redirect(Request.Headers["Referer"].ToString());
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

        public IActionResult AdminGiris()
        {
            return View();
        }
        public IActionResult AdminGirisKontrol(AdminGiris admin)
        {
           
            if ((admin.Email=="g211210011@sakarya.edu.tr"|| admin.Email=="g211210017@sakarya.edu.tr") && (admin.Sifre=="sau"))
            {
                HttpContext.Session.SetString("SessionUser", admin.Email);
                HttpContext.Session.SetString("Yetki", "Admin");
                return RedirectToAction("AdminIndex", "Home");
            }
            else
            {
                // Giriş başarısız, kullanıcıyı bilgilendir
                ViewBag.ErrorMessage = "TC Kimlik No veya Şifre yanlış.";
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult AdminIndex()
        {
            return View();
        }
    }
}
