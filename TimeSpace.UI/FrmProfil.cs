using System;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TimeSpace.Business;
using TimeSpace.DataAccess;
using System.Linq;
using TimeSpace.Entities; 

namespace TimeSpace.UI
{
    public partial class FrmProfil : DevExpress.XtraEditors.XtraForm
    {
        ChronoTaleDBEntities db = new ChronoTaleDBEntities();

        
        int aktifID = Program.KullaniciID;

        public FrmProfil()
        {
            InitializeComponent();
        }

        private void FrmProfil_Load(object sender, EventArgs e)
        {
            if (aktifID == 0)
            {
                
                aktifID = 2; 
            }

            
            var kullanici = db.Tbl_Kullanicilar.Find(aktifID);

            if (kullanici != null)
            {
                
                lblKullaniciAdi.Text = kullanici.KullaniciAdi.ToUpper();

                txtEposta.Text = kullanici.Email;       
                txtSifre.Text = kullanici.SifreHash;    

                if (kullanici.ProfilResmi != null)
                {
                    using (MemoryStream ms = new MemoryStream(kullanici.ProfilResmi))
                    {
                        pictureEdit1.Image = Image.FromStream(ms);
                    }
                }
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                Tbl_Kullanicilar guncellenecekUye;

                using (ChronoTaleDBEntities db = new ChronoTaleDBEntities())
                {
                    guncellenecekUye = db.Tbl_Kullanicilar.AsNoTracking().FirstOrDefault(x => x.KullaniciID == aktifID);
                }

                if (guncellenecekUye != null)
                {
                    
                    guncellenecekUye.Email = txtEposta.Text;
                    guncellenecekUye.SifreHash = txtSifre.Text; 

                    
                    if (pictureEdit1.Image != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            pictureEdit1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            guncellenecekUye.ProfilResmi = ms.ToArray();
                        }
                    }

                   
                    DMLManager.Guncelle(guncellenecekUye);

                    FrmMesaj basarili = new FrmMesaj("Bilgileriniz başarıyla güncellendi!", "İşlem Tamam");
                    basarili.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                // Hatanın detayını görmek için
                FrmMesaj hata = new FrmMesaj("Kullanıcı bulunamadı!", "Hata");
                hata.ShowDialog();
            }
            
        }
        // RESME TIKLAYINCA BİLGİSAYARDAN FOTOĞRAF SEÇME KODU
        private void pictureEdit1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyası |*.jpg;*.jpeg;*.png;*.bmp";
            dosya.Title = "Profil Fotoğrafı Seç";

            if (dosya.ShowDialog() == DialogResult.OK)
            {
                pictureEdit1.Image = Image.FromFile(dosya.FileName);
            }
        }
    }
}