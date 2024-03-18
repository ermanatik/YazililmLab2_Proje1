using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using YazilimLab2_Proje1.Models;

using System.Data.SqlClient;

namespace YazilimLab2_Proje1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult DogumYeriAyniOlanOgrencileriGetir(string dogumYeri)
        {
            // Veritabanýndan doðum yeri ayný olan öðrencilerin listesini al
            List<Ogrenciler> ogrenciler = DogumYeriAyniOlanOgrencileriGetirVeritabanindan(dogumYeri);

            // Öðrenci listesini View'e gönder
            return View("OgrenciListesi", ogrenciler);
        }

        private List<Ogrenciler> DogumYeriAyniOlanOgrencileriGetirVeritabanindan(string dogumYeri)
        {
            List<Ogrenciler> ogrenciler = new List<Ogrenciler>();

            using (var baglanti = new VeriTabaniBaglantisi("Data Source =.\\SQLEXPRESS; Initial Catalog = Yazilim_Lab_2_Proje_1; Integrated Security = True;"))
            {
                using (SqlCommand komut = new SqlCommand("SELECT * FROM Ogrenciler WHERE Ogrenci_dogum_yeri = @dogumYeri", baglanti.Connection))
                {
                    komut.Parameters.AddWithValue("@dogumYeri", dogumYeri);

                    baglanti.Connection.Open();

                    using (SqlDataReader reader = komut.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Ogrenciler ogrenci = new Ogrenciler();
                            ogrenci.Ogrenci_id = Convert.ToInt32(reader["Ogrenci_id"]);
                            ogrenci.Ogrenci_ad = reader["Ogrenci_ad"].ToString();
                            ogrenci.Ogrenci_soyad = reader["Ogrenci_soyad"].ToString();
                            ogrenci.Ogrenci_dogum_yeri = reader["Ogrenci_dogum_yeri"].ToString();
                            ogrenci.Ogrenci_dogum_tarihi = Convert.ToDateTime(reader["Ogrenci_dogum_tarihi"]);
                            ogrenci.Ogrenci_sehir = reader["Ogrenci_sehir"].ToString();

                            ogrenciler.Add(ogrenci);
                        }
                    }
                }
            }

            return ogrenciler;
        }

        


        public IActionResult DogumTarihiAyniOlanOgrencileriGetir()
        {
            List<Ogrenciler> ogrenciler = DogumTarihiAyniOlanOgrencileriGetirVeritabanindan();
            return View("DogumTarihiAyniOlanOgrenciler", ogrenciler);
        }

        private List<Ogrenciler> DogumTarihiAyniOlanOgrencileriGetirVeritabanindan()
        {
            List<Ogrenciler> ogrenciler = new List<Ogrenciler>();

            using (var baglanti = new VeriTabaniBaglantisi("Data Source =.\\SQLEXPRESS; Initial Catalog = Yazilim_Lab_2_Proje_1; Integrated Security = True;"))
            {
                string sorgu = "SELECT *\r\nFROM Ogrenciler\r\nWHERE Ogrenci_dogum_tarihi IN (\r\n    SELECT Ogrenci_dogum_tarihi\r\n    FROM Ogrenciler\r\n    GROUP BY Ogrenci_dogum_tarihi\r\n    HAVING COUNT(*) > 1\r\n)";

                using (SqlCommand komut = new SqlCommand(sorgu, baglanti.Connection))
                {
                    baglanti.Connection.Open();

                    using (SqlDataReader reader = komut.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Ogrenciler ogrenci = new Ogrenciler();
                            ogrenci.Ogrenci_id = Convert.ToInt32(reader["Ogrenci_id"]);
                            ogrenci.Ogrenci_ad = reader["Ogrenci_ad"].ToString();
                            ogrenci.Ogrenci_soyad = reader["Ogrenci_soyad"].ToString();
                            ogrenci.Ogrenci_dogum_tarihi = Convert.ToDateTime(reader["Ogrenci_dogum_tarihi"]);
                            ogrenci.Ogrenci_dogum_yeri = reader["Ogrenci_dogum_yeri"].ToString();
                            ogrenci.Ogrenci_sehir = reader["Ogrenci_sehir"].ToString();

                            ogrenciler.Add(ogrenci);
                        }
                    }
                }
            }

            return ogrenciler;
        }



        public IActionResult OgrencileriGetirBySehirVeNot(string dogumYeri, int notDegeri)
        {
            List<Ogrenciler> ogrenciler = OgrencileriGetirBySehirVeNotVeriTabanýndan(dogumYeri, notDegeri);
            return View("OgrencileriGetirBySehirVeNot", ogrenciler);
        }

        private List<Ogrenciler> OgrencileriGetirBySehirVeNotVeriTabanýndan(string dogumYeri, int notDegeri)
        {
            List<Ogrenciler> ogrenciler = new List<Ogrenciler>();

            using (var baglanti = new VeriTabaniBaglantisi("Data Source =.\\SQLEXPRESS; Initial Catalog = Yazilim_Lab_2_Proje_1; Integrated Security = True;"))
            {
                string sorgu = @"SELECT o.Ogrenci_id, o.Ogrenci_ad, o.Ogrenci_soyad, o.Ogrenci_dogum_yeri, o.Ogrenci_dogum_tarihi, o.Ogrenci_sehir
                FROM Ogrenciler o
                INNER JOIN Alinan_Dersler ad ON o.Ogrenci_id = ad.Ogrenci_id
                INNER JOIN Dersler d ON ad.Ders_id = d.Ders_id
                WHERE o.Ogrenci_dogum_yeri = @dogumYeri AND ad.Notu > @notDegeri";

                baglanti.Connection.Open();

                using (SqlCommand komut = new SqlCommand(sorgu, baglanti.Connection))
                {
                    komut.Parameters.AddWithValue("@dogumYeri", dogumYeri);
                    komut.Parameters.AddWithValue("@notDegeri", notDegeri);

                    SqlDataReader okuyucu = komut.ExecuteReader();
                    while (okuyucu.Read())
                    {
                        Ogrenciler ogrenci = new Ogrenciler
                        {
                            Ogrenci_id = (int)okuyucu["Ogrenci_id"],
                            Ogrenci_ad = okuyucu["Ogrenci_ad"].ToString(),
                            Ogrenci_soyad = okuyucu["Ogrenci_soyad"].ToString(),
                            Ogrenci_dogum_yeri = okuyucu["Ogrenci_dogum_yeri"].ToString(),
                            Ogrenci_dogum_tarihi = (DateTime)okuyucu["Ogrenci_dogum_tarihi"],
                            Ogrenci_sehir = okuyucu["Ogrenci_sehir"].ToString()
                        };
                        ogrenciler.Add(ogrenci);
                    }
                }
            }

            return ogrenciler;
        }




        public IActionResult NotuGecenOgrencileriGetir(string dersAdi)
        {
            List<Ogrenciler> ogrenciler = new List<Ogrenciler>();

            using (var baglanti = new VeriTabaniBaglantisi("Data Source =.\\SQLEXPRESS; Initial Catalog = Yazilim_Lab_2_Proje_1; Integrated Security = True;"))
            {
                // Ders adýna göre öðrencileri ve notlarý içeren bir sorgu oluþturun
                string sorgu = "SELECT Ogrenciler.Ogrenci_ad, Ogrenciler.Ogrenci_soyad, Ogrenciler.Ogrenci_dogum_yeri, Ogrenciler.Ogrenci_dogum_tarihi, Ogrenciler.Ogrenci_sehir " +
                               "FROM Ogrenciler " +
                               "INNER JOIN Alinan_Dersler ON Ogrenciler.Ogrenci_id = Alinan_Dersler.Ogrenci_id " +
                               "INNER JOIN Dersler ON Alinan_Dersler.Ders_id = Dersler.Ders_id " +
                               "WHERE Dersler.Ders_adi = @dersAdi AND Alinan_Dersler.Notu > @notSiniri";

                // Baðlantýyý açýn ve sorguyu çalýþtýrýn
                using (SqlCommand komut = new SqlCommand(sorgu, baglanti.Connection))
                {
                    komut.Parameters.AddWithValue("@dersAdi", dersAdi);
                    // Not sýnýrý için bir deðer belirleyin (örneðin 50)
                    komut.Parameters.AddWithValue("@notSiniri", 50);

                    baglanti.Connection.Open();

                    // Sonuçlarý okuyun ve öðrenci listesine ekleyin
                    using (SqlDataReader reader = komut.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Ogrenciler ogrenci = new Ogrenciler();
                            ogrenci.Ogrenci_ad = reader["Ogrenci_ad"].ToString();
                            ogrenci.Ogrenci_soyad = reader["Ogrenci_soyad"].ToString();
                            ogrenci.Ogrenci_dogum_yeri = reader["Ogrenci_dogum_yeri"].ToString();
                            ogrenci.Ogrenci_dogum_tarihi = Convert.ToDateTime(reader["Ogrenci_dogum_tarihi"]);
                            ogrenci.Ogrenci_sehir = reader["Ogrenci_sehir"].ToString();
                            ogrenciler.Add(ogrenci);
                        }
                    }
                }
            }

            // Öðrenci listesini görünüme aktarýn
            return View("OgrenciNotuListesi", ogrenciler);
        }

        public IActionResult OgretimUyesineGoreOgrenciSayisiGetir(string ogretimUyesiAdi, string dersAdi)
        {
            int gecenOgrenciSayisi = 0;
            int kalanOgrenciSayisi = 0;

            using (var baglanti = new VeriTabaniBaglantisi("Data Source =.\\SQLEXPRESS; Initial Catalog = Yazilim_Lab_2_Proje_1; Integrated Security = True;"))
            {
                string sorgu = "SELECT COUNT(*) AS OgrenciSayisi, Alinan_Dersler.Notu " +
                               "FROM Ogrenciler " +
                               "INNER JOIN Alinan_Dersler ON Ogrenciler.Ogrenci_id = Alinan_Dersler.Ogrenci_id " +
                               "INNER JOIN Dersler ON Alinan_Dersler.Ders_id = Dersler.Ders_id " +
                               "INNER JOIN Ogretim_Uyeleri ON Alinan_Dersler.Ogretim_Uyesi_id = Ogretim_Uyeleri.Ogretim_Uyesi_id " +
                               "WHERE Ogretim_Uyeleri.Ogretim_Uyesi_ad = @ogretimUyesiAdi AND Dersler.Ders_adi = @dersAdi " +
                               "GROUP BY Alinan_Dersler.Notu";

                using (SqlCommand komut = new SqlCommand(sorgu, baglanti.Connection))
                {
                    komut.Parameters.AddWithValue("@ogretimUyesiAdi", ogretimUyesiAdi);
                    komut.Parameters.AddWithValue("@dersAdi", dersAdi);

                    baglanti.Connection.Open();

                    using (SqlDataReader reader = komut.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int notu = Convert.ToInt32(reader["Notu"]);
                            int ogrenciSayisi = Convert.ToInt32(reader["OgrenciSayisi"]);

                            if (notu >= 50)
                            {
                                gecenOgrenciSayisi += ogrenciSayisi;
                            }
                            else
                            {
                                kalanOgrenciSayisi += ogrenciSayisi;
                            }
                        }
                    }
                }
            }

            // Sonuçlarý görünüme aktarýn
            ViewBag.GecenOgrenciSayisi = gecenOgrenciSayisi;
            ViewBag.KalanOgrenciSayisi = kalanOgrenciSayisi;

            return View("OgrenciSayisi", new { GecenOgrenciSayisi = gecenOgrenciSayisi, KalanOgrenciSayisi = kalanOgrenciSayisi });
        }


        public IActionResult DersEkle(string dersAdi, int dersKredi)
        {
            using (var baglanti = new VeriTabaniBaglantisi("Data Source =.\\SQLEXPRESS; Initial Catalog = Yazilim_Lab_2_Proje_1; Integrated Security = True;"))
            {
                string sorgu = "INSERT INTO Dersler (Ders_adi, Ders_kredi) VALUES (@dersAdi, @dersKredi)";

                using (SqlCommand komut = new SqlCommand(sorgu, baglanti.Connection))
                {
                    komut.Parameters.AddWithValue("@dersAdi", dersAdi);
                    komut.Parameters.AddWithValue("@dersKredi", dersKredi);

                    baglanti.Connection.Open();
                    komut.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index", "Dersler"); // Dersler tablosunu gösteren bir view'e yönlendirme yapabilirsiniz
        }

        public IActionResult OgrenciEkle(string ogrenciAd, string ogrenciSoyad, string ogrenciDogumYeri, DateTime ogrenciDogumTarihi, string ogrenciSehir)
        {
            using (var baglanti = new VeriTabaniBaglantisi("Data Source =.\\SQLEXPRESS; Initial Catalog = Yazilim_Lab_2_Proje_1; Integrated Security = True;"))
            {
                string sorgu = "INSERT INTO Ogrenciler (Ogrenci_ad, Ogrenci_soyad, Ogrenci_dogum_yeri, Ogrenci_dogum_tarihi, Ogrenci_sehir) " +
                               "VALUES (@ogrenciAd, @ogrenciSoyad, @ogrenciDogumYeri, @ogrenciDogumTarihi, @ogrenciSehir)";

                using (SqlCommand komut = new SqlCommand(sorgu, baglanti.Connection))
                {
                    komut.Parameters.AddWithValue("@ogrenciAd", ogrenciAd);
                    komut.Parameters.AddWithValue("@ogrenciSoyad", ogrenciSoyad);
                    komut.Parameters.AddWithValue("@ogrenciDogumYeri", ogrenciDogumYeri);
                    komut.Parameters.AddWithValue("@ogrenciDogumTarihi", ogrenciDogumTarihi);
                    komut.Parameters.AddWithValue("@ogrenciSehir", ogrenciSehir);

                    baglanti.Connection.Open();
                    komut.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index", "Ogrenciler"); // Ogrenciler tablosunu gösteren bir view'e yönlendirme yapabilirsiniz
        }

        public IActionResult OgretimUyesiEkle(string ogretimUyesiAd, string ogretimUyesiSoyad, string ogretimUyesiUnvan)
        {
            using (var baglanti = new VeriTabaniBaglantisi("Data Source =.\\SQLEXPRESS; Initial Catalog = Yazilim_Lab_2_Proje_1; Integrated Security = True;"))
            {
                string sorgu = "INSERT INTO Ogretim_Uyeleri (Ogretim_Uyesi_ad, Ogretim_Uyesi_soyad, Ogretim_Uyesi_unvan) " +
                               "VALUES (@ogretimUyesiAd, @ogretimUyesiSoyad, @ogretimUyesiUnvan)";

                using (SqlCommand komut = new SqlCommand(sorgu, baglanti.Connection))
                {
                    komut.Parameters.AddWithValue("@ogretimUyesiAd", ogretimUyesiAd);
                    komut.Parameters.AddWithValue("@ogretimUyesiSoyad", ogretimUyesiSoyad);
                    komut.Parameters.AddWithValue("@ogretimUyesiUnvan", ogretimUyesiUnvan);

                    baglanti.Connection.Open();
                    komut.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index", "OgretimUyeleri"); // OgretimUyeleri tablosunu gösteren bir view'e yönlendirme yapabilirsiniz
        }


        public IActionResult AlinanDersEkle(int ogrenciId, int dersId, int ogretimUyesiId, int notu)
        {
            using (var baglanti = new VeriTabaniBaglantisi("Data Source =.\\SQLEXPRESS; Initial Catalog = Yazilim_Lab_2_Proje_1; Integrated Security = True;"))
            {
                string sorgu = "INSERT INTO Alinan_Dersler (Ogrenci_id, Ders_id, Ogretim_Uyesi_id, Notu) " +
                               "VALUES (@ogrenciId, @dersId, @ogretimUyesiId, @notu)";

                using (SqlCommand komut = new SqlCommand(sorgu, baglanti.Connection))
                {
                    komut.Parameters.AddWithValue("@ogrenciId", ogrenciId);
                    komut.Parameters.AddWithValue("@dersId", dersId);
                    komut.Parameters.AddWithValue("@ogretimUyesiId", ogretimUyesiId);
                    komut.Parameters.AddWithValue("@notu", notu);

                    baglanti.Connection.Open();
                    komut.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index", "AlinanDersler"); // AlinanDersler tablosunu gösteren bir view'e yönlendirme yapabilirsiniz
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
