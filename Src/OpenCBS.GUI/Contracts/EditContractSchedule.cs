using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Contracts;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.GUI.Contracts
{
    public partial class EditContractSchedule : Form
    {
        public List<Installment> Installments = new List<Installment>();
        private Loan _loan;
        private readonly Loan _initialLoan;
        private bool _isEditable;

        public EditContractSchedule()
        {
            InitializeComponent();
        }

        public EditContractSchedule(ref Loan pLoan)
        {
            InitializeComponent();
            if (pLoan.Product.LoanType == OLoanTypes.Flat && !pLoan.UseCents)
            {
                pnlRounding.Visible = true;
                rbtnRoundTo5.Visible = true;
                rbtnRoundTo10.Visible = true;
                rbtnInitialSchedule.Visible = true;
            }
            if (pLoan.Product.LoanType == OLoanTypes.DecliningFixedPrincipal)
            {
                pnlRounding.Visible = true;
                chxAutomaticCalculation.Visible = true;
            }

            _loan = pLoan;
            _initialLoan = _loan;
            InitializeSchedule();
        }

        void InitializeSchedule()
        {
            lvSchedule.Items.Clear();
            //fill listview
            foreach (Installment installment in _loan.InstallmentList)
            {
                ListViewItem lvi = new ListViewItem { Tag = installment, Text = installment.Number.ToString() };
                lvi.SubItems.Add(installment.ExpectedDate.ToShortDateString());
                lvi.SubItems.Add(installment.InterestsRepayment.GetFormatedValue(_loan.UseCents));
                lvi.SubItems.Add(installment.CapitalRepayment.GetFormatedValue(_loan.UseCents));
                lvi.SubItems.Add((installment.InterestsRepayment + installment.CapitalRepayment).GetFormatedValue(_loan.UseCents));
                lvi.SubItems.Add(installment.OLB.GetFormatedValue(_loan.UseCents));
                lvSchedule.Items.Add(lvi);
            }

            // add summury
            ListViewItem sum = new ListViewItem("Σ");
            sum.Font = new Font(lvSchedule.Font, FontStyle.Bold);
            sum.UseItemStyleForSubItems = false;
            sum.SubItems.Add("", Color.Black, Color.White, sum.Font);
            sum.SubItems.Add("", Color.Black, Color.White, sum.Font);
            sum.SubItems.Add("", Color.Black, Color.White, sum.Font);
            sum.SubItems.Add("", Color.Black, Color.White, sum.Font);
            sum.SubItems.Add("", Color.Black, Color.White, sum.Font);
            lvSchedule.Items.Add(sum);

            UpdateAmount();
        }

        void UpdateAmount()
        {
            OCurrency principalAmount = 0;
            OCurrency interestAmount = 0;

            for (int i = 0; i <= _loan.InstallmentList.Count - 1;i++ )
            {
                Installments.Add((Installment)(lvSchedule.Items[i].Tag));
                principalAmount += (lvSchedule.Items[i].Tag as Installment).CapitalRepayment;
                interestAmount += (lvSchedule.Items[i].Tag as Installment).InterestsRepayment;
            }

            Color fg = principalAmount != _loan.Amount ? Color.Red : Color.Black;
            
            //update principal
            for (int i = 0; i <= _loan.InstallmentList.Count - 1; i++)
            {
                lvSchedule.Items[i].UseItemStyleForSubItems = false;
                lvSchedule.Items[i].SubItems[3].ForeColor = fg;
            }

            btnOK.Enabled = principalAmount != _loan.Amount || !_isEditable ? false : true;

            //update OLB
            OCurrency olb = _loan.Amount;
            for (int i = 0; i <= _loan.InstallmentList.Count - 1; i++)
            {
                lvSchedule.Items[i].UseItemStyleForSubItems = false;
                lvSchedule.Items[i].SubItems[5].Text = olb.GetFormatedValue(_loan.UseCents);
                olb -= ((Installment)(lvSchedule.Items[i].Tag)).CapitalRepayment;
                lvSchedule.Items[i].SubItems[5].ForeColor = Color.Black;
            }

            //update Interest
            if (chxAutomaticCalculation.Checked)
            {
                for (int i = 0; i <= _loan.InstallmentList.Count - 1; ++i)
                {
                    lvSchedule.Items[i].SubItems[2].Text = GetFormattedValue(Convert.ToDecimal(lvSchedule.Items[i].SubItems[5].Text)*(_loan.InterestRate));
                }
            }

            //update total amount
            for (int i = 0; i <= _loan.InstallmentList.Count - 1; i++)
            {
                lvSchedule.Items[i].UseItemStyleForSubItems = false;
                lvSchedule.Items[i].SubItems[4].Text = (((Installment)(lvSchedule.Items[i].Tag)).CapitalRepayment + ((Installment)(lvSchedule.Items[i].Tag)).InterestsRepayment).GetFormatedValue(_loan.UseCents);
                lvSchedule.Items[i].SubItems[4].ForeColor = Color.Black;
            }

            //update total amount 
            int index = lvSchedule.Items.Count - 1;
            lvSchedule.Items[index].SubItems[3].Text = principalAmount.GetFormatedValue(_loan.UseCents);
            lvSchedule.Items[index].SubItems[3].ForeColor = Color.Black;
            lvSchedule.Items[index].SubItems[2].Text = interestAmount.GetFormatedValue(_loan.UseCents);
            lvSchedule.Items[index].SubItems[2].ForeColor = Color.Black;
        }

        private void lvSchedule_SubItemClicked(object sender, UserControl.SubItemEventArgs e)
        {
            if (e.Item.Index < _loan.InstallmentList.Count)
            {
                if (1 == e.SubItem)
                {
                    dateTimePicker.Value = ((Installment) (e.Item.Tag)).ExpectedDate;
                    lvSchedule.StartEditing(dateTimePicker, e.Item, e.SubItem);
                }
                else if (2 == e.SubItem)
                {
                    textBox.Text = ((Installment) (e.Item.Tag)).InterestsRepayment.ToString();
                    lvSchedule.StartEditing(textBox, e.Item, e.SubItem);
                }
                else if (3 == e.SubItem)
                {
                    textBox.Text = GetFormattedValue(((Installment) (e.Item.Tag)).CapitalRepayment.Value);
                    lvSchedule.StartEditing(textBox, e.Item, e.SubItem);
                }
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            int keyCode = e.KeyChar;

            if ((keyCode >= 48 && keyCode <= 57) || (keyCode == 8) || (Char.IsControl(e.KeyChar) && e.KeyChar
                != ((char)Keys.V | (char)Keys.ControlKey)) || (Char.IsControl(e.KeyChar) && e.KeyChar !=
                ((char)Keys.C | (char)Keys.ControlKey)) || (e.KeyChar.ToString() ==
                System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Installments.Clear();

            for (int i = 0; i <= _loan.InstallmentList.Count - 1; i++)
            {
                Installments.Add(((Installment)(lvSchedule.Items[i].Tag)));
            }

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void lvSchedule_SubItemEndEditing(object sender, UserControl.SubItemEndEditingEventArgs e)
        {
            if (1 == e.SubItem)
            {
                _isEditable = true;
                if (string.IsNullOrEmpty(e.DisplayText))
                    e.DisplayText = ((Installment)(e.Item.Tag)).ExpectedDate.ToString();
               ((Installment)(e.Item.Tag)).ExpectedDate = DateTime.Parse(e.DisplayText);
            }
            else if (2 == e.SubItem)
            {
                _isEditable = true;
                if (string.IsNullOrEmpty(e.DisplayText))
                    e.DisplayText = GetFormattedValue(((Installment)(e.Item.Tag)).InterestsRepayment.Value);
                ((Installment)(e.Item.Tag)).InterestsRepayment = decimal.Parse(e.DisplayText);

            }
            else if (3 == e.SubItem)
            {
                _isEditable = true;
                if (string.IsNullOrEmpty(e.DisplayText))
                    e.DisplayText = GetFormattedValue(((Installment)(e.Item.Tag)).CapitalRepayment.Value);
                ((Installment)(e.Item.Tag)).CapitalRepayment = decimal.Parse(e.DisplayText);
            }

            Installments.Clear();
            UpdateAmount();
        }

        private string GetFormattedValue(decimal? value)
        {
            var val = value ?? 0;
            var roundPrecision = _loan.UseCents ? 2 : 0;
            //var fmt = pUseCents ? "{0:N}" : "{0:G}";
            val = Math.Round(val, roundPrecision, MidpointRounding.AwayFromZero);

            // Gets a NumberFormatInfo
            NumberFormatInfo nfi = new CultureInfo(CultureInfo.CurrentCulture.ToString(), false).NumberFormat;
            //copying settings
            nfi.NumberGroupSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
            nfi.NumberDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            nfi.CurrencyGroupSeparator = CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator;
            nfi.CurrencyDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            nfi.NumberDecimalDigits = roundPrecision;

            return val.ToString("N", nfi);
        }

        private void RoundSchedule(int roundTo)
        {
            _isEditable = true;
            FlatRoundingSchedule frs = new FlatRoundingSchedule();
            _loan = frs.EditSchedule(_initialLoan, roundTo);
            InitializeSchedule();
        }

        private void rbtnRoundTo5_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnRoundTo5.Checked)
                RoundSchedule(5);
        }

        private void rbtnRoundTo10_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnRoundTo10.Checked)
                RoundSchedule(10);
        }

        private void rbtnInitialSchedule_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnInitialSchedule.Checked)
            {
                RoundSchedule(0);
            }
        }

        private void cbxAutomaticCalculation_CheckedChanged(object sender, EventArgs e)
        {
            if (chxAutomaticCalculation.Checked)
                UpdateAmount();
        }
    }
}
