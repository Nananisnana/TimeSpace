using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TimeSpace.UI
{
    public class NeonButon : Button
    {
        // Ayarlar
        private int borderThickness = 3; // Çerçeve kalınlığı
        private int borderRadius = 20;   // Köşe yuvarlaklığı

        // Çerçeve Rengi (İstersen Color.MediumPurple yapabilirsin)
        private Color borderColor = Color.Cyan;

        // Büyüme Ayarı
        private int shrinkAmount = 4; // Normalde 4 piksel içeride, Hover'da 0 (Büyür)

        private bool isHovered = false;

        public NeonButon()
        {
            // 1. TEMEL AYARLAR
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(220, 60);
            this.BackColor = Color.Transparent;
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.Cursor = Cursors.Hand;
            this.DoubleBuffered = true;

            // 2. BEYAZLAŞMAYI ENGELLEYEN KOD (ÇOK ÖNEMLİ)
            // Mouse üzerine gelince veya tıklayınca arka plan şeffaf kalsın
            this.FlatAppearance.MouseOverBackColor = Color.Transparent;
            this.FlatAppearance.MouseDownBackColor = Color.Transparent;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            isHovered = true;
            this.Invalidate(); // Yeniden çiz (Büyüme efekti için)
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isHovered = false;
            this.Invalidate(); // Yeniden çiz (Küçülme efekti için)
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            // base.OnPaint'i çağırmıyoruz ki standart buton çizimleri (gri arka plan) karışmasın.
            // base.OnPaint(pevent); -> BU SATIRI SİLDİK VEYA YORUMA ALDIK

            Graphics graph = pevent.Graphics;
            graph.SmoothingMode = SmoothingMode.AntiAlias;

            // --- BÜYÜME MANTIĞI ---
            // Mouse üzerindeyse boşluk 0 (Büyük), değilse 4 (Küçük)
            int margin = isHovered ? 0 : shrinkAmount;

            // Dikdörtgen alanını hesapla
            RectangleF rect = new RectangleF(
                margin,
                margin,
                this.Width - (margin * 2),
                this.Height - (margin * 2)
            );

            // Çizgiler taşmasın diye milimetrik ayar
            // rect.Inflate(-1, -1); 

            using (GraphicsPath path = GetFigurePath(rect, borderRadius))
            {
                // 1. ZEMİN RENGİ (Hafif Siyahlık)
                // Bu sayede yazı uzay boşluğunda kaybolmaz, okunur.
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
                {
                    graph.FillPath(brush, path);
                }

                // 2. ÇERÇEVE ÇİZİMİ (Bunu senin kodunda eksikti, geri ekledim)
                using (Pen pen = new Pen(borderColor, borderThickness))
                {
                    pen.Alignment = PenAlignment.Inset; // Çizgiyi içeri doğru çiz
                    graph.DrawPath(pen, path);
                }
            }

            // 3. YAZI ÇİZİMİ
            TextRenderer.DrawText(graph, this.Text, this.Font, ClientRectangle, this.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        // Yuvarlak köşe hesaplayan yardımcı fonksiyon
        private GraphicsPath GetFigurePath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float diameter = radius * 2;

            // Hata önleyici kontrol
            if (rect.Width < diameter || rect.Height < diameter) diameter = Math.Min(rect.Width, rect.Height);

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}