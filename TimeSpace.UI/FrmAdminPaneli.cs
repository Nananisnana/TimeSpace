using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using TimeSpace.Business;   
using TimeSpace.DataAccess;

namespace TimeSpace.UI
{
    public partial class FrmAdminPaneli : Form
    {
        
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ChronoTaleDB;Integrated Security=True";

        // Kontroller
        Panel pnlSolMenu;
        Panel pnlIcerik;
        DataGridView dgvListe;

        // FORM ELEMANLARI 
        TextBox txtAd;          
        TextBox txtBaslangic;   
        TextBox txtBitis;       
        ComboBox cmbDonem;      
        TextBox txtAciklama;    

        string aktifMod = "Donemler";

        public FrmAdminPaneli()
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Maximized;
            
            this.BackColor = Color.FromArgb(18, 18, 25);

            ArayuzuOlustur();
            ArayuzGuncelle(); 
        }

        private void ArayuzuOlustur()
        {
            //  1. İÇERİK PANELİ 
            pnlIcerik = new Panel();
            pnlIcerik.Dock = DockStyle.Fill;
            pnlIcerik.Padding = new Padding(30);
            pnlIcerik.BackColor = Color.FromArgb(18, 18, 25);
            this.Controls.Add(pnlIcerik);

            // 2. SOL MENÜ 
            pnlSolMenu = new Panel();
            pnlSolMenu.Width = 250;
            pnlSolMenu.Dock = DockStyle.Left;
            pnlSolMenu.BackColor = Color.FromArgb(30, 30, 40);
            this.Controls.Add(pnlSolMenu);

            // MENÜ ELEMANLARI (TERSTEN EKLEME - Z-Order Mantığı)
            Button btnCikis = MenuButonuOlustur("Çıkış Yap");
            btnCikis.ForeColor = Color.IndianRed;
            btnCikis.Dock = DockStyle.Bottom;
            btnCikis.Click += (s, e) => { FrmGiris giris = new FrmGiris(); giris.Show(); this.Hide(); };
            pnlSolMenu.Controls.Add(btnCikis);

            Button btnKarakterler = MenuButonuOlustur("Karakter Yönetimi");
            btnKarakterler.Dock = DockStyle.Top;
            btnKarakterler.Click += (s, e) => { aktifMod = "Karakterler"; ArayuzGuncelle(); VerileriListele(); };
            pnlSolMenu.Controls.Add(btnKarakterler);

            Button btnDonemler = MenuButonuOlustur("Dönem Yönetimi");
            btnDonemler.Dock = DockStyle.Top;
            btnDonemler.Click += (s, e) => { aktifMod = "Donemler"; ArayuzGuncelle(); VerileriListele(); };
            pnlSolMenu.Controls.Add(btnDonemler);

            Label lblLogo = new Label();
            lblLogo.Text = "YÖNETİCİ\nPANELİ";
            lblLogo.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblLogo.ForeColor = Color.MediumPurple;
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            lblLogo.Dock = DockStyle.Top;
            lblLogo.Height = 150;
            pnlSolMenu.Controls.Add(lblLogo);

            // SAĞ İÇERİK ELEMANLARI 
            FormElemanlariniEkle();
        }

        private void FormElemanlariniEkle()
        {
            Label lblBaslik = new Label();
            lblBaslik.Text = "Veri Yönetim Merkezi";
            lblBaslik.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblBaslik.ForeColor = Color.White;
            lblBaslik.Dock = DockStyle.Top;
            lblBaslik.Height = 50;
            pnlIcerik.Controls.Add(lblBaslik);

            Panel pnlGirisler = new Panel();
            pnlGirisler.Dock = DockStyle.Top;
            pnlGirisler.Height = 160; 
            pnlIcerik.Controls.Add(pnlGirisler);

            // 1. Ortak Ad Kutusu
            txtAd = TextBoxOlustur(pnlGirisler, "Ad / Başlık", 0, 20, 200);

            // 2. Dönem İçin Tarih Kutuları
            txtBaslangic = TextBoxOlustur(pnlGirisler, "Başlangıç Yılı", 220, 20, 150);
            txtBitis = TextBoxOlustur(pnlGirisler, "Bitiş Yılı", 390, 20, 150);

            // 3. Karakter İçin Yeni Kutular
            // A) Dönem Seçimi (ComboBox)
            cmbDonem = new ComboBox();
            cmbDonem.Text = "Dönem Seçiniz...";
            cmbDonem.Location = new Point(220, 20); 
            cmbDonem.Width = 200;
            cmbDonem.Height = 30;
            cmbDonem.Font = new Font("Segoe UI", 11);
            cmbDonem.BackColor = Color.FromArgb(40, 40, 50);
            cmbDonem.ForeColor = Color.White;
            cmbDonem.FlatStyle = FlatStyle.Flat;
            pnlGirisler.Controls.Add(cmbDonem);

            // B) Açıklama Kutusu (MultiLine)
            txtAciklama = TextBoxOlustur(pnlGirisler, "Karakter Hikayesi / Açıklama", 0, 60, 540);
            txtAciklama.Multiline = true;
            txtAciklama.Height = 80;


            // BUTONLAR
            Button btnEkle = IslemButonuOlustur("EKLE", Color.SeaGreen, 580, 20);
            btnEkle.Click += BtnEkle_Click;
            pnlGirisler.Controls.Add(btnEkle);

            Button btnSil = IslemButonuOlustur("SİL", Color.Crimson, 690, 20);
            btnSil.Click += BtnSil_Click;
            pnlGirisler.Controls.Add(btnSil);

            // TABLO
            dgvListe = new DataGridView();
            dgvListe.Dock = DockStyle.Fill;
            dgvListe.BackgroundColor = Color.FromArgb(25, 25, 35);
            dgvListe.BorderStyle = BorderStyle.None;
            dgvListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvListe.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvListe.AllowUserToAddRows = false;
            dgvListe.EnableHeadersVisualStyles = false;
            dgvListe.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 40, 50);
            dgvListe.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvListe.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvListe.ColumnHeadersHeight = 40;
            dgvListe.DefaultCellStyle.BackColor = Color.FromArgb(30, 30, 40);
            dgvListe.DefaultCellStyle.ForeColor = Color.LightGray;
            dgvListe.DefaultCellStyle.SelectionBackColor = Color.MediumPurple;
            dgvListe.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvListe.RowTemplate.Height = 35;

            pnlIcerik.Controls.Add(dgvListe);
            pnlIcerik.Controls.Add(pnlGirisler);
            pnlGirisler.SendToBack();
            lblBaslik.SendToBack();
        }

        
        private void ArayuzGuncelle()
        {
            if (aktifMod == "Donemler")
            {
                
                txtAd.Text = "Dönem Adı"; txtAd.ForeColor = Color.Gray;
                txtBaslangic.Visible = true;
                txtBitis.Visible = true;
                cmbDonem.Visible = false;
                txtAciklama.Visible = false;
            }
            else 
            {
                txtAd.Text = "Karakter Adı"; txtAd.ForeColor = Color.Gray;
                txtBaslangic.Visible = false;
                txtBitis.Visible = false;

                cmbDonem.Visible = true;
                txtAciklama.Visible = true;

                DonemleriComboyaDoldur(); 
            }
        }

        

        private void DonemleriComboyaDoldur()
        {
            try
            {
                cmbDonem.Items.Clear();
                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("SELECT DonemAdi FROM Tbl_Donemler", baglanti);
                    SqlDataReader dr = komut.ExecuteReader();
                    while (dr.Read())
                    {
                        cmbDonem.Items.Add(dr[0].ToString());
                    }
                }
            }
            catch { }
        }

        private void VerileriListele()
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    baglanti.Open();
                    string sql = "";

                    if (aktifMod == "Donemler")
                        sql = "SELECT DonemID, DonemAdi, BaslangicYili, BitisYili FROM Tbl_Donemler";
                    else
                        
                        sql = "SELECT * FROM Tbl_Karakterler";

                    SqlDataAdapter da = new SqlDataAdapter(sql, baglanti);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvListe.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (aktifMod == "Donemler")
                {

                    Tbl_Donemler yeniDonem = new Tbl_Donemler();
                    yeniDonem.DonemAdi = txtAd.Text;
                    yeniDonem.BaslangicYili = Convert.ToInt32(txtBaslangic.Text);
                    yeniDonem.BitisYili = Convert.ToInt32(txtBitis.Text);

                    DMLManager.Ekle(yeniDonem);

                    FrmMesaj basarili = new FrmMesaj("Yeni dönem başarıyla eklendi.", "İşlem Başarılı");
                    basarili.ShowDialog();
                }
                else
                {

                    Tbl_Karakterler yeniKarakter = new Tbl_Karakterler();
                    yeniKarakter.AdSoyad = txtAd.Text;

                    DMLManager.Ekle(yeniKarakter);

                    FrmMesaj basarili = new FrmMesaj("Yeni karakter başarıyla eklendi.", "İşlem Başarılı");
                    basarili.ShowDialog();
                }
                VerileriListele();
            }
            catch (Exception ex)
            {
                FrmMesaj hata = new FrmMesaj("Ekleme Hatası:\n" + ex.Message, "HATA");
                hata.ShowDialog();
            }

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (dgvListe.SelectedRows.Count == 0) return;

            
            if (MessageBox.Show("Bu kaydı kalıcı olarak silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
               
                int id = Convert.ToInt32(dgvListe.SelectedRows[0].Cells[0].Value);

                if (aktifMod == "Donemler")
                {
                    
                    Tbl_Donemler silinecekDonem = new Tbl_Donemler();
                    silinecekDonem.DonemID = id; 

                    DMLManager.Sil(silinecekDonem);
                }
                else
                {
                    Tbl_Karakterler silinecekKarakter = new Tbl_Karakterler();
                    silinecekKarakter.KarakterID = id; 

                    DMLManager.Sil(silinecekKarakter);
                }

               
                FrmMesaj basarili = new FrmMesaj("Kayıt başarıyla silindi.", "İşlem Tamam");
                basarili.ShowDialog();

                VerileriListele();
            }
            catch (Exception ex)
            {
                
                FrmMesaj hata = new FrmMesaj("Silme işlemi sırasında hata oluştu:\n" + ex.Message, "HATA");
                hata.ShowDialog();
            }
            
        }


        private Button MenuButonuOlustur(string text)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Height = 50;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.ForeColor = Color.LightGray;
            btn.Font = new Font("Segoe UI", 12);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(20, 0, 0, 0);
            btn.Cursor = Cursors.Hand;
            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(50, 50, 60);
            btn.MouseLeave += (s, e) => btn.BackColor = Color.Transparent;
            return btn;
        }

        private TextBox TextBoxOlustur(Panel pnl, string placeholder, int x, int y, int width)
        {
            TextBox txt = new TextBox();
            txt.Text = placeholder;
            txt.Location = new Point(x, y);
            txt.Width = width;
            txt.Font = new Font("Segoe UI", 12);
            txt.BackColor = Color.FromArgb(40, 40, 50);
            txt.ForeColor = Color.Gray;
            txt.BorderStyle = BorderStyle.FixedSingle;

            txt.Enter += (s, e) => {
                if (txt.Text == placeholder) { txt.Text = ""; txt.ForeColor = Color.White; }
            };
            txt.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(txt.Text)) { txt.Text = placeholder; txt.ForeColor = Color.Gray; }
            };

            pnl.Controls.Add(txt);
            return txt;
        }

        private Button IslemButonuOlustur(string text, Color renk, int x, int y)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Location = new Point(x, y);
            btn.BackColor = renk;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Size = new Size(100, 30);
            btn.Cursor = Cursors.Hand;
            return btn;
        }
    }
}