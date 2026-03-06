using System;
using System.Drawing;
using System.Drawing.Drawing2D; 
using System.Windows.Forms;
using System.Data.SqlClient;
using TimeSpace.Business;    
using TimeSpace.DataAccess;  
namespace TimeSpace.UI
{
    public partial class FrmKayit01 : Form
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ChronoTaleDB;Integrated Security=True";

        DevExpress.XtraEditors.TextEdit txtAdSoyad, txtKullaniciAdi, txtEmail, txtSifre, txtYas, txtCinsiyet;
        
        Panel pnlCamKutu;

        public FrmKayit01()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            string bgYolu = System.IO.Path.Combine(Application.StartupPath, "giris_bg.jpg");
            if (System.IO.File.Exists(bgYolu))
                this.BackgroundImage = Image.FromFile(bgYolu);
            else
                this.BackColor = Color.FromArgb(10, 10, 20);

            this.BackgroundImageLayout = ImageLayout.Stretch;

            ArayuzuOlustur();
        }
        private void ArayuzuOlustur()
        {
            // --- 1. CAM KUTU (ANA PANEL) ---
            pnlCamKutu = new Panel();
            pnlCamKutu.Size = new Size(1000, 700);
            pnlCamKutu.Location = new Point((Screen.PrimaryScreen.Bounds.Width - 1000) / 2, (Screen.PrimaryScreen.Bounds.Height - 700) / 2);
            pnlCamKutu.BackColor = Color.Transparent;
            pnlCamKutu.Paint += PnlCamKutu_Paint;
            this.Controls.Add(pnlCamKutu);

            // --- 2. ÇIKIŞ BUTONU (GÜNCELLENDİ: Ana Menüye Gider) ---
            Label lblCikis = new Label();
            lblCikis.Text = "✕";
            lblCikis.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblCikis.ForeColor = Color.White;
            lblCikis.AutoSize = true;
            lblCikis.Cursor = Cursors.Hand;
            lblCikis.Location = new Point(pnlCamKutu.Width - 60, 25);
            lblCikis.Click += (s, e) =>
            {
                 FrmMesaj.Goster("Ana Menüye Yönlendiriliyorsunuz...", "YÖNLENDİRME");
                 FrmAnaMenu anaMenu = new FrmAnaMenu(); 
                 anaMenu.Show();
                 this.Hide();

                
            };
            pnlCamKutu.Controls.Add(lblCikis);

            // --- 3. BAŞLIK VE SLOGAN ---
            Label lblBaslik = new Label();
            lblBaslik.Text = "ZAMANDA YOLCULUĞA BAŞLA";
            lblBaslik.Font = new Font("Segoe UI", 28, FontStyle.Bold);
            lblBaslik.ForeColor = Color.White;
            lblBaslik.AutoSize = true;
            pnlCamKutu.Controls.Add(lblBaslik);
            lblBaslik.Location = new Point((pnlCamKutu.Width - lblBaslik.PreferredWidth) / 2, 50);

            Label lblSlogan = new Label();
            lblSlogan.Text = "\"Geçmişin sırları seni bekliyor...\"";
            lblSlogan.Font = new Font("Segoe UI", 14, FontStyle.Italic);
            lblSlogan.ForeColor = Color.MediumPurple;
            lblSlogan.AutoSize = true;
            pnlCamKutu.Controls.Add(lblSlogan);
            lblSlogan.Location = new Point((pnlCamKutu.Width - lblSlogan.PreferredWidth) / 2, 100);

            
            int solX = 80;
            int sagX = 540;
            int baslangicY = 180;
            int aralik = 110;
            int kutuGenislik = 380;

            // --- SOL SÜTUN ---
            BolumBasligiEkle("HESAP BİLGİLERİ", solX, baslangicY - 40);

            // Kullanıcı Adı 
            OlusturEtiket("Kullanıcı Adı", solX, baslangicY);
            txtKullaniciAdi = DevExpressKutuOlustur(solX, baslangicY + 30, kutuGenislik);
            pnlCamKutu.Controls.Add(txtKullaniciAdi);

            // E-Posta
            OlusturEtiket("E-Posta Adresi", solX, baslangicY + aralik);
            txtEmail = DevExpressKutuOlustur(solX, baslangicY + aralik + 30, kutuGenislik);
            pnlCamKutu.Controls.Add(txtEmail);

            // Şifre
            OlusturEtiket("Güvenli Şifre", solX, baslangicY + (aralik * 2));
            txtSifre = DevExpressKutuOlustur(solX, baslangicY + (aralik * 2) + 30, kutuGenislik);
            txtSifre.Properties.UseSystemPasswordChar = true; // Şifreli görünüm
            pnlCamKutu.Controls.Add(txtSifre);

            // --- SAĞ SÜTUN ---
            BolumBasligiEkle("KİŞİSEL DETAYLAR", sagX, baslangicY - 40);

            // Ad Soyad
            OlusturEtiket("Ad Soyad", sagX, baslangicY);
            txtAdSoyad = DevExpressKutuOlustur(sagX, baslangicY + 30, kutuGenislik);
            pnlCamKutu.Controls.Add(txtAdSoyad);

            // Yaş
            OlusturEtiket("Yaş", sagX, baslangicY + aralik);
            txtYas = DevExpressKutuOlustur(sagX, baslangicY + aralik + 30, 150);
            txtYas.Properties.Mask.EditMask = "d"; // Sadece sayı girilmesine izin ver
            txtYas.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            pnlCamKutu.Controls.Add(txtYas);
            OlusturEtiket("Cinsiyet", sagX, baslangicY + (aralik * 2));
            txtCinsiyet = DevExpressKutuOlustur(sagX, baslangicY + (aralik * 2) + 30, kutuGenislik);
            pnlCamKutu.Controls.Add(txtCinsiyet);

            // --- KAYIT BUTONU ---
            Button btnKayit = new Button();
            btnKayit.Text = "KAYIT İŞLEMİNİ TAMAMLA";
            btnKayit.Size = new Size(840, 70);
            btnKayit.Location = new Point(80, 560);
            btnKayit.FlatStyle = FlatStyle.Flat;
            btnKayit.FlatAppearance.BorderSize = 0;
            btnKayit.ForeColor = Color.White;
            btnKayit.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            btnKayit.Cursor = Cursors.Hand;
            btnKayit.Paint += (s, ev) => {
                using (LinearGradientBrush brush = new LinearGradientBrush(btnKayit.ClientRectangle,
                    Color.FromArgb(0, 200, 150), Color.FromArgb(0, 100, 200), 45F))
                {
                    GraphicsPath path = YuvarlakKose(btnKayit.ClientRectangle, 35);
                    btnKayit.Region = new Region(path);
                    ev.Graphics.FillPath(brush, path);
                    SizeF textSize = ev.Graphics.MeasureString(btnKayit.Text, btnKayit.Font);
                    ev.Graphics.DrawString(btnKayit.Text, btnKayit.Font, Brushes.White,
                        (btnKayit.Width - textSize.Width) / 2, (btnKayit.Height - textSize.Height) / 2);
                }
            };
            btnKayit.Click += BtnKayit_Click; 
            pnlCamKutu.Controls.Add(btnKayit);
            Label lblGiris = new Label();
            lblGiris.Text = "Zaten bir hesabın var mı? Giriş Yap";
            lblGiris.Font = new Font("Segoe UI", 11, FontStyle.Underline);
            lblGiris.ForeColor = Color.DeepSkyBlue;
            lblGiris.AutoSize = true;
            lblGiris.Cursor = Cursors.Hand;
            lblGiris.Click += (s, ev) => { FrmGiris giris = new FrmGiris(); giris.Show(); this.Hide(); };
            pnlCamKutu.Controls.Add(lblGiris);
            lblGiris.Location = new Point((pnlCamKutu.Width - lblGiris.PreferredWidth) / 2, 650);
        }
        private DevExpress.XtraEditors.TextEdit DevExpressKutuOlustur(int x, int y, int genislik)
        {
            DevExpress.XtraEditors.TextEdit txt = new DevExpress.XtraEditors.TextEdit();
            txt.Properties.AutoHeight = false; 
            txt.SetBounds(x, y, genislik, 55);
            txt.BackColor = Color.FromArgb(45, 45, 55);
            txt.ForeColor = Color.White;
            txt.Font = new Font("Segoe UI", 16);
            txt.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            txt.Properties.Padding = new Padding(10, 10, 10, 10);

            return txt;
        }

        // --- VERİTABANI KAYIT ---
        private void BtnKayit_Click(object sender, EventArgs e)
        {
            // 1. BOŞ ALAN KONTROLÜ
            if (string.IsNullOrEmpty(txtAdSoyad.Text) || string.IsNullOrEmpty(txtKullaniciAdi.Text) ||
                string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtSifre.Text))
            {
                FrmMesaj.Goster("Lütfen zorunlu alanları doldurunuz.", "EKSİK BİLGİ");
                return;
            }

            // 2. GÜVENLİK KONTROLÜ (Yasaklı Kelime)
            string kadiKontrol = txtKullaniciAdi.Text.Trim().ToLower();
            string mailKontrol = txtEmail.Text.Trim().ToLower();

            if (kadiKontrol.Contains("admin") || mailKontrol.Contains("admin"))
            {
                FrmMesaj.Goster("Bu kullanıcı adı sistem tarafından korunmaktadır.\nLütfen başka bir isim seçiniz.", "YASAKLI KULLANICI");
                return;
            }
            try
            {
                Tbl_Kullanicilar yeniUye = new Tbl_Kullanicilar();

                yeniUye.AdSoyad = txtAdSoyad.Text;
                yeniUye.KullaniciAdi = txtKullaniciAdi.Text;
                yeniUye.Email = txtEmail.Text;
                yeniUye.SifreHash = txtSifre.Text; 
                int girilenYas = 0;
                int.TryParse(txtYas.Text, out girilenYas);
                yeniUye.Yas = girilenYas;

                yeniUye.Cinsiyet = txtCinsiyet.Text;
                yeniUye.KullaniciTipi = "Uye"; 
                yeniUye.KayitTarihi = DateTime.Now;

                // 2. ADIM: DMLManager ile KayIT
                DMLManager.Ekle(yeniUye);

                // 3. Başarılı Mesajı
                FrmMesaj.Goster("Kayıt Başarılı!\nGiriş ekranına yönlendiriliyorsunuz.", "HOŞGELDİN");

               
                FrmGiris giris = new FrmGiris();
                giris.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                
                FrmMesaj.Goster("Veritabanı Hatası:\n" + ex.Message, "HATA");
            }
            
        }
        // --- GÖRSEL YARDIMCILAR ---

        private void BolumBasligiEkle(string baslik, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = baslik;
            lbl.ForeColor = Color.Cyan; 
            lbl.Font = new Font("Segoe UI", 16, FontStyle.Bold); 
            lbl.AutoSize = true;
            lbl.Location = new Point(x, y);
            pnlCamKutu.Controls.Add(lbl);
        }

        private void OlusturEtiket(string metin, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = metin; 
            lbl.ForeColor = Color.Silver;
            lbl.Font = new Font("Segoe UI", 12);
            lbl.AutoSize = true;
            lbl.Location = new Point(x, y);
            pnlCamKutu.Controls.Add(lbl);
        }

        private Panel OzelTextBoxPanel(int x, int y, int width)
        {
            Panel pnl = OzelPanel(x, y, width);
            TextBox txt = new TextBox();
            txt.BorderStyle = BorderStyle.None;
            txt.BackColor = Color.FromArgb(45, 45, 55);
            txt.ForeColor = Color.White;
            txt.Font = new Font("Segoe UI", 14); 
            txt.Width = width - 30;
            txt.Location = new Point(15, 12);
            pnl.Controls.Add(txt);
            return pnl;
        }

        private Panel OzelPanel(int x, int y, int width)
        {
            Panel pnl = new Panel();
            pnl.Size = new Size(width, 50);
            pnl.Location = new Point(x, y);
            pnl.BackColor = Color.Transparent;
            pnl.Paint += (s, e) => {
                using (GraphicsPath path = YuvarlakKose(pnl.ClientRectangle, 15))
                {
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(45, 45, 55)))
                        e.Graphics.FillPath(brush, path);
                    using (Pen pen = new Pen(Color.DimGray, 1))
                        e.Graphics.DrawPath(pen, path);
                }
            };
            return pnl;
        }

        private void PnlCamKutu_Paint(object sender, PaintEventArgs e)
        {
            using (GraphicsPath path = YuvarlakKose(pnlCamKutu.ClientRectangle, 40))
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(180, 10, 10, 20)))
                    e.Graphics.FillPath(brush, path);
                using (Pen pen = new Pen(Color.FromArgb(100, 0, 255, 255), 2))
                    e.Graphics.DrawPath(pen, path);
            }
        }

        private GraphicsPath YuvarlakKose(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}