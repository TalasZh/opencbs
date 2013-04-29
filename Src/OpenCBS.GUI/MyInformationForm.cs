using System;
using System.IO;
using System.Net;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Octopus.CoreDomain;
using Octopus.MultiLanguageRessources;
using Octopus.Services;
using Octopus.Shared.Settings;
using Octopus.GUI.UserControl;

namespace Octopus.GUI
{
    public partial class MyInformationForm : SweetBaseForm
    {
        private bool _isMandotoryFieldEmpty;
        public MyInformationForm()
        {
            InitializeComponent();
        }

        public bool IsValidEmail(string email)
        {
            //regular expression pattern for valid email
            //addresses, allows for the following domains:
            //com,edu,info,gov,int,mil,net,org,biz,name,museum,coop,aero,pro,tv
            const string pattern = 
                @"^[-a-zA-Z0-9][-.a-zA-Z0-9]*@[-.a-zA-Z0-9]+(\.[-.a-zA-Z0-9]+)*\.(com|edu|info|gov|int|mil|net|org|biz|name|museum|coop|aero|pro|tv|ru|kg|kz|tj|[a-zA-Z]{2})$";
            //Regular expression object
            Regex check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            //boolean variable to return to calling method
            //make sure an email address was provided
             bool valid = !string.IsNullOrEmpty(email) && check.IsMatch(email);
            //return the value to the calling method
            return valid;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            UserSettings.AutoUpdate = chkCheckForUpdates.Checked;
            _isMandotoryFieldEmpty = false;
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                textBoxName.BackColor = Color.Red;
                _isMandotoryFieldEmpty = true;
            }
            else if (string.IsNullOrEmpty(imageComboBoxCountry.Text))
            {
                imageComboBoxCountry.BackColor = Color.Red;
                _isMandotoryFieldEmpty = true;
            }
            else if (string.IsNullOrEmpty(textBoxEmail.Text))
            {
                textBoxEmail.BackColor = Color.Red;
                _isMandotoryFieldEmpty = true;
            }
            

            if (_isMandotoryFieldEmpty)
            {
                string message = MultiLanguageStrings.GetString(Ressource.MyInforamtionForm,
                                                                           "fieldIsRequired");
                MessageBox.Show(message, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            
            if (textBoxNumberOfClients.Text == string.Empty) textBoxNumberOfClients.Text = "0";
            if (textBoxGross.Text == string.Empty) textBoxGross.Text = "0";
            
            //Store data into database
            ServicesProvider.GetInstance().GetQuestionnaireServices().SaveQuestionnaire(
                                                                                textBoxName.Text,
                                                                                imageComboBoxCountry.Text,
                                                                                textBoxEmail.Text,
                                                                                textBoxNumberOfClients.Text,
                                                                                textBoxGross.Text,
                                                                                textBoxPosition.Text,
                                                                                textBoxPersonName.Text,
                                                                                textBoxPhone.Text,
                                                                                textBoxSkype.Text,
                                                                                comboBoxPurpose.Text,
                                                                                textBoxMainMessage.Text,
                                                                                false);
            HttpWebRequest request = null;
            WebResponse response = null;
            Guid? guid = InitializeGuid();
            try
            {
                request = GetRequestUrl(guid);
                response = request.GetResponse();
            }
            catch {}
            
            if (request!=null)
            {
                //Update data
                ServicesProvider.GetInstance().GetQuestionnaireServices().SaveQuestionnaire(
                                                                                textBoxName.Text,
                                                                                imageComboBoxCountry.Text,
                                                                                textBoxEmail.Text,
                                                                                textBoxNumberOfClients.Text,
                                                                                textBoxGross.Text,
                                                                                textBoxPosition.Text,
                                                                                textBoxPersonName.Text,
                                                                                textBoxPhone.Text,
                                                                                textBoxSkype.Text,
                                                                                comboBoxPurpose.Text,
                                                                                textBoxMainMessage.Text,
                                                                                request.HaveResponse);
            }
            if (response!=null)
                response.Close();
            TechnicalSettings.SentQuestionnaire = true;
            DialogResult = DialogResult.None;
            Close();
        }

        private static Guid? InitializeGuid()
        {
            Guid? guid = ServicesProvider.GetInstance().GetApplicationSettingsServices().GetGuid();
            if (!guid.HasValue)
            {
                Guid temp = Guid.NewGuid();
                ServicesProvider.GetInstance().GetApplicationSettingsServices().SetGuid(temp);
                guid = temp;
            }
            return guid;
        }

        private HttpWebRequest GetRequestUrl(Guid? guid)
        {
            string url = "http://www.octopusnetwork.org/info/questionnaire.php?guid=";
            url += guid + "&version=" + TechnicalSettings.SoftwareVersion;
            string buildNumber;
            try
            {
                StreamReader bn = new StreamReader(Path.Combine(Application.StartupPath, "BuildLabel.txt"));
                buildNumber = bn.ReadLine();
                if (string.IsNullOrEmpty(buildNumber)) buildNumber = "NA";
            }
            catch
            {
                buildNumber = "debug";
            }
            url += "." + buildNumber;
            url += "&Name=" + textBoxName.Text;
            url += "&Country=" + imageComboBoxCountry.Text;
            url += "&Email=" + textBoxEmail.Text;
            url += "&PositionInCompony=" + textBoxPosition.Text;
            url += "&OtherMessages=" + textBoxMainMessage.Text;
            url += "&GrossPortfolio=" + textBoxGross.Text;
            url += "&NumberOfClients=" + textBoxNumberOfClients.Text;
            url += "&PersonName=" + textBoxPersonName.Text;
            url += "&Phone=" + textBoxPhone.Text;
            url += "&Skype=" + textBoxSkype.Text;
            url += "&PurposeOfUsage=" + comboBoxPurpose.Text;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.UserAgent = "octopus";
            request.Timeout = 20000;
            return request;
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            textBoxName.BackColor = SystemColors.Window;
        }

        private void imageComboBoxCountry_TextChanged(object sender, EventArgs e)
        {
            imageComboBoxCountry.BackColor = SystemColors.Window;
        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {
            textBoxEmail.BackColor = SystemColors.Window;
        }

        private void textBoxNumberOfClients_KeyPress(object sender, KeyPressEventArgs e)
        {
            int x = e.KeyChar;
            if ((x >= 48 && x <= 57) || (x == 8))
                e.Handled = false; 
            else
                e.Handled = true;
        }

        private void QuestionnaireOctopus_Load(object sender, EventArgs e)
        {
            chkCheckForUpdates.Checked = UserSettings.AutoUpdate;

            MyInformation myInformation = ServicesProvider.GetInstance().GetQuestionnaireServices().GetQuestionnaire();
            if (myInformation==null)return;
            textBoxName.Text = myInformation.MfiName;
            textBoxEmail.Text = myInformation.Email;
            imageComboBoxCountry.Text = myInformation.Country;
            textBoxPosition.Text = myInformation.PositionInCompany;
            textBoxGross.Text = myInformation.GrossPortfolio;
            textBoxNumberOfClients.Text = myInformation.NumberOfClients;
            textBoxMainMessage.Text = myInformation.Comment;
            textBoxPersonName.Text = myInformation.PersonName;
            textBoxPhone.Text = myInformation.Phone;
            textBoxSkype.Text = myInformation.Skype;
            comboBoxPurpose.Text = myInformation.PurposeOfUsage;
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.mixmarket.org/service-providers/octopus-microfinance");
        }

        private void linkLearnMore_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://wiki.octopusnetwork.org/display/Release/about+questionnaire");
        }

        private void MyInformationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ServicesProvider.GetInstance().GetQuestionnaireServices().DataExist()) return;
            string message = MultiLanguageStrings.GetString(Ressource.MyInforamtionForm,
                                                                                    "ExitMessage");
            DialogResult dialogResult =MessageBox.Show(message, "", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Information
                );
            if (dialogResult == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
            this.DialogResult = dialogResult;
        }
   }
}
