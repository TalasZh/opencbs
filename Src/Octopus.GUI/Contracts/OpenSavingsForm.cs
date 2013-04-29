using System;
using Octopus.Shared;
using Octopus.CoreDomain.Products;
using Octopus.GUI.UserControl;

namespace Octopus.GUI.Contracts
{
    public partial class OpenSavingsForm : SweetBaseForm
    {
        private readonly SavingsBookProduct _savingsProduct;
        private OCurrency _initialAmount;
        private OCurrency _entryFees;
        private OCurrency _loanAmount;
        private readonly bool _isReopen;
        private bool UseCents { get { return _savingsProduct.Currency.UseCents; } }
        
        public OpenSavingsForm(OCurrency pInitialAmount, OCurrency pEntryFees, 
                               SavingsBookProduct savingsProduct, bool isReopen)
        {
            _savingsProduct = savingsProduct;
            _initialAmount = pInitialAmount;
            _entryFees = pEntryFees;
            _isReopen = isReopen;
            
            InitializeComponent();
            Initialize();

            if (_isReopen)
            {
                lbEntryFees.Text = GetString("lbReopenFees.Text");
                lbEntryFeesConfirmation.Text = GetString("lbReopenFeesConfirmation.Text");
                gbModifyInitialAmountEntryFees.Text = GetString("gbModifyInitialAmountReopenFees.Text");
            }
        }

        public OCurrency InitialAmount
        {
            get { return _initialAmount; }
        }

        public OCurrency EntryFees
        {
            get { return _entryFees; }
        }

        public void Initialize()
        {
            nudInitialAmount.DecimalPlaces = UseCents ? 2 : 0;
            nudInitialAmount.Minimum = _savingsProduct.InitialAmountMin.Value;
            nudInitialAmount.Maximum = _savingsProduct.InitialAmountMax.Value;
            nudInitialAmount.Text = _initialAmount.Value.ToString();

            if (_loanAmount.HasValue)
            {
                lbInitialAmountMinMax.Text = string.Format("{0} \n\r{1} ", "Min", "Max");
            }
            else
            {
                lbInitialAmountMinMax.Text = 
                    string.Format("{0}{1} {4}\r\n{2}{3} {4}",
                    "Min: ", _savingsProduct.InitialAmountMin.GetFormatedValue(UseCents),
                    "Max: ", _savingsProduct.InitialAmountMax.GetFormatedValue(UseCents),
                    _savingsProduct.Currency.Code);
            }

            OCurrency fees = 0;
            OCurrency feesMin = 0;
            OCurrency feesMax = 0;

            fees = _isReopen ? _savingsProduct.ReopenFees : _savingsProduct.EntryFees;
            feesMin = _isReopen ? _savingsProduct.ReopenFeesMin : _savingsProduct.EntryFeesMin;
            feesMax = _isReopen ? _savingsProduct.ReopenFeesMax : _savingsProduct.EntryFeesMax;
            

            if (fees.HasValue)
            {
                udEntryFees.Minimum = _entryFees.Value;
                udEntryFees.Maximum = _entryFees.Value;
                udEntryFees.Value = _entryFees.Value;

                lbEntryFeesMinMax.Text = string.Format("{0} {1}",
                    _entryFees.GetFormatedValue(UseCents), 
                    _savingsProduct.Currency.Code);
            }
            else
            {
                udEntryFees.DecimalPlaces = UseCents ? 2 : 0;
                udEntryFees.Minimum = feesMin.Value;
                udEntryFees.Maximum = feesMax.Value;
                if (_entryFees.Value >= udEntryFees.Minimum && _entryFees.Value <= udEntryFees.Maximum)
                    udEntryFees.Value = _entryFees.Value;
                else
                    udEntryFees.Value = feesMin.Value;
                
                lbEntryFeesMinMax.Text = string.Format("{0}{1} {4}\r\n{2}{3} {4}",
                                                       "Min: ", feesMin.GetFormatedValue(UseCents),
                                                       "Max: ", feesMax.GetFormatedValue(UseCents),
                                                       _savingsProduct.Currency.Code);
            }

            lbEntryFeesValue.Text =
                new OCurrency(udEntryFees.Value).GetFormatedValue(UseCents) 
                + " " + _savingsProduct.Currency.Code;
            lbInitialAmountValue.Text = 
                _initialAmount.GetFormatedValue(UseCents)
                + " " + _savingsProduct.Currency.Code;

            OCurrency total = _initialAmount + udEntryFees.Value;
            lbTotalAmountValue.Text = total.GetFormatedValue(UseCents) + " " + _savingsProduct.Currency.Code;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            _entryFees = udEntryFees.Value;
            _initialAmount = nudInitialAmount.Value;
        }

        private void udEntryFees_ValueChanged(object sender, EventArgs e)
        {
            OCurrency fees = udEntryFees.Value;
            OCurrency initialAmount = nudInitialAmount.Value;
            lbEntryFeesValue.Text = fees.GetFormatedValue(UseCents) + " " + _savingsProduct.Currency.Code;

            OCurrency total = initialAmount + udEntryFees.Value;
            lbTotalAmountValue.Text = string.Format("{0} {1}", 
                total.GetFormatedValue(UseCents), 
                _savingsProduct.Currency.Code);
        }

        private void nudInitialAmount_ValueChanged(object sender, EventArgs e)
        {
            OCurrency initialAmount = nudInitialAmount.Value;
            lbInitialAmountValue.Text = string.Format("{0} {1}",
                initialAmount.GetFormatedValue(UseCents), 
                _savingsProduct.Currency.Code);

            OCurrency total = initialAmount + udEntryFees.Value;
            lbTotalAmountValue.Text = string.Format("{0} {1}", 
                total.GetFormatedValue(UseCents), 
                _savingsProduct.Currency.Code);
        }
    }
}
