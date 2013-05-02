namespace OpenCBS.GUI.UserControl
{
    partial class SavingsListUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SavingsListUserControl));
            this.lvSavings = new System.Windows.Forms.ListView();
            this.columnHeaderSavingCode = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderSavingType = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderSavingDescription = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderSavingBalance = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCurrency = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderSavingCreationDate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderSavingLastActionDate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderSavingStatus = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderSavingCloseDate = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStripSaving = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonAddSaving = new System.Windows.Forms.Button();
            this.buttonViewSaving = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvSavings
            // 
            this.lvSavings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderSavingCode,
            this.columnHeaderSavingType,
            this.columnHeaderSavingDescription,
            this.columnHeaderSavingBalance,
            this.columnHeaderCurrency,
            this.columnHeaderSavingCreationDate,
            this.columnHeaderSavingLastActionDate,
            this.columnHeaderSavingStatus,
            this.columnHeaderSavingCloseDate});
            resources.ApplyResources(this.lvSavings, "lvSavings");
            this.lvSavings.FullRowSelect = true;
            this.lvSavings.GridLines = true;
            this.lvSavings.Name = "lvSavings";
            this.lvSavings.UseCompatibleStateImageBehavior = false;
            this.lvSavings.View = System.Windows.Forms.View.Details;
            this.lvSavings.DoubleClick += new System.EventHandler(this.listViewSavings_DoubleClick);
            // 
            // columnHeaderSavingCode
            // 
            resources.ApplyResources(this.columnHeaderSavingCode, "columnHeaderSavingCode");
            // 
            // columnHeaderSavingType
            // 
            resources.ApplyResources(this.columnHeaderSavingType, "columnHeaderSavingType");
            // 
            // columnHeaderSavingDescription
            // 
            resources.ApplyResources(this.columnHeaderSavingDescription, "columnHeaderSavingDescription");
            // 
            // columnHeaderSavingBalance
            // 
            resources.ApplyResources(this.columnHeaderSavingBalance, "columnHeaderSavingBalance");
            // 
            // columnHeaderCurrency
            // 
            resources.ApplyResources(this.columnHeaderCurrency, "columnHeaderCurrency");
            // 
            // columnHeaderSavingCreationDate
            // 
            resources.ApplyResources(this.columnHeaderSavingCreationDate, "columnHeaderSavingCreationDate");
            // 
            // columnHeaderSavingLastActionDate
            // 
            resources.ApplyResources(this.columnHeaderSavingLastActionDate, "columnHeaderSavingLastActionDate");
            // 
            // columnHeaderSavingStatus
            // 
            resources.ApplyResources(this.columnHeaderSavingStatus, "columnHeaderSavingStatus");
            // 
            // columnHeaderSavingCloseDate
            // 
            resources.ApplyResources(this.columnHeaderSavingCloseDate, "columnHeaderSavingCloseDate");
            // 
            // contextMenuStripSaving
            // 
            this.contextMenuStripSaving.Name = "contextMenuStripSaving";
            resources.ApplyResources(this.contextMenuStripSaving, "contextMenuStripSaving");
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.buttonAddSaving);
            this.flowLayoutPanel1.Controls.Add(this.buttonViewSaving);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // buttonAddSaving
            // 
            resources.ApplyResources(this.buttonAddSaving, "buttonAddSaving");
            this.buttonAddSaving.Name = "buttonAddSaving";
            this.buttonAddSaving.Click += new System.EventHandler(this.buttonAddSaving_Click);
            // 
            // buttonViewSaving
            // 
            resources.ApplyResources(this.buttonViewSaving, "buttonViewSaving");
            this.buttonViewSaving.Name = "buttonViewSaving";
            this.buttonViewSaving.Click += new System.EventHandler(this.buttonViewSaving_Click);
            // 
            // SavingsListUserControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.lvSavings);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "SavingsListUserControl";
            resources.ApplyResources(this, "$this");
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvSavings;
        private System.Windows.Forms.ColumnHeader columnHeaderSavingCode;
        private System.Windows.Forms.ColumnHeader columnHeaderSavingType;
        private System.Windows.Forms.ColumnHeader columnHeaderSavingDescription;
        private System.Windows.Forms.ColumnHeader columnHeaderSavingBalance;
        private System.Windows.Forms.ColumnHeader columnHeaderSavingCreationDate;
        private System.Windows.Forms.ColumnHeader columnHeaderSavingLastActionDate;
        private System.Windows.Forms.ColumnHeader columnHeaderSavingStatus;
        private System.Windows.Forms.ColumnHeader columnHeaderSavingCloseDate;
        private System.Windows.Forms.Button buttonAddSaving;
        private System.Windows.Forms.Button buttonViewSaving;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSaving;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ColumnHeader columnHeaderCurrency;
    }
}
