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
