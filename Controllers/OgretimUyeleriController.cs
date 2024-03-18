using Microsoft.AspNetCore.Mvc;
using YazilimLab2_Proje1.Models;

namespace YazilimLab2_Proje1.Controllers
{
    public class OgretimUyeleriController : Controller
    {
        public IActionResult Index()
        {
            Ogretim_Uyeleri ogretimUyeleriModel = new Ogretim_Uyeleri();
            var tumOgretimUyeleri = ogretimUyeleriModel.TumOgretimUyeleriGetir();

            return View(tumOgretimUyeleri);
        }

    }
}
