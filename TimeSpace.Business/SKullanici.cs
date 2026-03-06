using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TimeSpace.DataAccess;

namespace TimeSpace.Business
{
    public class SKullanici
    {
        /// <summary>
        /// Kullanıcı giriş kontrolü yapar.
        /// </summary>
        public Tbl_Kullanicilar GirisYap(string girisBilgisi, string sifre)
        {
            // Bağlantı nesneni oluştur (Kendi bağlantı cümlenle)
            // Not: Bağlantı cümleni buraya doğru yazdığından emin ol.
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ChronoTaleDB;Integrated Security=True";

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();

                // --- DÜZELTİLEN SORGU ---
                // Kullanıcı giriş kutusuna E-posta DA yazsa, Kullanıcı Adı DA yazsa kabul et.
                string sql = "SELECT * FROM Tbl_Kullanicilar WHERE (Email = @giris OR KullaniciAdi = @giris) AND SifreHash = @sifre";

                SqlCommand komut = new SqlCommand(sql, baglanti);
                komut.Parameters.AddWithValue("@giris", girisBilgisi); // Hem mail hem kullanıcı adı için aynı parametreyi kullanıyoruz
                komut.Parameters.AddWithValue("@sifre", sifre);

                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    Tbl_Kullanicilar kullanici = new Tbl_Kullanicilar();
                    kullanici.KullaniciID = Convert.ToInt32(dr["KullaniciID"]);
                    kullanici.AdSoyad = dr["AdSoyad"].ToString();
                    kullanici.Email = dr["Email"].ToString();
                    kullanici.KullaniciTipi = dr["KullaniciTipi"].ToString(); // Admin/Uye kontrolü için önemli!
                                                                              // Diğer lazım olan bilgileri de buraya ekleyebilirsin

                    return kullanici;
                }
                else
                {
                    return null; // Kullanıcı bulunamadı
                }
            }
        }

        /// <summary>
        /// Yeni kullanıcı kaydeder.
        /// </summary>
        public void KayitOl(string adSoyad, string kadi, string sifre)
        {
            try
            {
                using (ChronoTaleDBEntities db = new ChronoTaleDBEntities())
                {
                    Tbl_Kullanicilar yeniKullanici = new Tbl_Kullanicilar();
                    yeniKullanici.AdSoyad = adSoyad;
                    yeniKullanici.KullaniciAdi = kadi;
                    yeniKullanici.SifreHash = sifre;
                    yeniKullanici.KayitTarihi = DateTime.Now;

                    db.Tbl_Kullanicilar.Add(yeniKullanici);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                // Hata durumunda hatayı yukarı fırlatır
                throw;
            }
        }
    }
}