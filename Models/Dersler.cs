using System.Data.SqlClient;

namespace YazilimLab2_Proje1.Models
{
    public class Dersler
    {
        public int Ders_id { get; set; }
        public string Ders_adi { get; set; }
        public int Ders_kredi { get; set; }

        public List<Dersler> TümDersleriGetir()
        {

            List<Dersler> dersler = new List<Dersler>();

            using (var baglanti = new VeriTabaniBaglantisi("Data Source =.\\SQLEXPRESS; Initial Catalog = Yazilim_Lab_2_Proje_1; Integrated Security = True;"))
            {
                using (SqlCommand komut = new SqlCommand("SELECT * FROM Dersler", baglanti.Connection))
                {
                    baglanti.Connection.Open();

                    using (SqlDataReader reader = komut.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Dersler ders = new Dersler();
                            ders.Ders_id = Convert.ToInt32(reader["Ders_id"]);
                            ders.Ders_adi = reader["Ders_adi"].ToString();
                            ders.Ders_kredi = Convert.ToInt32(reader["Ders_kredi"]);
                         
                            dersler.Add(ders);
                        }
                    }

                }

            }

        return dersler;
        }
    }
}
