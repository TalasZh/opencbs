// LICENSE PLACEHOLDER

using System;
using System.ComponentModel;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.Enums;
using OpenCBS.ExceptionsHandler;
using OpenCBS.GUI.Accounting;
using OpenCBS.GUI.Clients;
using OpenCBS.Services;
using OpenCBS.GUI.UserControl;
using OpenCBS.Services.Accounting;
using OpenCBS.Services.Currencies;
using OpenCBS.Shared;

namespace OpenCBS.GUI
{
    public class AddGuarantorForm : SweetBaseForm
    {
        private GroupBox groupBoxName;
        private Label labelNameOfLeader;
        private TextBox textBoxName;
        private System.Windows.Forms.Button buttonAddMembres;
        private System.Windows.Forms.Button buttonSelectAMember;
        private Label labelAmount;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private Guarantor _guarantor;
        private readonly Form _mdiParent;
        private TextBox textBoxDesc;
        private Label labelDesc;
        private GroupBox groupBoxAmount;
        private NumericUpDown nudAmount;
        private const Container components = null;
        private Currency code;

        public AddGuarantorForm(Form pMdiParent, Currency tcode)
        {
            _mdiParent = pMdiParent;
            _guarantor = new Guarantor();
            _guarantor.Amount = 0;
            code = tcode;
            Initialization();
        }

        public AddGuarantorForm(Guarantor guarantor, Form pMdiParent, bool isView, Currency tcode)
        {
            _mdiParent = pMdiParent;
            _guarantor = guarantor;
            code = tcode;

            Initialization();
            InitializeGuarantor();

            if (isView)
            {
                groupBoxName.Enabled = false;
                groupBoxAmount.Enabled = false;
                buttonSave.Enabled = false;
            }
        }

        private void Initialization()
        {
            InitializeComponent();
            nudAmount.Minimum = 0;
            nudAmount.Maximum = decimal.MaxValue;
        }

        private void InitializeGuarantor()
        {
            buttonAddMembres.Enabled = false;
            buttonSelectAMember.Enabled = false;
            textBoxName.Text = _guarantor.Tiers.Name;
            nudAmount.Value = _guarantor.Amount.Value;
            textBoxDesc.Text = _guarantor.Description;
        }

        public Guarantor Guarantor
        {
            get { return _guarantor; }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddGuarantorForm));
            this.groupBoxAmount = new System.Windows.Forms.GroupBox();
            this.nudAmount = new System.Windows.Forms.NumericUpDown();
            this.textBoxDesc = new System.Windows.Forms.TextBox();
            this.labelAmount = new System.Windows.Forms.Label();
            this.labelDesc = new System.Windows.Forms.Label();
            this.groupBoxName = new System.Windows.Forms.GroupBox();
            this.labelNameOfLeader = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.buttonAddMembres = new System.Windows.Forms.Button();
            this.buttonSelectAMember = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.groupBoxAmount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmount)).BeginInit();
            this.groupBoxName.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxAmount
            //
            this.groupBoxAmount.Controls.Add(this.nudAmount);
            this.groupBoxAmount.Controls.Add(this.textBoxDesc);
            this.groupBoxAmount.Controls.Add(this.labelAmount);
            this.groupBoxAmount.Controls.Add(this.labelDesc);
            resources.ApplyResources(this.groupBoxAmount, "groupBoxAmount");
            this.groupBoxAmount.Name = "groupBoxAmount";
            this.groupBoxAmount.TabStop = false;
            // 
            // nudAmount
            // 
            resources.ApplyResources(this.nudAmount, "nudAmount");
            this.nudAmount.Name = "nudAmount";
            // 
            // textBoxDesc
            // 
            resources.ApplyResources(this.textBoxDesc, "textBoxDesc");
            this.textBoxDesc.Name = "textBoxDesc";
            this.textBoxDesc.TextChanged += new System.EventHandler(this.textBoxDesc_TextChanged);
            // 
            // labelAmount
            // 
            resources.ApplyResources(this.labelAmount, "labelAmount");
            this.labelAmount.BackColor = System.Drawing.Color.Transparent;
            this.labelAmount.Name = "labelAmount";
            // 
            // labelDesc
            // 
            resources.ApplyResources(this.labelDesc, "labelDesc");
            this.labelDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelDesc.Name = "labelDesc";
            // 
            // groupBoxName
            //
            this.groupBoxName.Controls.Add(this.labelNameOfLeader);
            this.groupBoxName.Controls.Add(this.textBoxName);
            this.groupBoxName.Controls.Add(this.buttonAddMembres);
            this.groupBoxName.Controls.Add(this.buttonSelectAMember);
            resources.ApplyResources(this.groupBoxName, "groupBoxName");
            this.groupBoxName.Name = "groupBoxName";
            this.groupBoxName.TabStop = false;
            // 
            // labelNameOfLeader
            // 
            resources.ApplyResources(this.labelNameOfLeader, "labelNameOfLeader");
            this.labelNameOfLeader.Name = "labelNameOfLeader";
            // 
            // textBoxName
            // 
            resources.ApplyResources(this.textBoxName, "textBoxName");
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.ReadOnly = true;
            // 
            // buttonAddMembres
            //
            resources.ApplyResources(this.buttonAddMembres, "buttonAddMembres");
            this.buttonAddMembres.Name = "buttonAddMembres";
            this.buttonAddMembres.Click += new System.EventHandler(this.buttonAddMembres_Click);
            // 
            // buttonSelectAMember
            //
            resources.ApplyResources(this.buttonSelectAMember, "buttonSelectAMember");
            this.buttonSelectAMember.Name = "buttonSelectAMember";
            this.buttonSelectAMember.Click += new System.EventHandler(this.buttonSelectAMember_Click);
            // 
            // buttonCancel
            //
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            //
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // AddGuarantorForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBoxAmount);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupBoxName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddGuarantorForm";
            this.groupBoxAmount.ResumeLayout(false);
            this.groupBoxAmount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmount)).EndInit();
            this.groupBoxName.ResumeLayout(false);
            this.groupBoxName.PerformLayout();
            this.ResumeLayout(false);

		}

        #endregion

        private void buttonAddMembres_Click(object sender, EventArgs e)
        {
            AddAGuarantor();
        }

        private void AddAGuarantor()
        {
            var personForm = new ClientForm(OClientTypes.Person, _mdiParent, true);
            personForm.ShowDialog();
            _guarantor.Tiers = personForm.Person;

            try
            {
                textBoxName.Text = ServicesProvider.GetInstance().GetClientServices().ClientIsAPerson(_guarantor.Tiers) 
                    ? _guarantor.Tiers.Name : String.Empty;
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void SelectAGuarantor()
        {
            using (SearchClientForm searchClientForm = SearchClientForm.GetInstance(OClientTypes.Person, false))
            {
                searchClientForm.ShowDialog();
                _guarantor.Tiers = searchClientForm.Client;

                try
                {
                    if (ServicesProvider.GetInstance().GetClientServices().ClientIsAPerson(_guarantor.Tiers))
                    {
                        ServicesProvider.GetInstance().GetClientServices().ClientsNumberGuarantee(_guarantor.Tiers);
                        textBoxName.Text = _guarantor.Tiers.Name;
                    }
                    else
                        textBoxName.Text = String.Empty;
                }
                catch (Exception ex)
                {
                    new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                }
            }
        }

        private void buttonSelectAMember_Click(object sender, EventArgs e)
        {
            SelectAGuarantor();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            _guarantor = null;
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            _guarantor.Amount = nudAmount.Value;
            if (textBoxName.Text == String.Empty)
            {
                Fail("GuarantorNameIsNull");
                return;
            }
            if (_guarantor.Amount == 0 || string.IsNullOrEmpty(nudAmount.Text))
            {
                Fail("AmountIsNull");
                return;
            }

            if (MaxAmountExceed())
            {
                MaxAmountExceedMessage();
                return;
            }
            Close();
        }

        private void MaxAmountExceedMessage()
        {
            string caption = GetString("SweetBaseForm", "error");
            string errorMessage = GetString("GuarantorMaxAmount.Text") + " " +
                                  ServicesProvider.GetInstance().GetGeneralSettings().MaxGuarantorAmount.ToString() + " " +
                                  ServicesProvider.GetInstance().GetCurrencyServices().GetPivot().Code;
            MessageBox.Show(errorMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool MaxAmountExceed()
        {
            OCurrency maxGuarantorAmount = ServicesProvider.GetInstance().GetGeneralSettings().MaxGuarantorAmount;
            OCurrency tempAmount;
            if (code.IsPivot) tempAmount = _guarantor.Amount;
            else
            {
                ExchangeRateServices rateServices = ServicesProvider.GetInstance().GetExchangeRateServices();
                ExchangeRate currentRate = rateServices.SelectExchangeRate(TimeProvider.Now, code);
                if (currentRate == null)
                {
                    ExchangeRateForm xrForm = new ExchangeRateForm(TimeProvider.Now.Date, code);
                    xrForm.ShowDialog();
                    currentRate = xrForm.ExchangeRate;
                }
                tempAmount = _guarantor.Amount/currentRate.Rate;
            }

            if (tempAmount > maxGuarantorAmount)
                return true;
            return false;
        }

        private void textBoxDesc_TextChanged(object sender, EventArgs e)
        {
            _guarantor.Description = textBoxDesc.Text;
        }

    }
}
