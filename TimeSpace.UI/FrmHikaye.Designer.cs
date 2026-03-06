namespace TimeSpace.UI
{
    partial class FrmHikaye
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
            this.grpKontrol = new DevExpress.XtraEditors.GroupControl();
            this.btnSecenek1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnSecenek3 = new DevExpress.XtraEditors.SimpleButton();
            this.btnSecenek2 = new DevExpress.XtraEditors.SimpleButton();
            this.memoHikaye = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.grpKontrol)).BeginInit();
            this.grpKontrol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoHikaye.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grpKontrol
            // 
            this.grpKontrol.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.grpKontrol.Appearance.Options.UseBackColor = true;
            this.grpKontrol.Controls.Add(this.btnSecenek1);
            this.grpKontrol.Controls.Add(this.btnSecenek3);
            this.grpKontrol.Controls.Add(this.btnSecenek2);
            this.grpKontrol.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpKontrol.Location = new System.Drawing.Point(0, 280);
            this.grpKontrol.Margin = new System.Windows.Forms.Padding(2);
            this.grpKontrol.Name = "grpKontrol";
            this.grpKontrol.ShowCaption = false;
            this.grpKontrol.Size = new System.Drawing.Size(856, 297);
            this.grpKontrol.TabIndex = 1;
            this.grpKontrol.Text = "Seçimini Yap";
            // 
            // btnSecenek1
            // 
            this.btnSecenek1.Location = new System.Drawing.Point(89, 248);
            this.btnSecenek1.Name = "btnSecenek1";
            this.btnSecenek1.Size = new System.Drawing.Size(143, 23);
            this.btnSecenek1.TabIndex = 2;
            this.btnSecenek1.Text = "simpleButton3";
            this.btnSecenek1.Click += new System.EventHandler(this.btnSecenek1_Click);
            // 
            // btnSecenek3
            // 
            this.btnSecenek3.Location = new System.Drawing.Point(603, 248);
            this.btnSecenek3.Name = "btnSecenek3";
            this.btnSecenek3.Size = new System.Drawing.Size(146, 23);
            this.btnSecenek3.TabIndex = 1;
            this.btnSecenek3.Text = "simpleButton2";
            this.btnSecenek3.Click += new System.EventHandler(this.btnSecenek3_Click);
            // 
            // btnSecenek2
            // 
            this.btnSecenek2.Location = new System.Drawing.Point(332, 248);
            this.btnSecenek2.Name = "btnSecenek2";
            this.btnSecenek2.Size = new System.Drawing.Size(151, 23);
            this.btnSecenek2.TabIndex = 0;
            this.btnSecenek2.Text = "simpleButton1";
            this.btnSecenek2.Click += new System.EventHandler(this.btnSecenek2_Click);
            // 
            // memoHikaye
            // 
            this.memoHikaye.Location = new System.Drawing.Point(-21, 279);
            this.memoHikaye.Name = "memoHikaye";
            this.memoHikaye.Size = new System.Drawing.Size(893, 219);
            this.memoHikaye.TabIndex = 4;
            // 
            // FrmHikaye
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 577);
            this.Controls.Add(this.memoHikaye);
            this.Controls.Add(this.grpKontrol);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "FrmHikaye";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TimeSpace/Macera Başlıyor";
            this.Load += new System.EventHandler(this.FrmHikaye_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grpKontrol)).EndInit();
            this.grpKontrol.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoHikaye.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.GroupControl grpKontrol;
        private DevExpress.XtraEditors.SimpleButton btnSecenek1;
        private DevExpress.XtraEditors.SimpleButton btnSecenek3;
        private DevExpress.XtraEditors.SimpleButton btnSecenek2;
        private DevExpress.XtraEditors.MemoEdit memoHikaye;
    }
}