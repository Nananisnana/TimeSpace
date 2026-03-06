using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TimeSpace.Business; 
using DevExpress.XtraEditors;

namespace TimeSpace.UI
{
    public partial class FrmGiris : DevExpress.XtraEditors.XtraForm
    {
        Size orjinalBoyut;
        Point orjinalKonum;
        Size orjinalKayitBoyut;
        Point orjinalKayitKonum;
        public FrmGiris()
        {
            InitializeComponent();


        }

        
        private void FrmGiris_Load(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            txtEposta.Left = (pnlGirisKutusu.Width - txtEposta.Width) / 2;

            // 2. Şifre Kutusunu Ortala
            txtSifre.Left = (pnlGirisKutusu.Width - txtSifre.Width) / 2;

            // 3. Butonu Ortala
            btnGiris.Left = (pnlGirisKutusu.Width - btnGiris.Width) / 2;
            orjinalBoyut = btnGiris.Size;
            orjinalKonum = btnGiris.Location;
            btnGiris.Left = (pnlGirisKutusu.Width - btnGiris.Width) / 2;

            // 2. Kayıt Butonunu Giriş Butonunun Altına geliyor böyle
            btnKayit.Size = btnGiris.Size; 
            btnKayit.Left = btnGiris.Left; 
            btnKayit.Top = btnGiris.Bottom + 15;
            orjinalKayitBoyut = btnKayit.Size;
            orjinalKayitKonum = btnKayit.Location;
        }

        private void txtEposta_EditValueChanged(object sender, EventArgs e)
        {

        }
        // 1. Yuvarlak Köşe + İnce Çizgi
        private void pnlGirisKutusu_Paint(object sender, PaintEventArgs e)
        {
            DevExpress.XtraEditors.PanelControl panel = sender as DevExpress.XtraEditors.PanelControl;
            if (panel == null) return;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int r = 40; 
            Rectangle rect = panel.ClientRectangle;
            rect.Width--; rect.Height--;

            path.AddArc(rect.X, rect.Y, r, r, 180, 90);
            path.AddArc(rect.Right - r, rect.Y, r, r, 270, 90);
            path.AddArc(rect.Right - r, rect.Bottom - r, r, r, 0, 90);
            path.AddArc(rect.X, rect.Bottom - r, r, r, 90, 90);
            path.CloseFigure();

            
            panel.Region = new Region(path);

            
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(180, 0, 0, 0)))
            {
                e.Graphics.FillPath(brush, path);
            }

            // Beyaz Çerçeve (Parlaklık Efekti)
            using (Pen pen = new Pen(Color.FromArgb(50, 255, 255, 255), 1))
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        // 2.Neon Mavi Gradient
        private void btnGiris_Paint(object sender, PaintEventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton btn = sender as DevExpress.XtraEditors.SimpleButton;
            if (btn == null) return;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Rectangle rect = btn.ClientRectangle;
            rect.Inflate(-2, -2);

           
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int r = 20;
            path.AddArc(rect.X, rect.Y, r, r, 180, 90);
            path.AddArc(rect.Right - r, rect.Y, r, r, 270, 90);
            path.AddArc(rect.Right - r, rect.Bottom - r, r, r, 0, 90);
            path.AddArc(rect.X, rect.Bottom - r, r, r, 90, 90);
            path.CloseFigure();

            btn.Region = new Region(path);

          
            using (System.Drawing.Drawing2D.LinearGradientBrush brush =
                   new System.Drawing.Drawing2D.LinearGradientBrush(rect,
                   Color.FromArgb(0, 110, 255), 
                   Color.FromArgb(0, 10, 50),   
                   90F))
            {
                e.Graphics.FillPath(brush, path);
            }
            using (Pen penGlow = new Pen(Color.FromArgb(50, 0, 255, 255), 6)) 
            {
                e.Graphics.DrawPath(penGlow, path);
            }
            using (Pen penNeon = new Pen(Color.Cyan, 2)) 
            {
                e.Graphics.DrawPath(penNeon, path);
            }
            Font butonFontu = new Font(btn.Appearance.Font.FontFamily, 12, FontStyle.Bold);

            SizeF textSize = e.Graphics.MeasureString(btn.Text, butonFontu);
            e.Graphics.DrawString(btn.Text, butonFontu, Brushes.White,
                (btn.Width - textSize.Width) / 2, (btn.Height - textSize.Height) / 2);
        }


        private void btnGiris_Click(object sender, EventArgs e)
        {
            
            string gelenEposta = txtEposta.Text.Trim();
            string sifre = txtSifre.Text.Trim();
            if (string.IsNullOrEmpty(gelenEposta) || gelenEposta == "Kullanıcı Adı veya E-Posta" ||
                    string.IsNullOrEmpty(sifre) || sifre == "Şifre")
            {
                FrmMesaj.Goster("Lütfen kullanıcı bilgilerinizi eksiksiz giriniz.", "EKSİK BİLGİ");
                return;
            }
            if (gelenEposta == "admin" && sifre == "1234")
            {
                MessageBox.Show("Yönetici girişi algılandı. Admin paneline yönlendiriliyorsunuz...", "Hoşgeldin Admin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FrmAdminPaneli adminForm = new FrmAdminPaneli();
                adminForm.Show();
                this.Hide(); 
                return; 
            }
            try
            {
                TimeSpace.Business.SKullanici servis = new TimeSpace.Business.SKullanici();
                var kullanici = servis.GirisYap(gelenEposta, sifre);

                if (kullanici != null)
                {
                    Program.KullaniciID = kullanici.KullaniciID;
                    string mesaj = $"Hoşgeldin {kullanici.AdSoyad}!\nSisteme başarıyla giriş yaptın.";
                    FrmMesaj.Goster(mesaj, "GİRİŞ BAŞARILI");
                    FrmDonemSecimi donemSecim = new FrmDonemSecimi();
                    donemSecim.Show();
                    this.Hide();


                    
                }
                else
                {
                    FrmMesaj.Goster("Hatalı E-posta veya Şifre girdiniz!", "GİRİŞ BAŞARISIZ");
                }
            }
            catch (Exception ex)
            {
                FrmMesaj.Goster("Veritabanı Hatası: " + ex.Message, "HATA");
            }
        }
        

       
        private void btnGiris_MouseEnter(object sender, EventArgs e)
        {
            btnGiris.Size = new Size(orjinalBoyut.Width + 10, orjinalBoyut.Height + 4);
            btnGiris.Location = new Point(orjinalKonum.X - 5, orjinalKonum.Y - 2);
            btnGiris.Cursor = Cursors.Hand;
        }

        private void btnGiris_MouseLeave(object sender, EventArgs e)
        {
           
            btnGiris.Size = orjinalBoyut;
            btnGiris.Location = orjinalKonum;

            btnGiris.Cursor = Cursors.Default;
        }
        private void btnKayit_Paint(object sender, PaintEventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton btn = sender as DevExpress.XtraEditors.SimpleButton;
            if (btn == null) return;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Rectangle rect = btn.ClientRectangle;
            rect.Width--; rect.Height--;
            rect.Inflate(-1, -1);
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int r = 20;
            path.AddArc(rect.X, rect.Y, r, r, 180, 90);
            path.AddArc(rect.Right - r, rect.Y, r, r, 270, 90);
            path.AddArc(rect.Right - r, rect.Bottom - r, r, r, 0, 90);
            path.AddArc(rect.X, rect.Bottom - r, r, r, 90, 90);
            path.CloseFigure();

            btn.Region = new Region(path);

            // Mouse Kontrolü
            bool mouseUzerinde = btn.ClientRectangle.Contains(btn.PointToClient(MousePosition));
            Color zeminRengi = mouseUzerinde ? Color.FromArgb(200, 100, 255, 255) : Color.FromArgb(50, 0, 0, 0);

            using (SolidBrush brush = new SolidBrush(zeminRengi))
            {
                e.Graphics.FillPath(brush, path);
            }

            Color cerceveRengi = mouseUzerinde ? Color.Cyan : Color.FromArgb(100, 255, 255, 255);
            int kalinlik = mouseUzerinde ? 2 : 1;

            using (Pen pen = new Pen(cerceveRengi, kalinlik))
            {
                e.Graphics.DrawPath(pen, path);
            }

         
            string yazi = "KAYIT OL";

            Brush yaziFircasi = mouseUzerinde ? Brushes.Black : Brushes.White;

            Font butonFontu = new Font(btn.Appearance.Font.FontFamily, 11, FontStyle.Bold);
            SizeF textSize = e.Graphics.MeasureString(yazi, butonFontu);

            e.Graphics.DrawString(yazi, butonFontu, yaziFircasi,
                (btn.Width - textSize.Width) / 2, (btn.Height - textSize.Height) / 2);
        }
        private void btnKayit_MouseEnter(object sender, EventArgs e)
        {
            
            btnKayit.Size = new Size(orjinalKayitBoyut.Width + 10, orjinalKayitBoyut.Height + 4);
            
            btnKayit.Location = new Point(orjinalKayitKonum.X - 5, orjinalKayitKonum.Y - 2);
            btnKayit.Cursor = Cursors.Hand;
            btnKayit.Invalidate();
        }

        private void btnKayit_MouseLeave(object sender, EventArgs e)
        {
            btnKayit.Size = orjinalKayitBoyut;
            btnKayit.Location = orjinalKayitKonum;
            btnKayit.Cursor = Cursors.Default;

            btnKayit.Invalidate();
        }
        private void btnKayit_Click(object sender, EventArgs e)
        {
            FrmKayit01 kayitForm = new FrmKayit01();
            kayitForm.Show();
            this.Hide();
        }
    
    }
}