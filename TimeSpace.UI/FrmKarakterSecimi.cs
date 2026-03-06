using DevExpress.XtraEditors;
using System;
using System.Drawing; 
using System.Linq;
using System.Windows.Forms;
using TimeSpace.DataAccess;
using TimeSpace.Entities;

namespace TimeSpace.UI
{
    public partial class FrmKarakterSecimi : DevExpress.XtraEditors.XtraForm
    {
        // 1. Veritabanı bağlantısı
        ChronoTaleDBEntities db = new ChronoTaleDBEntities();

        // Önceki sayfadan gelen veriler
        public int GelenDonemID;

        public FrmKarakterSecimi()
        {
            InitializeComponent();

            // --- FORM AYARLARI ---
            
            this.WindowState = FormWindowState.Maximized;
            this.DoubleBuffered = true;

            
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

            // --- TILE CONTROL AYARLARI (Kartların duracağı yer) ---
            tileControl1.BackColor = Color.Transparent; 
            tileControl1.Orientation = Orientation.Horizontal; 
            tileControl1.ItemSize = 180; 

            // --- GERİ BUTONU (SOL ÜST) ---
            Label lblGeri = new Label();
            lblGeri.Text = "🡰"; 
            lblGeri.Font = new Font("Segoe UI", 36, FontStyle.Bold);
            lblGeri.ForeColor = Color.Cyan;
            lblGeri.AutoSize = true;
            lblGeri.Location = new Point(20, 10);
            lblGeri.Cursor = Cursors.Hand;
            lblGeri.Click += btnGeriDon_Click; 
            this.Controls.Add(lblGeri);

            // --- BAŞLIK EKLE ---
            Label lblBaslik = new Label();
            lblBaslik.Text = "KARAKTERİNİ SEÇ";
            lblBaslik.Font = new Font("Segoe UI", 32, FontStyle.Bold);
            lblBaslik.ForeColor = Color.White;
            lblBaslik.AutoSize = true;
            lblBaslik.BackColor = Color.Transparent;
            lblBaslik.Name = "lblBaslik";
            this.Controls.Add(lblBaslik);

           
        }

        private void FrmKarakterSecimi_Load(object sender, EventArgs e)
        {
            Control lblBaslik = this.Controls["lblBaslik"];
            if (lblBaslik != null)
            {
                lblBaslik.Location = new Point((this.Width - lblBaslik.Width) / 2, 50);
            }
            tileControl1.Location = new Point(50, 150);
            tileControl1.Size = new Size(this.Width - 100, this.Height - 200);

            if (GelenDonemID == 0) GelenDonemID = 1;

            tileControl1.Groups[0].Items.Clear();
            var karakterListesi = db.Tbl_Karakterler.Where(x => x.DonemID == GelenDonemID).ToList();

            if (karakterListesi.Count == 0)
            {
                XtraMessageBox.Show("Bu dönem için henüz karakter girilmemiş!", "Bilgi");
                return;
            }

            foreach (var k in karakterListesi)
            {
                TileItem kutu = new TileItem();
                kutu.ItemSize = TileItemSize.Large;
                kutu.AppearanceItem.Normal.BackColor = Color.FromArgb(0, 50, 100);
                kutu.AppearanceItem.Normal.BorderColor = Color.Cyan;
                kutu.AppearanceItem.Hovered.BackColor = Color.FromArgb(0, 100, 200);
                TileItemElement baslik = new TileItemElement();
                baslik.Text = k.AdSoyad.ToUpper();
                baslik.TextAlignment = TileItemContentAlignment.TopCenter;
                baslik.Appearance.Normal.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                TileItemElement aciklama = new TileItemElement();
                aciklama.Text = k.Aciklama;
                aciklama.TextAlignment = TileItemContentAlignment.MiddleCenter;
                aciklama.Appearance.Normal.Font = new Font("Segoe UI", 10, FontStyle.Italic);
                aciklama.Appearance.Normal.Options.UseTextOptions = true;
                aciklama.Appearance.Normal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

                kutu.Elements.Add(baslik);
                kutu.Elements.Add(aciklama);
                kutu.Tag = k.KarakterID;

                tileControl1.Groups[0].Items.Add(kutu);
            }
        }

        private void tileControl1_ItemClick(object sender, TileItemEventArgs e)
        {
            if (e.Item == null || e.Item.Tag == null) return;

            int secilenID = Convert.ToInt32(e.Item.Tag);
            var secilenKarakter = db.Tbl_Karakterler.Find(secilenID);
            var secilenDonem = db.Tbl_Donemler.Find(GelenDonemID);

            if (secilenKarakter != null && secilenDonem != null)
            {
                FrmHikaye oyunFormu = new FrmHikaye();
                oyunFormu.GelenKarakterAd = secilenKarakter.AdSoyad;
                oyunFormu.GelenOzellikler = secilenKarakter.Aciklama;
                oyunFormu.GelenDonem = secilenDonem.DonemAdi;

                oyunFormu.Show();
                this.Hide();
            }
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            FrmTarihSecimi tarihSecimi = new FrmTarihSecimi();
            tarihSecimi.GelenDonemID = this.GelenDonemID;

            tarihSecimi.Show();
            this.Close();
        }
    }
}