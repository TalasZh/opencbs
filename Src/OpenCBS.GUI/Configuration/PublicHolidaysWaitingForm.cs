// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.Services;

namespace OpenCBS.GUI.Configuration
{
    /// <summary>
    /// Summary description for PublicHolidaysWaitingForm.
    /// </summary>
    public partial class PublicHolidaysWaitingForm : Form
    {
        public PublicHolidaysWaitingForm()
        {
            InitializeComponent();
        }

        public void UpdateInstallmentsDate()
        {
            Cursor = Cursors.WaitCursor;
            progressBar.Minimum = 1;
            progressBar.Value = 1;
            
 
            List<KeyValuePair<int,Installment>> list = ServicesProvider.GetInstance().GetContractServices().FindAllInstalments();
            progressBar.Maximum = list.Count;

            ServicesProvider.GetInstance().GetContractServices().UpdateAllInstallmentsDate(list);

            Cursor = Cursors.Default;
            Close();
        }

        public void UpdateInstallmentsDate(DateTime date, Dictionary<int, int> list)
        {
            Cursor = Cursors.WaitCursor;
            progressBar.Value = 1;
            progressBar.Minimum = 1;

            progressBar.Maximum = list.Count;

            ServicesProvider.GetInstance().GetContractServices().UpdateAllInstallmentsDate(date, list);

            Cursor = Cursors.Default;
            Close();
        }

        private void buttonUpdate_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            UpdateInstallmentsDate();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
