//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright ?2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Octopus.CoreDomain;
using Octopus.CoreDomain.Accounting;
using Octopus.CoreDomain.Contracts.Loans.Installments;
using Octopus.CoreDomain.FundingLines;
using Octopus.CoreDomain.LoanCycles;
using Octopus.CoreDomain.Products;
using Octopus.Enums;
using Octopus.ExceptionsHandler;
using Octopus.GUI.UserControl;
using Octopus.MultiLanguageRessources;
using Octopus.Services;
using Octopus.Shared.Settings;

namespace Octopus.GUI.Products
{
	/// <summary>
	/// Description résumée de AddPackage.
	/// </summary>
	public partial class FrmAddLoanProduct :SweetBaseForm
	{
        private LoanProduct _product;
        private InstallmentType _selectInstallmentType;
        private ExoticInstallmentsTable _exoticProduct;
        private ExoticInstallment _exoticInstallment;
        private bool _useExistingExoticProduct;
        private bool _useExoticProduct;
	    private bool _ischangeFee;
        private int _checkBoxCounter;//for check if one or more client types are selected
        private int _idForNewEntryFee;
        private List<CycleObject> _cycleObjects = new List<CycleObject>();
        private Cycle _editedParam;

        private const int IdxId = 0;

        private const int IdxNameOfFee = 1;
        private const int IdxMin = 2;
        private const int IdxMax = 3;
        private const int IdxValue = 4;
        private const int IdxIsRate = 5;
        private const int IdxIsAdded = 6;
        private const int IdxCycleId = 7;
        private const int IdxIdForNewItem = 8;
        private const int IdxIndex = 9;

        #region Constructors
        public FrmAddLoanProduct()
        {
            InitializeComponent();
            InitializeLabelCurrency("");
            Initialization();
            InitializeCycleObjects();
            InitializeAdditionalEmptyRowInListView();

            if (_product.LoanAmountCycleParams == null) _product.LoanAmountCycleParams = new List<LoanAmountCycle>();
            if (_product.RateCycleParams == null) _product.RateCycleParams = new List<RateCycle>();
            if (_product.MaturityCycleParams == null) _product.MaturityCycleParams = new List<MaturityCycle>();
        }

        public FrmAddLoanProduct(LoanProduct pPackage)
        {
            InitializeComponent();
            InitializeLabelCurrency(pPackage.Currency.Name);

            _product = pPackage;
            _product.DeletedEntryFees = new List<EntryFee>();
            _product.AddedEntryFees = new List<EntryFee>();
            InitializeComboBoxFundingLine();
            InitializeComboBoxCurrencies();
            InitializePackageValues(pPackage);
            comboBoxCurrencies.Enabled = false;
            groupBoxInterestRateType.Enabled = false;
            if (_product.LoanAmountCycleParams == null) _product.LoanAmountCycleParams = new List<LoanAmountCycle>();
            if (_product.RateCycleParams == null) _product.RateCycleParams = new List<RateCycle>();
            if (_product.MaturityCycleParams == null) _product.MaturityCycleParams = new List<MaturityCycle>();
            foreach (RateCycle cycle in _product.RateCycleParams)
            {
                cycle.Min = cycle.Min * 100;
                cycle.Max = cycle.Max * 100;
            }

            Text = _product.PackageMode == OPackageMode.Edit ?
                MultiLanguageStrings.GetString(Ressource.FrmAddLoanProduct, "FormEditProductCaption.Text") :
                MultiLanguageStrings.GetString(Ressource.FrmAddLoanProduct, "FormAddProductCaption.Text");
            InitializeClientTypes();
            InitializeCycleObjects();
            InitializeEntryFees(pPackage);
            _ischangeFee = false;
        }

        private void InitializeEntryFees(LoanProduct pPackage)
        {
            cbEnableEntryFeesCycle.Checked = _product.UseEntryFeesCycles;
            FillListViewEntryFees();
            InitializeComboboxEntryFeeCycles(pPackage);
        }

        private void FillListViewEntryFees()
        {
            lvEntryFees.Items.Clear();
            int? cycleId = _product.EntryFees.Min(i => i.CycleId);
            foreach (EntryFee fee in _product.EntryFees)
            {
                if (fee.CycleId != cycleId)
                    continue;
                ListViewItem item = new ListViewItem(fee.Id.ToString()) { Tag = fee };
                item.SubItems.Add(fee.Name);
                item.SubItems.Add(fee.Min.HasValue ? fee.Min.Value.ToString(CultureInfo.CurrentCulture) : "");
                item.SubItems.Add(fee.Max.HasValue ? fee.Max.Value.ToString(CultureInfo.CurrentCulture) : "");
                item.SubItems.Add(fee.Value.HasValue ? fee.Value.Value.ToString(CultureInfo.CurrentCulture) : "");
                item.SubItems.Add(fee.IsRate.ToString());
                item.SubItems.Add(fee.IsAdded.ToString());
                item.SubItems.Add(fee.CycleId.HasValue ? fee.CycleId.Value.ToString(CultureInfo.CurrentCulture) : "");
                item.SubItems.Add("");
                item.SubItems.Add(lvEntryFees.Items.Count.ToString(CultureInfo.CurrentCulture));
                lvEntryFees.Items.Add(item);
            }
            InitializeAdditionalEmptyRowInListView();
        }

        private void InitializeAdditionalEmptyRowInListView()
        {
            if (lvEntryFees.Items.Count < 11)
            {
                EntryFee entryFee = new EntryFee();
                ListViewItem tItem = new ListViewItem("") { Tag = entryFee };
                tItem.SubItems.Add("");
                tItem.SubItems.Add("");
                tItem.SubItems.Add("");
                tItem.SubItems.Add("");
                tItem.SubItems.Add("");
                tItem.SubItems.Add("");
                tItem.SubItems.Add("");
                tItem.SubItems.Add("");
                tItem.SubItems.Add(lvEntryFees.Items.Count.ToString(CultureInfo.CurrentCulture));
                lvEntryFees.Items.Add(tItem);
            }
        }

        private void InitializeComboboxEntryFeeCycles(LoanProduct pPackage)
        {
            if (pPackage.EntryFeeCycles != null)
            {
                cmbEntryFeesCycles.Items.Clear();
                foreach (int feeCycle in pPackage.EntryFeeCycles)
                {
                    cmbEntryFeesCycles.Items.Add(feeCycle);
                }
                if (pPackage.EntryFeeCycles.Count > 0)
                {
                    cmbEntryFeesCycles.SelectedItem = pPackage.EntryFeeCycles[0];
                    lvEntryFees.Enabled = true;
                }
                else if (cbEnableEntryFeesCycle.Checked)
                    lvEntryFees.Enabled = true;
            }
        }

        #endregion

        private void Initialization()
        {
            _product = new LoanProduct();
            InitializeTextBox();
            InitializeComboBoxInstallmentType();
            _product.LoanType = OLoanTypes.Flat;
            InitializeComboBoxExoticProduct();
            InitializeComboBoxLoanCycles();
            InitializeComboBoxFundingLine();
            InitializeComboBoxCurrencies();
            _product.ClientType = '-';
            _product.ChargeInterestWithinGracePeriod = true;

            _product.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            _product.AnticipatedPartialRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;

            _product.KeepExpectedInstallment = true;
            _product.PackageMode = OPackageMode.Insert;
            InitializeClientTypes();
            _product.DeletedEntryFees = new List<EntryFee>();

            lvEntryFees.SubItemClicked += lvEntryFees_SubItemClicked;
            lvEntryFees.SubItemEndEditing += lvEntryFees_SubItemEndEditing;
            lvEntryFees.DoubleClickActivation = true;
            _ischangeFee = false;
        }

        private void InitializeCycleObjects()
        {
            _cycleObjects = ServicesProvider.GetInstance().GetProductServices().GetCycleObjects();
            foreach (var cycleObject in _cycleObjects)
            {
                cbxCycleObjects.Items.Add(cycleObject);
            }
            cbxCycleObjects.DisplayMember = "Name";
            cbxCycleObjects.ValueMember = "Id";
            cbxCycleObjects.SelectedIndex = 0;
        }

        private void InitializeClientTypes()
        {
            _product.ProductClientTypes = ServicesProvider.GetInstance().GetProductServices().SelectClientTypes();
            ServicesProvider.GetInstance().GetProductServices().GetAssignedTypes(_product.ProductClientTypes, _product.Id);
            foreach (ProductClientType clientType in _product.ProductClientTypes)
            {
                switch (clientType.TypeName)
                {
                    case "All":
                        clientTypeAllCheckBox.Checked = clientType.IsChecked;
                        break;
                    case "Group":
                        clientTypeGroupCheckBox.Checked = clientType.IsChecked;
                        break;
                    case "Individual":
                        clientTypeIndivCheckBox.Checked = clientType.IsChecked;
                        break;
                    case "Corporate":
                        clientTypeCorpCheckBox.Checked = clientType.IsChecked;
                        break;
                    case "Village":
                        clientTypeVillageCheckBox.Checked = clientType.IsChecked;
                        break;
                }
            }
        }

        private void InitializeComboBoxInstallmentType()
        {
            comboBoxInstallmentType.Items.Clear();
            List<InstallmentType> installmentTypeList = ServicesProvider.GetInstance().GetProductServices().FindAllInstallmentTypes();
            if (installmentTypeList != null)
            {
                foreach (InstallmentType installmentType in installmentTypeList)
                {
                    comboBoxInstallmentType.Items.Add(installmentType);
                }
            }
            _selectInstallmentType = new InstallmentType
            {
                Name = MultiLanguageStrings.GetString(
                   Ressource.FrmAddLoanProduct, "messageBoxDefaultInstallmentType.Text")
            };
            comboBoxInstallmentType.Items.Add(_selectInstallmentType);
            comboBoxInstallmentType.SelectedItem = _selectInstallmentType;
        }

        private void InitializeComboBoxLoanCycles()
        {
            comboBoxLoanCyclesName.Items.Clear();

            List<LoanCycle> loanCycles = ServicesProvider.GetInstance().GetProductServices().GetLoanCycles();
            foreach (var loanCycle in loanCycles)
            {
                comboBoxLoanCyclesName.Items.Add(loanCycle);
            }
        }

        private void InitializeComboBoxExoticProduct()
        {
            comboBoxExoticProduct.Items.Clear();
            List<ExoticInstallmentsTable> exoticProductList = ServicesProvider.GetInstance().GetProductServices().FindAllExoticProducts();

            foreach (ExoticInstallmentsTable e in exoticProductList)
            {
                if ((_product.IsDeclining && e.IsExoticProductForDecliningRatePackage) ||
                    (!_product.IsDeclining && e.IsExoticProductForFlatRatePackage))
                {
                    comboBoxExoticProduct.Items.Add(e);
                }
            }
        }

        private void InitializeComboBoxFundingLine()
        {
            comboBoxFundingLine.Items.Clear();
            comboBoxFundingLine.Text = MultiLanguageStrings.GetString(Ressource.FrmAddLoanProduct, "Fundingline.Text");
            List<FundingLine> FundingLineList = ServicesProvider.GetInstance().GetFundingLinesServices().SelectFundingLines();
            FundingLine line = new FundingLine { Name = MultiLanguageStrings.GetString(Ressource.FrmAddLoanProduct, "Fundingline.Text"), Id = 0 };
            comboBoxFundingLine.Items.Add(line);

            foreach (FundingLine fundingLine in FundingLineList)
            {
                comboBoxFundingLine.Items.Add(fundingLine);
            }
        }

        private void InitializeComboBoxCurrencies()
        {
            comboBoxCurrencies.Items.Clear();
            comboBoxCurrencies.Text = MultiLanguageStrings.GetString(Ressource.FrmAddLoanProduct, "Currency.Text");
            List<Currency> currencies = ServicesProvider.GetInstance().GetCurrencyServices().FindAllCurrencies();
            Currency line = new Currency { Name = MultiLanguageStrings.GetString(Ressource.FrmAddLoanProduct, "Currency.Text"), Id = 0 };
            comboBoxCurrencies.Items.Add(line);

            foreach (Currency cur in currencies)
            {
                comboBoxCurrencies.Items.Add(cur);
            }
        }

        private void InitializeLabelCurrency(string pCurrency)
        {
            labelLoanCycle.Text = pCurrency;
            labelLoanCycleMin.Text = pCurrency;
            labelLoanCycleMax.Text = pCurrency;

            labelLOCMinAmount.Text = pCurrency;
            labelLOCMaxAmount.Text = pCurrency;
            labelLOCAmount.Text = pCurrency;
        }

        private void InitializePackageValues(LoanProduct pack)
        {
            InitializeComboBoxInstallmentType();
            textBoxName.Text = pack.Name;
            comboBoxInstallmentType.Text = pack.InstallmentType.Name;
            tbCreditInsuranceMin.Text = pack.CreditInsuranceMin.ToString("0.00");
            tbCreditInsuranceMax.Text = pack.CreditInsuranceMax.ToString("0.00");

            if (!pack.NbOfInstallments.HasValue)
            {
                textBoxNbOfInstallmentMin.Text = pack.NbOfInstallmentsMin.ToString();
                textBoxNbOfInstallmentMax.Text = pack.NbOfInstallmentsMax.ToString();
            }
            else
                textBoxNbOfInstallment.Text = pack.NbOfInstallments.ToString();

            ApplicationSettings dataParam = ApplicationSettings.GetInstance(string.Empty);
            int decimalPlaces = dataParam.InterestRateDecimalPlaces;

            if (!pack.InterestRate.HasValue)
            {
                decimal roundedMin = Math.Round(pack.InterestRateMin.Value*100m, decimalPlaces);
                decimal roundedMax = Math.Round(pack.InterestRateMax.Value*100m, decimalPlaces);
                textBoxRateMax.Text = Convert.ToString(roundedMax);
                textBoxRateMin.Text = Convert.ToString(roundedMin);
            }
            else
            {
                decimal rounded = Math.Round(pack.InterestRate.Value*100m, decimalPlaces);
                textBoxRate.Text = Convert.ToString(rounded);
            }

            if (!pack.GracePeriod.HasValue)
            {
                textBoxGracePeriodMax.Text = pack.GracePeriodMax.ToString();
                textBoxGracePeriodMin.Text = pack.GracePeriodMin.ToString();
            }
            else
                textBoxGracePeriod.Text = pack.GracePeriod.ToString();


            if (!pack.AnticipatedTotalRepaymentPenalties.HasValue)
            {
                textBoxAnticipatedRepaymentPenaltiesMin.Text = (pack.AnticipatedTotalRepaymentPenaltiesMin * 100).ToString();
                textBoxAnticipatedRepaymentPenaltiesMax.Text = (pack.AnticipatedTotalRepaymentPenaltiesMax * 100).ToString();
            }
            else
                textBoxAnticipatedRepaymentPenalties.Text = (pack.AnticipatedTotalRepaymentPenalties * 100).ToString();

            if (!pack.AnticipatedPartialRepaymentPenalties.HasValue)
            {
                textBoxAnticipatedPartialRepaimentMin.Text = (pack.AnticipatedPartialRepaymentPenaltiesMin * 100).ToString();
                textBoxAnticipatedPartialRepaimentMax.Text = (pack.AnticipatedPartialRepaymentPenaltiesMax * 100).ToString();
            }
            else
                textBoxAnticipatedPartialRepaiment.Text = (pack.AnticipatedPartialRepaymentPenalties * 100).ToString();

            if (!pack.ChargeInterestWithinGracePeriod)
                radioButtonChargeInterestNo.Checked = true;
            else
                radioButtonChargeInterestYes.Checked = true;

            switch (pack.RoundingType)
            {
                case ORoundingType.Approximate:
                    comboBoxRoundingType.SelectedIndex = 0; break;
                case ORoundingType.Begin:
                    comboBoxRoundingType.SelectedIndex = 1; break;
                case ORoundingType.End:
                    comboBoxRoundingType.SelectedIndex = 2; break;
            }

            if (pack.CycleId != null)
            {
                cbUseLoanCycle.Checked = true;
                InitializeComboBoxLoanCycles();
                
                SelectLoanCycleItemInComboBox((int)_product.CycleId);
            }

            if (pack.Amount.HasValue)
            {
                textBoxAmount.Text = pack.Amount.GetFormatedValue(pack.UseCents);
            }
            else
            {
                textBoxAmountMax.Text = pack.AmountMax.GetFormatedValue(pack.UseCents);
                textBoxAmountMin.Text = pack.AmountMin.GetFormatedValue(pack.UseCents);
            }

            if (pack.NonRepaymentPenalties.InitialAmount.HasValue)
            {
                tBInitialAmountValue.Text = (pack.NonRepaymentPenalties.InitialAmount.Value * 100).ToString();
            }
            else
            {
                tBInitialAmountMax.Text = (pack.NonRepaymentPenaltiesMax.InitialAmount.Value * 100).ToString();
                tBInitialAmountMin.Text = (pack.NonRepaymentPenaltiesMin.InitialAmount.Value * 100).ToString();
            }

            if (pack.NonRepaymentPenalties.OverDuePrincipal.HasValue)
            {
                tBOverduePrincipalValue.Text = (pack.NonRepaymentPenalties.OverDuePrincipal.Value * 100).ToString();
            }
            else
            {
                tBOverduePrincipalMax.Text = (pack.NonRepaymentPenaltiesMax.OverDuePrincipal.Value * 100).ToString();
                tBOverduePrincipalMin.Text = (pack.NonRepaymentPenaltiesMin.OverDuePrincipal.Value * 100).ToString();
            }

            if (pack.NonRepaymentPenalties.OLB.HasValue)
            {
                tBOLBValue.Text = (pack.NonRepaymentPenalties.OLB.Value * 100).ToString();
            }
            else
            {
                tBOLBMax.Text = (pack.NonRepaymentPenaltiesMax.OLB.Value * 100).ToString();
                tBOLBMin.Text = (pack.NonRepaymentPenaltiesMin.OLB.Value * 100).ToString();
            }

            if (pack.NonRepaymentPenalties.OverDueInterest.HasValue)
            {
                tBOverdueInterestValue.Text = (pack.NonRepaymentPenalties.OverDueInterest.Value * 100).ToString();
            }
            else
            {
                tBOverdueInterestMax.Text = (pack.NonRepaymentPenaltiesMax.OverDueInterest.Value * 100).ToString();
                tBOverdueInterestMin.Text = (pack.NonRepaymentPenaltiesMin.OverDueInterest.Value * 100).ToString();
            }

            if (pack.AnticipatedTotalRepaymentPenaltiesBase == OAnticipatedRepaymentPenaltiesBases.RemainingOLB)
            {
                rbRemainingOLB.Checked = true;
            }
            else
            {
                rbRemainingInterest.Checked = true;
            }

            switch (pack.AnticipatedPartialRepaymentPenaltiesBase)
            {
                case OAnticipatedRepaymentPenaltiesBases.RemainingOLB:
                    rbPartialRemainingOLB.Checked = true;
                    break;
                case OAnticipatedRepaymentPenaltiesBases.RemainingInterest:
                    rbPartialRemainingInterest.Checked = true;
                    break;
                case OAnticipatedRepaymentPenaltiesBases.PrepaidPrincipal:
                    rbPrepaidPrincipal.Checked = true;
                    break;
            }

            if (pack.FundingLine != null)
                comboBoxFundingLine.Text = pack.FundingLine.Name;
            if (pack.Currency != null)
                comboBoxCurrencies.Text = pack.Currency.Name;

            switch (_product.LoanType)
            {
                case OLoanTypes.DecliningFixedPrincipal:
                    {
                        rbDecliningFixedPrincipal.Checked = true;
                        break;
                    }
                case OLoanTypes.Flat:
                    {
                        rbFlat.Checked = true;
                        break;
                    }
                case OLoanTypes.DecliningFixedInstallments:
                    {
                        rbDecliningFixedInstallments.Checked = true;
                        break;
                    }
                case OLoanTypes.DecliningFixedPrincipalWithRealInterest:
                    {
                        rbDecliningFixedPrincipalRelaInterest.Checked = true;
                        break;
                    }
            }

            textBoxCode.Text = pack.Code;
            textBoxGracePeriodLateFee.Text = pack.GracePeriodOfLateFees.ToString();
            
            if (pack.IsExotic)
            {
                groupBoxGracePeriod.Enabled = false;
                textBoxGracePeriodMin.Text = "";
                textBoxGracePeriodMax.Text = "";
                textBoxGracePeriod.Text = "0";
                checkBoxUseExceptionalInstallmen.Checked = true;
                InitializeComboBoxExoticProduct();
                comboBoxExoticProduct.Text = pack.ExoticProduct.Name;
            }

            pack.PackageMode = OPackageMode.Edit;
            checkBoxFlexPackage.Checked = pack.AllowFlexibleSchedule;

            // Guarantors and collateralls components initialization
            if (pack.UseGuarantorCollateral)
            {
                checkBoxGuarCollRequired.Checked = true;
                if (!pack.SetSeparateGuarantorCollateral)
                {
                    groupBoxTotGuarColl.Enabled = true;
                    groupBoxSepGuarColl.Enabled = false;
                    checkBoxSetSepCollGuar.Enabled = true;
                    checkBoxSetSepCollGuar.Checked = false;
                    textBoxCollGuar.Text = pack.PercentageTotalGuarantorCollateral.ToString();
                    textBoxGuar.Text = "0";
                    textBoxColl.Text = "0";
                }
                else
                {
                    groupBoxTotGuarColl.Enabled = false;
                    groupBoxSepGuarColl.Enabled = true;
                    checkBoxSetSepCollGuar.Enabled = true;
                    checkBoxSetSepCollGuar.Checked = true;
                    textBoxGuar.Text = pack.PercentageSeparateGuarantour.ToString();
                    textBoxColl.Text = pack.PercentageSeparateCollateral.ToString();
                    textBoxCollGuar.Text = "0";
                }
            }
            else
            {
                checkBoxSetSepCollGuar.Checked = false;
                checkBoxSetSepCollGuar.Enabled = false;
                groupBoxTotGuarColl.Enabled = false;
                groupBoxSepGuarColl.Enabled = false;
            }

            /* Line of credit */
            if (pack.DrawingsNumber.HasValue || pack.AmountUnderLoc.HasValue || pack.AmountUnderLocMin.HasValue ||
                pack.AmountUnderLocMax.HasValue || pack.MaturityLoc.HasValue || pack.MaturityLocMin.HasValue ||
                pack.MaturityLocMax.HasValue)
            {
                useLOCCheckBox.Checked = true;
                drawNumGroupBox.Enabled = true;
                maxLOCAmountGroupBox.Enabled = true;
                maxLOCMaturityGroupBox.Enabled = true;

                if (pack.DrawingsNumber.HasValue)
                    textBoxNumOfDrawings.Text = pack.DrawingsNumber.ToString();

                if (!pack.AmountUnderLoc.HasValue)
                {
                    textBoxAmountUnderLOCMin.Text = pack.AmountUnderLocMin.RoundingValue;
                    textBoxAmountUnderLOCMax.Text = pack.AmountUnderLocMax.RoundingValue;
                }
                else
                {
                    textBoxAmountUnderLOC.Text = pack.AmountUnderLoc.RoundingValue;
                }

                if (!pack.MaturityLoc.HasValue)
                {
                    textBoxLOCMaturityMin.Text = pack.MaturityLocMin.ToString();
                    textBoxLOCMaturityMax.Text = pack.MaturityLocMax.ToString();
                }
                else
                    textBoxLOCMaturity.Text = pack.MaturityLoc.ToString();
            }

            // Compulsory savings
            cbUseCompulsorySavings.Checked = pack.UseCompulsorySavings;
            gbCSAmount.Enabled = pack.UseCompulsorySavings;

            if (pack.UseCompulsorySavings)
            {
                if (pack.CompulsoryAmount.HasValue)
                    txbCompulsoryAmountValue.Text = pack.CompulsoryAmount.ToString();
                else
                {
                    txbCompulsoryAmountMin.Text = pack.CompulsoryAmountMin.ToString();
                    txbCompulsoryAmountMax.Text = pack.CompulsoryAmountMax.ToString();
                }
            }
        }

        private void InitializeTextBox()
        {
            textBoxName.Text = "";
            textBoxNbOfInstallment.Text = "";
            textBoxNbOfInstallmentMax.Text = "";
            textBoxNbOfInstallmentMin.Text = "";
            textBoxRate.Text = "";
            textBoxRateMax.Text = "";
            textBoxRateMin.Text = "";
            textBoxGracePeriod.Text = "";
            textBoxGracePeriodMax.Text = "";
            textBoxGracePeriodMin.Text = "";
            textBoxAmount.Text = "";
            textBoxAmountMin.Text = "";
            textBoxAmountMax.Text = "";
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateEntryFee();

                switch (_product.PackageMode)
                {
                    case OPackageMode.Insert:
                        {
                            ServicesProvider.GetInstance().GetProductServices().ParseFieldsAndCheckErrors(_product, _useExoticProduct, _checkBoxCounter);
                            ServicesProvider.GetInstance().GetProductServices().AddPackage(_product);
                            Close();
                            break;
                        }

                    case OPackageMode.Edit:
                        {
                            ServicesProvider.GetInstance().GetProductServices().ParseFieldsAndCheckErrors(_product, _useExoticProduct, _checkBoxCounter);

                            if (_ischangeFee)
                            {
                                if (MessageBox.Show(
                                    MultiLanguageStrings.GetString(Ressource.FrmAddLoanProduct, "messageUpdate.Text"),
                                    MultiLanguageStrings.GetString(Ressource.PackagesForm, "title.Text"),
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    ServicesProvider.GetInstance().GetProductServices().UpdatePackage(_product, true);
                                }
                            }
                            else
                            {
                                ServicesProvider.GetInstance().GetProductServices().UpdatePackage(_product, false);
                            }

                            Close();
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            _product.Name = ServicesHelper.CheckTextBoxText(textBoxName.Text);
            buttonSave.Enabled = true;
        }

        private void comboBoxInstallmentType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _product.InstallmentType = (InstallmentType)comboBoxInstallmentType.SelectedItem;
            buttonSave.Enabled = true;
        }

        private void textBoxRateMin_TextChanged(object sender, EventArgs e)
        {
            _product.InterestRateMin = ServicesHelper.ConvertStringToNullableDecimal(textBoxRateMin.Text, true);
            buttonSave.Enabled = true;
        }

        private void textBoxRateMax_TextChanged(object sender, EventArgs e)
        {
            _product.InterestRateMax = ServicesHelper.ConvertStringToNullableDecimal(textBoxRateMax.Text, true);
            buttonSave.Enabled = true;
        }

        private void textBoxRate_TextChanged(object sender, EventArgs e)
        {
            _product.InterestRate = ServicesHelper.ConvertStringToNullableDecimal(textBoxRate.Text, true);
            buttonSave.Enabled = true;
        }

        private void textBoxGracePeriodMin_TextChanged(object sender, EventArgs e)
        {
            _product.GracePeriodMin = ServicesHelper.ConvertStringToNullableInt32(textBoxGracePeriodMin.Text);
            buttonSave.Enabled = true;
        }

        private void textBoxGracePeriodMax_TextChanged(object sender, EventArgs e)
        {
            _product.GracePeriodMax = ServicesHelper.ConvertStringToNullableInt32(textBoxGracePeriodMax.Text);
            buttonSave.Enabled = true;
        }

        private void textBoxGracePeriod_TextChanged(object sender, EventArgs e)
        {
            _product.GracePeriod = ServicesHelper.ConvertStringToNullableInt32(textBoxGracePeriod.Text);
            buttonSave.Enabled = true;
        }

        private void textBoxNbOfInstallmentMin_TextChanged(object sender, EventArgs e)
        {
            _product.NbOfInstallmentsMin = ServicesHelper.ConvertStringToNullableInt32(textBoxNbOfInstallmentMin.Text);
            buttonSave.Enabled = true;
        }

        private void textBoxNbOfInstallmentMax_TextChanged(object sender, EventArgs e)
        {
            _product.NbOfInstallmentsMax = ServicesHelper.ConvertStringToNullableInt32(textBoxNbOfInstallmentMax.Text);
            buttonSave.Enabled = true;
        }

        private void textBoxNbOfInstallment_TextChanged(object sender, EventArgs e)
        {
            _product.NbOfInstallments = ServicesHelper.ConvertStringToNullableInt32(textBoxNbOfInstallment.Text);
            buttonSave.Enabled = true;
        }

        private void textBoxAnticipatedRepaymentPenaltiesMin_TextChanged(object sender, EventArgs e)
        {
            _product.AnticipatedTotalRepaymentPenaltiesMin =
                    ServicesHelper.ConvertStringToNullableDouble(textBoxAnticipatedRepaymentPenaltiesMin.Text, true);

            buttonSave.Enabled = true;
            _ischangeFee = true;
        }

        private void textBoxAnticipatedRepaymentPenaltiesMax_TextChanged(object sender, EventArgs e)
        {
            _product.AnticipatedTotalRepaymentPenaltiesMax =
                    ServicesHelper.ConvertStringToNullableDouble(textBoxAnticipatedRepaymentPenaltiesMax.Text, true);

            buttonSave.Enabled = true;
            _ischangeFee = true;
        }

        private void textBoxAnticipatedRepaymentPenalties_TextChanged(object sender, EventArgs e)
        {
            _product.AnticipatedTotalRepaymentPenalties =
                    ServicesHelper.ConvertStringToNullableDouble(textBoxAnticipatedRepaymentPenalties.Text, true);

            buttonSave.Enabled = true;
            _ischangeFee = true;
        }

        private void textBoxAmountMin_TextChanged(object sender, EventArgs e)
        {
            _product.AmountMin = ServicesHelper.ConvertStringToNullableDecimal(textBoxAmountMin.Text);
            buttonSave.Enabled = true;
        }

        private void textBoxAmountMax_TextChanged(object sender, EventArgs e)
        {
            _product.AmountMax = ServicesHelper.ConvertStringToNullableDecimal(textBoxAmountMax.Text);
            buttonSave.Enabled = true;
        }

        private void textBoxAmount_TextChanged(object sender, EventArgs e)
        {
            _product.Amount = ServicesHelper.ConvertStringToNullableDecimal(textBoxAmount.Text);
            buttonSave.Enabled = true;
        }

        private void radioButtonFlat_CheckedChanged(object sender, EventArgs e)
        {
            _CheckInterestRateType();
            buttonSave.Enabled = true;
            interestRateTextChanged();
        }

        private void CheckChargeInterestWithInGracePeriod()
        {
            _product.ChargeInterestWithinGracePeriod = !radioButtonChargeInterestNo.Checked;
        }

        private void radioButtonChargeInterestYes_CheckedChanged(object sender, EventArgs e)
        {
            CheckChargeInterestWithInGracePeriod();
            buttonSave.Enabled = true;
        }

        private void radioButtonChargeInterestNo_CheckedChanged(object sender, EventArgs e)
        {
            CheckChargeInterestWithInGracePeriod();
            buttonSave.Enabled = true;
        }

        private void CheckTotalAnticipatedRepaymentBase()
        {
            _product.AnticipatedTotalRepaymentPenaltiesBase = rbRemainingOLB.Checked
                                                                      ? OAnticipatedRepaymentPenaltiesBases.RemainingOLB
                                                                      : OAnticipatedRepaymentPenaltiesBases.RemainingInterest;

        }

        private void CheckPartialAnticipatedRepaymentBase()
        {
            if (rbPartialRemainingOLB.Checked)
                _product.AnticipatedPartialRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            else if (rbPartialRemainingInterest.Checked)
                _product.AnticipatedPartialRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingInterest;
            else if (rbPrepaidPrincipal.Checked)
                _product.AnticipatedPartialRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.PrepaidPrincipal;
        }

        private void radioButtonDeclining_CheckedChanged(object sender, EventArgs e)
        {
            _CheckInterestRateType();
            buttonSave.Enabled = true;
            interestRateTextChanged();
        }

        private void radioButtonRemainingOLB_CheckedChanged(object sender, EventArgs e)
        {
            CheckTotalAnticipatedRepaymentBase();
            _ischangeFee = true;
            buttonSave.Enabled = true;
        }

        private void radioButtonRemainingInterest_CheckedChanged(object sender, EventArgs e)
        {
            CheckTotalAnticipatedRepaymentBase();
            buttonSave.Enabled = true;
            _ischangeFee = true;
        }

        private void radioButtonDecliningFixedPrincipal_CheckedChanged(object sender, EventArgs e)
        {
            _CheckInterestRateType();
            buttonSave.Enabled = true;
            interestRateTextChanged();
        }

        private void _CancelSpecifiedAmount()
        {
            _product.Amount = null;
            _product.AmountMin = null;
            _product.AmountMax = null;
            textBoxAmount.Clear();
            textBoxAmountMin.Clear();
            textBoxAmountMax.Clear();
            buttonSave.Enabled = true;
        }

        private void _CheckAmountTypes(bool pUseSpecifiedAmount)
        {
            if (pUseSpecifiedAmount)
            {
                groupBoxAmount.Visible = true;
                groupBoxAmountCycles.Visible = false;
                _CancelSpecifiedAmount();
            }
            else
            {
                groupBoxAmount.Visible = false;
                groupBoxAmountCycles.Visible = true;
                _CancelAmountCycles();
            }
        }

        private void buttonNewAmountCycles_Click(object sender, EventArgs e)
        {
            try
            {
                InputBox inputBox = new InputBox();
                string inputBoxText = MultiLanguageStrings.GetString(Ressource.PackagesForm, "InputNameCaption.Text");
                inputBox.Text = inputBoxText;
                inputBox.ShowDialog();

                if (inputBox.Result != null)
                {
                    listViewLoanCycles.Items.Clear();
                    buttonRemoveAmountCycles.Enabled = false;
                    buttonAmountCyclesSave.Enabled = false;
                    buttonAddAmountCycle.Enabled = true;
                    panelAmountCycles.Enabled = true;
                    panelAmountCycles.Visible = true;
                    groupBoxAmountCycle.Text = MultiLanguageStrings.GetString(Ressource.FrmAddLoanProduct, "messageAmountCycle.Text");
                    buttonSave.Enabled = true;
                    LoanCycle loanCycle = new LoanCycle();
                    loanCycle.Name = inputBox.Result;
                    loanCycle.Id = ServicesProvider.GetInstance().GetProductServices().InsertLoanCycle(loanCycle);
                    InitializeComboBoxLoanCycles();
                    SelectLoanCycleItemInComboBox(loanCycle.Id);
                    groupBoxAmountCycle.Text = MultiLanguageStrings.GetString(Ressource.FrmAddLoanProduct, "messageAmountCycle.Text");
                }
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void SelectLoanCycleItemInComboBox(int loanCycleId)
        {
            if (comboBoxLoanCyclesName.Items.Count == 0) return;
            for (int i = 0; i < comboBoxLoanCyclesName.Items.Count; i++)
            {
                if (((LoanCycle)comboBoxLoanCyclesName.Items[i]).Id == loanCycleId)
                {
                    comboBoxLoanCyclesName.SelectedItem = comboBoxLoanCyclesName.Items[i];
                    break;
                }
            }
        }

        private void buttonCancelAmountCycles_Click(object sender, EventArgs e)
        {
            _CancelAmountCycles();
        }

        private void buttonCancelExoticProduct_Click(object sender, EventArgs e)
        {
            _CancelExoticProduct();
        }

        private void _CancelAmountCycles()
        {
            panelAmountCycles.Enabled = false;
            listViewLoanCycles.Items.Clear();
            panelAmountCycles.Visible = false;
            comboBoxLoanCyclesName.Text = MultiLanguageStrings.GetString(Ressource.FrmAddLoanProduct, "messageSelectAmountCycles.Text");
            buttonSave.Enabled = true;
        }

        private void _CancelExoticProduct()
        {
            panelExoticInstallment.Enabled = false;
            listViewExoticInstallments.Items.Clear();
            panelExoticInstallment.Visible = false;
            panelExoticProduct.Visible = false;
            _exoticProduct = null;
            _product.ExoticProduct = null;
            comboBoxExoticProduct.Text = MultiLanguageStrings.GetString(Ressource.FrmAddLoanProduct, "messageSelectExotic.Text");
            groupBoxExoticProducts.Size = new Size(754, 298);
            buttonSave.Enabled = true;
        }

        private void _InitializeListViewExoticInstallments(ExoticInstallmentsTable pExoticProduct)
        {
            listViewExoticInstallments.Items.Clear();
            for (int i = 0; i < pExoticProduct.GetNumberOfInstallments; i++)
            {
                ExoticInstallment exoInstallment = pExoticProduct.GetExoticInstallment(i);
                ListViewItem listViewItem = new ListViewItem(exoInstallment.Number.ToString());

                listViewItem.SubItems.Add((exoInstallment.PrincipalCoeff * 100).ToString());

                listViewItem.SubItems.Add(_product.IsDeclining
                                              ? "-"
                                              : (exoInstallment.InterestCoeff.Value * 100).ToString());

                listViewItem.Tag = exoInstallment;
                listViewItem.BackColor = Color.White;
                listViewExoticInstallments.Items.Add(listViewItem);
            }
            labelTotalPrincipal.Text = string.Format("{0} %", (pExoticProduct.SumOfPrincipalCoeff * 100));
            labelTotalInterest.Text = _product.IsDeclining ? " - " : string.Format("{0} %", pExoticProduct.SumOfInterestCoeff * 100);

            textBoxNbOfInstallment.Text = _exoticProduct.GetNumberOfInstallments.ToString();
        }

        private void buttonAddAmountCycle_Click(object sender, EventArgs e)
        {
            int loanCycle;
            if (listViewLoanCycles.Items.Count == 0)
                loanCycle = 0;
            else
                loanCycle = ((Cycle)listViewLoanCycles.Items[listViewLoanCycles.Items.Count - 1].Tag).LoanCycle + 1;

            textBoxCycleMin.TextChanged -= textBoxCycleMin_TextChanged;
            textBoxCycleMax.TextChanged -= textBoxCycleMax_TextChanged;
            textBoxCycleMin.Text = "0";
            textBoxCycleMax.Text = "0";
            textBoxCycleMin.TextChanged += textBoxCycleMin_TextChanged;
            textBoxCycleMax.TextChanged += textBoxCycleMax_TextChanged;
            buttonRemoveAmountCycles.Enabled = true;
            buttonAmountCyclesSave.Enabled = true;

            int cycleObjectId = ((CycleObject)cbxCycleObjects.SelectedItem).Id;

            switch (cycleObjectId)
            {
                case 1:
                    var parameter = new LoanAmountCycle
                    {
                        CycleId = ((LoanCycle)comboBoxLoanCyclesName.SelectedItem).Id,
                        CycleObjectId = cycleObjectId,
                        Min = 0,
                        Max = 0,
                        LoanCycle = loanCycle
                    };
                    var amountCycleItem = new ListViewItem((parameter.LoanCycle + 1).ToString()) { Tag = parameter };
                    amountCycleItem.SubItems.Add(parameter.Min.ToString());
                    amountCycleItem.SubItems.Add(parameter.Max.ToString());
                    listViewLoanCycles.Items.Add(amountCycleItem);
                    _product.LoanAmountCycleParams.Add(parameter);
                    break;
                case 2:
                    var rateCycleParam = new RateCycle
                    {
                        CycleId = ((LoanCycle)comboBoxLoanCyclesName.SelectedItem).Id,
                        CycleObjectId = cycleObjectId,
                        Min = 0,
                        Max = 0,
                        LoanCycle = loanCycle
                    };
                    var interestRateCycleItem = new ListViewItem((rateCycleParam.LoanCycle + 1).ToString()) { Tag = rateCycleParam };
                    interestRateCycleItem.SubItems.Add(rateCycleParam.Min.ToString());
                    interestRateCycleItem.SubItems.Add(rateCycleParam.Max.ToString());
                    listViewLoanCycles.Items.Add(interestRateCycleItem);
                    _product.RateCycleParams.Add(rateCycleParam);
                    break;
                case 3:
                    var maturityCycleParam = new MaturityCycle
                    {
                        CycleId = ((LoanCycle)comboBoxLoanCyclesName.SelectedItem).Id,
                        CycleObjectId = cycleObjectId,
                        Min = 0,
                        Max = 0,
                        LoanCycle = loanCycle
                    };
                    var maturityCycleItem = new ListViewItem((maturityCycleParam.LoanCycle + 1).ToString()) { Tag = maturityCycleParam };
                    maturityCycleItem.SubItems.Add(maturityCycleParam.Min.ToString());
                    maturityCycleItem.SubItems.Add(maturityCycleParam.Max.ToString());
                    listViewLoanCycles.Items.Add(maturityCycleItem);
                    _product.MaturityCycleParams.Add(maturityCycleParam);
                    break;
                default: break;
            }
            listViewLoanCycles.Items[listViewLoanCycles.Items.Count - 1].Selected = true;
        }

        private void textBoxCycleMin_TextChanged(object sender, EventArgs e)
        {
            int cycleObjectId = ((CycleObject)cbxCycleObjects.SelectedItem).Id;
            if (string.IsNullOrEmpty(textBoxCycleMin.Text)) return;
            switch (cycleObjectId)
            {
                case 1:
                    ((LoanAmountCycle)listViewLoanCycles.SelectedItems[0].Tag).Min = decimal.Parse(textBoxCycleMin.Text);
                    listViewLoanCycles.SelectedItems[0].SubItems[1].Text = textBoxCycleMin.Text;
                    break;
                case 2:
                    ((RateCycle)listViewLoanCycles.SelectedItems[0].Tag).Min = decimal.Parse(textBoxCycleMin.Text);
                    listViewLoanCycles.SelectedItems[0].SubItems[1].Text = textBoxCycleMin.Text;
                    break;
                case 3:
                    ((MaturityCycle)listViewLoanCycles.SelectedItems[0].Tag).Min = decimal.Parse(textBoxCycleMin.Text);
                    listViewLoanCycles.SelectedItems[0].SubItems[1].Text = textBoxCycleMin.Text;
                    break;
                default:
                    break;
            }
            buttonAmountCyclesSave.Enabled = true;
            buttonSave.Enabled = true;
        }

        private void textBoxCycleMax_TextChanged(object sender, EventArgs e)
        {
            int cycleObjectId = ((CycleObject)cbxCycleObjects.SelectedItem).Id;
            if (string.IsNullOrEmpty(textBoxCycleMax.Text)) return;
            switch (cycleObjectId)
            {
                case 1:
                    ((LoanAmountCycle)listViewLoanCycles.SelectedItems[0].Tag).Max = decimal.Parse(textBoxCycleMax.Text);
                    listViewLoanCycles.SelectedItems[0].SubItems[2].Text = textBoxCycleMax.Text;
                    break;
                case 2:
                    ((RateCycle)listViewLoanCycles.SelectedItems[0].Tag).Max = decimal.Parse(textBoxCycleMax.Text);
                    listViewLoanCycles.SelectedItems[0].SubItems[2].Text = textBoxCycleMax.Text;
                    break;
                case 3:
                    ((MaturityCycle)listViewLoanCycles.SelectedItems[0].Tag).Max = decimal.Parse(textBoxCycleMax.Text);
                    listViewLoanCycles.SelectedItems[0].SubItems[2].Text = textBoxCycleMax.Text;
                    break;
                default:
                    break;
            }
            buttonAmountCyclesSave.Enabled = true;
            buttonSave.Enabled = true;
        }

        private void textBoxCycleMin_Leave(object sender, EventArgs e)
        {
            if (textBoxCycleMin.BackColor == Color.Red)
                textBoxCycleMin.Focus();
        }

        private void textBoxCycleMax_Leave(object sender, EventArgs e)
        {
            if (textBoxCycleMax.BackColor == Color.Red)
                textBoxCycleMax.Focus();
        }

        private void buttonRemoveAmountCycles_Click(object sender, EventArgs e)
        {
            if (listViewLoanCycles.Items.Count == 0) return;
            int cycleObjectId = ((CycleObject)cbxCycleObjects.SelectedItem).Id;
            switch (cycleObjectId)
            {
                case 1:
                    listViewLoanCycles.Items.RemoveAt(listViewLoanCycles.Items.Count - 1);
                    _product.LoanAmountCycleParams.RemoveAt(_product.LoanAmountCycleParams.Count - 1);
                    break;
                case 2:
                    listViewLoanCycles.Items.RemoveAt(listViewLoanCycles.Items.Count - 1);
                    _product.RateCycleParams.RemoveAt(_product.RateCycleParams.Count - 1);
                    break;
                case 3:
                    listViewLoanCycles.Items.RemoveAt(listViewLoanCycles.Items.Count - 1);
                    _product.MaturityCycleParams.RemoveAt(_product.MaturityCycleParams.Count - 1);
                    break;
                default: break;
            }
            if (listViewLoanCycles.Items.Count != 0)
                listViewLoanCycles.Items[listViewLoanCycles.Items.Count - 1].Selected = true;
            buttonAmountCyclesSave.Enabled = true;
            listViewLoanCycles.Focus();
        }

        private void buttonRemoveExoticInstallment_Click(object sender, EventArgs e)
        {
            ExoticInstallment installment = (ExoticInstallment)listViewExoticInstallments.SelectedItems[0].Tag;
            _exoticProduct.Remove(installment);
            _InitializeListViewExoticInstallments(_exoticProduct);

            groupBoxExoticInstallmentProperties.Text = MultiLanguageStrings.GetString(Ressource.FrmAddLoanProduct,
                "messageExoticInstallment.Text");

            buttonDecreaseExoticInstallment.Enabled = false;
            if (_exoticProduct.GetNumberOfInstallments == 0)
            {
                panelExoticInstallment.Visible = false;
                buttonSaveExoticProduct.Enabled = false;
                buttonRemoveExoticInstallment.Enabled = false;
            }
            else
            {
                panelExoticProductNavigationButtons.Enabled = _exoticProduct.GetNumberOfInstallments > 1;
                listViewExoticInstallments.Items[_exoticProduct.GetNumberOfInstallments - 1].EnsureVisible();
                listViewExoticInstallments.Items[_exoticProduct.GetNumberOfInstallments - 1].Selected = true;
                listViewExoticInstallments.Focus();
            }
        }

        private void buttonAmountCyclesSave_Click(object sender, EventArgs e)
        {
            try
            {
                ServicesProvider.GetInstance().GetProductServices().SaveAllCycleParams(
               _product.LoanAmountCycleParams
               , _product.RateCycleParams
               , _product.MaturityCycleParams);
                string msg = MultiLanguageStrings.GetString(Ressource.PackagesForm, "CyclesSaved.Text");
                MessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OctopusPackageSaveException exception)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(exception)).ShowDialog();
            }
        }

        private void buttonSaveExoticProduct_Click(object sender, EventArgs e)
        {
            try
            {
                InputBox inPutBox = new InputBox();
                inPutBox.ShowDialog();

                if (inPutBox.Result != null)
                {
                    _exoticProduct.Name = inPutBox.Result;
                    _exoticProduct.Id =
                        ServicesProvider.GetInstance().GetProductServices().AddExoticProduct(_exoticProduct,
                                                                                             _product.LoanType);
                    panelExoticProductNavigationButtons.Enabled = false;
                    listViewExoticInstallments.Enabled = false;
                    buttonAddExoticInstallment.Enabled = false;
                    buttonRemoveExoticInstallment.Enabled = false;
                    buttonSaveExoticProduct.Enabled = false;
                    InitializeComboBoxExoticProduct();
                    comboBoxExoticProduct.Text = _exoticProduct.Name;

                    _product.ExoticProduct = _exoticProduct;
                    groupBoxExoticInstallmentProperties.Text = MultiLanguageStrings.GetString(Ressource.FrmAddLoanProduct,
                                                                                              "messageExoticInstallment.Text");
                    panelExoticInstallment.Visible = false;
                }
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void comboBoxExoticProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            _exoticProduct = (ExoticInstallmentsTable)comboBoxExoticProduct.SelectedItem;
            _product.ExoticProduct = _exoticProduct;
            _InitializeListViewExoticInstallments(_exoticProduct);
            groupBoxExoticProducts.Size = new Size(754, 298);
            panelExoticProduct.Enabled = true;
            panelExoticProduct.Visible = true;
            panelExoticInstallment.Visible = true;
            panelExoticInstallment.Enabled = false;
            buttonAddExoticInstallment.Enabled = false;
            buttonRemoveExoticInstallment.Enabled = false;
            buttonSaveExoticProduct.Enabled = false;
            panelExoticProductNavigationButtons.Enabled = false;
            _useExistingExoticProduct = true;
            panelExoticInstallment.Visible = false;
            groupBoxExoticInstallmentProperties.Text = MultiLanguageStrings.GetString(Ressource.FrmAddLoanProduct, "messageExoticInstallment.Text");
            buttonSave.Enabled = true;
        }

        private void listViewExoticInstallments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewExoticInstallments.SelectedItems.Count != 0)
                groupBoxExoticInstallmentProperties.Text = string.Format(MultiLanguageStrings.GetString(Ressource.FrmAddLoanProduct, "exoticInstallmentProperties.Text"), ((ExoticInstallment)listViewExoticInstallments.SelectedItems[0].Tag).Number);
        }

        private void buttonIncreaseExoticInstallment_Click(object sender, EventArgs e)
        {
            int i = listViewExoticInstallments.SelectedIndices[0];

            ExoticInstallment installment = (ExoticInstallment)listViewExoticInstallments.SelectedItems[0].Tag;
            _exoticProduct.Move(installment, installment.Number - 1);

            _InitializeListViewExoticInstallments(_exoticProduct);
            listViewExoticInstallments.Items[i - 1].EnsureVisible();
            listViewExoticInstallments.Items[i - 1].Selected = true;
            listViewExoticInstallments.Focus();

            buttonIncreaseExoticInstallment.Enabled = installment.Number != 1;
            buttonDecreaseExoticInstallment.Enabled = installment.Number != _exoticProduct.GetNumberOfInstallments;
        }

        private void buttonDecreaseExoticInstallment_Click(object sender, EventArgs e)
        {
            int i = listViewExoticInstallments.SelectedIndices[0];

            ExoticInstallment installment = (ExoticInstallment)listViewExoticInstallments.SelectedItems[0].Tag;
            _exoticProduct.Move(installment, installment.Number + 1);

            _InitializeListViewExoticInstallments(_exoticProduct);
            listViewExoticInstallments.Items[i + 1].EnsureVisible();
            listViewExoticInstallments.Items[i + 1].Selected = true;
            listViewExoticInstallments.Focus();

            buttonIncreaseExoticInstallment.Enabled = installment.Number != 1;
            buttonDecreaseExoticInstallment.Enabled = installment.Number != _exoticProduct.GetNumberOfInstallments;
        }

        private void listViewExoticInstallments_Click(object sender, EventArgs e)
        {
            int i = listViewExoticInstallments.SelectedIndices[0];
            _exoticInstallment = (ExoticInstallment)listViewExoticInstallments.SelectedItems[0].Tag;
            textBoxExoticInstallmentPrincipal.Text = (_exoticInstallment.PrincipalCoeff * 100).ToString();

            if (_product.IsDeclining)
            {
                textBoxExoticInstallmentInterest.Clear();
                textBoxExoticInstallmentInterest.Enabled = false;
            }
            else
            {
                textBoxExoticInstallmentInterest.Enabled = true;
                textBoxExoticInstallmentInterest.Text = (_exoticInstallment.InterestCoeff * 100).ToString();
            }
            panelExoticInstallment.Visible = true;

            if (!_useExistingExoticProduct)
            {
                panelExoticProductNavigationButtons.Enabled = _exoticProduct.GetNumberOfInstallments > 1;
                buttonIncreaseExoticInstallment.Enabled = _exoticInstallment.Number != 1;
                buttonDecreaseExoticInstallment.Enabled = _exoticInstallment.Number != _exoticProduct.GetNumberOfInstallments;
            }

            listViewExoticInstallments.Items[i].EnsureVisible();
            listViewExoticInstallments.Items[i].Selected = true;
            listViewExoticInstallments.Focus();
        }

        private void textBoxExoticInstallmentPrincipal_Leave(object sender, EventArgs e)
        {
            if (textBoxExoticInstallmentPrincipal.BackColor == Color.Red)
                textBoxExoticInstallmentPrincipal.Focus();
        }

        private void textBoxExoticInstallmentInterest_Leave(object sender, EventArgs e)
        {
            if (textBoxExoticInstallmentInterest.BackColor == Color.Red)
                textBoxExoticInstallmentInterest.Focus();
        }

        private void textBoxExoticInstallmentPrincipal_TextChanged(object sender, EventArgs e)
        {
            panelExoticProductNavigationButtons.Enabled = false;

            double? principal =
                ServicesHelper.ConvertStringToNullableDouble(textBoxExoticInstallmentPrincipal.Text, false) / 100;

            if (principal.HasValue)
            {
                textBoxExoticInstallmentPrincipal.BackColor = Color.White;
                _exoticInstallment.PrincipalCoeff = principal.Value;
                _InitializeListViewExoticInstallments(_exoticProduct);
            }
            else
            {
                textBoxExoticInstallmentPrincipal.BackColor = Color.Red;
                textBoxExoticInstallmentPrincipal.Focus();
            }
        }

        private void textBoxExoticInstallmentInterest_TextChanged(object sender, EventArgs e)
        {
            if (!_product.IsDeclining)
            {
                panelExoticProductNavigationButtons.Enabled = false;
                double? interest =
                    ServicesHelper.ConvertStringToNullableDouble(textBoxExoticInstallmentInterest.Text, false) / 100;

                if (interest.HasValue)
                {
                    textBoxExoticInstallmentInterest.BackColor = Color.White;
                    _exoticInstallment.InterestCoeff = interest.Value;
                    _InitializeListViewExoticInstallments(_exoticProduct);
                }
                else
                {
                    textBoxExoticInstallmentInterest.BackColor = Color.Red;
                    textBoxExoticInstallmentInterest.Focus();
                }
            }
            else
            {
                textBoxExoticInstallmentInterest.BackColor = Color.White;
                _exoticInstallment.InterestCoeff = null;
                _InitializeListViewExoticInstallments(_exoticProduct);
            }
        }

        private void buttonAddExoticInstallment_Click(object sender, EventArgs e)
        {
            textBoxExoticInstallmentInterest.Clear();
            textBoxExoticInstallmentPrincipal.Clear();
            _exoticInstallment = new ExoticInstallment();
            if (_product.IsDeclining)
            {
                textBoxExoticInstallmentInterest.Enabled = false;
                _exoticInstallment.InterestCoeff = null;
            }
            else
            {
                textBoxExoticInstallmentInterest.Enabled = true;
                textBoxExoticInstallmentInterest.Text = "0";
            }

            textBoxExoticInstallmentPrincipal.Text = "0";
            panelExoticInstallment.Visible = true;
            buttonRemoveExoticInstallment.Enabled = true;

            buttonSaveExoticProduct.Enabled = true;
            buttonDecreaseExoticInstallment.Enabled = false;
            _exoticProduct.Add(_exoticInstallment);
            _InitializeListViewExoticInstallments(_exoticProduct);
            panelExoticProductNavigationButtons.Enabled = _exoticProduct.GetNumberOfInstallments > 1;
            buttonDecreaseExoticInstallment.Enabled = false;

            listViewExoticInstallments.Items[_exoticProduct.GetNumberOfInstallments - 1].EnsureVisible();
            listViewExoticInstallments.Items[_exoticProduct.GetNumberOfInstallments - 1].Selected = true;
            listViewExoticInstallments.Focus();
        }

        private void buttonNewExoticProduct_Click(object sender, EventArgs e)
        {
            listViewExoticInstallments.Items.Clear();
            panelExoticInstallment.Visible = false;
            panelExoticInstallment.Enabled = true;
            buttonRemoveExoticInstallment.Enabled = false;
            buttonSaveExoticProduct.Enabled = false;
            listViewExoticInstallments.Enabled = true;
            buttonAddExoticInstallment.Enabled = true;
            panelExoticProductNavigationButtons.Enabled = false;
            panelExoticProduct.Enabled = true;
            panelExoticProduct.Visible = true;
            _product.ExoticProduct = null;
            _useExistingExoticProduct = false;
            _exoticProduct = new ExoticInstallmentsTable();
            comboBoxExoticProduct.Text = MultiLanguageStrings.GetString(Ressource.FrmAddLoanProduct, "messageSelectExotic.Text");
            groupBoxExoticInstallmentProperties.Text = MultiLanguageStrings.GetString(Ressource.FrmAddLoanProduct, "messageExoticInstallment.Text");
            groupBoxExoticProducts.Size = new Size(754, 298);
        }

        private void _CheckInterestRateType()
        {
            checkBoxUseExceptionalInstallmen.Checked = false;

            if (rbDecliningFixedInstallments.Checked)
            {
                _product.LoanType = OLoanTypes.DecliningFixedInstallments;
                checkBoxUseExceptionalInstallmen.Enabled = false;
                checkBoxUseExceptionalInstallmen.Checked = false;
            }
            else if (rbDecliningFixedPrincipal.Checked)
            {
                _product.LoanType = OLoanTypes.DecliningFixedPrincipal;
                checkBoxUseExceptionalInstallmen.Enabled = true;
            }
            else if (rbFlat.Checked)
            {
                _product.LoanType = OLoanTypes.Flat;
                checkBoxUseExceptionalInstallmen.Enabled = true;
            }
            else if (rbDecliningFixedPrincipalRelaInterest.Checked)
            {
                _product.LoanType = OLoanTypes.DecliningFixedPrincipalWithRealInterest;
                checkBoxUseExceptionalInstallmen.Enabled = true;
            }

            InitializeComboBoxExoticProduct();
            comboBoxExoticProduct.Text = MultiLanguageStrings.GetString(Ressource.FrmAddLoanProduct, "messageSelectExotic.Text");
        }

        private void checkBoxUseExceptionalInstallmen_CheckedChanged(object sender, EventArgs e)
        {
            _CheckExoticProduct();
            buttonSave.Enabled = true;

            if (checkBoxUseExceptionalInstallmen.Checked)
            {
                groupBoxGracePeriod.Enabled = false;
                textBoxGracePeriodMin.Text = "";
                textBoxGracePeriodMax.Text = "";
                textBoxGracePeriod.Text = "0";
            }
            else
                groupBoxGracePeriod.Enabled = true;

        }

        private void _CheckExoticProduct()
        {
            if (checkBoxUseExceptionalInstallmen.Checked)
            {
                groupBoxExoticProducts.Enabled = true;
                groupBoxExoticProducts.Visible = true;
                groupBoxExoticProducts.Size = new Size(754, 298);
                groupBoxNumberOfInstallments.Enabled = false;
                _useExoticProduct = true;
            }
            else
            {
                _useExoticProduct = false;
                groupBoxExoticProducts.Enabled = false;
                groupBoxExoticProducts.Visible = false;
                listViewExoticInstallments.Items.Clear();
                panelExoticInstallment.Visible = false;
                groupBoxNumberOfInstallments.Enabled = true;

                _exoticProduct = null;
                _product.ExoticProduct = null;
                comboBoxLoanCyclesName.Text = MultiLanguageStrings.GetString(Ressource.FrmAddLoanProduct, "messageSelectExotic.Text");
            }
            textBoxNbOfInstallment.Clear();
            textBoxNbOfInstallmentMax.Clear();
            textBoxNbOfInstallmentMin.Clear();
        }

        private void tBInitialAmountMin_TextChanged(object sender, EventArgs e)
        {
            _product.NonRepaymentPenaltiesMin.InitialAmount = ServicesHelper.ConvertStringToNullableDouble(tBInitialAmountMin.Text, true);
            buttonSave.Enabled = true;
            _ischangeFee = true;
        }

        private void tBInitialAmountMax_TextChanged(object sender, EventArgs e)
        {
            _product.NonRepaymentPenaltiesMax.InitialAmount = ServicesHelper.ConvertStringToNullableDouble(tBInitialAmountMax.Text, true);
            buttonSave.Enabled = true;
            _ischangeFee = true;
        }

        private void tBInitialAmountValue_TextChanged(object sender, EventArgs e)
        {
            _product.NonRepaymentPenalties.InitialAmount = ServicesHelper.ConvertStringToNullableDouble(tBInitialAmountValue.Text, true);
            buttonSave.Enabled = true;
            _ischangeFee = true;
        }

        private void tBOLBMin_TextChanged(object sender, EventArgs e)
        {
            _product.NonRepaymentPenaltiesMin.OLB = ServicesHelper.ConvertStringToNullableDouble(tBOLBMin.Text, true);
            buttonSave.Enabled = true;
            _ischangeFee = true;
        }

        private void tBOLBMax_TextChanged(object sender, EventArgs e)
        {
            _product.NonRepaymentPenaltiesMax.OLB = ServicesHelper.ConvertStringToNullableDouble(tBOLBMax.Text, true);
            buttonSave.Enabled = true;
            _ischangeFee = true;
        }

        private void tBOLBValue_TextChanged(object sender, EventArgs e)
        {
            _product.NonRepaymentPenalties.OLB = ServicesHelper.ConvertStringToNullableDouble(tBOLBValue.Text, true);
            buttonSave.Enabled = true;
            _ischangeFee = true;
        }

        private void tBOverduePrincipalMin_TextChanged(object sender, EventArgs e)
        {
            _product.NonRepaymentPenaltiesMin.OverDuePrincipal = ServicesHelper.ConvertStringToNullableDouble(tBOverduePrincipalMin.Text, true);
            buttonSave.Enabled = true;
            _ischangeFee = true;
        }

        private void tBOverduePrincipalMax_TextChanged(object sender, EventArgs e)
        {
            _product.NonRepaymentPenaltiesMax.OverDuePrincipal = ServicesHelper.ConvertStringToNullableDouble(tBOverduePrincipalMax.Text, true);
            buttonSave.Enabled = true;
            _ischangeFee = true;
        }

        private void tBOverduePrincipalValue_TextChanged(object sender, EventArgs e)
        {
            _product.NonRepaymentPenalties.OverDuePrincipal = ServicesHelper.ConvertStringToNullableDouble(tBOverduePrincipalValue.Text, true);
            buttonSave.Enabled = true;
            _ischangeFee = true;
        }

        private void tBOverdueInterestMin_TextChanged(object sender, EventArgs e)
        {
            _product.NonRepaymentPenaltiesMin.OverDueInterest = ServicesHelper.ConvertStringToNullableDouble(tBOverdueInterestMin.Text, true);
            buttonSave.Enabled = true;
        }

        private void tBOverdueInterestMax_TextChanged(object sender, EventArgs e)
        {
            _product.NonRepaymentPenaltiesMax.OverDueInterest = ServicesHelper.ConvertStringToNullableDouble(tBOverdueInterestMax.Text, true);
            buttonSave.Enabled = true;
        }

        private void tBOverdueInterestValue_TextChanged(object sender, EventArgs e)
        {
            _product.NonRepaymentPenalties.OverDueInterest = ServicesHelper.ConvertStringToNullableDouble(tBOverdueInterestValue.Text, true);
            buttonSave.Enabled = true;
        }

        private void comboBoxFundingLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            FundingLine _selected = (FundingLine)comboBoxFundingLine.SelectedItem;
            if (_selected.Id != 0)
            {
                _product.FundingLine = (FundingLine)comboBoxFundingLine.SelectedItem;

                if (_product.FundingLine.Currency != null)
                {
                    _product.Currency = _product.FundingLine.Currency;
                    InitializeLabelCurrency(_product.Currency.Name);
                    comboBoxCurrencies.Items.Clear();
                    comboBoxCurrencies.Items.Add(_product.Currency);
                    comboBoxCurrencies.SelectedItem = _product.Currency;
                }
            }
            else
            {
                InitializeComboBoxCurrencies();
                _product.Currency = null;
            }

            buttonSave.Enabled = true;
        }

        private void AddPackageForm_Load(object sender, EventArgs e)
        {
            buttonSave.Enabled = false;
        }

        private void textBoxCode_TextChanged(object sender, EventArgs e)
        {
            _product.Code = textBoxCode.Text;
            buttonSave.Enabled = true;
        }

        private void comboBoxCurrencies_SelectedIndexChanged(object sender, EventArgs e)
        {
            _product.Currency = (Currency)comboBoxCurrencies.SelectedItem;
            InitializeLabelCurrency(_product.Currency.Code);
        }

        private void comboBoxRoundingType_SelectedValueChanged(object sender, EventArgs e)
        {
            switch (comboBoxRoundingType.SelectedIndex + 1)
            {
                case 1:
                    _product.RoundingType = ORoundingType.Approximate; break;
                case 2:
                    _product.RoundingType = ORoundingType.Begin; break;
                case 3:
                    _product.RoundingType = ORoundingType.End; break;
            }

            buttonSave.Enabled = true;
        }

        private void textBoxGracePeriodLateFee_TextChanged(object sender, EventArgs e)
        {
            _product.GracePeriodOfLateFees = ServicesHelper.ConvertStringToNullableInt32(textBoxGracePeriodLateFee.Text);
            buttonSave.Enabled = true;
            _ischangeFee = true;
        }

        private void useLOCCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            drawNumGroupBox.Enabled = useLOCCheckBox.Checked;
            maxLOCAmountGroupBox.Enabled = useLOCCheckBox.Checked;
            maxLOCMaturityGroupBox.Enabled = useLOCCheckBox.Checked;
            _product.ActivatedLOC = useLOCCheckBox.Checked;
            buttonSave.Enabled = true;
        }

        private void textBoxBoxAnticipatedPartialRepaimentMax_TextChanged(object sender, EventArgs e)
        {
            _product.AnticipatedPartialRepaymentPenaltiesMax =
                   ServicesHelper.ConvertStringToNullableDouble(textBoxAnticipatedPartialRepaimentMax.Text, true);
            buttonSave.Enabled = true;
            _ischangeFee = true;
        }

        private void textBoxAmountUnderLOCMin_TextChanged(object sender, EventArgs e)
        {
            _product.AmountUnderLocMin = ServicesHelper.ConvertStringToNullableDecimal(textBoxAmountUnderLOCMin.Text);
            buttonSave.Enabled = true;
        }

        private void textBoxAmountUnderLOCMax_TextChanged(object sender, EventArgs e)
        {
            _product.AmountUnderLocMax = ServicesHelper.ConvertStringToNullableDecimal(textBoxAmountUnderLOCMax.Text);
            buttonSave.Enabled = true;
        }

        private void textBoxAmountUnderLOC_TextChanged(object sender, EventArgs e)
        {
            _product.AmountUnderLoc = ServicesHelper.ConvertStringToNullableDecimal(textBoxAmountUnderLOC.Text);
            buttonSave.Enabled = true;
        }

        private void textBoxLOCMaturityMin_TextChanged(object sender, EventArgs e)
        {
            _product.MaturityLocMin = ServicesHelper.ConvertStringToNullableInt32(textBoxLOCMaturityMin.Text);
            buttonSave.Enabled = true;
        }

        private void textBoxLOCMaturityMax_TextChanged(object sender, EventArgs e)
        {
            _product.MaturityLocMax = ServicesHelper.ConvertStringToNullableInt32(textBoxLOCMaturityMax.Text);
            buttonSave.Enabled = true;
        }

        private void textBoxLOCMaturity_TextChanged(object sender, EventArgs e)
        {
            _product.MaturityLoc = ServicesHelper.ConvertStringToNullableInt32(textBoxLOCMaturity.Text);
            buttonSave.Enabled = true;
        }

        private void textBoxAnticipatedPartialRepaiment_TextChanged(object sender, EventArgs e)
        {
            _product.AnticipatedPartialRepaymentPenalties =
                    ServicesHelper.ConvertStringToNullableDouble(textBoxAnticipatedPartialRepaiment.Text, true);
            buttonSave.Enabled = true;
            _ischangeFee = true;
        }

        private void textBoxAnticipatedPartialRepaimentMin_TextChanged(object sender, EventArgs e)
        {
            _product.AnticipatedPartialRepaymentPenaltiesMin =
                    ServicesHelper.ConvertStringToNullableDouble(textBoxAnticipatedPartialRepaimentMin.Text, true);
            buttonSave.Enabled = true;
            _ischangeFee = true;
        }

        private void radioButtonPartialRemainingOLB_CheckedChanged(object sender, EventArgs e)
        {
            CheckPartialAnticipatedRepaymentBase();
            buttonSave.Enabled = true;
            _ischangeFee = true;
        }

        private void radioButtonPartialRemainingInterest_TextChanged(object sender, EventArgs e)
        {
            CheckPartialAnticipatedRepaymentBase();
            buttonSave.Enabled = true;
            _ischangeFee = true;
        }

        private void textBoxNumOfDrawings_TextChanged(object sender, EventArgs e)
        {
            //int? t = ServicesHelper.ConvertStringToNullableInt32(textBoxNumOfDrawings.Text);
            //if (t.HasValue && t.Value > 0 && t.Value < 25)
            _product.DrawingsNumber = ServicesHelper.ConvertStringToNullableInt32(textBoxNumOfDrawings.Text);
            //else return;

            buttonSave.Enabled = true;
        }

        private void checkBoxFlexPackage_CheckedChanged(object sender, EventArgs e)
        {
            _product.AllowFlexibleSchedule = checkBoxFlexPackage.Checked;
            buttonSave.Enabled = true;
        }

        private void checkBoxGuarCollRequired_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxGuarCollRequired.Checked)
            {
                groupBoxTotGuarColl.Enabled = true;
                groupBoxSepGuarColl.Enabled = false;
                checkBoxSetSepCollGuar.Enabled = true;
                checkBoxSetSepCollGuar.Checked = false;
            }
            else
            {
                groupBoxTotGuarColl.Enabled = false;
                groupBoxSepGuarColl.Enabled = false;
                checkBoxSetSepCollGuar.Enabled = false;
                checkBoxSetSepCollGuar.Checked = false;
            }

            _product.UseGuarantorCollateral = checkBoxGuarCollRequired.Checked;
            buttonSave.Enabled = true;
        }

        private void checkBoxSetSepCollGuar_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSetSepCollGuar.Checked)
            {
                groupBoxTotGuarColl.Enabled = false;
                groupBoxSepGuarColl.Enabled = true;

                _product.PercentageTotalGuarantorCollateral = 0;
                textBoxCollGuar.Text = "0";
            }
            else
            {
                groupBoxTotGuarColl.Enabled = checkBoxGuarCollRequired.Checked;
                groupBoxSepGuarColl.Enabled = false;

                _product.PercentageSeparateGuarantour = 0;
                textBoxGuar.Text = "0";

                _product.PercentageSeparateCollateral = 0;
                textBoxColl.Text = "0";
            }

            _product.SetSeparateGuarantorCollateral = checkBoxSetSepCollGuar.Checked;
            buttonSave.Enabled = true;
        }

        private void textBoxNumOfDrawings_KeyPress(object sender, KeyPressEventArgs e)
        {
            int keyCode = e.KeyChar;

            if ((keyCode >= 48 && keyCode <= 57) || (keyCode == 8) || (Char.IsControl(e.KeyChar) && e.KeyChar
                != ((char)Keys.V | (char)Keys.ControlKey)) || (Char.IsControl(e.KeyChar) && e.KeyChar !=
                ((char)Keys.C | (char)Keys.ControlKey)) || (e.KeyChar.ToString() == NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void textBoxCollGuar_TextChanged(object sender, EventArgs e)
        {
            _product.PercentageTotalGuarantorCollateral = (int)ServicesHelper.ConvertStringToDecimal(textBoxCollGuar.Text, false);
            buttonSave.Enabled = true;
        }

        private void textBoxGuar_TextChanged(object sender, EventArgs e)
        {
            _product.PercentageSeparateGuarantour = (int)ServicesHelper.ConvertStringToDecimal(textBoxGuar.Text, false);
            buttonSave.Enabled = true;
        }

        private void textBoxColl_TextChanged(object sender, EventArgs e)
        {
            _product.PercentageSeparateCollateral = (int)ServicesHelper.ConvertStringToDecimal(textBoxColl.Text, false);
            buttonSave.Enabled = true;
        }

        private void clientTypeAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (clientTypeAllCheckBox.Checked)
            {
                clientTypeGroupCheckBox.Checked = true;
                clientTypeIndivCheckBox.Checked = true;
                clientTypeCorpCheckBox.Checked = true;
                clientTypeVillageCheckBox.Checked = true;
            }
            AssignClientTypeToProduct(sender);
        }

        private void AssignClientTypeToProduct(object sender)
        {
            CheckBox receivedCheckBox = (CheckBox)sender;
            string clientTypeName;
            switch (receivedCheckBox.Name)
            {
                case "clientTypeAllCheckBox":
                    clientTypeName = "All";
                    break;
                case "clientTypeGroupCheckBox":
                    clientTypeName = "Group";
                    break;
                case "clientTypeIndivCheckBox":
                    clientTypeName = "Individual";
                    break;
                case "clientTypeCorpCheckBox":
                    clientTypeName = "Corporate";
                    break;
                case "clientTypeVillageCheckBox":
                    clientTypeName = "Village";
                    break;
                default:
                    clientTypeName = null;
                    break;
            }

            for (int i = 0; i < _product.ProductClientTypes.Count; i++)
            {
                if (_product.ProductClientTypes[i].TypeName == clientTypeName)
                {
                    _product.ProductClientTypes[i].IsChecked = receivedCheckBox.Checked;
                    break;
                }
            }
        }

        private void clientTypeGroupCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox recievedCheckBox = (CheckBox)sender;
            if (recievedCheckBox.Checked)
            {
                AssignClientTypeToProduct(sender);
                _checkBoxCounter++;
                if (_checkBoxCounter == 4)
                    clientTypeAllCheckBox.Checked = true;
            }
            else
            {
                recievedCheckBox.Checked = false;
                AssignClientTypeToProduct(sender);
                _checkBoxCounter--;
            }

        }

        private void clientTypeAllCheckBox_Click(object sender, EventArgs e)
        {
            if (!clientTypeAllCheckBox.Checked)
            {
                clientTypeGroupCheckBox.Checked = false;
                clientTypeIndivCheckBox.Checked = false;
                clientTypeCorpCheckBox.Checked = false;
                clientTypeVillageCheckBox.Checked = false;
            }
            buttonSave.Enabled = true;
        }

        private void clientTypeGroupCheckBox_Click(object sender, EventArgs e)
        {
            buttonSave.Enabled = true;
        }

        private void textBoxCompulsoryAmountMin_TextChanged(object sender, EventArgs e)
        {
            double? tempCompulsoryAmount = ServicesHelper.ConvertStringToNullableDouble(txbCompulsoryAmountMin.Text, false);
            if(tempCompulsoryAmount != null)
            {
                if (tempCompulsoryAmount - Math.Floor(Convert.ToDouble(tempCompulsoryAmount)) != 0)
                {
                    Fail("OnlyIntegerIsAccepted.Text");
                    buttonSave.Enabled = false;
                    return;
                }
            }
            _product.CompulsoryAmountMin = ServicesHelper.ConvertStringToNullableInt32(txbCompulsoryAmountMin.Text);
            buttonSave.Enabled = true;
        }

        private void textBoxCompulsoryAmountMax_TextChanged(object sender, EventArgs e)
        {
            double? tempCompulsoryAmount = ServicesHelper.ConvertStringToNullableDouble(txbCompulsoryAmountMax.Text, false);
            if (tempCompulsoryAmount != null)
            {
                if (tempCompulsoryAmount - Math.Floor(Convert.ToDouble(tempCompulsoryAmount)) != 0)
                {
                    Fail("OnlyIntegerIsAccepted.Text");
                    buttonSave.Enabled = false;
                    return;
                }
            }
            _product.CompulsoryAmountMax = ServicesHelper.ConvertStringToNullableInt32(txbCompulsoryAmountMax.Text);
            buttonSave.Enabled = true;
        }

        private void textBoxCompulsoryAmountValue_TextChanged(object sender, EventArgs e)
        {
            double? tempCompulsoryAmount = ServicesHelper.ConvertStringToNullableDouble(txbCompulsoryAmountValue.Text, false);
            if (tempCompulsoryAmount != null)
            {
                if (tempCompulsoryAmount - Math.Floor(Convert.ToDouble(tempCompulsoryAmount)) != 0)
                {
                    Fail("OnlyIntegerIsAccepted.Text");
                    buttonSave.Enabled = false;
                    return;
                }
            }
            _product.CompulsoryAmount = ServicesHelper.ConvertStringToNullableInt32(txbCompulsoryAmountValue.Text);
            buttonSave.Enabled = true;
        }

        private void checkBoxUseCompulsorySavings_CheckedChanged(object sender, EventArgs e)
        {
            gbCSAmount.Enabled = cbUseCompulsorySavings.Checked;
            _product.UseCompulsorySavings = cbUseCompulsorySavings.Checked;
            buttonSave.Enabled = true;
        }

        private void DeleteEntryFeeFromLists(EntryFee entryFee)
        {
            if (entryFee.IsAdded)
            {
                foreach (EntryFee fee in _product.AddedEntryFees)
                {
                    if (fee.IdForNewItem != entryFee.IdForNewItem) continue;
                    _product.AddedEntryFees.Remove(fee);
                    break;
                }
                foreach (EntryFee fee in _product.EntryFees)
                {
                    if (fee.IdForNewItem != entryFee.IdForNewItem) continue;
                    _product.EntryFees.Remove(fee);
                    break;
                }
            }
            else
            {
                foreach (EntryFee fee in _product.EntryFees)
                {
                    if (fee.Id != entryFee.Id) continue;
                    _product.DeletedEntryFees.Add(fee);
                    _product.EntryFees.Remove(fee);
                    break;
                }
            }
        }

        private void cbUseLoanCycle_CheckedChanged(object sender, EventArgs e)
        {
            if (cbUseLoanCycle.Checked)
            {
                groupBoxAmount.Visible = false;
                groupBoxInterestRate.Visible = false;
                groupBoxNumberOfInstallments.Visible = false;
                groupBoxAmountCycles.Visible = true;
                if (comboBoxLoanCyclesName.SelectedItem != null)
                {
                    _product.CycleId = ((LoanCycle)comboBoxLoanCyclesName.SelectedItem).Id;
                    comboBoxLoanCyclesName_SelectedIndexChanged(sender, e);
                }
            }
            else
            {
                groupBoxAmount.Visible = true;
                groupBoxInterestRate.Visible = true;
                groupBoxNumberOfInstallments.Visible = true;
                groupBoxAmountCycles.Visible = false;
                panelAmountCycles.Visible = false;
                _product.CycleId = null;
            }
            buttonSave.Enabled = true;
        }

        public void FillListViewLoanCycle(List<LoanAmountCycle> cycleParameters)
        {
            listViewLoanCycles.Items.Clear();
            if (cycleParameters != null)
                foreach (LoanAmountCycle parameter in cycleParameters)
                {
                    ListViewItem item = new ListViewItem((parameter.LoanCycle + 1).ToString());
                    item.Tag = parameter;
                    item.SubItems.Add(parameter.Min.Value.ToString("0"));
                    item.SubItems.Add(parameter.Max.Value.ToString("0"));
                    listViewLoanCycles.Items.Add(item);
                }
        }

        public void FillListViewLoanCycle(List<RateCycle> interestRateCycles)
        {
            listViewLoanCycles.Items.Clear();
            if (interestRateCycles != null)
                foreach (RateCycle parameter in interestRateCycles)
                {
                    ListViewItem item = new ListViewItem((parameter.LoanCycle + 1).ToString());
                    item.Tag = parameter;
                    item.SubItems.Add((parameter.Min.Value).ToString("0.00"));
                    item.SubItems.Add((parameter.Max.Value).ToString("0.00"));
                    listViewLoanCycles.Items.Add(item);
                }
        }

        public void FillListViewLoanCycle(List<MaturityCycle> numberOfInstallmentsCycles)
        {
            listViewLoanCycles.Items.Clear();
            if (numberOfInstallmentsCycles != null)
                foreach (MaturityCycle parameter in numberOfInstallmentsCycles)
                {
                    ListViewItem item = new ListViewItem((parameter.LoanCycle + 1).ToString());
                    item.Tag = parameter;
                    item.SubItems.Add(parameter.Min.Value.ToString("0"));
                    item.SubItems.Add(parameter.Max.Value.ToString("0"));
                    listViewLoanCycles.Items.Add(item);
                }
        }

        private void comboBoxCycleParams_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxCycleMin.Enabled = false;
            textBoxCycleMax.Enabled = false;
            textBoxCycleMin.TextChanged -= textBoxCycleMin_TextChanged;
            textBoxCycleMax.TextChanged -= textBoxCycleMax_TextChanged;
            textBoxCycleMin.Text = "";
            textBoxCycleMax.Text = "";
            if (cbxCycleObjects.SelectedItem == null) return;
            switch (((CycleObject)cbxCycleObjects.SelectedItem).Id)
            {
                case 1:
                    FillListViewLoanCycle(_product.LoanAmountCycleParams);
                    break;
                case 2:
                    FillListViewLoanCycle(_product.RateCycleParams);
                    break;
                case 3:
                    FillListViewLoanCycle(_product.MaturityCycleParams);
                    break;
            }
            textBoxCycleMin.TextChanged += textBoxCycleMin_TextChanged;
            textBoxCycleMax.TextChanged += textBoxCycleMax_TextChanged;
        }

        private void listViewLoanCycles_Click(object sender, EventArgs e)
        {
            try
            {
                buttonRemoveAmountCycles.Enabled = false;
                if (cbxCycleObjects.SelectedItem == null) return;
                buttonRemoveAmountCycles.Enabled = true;
                textBoxCycleMin.TextChanged -= textBoxCycleMin_TextChanged;
                textBoxCycleMax.TextChanged -= textBoxCycleMax_TextChanged;
                switch (((CycleObject)cbxCycleObjects.SelectedItem).Id)
                {
                    case 1:
                        _editedParam = (LoanAmountCycle)listViewLoanCycles.SelectedItems[0].Tag;
                        textBoxCycleMin.Text = _editedParam.Min.Value.ToString("0");
                        textBoxCycleMax.Text = _editedParam.Max.Value.ToString("0");
                        textBoxCycleMin.Enabled = true;
                        textBoxCycleMax.Enabled = true;
                        break;
                    case 2:
                        _editedParam = (RateCycle)listViewLoanCycles.SelectedItems[0].Tag;
                        textBoxCycleMin.Text = _editedParam.Min.Value.ToString("0.00");
                        textBoxCycleMax.Text = _editedParam.Max.Value.ToString("0.00");
                        textBoxCycleMin.Enabled = true;
                        textBoxCycleMax.Enabled = true;
                        break;
                    case 3:
                        _editedParam = (MaturityCycle)listViewLoanCycles.SelectedItems[0].Tag;
                        textBoxCycleMin.Text = _editedParam.Min.Value.ToString("0");
                        textBoxCycleMax.Text = _editedParam.Max.Value.ToString("0");
                        textBoxCycleMin.Enabled = true;
                        textBoxCycleMax.Enabled = true;
                        break;
                    default: break;
                }
            }
            catch { }
            finally
            {
                textBoxCycleMin.TextChanged += textBoxCycleMin_TextChanged;
                textBoxCycleMax.TextChanged += textBoxCycleMax_TextChanged;
            }
        }

        private void comboBoxLoanCyclesName_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelAmountCycles.Visible = true;
            buttonAddAmountCycle.Enabled = true;
            textBoxCycleMin.Enabled = false;
            textBoxCycleMax.Enabled = false;
            textBoxCycleMin.Text = "";
            textBoxCycleMax.Text = "";
            int cycleId = ((LoanCycle)comboBoxLoanCyclesName.SelectedItem).Id;
            if (cbUseLoanCycle.Checked)
                _product.CycleId = cycleId;
            if (cbxCycleObjects.SelectedItem == null || comboBoxLoanCyclesName.SelectedItem == null) return;
            _product.LoanAmountCycleParams =
                ServicesProvider.GetInstance().GetProductServices().GetLoanAmountCycleParameters(cycleId);
            _product.RateCycleParams =
                ServicesProvider.GetInstance().GetProductServices().GetInterestRateCycleParams(cycleId);
            foreach (RateCycle cycle in _product.RateCycleParams)
            {
                cycle.Min = cycle.Min * 100;
                cycle.Max = cycle.Max * 100;
            }
            _product.MaturityCycleParams =
                ServicesProvider.GetInstance().GetProductServices().GetMaturityCycleParams(_product.Id, cycleId);
            cbxCycleObjects.SelectedIndex = 1;//for raise selected index changed event
            cbxCycleObjects.SelectedIndex = 0;
            if (_product.LoanAmountCycleParams == null) _product.LoanAmountCycleParams = new List<LoanAmountCycle>();
            if (_product.RateCycleParams == null) _product.RateCycleParams = new List<RateCycle>();
            if (_product.MaturityCycleParams == null) _product.MaturityCycleParams = new List<MaturityCycle>();
        }

        private void textBoxGracePeriodMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            int keyCode = e.KeyChar;

            if (
                (keyCode >= 48 && keyCode <= 57) || 
                (keyCode == 8) || 
                (Char.IsControl(e.KeyChar) && e.KeyChar != ((char)Keys.V | (char)Keys.ControlKey)) 
                || 
                (Char.IsControl(e.KeyChar) && e.KeyChar != ((char)Keys.C | (char)Keys.ControlKey)) 
                || 
                (e.KeyChar.ToString() == NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void tbCreditInsuranceMin_TextChanged(object sender, EventArgs e)
        {
            _product.CreditInsuranceMin = ServicesHelper.ConvertStringToDecimal(tbCreditInsuranceMin.Text, true);
            buttonSave.Enabled = true;
        }

        private void tbCreditInsuranceMax_TextChanged(object sender, EventArgs e)
        {
            _product.CreditInsuranceMax = ServicesHelper.ConvertStringToDecimal(tbCreditInsuranceMax.Text, true);
            buttonSave.Enabled = true;
        }

        private void cbEnableEntryFeesCycle_CheckedChanged(object sender, EventArgs e)
        {
            buttonSave.Enabled = true;
            _product.UseEntryFeesCycles = cbEnableEntryFeesCycle.Checked;
            lblEntryFeesCycle.Visible = cbEnableEntryFeesCycle.Checked;
            lblEntryFeesFromCycle.Visible = cbEnableEntryFeesCycle.Checked;
            cmbEntryFeesCycles.Visible = cbEnableEntryFeesCycle.Checked;
            swbtnEntryFeesAddCycle.Visible = cbEnableEntryFeesCycle.Checked;
            swbtnEntryFeesRemoveCycle.Visible = cbEnableEntryFeesCycle.Checked;
            nudEntryFeescycleFrom.Visible = cbEnableEntryFeesCycle.Checked;
            ServicesProvider.GetInstance().GetProductServices().GetEntryFees(_product);
            FillListViewEntryFees();
            InitializeComboboxEntryFeeCycles(_product);
            SetListView();
        }

        private void SetListView()
        {
            if (cbEnableEntryFeesCycle.Checked && cmbEntryFeesCycles.Items.Count == 0)
                lvEntryFees.Enabled = false;
            else
                lvEntryFees.Enabled = true;
        }

        private void cmbEntryFeesCycle_SelectedValueChanged(object sender, EventArgs e)
        {
            if (_product.UseEntryFeesCycles == false)
                return;

            //deleting entry fees from loan product with current cycle_id
            if (_product.EntryFees != null && _product.EntryFees.Count != 0)
            {
                _product.EntryFees.RemoveAll(item => item.CycleId == Convert.ToInt32(lvEntryFees.Items[0].SubItems[IdxCycleId].Text));
            }
            foreach (ListViewItem item in lvEntryFees.Items)
            {
                if (CheckListViewCorrectness(item))
                {
                    EntryFee entryFee = GetEntryFeeFromListView(item);
                    _product.EntryFees.Add(entryFee);
                }
            }

            lvEntryFees.Items.Clear();

            foreach (EntryFee entryFee in _product.EntryFees)
            {
                if ((int)cmbEntryFeesCycles.SelectedItem == entryFee.CycleId)
                {
                    ListViewItem item = new ListViewItem("") { Tag = entryFee };
                    item.SubItems.Add(entryFee.Name);
                    item.SubItems.Add(entryFee.Min.HasValue
                                          ? entryFee.Min.Value.ToString(CultureInfo.CurrentCulture)
                                          : "");
                    item.SubItems.Add(entryFee.Max.HasValue
                                          ? entryFee.Max.Value.ToString(CultureInfo.CurrentCulture)
                                          : "");
                    item.SubItems.Add(entryFee.Value.HasValue
                                          ? entryFee.Value.Value.ToString(CultureInfo.CurrentCulture)
                                          : "");
                    item.SubItems.Add(entryFee.IsRate.ToString());

                    item.SubItems.Add("");
                    item.SubItems.Add(entryFee.CycleId.ToString());
                    item.SubItems.Add("");
                    item.SubItems.Add(entryFee.Index.ToString());

                    lvEntryFees.Items.Add(item);
                }
            }

            InitializeAdditionalEmptyRowInListView();
        }

        private static EntryFee GetEntryFeeFromListView(ListViewItem item)
        {
            decimal? min = string.IsNullOrEmpty(item.SubItems[IdxMin].Text) ? null : (decimal?)Convert.ToDecimal(item.SubItems[IdxMin].Text);
            decimal? max = string.IsNullOrEmpty(item.SubItems[IdxMax].Text) ? null : (decimal?)Convert.ToDecimal(item.SubItems[IdxMax].Text);
            decimal? v = string.IsNullOrEmpty(item.SubItems[IdxValue].Text) ? null : (decimal?)Convert.ToDecimal(item.SubItems[IdxValue].Text);
            int id = string.IsNullOrEmpty(item.SubItems[IdxId].Text) ? 0 : Convert.ToInt32(item.SubItems[IdxId].Text);
            int? cycleId = string.IsNullOrEmpty(item.SubItems[IdxCycleId].Text) ? null : (int?)Convert.ToInt32(item.SubItems[IdxCycleId].Text);

            EntryFee entryFee = new EntryFee
            {
                Id = id,
                Name = item.SubItems[IdxNameOfFee].Text,
                Min = min,
                Max = max,
                Value = v,
                IsRate = Convert.ToBoolean(item.SubItems[IdxIsRate].Text),
                IsAdded = id <= 0,
                CycleId = cycleId,
                IdForNewItem = 0,// Convert.ToInt32(item.SubItems[IdxIdForNewItem].Text),
                Index = Convert.ToInt32(item.SubItems[IdxIndex].Text)
            };
            return entryFee;
        }

        private void lvEntryFees_SubItemClicked(object sender, SubItemEventArgs e)
        {
            switch (e.SubItem)
            {
                case IdxNameOfFee:
                    lvEntryFees.StartEditing(tbEntryFeesValues, e.Item, e.SubItem);
                    break;

                case IdxMin:
                    lvEntryFees.StartEditing(tbEntryFeesValues, e.Item, e.SubItem);
                    break;

                case IdxMax:
                    lvEntryFees.StartEditing(tbEntryFeesValues, e.Item, e.SubItem);
                    break;

                case IdxValue:
                    lvEntryFees.StartEditing(tbEntryFeesValues, e.Item, e.SubItem);
                    break;

                case IdxIsRate:
                    lvEntryFees.StartEditing(cbRate, e.Item, e.SubItem);
                    break;
            }
        }

        private void lvEntryFees_SubItemEndEditing(object sender, SubItemEndEditingEventArgs e)
        {
            buttonSave.Enabled = true;
            var subItems = e.Item.SubItems;
            switch (e.SubItem)
            {
                case IdxNameOfFee:
                    subItems[e.SubItem].Text = tbEntryFeesValues.Text;
                    break;

                case IdxMin:
                    subItems[e.SubItem].Text = tbEntryFeesValues.Text;
                    break;

                case IdxMax:
                    subItems[e.SubItem].Text = tbEntryFeesValues.Text;
                    break;

                case IdxValue:
                    subItems[e.SubItem].Text = tbEntryFeesValues.Text;
                    break;

                case IdxIsRate:
                    subItems[e.SubItem].Text = cbRate.Text;
                    break;
            }

            _idForNewEntryFee++;

            e.Item.SubItems[IdxIdForNewItem].Text = _idForNewEntryFee.ToString();
            e.Item.SubItems[IdxIsAdded].Text = @"true";
            e.Item.SubItems[IdxCycleId].Text = cbEnableEntryFeesCycle.Checked ? cmbEntryFeesCycles.SelectedItem.ToString() : "";

            if (Convert.ToInt32(e.Item.SubItems[IdxIndex].Text) == lvEntryFees.Items.Count - 1 && e.SubItem != IdxIsRate && !string.IsNullOrEmpty(subItems[e.SubItem].Text))
            {
                if (string.IsNullOrEmpty(e.Item.SubItems[IdxIsRate].Text))
                    e.Item.SubItems[IdxIsRate].Text = @"True";
                InitializeAdditionalEmptyRowInListView();
            }
        }

        private bool CheckListViewCorrectness(ListViewItem item)
        {
            decimal? min = string.IsNullOrEmpty(item.SubItems[IdxMin].Text) ? null : (decimal?)Convert.ToDecimal(item.SubItems[IdxMin].Text);
            decimal? max = string.IsNullOrEmpty(item.SubItems[IdxMax].Text) ? null : (decimal?)Convert.ToDecimal(item.SubItems[IdxMax].Text);
            decimal? v = string.IsNullOrEmpty(item.SubItems[IdxValue].Text) ? null : (decimal?)Convert.ToDecimal(item.SubItems[IdxValue].Text);

            if (!string.IsNullOrEmpty(item.SubItems[IdxNameOfFee].Text))
            {
                if ((min != null && max != null && max > min) || v != null)
                {
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void UpdateEntryFee()
        {
            int? cycleId = string.IsNullOrEmpty(lvEntryFees.Items[0].SubItems[IdxCycleId].Text) ? null : (int?)Convert.ToDecimal(lvEntryFees.Items[0].SubItems[IdxCycleId].Text);
            if (_product.EntryFees != null && _product.EntryFees.Count != 0)
            {
                _product.EntryFees.RemoveAll(item => item.CycleId == cycleId);
            }

            foreach (ListViewItem item in lvEntryFees.Items)
            {
                if (CheckListViewCorrectness(item))
                {
                    EntryFee entryFee = GetEntryFeeFromListView(item);
                    if (_product.EntryFees == null)
                        _product.EntryFees = new List<EntryFee>();
                    _product.EntryFees.Add(entryFee);
                }
            }
        }

        private void interestRateTextChanged()
        {
            if (rbDecliningFixedPrincipalRelaInterest.Checked == false)
            {
                groupBoxInterestRate.Text = MultiLanguageStrings.GetString(Ressource.FrmAddLoanProduct,
                                                                           "grBoxIntRatePer.Text");
            }
            else
            {
                groupBoxInterestRate.Text = MultiLanguageStrings.GetString(Ressource.FrmAddLoanProduct,
                                                                            "grBoxIntRateYear.Text");
            }
        }

        private void rbDecliningFixedPrincipalRelaInterest_CheckedChanged(object sender, EventArgs e)
        {
            interestRateTextChanged();
        }

        private void swbtnEntryFeesAddCycle_Click(object sender, EventArgs e)
        {
       
            try
            {
                ServicesProvider.GetInstance().GetProductServices().CheckIfEntryFeeCycleExists(_product, (int)nudEntryFeescycleFrom.Value);
                cmbEntryFeesCycles.Items.Add((int)nudEntryFeescycleFrom.Value);
                cmbEntryFeesCycles.SelectedItem = (int)nudEntryFeescycleFrom.Value;
                _product.EntryFeeCycles.Add((int)nudEntryFeescycleFrom.Value);
                lvEntryFees.Enabled = true;
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void swbtnEntryFeesRemoveCycle_Click(object sender, EventArgs e)
        {
            if (_product.EntryFees != null && _product.EntryFees.Count != 0)
            {
                _product.EntryFees.RemoveAll(item => item.CycleId == (int?)cmbEntryFeesCycles.SelectedItem);
            }

            lvEntryFees.Items.Clear();
            cmbEntryFeesCycles.Items.Remove(cmbEntryFeesCycles.SelectedItem);
            if (cmbEntryFeesCycles.Items.Count > 0)
                cmbEntryFeesCycles.SelectedItem = cmbEntryFeesCycles.Items[0];
            else
            {
                lvEntryFees.Enabled = false;
            }
            buttonSave.Enabled = true;
        }
	}
}