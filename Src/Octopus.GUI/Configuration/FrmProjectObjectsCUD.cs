using System;
using System.Windows.Forms;
using Octopus.CoreDomain;

namespace Octopus.GUI
{
    public partial class FrmProjectObjectsCUD : Form
    {
        private string _name;

        public FrmProjectObjectsCUD()
        {
            InitializeComponent();
            //_InitializeSecurity();
        }

        /*private void _InitializeSecurity()
        {
            if (User.CurrentUser.isVisitor || User.CurrentUser.isCashier)
            {
                buttonAdd.Visible = false;
            }
        }*/

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmProvinces_Load(object sender, EventArgs e)
        {
            _LoadInstallmentTypes();
        }

        private void _LoadInstallmentTypes()
        {
            //listViewProjectObjects.Items.Clear();
            //List<PostingFrequencyType> list = _packageServices.FindAllInstallmentTypes();
            //foreach (PostingFrequencyType installmentType in list)
            //{
            //    ListViewItem listView = new ListViewItem(installmentType.Name);
            //    listView.Tag = installmentType;
            //    listViewProjectObjects.Items.Add(listView);
            //}
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //try
            //{
            //_packageServices.AddInstallmentType(new PostingFrequencyType(_name,_nbOfDays,_nbOfMonths));
            //_LoadInstallmentTypes();
            //}
            //catch(Exception ex)
            //{
            //    new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            //}
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            //if (listViewInstallmentTypes.SelectedItems.Count != 0)
            //{
            //    FundingLine line = (FundingLine) listViewInstallmentTypes.SelectedItems[0].Tag;
            //    line.Deleted = true;
            //    _contractServices.UpdateFundingLine(line);
            //    _LoadFundingLine();
            //}
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            _name = textBoxName.Text;
        }
    }
}