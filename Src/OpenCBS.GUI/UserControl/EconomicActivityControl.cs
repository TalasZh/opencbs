using System;
using System.Drawing;
using OpenCBS.CoreDomain.EconomicActivities;

namespace OpenCBS.GUI.UserControl
{
    public partial class EconomicActivityControl : System.Windows.Forms.UserControl
    {
        private EconomicActivity _economicActivity; 
        public event EventHandler<EconomicActivtyEventArgs> EconomicActivityChange;

        public EconomicActivity Activity
        {
            get { return _economicActivity; }
            set
            {
                _economicActivity = value;
                txbActivity.Text = _economicActivity == null ? string.Empty : _economicActivity.Name;
                OnActivityChange();
            }
        }

        public EconomicActivityControl()
        {            
            InitializeComponent();
        }

        private void BtnSelectClick(object sender, EventArgs e)
        {
            frmActivity frmActivity = new frmActivity();
            frmActivity.ShowDialog();

            txbActivity.Clear();
            Activity = frmActivity.EconomicActivity;
            OnActivityChange();
        }

        protected void OnActivityChange()
        {
            var localHandler = EconomicActivityChange;
            if (localHandler != null) localHandler(this, new EconomicActivtyEventArgs(Activity));
        }

        public void Reset() { Activity = null; }

        private void EconomicActivityControlLoad(object sender, EventArgs e)
        {
            //layout controls to avoid overlap
            btnSelect.Width = 25;
            btnSelect.Height = txbActivity.Height;
            txbActivity.Width = Width - btnSelect.Width - 1;
            btnSelect.Location = new Point(txbActivity.Width, 0); 
        }
    }

    public class EconomicActivtyEventArgs : EventArgs
    {
        private readonly EconomicActivity _economicActivity;

        public EconomicActivtyEventArgs(EconomicActivity economicActivity) {
            _economicActivity = economicActivity;
        }

        public EconomicActivity EconomicActivity { get { return _economicActivity; } }
    } 
}
