namespace OpenCBS.GUI.Export
{
    partial class CustomizableExportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomizableExportForm));
            this.splitContainerSage = new System.Windows.Forms.SplitContainer();
            this.tabControlExportations = new System.Windows.Forms.TabControl();
            this.tabPageInstallments = new System.Windows.Forms.TabPage();
            this.splitContainerAccountTiers = new System.Windows.Forms.SplitContainer();
            this.dateTimePickerEndDateInstallments = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerBeginDateInstallments = new System.Windows.Forms.DateTimePicker();
            this.labelBeginDateInstallments = new System.Windows.Forms.Label();
            this.labelEndDateInstallments = new System.Windows.Forms.Label();
            this.listViewFormatedInstallments = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelSlashInstallments = new System.Windows.Forms.Label();
            this.labelSelectedInstallments = new System.Windows.Forms.Label();
            this.labelTotalInstallments = new System.Windows.Forms.Label();
            this.groupBoxExportAccountsTiers = new System.Windows.Forms.GroupBox();
            this.listViewInstallments = new System.Windows.Forms.ListView();
            this.columnHeaderInstallmentContractCode = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderInstallmentClientName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderInstallmentNumber = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderInstallmentDate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderInstallmentAmount = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tagInstallmentsAsPendingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialogInstallments = new System.Windows.Forms.SaveFileDialog();
            this.openExportFileDialog = new System.Windows.Forms.OpenFileDialog();
            this._buttonExit = new System.Windows.Forms.Button();
            this._labelTitle = new System.Windows.Forms.Label();
            this.buttonRefreshInstallments = new System.Windows.Forms.Button();
            this.btnSelectAllInstallments = new System.Windows.Forms.Button();
            this.btnDeselectAllInstallments = new System.Windows.Forms.Button();
            this.buttonExportAccountTiers = new System.Windows.Forms.Button();
            this.splitContainerSage.Panel1.SuspendLayout();
            this.splitContainerSage.Panel2.SuspendLayout();
            this.splitContainerSage.SuspendLayout();
            this.tabControlExportations.SuspendLayout();
            this.tabPageInstallments.SuspendLayout();
            this.splitContainerAccountTiers.Panel1.SuspendLayout();
            this.splitContainerAccountTiers.Panel2.SuspendLayout();
            this.splitContainerAccountTiers.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxExportAccountsTiers.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerSage
            // 
            this.splitContainerSage.AccessibleDescription = null;
            this.splitContainerSage.AccessibleName = null;
            resources.ApplyResources(this.splitContainerSage, "splitContainerSage");
            this.splitContainerSage.BackgroundImage = null;
            this.splitContainerSage.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerSage.Font = null;
            this.splitContainerSage.Name = "splitContainerSage";
            // 
            // splitContainerSage.Panel1
            // 
            this.splitContainerSage.Panel1.AccessibleDescription = null;
            this.splitContainerSage.Panel1.AccessibleName = null;
            resources.ApplyResources(this.splitContainerSage.Panel1, "splitContainerSage.Panel1");
            this.splitContainerSage.Panel1.BackgroundImage = null;
            this.splitContainerSage.Panel1.Controls.Add(this._buttonExit);
            this.splitContainerSage.Panel1.Controls.Add(this._labelTitle);
            this.splitContainerSage.Panel1.Font = null;
            // 
            // splitContainerSage.Panel2
            // 
            this.splitContainerSage.Panel2.AccessibleDescription = null;
            this.splitContainerSage.Panel2.AccessibleName = null;
            resources.ApplyResources(this.splitContainerSage.Panel2, "splitContainerSage.Panel2");
            this.splitContainerSage.Panel2.BackgroundImage = null;
            this.splitContainerSage.Panel2.Controls.Add(this.tabControlExportations);
            this.splitContainerSage.Panel2.Font = null;
            // 
            // tabControlExportations
            // 
            this.tabControlExportations.AccessibleDescription = null;
            this.tabControlExportations.AccessibleName = null;
            resources.ApplyResources(this.tabControlExportations, "tabControlExportations");
            this.tabControlExportations.BackgroundImage = null;
            this.tabControlExportations.Controls.Add(this.tabPageInstallments);
            this.tabControlExportations.Font = null;
            this.tabControlExportations.Name = "tabControlExportations";
            this.tabControlExportations.SelectedIndex = 0;
            // 
            // tabPageInstallments
            // 
            this.tabPageInstallments.AccessibleDescription = null;
            this.tabPageInstallments.AccessibleName = null;
            resources.ApplyResources(this.tabPageInstallments, "tabPageInstallments");
            this.tabPageInstallments.Controls.Add(this.splitContainerAccountTiers);
            this.tabPageInstallments.Font = null;
            this.tabPageInstallments.Name = "tabPageInstallments";
            // 
            // splitContainerAccountTiers
            // 
            this.splitContainerAccountTiers.AccessibleDescription = null;
            this.splitContainerAccountTiers.AccessibleName = null;
            resources.ApplyResources(this.splitContainerAccountTiers, "splitContainerAccountTiers");
            this.splitContainerAccountTiers.BackgroundImage = null;
            this.splitContainerAccountTiers.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerAccountTiers.Font = null;
            this.splitContainerAccountTiers.Name = "splitContainerAccountTiers";
            // 
            // splitContainerAccountTiers.Panel1
            // 
            this.splitContainerAccountTiers.Panel1.AccessibleDescription = null;
            this.splitContainerAccountTiers.Panel1.AccessibleName = null;
            resources.ApplyResources(this.splitContainerAccountTiers.Panel1, "splitContainerAccountTiers.Panel1");
            this.splitContainerAccountTiers.Panel1.BackgroundImage = null;
            this.splitContainerAccountTiers.Panel1.Controls.Add(this.dateTimePickerEndDateInstallments);
            this.splitContainerAccountTiers.Panel1.Controls.Add(this.dateTimePickerBeginDateInstallments);
            this.splitContainerAccountTiers.Panel1.Controls.Add(this.labelBeginDateInstallments);
            this.splitContainerAccountTiers.Panel1.Controls.Add(this.labelEndDateInstallments);
            this.splitContainerAccountTiers.Panel1.Controls.Add(this.buttonRefreshInstallments);
            this.splitContainerAccountTiers.Panel1.Font = null;
            // 
            // splitContainerAccountTiers.Panel2
            // 
            this.splitContainerAccountTiers.Panel2.AccessibleDescription = null;
            this.splitContainerAccountTiers.Panel2.AccessibleName = null;
            resources.ApplyResources(this.splitContainerAccountTiers.Panel2, "splitContainerAccountTiers.Panel2");
            this.splitContainerAccountTiers.Panel2.BackgroundImage = null;
            this.splitContainerAccountTiers.Panel2.Controls.Add(this.listViewFormatedInstallments);
            this.splitContainerAccountTiers.Panel2.Controls.Add(this.groupBox1);
            this.splitContainerAccountTiers.Panel2.Controls.Add(this.groupBoxExportAccountsTiers);
            this.splitContainerAccountTiers.Panel2.Controls.Add(this.listViewInstallments);
            this.splitContainerAccountTiers.Panel2.Font = null;
            // 
            // dateTimePickerEndDateInstallments
            // 
            this.dateTimePickerEndDateInstallments.AccessibleDescription = null;
            this.dateTimePickerEndDateInstallments.AccessibleName = null;
            resources.ApplyResources(this.dateTimePickerEndDateInstallments, "dateTimePickerEndDateInstallments");
            this.dateTimePickerEndDateInstallments.BackgroundImage = null;
            this.dateTimePickerEndDateInstallments.CalendarFont = null;
            this.dateTimePickerEndDateInstallments.CustomFormat = null;
            this.dateTimePickerEndDateInstallments.Name = "dateTimePickerEndDateInstallments";
            // 
            // dateTimePickerBeginDateInstallments
            // 
            this.dateTimePickerBeginDateInstallments.AccessibleDescription = null;
            this.dateTimePickerBeginDateInstallments.AccessibleName = null;
            resources.ApplyResources(this.dateTimePickerBeginDateInstallments, "dateTimePickerBeginDateInstallments");
            this.dateTimePickerBeginDateInstallments.BackgroundImage = null;
            this.dateTimePickerBeginDateInstallments.CalendarFont = null;
            this.dateTimePickerBeginDateInstallments.CustomFormat = null;
            this.dateTimePickerBeginDateInstallments.Name = "dateTimePickerBeginDateInstallments";
            // 
            // labelBeginDateInstallments
            // 
            this.labelBeginDateInstallments.AccessibleDescription = null;
            this.labelBeginDateInstallments.AccessibleName = null;
            resources.ApplyResources(this.labelBeginDateInstallments, "labelBeginDateInstallments");
            this.labelBeginDateInstallments.Name = "labelBeginDateInstallments";
            // 
            // labelEndDateInstallments
            // 
            this.labelEndDateInstallments.AccessibleDescription = null;
            this.labelEndDateInstallments.AccessibleName = null;
            resources.ApplyResources(this.labelEndDateInstallments, "labelEndDateInstallments");
            this.labelEndDateInstallments.Name = "labelEndDateInstallments";
            // 
            // listViewFormatedInstallments
            // 
            this.listViewFormatedInstallments.AccessibleDescription = null;
            this.listViewFormatedInstallments.AccessibleName = null;
            resources.ApplyResources(this.listViewFormatedInstallments, "listViewFormatedInstallments");
            this.listViewFormatedInstallments.BackgroundImage = null;
            this.listViewFormatedInstallments.Font = null;
            this.listViewFormatedInstallments.FullRowSelect = true;
            this.listViewFormatedInstallments.GridLines = true;
            this.listViewFormatedInstallments.HideSelection = false;
            this.listViewFormatedInstallments.MultiSelect = false;
            this.listViewFormatedInstallments.Name = "listViewFormatedInstallments";
            this.listViewFormatedInstallments.UseCompatibleStateImageBehavior = false;
            this.listViewFormatedInstallments.View = System.Windows.Forms.View.Details;
            this.listViewFormatedInstallments.Leave += new System.EventHandler(this.listViewFormatedInstallments_Leave);
            this.listViewFormatedInstallments.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewFormatedInstallments_ItemSelectionChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = null;
            this.groupBox1.AccessibleName = null;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.labelSlashInstallments);
            this.groupBox1.Controls.Add(this.labelSelectedInstallments);
            this.groupBox1.Controls.Add(this.btnSelectAllInstallments);
            this.groupBox1.Controls.Add(this.labelTotalInstallments);
            this.groupBox1.Controls.Add(this.btnDeselectAllInstallments);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // labelSlashInstallments
            // 
            this.labelSlashInstallments.AccessibleDescription = null;
            this.labelSlashInstallments.AccessibleName = null;
            resources.ApplyResources(this.labelSlashInstallments, "labelSlashInstallments");
            this.labelSlashInstallments.Font = null;
            this.labelSlashInstallments.Name = "labelSlashInstallments";
            // 
            // labelSelectedInstallments
            // 
            this.labelSelectedInstallments.AccessibleDescription = null;
            this.labelSelectedInstallments.AccessibleName = null;
            resources.ApplyResources(this.labelSelectedInstallments, "labelSelectedInstallments");
            this.labelSelectedInstallments.Font = null;
            this.labelSelectedInstallments.Name = "labelSelectedInstallments";
            // 
            // labelTotalInstallments
            // 
            this.labelTotalInstallments.AccessibleDescription = null;
            this.labelTotalInstallments.AccessibleName = null;
            resources.ApplyResources(this.labelTotalInstallments, "labelTotalInstallments");
            this.labelTotalInstallments.Font = null;
            this.labelTotalInstallments.Name = "labelTotalInstallments";
            // 
            // groupBoxExportAccountsTiers
            // 
            this.groupBoxExportAccountsTiers.AccessibleDescription = null;
            this.groupBoxExportAccountsTiers.AccessibleName = null;
            resources.ApplyResources(this.groupBoxExportAccountsTiers, "groupBoxExportAccountsTiers");
            this.groupBoxExportAccountsTiers.Controls.Add(this.buttonExportAccountTiers);
            this.groupBoxExportAccountsTiers.Name = "groupBoxExportAccountsTiers";
            this.groupBoxExportAccountsTiers.TabStop = false;
            // 
            // listViewInstallments
            // 
            this.listViewInstallments.AccessibleDescription = null;
            this.listViewInstallments.AccessibleName = null;
            resources.ApplyResources(this.listViewInstallments, "listViewInstallments");
            this.listViewInstallments.BackgroundImage = null;
            this.listViewInstallments.CheckBoxes = true;
            this.listViewInstallments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderInstallmentContractCode,
            this.columnHeaderInstallmentClientName,
            this.columnHeaderInstallmentNumber,
            this.columnHeaderInstallmentDate,
            this.columnHeaderInstallmentAmount});
            this.listViewInstallments.ContextMenuStrip = this.contextMenuStrip1;
            this.listViewInstallments.Font = null;
            this.listViewInstallments.FullRowSelect = true;
            this.listViewInstallments.GridLines = true;
            this.listViewInstallments.HideSelection = false;
            this.listViewInstallments.MultiSelect = false;
            this.listViewInstallments.Name = "listViewInstallments";
            this.listViewInstallments.UseCompatibleStateImageBehavior = false;
            this.listViewInstallments.View = System.Windows.Forms.View.Details;
            this.listViewInstallments.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewInstallments_ItemChecked);
            this.listViewInstallments.Leave += new System.EventHandler(this.listViewInstallments_Leave);
            this.listViewInstallments.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewInstallments_ItemSelectionChanged);
            // 
            // columnHeaderInstallmentContractCode
            // 
            resources.ApplyResources(this.columnHeaderInstallmentContractCode, "columnHeaderInstallmentContractCode");
            // 
            // columnHeaderInstallmentClientName
            // 
            resources.ApplyResources(this.columnHeaderInstallmentClientName, "columnHeaderInstallmentClientName");
            // 
            // columnHeaderInstallmentNumber
            // 
            resources.ApplyResources(this.columnHeaderInstallmentNumber, "columnHeaderInstallmentNumber");
            // 
            // columnHeaderInstallmentDate
            // 
            resources.ApplyResources(this.columnHeaderInstallmentDate, "columnHeaderInstallmentDate");
            // 
            // columnHeaderInstallmentAmount
            // 
            resources.ApplyResources(this.columnHeaderInstallmentAmount, "columnHeaderInstallmentAmount");
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.AccessibleDescription = null;
            this.contextMenuStrip1.AccessibleName = null;
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.BackgroundImage = null;
            this.contextMenuStrip1.Font = null;
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tagInstallmentsAsPendingToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            // 
            // tagInstallmentsAsPendingToolStripMenuItem
            // 
            this.tagInstallmentsAsPendingToolStripMenuItem.AccessibleDescription = null;
            this.tagInstallmentsAsPendingToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.tagInstallmentsAsPendingToolStripMenuItem, "tagInstallmentsAsPendingToolStripMenuItem");
            this.tagInstallmentsAsPendingToolStripMenuItem.BackgroundImage = null;
            this.tagInstallmentsAsPendingToolStripMenuItem.Name = "tagInstallmentsAsPendingToolStripMenuItem";
            this.tagInstallmentsAsPendingToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.tagInstallmentsAsPendingToolStripMenuItem.Click += new System.EventHandler(this.tagInstallmentsAsPendingToolStripMenuItem_Click);
            // 
            // saveFileDialogInstallments
            // 
            this.saveFileDialogInstallments.DefaultExt = "txt";
            resources.ApplyResources(this.saveFileDialogInstallments, "saveFileDialogInstallments");
            // 
            // openExportFileDialog
            // 
            resources.ApplyResources(this.openExportFileDialog, "openExportFileDialog");
            // 
            // _buttonExit
            // 
            this._buttonExit.AccessibleDescription = null;
            this._buttonExit.AccessibleName = null;
            resources.ApplyResources(this._buttonExit, "_buttonExit");
            this._buttonExit.BackColor = System.Drawing.Color.Gainsboro;
            this._buttonExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this._buttonExit.Name = "_buttonExit";
            this._buttonExit.UseVisualStyleBackColor = false;
            this._buttonExit.Click += new System.EventHandler(this._buttonExit_Click);
            // 
            // _labelTitle
            // 
            this._labelTitle.AccessibleDescription = null;
            this._labelTitle.AccessibleName = null;
            resources.ApplyResources(this._labelTitle, "_labelTitle");
            this._labelTitle.Name = "_labelTitle";
            // 
            // buttonRefreshInstallments
            // 
            this.buttonRefreshInstallments.AccessibleDescription = null;
            this.buttonRefreshInstallments.AccessibleName = null;
            resources.ApplyResources(this.buttonRefreshInstallments, "buttonRefreshInstallments");
            this.buttonRefreshInstallments.Font = null;
            this.buttonRefreshInstallments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonRefreshInstallments.Name = "buttonRefreshInstallments";
            this.buttonRefreshInstallments.UseVisualStyleBackColor = true;
            this.buttonRefreshInstallments.Click += new System.EventHandler(this.buttonRefreshInstallments_Click);
            // 
            // btnSelectAllInstallments
            // 
            this.btnSelectAllInstallments.AccessibleDescription = null;
            this.btnSelectAllInstallments.AccessibleName = null;
            resources.ApplyResources(this.btnSelectAllInstallments, "btnSelectAllInstallments");
            this.btnSelectAllInstallments.Font = null;
            this.btnSelectAllInstallments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnSelectAllInstallments.Name = "btnSelectAllInstallments";
            this.btnSelectAllInstallments.UseVisualStyleBackColor = true;
            this.btnSelectAllInstallments.Click += new System.EventHandler(this.btnSelectAllInstallments_Click);
            // 
            // btnDeselectAllInstallments
            // 
            this.btnDeselectAllInstallments.AccessibleDescription = null;
            this.btnDeselectAllInstallments.AccessibleName = null;
            resources.ApplyResources(this.btnDeselectAllInstallments, "btnDeselectAllInstallments");
            this.btnDeselectAllInstallments.Font = null;
            this.btnDeselectAllInstallments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnDeselectAllInstallments.Name = "btnDeselectAllInstallments";
            this.btnDeselectAllInstallments.UseVisualStyleBackColor = true;
            this.btnDeselectAllInstallments.Click += new System.EventHandler(this.btnDeselectAllInstallments_Click);
            // 
            // buttonExportAccountTiers
            // 
            this.buttonExportAccountTiers.AccessibleDescription = null;
            this.buttonExportAccountTiers.AccessibleName = null;
            resources.ApplyResources(this.buttonExportAccountTiers, "buttonExportAccountTiers");
            this.buttonExportAccountTiers.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonExportAccountTiers.Font = null;
            this.buttonExportAccountTiers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonExportAccountTiers.Name = "buttonExportAccountTiers";
            this.buttonExportAccountTiers.UseVisualStyleBackColor = false;
            this.buttonExportAccountTiers.Click += new System.EventHandler(this.buttonExportAccountTiers_Click);
            // 
            // CustomizableExportForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerSage);
            this.Font = null;
            this.Name = "CustomizableExportForm";
            this.splitContainerSage.Panel1.ResumeLayout(false);
            this.splitContainerSage.Panel2.ResumeLayout(false);
            this.splitContainerSage.ResumeLayout(false);
            this.tabControlExportations.ResumeLayout(false);
            this.tabPageInstallments.ResumeLayout(false);
            this.splitContainerAccountTiers.Panel1.ResumeLayout(false);
            this.splitContainerAccountTiers.Panel1.PerformLayout();
            this.splitContainerAccountTiers.Panel2.ResumeLayout(false);
            this.splitContainerAccountTiers.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxExportAccountsTiers.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerSage;
        private System.Windows.Forms.Button _buttonExit;
        private System.Windows.Forms.Label _labelTitle;
        private System.Windows.Forms.TabControl tabControlExportations;
        private System.Windows.Forms.TabPage tabPageInstallments;
        private System.Windows.Forms.SplitContainer splitContainerAccountTiers;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndDateInstallments;
        private System.Windows.Forms.DateTimePicker dateTimePickerBeginDateInstallments;
        private System.Windows.Forms.Label labelBeginDateInstallments;
        private System.Windows.Forms.Label labelEndDateInstallments;
        private System.Windows.Forms.Button buttonRefreshInstallments;
        private System.Windows.Forms.GroupBox groupBoxExportAccountsTiers;
        private System.Windows.Forms.Label labelSlashInstallments;
        private System.Windows.Forms.Label labelSelectedInstallments;
        private System.Windows.Forms.Label labelTotalInstallments;
        private System.Windows.Forms.Button btnDeselectAllInstallments;
        private System.Windows.Forms.Button btnSelectAllInstallments;
        private System.Windows.Forms.Button buttonExportAccountTiers;
        private System.Windows.Forms.ListView listViewInstallments;
        private System.Windows.Forms.ColumnHeader columnHeaderInstallmentContractCode;
        private System.Windows.Forms.ColumnHeader columnHeaderInstallmentClientName;
        private System.Windows.Forms.SaveFileDialog saveFileDialogInstallments;
        private System.Windows.Forms.ListView listViewFormatedInstallments;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ColumnHeader columnHeaderInstallmentNumber;
        private System.Windows.Forms.ColumnHeader columnHeaderInstallmentDate;
        private System.Windows.Forms.ColumnHeader columnHeaderInstallmentAmount;
        private System.Windows.Forms.OpenFileDialog openExportFileDialog;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tagInstallmentsAsPendingToolStripMenuItem;
    }
}