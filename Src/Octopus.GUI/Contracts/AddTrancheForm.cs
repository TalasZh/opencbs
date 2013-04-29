using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Octopus.CoreDomain.Clients;
using Octopus.CoreDomain.Contracts.Loans;
using Octopus.CoreDomain.Contracts.Loans.Installments;
using Octopus.Enums;
using Octopus.ExceptionsHandler;
using Octopus.GUI.UserControl;
using Octopus.MultiLanguageRessources;
using Octopus.Services;
using Octopus.Shared;

namespace Octopus.GUI.Contracts
{
    public partial class AddTrancheForm : SweetBaseForm
    {
        private bool _interestRateChanged;
        private bool _chargeInterestDuringShift;
        private bool _chargeInterestDuringGracePeriod;
        private int numberOfMaturity;
        private decimal _IR;
        private int _dateOffsetOrAmount;
        private DateTime _trancheDate;
        private Loan _contract;
        public DialogResult resultReschedulingForm;
        private Loan LoanWithNewTranche;
        private int _gracePeriod;
        private IClient _client;

        public AddTrancheForm(Loan contract, IClient pClient)
        {
            InitializeComponent();
            _client = pClient;
            this._contract = contract;
            labelContractCode.Text = contract.Code;
            labelMaturityUnity.Text = contract.InstallmentType.Name;
            _IR = Convert.ToDecimal(contract.InterestRate);
            numericUpDownNewIR.Value = _IR * 100;
            numericUpDownNewIR.Text = (_IR * 100).ToString();
            if (contract.Product.InterestRate.HasValue) { /* checkBoxIRChanged.Enabled = false; */ }
            else
            {
                numericUpDownNewIR.Minimum = Convert.ToDecimal(contract.Product.InterestRateMin * 100);
                numericUpDownNewIR.Maximum = Convert.ToDecimal(contract.Product.InterestRateMax * 100);
            }
            DisplayInstallmentsForRepaymentsStatus(this._contract);
            InitializeTrancheComponents();
            CenterToScreen();
        }

        public void InitializeTrancheComponents()
        {
            labelStartDate.Visible = true;
            dateTimePickerStartDate.Visible = true;
            cbApplynewInterestforOLB.Visible = true;
            cbApplynewInterestforOLB.Enabled = true;
            labelShiftDateDays.Text = @" + " + Contract.GetRemainAmount().GetFormatedValue(Contract.Product.Currency.UseCents);
            buttonConfirm.Text = GetString("AddTranche.Text");
            Text = GetString("AddTranche.Text");
            cbApplynewInterestforOLB.Text = GetString("ApplynewInterestforOLB.Text");
            labelShiftDate.Text = GetString("TranchePrincipal.Text");

            dateTimePickerStartDate.Value = TimeProvider.Now;

            if (Contract.Product.NbOfInstallments != null)
            {
                numericUpDownMaturity.Maximum = (decimal)Contract.Product.NbOfInstallments;
                numericUpDownMaturity.Minimum = (decimal)Contract.Product.NbOfInstallments;
            }
            else
            {
                numericUpDownMaturity.Maximum = (decimal)Contract.Product.NbOfInstallmentsMax;
                numericUpDownMaturity.Minimum = (decimal)Contract.Product.NbOfInstallmentsMin;
            }
        }

        private void DisplayInstallmentsForRepaymentsStatus(Loan contractToDisplay)
        {
            listViewRepayments.Items.Clear();
            foreach (Installment installment in contractToDisplay.InstallmentList)
            {
                ListViewItem listViewItem = new ListViewItem(installment.Number.ToString());
                if (installment.IsRepaid)
                {
                    listViewItem.BackColor = Color.FromArgb(((Byte)(0)), ((Byte)(88)), ((Byte)(56)));
                    listViewItem.ForeColor = Color.White;
                }
                listViewItem.Tag = installment;
                listViewItem.SubItems.Add(installment.ExpectedDate.ToShortDateString());
                listViewItem.SubItems.Add(installment.InterestsRepayment.GetFormatedValue(contractToDisplay.UseCents));
                listViewItem.SubItems.Add(installment.CapitalRepayment.GetFormatedValue(contractToDisplay.UseCents));
                listViewItem.SubItems.Add(installment.Amount.GetFormatedValue(contractToDisplay.UseCents));

                if (ServicesProvider.GetInstance().GetGeneralSettings().IsOlbBeforeRepayment)
                    listViewItem.SubItems.Add(installment.OLB.GetFormatedValue(contractToDisplay.UseCents));
                else
                    listViewItem.SubItems.Add(installment.OLBAfterRepayment.GetFormatedValue(contractToDisplay.UseCents));

                listViewItem.SubItems.Add(installment.PaidInterests.GetFormatedValue(contractToDisplay.UseCents));
                listViewItem.SubItems.Add(installment.PaidCapital.GetFormatedValue(contractToDisplay.UseCents));
                if (installment.PaidDate.HasValue)
                    listViewItem.SubItems.Add(installment.PaidDate.Value.ToShortDateString());
                else
                    listViewItem.SubItems.Add("-");
                listViewRepayments.Items.Add(listViewItem);
            }
        }

        public Loan Contract
        {
            get { return _contract; }
        }

        private void _GetParameters()
        {
            numberOfMaturity = Convert.ToInt32(numericUpDownMaturity.Value);

            try
            {
                _dateOffsetOrAmount = Int32.Parse(tbDateOffset.Text);
            }
            catch
            {
                _dateOffsetOrAmount = 0;
            }

            _interestRateChanged = cbApplynewInterestforOLB.Checked;

            _chargeInterestDuringShift = false;
            _chargeInterestDuringGracePeriod = false;
            _IR = Convert.ToDecimal(numericUpDownNewIR.Value / 100);
            _trancheDate = dateTimePickerStartDate.Value.Date;
            _gracePeriod = 0;
        }

        private void numericUpDownNewIR_ValueChanged(object sender, EventArgs e)
        {
            _GetParameters();
            if ((_dateOffsetOrAmount != 0) && (numberOfMaturity > 0))
            {
                LoanWithNewTranche = _contract;

                Loan fakeContract = ServicesProvider.GetInstance().GetContractServices().FakeTranche(LoanWithNewTranche,
                                                                                                     _trancheDate,
                                                                                                     numberOfMaturity,
                                                                                                     _dateOffsetOrAmount,
                                                                                                     _interestRateChanged,
                                                                                                     _IR);
                DisplayInstallmentsForRepaymentsStatus(fakeContract);
            }
        }

        private void numericUpDownMaturity_ValueChanged(object sender, EventArgs e)
        {
            _GetParameters();
            if ((_dateOffsetOrAmount != 0) && (numberOfMaturity > 0))
            {
                LoanWithNewTranche = _contract;

                Loan fakeContract = ServicesProvider.GetInstance().GetContractServices().FakeTranche(LoanWithNewTranche,
                                                                                                     _trancheDate,
                                                                                                     numberOfMaturity,
                                                                                                     _dateOffsetOrAmount,
                                                                                                     _interestRateChanged,
                                                                                                     _IR);
                DisplayInstallmentsForRepaymentsStatus(fakeContract);
            }
        }

        private void tbDateOffset_TextChanged(object sender, EventArgs e)
        {
            _GetParameters();
            if ((_dateOffsetOrAmount != 0) && (numberOfMaturity > 0))
            {
                LoanWithNewTranche = _contract;

                Loan fakeContract = ServicesProvider.GetInstance().GetContractServices().FakeTranche(LoanWithNewTranche,
                                                                                                     _trancheDate,
                                                                                                     numberOfMaturity,
                                                                                                     _dateOffsetOrAmount,
                                                                                                     _interestRateChanged,
                                                                                                     _IR);
                DisplayInstallmentsForRepaymentsStatus(fakeContract);
            }
        }

        private void tbDateOffset_KeyDown(object sender, KeyEventArgs e)
        {
            Keys c = e.KeyCode;
            
            if (c >= Keys.NumPad0 && c <= Keys.NumPad9) return;
            if (c >= Keys.D0 && c <= Keys.D9) return;
            if (e.KeyValue==110 || e.KeyValue==188) return;
            if (e.Control && (c == Keys.X || c == Keys.C || c == Keys.V || c == Keys.Z)) return;
            if (c == Keys.Delete || c == Keys.Back) return;
            if (c == Keys.Left || c == Keys.Right || c == Keys.Up || c == Keys.Down) return;
            e.SuppressKeyPress = true;
        }

        private void tbDateOffset_Enter(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (0 == tb.Text.Length) tb.Text = @"0";
        }

        private void dateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
        {
            _GetParameters();
            if ((_dateOffsetOrAmount != 0) && (numberOfMaturity > 0))
            {
                LoanWithNewTranche = _contract;

                Loan fakeContract =
                    ServicesProvider.GetInstance().GetContractServices().FakeTranche(LoanWithNewTranche,
                                                                                     _trancheDate,
                                                                                     numberOfMaturity,
                                                                                     _dateOffsetOrAmount,
                                                                                     _interestRateChanged,
                                                                                     _IR);
                DisplayInstallmentsForRepaymentsStatus(fakeContract);
            }
        }

        private void cbApplynewInterestforOLB_CheckedChanged(object sender, EventArgs e)
        {
            _GetParameters();
            if ((_dateOffsetOrAmount != 0) && (numberOfMaturity > 0))
            {
                LoanWithNewTranche = _contract;

                Loan fakeContract = ServicesProvider.GetInstance().GetContractServices().FakeTranche(LoanWithNewTranche,
                                                                                                     _trancheDate,
                                                                                                     numberOfMaturity,
                                                                                                     _dateOffsetOrAmount,
                                                                                                     _interestRateChanged,
                                                                                                     _IR);
                DisplayInstallmentsForRepaymentsStatus(fakeContract);
            }
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            string messageConfirm = GetString("ConfirmTrancheContract.Text") + " " + _contract.Code;
            messageConfirm += "\n" + GetString("ChargeInterest.Text") + " " + (_interestRateChanged ? GetString("Yes.Text") : GetString("No.Text"));
            messageConfirm += "\n" + GetString("NewInstallment.Text") + " " + numberOfMaturity;
            messageConfirm += "\n" + GetString("InterestRate.Text") + " " + _IR*100 + "%";

            resultReschedulingForm = MessageBox.Show(messageConfirm, GetString("ConfirmTheTranche.Text"),
                                                     MessageBoxButtons.OKCancel,
                                                     MessageBoxIcon.Question);

            if (resultReschedulingForm == DialogResult.OK)
            {
                try
                {
                    {

                        _contract = ServicesProvider.GetInstance().GetContractServices().
                            AddTranche(_contract, _client, _trancheDate, numberOfMaturity, _dateOffsetOrAmount,
                                       _interestRateChanged, _IR);
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                }
                catch (Exception ex)
                {
                    new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddTrancheForm_Load(object sender, EventArgs e)
        {
        }
    }
}
