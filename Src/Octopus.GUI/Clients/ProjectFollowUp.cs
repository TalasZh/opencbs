
using System.Collections.Generic;
using System.Windows.Forms;
using Octopus.CoreDomain.Clients;
using Octopus.Enums;
using Octopus.Services;

namespace Octopus.GUI
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
