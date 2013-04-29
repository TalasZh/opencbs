using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Octopus.CoreDomain.Contracts.Loans;
using Octopus.CoreDomain.Contracts.Loans.Installments;
using Octopus.CoreDomain.Events;
using Octopus.CoreDomain.Events.Loan;
using Octopus.GUI.UserControl;

namespace Octopus.GUI
{
    public partial class EventCancelConfirmationForm : SweetBaseForm
    {
        public EventCancelConfirmationForm(Loan pContract, Event pEvent, List<Installment> archivedInstallments)
        {
            InitializeComponent();
            _contract = pContract;
            _archivedInstallments = archivedInstallments;
            _event = pEvent;

            if ((archivedInstallments == null) || (archivedInstallments.Count == 0))
            {
                listViewRepayments.Visible = false;
                cbShowCurrentState.Visible = false;
                lblComeBackToState.Visible = false;
                DisplayEvent();
            }
            else
            {
                listViewEvents.Visible = false;
                lblConfirmEventDelete.Visible = false;
                DisplayInstallmentsForRepaymentsStatus();
            }            
        }

        private readonly Loan _contract;
        private readonly List<Installment> _archivedInstallments;
        private readonly Event _event;
        private string _comment;

        public string Comment
        {
            get { return _comment; }
        }

        private void DisplayEvent()
        {
            listViewEvents.Items.Clear();

            ListViewItem listViewItem = new ListViewItem(_event.Date.ToShortDateString());
            listViewItem.SubItems.Add(_event.Code);
            listViewItem.Tag = _event;

            if (_event is LoanDisbursmentEvent)
            {
                LoanDisbursmentEvent evt = _event as LoanDisbursmentEvent;
                listViewItem.SubItems.Add(evt.Amount.GetFormatedValue(true));
                listViewItem.SubItems.Add("-");
                listViewItem.SubItems.Add("-");
            }
            else if (_event is RepaymentEvent)
            {
                RepaymentEvent evt = _event as RepaymentEvent;
                listViewItem.SubItems.Add(evt.Principal.GetFormatedValue(true));
                listViewItem.SubItems.Add(evt.Interests.GetFormatedValue(true));
                listViewItem.SubItems.Add(evt.Fees.GetFormatedValue(true));
            }
            else if (_event is BadLoanRepaymentEvent)
            {
                BadLoanRepaymentEvent evt = _event as BadLoanRepaymentEvent;
                listViewItem.SubItems.Add(evt.Principal.GetFormatedValue(true));
                listViewItem.SubItems.Add(evt.Interests.GetFormatedValue(true));
                listViewItem.SubItems.Add(evt.Fees.GetFormatedValue(true));
            }
            else if (_event is TrancheEvent)
            {
                TrancheEvent evt = _event as TrancheEvent;
                listViewItem.SubItems.Add(evt.Amount.GetFormatedValue(true));
                listViewItem.SubItems.Add(evt.InterestRate.GetFormatedValue(true));
                listViewItem.SubItems.Add("-");
            }
            else if (_event is RegEvent || _event is WriteOffEvent || _event is LoanValidationEvent)
            {
                listViewItem.SubItems.Add("-");
                listViewItem.SubItems.Add("-");
                listViewItem.SubItems.Add("-");
            }
            else if (_event is RescheduledLoanRepaymentEvent)
            {
                RescheduledLoanRepaymentEvent evt = _event as RescheduledLoanRepaymentEvent;
                listViewItem.SubItems.Add(evt.Principal.GetFormatedValue(true));
                listViewItem.SubItems.Add(evt.Interests.GetFormatedValue(true));
                listViewItem.SubItems.Add(evt.Fees.GetFormatedValue(true));
            }
            else if (_event is RescheduleLoanEvent)
            {
                RescheduleLoanEvent evt = _event as RescheduleLoanEvent;
                listViewItem.SubItems.Add(evt.Amount.GetFormatedValue(true));
                listViewItem.SubItems.Add("-");
                listViewItem.SubItems.Add("-");
            }
            listViewItem.SubItems.Add(_event.Cancelable.ToString());

            if (_event.Deleted)
            {
                listViewItem.BackColor = Color.FromArgb(188, 209, 199);
                listViewItem.ForeColor = Color.White;
            }
            listViewEvents.Items.Add(listViewItem);
        }

        private void DisplayInstallmentsForRepaymentsStatus()
        {
            listViewRepayments.Items.Clear();
            List<Installment> installments = cbShowCurrentState.Checked
                                                 ? _contract.InstallmentList
                                                 : _archivedInstallments;
            foreach (IInstallment installment in installments)
            {
                ListViewItem listViewItem = new ListViewItem(installment.Number.ToString());
                if (installment.IsRepaid)
                {
                    listViewItem.BackColor = Color.FromArgb(0, 88, 56);
                    listViewItem.ForeColor = Color.White;
                } 
                listViewItem.SubItems.Add(installment.ExpectedDate.ToShortDateString());
                listViewItem.SubItems.Add(installment.InterestsRepayment.GetFormatedValue(_contract.UseCents));
                listViewItem.SubItems.Add(installment.CapitalRepayment.GetFormatedValue(_contract.UseCents));

                if (installment.PaidInterests == 0)
                    listViewItem.SubItems.Add("-");
                else
                    listViewItem.SubItems.Add(installment.PaidInterests.GetFormatedValue(_contract.UseCents));

                if (installment.PaidCapital == 0)
                    listViewItem.SubItems.Add("-");
                else
                    listViewItem.SubItems.Add(installment.PaidCapital.GetFormatedValue(_contract.UseCents));

                if (installment.PaidDate.HasValue)
                    listViewItem.SubItems.Add(installment.PaidDate.Value.ToShortDateString());
                else
                    listViewItem.SubItems.Add("-");
                listViewRepayments.Items.Add(listViewItem);
            }
        }

        private void cbShowCurrentState_CheckedChanged(object sender, EventArgs e)
        {
            DisplayInstallmentsForRepaymentsStatus();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            _comment = textBoxComments.Text;
            string message = GetString("ConfirmCancelLastEvent");
            string caption = GetString("Confirm");
            DialogResult res = MessageBox.Show(message, caption, MessageBoxButtons.YesNo);
            DialogResult = res == DialogResult.Yes ? DialogResult.OK : DialogResult.Cancel;
        }

        private void textBoxComments_TextChanged(object sender, EventArgs e)
        {
            _comment = textBoxComments.Text;
        }
    }
}