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

using System.Collections.Generic;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.Enums;
using OpenCBS.Services;

namespace OpenCBS.GUI
{
    public partial class ProjectFollowUp : Form
    {
        private FollowUp _followUp;
        public FollowUp FollowUp
        {
            get { return _followUp; }
            set { _followUp = value; }
        }

        public ProjectFollowUp()
        {
            InitializeComponent();
            _followUp = new FollowUp {Year = 1};
        }
        public ProjectFollowUp(FollowUp pFollowUp)
        {
            InitializeComponent();
            _followUp = pFollowUp;
            _InitializeFollowUp();
        }

        private void _InitializeFollowUp()
        {
            numericUpDownYear.Value = _followUp.Year;
            numericUpDownJobs1.Value = _followUp.Jobs1;
            numericUpDownJobs2.Value = _followUp.Jobs2;
            tBCA.Text = _followUp.CA.GetFormatedValue(true);
            comboBoxPersonalSituation.Text = _followUp.PersonalSituation;
            comboBoxActivity.Text = _followUp.Activity;
            tBComment.Text = _followUp.Comment;
        }

        private void buttonSave_Click(object sender, System.EventArgs e)
        {
            _followUp.Year = (int)numericUpDownYear.Value;
            _followUp.Jobs1 = (int)numericUpDownJobs1.Value;
            _followUp.Jobs2 = (int)numericUpDownJobs2.Value;
            _followUp.CA = ServicesHelper.ConvertStringToNullableDecimal(tBCA.Text);
            _followUp.PersonalSituation = comboBoxPersonalSituation.Text;
            _followUp.Activity = comboBoxActivity.Text;
            _followUp.Comment = tBComment.Text;
            Close();
        }

        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            _followUp = null;
            Close();
        }

        private void comboBoxPersonalSituation_DropDown(object sender, System.EventArgs e)
        {
            comboBoxPersonalSituation.Items.Clear();
            List<string> list = ServicesProvider.GetInstance().GetClientServices().FindAllSetUpFields(OSetUpFieldTypes.PersonalSituation);
            foreach (string s in list)
            {
                comboBoxPersonalSituation.Items.Add(s);
            }
        }

        private void comboBoxActivity_DropDown(object sender, System.EventArgs e)
        {
            comboBoxActivity.Items.Clear();
            List<string> list = ServicesProvider.GetInstance().GetClientServices().FindAllSetUpFields(OSetUpFieldTypes.ActivityState);
            foreach (string s in list)
            {
                comboBoxActivity.Items.Add(s);
            }
        }
    }
}
