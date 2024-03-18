using System.Data.SqlClient;

namespace YazilimLab2_Proje1.Models
{
    public class Alinan_Dersler
    {
        public int Alinan_Ders_id { get; set; }
        public int Ogrenci_id { get; set; }
        public int Ders_id { get; set; }
        public int Ogretim_Uyesi_id { get; set; }
        public int Notu {  get; set; }

        public List<Alinan_Dersler> TumAlinanDersleriGetir()
        {
            List<Alinan_Dersler> alinanDersler = new List<Alinan_Dersler>();

            using (var baglanti = new VeriTabaniBaglantisi("Data Source=.\\SQLEXPRESS; Initial Catalog=Yazilim_Lab_2_Proje_1; Integrated Security=True;"))
            {
                using (SqlCommand komut = new SqlCommand("SELECT * FROM Alinan_Dersler", baglanti.Connection))
                {
                    baglanti.Connection.Open();

                    using (SqlDataReader reader = komut.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Alinan_Dersler alinanDers = new Alinan_Dersler();
                            alinanDers.Alinan_Ders_id = Convert.ToInt32(reader["Alinan_Ders_id"]);
                            alinanDers.Ogrenci_id = Convert.ToInt32(reader["Ogrenci_id"]);
                            alinanDers.Ders_id = Convert.ToInt32(reader["Ders_id"]);
                            alinanDers.Ogretim_Uyesi_id = Convert.ToInt32(reader["Ogretim_Uyesi_id"]);
                            alinanDers.Notu = Convert.ToInt32(reader["Notu"]);

                            alinanDersler.Add(alinanDers);
                        }
                    }
                }
            }

            return alinanDersler;
        }

    }
}
