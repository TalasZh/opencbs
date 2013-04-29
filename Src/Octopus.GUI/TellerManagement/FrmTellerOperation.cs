using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Octopus.CoreDomain;
using Octopus.CoreDomain.Events.Teller;
using Octopus.ExceptionsHandler;
using Octopus.GUI.UserControl;
using Octopus.Services;
using Octopus.Shared;

namespace Octopus.GUI.TellerManagement
{
    public partial class FrmTellerOperation : SweetBaseForm
    {
        private OCurrency amount = 0;
        public TellerCashInEvent TellerCashInEvent { get; set; }
        public TellerCashOutEvent TellerCashOutEvent { get; set; }

        public FrmTellerOperation()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            cmbBranch.SelectedIndexChanged -= cmbBranch_SelectedIndexChanged;
            cmbTeller.SelectedIndexChanged -= cmbTeller_SelectedIndexChanged;
            InitializeBranch();
            InitializeTeller();
            cmbTeller.SelectedIndexChanged -= cmbTeller_SelectedIndexChanged;
            cmbBranch.SelectedIndexChanged += cmbBranch_SelectedIndexChanged;
        }

        private void InitializeBranch()
        {
            cmbBranch.ValueMember = "Name";
            cmbBranch.DisplayMember = "";
            cmbBranch.DataSource =
                ServicesProvider.GetInstance().GetBranchService().FindAllNonDeletedWithVault().OrderBy(item => item.Id).ToList();
        }

        private void InitializeTeller()
        {
            cmbTeller.ValueMember = "Name";
            cmbTeller.DisplayMember = "";
            List<Teller> tellers = ServicesProvider.GetInstance().GetTellerServices().FindAllNonDeletedTellersOfBranch(cmbBranch.SelectedItem as Branch);
            cmbTeller.DataSource = tellers;
        }

        private void GenerateEvents()
        {
            if (rbtnCashIn.Checked)
            {
                TellerCashInEvent = new TellerCashInEvent();
                TellerCashInEvent.Amount = amount;
                TellerCashInEvent.Date = TimeProvider.Now;
                TellerCashInEvent.TellerId = (cmbTeller.SelectedItem as Teller).Id;
                TellerCashInEvent.Description = tbxDescription.Text;
                ServicesProvider.GetInstance().GetEventProcessorServices().FireTellerEvent(TellerCashInEvent);
            }
            else if (rbtnCashOut.Checked)
            {
                TellerCashOutEvent = new TellerCashOutEvent();
                TellerCashOutEvent.Amount = amount;
                TellerCashOutEvent.Date = TimeProvider.Now;
                TellerCashOutEvent.TellerId = (cmbTeller.SelectedItem as Teller).Id;
                TellerCashOutEvent.Description = tbxDescription.Text;
                ServicesProvider.GetInstance().GetEventProcessorServices().FireTellerEvent(TellerCashOutEvent);
            }
        }

        private void cmbTeller_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeTeller();
        }

        private void tbxAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            int keyCode = e.KeyChar;

            if ((keyCode >= 48 && keyCode <= 57) || (keyCode == 8) || (Char.IsControl(e.KeyChar) && e.KeyChar
                                                                       != ((char)Keys.V | (char)Keys.ControlKey)) ||
                (Char.IsControl(e.KeyChar) && e.KeyChar !=
                 ((char)Keys.C | (char)Keys.ControlKey)) ||
                (e.KeyChar.ToString() == NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void tbxAmount_TextChanged(object sender, EventArgs e)
        {
            amount = Convert.ToDecimal(tbxAmount.Text);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateEvents();
                Close();
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
