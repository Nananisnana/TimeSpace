using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using TimeSpace.DataAccess;
using TimeSpace.Entities;

namespace TimeSpace.UI
{
    public partial class FrmTarihSecimi : XtraForm
    {
        ChronoTaleDBEntities db = new ChronoTaleDBEntities();
        public int GelenDonemID { get; set; }
        public int SecilenYil { get; set; }

        public FrmTarihSecimi()
        {
            InitializeComponent();

           
            
            this.WindowState = FormWindowState.Maximized;
            this.DoubleBuffered = true;

            // Arka Plan
            try
            {
                string bgYolu = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "giris_bg.jpg");
                if (System.IO.File.Exists(bgYolu))
                    this.BackgroundImage = Image.FromFile(bgYolu);
                else
                    this.BackColor = Color.FromArgb(10, 10, 30);
            }
            catch { this.BackColor = Color.FromArgb(10, 10, 30); }
            this.BackgroundImageLayout = ImageLayout.Stretch;

            // --- OLAYLAR ---
            pnlOrta.Paint += pnlOrta_Paint;

            // Buton Olayları
            simpleButton1.Paint -= simpleButton1_Paint;
            simpleButton1.Paint += simpleButton1_Paint;
            simpleButton1.Click -= simpleButton1_Click;
            simpleButton1.Click += simpleButton1_Click;

            trackBarControl1.EditValueChanged += trackBarControl1_EditValueChanged;

            this.Resize += (s, e) => { BilesenleriHizala(); };
            pnlOrta.Resize += (s, e) => { BilesenleriHizala(); };

            
            Label lblGeri = new Label();
            lblGeri.Text = "🡰"; 
            lblGeri.Font = new Font("Segoe UI", 36, FontStyle.Bold); 
            lblGeri.ForeColor = Color.Cyan; 
            lblGeri.AutoSize = true;
            lblGeri.Location = new Point(20, 10); 
            lblGeri.Cursor = Cursors.Hand;

            
            lblGeri.Click += (s, e) =>
            {
                FrmDonemSecimi donem = new FrmDonemSecimi();
                donem.Show();
                this.Close(); 
            };
            this.Controls.Add(lblGeri);

           
        }

        private void FrmTarihSecimi_Load(object sender, EventArgs e)
        {
            if (GelenDonemID == 0) GelenDonemID = 1;

            try
            {
                var donem = db.Tbl_Donemler.FirstOrDefault(x => x.DonemID == GelenDonemID);
                if (donem != null)
                {
                    lblBaslik.Text = donem.DonemAdi.ToUpper();

                    int baslangic = (int)donem.BaslangicYili;
                    int bitis = (int)donem.BitisYili;

                    trackBarControl1.Properties.Minimum = baslangic;
                    trackBarControl1.Properties.Maximum = bitis;
                    trackBarControl1.Properties.TickFrequency = Math.Max(1, (bitis - baslangic) / 20);

                    lblMinTarih.Text = TarihFormatla(baslangic);
                    lblMaxTarih.Text = TarihFormatla(bitis);

                    int orta = (baslangic + bitis) / 2;
                    trackBarControl1.Value = orta;
                    TarihGuncelle(orta);
                }
            }
            catch (Exception ex) { MessageBox.Show("Veri hatası: " + ex.Message); }

            BilesenleriHizala();
        }

        private void BilesenleriHizala()
        {
            pnlOrta.Size = new Size(800, 450);
            pnlOrta.Location = new Point((this.Width - pnlOrta.Width) / 2, (this.Height - pnlOrta.Height) / 2);

            if (lblBaslik.Parent == pnlOrta)
            {
                lblBaslik.Parent = this;
                lblBaslik.BringToFront();
            }

            lblBaslik.AutoSize = true;
            lblBaslik.Font = new Font("Segoe UI", 36, FontStyle.Bold);
            lblBaslik.ForeColor = Color.White;
            lblBaslik.BackColor = Color.Transparent;
            lblBaslik.Location = new Point((this.Width - lblBaslik.Width) / 2, pnlOrta.Top - 100);

            int pW = pnlOrta.Width;
            int pH = pnlOrta.Height;

            lblSecilenTarih.AutoSize = true;
            lblSecilenTarih.Location = new Point((pW - lblSecilenTarih.Width) / 2, 80);

            trackBarControl1.AutoSize = false;
            trackBarControl1.Size = new Size(pW - 100, 45);
            trackBarControl1.Location = new Point(50, pH / 2 - 20);

            lblMinTarih.Location = new Point(50, trackBarControl1.Bottom + 5);
            lblMaxTarih.Location = new Point(pW - 50 - lblMaxTarih.Width, trackBarControl1.Bottom + 5);

            simpleButton1.Size = new Size(250, 60);
            simpleButton1.Location = new Point((pW - simpleButton1.Width) / 2, pH - 90);
        }

        private void trackBarControl1_EditValueChanged(object sender, EventArgs e)
        {
            TarihGuncelle(trackBarControl1.Value);
            lblSecilenTarih.Location = new Point((pnlOrta.Width - lblSecilenTarih.Width) / 2, 80);
        }

        private void TarihGuncelle(int yil)
        {
            SecilenYil = yil;
            lblSecilenTarih.Text = "Seçilen Tarih: " + TarihFormatla(yil);
        }

        private string TarihFormatla(int yil)
        {
            if (yil < 0) return "M.Ö. " + Math.Abs(yil);
            else return "M.S. " + yil;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FrmKarakterSecimi karakterFormu = new FrmKarakterSecimi();
            karakterFormu.GelenDonemID = this.GelenDonemID;
            karakterFormu.Show();
            this.Hide();
        }

       
        private void pnlOrta_Paint(object sender, PaintEventArgs e)
        {
            Panel pnl = sender as Panel; Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = pnl.ClientRectangle; rect.Width--; rect.Height--;
            GraphicsPath path = YuvarlakKose(rect, 40);

            using (SolidBrush brush = new SolidBrush(Color.FromArgb(160, 0, 0, 20)))
            { g.FillPath(brush, path); }

            using (Pen pen = new Pen(Color.Cyan, 2))
            { g.DrawPath(pen, path); }
        }

        private void simpleButton1_Paint(object sender, PaintEventArgs e)
        {
            SimpleButton btn = sender as SimpleButton; if (btn == null) return;
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = btn.ClientRectangle; rect.Width--; rect.Height--;
            GraphicsPath path = YuvarlakKose(rect, 30);

            g.Clear(Color.FromArgb(10, 10, 30));

            using (LinearGradientBrush brush = new LinearGradientBrush(rect,
                Color.FromArgb(0, 50, 100), Color.FromArgb(0, 150, 255), 0F))
            { g.FillPath(brush, path); }

            using (Pen pen = new Pen(Color.White, 2))
            { g.DrawPath(pen, path); }

            string yazi = "ONAYLA VE DEVAM ET";
            Font font = new Font("Segoe UI", 12, FontStyle.Bold);
            SizeF textSize = g.MeasureString(yazi, font);
            g.DrawString(yazi, font, Brushes.White,
                (btn.Width - textSize.Width) / 2, (btn.Height - textSize.Height) / 2);
        }

        private GraphicsPath YuvarlakKose(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath(); int d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90); path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90); path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure(); return path;
        }
    }
}