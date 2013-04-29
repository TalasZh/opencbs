using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace Octopus.GUI.UserControl
{
    using Res = Properties.Resources;

    public class SweetButton : Button
    {
        public enum ButtonIcon
        {
            None
            , Close
            , Database
            , Delete
            , Edit
            , Export
            , Import
            , Next
            , New
            , Previous
            , Print
            , Refresh
            , Save
            , Search
            , View
            , Repayment
            , Reschedule
            , Validity
            , Up
            , Down
        };

        private readonly Color _startColor = Color.White;
        private readonly Color _endColor = Color.FromArgb(176, 217, 200);
        private readonly Color _darkBorderColor = Color.FromArgb(124, 153, 141);
        private readonly Color _lightBorderColor = Color.FromArgb(165, 204, 188);
        private readonly Brush _textBrush = new SolidBrush(Color.FromArgb(0, 88, 56));
        private readonly Brush _disabledTextBrush = new SolidBrush(Color.FromArgb(119, 119, 119));
        private bool _down;
        private ButtonIcon _icon;
        private ContextMenuStrip _menu;
        private bool _inProgress;
        private int _progressIndex;
        private readonly Timer _timer = new Timer();

        public SweetButton()
        {
            InitializeComponent();
            _timer.Elapsed += OnTimer;
            _timer.Interval = 100;
            _timer.Enabled = false;
        }

        public void StartProgress()
        {
            _inProgress = true;
            _progressIndex = 0;
            _timer.Enabled = true;
            Invalidate();
        }

        public void StopProgress()
        {
            _inProgress = false;
            _timer.Enabled = false;
            Invalidate();
        }

        public void OnTimer(object sender, ElapsedEventArgs e)
        {
            _progressIndex++;
            _progressIndex = _progressIndex%6;
            Invalidate();
        }

        public virtual ButtonIcon Icon
        {
            get
            {
                return _icon;
            }

            set
            {
                _icon = value;
                Invalidate();
            }
        }

        public virtual ContextMenuStrip Menu
        {
            get
            {
                return _menu;
            }

            set
            {
                _menu = value;
                Invalidate();
            }
        }
        
        private void InitializeComponent()
        {
            MouseDown += OnMouseDown;
            MouseUp += OnMouseUp;
            EnabledChanged += OnEnabledChanged;
            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            _down = true;
            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            _down = false;
            Invalidate();
            if (_inProgress) return;

            base.OnClick(e);
            if (HasMenu)
            {
                _menu.Show(this, 0, Height);
            }
        }
        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            _down = false;
            Invalidate();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space) return;

            _down = true;
            Invalidate();
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space) return;

            _down = false;
            Invalidate();
        }

        private void OnEnabledChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private bool HasIcon
        {
            get { return _icon != ButtonIcon.None; }
        }

        private bool HasMenu
        {
            get { return _menu != null; }
        }

        private Bitmap GetIcon()
        {
            switch (Icon)
            {
                case ButtonIcon.Close:
                    return new Bitmap(Res.theme1_1_bouton_close);

                case ButtonIcon.Database:
                    return new Bitmap(Res.thame1_1_database);

                case ButtonIcon.Delete:
                    return new Bitmap(Res.theme1_1_bouton_delete);

                case ButtonIcon.Edit:
                    return new Bitmap(Res.theme1_1_doc);

                case ButtonIcon.Export:
                    return new Bitmap(Res.theme1_1_export);

                case ButtonIcon.Import:
                    return new Bitmap(Res.theme1_1_import);

                case ButtonIcon.New:
                    return new Bitmap(Res.theme1_1_bouton_new);
                
                case ButtonIcon.Print:
                    return new Bitmap(Res.theme1_1_bouton_print);

                case ButtonIcon.Refresh:
                    return new Bitmap(Res.refresh);

                case ButtonIcon.Save:
                    return new Bitmap(Res.theme1_1_bouton_save);

                case ButtonIcon.Search:
                    return new Bitmap(Res.theme1_1_search);

                case ButtonIcon.View:
                    return new Bitmap(Res.theme1_1_view);

                case ButtonIcon.Repayment:
                    return new Bitmap(Res.repayments_16x16);

                case ButtonIcon.Reschedule:
                    return new Bitmap(Res.reschedule_16x16);

                case ButtonIcon.Validity:
                    return new Bitmap(Res.theme1_1_bouton_validity);

                case ButtonIcon.Next:
                    return new Bitmap(Res.theme1_1_bouton_next);

                case ButtonIcon.Previous:
                    return new Bitmap(Res.theme1_1_bouton_previous);
                
                case ButtonIcon.Up:
                    return new Bitmap(Res.theme1_1_bouton_up);
                
                case ButtonIcon.Down:
                    return new Bitmap(Res.theme1_1_bouton_down);

                default:
                    return null;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            Color from = (_down && Enabled) ? _endColor : _startColor;
            Color to = (_down && Enabled) ? _startColor : _endColor;
            Brush brush = new LinearGradientBrush(ClientRectangle, from, to, LinearGradientMode.Vertical);
            g.FillRectangle(brush, ClientRectangle);

            DrawBorder(g);
            DrawText(g);
            if (HasMenu) DrawArrow(g);
            if (HasIcon && !_inProgress) DrawIcon(g);
            if (_inProgress) DrawProgressIcon(g);
            if (Focused && Enabled) DrawFocus(g);
        }

        private void DrawIcon(Graphics g)
        {
            Debug.Assert(g != null, "Graphic device is null");
            Bitmap bmp = GetIcon();
            Debug.Assert(bmp != null, "Icon is null");
            if (!Enabled) bmp = ToGrascale(bmp);
            float x = ButtonIcon.Next == Icon ? Width - bmp.Width - 5 : 5;
            float y = (float)(Height - bmp.Height)/2;
            x += (_down && Enabled) ? 1 : 0;
            y += (_down && Enabled) ? 1 : 0;
            g.DrawImage(bmp, x, y);
        }

        private void DrawProgressIcon(Graphics g)
        {
            Debug.Assert(g != null, "Graphic device is null");
            Bitmap bmp = GetProgressIcon();
            const float x = 5;
            float y = (float)(Height - bmp.Height) / 2;
            g.DrawImage(bmp, x, y);
        }

        private Bitmap GetProgressIcon()
        {
            Bitmap bmp = new Bitmap(16, 16);
            Graphics g = Graphics.FromImage(bmp);

            Bitmap src = Res.progress;
            Rectangle rect = new Rectangle(16*_progressIndex, 0, 16, 16);
            g.DrawImage(src, 0, 0, rect, GraphicsUnit.Pixel);
            g.Dispose();
            return bmp;
        }

        private void DrawText(Graphics g)
        {
            Debug.Assert(g != null, "Graphic device is null");
            SizeF size = g.MeasureString(Text, Font);
            float x;
            float y = (Height - size.Height) / 2;
            switch (TextAlign)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                    x = HasIcon ? 25 : 10;
                    break;

                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    x = Width - size.Width - 10;
                    x -= HasMenu ? 8 : 0;
                    break;

                default:
                    x = (Width - size.Width) / 2;
                    x -= HasMenu ? 4 : 0;
                    break;
            }
            x += (_down && Enabled) ? 1 : 0;
            y += (_down && Enabled) ? 1 : 0;
            Brush brush = Enabled ? _textBrush : _disabledTextBrush;
            g.DrawString(Text, Font, brush, x, y);
        }

        private void DrawArrow(Graphics g)
        {
            Debug.Assert(g != null, "Graphic device is null");
            SizeF size = g.MeasureString(Text, Font);
            float x;
            Bitmap arrow = new Bitmap(Res.arrow_down_small);
            if (!Enabled) arrow = ToGrascale(arrow);
            float y = (float)(Height - arrow.Height) / 2;

            switch (TextAlign)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                    x = size.Width;
                    x += HasIcon ? 25 : 10;
                    break;

                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    x = Width - arrow.Width - 10;
                    break;

                default:
                    x = (Width + size.Width)/2 - 4;
                    break;
            }
            x += (_down && Enabled) ? 1 : 0;
            y += (_down && Enabled) ? 1 : 0;
            g.DrawImage(arrow, x, y);
        }

        private void DrawBorder(Graphics g)
        {
            Debug.Assert(g != null, "Graphic device is null");
            Pen darkPen = new Pen(_darkBorderColor, 1);
            Pen lightPen = new Pen(_lightBorderColor, 1);

            Point[] leftTopPoints = new[]
            {
                new Point(0, Height - 1)
                , new Point(0, 0)
                , new Point(Width - 1, 0)
            };

            Point[] rightBottomPoints = new[]
            {
                new Point(Width - 1, 0)
                , new Point(Width - 1, Height - 1)
                , new Point(0, Height - 1)
            };

            Pen leftTopPen = _down ? darkPen : lightPen;
            Pen rightBottomPen = _down ? lightPen : darkPen;
            g.DrawLines(leftTopPen, leftTopPoints);
            g.DrawLines(rightBottomPen, rightBottomPoints);

            Pen whitePen = new Pen(Color.White);
            g.DrawLine(whitePen, 1, 1, 1, Height - 2);
            g.DrawLine(whitePen, Width - 2, 1, Width - 2, Height - 2);
        }

        private void DrawFocus(Graphics g)
        {
            Pen pen = new Pen(_textBrush);
            pen.DashStyle = DashStyle.Dot;
            Point[] points = new[]
            {
                new Point(3, 3)
                , new Point(Width - 4, 3)
                , new Point(Width - 4, Height - 4)
                , new Point(3, Height - 4)
                , new Point(3, 3)
            };

            if (_down)
            {
                for (int i = 0; i < points.Length; i++)
                {
                    points[i].Offset(1, 1);
                }
            }

            g.DrawLines(pen, points);
        }

        private static Bitmap ToGrascale(Image original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(new[]
            {
                new[] {.3f, .3f, .3f, 0, 0}, // red
                new[] {.59f, .59f, .59f, 0, 0}, // green
                new[] {.11f, .11f, .11f, 0, 0}, // blue
                new[] {0f, 0f, 0f, 1f, 0f}, // alpha
                new[] {0.3f, 0.3f, 0.3f, 0, 1} // brightness
            });

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                        0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }
    }
}