using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace YazilimLab2_Proje1.Models
{
    public class Ogrenciler
    {
        public int Ogrenci_id { get; set; }
        public string Ogrenci_ad { get; set; }
        public string Ogrenci_soyad { get; set; }
        public DateTime Ogrenci_dogum_tarihi { get; set; }
        public string Ogrenci_dogum_yeri { get; set; }
        public string Ogrenci_sehir { get; set; }

        public List<Ogrenciler> TumOgrencileriGetir()
        {
            List<Ogrenciler> ogrenciler = new List<Ogrenciler>();

            using (var baglanti = new VeriTabaniBaglantisi("Data Source =.\\SQLEXPRESS; Initial Catalog = Yazilim_Lab_2_Proje_1; Integrated Security = True;"))
            {
                using (SqlCommand komut = new SqlCommand("SELECT * FROM Ogrenciler", baglanti.Connection))
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

        public static List<Ogrenciler> OgrencileriGetirDogumYeri(string dogumYeri)
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

        public List<Ogrenciler> DogumTarihiAyniOlanOgrencileriGetir()
        {
            List<Ogrenciler> ogrenciler = new List<Ogrenciler>();

            using (var baglanti = new VeriTabaniBaglantisi("Data Source =.\\SQLEXPRESS; Initial Catalog = Yazilim_Lab_2_Proje_1; Integrated Security = True;"))
            {
                string sorgu = "SELECT * FROM Ogrenciler GROUP BY Ogrenci_dogum_tarihi HAVING COUNT(Ogrenci_dogum_tarihi) > 1";

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




    }
}
