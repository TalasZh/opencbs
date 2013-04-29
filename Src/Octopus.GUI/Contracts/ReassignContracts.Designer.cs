using Octopus.GUI.UserControl;

namespace Octopus.GUI.Contracts
{
    partial class ReassignContractsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReassignContractsForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listViewAlert = new System.Windows.Forms.ListView();
            this.columnHeaderContract = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderDate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderAmount = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderClient = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderClientDistrict = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderStartDate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCloseDate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCreateDate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderInstallmentType = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderInterestRate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderOLB = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderContractId = new System.Windows.Forms.ColumnHeader();
            this.lblContractFilter = new System.Windows.Forms.Label();
            this.textBoxContractFilter = new System.Windows.Forms.TextBox();
            this.statusStripContacts = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.gbLoanOffficers = new System.Windows.Forms.GroupBox();
            this.chkBox_only_active = new System.Windows.Forms.CheckBox();
            this.checkBoxAll = new System.Windows.Forms.CheckBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.cbLoanOfficerTo = new System.Windows.Forms.ComboBox();
            this.cbLoanOfficerFrom = new System.Windows.Forms.ComboBox();
            this.lblCaption = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAssing = new Octopus.GUI.UserControl.SweetButton();
            this.buttonClose = new Octopus.GUI.UserControl.SweetButton();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStripContacts.SuspendLayout();
            this.gbLoanOffficers.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listViewAlert);
            this.splitContainer1.Panel1.Controls.Add(this.lblContractFilter);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxContractFilter);
            this.splitContainer1.Panel1.Controls.Add(this.statusStripContacts);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gbLoanOffficers);
            // 
            // listViewAlert
            // 
            this.listViewAlert.BackColor = System.Drawing.SystemColors.Window;
            this.listViewAlert.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewAlert.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderContract,
            this.columnHeaderDate,
            this.columnHeaderAmount,
            this.columnHeaderClient,
            this.columnHeaderClientDistrict,
            this.columnHeaderStartDate,
            this.columnHeaderCloseDate,
            this.columnHeaderCreateDate,
            this.columnHeaderInstallmentType,
            this.columnHeaderInterestRate,
            this.columnHeaderOLB,
            this.columnHeaderContractId});
            resources.ApplyResources(this.listViewAlert, "listViewAlert");
            this.listViewAlert.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.listViewAlert.FullRowSelect = true;
            this.listViewAlert.GridLines = true;
            this.listViewAlert.Name = "listViewAlert";
            this.listViewAlert.UseCompatibleStateImageBehavior = false;
            this.listViewAlert.View = System.Windows.Forms.View.Details;
            this.listViewAlert.SelectedIndexChanged += new System.EventHandler(this.listViewAlert_SelectedIndexChanged);
            // 
            // columnHeaderContract
            // 
            resources.ApplyResources(this.columnHeaderContract, "columnHeaderContract");
            // 
            // columnHeaderDate
            // 
            resources.ApplyResources(this.columnHeaderDate, "columnHeaderDate");
            // 
            // columnHeaderAmount
            // 
            resources.ApplyResources(this.columnHeaderAmount, "columnHeaderAmount");
            // 
            // columnHeaderClient
            // 
            resources.ApplyResources(this.columnHeaderClient, "columnHeaderClient");
            // 
            // columnHeaderClientDistrict
            // 
            resources.ApplyResources(this.columnHeaderClientDistrict, "columnHeaderClientDistrict");
            // 
            // columnHeaderStartDate
            // 
            resources.ApplyResources(this.columnHeaderStartDate, "columnHeaderStartDate");
            // 
            // columnHeaderCloseDate
            // 
            resources.ApplyResources(this.columnHeaderCloseDate, "columnHeaderCloseDate");
            // 
            // columnHeaderCreateDate
            // 
            resources.ApplyResources(this.columnHeaderCreateDate, "columnHeaderCreateDate");
            // 
            // columnHeaderInstallmentType
            // 
            resources.ApplyResources(this.columnHeaderInstallmentType, "columnHeaderInstallmentType");
            // 
            // columnHeaderInterestRate
            // 
            resources.ApplyResources(this.columnHeaderInterestRate, "columnHeaderInterestRate");
            // 
            // columnHeaderOLB
            // 
            resources.ApplyResources(this.columnHeaderOLB, "columnHeaderOLB");
            // 
            // columnHeaderContractId
            // 
            resources.ApplyResources(this.columnHeaderContractId, "columnHeaderContractId");
            // 
            // lblContractFilter
            // 
            resources.ApplyResources(this.lblContractFilter, "lblContractFilter");
            this.lblContractFilter.Name = "lblContractFilter";
            // 
            // textBoxContractFilter
            // 
            resources.ApplyResources(this.textBoxContractFilter, "textBoxContractFilter");
            this.textBoxContractFilter.Name = "textBoxContractFilter";
            this.textBoxContractFilter.TextChanged += new System.EventHandler(this.textBoxContractFilter_TextChanged);
            // 
            // statusStripContacts
            // 
            this.statusStripContacts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelTotal});
            resources.ApplyResources(this.statusStripContacts, "statusStripContacts");
            this.statusStripContacts.Name = "statusStripContacts";
            // 
            // toolStripStatusLabelTotal
            // 
            this.toolStripStatusLabelTotal.Name = "toolStripStatusLabelTotal";
            resources.ApplyResources(this.toolStripStatusLabelTotal, "toolStripStatusLabelTotal");
            // 
            // gbLoanOffficers
            // 
            this.gbLoanOffficers.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_gris_180;
            resources.ApplyResources(this.gbLoanOffficers, "gbLoanOffficers");
            this.gbLoanOffficers.Controls.Add(this.chkBox_only_active);
            this.gbLoanOffficers.Controls.Add(this.checkBoxAll);
            this.gbLoanOffficers.Controls.Add(this.btnAssing);
            this.gbLoanOffficers.Controls.Add(this.lblTo);
            this.gbLoanOffficers.Controls.Add(this.lblFrom);
            this.gbLoanOffficers.Controls.Add(this.cbLoanOfficerTo);
            this.gbLoanOffficers.Controls.Add(this.cbLoanOfficerFrom);
            this.gbLoanOffficers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.gbLoanOffficers.Name = "gbLoanOffficers";
            this.gbLoanOffficers.TabStop = false;
            // 
            // chkBox_only_active
            // 
            resources.ApplyResources(this.chkBox_only_active, "chkBox_only_active");
            this.chkBox_only_active.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.chkBox_only_active.Name = "chkBox_only_active";
            this.chkBox_only_active.UseVisualStyleBackColor = true;
            this.chkBox_only_active.CheckedChanged += new System.EventHandler(this.chkBox_only_active_CheckedChanged);
            // 
            // checkBoxAll
            // 
            resources.ApplyResources(this.checkBoxAll, "checkBoxAll");
            this.checkBoxAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.checkBoxAll.Name = "checkBoxAll";
            this.checkBoxAll.UseVisualStyleBackColor = true;
            this.checkBoxAll.CheckedChanged += new System.EventHandler(this.checkBoxAll_CheckedChanged);
            // 
            // lblTo
            // 
            resources.ApplyResources(this.lblTo, "lblTo");
            this.lblTo.BackColor = System.Drawing.Color.White;
            this.lblTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lblTo.Name = "lblTo";
            // 
            // lblFrom
            // 
            resources.ApplyResources(this.lblFrom, "lblFrom");
            this.lblFrom.BackColor = System.Drawing.Color.White;
            this.lblFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lblFrom.Name = "lblFrom";
            // 
            // cbLoanOfficerTo
            // 
            this.cbLoanOfficerTo.BackColor = System.Drawing.SystemColors.Window;
            this.cbLoanOfficerTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLoanOfficerTo.FormattingEnabled = true;
            resources.ApplyResources(this.cbLoanOfficerTo, "cbLoanOfficerTo");
            this.cbLoanOfficerTo.Name = "cbLoanOfficerTo";
            this.cbLoanOfficerTo.SelectedIndexChanged += new System.EventHandler(this.cbLoanOfficerTo_SelectedIndexChanged);
            // 
            // cbLoanOfficerFrom
            // 
            this.cbLoanOfficerFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLoanOfficerFrom.FormattingEnabled = true;
            resources.ApplyResources(this.cbLoanOfficerFrom, "cbLoanOfficerFrom");
            this.cbLoanOfficerFrom.Name = "cbLoanOfficerFrom";
            this.cbLoanOfficerFrom.SelectedIndexChanged += new System.EventHandler(this.cbLoanOfficerFrom_SelectedIndexChanged);
            // 
            // lblCaption
            // 
            this.lblCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.lblCaption, "lblCaption");
            this.lblCaption.ForeColor = System.Drawing.Color.White;
            this.lblCaption.Name = "lblCaption";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.lblCaption, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonClose, 1, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // btnAssing
            // 
            this.btnAssing.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.btnAssing, "btnAssing");
            this.btnAssing.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnAssing.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Save;
            this.btnAssing.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_validity;
            this.btnAssing.Menu = null;
            this.btnAssing.Name = "btnAssing";
            this.btnAssing.UseVisualStyleBackColor = false;
            this.btnAssing.Click += new System.EventHandler(this.buttonAssing_Click);
            // 
            // buttonClose
            // 
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonClose.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Close;
            this.buttonClose.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_close;
            this.buttonClose.Menu = null;
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.button1_Click);
            // 
            // ReassignContractsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ReassignContractsForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.statusStripContacts.ResumeLayout(false);
            this.statusStripContacts.PerformLayout();
            this.gbLoanOffficers.ResumeLayout(false);
            this.gbLoanOffficers.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCaption;
        private SweetButton buttonClose;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox gbLoanOffficers;
        private SweetButton btnAssing;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.ComboBox cbLoanOfficerTo;
        private System.Windows.Forms.ComboBox cbLoanOfficerFrom;
        private System.Windows.Forms.StatusStrip statusStripContacts;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTotal;
        private System.Windows.Forms.CheckBox checkBoxAll;
        private System.Windows.Forms.Label lblContractFilter;
        private System.Windows.Forms.TextBox textBoxContractFilter;
        private System.Windows.Forms.ListView listViewAlert;
        private System.Windows.Forms.ColumnHeader columnHeaderContract;
        private System.Windows.Forms.ColumnHeader columnHeaderDate;
        private System.Windows.Forms.ColumnHeader columnHeaderAmount;
        private System.Windows.Forms.ColumnHeader columnHeaderClient;
        private System.Windows.Forms.ColumnHeader columnHeaderClientDistrict;
        private System.Windows.Forms.ColumnHeader columnHeaderStartDate;
        private System.Windows.Forms.ColumnHeader columnHeaderCloseDate;
        private System.Windows.Forms.ColumnHeader columnHeaderCreateDate;
        private System.Windows.Forms.ColumnHeader columnHeaderInstallmentType;
        private System.Windows.Forms.ColumnHeader columnHeaderInterestRate;
        private System.Windows.Forms.ColumnHeader columnHeaderOLB;
        private System.Windows.Forms.ColumnHeader columnHeaderContractId;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox chkBox_only_active;

    }
}