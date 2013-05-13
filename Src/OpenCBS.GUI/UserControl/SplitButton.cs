// Octopus MFS is an integrated suite for managing a Micro Finance Institution: 
// clients, contracts, accounting, reporting and risk
// Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Drawing.Drawing2D;

namespace OpenCBS.GUI.UserControl
{
    public partial class SplitButton : Button
    {
        #region Fields

        private bool _DoubleClickedEnabled;

        private bool _AlwaysDropDown;
        private bool _AlwaysHoverChange;

        private bool _CalculateSplitRect = true;
        private bool _FillSplitHeight = true;
        private int _SplitHeight;
        private int _SplitWidth;

        private String _NormalImage;
        private String _HoverImage;
        private String _ClickedImage;
        private String _DisabledImage;
        private String _FocusedImage;

        private ImageList _DefaultSplitImages;


        [Browsable(true)]
        [Category("Action")]
        [Description("Occurs when the button part of the SplitButton is clicked.")]
        public event EventHandler ButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Occurs when the button part of the SplitButton is clicked.")]
        public event EventHandler ButtonDoubleClick;

        #endregion


        #region Properties
        /// <summary>
        /// 
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether the double click event is raised on the SplitButton")]
        [DefaultValue(false)]
        public bool DoubleClickedEnabled
        {
            get
            {
                return _DoubleClickedEnabled;
            }
            set
            {
                _DoubleClickedEnabled = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Split Button")]
        [Description("Indicates whether the SplitButton always shows the drop down menu even if the button part of the SplitButton is clicked.")]
        [DefaultValue(false)]
        public bool AlwaysDropDown
        {
            get
            {
                return _AlwaysDropDown;
            }
            set
            {
                _AlwaysDropDown = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Split Button")]
        [Description("Indicates whether the SplitButton always shows the Hover image status in the split part even if the button part of the SplitButton is hovered.")]
        [DefaultValue(false)]
        public bool AlwaysHoverChange
        {
            get
            {
                return _AlwaysHoverChange;
            }
            set
            {
                _AlwaysHoverChange = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Split Button")]
        [Description("Indicates whether the split rectange must be calculated (basing on Split image size)")]
        [DefaultValue(true)]
        public bool CalculateSplitRect
        {
            get
            {
                return _CalculateSplitRect;
            }
            set
            {
                bool flag1 = _CalculateSplitRect;

                _CalculateSplitRect = value;

                if (flag1 != _CalculateSplitRect)
                {
                    if (_SplitWidth > 0 && _SplitHeight > 0)
                    {
                        InitDefaultSplitImages(true);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [fill split height].
        /// </summary>
        /// <value><c>true</c> if [fill split height]; otherwise, <c>false</c>.</value>
        [Category("Split Button")]
        [Description("Indicates whether the split height must be filled to the button height even if the split image height is lower.")]
        [DefaultValue(true)]
        public bool FillSplitHeight
        {
            get
            {
                return _FillSplitHeight;
            }
            set
            {
                _FillSplitHeight = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Split Button")]
        [Description("The split height (ignored if CalculateSplitRect is setted to true).")]
        [DefaultValue(0)]
        public int SplitHeight
        {
            get
            {
                return _SplitHeight;
            }
            set
            {
                _SplitHeight = value;

                if (!_CalculateSplitRect)
                {
                    if (_SplitWidth > 0 && _SplitHeight > 0)
                    {
                        InitDefaultSplitImages(true);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Split Button")]
        [Description("The split width (ignored if CalculateSplitRect is setted to true).")]
        [DefaultValue(0)]
        public int SplitWidth
        {
            get
            {
                return _SplitWidth;
            }
            set
            {
                _SplitWidth = value;

                if (!_CalculateSplitRect)
                {
                    if (_SplitWidth > 0 && _SplitHeight > 0)
                    {
                        InitDefaultSplitImages(true);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Split Button Images")]
        [Description("The Normal status image name in the ImageList.")]
        [DefaultValue("")]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Localizable(true), RefreshProperties(RefreshProperties.Repaint), TypeConverter(typeof(ImageKeyConverter))]
        public String NormalImage
        {
            get
            {
                return _NormalImage;
            }
            set
            {
                _NormalImage = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Split Button Images")]
        [Description("The Hover status image name in the ImageList.")]
        [DefaultValue("")]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Localizable(true), RefreshProperties(RefreshProperties.Repaint), TypeConverter(typeof(ImageKeyConverter))]
        public String HoverImage
        {
            get
            {
                return _HoverImage;
            }
            set
            {
                _HoverImage = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Split Button Images")]
        [Description("The Clicked status image name in the ImageList.")]
        [DefaultValue("")]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Localizable(true), RefreshProperties(RefreshProperties.Repaint), TypeConverter(typeof(ImageKeyConverter))]
        public String ClickedImage
        {
            get
            {
                return _ClickedImage;
            }
            set
            {
                _ClickedImage = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Split Button Images")]
        [Description("The Disabled status image name in the ImageList.")]
        [DefaultValue("")]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Localizable(true), RefreshProperties(RefreshProperties.Repaint), TypeConverter(typeof(ImageKeyConverter))]
        public String DisabledImage
        {
            get
            {
                return _DisabledImage;
            }
            set
            {
                _DisabledImage = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Split Button Images")]
        [Description("The Focused status image name in the ImageList.")]
        [DefaultValue("")]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Localizable(true), RefreshProperties(RefreshProperties.Repaint), TypeConverter(typeof(ImageKeyConverter))]
        public String FocusedImage
        {
            get
            {
                return _FocusedImage;
            }
            set
            {
                _FocusedImage = value;
            }
        }

        #endregion


        #region Construction

        public SplitButton()
        {
            InitializeComponent();
        }

        #endregion


        #region Methods

        protected override void OnCreateControl()
        {
            InitDefaultSplitImages();

            if (this.ImageList == null)
            {
                this.ImageList = _DefaultSplitImages;
            }

            if (Enabled)
            {
                SetSplit(_NormalImage);
            }
            else
            {
                SetSplit(_DisabledImage);
            }
            
            base.OnCreateControl();
        }

        private void InitDefaultSplitImages()
        {
            InitDefaultSplitImages(false);
        }

        private void InitDefaultSplitImages(bool refresh)
        {
            if (String.IsNullOrEmpty(_NormalImage))
            {
                _NormalImage = "Normal";
            }

            if (String.IsNullOrEmpty(_HoverImage))
            {
                _HoverImage = "Hover";
            }

            if (String.IsNullOrEmpty(_ClickedImage))
            {
                _ClickedImage = "Clicked";
            }

            if (String.IsNullOrEmpty(_DisabledImage))
            {
                _DisabledImage = "Disabled";
            }

            if (String.IsNullOrEmpty(_FocusedImage))
            {
                _FocusedImage = "Focused";
            }

            if (_DefaultSplitImages == null)
            {
                _DefaultSplitImages = new ImageList();
            }

            if (_DefaultSplitImages.Images.Count == 0 || refresh)
            {
                if (_DefaultSplitImages.Images.Count > 0)
                {
                    _DefaultSplitImages.Images.Clear();
                }

                try
                {
                    int w = 0;
                    int h = 0;

                    if (!_CalculateSplitRect && _SplitWidth > 0)
                    {
                        w = _SplitWidth;
                    }
                    else
                    {
                        w = 18;
                    }

                    if (!CalculateSplitRect && SplitHeight > 0)
                    {
                        h = SplitHeight;
                    }
                    else
                    {
                        h = Height;
                    }

                    h -= 8;

                    _DefaultSplitImages.ImageSize = new Size(w, h);

                    int mw = w / 2;
                    mw += (mw % 2);
                    int mh = h / 2;

                    Pen fPen = new Pen(ForeColor, 1);
                    SolidBrush fBrush = new SolidBrush(ForeColor);

                    Bitmap imgN = new Bitmap(w, h);
                    Graphics g = Graphics.FromImage(imgN);

                    g.CompositingQuality = CompositingQuality.HighQuality;

                    //g.DrawLine(SystemPens.ButtonShadow, new Point(1, 1), new Point(1, h - 2));
                    //g.DrawLine(SystemPens.ButtonFace, new Point(2, 1), new Point(2, h));

                    g.FillPolygon(fBrush, new Point[] { new Point(mw - 2, mh - 1), 
                                                         new Point(mw + 3, mh - 1),
                                                         new Point(mw, mh + 2) });

                    g.Dispose();

                    Bitmap imgH = new Bitmap(w, h);
                    g = Graphics.FromImage(imgH);

                    g.CompositingQuality = CompositingQuality.HighQuality;

                    //g.DrawLine(SystemPens.ButtonShadow, new Point(1, 1), new Point(1, h - 2));
                    //g.DrawLine(SystemPens.ButtonFace, new Point(2, 1), new Point(2, h));

                    g.FillPolygon(fBrush, new Point[] { new Point(mw - 3, mh - 2), 
                                                         new Point(mw + 4, mh - 2),
                                                         new Point(mw, mh + 2) });

                    g.Dispose();

                    Bitmap imgC = new Bitmap(w, h);
                    g = Graphics.FromImage(imgC);

                    g.CompositingQuality = CompositingQuality.HighQuality;

                    //g.DrawLine(SystemPens.ButtonShadow, new Point(1, 1), new Point(1, h - 2));
                    //g.DrawLine(SystemPens.ButtonFace, new Point(2, 1), new Point(2, h));

                    g.FillPolygon(fBrush, new Point[] { new Point(mw - 2, mh - 1), 
                                                         new Point(mw + 3, mh - 1),
                                                         new Point(mw, mh + 2) });

                    g.Dispose();

                    Bitmap imgD = new Bitmap(w, h);
                    g = Graphics.FromImage(imgD);

                    g.CompositingQuality = CompositingQuality.HighQuality;

                    //g.DrawLine(SystemPens.GrayText, new Point(1, 1), new Point(1, h - 2));

                    g.FillPolygon(new SolidBrush(SystemColors.GrayText), new Point[] { new Point(mw - 2, mh - 1), 
                                                         new Point(mw + 3, mh - 1),
                                                         new Point(mw, mh + 2) });

                    g.Dispose();

                    Bitmap imgF = new Bitmap(w, h);
                    g = Graphics.FromImage(imgF);

                    g.CompositingQuality = CompositingQuality.HighQuality;

                    //g.DrawLine(SystemPens.ButtonShadow, new Point(1, 1), new Point(1, h - 2));
                    //g.DrawLine(SystemPens.ButtonFace, new Point(2, 1), new Point(2, h));

                    g.FillPolygon(fBrush, new Point[] { new Point(mw - 2, mh - 1), 
                                                         new Point(mw + 3, mh - 1),
                                                         new Point(mw, mh + 2) });

                    g.Dispose();

                    fPen.Dispose();
                    fBrush.Dispose();

                    _DefaultSplitImages.Images.Add(_NormalImage, imgN);
                    _DefaultSplitImages.Images.Add(_HoverImage, imgH);
                    _DefaultSplitImages.Images.Add(_ClickedImage, imgC);
                    _DefaultSplitImages.Images.Add(_DisabledImage, imgD);
                    _DefaultSplitImages.Images.Add(_FocusedImage, imgF);
                }
                catch
                { }
            }
        }

        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            if (_AlwaysDropDown || _AlwaysHoverChange || MouseInSplit())
            {
                if (Enabled)
                {
                    SetSplit(_HoverImage);
                }
            }
            else
            {
                if (Enabled)
                {
                    SetSplit(_NormalImage);
                }
            }

            base.OnMouseMove(mevent);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (Enabled)
            {
                SetSplit(_NormalImage);
            }

            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (_AlwaysDropDown || MouseInSplit())
            {
                if (Enabled)
                {
                    SetSplit(_ClickedImage);

                    if (this.ContextMenuStrip != null && this.ContextMenuStrip.Items.Count > 0)
                    {
                        this.ContextMenuStrip.Show(this, new Point(0, Height));
                    }
                }
            }
            else
            {
                if (Enabled)
                {
                    SetSplit(_NormalImage);
                }
            }

            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (_AlwaysDropDown || _AlwaysHoverChange || MouseInSplit())
            {
                if (Enabled)
                {
                    SetSplit(_HoverImage);
                }
            }
            else
            {
                if (Enabled)
                {
                    SetSplit(_NormalImage);
                }
            }

            base.OnMouseUp(mevent);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            if (!Enabled)
            {
                SetSplit(_DisabledImage);
            }
            else
            {
                if (MouseInSplit())
                {
                    SetSplit(_HoverImage);
                }
                else
                {
                    SetSplit(_NormalImage);
                }
            }

            base.OnEnabledChanged(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            if (Enabled)
            {
                SetSplit(_FocusedImage);
            }

            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            if (Enabled)
            {
                SetSplit(_NormalImage);
            }

            base.OnLostFocus(e);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (!MouseInSplit() && !_AlwaysDropDown)
            {
                if (ButtonClick != null)
                {
                    ButtonClick(this, e);
                }
            }
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            if (_DoubleClickedEnabled)
            {
                base.OnDoubleClick(e);

                if (!MouseInSplit() && !_AlwaysDropDown)
                {
                    if (ButtonClick != null)
                    {
                        ButtonDoubleClick(this, e);
                    }
                }
            }
        }

        private void SetSplit(String imageName)
        {
            if (imageName != null && ImageList != null && ImageList.Images.ContainsKey(imageName))
            {
                this.ImageKey = imageName;
            }
        }

        public bool MouseInSplit()
        {
            return PointInSplit(PointToClient(MousePosition));
        }

        public bool PointInSplit(Point pt)
        {
            Rectangle splitRect = GetImageRect(_NormalImage);

            if (!_CalculateSplitRect)
            {
                splitRect.Width = _SplitWidth;
                splitRect.Height = _SplitHeight;
            }

            return splitRect.Contains(pt);
        }

        public Rectangle GetImageRect(String imageKey)
        {
                Image currImg = GetImage(imageKey);

                if (currImg != null)
                {
                    int x = 0,
                        y = 0,
                        w = currImg.Width+1,
                        h = currImg.Height+1;

                    if (w > this.Width)
                    {
                        w = this.Width;
                    }

                    if (h > this.Width)
                    {
                        h = this.Width;
                    }

                    switch (ImageAlign)
                    {
                        case ContentAlignment.TopLeft:
                            {
                                x = 0;
                                y = 0;

                                break;
                            }
                        case ContentAlignment.TopCenter:
                            {
                                x = (this.Width - w) / 2;
                                y = 0;

                                if ((this.Width - w) % 2 > 0)
                                {
                                    x += 1;
                                }

                                break;
                            }
                        case ContentAlignment.TopRight:
                            {
                                x = this.Width - w;
                                y = 0;

                                break;
                            }
                        case ContentAlignment.MiddleLeft:
                            {
                                x = 0;
                                y = (this.Height - h) / 2;

                                if ((this.Height - h) % 2 > 0)
                                {
                                    y += 1;
                                }

                                break;
                            }
                        case ContentAlignment.MiddleCenter:
                            {
                                x = (this.Width - w) / 2;
                                y = (this.Height - h) / 2;

                                if ((this.Width - w) % 2 > 0)
                                {
                                    x += 1;
                                }
                                if ((this.Height - h) % 2 > 0)
                                {
                                    y += 1;
                                }

                                break;
                            }
                        case ContentAlignment.MiddleRight:
                            {
                                x = this.Width - w;
                                y = (this.Height - h) / 2;

                                if ((this.Height - h) % 2 > 0)
                                {
                                    y += 1;
                                }

                                break;
                            }
                        case ContentAlignment.BottomLeft:
                            {
                                x = 0;
                                y = this.Height - h;

                                if ((this.Height - h) % 2 > 0)
                                {
                                    y += 1;
                                }

                                break;
                            }
                        case ContentAlignment.BottomCenter:
                            {
                                x = (this.Width - w) / 2;
                                y = this.Height - h;

                                if ((this.Width - w) % 2 > 0)
                                {
                                    x += 1;
                                }

                                break;
                            }
                        case ContentAlignment.BottomRight:
                            {
                                x = this.Width - w;
                                y = this.Height - h;

                                break;
                            }
                    }

                    if (_FillSplitHeight && h < this.Height)
                    {
                        h = this.Height;
                    }

                    if (x > 0)
                    {
                        x -= 1;
                    }
                    if (y > 0)
                    {
                        y -= 1;
                    }

                    return new Rectangle(x, y, w, h);
                }

            return Rectangle.Empty;
        }

        private Image GetImage(String imageName)
        {
            if (this.ImageList != null && this.ImageList.Images.ContainsKey(imageName))
            {
                return this.ImageList.Images[imageName];
            }

            return null;
        }

        #endregion
    }
}
