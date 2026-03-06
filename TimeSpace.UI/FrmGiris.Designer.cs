namespace TimeSpace.UI
{
    partial class FrmGiris
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGiris));
            this.pnlGirisKutusu = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnKayit = new DevExpress.XtraEditors.SimpleButton();
            this.btnGiris = new DevExpress.XtraEditors.SimpleButton();
            this.txtSifre = new DevExpress.XtraEditors.TextEdit();
            this.txtEposta = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGirisKutusu)).BeginInit();
            this.pnlGirisKutusu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSifre.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEposta.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlGirisKutusu
            // 
            this.pnlGirisKutusu.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlGirisKutusu.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlGirisKutusu.Appearance.Options.UseBackColor = true;
            this.pnlGirisKutusu.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlGirisKutusu.Controls.Add(this.labelControl2);
            this.pnlGirisKutusu.Controls.Add(this.labelControl1);
            this.pnlGirisKutusu.Controls.Add(this.btnKayit);
            this.pnlGirisKutusu.Controls.Add(this.btnGiris);
            this.pnlGirisKutusu.Controls.Add(this.txtSifre);
            this.pnlGirisKutusu.Controls.Add(this.txtEposta);
            this.pnlGirisKutusu.Location = new System.Drawing.Point(289, 160);
            this.pnlGirisKutusu.LookAndFeel.UseDefaultLookAndFeel = false;
            this.pnlGirisKutusu.Name = "pnlGirisKutusu";
            this.pnlGirisKutusu.Size = new System.Drawing.Size(400, 500);
            this.pnlGirisKutusu.TabIndex = 0;
            this.pnlGirisKutusu.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlGirisKutusu_Paint);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Atkinson Hyperlegible Mono", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(51, 169);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(40, 17);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "Şifre";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Atkinson Hyperlegible Mono", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(51, 73);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(208, 17);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Kullanıcı Adı veya E-Posta";
            // 
            // btnKayit
            // 
            this.btnKayit.Location = new System.Drawing.Point(66, 372);
            this.btnKayit.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnKayit.Name = "btnKayit";
            this.btnKayit.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnKayit.Size = new System.Drawing.Size(280, 45);
            this.btnKayit.TabIndex = 3;
            this.btnKayit.Click += new System.EventHandler(this.btnKayit_Click);
            this.btnKayit.Paint += new System.Windows.Forms.PaintEventHandler(this.btnKayit_Paint);
            this.btnKayit.MouseEnter += new System.EventHandler(this.btnKayit_MouseEnter);
            this.btnKayit.MouseLeave += new System.EventHandler(this.btnKayit_MouseLeave);
            // 
            // btnGiris
            // 
            this.btnGiris.Appearance.Font = new System.Drawing.Font("Exo 2", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnGiris.Appearance.Options.UseFont = true;
            this.btnGiris.Location = new System.Drawing.Point(51, 304);
            this.btnGiris.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnGiris.Name = "btnGiris";
            this.btnGiris.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnGiris.Size = new System.Drawing.Size(304, 52);
            this.btnGiris.TabIndex = 2;
            this.btnGiris.Text = "GİRİŞ YAP";
            this.btnGiris.Click += new System.EventHandler(this.btnGiris_Click);
            this.btnGiris.Paint += new System.Windows.Forms.PaintEventHandler(this.btnGiris_Paint);
            this.btnGiris.MouseEnter += new System.EventHandler(this.btnGiris_MouseEnter);
            this.btnGiris.MouseLeave += new System.EventHandler(this.btnGiris_MouseLeave);
            // 
            // txtSifre
            // 
            this.txtSifre.EditValue = "";
            this.txtSifre.Location = new System.Drawing.Point(51, 197);
            this.txtSifre.Name = "txtSifre";
            this.txtSifre.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtSifre.Properties.Appearance.Font = new System.Drawing.Font("Exo 2 Thin", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtSifre.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.txtSifre.Properties.Appearance.Options.UseBackColor = true;
            this.txtSifre.Properties.Appearance.Options.UseFont = true;
            this.txtSifre.Properties.Appearance.Options.UseForeColor = true;
            this.txtSifre.Properties.AutoHeight = false;
            this.txtSifre.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtSifre.Properties.ContextImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("txtSifre.Properties.ContextImageOptions.SvgImage")));
            this.txtSifre.Properties.PasswordChar = '*';
            this.txtSifre.Size = new System.Drawing.Size(304, 48);
            this.txtSifre.TabIndex = 1;
            // 
            // txtEposta
            // 
            this.txtEposta.EditValue = "";
            this.txtEposta.Location = new System.Drawing.Point(51, 102);
            this.txtEposta.Name = "txtEposta";
            this.txtEposta.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtEposta.Properties.Appearance.Font = new System.Drawing.Font("Exo 2 Thin", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtEposta.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.txtEposta.Properties.Appearance.Options.UseBackColor = true;
            this.txtEposta.Properties.Appearance.Options.UseFont = true;
            this.txtEposta.Properties.Appearance.Options.UseForeColor = true;
            this.txtEposta.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.White;
            this.txtEposta.Properties.AppearanceReadOnly.Options.UseForeColor = true;
            this.txtEposta.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtEposta.Properties.ContextImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("txtEposta.Properties.ContextImageOptions.SvgImage")));
            this.txtEposta.Size = new System.Drawing.Size(304, 48);
            this.txtEposta.TabIndex = 0;
            this.txtEposta.EditValueChanged += new System.EventHandler(this.txtEposta_EditValueChanged);
            // 
            // FrmGiris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Stretch;
            this.BackgroundImageStore = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImageStore")));
            this.ClientSize = new System.Drawing.Size(985, 778);
            this.Controls.Add(this.pnlGirisKutusu);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmGiris";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Giriş Ekranı";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmGiris_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlGirisKutusu)).EndInit();
            this.pnlGirisKutusu.ResumeLayout(false);
            this.pnlGirisKutusu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSifre.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEposta.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlGirisKutusu;
        private DevExpress.XtraEditors.TextEdit txtEposta;
        private DevExpress.XtraEditors.TextEdit txtSifre;
        private DevExpress.XtraEditors.SimpleButton btnGiris;
        private DevExpress.XtraEditors.SimpleButton btnKayit;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}