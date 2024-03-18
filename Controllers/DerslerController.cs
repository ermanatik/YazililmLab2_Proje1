using Microsoft.AspNetCore.Mvc;
using YazilimLab2_Proje1.Models;

namespace YazilimLab2_Proje1.Controllers
{
    public class DerslerController : Controller
    {
        public IActionResult Index()
        {
            Dersler derslerModel = new Dersler();
            var tumDersler = derslerModel.TümDersleriGetir();
            return View(tumDersler);
       
        }
    }
}
