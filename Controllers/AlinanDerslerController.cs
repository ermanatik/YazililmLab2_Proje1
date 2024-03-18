using Microsoft.AspNetCore.Mvc;
using YazilimLab2_Proje1.Models;

namespace YazilimLab2_Proje1.Controllers
{
    public class AlinanDerslerController : Controller
    {
        public IActionResult Index()
        {
            Alinan_Dersler alinanDerslerModel = new Alinan_Dersler();
            var tumAlinanDersler = alinanDerslerModel.TumAlinanDersleriGetir();

            return View(tumAlinanDersler);
        }


    }
}
