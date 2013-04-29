//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright © 2006,2007 OCTO Technology & OXUS Development Network
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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Octopus.CoreDomain.Clients;
using Octopus.CoreDomain.Contracts.Loans;
using Octopus.CoreDomain.Contracts.Loans.Installments;
using Octopus.ExceptionsHandler;
using Octopus.Services;
using Octopus.Shared;
using Octopus.GUI.UserControl;

namespace Octopus.GUI.Contracts
{
    /// <summary>
    /// Summary description for ContractReschedulingForm.
    /// </summary>
    public class ContractReschedulingForm : SweetBaseForm
    {
        private bool _chargeInterestDuringShift;
        private bool _chargeInterestDuringGracePeriod;
        private Container components = null;

        private int numberOfMaturity;
        private decimal _ir;
        private int _dateOffsetOrAmount;
        private TableLayoutPanel tableLayoutPanel1;
        private GroupBox groupBoxParameters;
        private ListView listViewRepayments;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader9;
        private Label labelContractCode;
        private Label labelTitleRescheduleContract;
        private Label labelMaturity;
        private CheckBox checkBoxAccrue;
        private NumericUpDown numericUpDownMaturity;
        private NumericUpDown numericUpDownNewIR;
        private Label labelMaturityUnity;
        private Loan contract;
        private Label labelShiftDate;
        private Label labelShiftDateDays;
        private TextBox tbDateOffset;
        public DialogResult resultReschedulingForm;
        private Label lbNewInterest;
        private Label lblGracePeriod;
        private NumericUpDown nudGracePeriod;
        private int _gracePeriod;
        private CheckBox cbChargeInterestDuringGracePeriod;
        private GroupBox groupBoxConfirm;
        private SweetButton buttonCancel;
        private SweetButton buttonConfirm;

        public ContractReschedulingForm(Loan contract, IClient pClient)
        {
            InitializeComponent();

            this.contract = contract;
            labelContractCode.Text = contract.Code;
            labelMaturityUnity.Text = contract.InstallmentType.Name;
            _ir = Convert.ToDecimal(contract.InterestRate);
            numericUpDownNewIR.Value = _ir * 100;
            numericUpDownNewIR.Text = (_ir* 100).ToString();
            if (contract.Product.InterestRate.HasValue) { /* checkBoxIRChanged.Enabled = false; */ }
            else
            {
                numericUpDownNewIR.Minimum = Convert.ToDecimal(contract.Product.InterestRateMin * 100);
                numericUpDownNewIR.Maximum = Convert.ToDecimal(contract.Product.InterestRateMax * 100);
            }
            DisplayInstallmentsForRepaymentsStatus(this.contract);
        }

        public Loan Contract
        {
            get { return contract; }
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

            _chargeInterestDuringShift = checkBoxAccrue.Checked;
            _chargeInterestDuringGracePeriod = cbChargeInterestDuringGracePeriod.Checked;
            _ir = Convert.ToDecimal(numericUpDownNewIR.Value / 100);
            _gracePeriod = Convert.ToInt32(nudGracePeriod.Value);
        }

        private void DisplayInstallmentsForRepaymentsStatus(Loan contractToDisplay)
        {
            listViewRepayments.Items.Clear();
            foreach (Installment installment in contractToDisplay.InstallmentList)
            {
                ListViewItem listViewItem = new ListViewItem(installment.Number.ToString());
                if (installment.IsRepaid)
                {
                    listViewItem.BackColor = Color.FromArgb(0,88,56);
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

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContractReschedulingForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listViewRepayments = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.groupBoxParameters = new System.Windows.Forms.GroupBox();
            this.cbChargeInterestDuringGracePeriod = new System.Windows.Forms.CheckBox();
            this.nudGracePeriod = new System.Windows.Forms.NumericUpDown();
            this.lblGracePeriod = new System.Windows.Forms.Label();
            this.lbNewInterest = new System.Windows.Forms.Label();
            this.labelShiftDateDays = new System.Windows.Forms.Label();
            this.tbDateOffset = new System.Windows.Forms.TextBox();
            this.labelShiftDate = new System.Windows.Forms.Label();
            this.labelMaturityUnity = new System.Windows.Forms.Label();
            this.numericUpDownNewIR = new System.Windows.Forms.NumericUpDown();
            this.checkBoxAccrue = new System.Windows.Forms.CheckBox();
            this.numericUpDownMaturity = new System.Windows.Forms.NumericUpDown();
            this.labelMaturity = new System.Windows.Forms.Label();
            this.labelContractCode = new System.Windows.Forms.Label();
            this.labelTitleRescheduleContract = new System.Windows.Forms.Label();
            this.groupBoxConfirm = new System.Windows.Forms.GroupBox();
            this.buttonCancel = new Octopus.GUI.UserControl.SweetButton();
            this.buttonConfirm = new Octopus.GUI.UserControl.SweetButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGracePeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNewIR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaturity)).BeginInit();
            this.groupBoxConfirm.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.listViewRepayments, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxParameters, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxConfirm, 0, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // listViewRepayments
            // 
            this.listViewRepayments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            resources.ApplyResources(this.listViewRepayments, "listViewRepayments");
            this.listViewRepayments.FullRowSelect = true;
            this.listViewRepayments.GridLines = true;
            this.listViewRepayments.MultiSelect = false;
            this.listViewRepayments.Name = "listViewRepayments";
            this.listViewRepayments.UseCompatibleStateImageBehavior = false;
            this.listViewRepayments.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // columnHeader3
            // 
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            // 
            // columnHeader4
            // 
            resources.ApplyResources(this.columnHeader4, "columnHeader4");
            // 
            // columnHeader5
            // 
            resources.ApplyResources(this.columnHeader5, "columnHeader5");
            // 
            // columnHeader6
            // 
            resources.ApplyResources(this.columnHeader6, "columnHeader6");
            // 
            // columnHeader7
            // 
            resources.ApplyResources(this.columnHeader7, "columnHeader7");
            // 
            // columnHeader8
            // 
            resources.ApplyResources(this.columnHeader8, "columnHeader8");
            // 
            // columnHeader9
            // 
            resources.ApplyResources(this.columnHeader9, "columnHeader9");
            // 
            // groupBoxParameters
            // 
            this.groupBoxParameters.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxParameters.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_gris_180;
            resources.ApplyResources(this.groupBoxParameters, "groupBoxParameters");
            this.groupBoxParameters.Controls.Add(this.cbChargeInterestDuringGracePeriod);
            this.groupBoxParameters.Controls.Add(this.nudGracePeriod);
            this.groupBoxParameters.Controls.Add(this.lblGracePeriod);
            this.groupBoxParameters.Controls.Add(this.lbNewInterest);
            this.groupBoxParameters.Controls.Add(this.labelShiftDateDays);
            this.groupBoxParameters.Controls.Add(this.tbDateOffset);
            this.groupBoxParameters.Controls.Add(this.labelShiftDate);
            this.groupBoxParameters.Controls.Add(this.labelMaturityUnity);
            this.groupBoxParameters.Controls.Add(this.numericUpDownNewIR);
            this.groupBoxParameters.Controls.Add(this.checkBoxAccrue);
            this.groupBoxParameters.Controls.Add(this.numericUpDownMaturity);
            this.groupBoxParameters.Controls.Add(this.labelMaturity);
            this.groupBoxParameters.Controls.Add(this.labelContractCode);
            this.groupBoxParameters.Controls.Add(this.labelTitleRescheduleContract);
            this.groupBoxParameters.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.groupBoxParameters.Name = "groupBoxParameters";
            this.groupBoxParameters.TabStop = false;
            // 
            // cbChargeInterestDuringGracePeriod
            // 
            resources.ApplyResources(this.cbChargeInterestDuringGracePeriod, "cbChargeInterestDuringGracePeriod");
            this.cbChargeInterestDuringGracePeriod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.cbChargeInterestDuringGracePeriod.Name = "cbChargeInterestDuringGracePeriod";
            this.cbChargeInterestDuringGracePeriod.UseVisualStyleBackColor = true;
            this.cbChargeInterestDuringGracePeriod.CheckedChanged += new System.EventHandler(this.cbChargeInterestDuringGracePeriod_CheckedChanged);
            // 
            // nudGracePeriod
            // 
            resources.ApplyResources(this.nudGracePeriod, "nudGracePeriod");
            this.nudGracePeriod.Name = "nudGracePeriod";
            this.nudGracePeriod.ValueChanged += new System.EventHandler(this.OnGracePeriodChanged);
            // 
            // lblGracePeriod
            // 
            resources.ApplyResources(this.lblGracePeriod, "lblGracePeriod");
            this.lblGracePeriod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lblGracePeriod.Name = "lblGracePeriod";
            // 
            // lbNewInterest
            // 
            resources.ApplyResources(this.lbNewInterest, "lbNewInterest");
            this.lbNewInterest.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lbNewInterest.Name = "lbNewInterest";
            // 
            // labelShiftDateDays
            // 
            resources.ApplyResources(this.labelShiftDateDays, "labelShiftDateDays");
            this.labelShiftDateDays.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.labelShiftDateDays.Name = "labelShiftDateDays";
            // 
            // tbDateOffset
            // 
            resources.ApplyResources(this.tbDateOffset, "tbDateOffset");
            this.tbDateOffset.Name = "tbDateOffset";
            this.tbDateOffset.TextChanged += new System.EventHandler(this.tbDateOffset_TextChanged);
            this.tbDateOffset.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbDateOffset_KeyDown);
            this.tbDateOffset.Leave += new System.EventHandler(this.tbDateOffset_Enter);
            this.tbDateOffset.Enter += new System.EventHandler(this.tbDateOffset_Enter);
            // 
            // labelShiftDate
            // 
            resources.ApplyResources(this.labelShiftDate, "labelShiftDate");
            this.labelShiftDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.labelShiftDate.Name = "labelShiftDate";
            // 
            // labelMaturityUnity
            // 
            resources.ApplyResources(this.labelMaturityUnity, "labelMaturityUnity");
            this.labelMaturityUnity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.labelMaturityUnity.Name = "labelMaturityUnity";
            // 
            // numericUpDownNewIR
            // 
            this.numericUpDownNewIR.DecimalPlaces = 2;
            this.numericUpDownNewIR.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            resources.ApplyResources(this.numericUpDownNewIR, "numericUpDownNewIR");
            this.numericUpDownNewIR.Name = "numericUpDownNewIR";
            this.numericUpDownNewIR.ValueChanged += new System.EventHandler(this.numericUpDownNewIR_ValueChanged);
            // 
            // checkBoxAccrue
            // 
            resources.ApplyResources(this.checkBoxAccrue, "checkBoxAccrue");
            this.checkBoxAccrue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.checkBoxAccrue.Name = "checkBoxAccrue";
            this.checkBoxAccrue.UseVisualStyleBackColor = true;
            this.checkBoxAccrue.CheckedChanged += new System.EventHandler(this.checkBoxAccrue_CheckedChanged);
            // 
            // numericUpDownMaturity
            // 
            resources.ApplyResources(this.numericUpDownMaturity, "numericUpDownMaturity");
            this.numericUpDownMaturity.Name = "numericUpDownMaturity";
            this.numericUpDownMaturity.ValueChanged += new System.EventHandler(this.OnMaturityChanged);
            // 
            // labelMaturity
            // 
            resources.ApplyResources(this.labelMaturity, "labelMaturity");
            this.labelMaturity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.labelMaturity.Name = "labelMaturity";
            // 
            // labelContractCode
            // 
            resources.ApplyResources(this.labelContractCode, "labelContractCode");
            this.labelContractCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.labelContractCode.Name = "labelContractCode";
            // 
            // labelTitleRescheduleContract
            // 
            resources.ApplyResources(this.labelTitleRescheduleContract, "labelTitleRescheduleContract");
            this.labelTitleRescheduleContract.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.labelTitleRescheduleContract.Name = "labelTitleRescheduleContract";
            // 
            // groupBoxConfirm
            // 
            this.groupBoxConfirm.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxConfirm.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_gris_180;
            resources.ApplyResources(this.groupBoxConfirm, "groupBoxConfirm");
            this.groupBoxConfirm.Controls.Add(this.buttonCancel);
            this.groupBoxConfirm.Controls.Add(this.buttonConfirm);
            this.groupBoxConfirm.Name = "groupBoxConfirm";
            this.groupBoxConfirm.TabStop = false;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonCancel.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.None;
            this.buttonCancel.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_close;
            this.buttonCancel.Menu = null;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonConfirm
            // 
            resources.ApplyResources(this.buttonConfirm, "buttonConfirm");
            this.buttonConfirm.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonConfirm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonConfirm.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.None;
            this.buttonConfirm.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_validity;
            this.buttonConfirm.Menu = null;
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.UseVisualStyleBackColor = false;
            this.buttonConfirm.Click += new System.EventHandler(this.buttonConfirm_Click);
            // 
            // ContractReschedulingForm
            // 
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_gris_180;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ContractReschedulingForm";
            this.Load += new System.EventHandler(this.OnLoad);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBoxParameters.ResumeLayout(false);
            this.groupBoxParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGracePeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNewIR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaturity)).EndInit();
            this.groupBoxConfirm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            try {
                if (numericUpDownMaturity.Value != 0 || int.Parse(tbDateOffset.Text) > 0 || nudGracePeriod.Value > 0
                    || (numericUpDownNewIR.Value / 100 != contract.InterestRate))
                {
                    string messageConfirm = "Please confirm you want to reschedule the contract: " + contract.Code +
                                            "\nwith the following parameters: \n\nShift schedule: " +
                                            _dateOffsetOrAmount +
                                            "\nCharge interest during shift: " +
                                            (_chargeInterestDuringShift ? "Yes" : "No") +
                                            "\nNew installments: " + numberOfMaturity +
                                            "\nInterest rate: " + _ir*100 + "%" +
                                            "\nGrace period: " + _gracePeriod +
                                            "\nCharge interest during grace period: " +
                                            (_chargeInterestDuringGracePeriod ? "Yes" : "No");
                    resultReschedulingForm = MessageBox.Show(messageConfirm, @"Confirm rescheduling",
                                                             MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (resultReschedulingForm == DialogResult.OK)
                    {
                        _GetParameters();
                        contract.Rescheduled = true;
                        contract = ServicesProvider.GetInstance().GetContractServices().
                            Reschedule(contract, TimeProvider.Today, numberOfMaturity, _dateOffsetOrAmount,
                                       _chargeInterestDuringShift, _ir, _gracePeriod, _chargeInterestDuringGracePeriod);
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show(@"Please enter some valid parameters", @"Wrong parameters", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
            catch (Exception up)
            {
                contract.Rescheduled = false;
                new frmShowError(CustomExceptionHandler.ShowExceptionText(up)).ShowDialog();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void checkBoxAccrue_CheckedChanged(object sender, EventArgs e)
        {
            _GetParameters();
            Loan fakeContract = ServicesProvider.GetInstance().GetContractServices().FakeReschedule(contract,
                                                                                                    TimeProvider.Today,
                                                                                                    numberOfMaturity,
                                                                                                    _dateOffsetOrAmount,
                                                                                                    _chargeInterestDuringShift,
                                                                                                    _ir, _gracePeriod,
                                                                                                    _chargeInterestDuringGracePeriod);
            DisplayInstallmentsForRepaymentsStatus(fakeContract);
        }

        private void numericUpDownNewIR_ValueChanged(object sender, EventArgs e)
        {
            _GetParameters();
            Loan fakeContract = ServicesProvider.GetInstance().GetContractServices().FakeReschedule(contract,
                                                                                                    TimeProvider.
                                                                                                        Today,
                                                                                                    numberOfMaturity,
                                                                                                    _dateOffsetOrAmount,
                                                                                                    _chargeInterestDuringShift,
                                                                                                    _ir, _gracePeriod,
                                                                                                    _chargeInterestDuringGracePeriod);
            DisplayInstallmentsForRepaymentsStatus(fakeContract);
        }

        private void SetGracePeriodUpperLimit()
        {
            var numUnpaid = contract.InstallmentList.FindAll(i => !i.IsRepaid).Count;
            var upperLimit = numUnpaid + numericUpDownMaturity.Value - 1;
            if (nudGracePeriod.Value > upperLimit)
            {
                nudGracePeriod.Value = upperLimit;
            }
            nudGracePeriod.Maximum = upperLimit;
        }

        private void OnMaturityChanged(object sender, EventArgs e)
        {
            SetGracePeriodUpperLimit();
            _GetParameters();
            Loan fakeContract = ServicesProvider.GetInstance().GetContractServices().FakeReschedule(contract,
                                                                                                    TimeProvider.
                                                                                                        Today,
                                                                                                    numberOfMaturity,
                                                                                                    _dateOffsetOrAmount,
                                                                                                    _chargeInterestDuringShift,
                                                                                                    _ir, _gracePeriod,
                                                                                                    _chargeInterestDuringGracePeriod);
            DisplayInstallmentsForRepaymentsStatus(fakeContract);
        }

        private void tbDateOffset_TextChanged(object sender, EventArgs e)
        {
            _GetParameters();
            Loan fakeContract = ServicesProvider.GetInstance().GetContractServices().FakeReschedule(contract,
                                                                                                    TimeProvider.
                                                                                                        Today,
                                                                                                    numberOfMaturity,
                                                                                                    _dateOffsetOrAmount,
                                                                                                    _chargeInterestDuringShift,
                                                                                                    _ir, _gracePeriod,
                                                                                                    _chargeInterestDuringGracePeriod);
            DisplayInstallmentsForRepaymentsStatus(fakeContract);
        }

        private void tbDateOffset_KeyDown(object sender, KeyEventArgs e)
        {
            Keys c = e.KeyCode;
            if (c >= Keys.NumPad0 && c <= Keys.NumPad9) return;
            if (c >= Keys.D0 && c <= Keys.D9) return;
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

        private void OnLoad(object sender, EventArgs e)
        {
            cbChargeInterestDuringGracePeriod.Enabled = nudGracePeriod.Enabled;
            lblGracePeriod.Enabled = nudGracePeriod.Enabled;
            SetGracePeriodUpperLimit();
        }

        private void OnGracePeriodChanged(object sender, EventArgs e)
        {
            _GetParameters();
            Loan fakeContract = ServicesProvider.GetInstance().GetContractServices().FakeReschedule(contract,
                                                                                                    TimeProvider.
                                                                                                        Today,
                                                                                                    numberOfMaturity,
                                                                                                    _dateOffsetOrAmount,
                                                                                                    _chargeInterestDuringShift,
                                                                                                    _ir, _gracePeriod,
                                                                                                    _chargeInterestDuringGracePeriod);
            DisplayInstallmentsForRepaymentsStatus(fakeContract);
        }

        private void cbChargeInterestDuringGracePeriod_CheckedChanged(object sender, EventArgs e)
        {
            _GetParameters();
            Loan fakeContract = ServicesProvider.GetInstance().GetContractServices().FakeReschedule(contract,
                                                                                                    TimeProvider.
                                                                                                        Today,
                                                                                                    numberOfMaturity,
                                                                                                    _dateOffsetOrAmount,
                                                                                                    _chargeInterestDuringShift,
                                                                                                    _ir, _gracePeriod,
                                                                                                    _chargeInterestDuringGracePeriod);
            DisplayInstallmentsForRepaymentsStatus(fakeContract);
        }
    }
}