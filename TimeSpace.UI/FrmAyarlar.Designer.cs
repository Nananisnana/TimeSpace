namespace TimeSpace.UI
{
    partial class FrmAyarlar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAyarlar));
            this.btnGecmisiTemizle = new DevExpress.XtraEditors.SimpleButton();
            this.btnHesabiSil = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // btnGecmisiTemizle
            // 
            this.btnGecmisiTemizle.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnGecmisiTemizle.Appearance.Options.UseBackColor = true;
            this.btnGecmisiTemizle.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnGecmisiTemizle.ImageOptions.Image")));
            this.btnGecmisiTemizle.Location = new System.Drawing.Point(51, 114);
            this.btnGecmisiTemizle.Name = "btnGecmisiTemizle";
            this.btnGecmisiTemizle.Size = new System.Drawing.Size(185, 66);
            this.btnGecmisiTemizle.TabIndex = 0;
            this.btnGecmisiTemizle.Text = "Tüm Hikaye Geçmişi Temizle";
            this.btnGecmisiTemizle.Click += new System.EventHandler(this.btnGecmisiTemizle_Click);
            // 
            // btnHesabiSil
            // 
            this.btnHesabiSil.Appearance.BackColor = System.Drawing.Color.IndianRed;
            this.btnHesabiSil.Appearance.Options.UseBackColor = true;
            this.btnHesabiSil.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnHesabiSil.ImageOptions.SvgImage")));
            this.btnHesabiSil.Location = new System.Drawing.Point(51, 246);
            this.btnHesabiSil.Name = "btnHesabiSil";
            this.btnHesabiSil.Size = new System.Drawing.Size(185, 62);
            this.btnHesabiSil.TabIndex = 1;
            this.btnHesabiSil.Text = "HESABIMI KALICI OLARAK SİL";
            this.btnHesabiSil.Click += new System.EventHandler(this.btnHesabiSil_Click);
            // 
            // FrmAyarlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 518);
            this.Controls.Add(this.btnHesabiSil);
            this.Controls.Add(this.btnGecmisiTemizle);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("FrmAyarlar.IconOptions.SvgImage")));
            this.Name = "FrmAyarlar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayarlar Menüsü";
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnGecmisiTemizle;
        private DevExpress.XtraEditors.SimpleButton btnHesabiSil;
    }
}