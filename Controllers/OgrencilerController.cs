using Microsoft.AspNetCore.Mvc;
using YazilimLab2_Proje1.Models;

namespace YazilimLab2_Proje1.Controllers
{
    public class OgrencilerController : Controller
    {
        public IActionResult Index()
        {
            Ogrenciler ogrenciModel = new Ogrenciler();
            var tumOgrenciler = ogrenciModel.TumOgrencileriGetir();

            return View(tumOgrenciler);
        }
        public IActionResult OgrencileriGetir(string dogumYeri)
        {
            List<Ogrenciler> ogrenciler = Ogrenciler.OgrencileriGetirDogumYeri(dogumYeri);
            return View(ogrenciler);
        }
 
    }

}
