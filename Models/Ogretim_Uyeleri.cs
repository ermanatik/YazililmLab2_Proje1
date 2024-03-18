using System.Data.SqlClient;

namespace YazilimLab2_Proje1.Models
{
    public class Ogretim_Uyeleri
    {
        public int Ogretim_Uyesi_id { get; set; }
        public string Ogretim_Uyesi_ad { get; set; }
        public string Ogretim_Uyesi_soyad { get; set; }
        public string Ogretim_Uyesi_unvan { get; set; }

        public List<Ogretim_Uyeleri> TumOgretimUyeleriGetir()
        {
            List<Ogretim_Uyeleri> ogretimUyeleri = new List<Ogretim_Uyeleri>();

            using (var baglanti = new VeriTabaniBaglantisi("Data Source=.\\SQLEXPRESS; Initial Catalog=Yazilim_Lab_2_Proje_1; Integrated Security=True;"))
            {
                using (SqlCommand komut = new SqlCommand("SELECT * FROM Ogretim_Uyeleri", baglanti.Connection))
                {
                    baglanti.Connection.Open();

                    using (SqlDataReader reader = komut.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Ogretim_Uyeleri ogretimUyesi = new Ogretim_Uyeleri();
                            ogretimUyesi.Ogretim_Uyesi_id = Convert.ToInt32(reader["Ogretim_Uyesi_id"]);
                            ogretimUyesi.Ogretim_Uyesi_ad = reader["Ogretim_Uyesi_ad"].ToString();
                            ogretimUyesi.Ogretim_Uyesi_soyad = reader["Ogretim_Uyesi_soyad"].ToString();
                            ogretimUyesi.Ogretim_Uyesi_unvan = reader["Ogretim_Uyesi_unvan"].ToString();

                            ogretimUyeleri.Add(ogretimUyesi);
                        }
                    }
                }
            }

            return ogretimUyeleri;
        }

    }
}
