using System;
using System.Linq;
using System.Windows.Forms;
using TimeSpace.DataAccess;
using TimeSpace.Entities;

namespace TimeSpace.UI
{
    public partial class FrmAyarlar : DevExpress.XtraEditors.XtraForm
    {
        ChronoTaleDBEntities db = new ChronoTaleDBEntities();
        int aktifID = Program.KullaniciID;

        public FrmAyarlar()
        {
            InitializeComponent();
        }

        private void btnGecmisiTemizle_Click(object sender, EventArgs e)
        {
            
            FrmMesaj soru = new FrmMesaj("Tüm hikaye geçmişiniz silinecek. Emin misiniz?", "Geçmiş Temizliği");

           
            if (soru.ShowDialog() == DialogResult.Yes)
            {
                try
                {
                    var kullaniciHikayeleri = db.Tbl_Hikayeler.Where(x => x.KullaniciID == aktifID).ToList();

                    if (kullaniciHikayeleri.Count > 0)
                    {
                        db.Tbl_Hikayeler.RemoveRange(kullaniciHikayeleri);
                        db.SaveChanges();

                        
                        new FrmMesaj("Hikaye geçmişiniz tertemiz oldu!", "İşlem Tamam").ShowDialog();
                    }
                    else
                    {
                        new FrmMesaj("Zaten silinecek bir hikayeniz yok.", "Bilgi").ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    new FrmMesaj("Hata oluştu: " + ex.Message, "Hata").ShowDialog();
                }
            }
        }

        private void btnHesabiSil_Click(object sender, EventArgs e)
        {
            
            FrmMesaj mesajKutusu = new FrmMesaj("Hesabınız, profil resminiz ve tüm hikayeleriniz KALICI OLARAK silinecek.\nBu işlem geri alınamaz.\nDevam etmek istiyor musunuz?", "HESAP SİLME");

            DialogResult cevap = mesajKutusu.ShowDialog();

            
            if (cevap == DialogResult.Yes)
            {
                try
                {
                    // 1. Önce Hikayeleri Sil
                    var hikayeler = db.Tbl_Hikayeler.Where(x => x.KullaniciID == aktifID).ToList();
                    if (hikayeler.Count > 0)
                    {
                        db.Tbl_Hikayeler.RemoveRange(hikayeler);
                    }

                    // 2. Sonra Kullanıcıyı Sil
                    var kullanici = db.Tbl_Kullanicilar.Find(aktifID);
                    if (kullanici != null)
                    {
                        db.Tbl_Kullanicilar.Remove(kullanici);
                        db.SaveChanges(); // Veritabanına işle
                    }

                    // 3. Bilgi Ver
                    
                    new FrmMesaj("Hesabınız başarıyla silindi. Hoşçakalın...", "Elveda").ShowDialog();

                    // 4. Uygulamayı Sıfırla
                    Program.KullaniciID = 0;
                    Application.Restart();
                }
                catch (Exception ex)
                {
                    new FrmMesaj("Hata: " + ex.Message, "HATA").ShowDialog();
                }
            }
        }
    }
}