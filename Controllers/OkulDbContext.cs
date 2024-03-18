using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using YazilimLab2_Proje1.Models;

namespace YazilimLab2_Proje1.Controllers
{
    public class OkulDbContext: DbContext
    {
        public OkulDbContext(DbContextOptions<OkulDbContext> options) :base(options) 
        { 
        }


        public DbSet<Ogrenciler> Ogrenci { get; set; } 
        public DbSet<Dersler> Ders { get; set; }
        public DbSet<Ogretim_Uyeleri> Ogretim_Uyesi{ get; set; }
        public DbSet<Alinan_Dersler> Alinan_Ders { get; set; }




    }
}
