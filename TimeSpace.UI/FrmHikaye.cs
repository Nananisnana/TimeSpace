using DevExpress.XtraEditors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeSpace.DataAccess; 
using TimeSpace.Entities;
using TimeSpace.Business;

namespace TimeSpace.UI
{
    public partial class FrmHikaye : XtraForm
    {
        public string GelenDonem { get; set; }
        public string GelenKarakterAd { get; set; }
        public string GelenOzellikler { get; set; }

        private int _turSayisi = 0;
        private const int OYUN_LIMITI = 4;
        private string _hikayeGecmisi = "";
        private string _toplamHikayeMetni = "";
        private bool _butonlarAktifMi = false;
        private string _sabitKarakterTarifi = "";
        private Color _anaRenk = Color.FromArgb(220, 10, 10, 30);

        private Panel pnlAnaKutu;
        private PictureBox pbGorsel;
        private SimpleButton btnKaydet;
        private SimpleButton btnGeri;

        public FrmHikaye()
        {
            InitializeComponent();
            KurulumuYap();
        }

        private void KurulumuYap()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.DoubleBuffered = true;
            bgAyarlar();

            // Gri Şeridi (GroupControl) Gizle
            foreach (Control ctrl in this.Controls) { if (ctrl is GroupControl) ctrl.Visible = false; }

            // 1. ANA PANEL
            pnlAnaKutu = new Panel();
            pnlAnaKutu.BackColor = Color.Transparent;
            pnlAnaKutu.Paint += PnlCamKutu_Paint;
            this.Controls.Add(pnlAnaKutu);

            // 2. RESİM
            pbGorsel = new PictureBox();
            pbGorsel.BackColor = Color.Transparent;
            pbGorsel.SizeMode = PictureBoxSizeMode.Zoom;
            pnlAnaKutu.Controls.Add(pbGorsel);

            // 3. YAZI KUTUSU
            memoHikaye.Parent = pnlAnaKutu;
            memoHikaye.BackColor = _anaRenk;
            memoHikaye.Properties.Appearance.BackColor = _anaRenk;
            memoHikaye.Properties.Appearance.Options.UseBackColor = true;
            memoHikaye.Properties.Appearance.ForeColor = Color.White;
            memoHikaye.Properties.Appearance.Font = new Font("Segoe UI", 18, FontStyle.Regular);
            memoHikaye.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            memoHikaye.Properties.ScrollBars = ScrollBars.None;
            memoHikaye.Properties.ReadOnly = true;
            memoHikaye.Padding = new Padding(15);

            // 4. BUTONLAR
            Font btnFont = new Font("Segoe UI", 12, FontStyle.Bold);
            ButonGorselAyarla(btnSecenek1, btnFont);
            ButonGorselAyarla(btnSecenek2, btnFont);
            ButonGorselAyarla(btnSecenek3, btnFont);
            btnSecenek1.Click -= btnSecenek1_Click; btnSecenek1.Click += btnSecenek1_Click;
            btnSecenek2.Click -= btnSecenek2_Click; btnSecenek2.Click += btnSecenek2_Click;
            btnSecenek3.Click -= btnSecenek3_Click; btnSecenek3.Click += btnSecenek3_Click;

            // Kaydet Butonu
            btnKaydet = new SimpleButton(); btnKaydet.Text = "MACERAYI KAYDET 💾";
            btnKaydet.Appearance.Font = btnFont; btnKaydet.Visible = false;
            btnKaydet.Paint += BtnNeon_Paint; btnKaydet.Click += BtnKaydet_Click; this.Controls.Add(btnKaydet);

            // Geri Butonu
            btnGeri = new SimpleButton(); btnGeri.Text = "←";
            btnGeri.Appearance.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            btnGeri.Size = new Size(60, 60); btnGeri.Location = new Point(30, 30);
            btnGeri.Cursor = Cursors.Hand;
            btnGeri.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            btnGeri.Appearance.BackColor = Color.Transparent;
            btnGeri.Appearance.Options.UseBackColor = true;
            btnGeri.Paint += BtnNeon_Paint;
            btnGeri.Click += btnGeriDon_Click;
            this.Controls.Add(btnGeri); btnGeri.BringToFront();

            ButonlariYonet(false);
        }

        private void EkranDuzeniniAyarla()
        {
            int w = this.ClientSize.Width; int h = this.ClientSize.Height;
            if (w < 100) return;

            int bosluk = 40; int btnH = 70;
            int btnW = (w - (bosluk * 4)) / 3;
            int btnY = h - btnH - 60;

            Yerlestir(btnSecenek1, bosluk, btnY, btnW, btnH);
            Yerlestir(btnSecenek2, bosluk + btnW + bosluk, btnY, btnW, btnH);
            Yerlestir(btnSecenek3, bosluk + (btnW + bosluk) * 2, btnY, btnW, btnH);
            if (btnKaydet != null) Yerlestir(btnKaydet, (w - 250) / 2, btnY, 250, btnH);

            int panelY = 80;
            int panelH = btnY - panelY - 30;
            pnlAnaKutu.Location = new Point(bosluk, panelY);
            pnlAnaKutu.Size = new Size(w - bosluk * 2, panelH);
            pnlAnaKutu.SendToBack();

            int icW = pnlAnaKutu.Width; int icH = pnlAnaKutu.Height;
            int resimH = (int)(icH * 0.60);

            pbGorsel.Location = new Point(20, 20);
            pbGorsel.Size = new Size(icW - 40, resimH);

            int yaziY = pbGorsel.Bottom + 15;
            int yaziH = icH - yaziY - 60;

            memoHikaye.Location = new Point(20, yaziY);
            memoHikaye.Size = new Size(icW - 40, yaziH);
        }

        private void PnlCamKutu_Paint(object sender, PaintEventArgs e)
        {
            Panel p = sender as Panel; Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle r = p.ClientRectangle; r.Width--; r.Height--;
            GraphicsPath path = YuvarlakKose(r, 30);
            using (SolidBrush b = new SolidBrush(_anaRenk)) { g.FillPath(b, path); }
            using (Pen pen = new Pen(Color.FromArgb(200, 0, 255, 255), 2)) { g.DrawPath(pen, path); }
        }

        // --- BUTON TIKLAMA OLAYLARI ---
        private async void btnSecenek1_Click(object sender, EventArgs e) { if (_butonlarAktifMi) await OyunHamlesiYap(btnSecenek1.Text); }
        private async void btnSecenek2_Click(object sender, EventArgs e) { if (_butonlarAktifMi) await OyunHamlesiYap(btnSecenek2.Text); }
        private async void btnSecenek3_Click(object sender, EventArgs e) { if (_butonlarAktifMi) await OyunHamlesiYap(btnSecenek3.Text); }

        private async void FrmHikaye_Load(object sender, EventArgs e)
        {
            _toplamHikayeMetni = $"Karakter: {GelenKarakterAd} | Dönem: {GelenDonem}\r\n\r\n";
            EkranDuzeniniAyarla();
            _sabitKarakterTarifi = $"{GelenKarakterAd}, a character from {GelenDonem} era, {GelenOzellikler}, wearing period accurate clothes, detailed face";
            memoHikaye.Text = "📜 TimeSpace bağlantısı kuruluyor...\r\nMaceranız yükleniyor.";
            Application.DoEvents();
            await ResimGetir("standing confidently, looking at horizon", true);
            await OyunHamlesiYap("Oyunu Başlat.");
        }

        public async Task HikayeyiSinematikOynat(string metin)
        {
            string[] cumleler = metin.Split(new[] { '.', '!', '?', ':' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> sahneler = new List<string>();
            string tampon = "";
            foreach (string s in cumleler) { tampon += s.Trim() + ". "; if (tampon.Length > 60 || s == cumleler.Last()) { sahneler.Add(tampon); tampon = ""; } }
            foreach (string sahne in sahneler) { if (string.IsNullOrWhiteSpace(sahne)) continue; Image img = await ResimGetir(sahne); if (img != null) pbGorsel.Image = img; memoHikaye.Text = sahne; memoHikaye.SelectionStart = memoHikaye.Text.Length; memoHikaye.ScrollToCaret(); await Task.Delay(2000); }
            if (_turSayisi >= OYUN_LIMITI) { btnKaydet.Visible = true; btnKaydet.BringToFront(); btnSecenek1.Visible = false; btnSecenek2.Visible = false; btnSecenek3.Visible = false; }
            else { btnSecenek1.Text = btnSecenek1.Tag.ToString(); btnSecenek2.Text = btnSecenek2.Tag.ToString(); btnSecenek3.Text = btnSecenek3.Tag.ToString(); ButonlariYonet(true); }
        }

        private async Task<Image> ResimGetir(string metin, bool warmUp = false)
        {
            try
            {
                string style = "minimalist line art drawing, delicate ink sketch, black and white, clean thin lines, detailed, masterpiece, on white paper";
                string prompt = $"{style}, a detailed scene showing (({metin})), with environmental elements. featuring character: {_sabitKarakterTarifi}. {GelenDonem} era atmosphere, detailed background. best quality, sharp focus";
                var payload = new
                {
                    prompt = prompt,
                    negative_prompt = "heavy shadows, solid black areas, thick lines, overly dark, cross-hatching, color, colored, painted, bad anatomy, messy lines, blurry, ugly, text, watermark, 3d, photorealistic, realistic, photography, morphing face, changing clothes, empty background, simple background",
                    steps = 22,
                    cfg_scale = 7.5,
                    sampler_name = "Euler a",
                    width = 512,
                    height = 512,
                    batch_size = 1
                };
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMinutes(3);
                    var json = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("http://127.0.0.1:7860/sdapi/v1/txt2img", json);

                    if (response.IsSuccessStatusCode)
                    {
                        if (warmUp) return null;
                        var respStr = await response.Content.ReadAsStringAsync();
                        dynamic jsonResp = JsonConvert.DeserializeObject(respStr);
                        if (jsonResp.images == null) return null;
                        byte[] bytes = Convert.FromBase64String((string)jsonResp.images[0]);
                        using (var ms = new MemoryStream(bytes)) return Image.FromStream(ms);
                    }
                }
            }
            catch { }
            return null;
        }

        private async Task OyunHamlesiYap(string secim)
        {
            try { ButonlariYonet(false); _turSayisi++; Application.DoEvents(); YapayZekaServisi zeka = new YapayZekaServisi(); string prompt = $"Dönem: {GelenDonem}, Karakter: {GelenKarakterAd}, Özellikler: {GelenOzellikler}.\nTUR: {_turSayisi}/{OYUN_LIMITI}.\nGEÇMİŞ: {_hikayeGecmisi}\nHAMLE: {secim}\nGÖREV: {(_turSayisi == 1 ? "Tanıt" : "Devam Et")}"; OyunSahnesi sahne = await zeka.HikayeGetir(prompt, GelenDonem); _hikayeGecmisi += $" {secim}->{sahne.Hikaye} "; _toplamHikayeMetni += $"SİZ: {secim}\nOYUN: {sahne.Hikaye}\n\n"; if (_turSayisi < OYUN_LIMITI) { btnSecenek1.Tag = sahne.Secenek1; btnSecenek2.Tag = sahne.Secenek2; btnSecenek3.Tag = sahne.Secenek3; } await HikayeyiSinematikOynat(sahne.Hikaye); } catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); ButonlariYonet(true); }
        }

        private void bgAyarlar() { try { string bg = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "giris_bg.jpg"); if (File.Exists(bg)) this.BackgroundImage = Image.FromFile(bg); else this.BackColor = Color.FromArgb(10, 10, 30); } catch { this.BackColor = Color.FromArgb(10, 10, 30); } this.BackgroundImageLayout = ImageLayout.Stretch; }

        private void ButonGorselAyarla(SimpleButton btn, Font font)
        {
            btn.Appearance.Font = font; btn.Cursor = Cursors.Hand; btn.Paint -= BtnNeon_Paint; btn.Paint += BtnNeon_Paint; btn.Parent = this; btn.BringToFront();
            btn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            btn.Appearance.BackColor = Color.Transparent;
            btn.Appearance.Options.UseBackColor = true;
        }
        private void Yerlestir(Control c, int x, int y, int w, int h) { c.Location = new Point(x, y); c.Size = new Size(w, h); c.BringToFront(); }
        private void ButonlariYonet(bool aktif) { _butonlarAktifMi = aktif; if (!aktif) { btnSecenek1.Text = "⏳ Bekleniyor..."; btnSecenek2.Text = "⏳ Bekleniyor..."; btnSecenek3.Text = "⏳ Bekleniyor..."; } btnSecenek1.Invalidate(); btnSecenek2.Invalidate(); btnSecenek3.Invalidate(); }

        private void BtnNeon_Paint(object sender, PaintEventArgs e)
        {
            SimpleButton btn = sender as SimpleButton; Graphics g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias; Rectangle r = btn.ClientRectangle; r.Width--; r.Height--; GraphicsPath path = YuvarlakKose(r, 20);
            if (btn.Region != null) btn.Region.Dispose(); btn.Region = new Region(path);
            Color c1, c2, border; if (_butonlarAktifMi || btn == btnKaydet || btn == btnGeri) { c1 = Color.FromArgb(0, 150, 220); c2 = Color.FromArgb(0, 50, 150); border = Color.Cyan; } else { c1 = Color.FromArgb(40, 40, 50); c2 = Color.FromArgb(20, 20, 30); border = Color.Gray; }
            using (LinearGradientBrush br = new LinearGradientBrush(r, c1, c2, 90F)) { g.FillPath(br, path); }
            using (Pen p = new Pen(border, 2)) { g.DrawPath(p, path); }
            StringFormat sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }; Brush textBrush = (_butonlarAktifMi || btn == btnKaydet || btn == btnGeri) ? Brushes.White : Brushes.Gray; g.DrawString(btn.Text, btn.Appearance.Font, textBrush, r, sf);
        }
        private GraphicsPath YuvarlakKose(Rectangle r, int radius) { GraphicsPath p = new GraphicsPath(); int d = radius * 2; p.AddArc(r.X, r.Y, d, d, 180, 90); p.AddArc(r.Right - d, r.Y, d, d, 270, 90); p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90); p.AddArc(r.X, r.Bottom - d, d, d, 90, 90); p.CloseFigure(); return p; }

        
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                using (ChronoTaleDBEntities db = new ChronoTaleDBEntities())
                {
                    Tbl_Hikayeler yeniHikaye = new Tbl_Hikayeler();
                    yeniHikaye.Baslik = $"{GelenKarakterAd} ile {GelenDonem} Macerası";
                    yeniHikaye.HikayeMetni = _toplamHikayeMetni;
                    yeniHikaye.OlusturmaTarihi = DateTime.Now;
                    yeniHikaye.KullaniciID = Program.KullaniciID;
                    DMLManager.Ekle(yeniHikaye);
                }

                // Mesaj ve Yönlendirme
                FrmMesaj mesaj = new FrmMesaj("Hikayeniz başarıyla kaydedildi! Dönem seçimine yönlendiriliyorsunuz.", "Başarılı");
                mesaj.ShowDialog();
                FrmDonemSecimi frm = new FrmDonemSecimi();
                frm.Show();
                this.Close(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            FrmMesaj mesaj = new FrmMesaj("Macerayı sonlandırıp karakter seçimine dönmek istiyor musunuz?", "Çıkış Onayı");
            DialogResult sonuc = mesaj.ShowDialog();

            if (sonuc == DialogResult.Yes || sonuc == DialogResult.OK)
            {
                FrmKarakterSecimi frm = new FrmKarakterSecimi();
                frm.Show();
                this.Close();
            }
        }
    }
}