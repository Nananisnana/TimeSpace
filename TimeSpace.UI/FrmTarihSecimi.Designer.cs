namespace TimeSpace.UI
{
    partial class FrmTarihSecimi
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTarihSecimi));
            this.trackBarControl1 = new DevExpress.XtraEditors.TrackBarControl();
            this.lblBaslik = new DevExpress.XtraEditors.LabelControl();
            this.lblMaxTarih = new DevExpress.XtraEditors.LabelControl();
            this.lblMinTarih = new DevExpress.XtraEditors.LabelControl();
            this.lblSecilenTarih = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.pnlOrta = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1.Properties)).BeginInit();
            this.pnlOrta.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBarControl1
            // 
            this.trackBarControl1.EditValue = null;
            this.trackBarControl1.Location = new System.Drawing.Point(145, 182);
            this.trackBarControl1.Name = "trackBarControl1";
            this.trackBarControl1.Properties.LabelAppearance.Options.UseTextOptions = true;
            this.trackBarControl1.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.trackBarControl1.Size = new System.Drawing.Size(513, 45);
            this.trackBarControl1.TabIndex = 0;
            // 
            // lblBaslik
            // 
            this.lblBaslik.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBaslik.Appearance.Font = new System.Drawing.Font("Atkinson Hyperlegible Mono Medi", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBaslik.Appearance.Options.UseFont = true;
            this.lblBaslik.Location = new System.Drawing.Point(118, 41);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Size = new System.Drawing.Size(644, 52);
            this.lblBaslik.TabIndex = 1;
            this.lblBaslik.Text = "HANGİ YILA GİTMEK İSTERSİNİZ";
            // 
            // lblMaxTarih
            // 
            this.lblMaxTarih.Appearance.Font = new System.Drawing.Font("Exo 2", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblMaxTarih.Appearance.Options.UseFont = true;
            this.lblMaxTarih.Location = new System.Drawing.Point(605, 233);
            this.lblMaxTarih.Name = "lblMaxTarih";
            this.lblMaxTarih.Size = new System.Drawing.Size(72, 24);
            this.lblMaxTarih.TabIndex = 2;
            this.lblMaxTarih.Text = "Max Tarih";
            // 
            // lblMinTarih
            // 
            this.lblMinTarih.Appearance.Font = new System.Drawing.Font("Exo 2", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblMinTarih.Appearance.Options.UseFont = true;
            this.lblMinTarih.Location = new System.Drawing.Point(120, 233);
            this.lblMinTarih.Name = "lblMinTarih";
            this.lblMinTarih.Size = new System.Drawing.Size(68, 24);
            this.lblMinTarih.TabIndex = 3;
            this.lblMinTarih.Text = "Min Tarih";
            // 
            // lblSecilenTarih
            // 
            this.lblSecilenTarih.Appearance.Font = new System.Drawing.Font("Exo 2 Medium", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSecilenTarih.Appearance.Options.UseFont = true;
            this.lblSecilenTarih.Location = new System.Drawing.Point(221, 389);
            this.lblSecilenTarih.Name = "lblSecilenTarih";
            this.lblSecilenTarih.Size = new System.Drawing.Size(167, 39);
            this.lblSecilenTarih.TabIndex = 4;
            this.lblSecilenTarih.Text = "Seçilen Tarih:";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Atkinson Hyperlegible Mono Medi", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(220, 605);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(404, 29);
            this.simpleButton1.TabIndex = 5;
            this.simpleButton1.Text = "Tarihi onayla ve karakter seç";
            // 
            // pnlOrta
            // 
            this.pnlOrta.BackColor = System.Drawing.Color.Transparent;
            this.pnlOrta.Controls.Add(this.lblMinTarih);
            this.pnlOrta.Controls.Add(this.trackBarControl1);
            this.pnlOrta.Controls.Add(this.lblSecilenTarih);
            this.pnlOrta.Controls.Add(this.lblMaxTarih);
            this.pnlOrta.Location = new System.Drawing.Point(28, 99);
            this.pnlOrta.Name = "pnlOrta";
            this.pnlOrta.Size = new System.Drawing.Size(800, 500);
            this.pnlOrta.TabIndex = 6;
            // 
            // FrmTarihSecimi
            // 
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Stretch;
            this.BackgroundImageStore = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImageStore")));
            this.ClientSize = new System.Drawing.Size(857, 665);
            this.Controls.Add(this.lblBaslik);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.pnlOrta);
            this.IconOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("FrmTarihSecimi.IconOptions.LargeImage")));
            this.Name = "FrmTarihSecimi";
            this.Text = "Tarih Seçimi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmTarihSecimi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1)).EndInit();
            this.pnlOrta.ResumeLayout(false);
            this.pnlOrta.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private DevExpress.XtraEditors.TrackBarControl trackBarControl1;
        private DevExpress.XtraEditors.LabelControl lblBaslik;
        private DevExpress.XtraEditors.LabelControl lblMaxTarih;
        private DevExpress.XtraEditors.LabelControl lblMinTarih;
        private DevExpress.XtraEditors.LabelControl lblSecilenTarih;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.Panel pnlOrta;
    }
}