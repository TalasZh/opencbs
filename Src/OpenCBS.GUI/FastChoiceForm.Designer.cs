

namespace OpenCBS.GUI
{
    partial class FastChoiceForm
    {
        private System.ComponentModel.IContainer components;

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


        #region Code généré par le Concepteur Windows Form
        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FastChoiceForm));
            this.generalInfoPanel = new System.Windows.Forms.Panel();
            this.infoPanel = new System.Windows.Forms.Panel();
            this.riskTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.statsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.portfolioPanel = new System.Windows.Forms.Panel();
            this.parPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.activityPanel = new System.Windows.Forms.Panel();
            this.activityListView = new BrightIdeasSoftware.ObjectListView();
            this.activityPefrormedAtColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.activityAction = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.activityLoanOfficerColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.activtyContractCodeColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.activityAmountColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.activityClientColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.activityStreamLabel = new System.Windows.Forms.Label();
            this.quickLinksPanel = new System.Windows.Forms.Panel();
            this.newCorporateClientLinkLabel = new System.Windows.Forms.LinkLabel();
            this.newNonSolidairtyGroupLinkLabel = new System.Windows.Forms.LinkLabel();
            this.newSolidarityGroupLinkLabel = new System.Windows.Forms.LinkLabel();
            this.newIndividualClientLinkLabel = new System.Windows.Forms.LinkLabel();
            this.clientsLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.generalInfoPanel.SuspendLayout();
            this.infoPanel.SuspendLayout();
            this.riskTableLayoutPanel.SuspendLayout();
            this.statsTableLayoutPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.activityPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.activityListView)).BeginInit();
            this.quickLinksPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // generalInfoPanel
            // 
            this.generalInfoPanel.Controls.Add(this.infoPanel);
            this.generalInfoPanel.Controls.Add(this.quickLinksPanel);
            resources.ApplyResources(this.generalInfoPanel, "generalInfoPanel");
            this.generalInfoPanel.Name = "generalInfoPanel";
            // 
            // infoPanel
            // 
            this.infoPanel.BackColor = System.Drawing.Color.White;
            this.infoPanel.Controls.Add(this.riskTableLayoutPanel);
            this.infoPanel.Controls.Add(this.tableLayoutPanel1);
            this.infoPanel.Controls.Add(this.activityPanel);
            this.infoPanel.Controls.Add(this.activityStreamLabel);
            resources.ApplyResources(this.infoPanel, "infoPanel");
            this.infoPanel.Name = "infoPanel";
            // 
            // riskTableLayoutPanel
            // 
            resources.ApplyResources(this.riskTableLayoutPanel, "riskTableLayoutPanel");
            this.riskTableLayoutPanel.Controls.Add(this.statsTableLayoutPanel, 0, 0);
            this.riskTableLayoutPanel.Name = "riskTableLayoutPanel";
            // 
            // statsTableLayoutPanel
            // 
            resources.ApplyResources(this.statsTableLayoutPanel, "statsTableLayoutPanel");
            this.statsTableLayoutPanel.Controls.Add(this.portfolioPanel, 0, 0);
            this.statsTableLayoutPanel.Controls.Add(this.parPanel, 1, 0);
            this.statsTableLayoutPanel.Name = "statsTableLayoutPanel";
            // 
            // portfolioPanel
            // 
            resources.ApplyResources(this.portfolioPanel, "portfolioPanel");
            this.portfolioPanel.Name = "portfolioPanel";
            // 
            // parPanel
            // 
            resources.ApplyResources(this.parPanel, "parPanel");
            this.parPanel.Name = "parPanel";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.label1.Name = "label1";
            // 
            // activityPanel
            // 
            this.activityPanel.Controls.Add(this.activityListView);
            resources.ApplyResources(this.activityPanel, "activityPanel");
            this.activityPanel.Name = "activityPanel";
            // 
            // activityListView
            // 
            this.activityListView.AllColumns.Add(this.activityPefrormedAtColumn);
            this.activityListView.AllColumns.Add(this.activityAction);
            this.activityListView.AllColumns.Add(this.activityLoanOfficerColumn);
            this.activityListView.AllColumns.Add(this.activtyContractCodeColumn);
            this.activityListView.AllColumns.Add(this.activityAmountColumn);
            this.activityListView.AllColumns.Add(this.activityClientColumn);
            this.activityListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.activityPefrormedAtColumn,
            this.activityAction,
            this.activityLoanOfficerColumn,
            this.activtyContractCodeColumn,
            this.activityAmountColumn,
            this.activityClientColumn});
            resources.ApplyResources(this.activityListView, "activityListView");
            this.activityListView.FullRowSelect = true;
            this.activityListView.GridLines = true;
            this.activityListView.HasCollapsibleGroups = false;
            this.activityListView.Name = "activityListView";
            this.activityListView.ShowGroups = false;
            this.activityListView.UseCompatibleStateImageBehavior = false;
            this.activityListView.View = System.Windows.Forms.View.Details;
            // 
            // activityPefrormedAtColumn
            // 
            this.activityPefrormedAtColumn.AspectName = "PerformedAt";
            this.activityPefrormedAtColumn.IsEditable = false;
            resources.ApplyResources(this.activityPefrormedAtColumn, "activityPefrormedAtColumn");
            // 
            // activityAction
            // 
            this.activityAction.AspectName = "Type";
            resources.ApplyResources(this.activityAction, "activityAction");
            // 
            // activityLoanOfficerColumn
            // 
            this.activityLoanOfficerColumn.AspectName = "LoanOfficer";
            this.activityLoanOfficerColumn.IsEditable = false;
            resources.ApplyResources(this.activityLoanOfficerColumn, "activityLoanOfficerColumn");
            // 
            // activtyContractCodeColumn
            // 
            this.activtyContractCodeColumn.AspectName = "ContractCode";
            this.activtyContractCodeColumn.IsEditable = false;
            resources.ApplyResources(this.activtyContractCodeColumn, "activtyContractCodeColumn");
            // 
            // activityAmountColumn
            // 
            this.activityAmountColumn.AspectName = "Amount";
            this.activityAmountColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.activityAmountColumn.IsEditable = false;
            resources.ApplyResources(this.activityAmountColumn, "activityAmountColumn");
            // 
            // activityClientColumn
            // 
            this.activityClientColumn.AspectName = "ClientName";
            resources.ApplyResources(this.activityClientColumn, "activityClientColumn");
            // 
            // activityStreamLabel
            // 
            resources.ApplyResources(this.activityStreamLabel, "activityStreamLabel");
            this.activityStreamLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.activityStreamLabel.Name = "activityStreamLabel";
            // 
            // quickLinksPanel
            // 
            this.quickLinksPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.quickLinksPanel.Controls.Add(this.button1);
            this.quickLinksPanel.Controls.Add(this.newCorporateClientLinkLabel);
            this.quickLinksPanel.Controls.Add(this.newNonSolidairtyGroupLinkLabel);
            this.quickLinksPanel.Controls.Add(this.newSolidarityGroupLinkLabel);
            this.quickLinksPanel.Controls.Add(this.newIndividualClientLinkLabel);
            this.quickLinksPanel.Controls.Add(this.clientsLabel);
            resources.ApplyResources(this.quickLinksPanel, "quickLinksPanel");
            this.quickLinksPanel.Name = "quickLinksPanel";
            // 
            // newCorporateClientLinkLabel
            // 
            this.newCorporateClientLinkLabel.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.newCorporateClientLinkLabel, "newCorporateClientLinkLabel");
            this.newCorporateClientLinkLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
            this.newCorporateClientLinkLabel.Name = "newCorporateClientLinkLabel";
            this.newCorporateClientLinkLabel.TabStop = true;
            this.newCorporateClientLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnCorporateClientLinkLabelLinkClick);
            // 
            // newNonSolidairtyGroupLinkLabel
            // 
            this.newNonSolidairtyGroupLinkLabel.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.newNonSolidairtyGroupLinkLabel, "newNonSolidairtyGroupLinkLabel");
            this.newNonSolidairtyGroupLinkLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
            this.newNonSolidairtyGroupLinkLabel.Name = "newNonSolidairtyGroupLinkLabel";
            this.newNonSolidairtyGroupLinkLabel.TabStop = true;
            this.newNonSolidairtyGroupLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnNewNonSolidairtyGroupLinkLabelLinkClick);
            // 
            // newSolidarityGroupLinkLabel
            // 
            this.newSolidarityGroupLinkLabel.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.newSolidarityGroupLinkLabel, "newSolidarityGroupLinkLabel");
            this.newSolidarityGroupLinkLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
            this.newSolidarityGroupLinkLabel.Name = "newSolidarityGroupLinkLabel";
            this.newSolidarityGroupLinkLabel.TabStop = true;
            this.newSolidarityGroupLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnNewSolidarityGroupLinkLabelLinkClick);
            // 
            // newIndividualClientLinkLabel
            // 
            this.newIndividualClientLinkLabel.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.newIndividualClientLinkLabel, "newIndividualClientLinkLabel");
            this.newIndividualClientLinkLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
            this.newIndividualClientLinkLabel.Name = "newIndividualClientLinkLabel";
            this.newIndividualClientLinkLabel.TabStop = true;
            this.newIndividualClientLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnNewIndividualClientLinkLabelClick);
            // 
            // clientsLabel
            // 
            resources.ApplyResources(this.clientsLabel, "clientsLabel");
            this.clientsLabel.ForeColor = System.Drawing.Color.White;
            this.clientsLabel.Name = "clientsLabel";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FastChoiceForm
            // 
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.generalInfoPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FastChoiceForm";
            this.ShowInTaskbar = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.OnLoad);
            this.generalInfoPanel.ResumeLayout(false);
            this.infoPanel.ResumeLayout(false);
            this.infoPanel.PerformLayout();
            this.riskTableLayoutPanel.ResumeLayout(false);
            this.statsTableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.activityPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.activityListView)).EndInit();
            this.quickLinksPanel.ResumeLayout(false);
            this.quickLinksPanel.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel generalInfoPanel;
        private System.Windows.Forms.Panel quickLinksPanel;
        private System.Windows.Forms.Panel infoPanel;
        private System.Windows.Forms.Label clientsLabel;
        private System.Windows.Forms.LinkLabel newIndividualClientLinkLabel;
        private System.Windows.Forms.LinkLabel newNonSolidairtyGroupLinkLabel;
        private System.Windows.Forms.LinkLabel newSolidarityGroupLinkLabel;
        private System.Windows.Forms.LinkLabel newCorporateClientLinkLabel;
        private System.Windows.Forms.Label activityStreamLabel;
        private BrightIdeasSoftware.ObjectListView activityListView;
        private BrightIdeasSoftware.OLVColumn activtyContractCodeColumn;
        private BrightIdeasSoftware.OLVColumn activityLoanOfficerColumn;
        private BrightIdeasSoftware.OLVColumn activityPefrormedAtColumn;
        private BrightIdeasSoftware.OLVColumn activityAmountColumn;
        private System.Windows.Forms.Panel activityPanel;
        private BrightIdeasSoftware.OLVColumn activityAction;
        private BrightIdeasSoftware.OLVColumn activityClientColumn;
        private System.Windows.Forms.TableLayoutPanel riskTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel statsTableLayoutPanel;
        private System.Windows.Forms.Panel portfolioPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel parPanel;
        private System.Windows.Forms.Button button1;
    }
}
