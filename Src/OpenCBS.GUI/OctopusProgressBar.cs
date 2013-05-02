

using System.Drawing;
using System.Windows.Forms;

namespace OpenCBS.GUI
{
    public partial class OctopusProgressBar : Label
    {
        private int _value,_step, _maximum = 100, _minimum = 0;
        
        public OctopusProgressBar()
        {
            InitializeComponent();
        }

        public void PerformStep()
        {
            int tempValue = _value + _step;

            if (tempValue > _maximum)
                tempValue = _maximum;
            else if (tempValue < _minimum)
                tempValue = _minimum;
            
            _value = tempValue;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawPrgressBarBorder(e.Graphics);
            DrawProgressBar(e.Graphics);
            base.OnPaint(e);
        }

        private void DrawProgressBar(Graphics g)
        {
            SolidBrush brush = new SolidBrush(Color.FromArgb(0,88,56));
            float percent = (float)(_value - _minimum) / (_maximum - _minimum);
            g.FillRectangle(brush, ClientRectangle.Left + 2, ClientRectangle.Top + 2, (int)((ClientRectangle.Width - 4) * percent), ClientRectangle.Height - 4);
        }

        private void DrawPrgressBarBorder(Graphics g)
        {
            ControlPaint.DrawBorder(g, ClientRectangle, Color.FromArgb(0, 88, 56), ButtonBorderStyle.Solid);
        }

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public int Step
        {
            get { return _step; }
            set { _step = value; }
        }
    }
}
