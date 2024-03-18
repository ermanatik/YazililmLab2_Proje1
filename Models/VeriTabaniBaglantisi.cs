using System.Data.SqlClient;
using System.Threading;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace YazilimLab2_Proje1.Models
{
    public class VeriTabaniBaglantisi : IDisposable
    {

        private static string connectionString = "Data Source =.\\SQLEXPRESS; Initial Catalog = Yazilim_Lab_2_Proje_1; Integrated Security = True;";

        public SqlConnection Connection { get; private set; }

        public VeriTabaniBaglantisi(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
        }




        public static SqlConnection BaglantiyiAc()
        {
            SqlConnection baglanti = new SqlConnection(connectionString);
            baglanti.Open();
            return baglanti;
        }
    
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Bağlantıyı kapatma işlemleri
                if (Connection != null)
                {
                    Connection.Close();
                    Connection.Dispose();
                }
            }
        }
        public static void BaglantiyiKapat(SqlConnection baglanti)
        {
            baglanti.Close();
        }
    }
}
