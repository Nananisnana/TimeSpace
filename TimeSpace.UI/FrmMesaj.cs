using System;
using System.Drawing;
using System.Drawing.Drawing2D; 
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace TimeSpace.UI
{
    public partial class FrmMesaj : XtraForm
    {
        
        private bool surukleniyor = false;
        private Point baslangicNoktasi;

        // 1. YAPICI METOT (Constructor)
        public FrmMesaj(string mesaj, string baslik)
        {
            InitializeComponent();

            // --- FORM AYARLARI ---
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.Size = new Size(450, 220);
            this.StartPosition = FormStartPosition.CenterScreen;

            // --- BAŞLIK AYARLARI ---
            
            lblBaslik.Text = baslik.ToUpper();
            lblBaslik.Appearance.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblBaslik.Appearance.ForeColor = Color.Cyan;

            lblBaslik.AutoSizeMode = LabelAutoSizeMode.None;
            lblBaslik.Size = new Size(this.Width - 20, 30);
            lblBaslik.Location = new Point(10, 20);
            lblBaslik.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            // --- MESAJ METNİ AYARLARI ---
            lblMesaj.Text = mesaj;
            lblMesaj.Appearance.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            lblMesaj.Appearance.ForeColor = Color.White;

            lblMesaj.AutoSizeMode = LabelAutoSizeMode.None;
            lblMesaj.Size = new Size(this.Width - 40, 80);
            lblMesaj.Location = new Point(20, 60);
            lblMesaj.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            lblMesaj.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            lblMesaj.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            // --- BUTON KONUMU ---
            btnTamam.Size = new Size(120, 40);
            btnTamam.Location = new Point((this.Width - btnTamam.Width) / 2, this.Height - 60);

            // --- OLAYLARI BAĞLA ---
            this.Paint += FrmMesaj_Paint;
            this.MouseDown += FrmMesaj_MouseDown;
            this.MouseMove += FrmMesaj_MouseMove;
            this.MouseUp += FrmMesaj_MouseUp;
            btnTamam.Click -= btnTamam_Click;
            btnTamam.Click += btnTamam_Click;

            btnTamam.Paint += BtnTamam_Paint;
        }

        private void btnTamam_Click(object sender, EventArgs e)
        {
            
            this.DialogResult = DialogResult.Yes;

            this.Close();
        }

        
        private void FrmMesaj_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = this.ClientRectangle;
            rect.Width--; rect.Height--;

            GraphicsPath path = new GraphicsPath();
            int r = 20;
            path.AddArc(rect.X, rect.Y, r, r, 180, 90);
            path.AddArc(rect.Right - r, rect.Y, r, r, 270, 90);
            path.AddArc(rect.Right - r, rect.Bottom - r, r, r, 0, 90);
            path.AddArc(rect.X, rect.Bottom - r, r, r, 90, 90);
            path.CloseFigure();

            this.Region = new Region(path);

            using (SolidBrush brush = new SolidBrush(Color.FromArgb(240, 10, 10, 20)))
            {
                e.Graphics.FillPath(brush, path);
            }

            using (Pen pen = new Pen(Color.Cyan, 2))
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        
        private void BtnTamam_Paint(object sender, PaintEventArgs e)
        {
            SimpleButton btn = sender as SimpleButton;
            if (btn == null) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = btn.ClientRectangle;
            rect.Width--; rect.Height--;

            GraphicsPath path = new GraphicsPath();
            int r = 15;
            path.AddArc(rect.X, rect.Y, r, r, 180, 90);
            path.AddArc(rect.Right - r, rect.Y, r, r, 270, 90);
            path.AddArc(rect.Right - r, rect.Bottom - r, r, r, 0, 90);
            path.AddArc(rect.X, rect.Bottom - r, r, r, 90, 90);
            path.CloseFigure();

            btn.Region = new Region(path);

            using (LinearGradientBrush brush = new LinearGradientBrush(rect,
                    Color.FromArgb(0, 100, 200), Color.FromArgb(0, 50, 100), 90F))
            {
                e.Graphics.FillPath(brush, path);
            }

            using (Pen pen = new Pen(Color.Cyan, 2))
            {
                e.Graphics.DrawPath(pen, path);
            }

            string text = btn.Text;
            Font font = new Font(btn.Appearance.Font.FontFamily, 10, FontStyle.Bold);
            SizeF textSize = e.Graphics.MeasureString(text, font);
            e.Graphics.DrawString(text, font, Brushes.White,
                (btn.Width - textSize.Width) / 2, (btn.Height - textSize.Height) / 2);
        }

        
        private void FrmMesaj_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                surukleniyor = true;
                baslangicNoktasi = e.Location;
            }
        }

        private void FrmMesaj_MouseMove(object sender, MouseEventArgs e)
        {
            if (surukleniyor)
            {
                Point yeniKonum = this.Location;
                yeniKonum.X += e.X - baslangicNoktasi.X;
                yeniKonum.Y += e.Y - baslangicNoktasi.Y;
                this.Location = yeniKonum;
            }
        }

        private void FrmMesaj_MouseUp(object sender, MouseEventArgs e)
        {
            surukleniyor = false;
        }

        
        public static void Goster(string mesaj, string baslik = "BİLGİ")
        {
            Form shadow = new Form();
            shadow.ShowInTaskbar = false;
            shadow.FormBorderStyle = FormBorderStyle.None;
            shadow.Opacity = 0.5;
            shadow.BackColor = Color.Black;
            shadow.WindowState = FormWindowState.Maximized;
            shadow.TopMost = true;
            shadow.Show();

            FrmMesaj msg = new FrmMesaj(mesaj, baslik);
            msg.TopMost = true;
            msg.ShowDialog();

            shadow.Close();
        }
    }
}