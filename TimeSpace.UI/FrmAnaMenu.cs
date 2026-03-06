using System;
using System.Drawing;
using System.Windows.Forms;

namespace TimeSpace.UI
{
    public partial class FrmAnaMenu : DevExpress.XtraEditors.XtraForm
    {
        public FrmAnaMenu()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void FrmAnaMenu_Load(object sender, EventArgs e)
        {

        }

       
       
        private void neonButon2_Click(object sender, EventArgs e)
        {
            FrmGiris donem = new FrmGiris();
            donem.Show();
            this.Hide();
        }

        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tablePanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        
        private void KartBoyama(object sender, PaintEventArgs e, Color renk1, Color renk2)
        {
            DevExpress.XtraEditors.PanelControl panel = sender as DevExpress.XtraEditors.PanelControl;
            if (panel == null) return;

           
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

           
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int kavis = 30; // Yuvarlaklık derecesi
            Rectangle rect = panel.ClientRectangle;
            rect.Width--; rect.Height--; // Kenar taşmasını önle

           
            path.AddArc(rect.X, rect.Y, kavis, kavis, 180, 90);
            path.AddArc(rect.Right - kavis, rect.Y, kavis, kavis, 270, 90);
            path.AddArc(rect.Right - kavis, rect.Bottom - kavis, kavis, kavis, 0, 90);
            path.AddArc(rect.X, rect.Bottom - kavis, kavis, kavis, 90, 90);
            path.CloseFigure();

            
            panel.Region = new Region(path);

          
            using (System.Drawing.Drawing2D.LinearGradientBrush firca =
                   new System.Drawing.Drawing2D.LinearGradientBrush(panel.ClientRectangle,
                   renk1, renk2, 45F)) 
            {
                e.Graphics.FillPath(firca, path);
            }
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {
            KartBoyama(sender, e, Color.FromArgb(45, 40, 100), Color.FromArgb(20, 60, 200));
        }

        private void panelControl6_Paint(object sender, PaintEventArgs e)
        {
            
            KartBoyama(sender, e, Color.FromArgb(80, 10, 30), Color.FromArgb(160, 20, 60));
        }

        private void panelControl3_Paint(object sender, PaintEventArgs e)
        {
            
            KartBoyama(sender, e, Color.FromArgb(120, 20, 80), Color.FromArgb(60, 20, 100));
        }

        private void panelControl4_Paint(object sender, PaintEventArgs e)
        {
            
            KartBoyama(sender, e, Color.FromArgb(20, 60, 40), Color.FromArgb(40, 120, 80));
        }

        private void panelControl5_Paint(object sender, PaintEventArgs e)
        {
            
            KartBoyama(sender, e, Color.FromArgb(100, 50, 20), Color.FromArgb(180, 100, 40));
        }

        private void panelControl7_Paint(object sender, PaintEventArgs e)
        {
           
            KartBoyama(sender, e, Color.FromArgb(10, 30, 80), Color.FromArgb(20, 100, 180));
        }

        private void labelControl8_Click(object sender, EventArgs e)
        {

        }

        private void svgImageBox8_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.instagram.com/nananisnana/");
            
        }
        
       
        

    }
}