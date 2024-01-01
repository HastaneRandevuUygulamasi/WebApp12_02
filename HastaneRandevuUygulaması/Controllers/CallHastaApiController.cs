using HastaneRandevuUygulaması.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HastaneRandevuUygulaması.Controllers
{
    public class CallHastaApiController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Hasta> hastalar = new List<Hasta>();
            HttpClient client = new HttpClient();
   
            var response = await client.GetAsync("https://localhost:7245/api/HastaApi");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            hastalar = JsonConvert.DeserializeObject<List<Hasta>>(jsonResponse);


            return View(hastalar);
        }
    }
}
