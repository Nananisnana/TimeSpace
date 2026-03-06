using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TimeSpace.DataAccess;
using TimeSpace.Entities;

namespace TimeSpace.UI
{
    public partial class FrmGecmisHikayeler : DevExpress.XtraEditors.XtraForm
    {
        public FrmGecmisHikayeler()
        {
            InitializeComponent();
        }

        // Grid için veri taşıyıcı sınıf
        public class HikayeVerisi
        {
            public int ID { get; set; }
            public DateTime? Tarih { get; set; }
            public string Baslik { get; set; }
        }

        private void FrmGecmisHikayeler_Load(object sender, EventArgs e)
        {
            btnPdfIndir.Click -= btnPdfIndir_Click;
            btnPdfIndir.Click += btnPdfIndir_Click;

            ListeyiDoldur();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            ListeyiDoldur();
        }

        void ListeyiDoldur()
        {
            using (ChronoTaleDBEntities db = new ChronoTaleDBEntities())
            {
                var hikayeler = db.Tbl_Hikayeler
                                  .OrderByDescending(x => x.OlusturmaTarihi)
                                  .Select(x => new HikayeVerisi
                                  {
                                      ID = x.HikayeID,
                                      Tarih = x.OlusturmaTarihi,
                                      Baslik = x.Baslik
                                  }).ToList();

                gridControl1.DataSource = hikayeler;

                GridView view = gridControl1.MainView as GridView;
                if (view != null)
                {
                    view.PopulateColumns();
                    if (view.Columns["ID"] != null) view.Columns["ID"].Visible = false;
                    view.BestFitColumns();
                }
            }
        }

        private void btnPdfIndir_Click(object sender, EventArgs e)
        {
            GridView view = gridControl1.MainView as GridView;

            if (view == null || view.FocusedRowHandle < 0)
            {
                
                FrmMesaj uyari = new FrmMesaj("Lütfen listeden indirmek istediğiniz bir hikayeye tıklayın.", "Seçim Yapılmadı");
                uyari.ShowDialog();
                return;
            }

            try
            {
                object idVal = view.GetRowCellValue(view.FocusedRowHandle, "ID");
                if (idVal == null) return;

                int secilenHikayeID = Convert.ToInt32(idVal);

                using (ChronoTaleDBEntities db = new ChronoTaleDBEntities())
                {
                    var hikaye = db.Tbl_Hikayeler.Find(secilenHikayeID);
                    if (hikaye != null)
                    {
                        PDFOlarakKaydet(hikaye.Baslik, hikaye.HikayeMetni, hikaye.OlusturmaTarihi.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("İşlem sırasında hata oluştu: " + ex.Message, "Hata");
            }
        }

        void PDFOlarakKaydet(string baslik, string icerik, string tarih)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "PDF Dosyası|*.pdf";
            save.FileName = baslik.Replace(" ", "_") + ".pdf";

            if (save.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (RichEditControl pdfMotoru = new RichEditControl())
                    {
                        string htmlIcerik = $@"
                            <html>
                                <body style='font-family:Arial; padding:20px;'>
                                    <h1 style='color:#003366; text-align:center; border-bottom:2px solid #003366; padding-bottom:10px;'>{baslik}</h1>
                                    <p style='text-align:right; color:gray; font-size:10pt;'><i>{tarih}</i></p>
                                    <br/>
                                    <div style='font-size:12pt; line-height:1.5; text-align:justify;'>
                                        {icerik.Replace("\n", "<br/>")}
                                    </div>
                                    <br/><br/>
                                    <hr/>
                                    <center style='color:gray; font-size:9pt;'>TimeSpace - Yapay Zeka Destekli Tarih Simülasyonu</center>
                                </body>
                            </html>";

                        pdfMotoru.HtmlText = htmlIcerik;
                        pdfMotoru.Document.Sections[0].Page.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
                        pdfMotoru.ExportToPdf(save.FileName);
                    }

                    
                    FrmMesaj basarili = new FrmMesaj("Hikayeniz PDF olarak başarıyla kaydedildi!", "İndirme Tamamlandı");
                    basarili.ShowDialog();

                    System.Diagnostics.Process.Start(save.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("PDF oluşturulurken hata: " + ex.Message);
                }
            }
        }
    }
}