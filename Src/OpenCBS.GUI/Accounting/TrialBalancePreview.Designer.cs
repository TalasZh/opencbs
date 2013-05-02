namespace OpenCBS.GUI.Accounting
{
    partial class TrialBalancePreview
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrialBalancePreview));
            this.tlvBalances = new BrightIdeasSoftware.TreeListView();
            this.olvColumnLACNumber = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnLACLabel = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnLACBalance = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_CloseBalance = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnLACCurrency = new BrightIdeasSoftware.OLVColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tlvBalances)).BeginInit();
            this.SuspendLayout();
            // 
            // tlvBalances
            // 
            this.tlvBalances.AccessibleDescription = null;
            this.tlvBalances.AccessibleName = null;
            resources.ApplyResources(this.tlvBalances, "tlvBalances");
            this.tlvBalances.AllColumns.Add(this.olvColumnLACNumber);
            this.tlvBalances.AllColumns.Add(this.olvColumnLACLabel);
            this.tlvBalances.AllColumns.Add(this.olvColumnLACBalance);
            this.tlvBalances.AllColumns.Add(this.olvColumn_CloseBalance);
            this.tlvBalances.AllColumns.Add(this.olvColumnLACCurrency);
            this.tlvBalances.BackgroundImage = null;
            this.tlvBalances.CheckBoxes = false;
            this.tlvBalances.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnLACNumber,
            this.olvColumnLACLabel,
            this.olvColumnLACBalance,
            this.olvColumn_CloseBalance,
            this.olvColumnLACCurrency});
            this.tlvBalances.EmptyListMsg = null;
            this.tlvBalances.Font = null;
            this.tlvBalances.GroupWithItemCountSingularFormat = null;
            this.tlvBalances.Name = "tlvBalances";
            this.tlvBalances.OverlayText.Text = null;
            this.tlvBalances.OwnerDraw = true;
            this.tlvBalances.ShowGroups = false;
            this.tlvBalances.UseCompatibleStateImageBehavior = false;
            this.tlvBalances.View = System.Windows.Forms.View.Details;
            this.tlvBalances.VirtualMode = true;
            // 
            // olvColumnLACNumber
            // 
            this.olvColumnLACNumber.AspectName = "Number";
            resources.ApplyResources(this.olvColumnLACNumber, "olvColumnLACNumber");
            this.olvColumnLACNumber.GroupWithItemCountSingularFormat = null;
            this.olvColumnLACNumber.ToolTipText = null;
            // 
            // olvColumnLACLabel
            // 
            this.olvColumnLACLabel.AspectName = "Label";
            this.olvColumnLACLabel.GroupWithItemCountFormat = null;
            this.olvColumnLACLabel.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.olvColumnLACLabel, "olvColumnLACLabel");
            this.olvColumnLACLabel.ToolTipText = null;
            // 
            // olvColumnLACBalance
            // 
            this.olvColumnLACBalance.AspectName = "OpenBalance";
            this.olvColumnLACBalance.GroupWithItemCountFormat = null;
            this.olvColumnLACBalance.GroupWithItemCountSingularFormat = null;
            this.olvColumnLACBalance.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            resources.ApplyResources(this.olvColumnLACBalance, "olvColumnLACBalance");
            this.olvColumnLACBalance.ToolTipText = null;
            // 
            // olvColumn_CloseBalance
            // 
            this.olvColumn_CloseBalance.AspectName = "CloseBalance";
            this.olvColumn_CloseBalance.GroupWithItemCountFormat = null;
            this.olvColumn_CloseBalance.GroupWithItemCountSingularFormat = null;
            this.olvColumn_CloseBalance.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            resources.ApplyResources(this.olvColumn_CloseBalance, "olvColumn_CloseBalance");
            this.olvColumn_CloseBalance.ToolTipText = null;
            // 
            // olvColumnLACCurrency
            // 
            this.olvColumnLACCurrency.AspectName = "CurrencyCode";
            this.olvColumnLACCurrency.GroupWithItemCountFormat = null;
            this.olvColumnLACCurrency.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.olvColumnLACCurrency, "olvColumnLACCurrency");
            this.olvColumnLACCurrency.ToolTipText = null;
            // 
            // TrialBalancePreview
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlvBalances);
            this.Font = null;
            this.Name = "TrialBalancePreview";
            ((System.ComponentModel.ISupportInitialize)(this.tlvBalances)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.TreeListView tlvBalances;
        private BrightIdeasSoftware.OLVColumn olvColumnLACNumber;
        private BrightIdeasSoftware.OLVColumn olvColumnLACLabel;
        private BrightIdeasSoftware.OLVColumn olvColumnLACBalance;
        private BrightIdeasSoftware.OLVColumn olvColumnLACCurrency;
        private BrightIdeasSoftware.OLVColumn olvColumn_CloseBalance;
    }
}