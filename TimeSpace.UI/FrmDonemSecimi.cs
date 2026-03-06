using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeSpace.UI
{
    public partial class FrmDonemSecimi : DevExpress.XtraEditors.XtraForm
    {
        LabelControl lblBaslik;
        public FrmDonemSecimi()
        {
            InitializeComponent();
        }
        private void tileControl1_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            // 1. Güvenlik: Boş yere tıklanırsa hata vermesin
            if (e.Item.Tag == null) return;

            int secilenID = Convert.ToInt32(e.Item.Tag);

            // 2. Tarih formunu oluştur
            FrmTarihSecimi tarihFormu = new FrmTarihSecimi();

            // 3: ID'yi diğer formdaki çantaya koy
            tarihFormu.GelenDonemID = secilenID;

            // 4. Formu aç
            tarihFormu.Show();
            this.Hide();
        }

        private void FrmDonemSecimi_Load(object sender, EventArgs e)
        {
            
            this.WindowState = FormWindowState.Maximized;
            RenkleriAyarla();




        }

        private void accordionControl1_ElementClick(object sender, DevExpress.XtraBars.Navigation.ElementClickEventArgs e)
        {
            string tiklanan = e.Element.Text;

            // 1. PROFİL (Gerçek Form)
            if (tiklanan == "Profil" || tiklanan == "Profilim")
            {
                FrmProfil profilFormu = new FrmProfil();
                profilFormu.ShowDialog();
            }
            if (tiklanan == "Hikayeler")
            {
                FrmGecmisHikayeler hikayeFormu = new FrmGecmisHikayeler();
                hikayeFormu.ShowDialog();
            }

        }
        private void RenkleriAyarla()
        {
            // TileControl içindeki tüm grupları ve öğeleri gez
            foreach (TileGroup grup in tileControl1.Groups)
            {
                foreach (TileItem kutu in grup.Items)
                {
                    // 1. Yazı Fontu ve Rengi
                    kutu.AppearanceItem.Normal.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                    kutu.AppearanceItem.Normal.ForeColor = Color.White;

                    // 2. Arka Plan Rengi (Gradient)
                    kutu.AppearanceItem.Normal.BackColor = Color.DimGray; // Başlangıç (Gri)
                    kutu.AppearanceItem.Normal.BackColor2 = Color.Black;  // Bitiş (Siyah)
                    kutu.AppearanceItem.Normal.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;

                    // 3. Kenarlık Rengi (İsteğe bağlı, kutu belli olsun diye)
                    kutu.AppearanceItem.Normal.BorderColor = Color.WhiteSmoke;
                }
            }
        }

        private void accordionControlElement1_Click(object sender, EventArgs e)
        {

        }

        private void accordionControlElement3_Click(object sender, EventArgs e)
        {
            FrmAyarlar ayarlar = new FrmAyarlar();
            ayarlar.ShowDialog();
        }
    }
}