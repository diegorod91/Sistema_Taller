#region Using Statements

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;

#endregion

namespace Utilities.UI
{
    public class CaptchaImage
    {
        private Color _backColor = Color.White;
        private BackgroundNoiseLevel _backgroundNoise = BackgroundNoiseLevel.Low;
        private Color _fontColor = Color.Black;
        private string _fontFamilyName = "";
        private FontWarpFactor _fontWarp = FontWarpFactor.Low;
        private string[] _fontWhitelist;
        private DateTime _generatedAt;
        private string _guid;
        private int _height = 50;
        private Color _lineColor = Color.Black;
        private LineNoiseLevel _lineNoise = LineNoiseLevel.None;
        private Color _noiseColor = Color.Black;
        private Random _rand = new Random();
        private string _randomText;
        private string _randomTextChars = "ACDEFGHJKLNPQRTUVXYZ2346789";
        private int _randomTextLength = 5;
        private int _width = 180;

        public CaptchaImage()
        {
            this._randomText = this.GenerateRandomText();
            this._generatedAt = DateTime.Now;
            this._guid = Guid.NewGuid().ToString();
        }

        private void AddLine(Graphics graphics1, Rectangle rect)
        {
            int num = 0;
            float width = 1f;
            int num3 = 0;
            switch (this._lineNoise)
            {
                case LineNoiseLevel.None:
                    return;

                case LineNoiseLevel.Low:
                    num = 4;
                    width = Convert.ToSingle((double)(((double)this._height) / 31.25));
                    num3 = 1;
                    break;

                case LineNoiseLevel.Medium:
                    num = 5;
                    width = Convert.ToSingle((double)(((double)this._height) / 27.7777));
                    num3 = 1;
                    break;

                case LineNoiseLevel.High:
                    num = 3;
                    width = Convert.ToSingle((int)(this._height / 0x19));
                    num3 = 2;
                    break;

                case LineNoiseLevel.Extreme:
                    num = 3;
                    width = Convert.ToSingle((double)(((double)this._height) / 22.7272));
                    num3 = 3;
                    break;
            }
            PointF[] points = new PointF[num + 1];
            using (Pen pen = new Pen(this._lineColor, width))
            {
                for (int i = 1; i <= num3; i++)
                {
                    for (int j = 0; j <= num; j++)
                    {
                        points[j] = this.RandomPoint(rect);
                    }
                    graphics1.DrawCurve(pen, points, 1.75f);
                }
            }
        }

        private void AddNoise(Graphics graphics1, Rectangle rect)
        {
            int num = 0;
            int num2 = 0;
            switch (this._backgroundNoise)
            {
                case BackgroundNoiseLevel.None:
                    return;

                case BackgroundNoiseLevel.Low:
                    num = 30;
                    num2 = 40;
                    break;

                case BackgroundNoiseLevel.Medium:
                    num = 0x12;
                    num2 = 40;
                    break;

                case BackgroundNoiseLevel.High:
                    num = 0x10;
                    num2 = 0x27;
                    break;

                case BackgroundNoiseLevel.Extreme:
                    num = 12;
                    num2 = 0x26;
                    break;
            }
            using (SolidBrush brush = new SolidBrush(this._noiseColor))
            {
                int maxValue = Convert.ToInt32((int)(Math.Max(rect.Width, rect.Height) / num2));
                for (int i = 0; i <= Convert.ToInt32((int)((rect.Width * rect.Height) / num)); i++)
                {
                    graphics1.FillEllipse(brush, this._rand.Next(rect.Width), this._rand.Next(rect.Height), this._rand.Next(maxValue), this._rand.Next(maxValue));
                }
            }
        }

        private Bitmap GenerateImagePrivate()
        {
            System.Drawing.Font f = null;
            Bitmap image = new Bitmap(this._width, this._height, PixelFormat.Format32bppArgb);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                Brush brush;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(0, 0, this._width, this._height);

                using (brush = new SolidBrush(this._backColor))
                {
                    graphics.FillRectangle(brush, rect);
                }
                this.AddNoise(graphics, rect);
                int num = 0;
                double num2 = this._width / this._randomTextLength;
                using (brush = new SolidBrush(this._fontColor))
                {
                    foreach (char ch in this._randomText)
                    {
                        using (f = this.GetFont())
                        {
                            Rectangle r = new Rectangle(Convert.ToInt32((double)(num * num2)), 0, Convert.ToInt32(num2), this._height);
                            using (GraphicsPath path = this.TextPath(ch.ToString(), f, r))
                            {
                                this.WarpText(path, r);
                                graphics.FillPath(brush, path);
                            }
                        }
                        num++;
                    }
                }
                // this.AddNoise(graphics, rect);
                this.AddLine(graphics, rect);
            }
            return image;
        }

        private string GenerateRandomText()
        {
            StringBuilder builder = new StringBuilder(this._randomTextLength);
            int length = this._randomTextChars.Length;
            for (int i = 0; i <= (this._randomTextLength - 1); i++)
            {
                builder.Append(this._randomTextChars.Substring(this._rand.Next(length), 1));
            }
            return builder.ToString();
        }

        private System.Drawing.Font GetFont()
        {
            float emSize = 0f;
            string familyName = this._fontFamilyName;
            if (familyName == "")
            {
                familyName = this.RandomFontFamily();
            }
            switch (this.FontWarp)
            {
                case FontWarpFactor.None:
                    emSize = Convert.ToInt32((double)(this._height * 0.7));
                    break;

                case FontWarpFactor.Low:
                    emSize = Convert.ToInt32((double)(this._height * 0.8));
                    break;

                case FontWarpFactor.Medium:
                    emSize = Convert.ToInt32((double)(this._height * 0.85));
                    break;

                case FontWarpFactor.High:
                    emSize = Convert.ToInt32((double)(this._height * 0.9));
                    break;

                case FontWarpFactor.Extreme:
                    emSize = Convert.ToInt32((double)(this._height * 0.95));
                    break;
            }
            return new System.Drawing.Font(familyName, emSize, FontStyle.Bold);
        }

        private string RandomFontFamily()
        {
            return this.FontWhitelist[this._rand.Next(0, this.FontWhitelist.Length)];
        }

        private PointF RandomPoint(Rectangle rect)
        {
            return this.RandomPoint(rect.Left, rect.Width, rect.Top, rect.Bottom);
        }

        private PointF RandomPoint(int xmin, int xmax, int ymin, int ymax)
        {
            return new PointF((float)this._rand.Next(xmin, xmax), (float)this._rand.Next(ymin, ymax));
        }

        public Bitmap RenderImage()
        {
            return this.GenerateImagePrivate();
        }

        private GraphicsPath TextPath(string s, System.Drawing.Font f, Rectangle r)
        {
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Near;
            GraphicsPath path = new GraphicsPath();
            path.AddString(s, f.FontFamily, (int)f.Style, f.Size, r, format);
            return path;
        }

        private void WarpText(GraphicsPath textPath, Rectangle rect)
        {
            float num = 1f;
            float num2 = 1f;
            switch (this._fontWarp)
            {
                case FontWarpFactor.None:
                    return;

                case FontWarpFactor.Low:
                    num = 6f;
                    num2 = 1f;
                    break;

                case FontWarpFactor.Medium:
                    num = 5f;
                    num2 = 1.3f;
                    break;

                case FontWarpFactor.High:
                    num = 4.5f;
                    num2 = 1.4f;
                    break;

                case FontWarpFactor.Extreme:
                    num = 4f;
                    num2 = 1.5f;
                    break;
            }
            RectangleF srcRect = new RectangleF(Convert.ToSingle(rect.Left), 0f, Convert.ToSingle(rect.Width), (float)rect.Height);
            int num3 = Convert.ToInt32((float)(((float)rect.Height) / num));
            int num4 = Convert.ToInt32((float)(((float)rect.Width) / num));
            int xmin = rect.Left - Convert.ToInt32((float)(num4 * num2));
            int ymin = rect.Top - Convert.ToInt32((float)(num3 * num2));
            int xmax = (rect.Left + rect.Width) + Convert.ToInt32((float)(num4 * num2));
            int ymax = (rect.Top + rect.Height) + Convert.ToInt32((float)(num3 * num2));
            if (xmin < 0)
            {
                xmin = 0;
            }
            if (ymin < 0)
            {
                ymin = 0;
            }
            if (xmax > this.Width)
            {
                xmax = this.Width;
            }
            if (ymax > this.Height)
            {
                ymax = this.Height;
            }
            PointF tf = this.RandomPoint(xmin, xmin + num4, ymin, ymin + num3);
            PointF tf2 = this.RandomPoint(xmax - num4, xmax, ymin, ymin + num3);
            PointF tf3 = this.RandomPoint(xmin, xmin + num4, ymax - num3, ymax);
            PointF tf4 = this.RandomPoint(xmax - num4, xmax, ymax - num3, ymax);
            PointF[] destPoints = new PointF[] { tf, tf2, tf3, tf4 };
            Matrix matrix = new Matrix();
            matrix.Translate(0f, 0f);
            textPath.Warp(destPoints, srcRect, matrix, WarpMode.Perspective, 0f);
        }

        public Color BackColor
        {
            get
            {
                return this._backColor;
            }
            set
            {
                this._backColor = value;
            }
        }

        public BackgroundNoiseLevel BackgroundNoise
        {
            get
            {
                return this._backgroundNoise;
            }
            set
            {
                this._backgroundNoise = value;
            }
        }

        public string Font
        {
            get
            {
                return this._fontFamilyName;
            }
            set
            {
                System.Drawing.Font font = null;
                try
                {
                    font = new System.Drawing.Font(value, 12f);
                    this._fontFamilyName = value;
                }
                catch (Exception)
                {
                    this._fontFamilyName = FontFamily.GenericSerif.Name;
                }
                finally
                {
                    font.Dispose();
                }
            }
        }

        public Color FontColor
        {
            get
            {
                return this._fontColor;
            }
            set
            {
                this._fontColor = value;
            }
        }

        public FontWarpFactor FontWarp
        {
            get
            {
                return this._fontWarp;
            }
            set
            {
                this._fontWarp = value;
            }
        }

        public string[] FontWhitelist
        {
            get
            {
                return this._fontWhitelist;
            }
            set
            {
                this._fontWhitelist = value;
            }
        }

        public int Height
        {
            get
            {
                return this._height;
            }
            set
            {
                if (value <= 30)
                {
                    throw new ArgumentOutOfRangeException("height", value, "height must be greater than 30.");
                }
                this._height = value;
            }
        }

        public Color LineColor
        {
            get
            {
                return this._lineColor;
            }
            set
            {
                this._lineColor = value;
            }
        }

        public LineNoiseLevel LineNoise
        {
            get
            {
                return this._lineNoise;
            }
            set
            {
                this._lineNoise = value;
            }
        }

        public Color NoiseColor
        {
            get
            {
                return this._noiseColor;
            }
            set
            {
                this._noiseColor = value;
            }
        }

        public DateTime RenderedAt
        {
            get
            {
                return this._generatedAt;
            }
        }

        public string Text
        {
            get
            {
                return this._randomText;
            }
        }

        public string TextChars
        {
            get
            {
                return this._randomTextChars;
            }
            set
            {
                this._randomTextChars = value;
                this._randomText = this.GenerateRandomText();
            }
        }

        public int TextLength
        {
            get
            {
                return this._randomTextLength;
            }
            set
            {
                this._randomTextLength = value;
                this._randomText = this.GenerateRandomText();
            }
        }

        public string UniqueId
        {
            get
            {
                return this._guid;
            }
        }

        public int Width
        {
            get
            {
                return this._width;
            }
            set
            {
                if (value <= 60)
                {
                    throw new ArgumentOutOfRangeException("width", value, "width must be greater than 60.");
                }
                this._width = value;
            }
        }

        public enum BackgroundNoiseLevel
        {
            None,
            Low,
            Medium,
            High,
            Extreme
        }

        public enum FontWarpFactor
        {
            None,
            Low,
            Medium,
            High,
            Extreme
        }

        public enum LineNoiseLevel
        {
            None,
            Low,
            Medium,
            High,
            Extreme
        }
    }
}
