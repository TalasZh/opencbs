using Octopus.GUI.UserControl;

namespace Octopus.GUI.Accounting
{
    partial class AddBooking
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddBooking));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageEntry = new System.Windows.Forms.TabPage();
            this.tabPageStandardBooking = new System.Windows.Forms.TabPage();
            this.cbCurrencies = new System.Windows.Forms.ComboBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.lblAmmount = new System.Windows.Forms.Label();
            this.lblBooking = new System.Windows.Forms.Label();
            this.textBoxAmount = new System.Windows.Forms.TextBox();
            this.cbBranches = new System.Windows.Forms.ComboBox();
            this.cbBookings = new System.Windows.Forms.ComboBox();
            this.gbPanel = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageStandardBooking.SuspendLayout();
            this.gbPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.AccessibleDescription = null;
            this.splitContainer1.AccessibleName = null;
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.BackgroundImage = null;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Font = null;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AccessibleDescription = null;
            this.splitContainer1.Panel1.AccessibleName = null;
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            this.splitContainer1.Panel1.BackgroundImage = null;
            this.splitContainer1.Panel1.Controls.Add(this.tabControl);
            this.splitContainer1.Panel1.Font = null;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AccessibleDescription = null;
            this.splitContainer1.Panel2.AccessibleName = null;
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            this.splitContainer1.Panel2.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_gris_180;
            this.splitContainer1.Panel2.Controls.Add(this.gbPanel);
            this.splitContainer1.Panel2.Font = null;
            // 
            // tabControl
            // 
            this.tabControl.AccessibleDescription = null;
            this.tabControl.AccessibleName = null;
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.BackgroundImage = null;
            this.tabControl.Controls.Add(this.tabPageEntry);
            this.tabControl.Controls.Add(this.tabPageStandardBooking);
            this.tabControl.Font = null;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabPageEntry
            // 
            this.tabPageEntry.AccessibleDescription = null;
            this.tabPageEntry.AccessibleName = null;
            resources.ApplyResources(this.tabPageEntry, "tabPageEntry");
            this.tabPageEntry.Font = null;
            this.tabPageEntry.Name = "tabPageEntry";
            // 
            // tabPageStandardBooking
            // 
            this.tabPageStandardBooking.AccessibleDescription = null;
            this.tabPageStandardBooking.AccessibleName = null;
            resources.ApplyResources(this.tabPageStandardBooking, "tabPageStandardBooking");
            this.tabPageStandardBooking.Controls.Add(this.cbCurrencies);
            this.tabPageStandardBooking.Controls.Add(this.lblBranch);
            this.tabPageStandardBooking.Controls.Add(this.lblDescription);
            this.tabPageStandardBooking.Controls.Add(this.textBoxDescription);
            this.tabPageStandardBooking.Controls.Add(this.lblCurrency);
            this.tabPageStandardBooking.Controls.Add(this.lblAmmount);
            this.tabPageStandardBooking.Controls.Add(this.lblBooking);
            this.tabPageStandardBooking.Controls.Add(this.textBoxAmount);
            this.tabPageStandardBooking.Controls.Add(this.cbBranches);
            this.tabPageStandardBooking.Controls.Add(this.cbBookings);
            this.tabPageStandardBooking.Font = null;
            this.tabPageStandardBooking.Name = "tabPageStandardBooking";
            // 
            // cbCurrencies
            // 
            this.cbCurrencies.AccessibleDescription = null;
            this.cbCurrencies.AccessibleName = null;
            resources.ApplyResources(this.cbCurrencies, "cbCurrencies");
            this.cbCurrencies.BackgroundImage = null;
            this.cbCurrencies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCurrencies.FormattingEnabled = true;
            this.cbCurrencies.Name = "cbCurrencies";
            // 
            // lblBranch
            // 
            this.lblBranch.AccessibleDescription = null;
            this.lblBranch.AccessibleName = null;
            resources.ApplyResources(this.lblBranch, "lblBranch");
            this.lblBranch.BackColor = System.Drawing.Color.Transparent;
            this.lblBranch.Name = "lblBranch";
            // 
            // lblDescription
            // 
            this.lblDescription.AccessibleDescription = null;
            this.lblDescription.AccessibleName = null;
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblDescription.Name = "lblDescription";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.AccessibleDescription = null;
            this.textBoxDescription.AccessibleName = null;
            resources.ApplyResources(this.textBoxDescription, "textBoxDescription");
            this.textBoxDescription.BackgroundImage = null;
            this.textBoxDescription.Font = null;
            this.textBoxDescription.Name = "textBoxDescription";
            // 
            // lblCurrency
            // 
            this.lblCurrency.AccessibleDescription = null;
            this.lblCurrency.AccessibleName = null;
            resources.ApplyResources(this.lblCurrency, "lblCurrency");
            this.lblCurrency.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrency.Name = "lblCurrency";
            // 
            // lblAmmount
            // 
            this.lblAmmount.AccessibleDescription = null;
            this.lblAmmount.AccessibleName = null;
            resources.ApplyResources(this.lblAmmount, "lblAmmount");
            this.lblAmmount.BackColor = System.Drawing.Color.Transparent;
            this.lblAmmount.Name = "lblAmmount";
            // 
            // lblBooking
            // 
            this.lblBooking.AccessibleDescription = null;
            this.lblBooking.AccessibleName = null;
            resources.ApplyResources(this.lblBooking, "lblBooking");
            this.lblBooking.BackColor = System.Drawing.Color.Transparent;
            this.lblBooking.Name = "lblBooking";
            // 
            // textBoxAmount
            // 
            this.textBoxAmount.AccessibleDescription = null;
            this.textBoxAmount.AccessibleName = null;
            resources.ApplyResources(this.textBoxAmount, "textBoxAmount");
            this.textBoxAmount.BackgroundImage = null;
            this.textBoxAmount.Font = null;
            this.textBoxAmount.Name = "textBoxAmount";
            this.textBoxAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxAmount_KeyPress);
            // 
            // cbBranches
            // 
            this.cbBranches.AccessibleDescription = null;
            this.cbBranches.AccessibleName = null;
            resources.ApplyResources(this.cbBranches, "cbBranches");
            this.cbBranches.BackgroundImage = null;
            this.cbBranches.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranches.Font = null;
            this.cbBranches.FormattingEnabled = true;
            this.cbBranches.Name = "cbBranches";
            // 
            // cbBookings
            // 
            this.cbBookings.AccessibleDescription = null;
            this.cbBookings.AccessibleName = null;
            resources.ApplyResources(this.cbBookings, "cbBookings");
            this.cbBookings.BackgroundImage = null;
            this.cbBookings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBookings.DropDownWidth = 480;
            this.cbBookings.Font = null;
            this.cbBookings.FormattingEnabled = true;
            this.cbBookings.Name = "cbBookings";
            // 
            // gbPanel
            // 
            this.gbPanel.AccessibleDescription = null;
            this.gbPanel.AccessibleName = null;
            resources.ApplyResources(this.gbPanel, "gbPanel");
            this.gbPanel.Controls.Add(this.btnClose);
            this.gbPanel.Controls.Add(this.btnSave);
            this.gbPanel.Font = null;
            this.gbPanel.Name = "gbPanel";
            this.gbPanel.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.AccessibleDescription = null;
            this.btnClose.AccessibleName = null;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleDescription = null;
            this.btnSave.AccessibleName = null;
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // AddBooking
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddBooking";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageStandardBooking.ResumeLayout(false);
            this.tabPageStandardBooking.PerformLayout();
            this.gbPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox gbPanel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageEntry;
        private System.Windows.Forms.TabPage tabPageStandardBooking;
        private System.Windows.Forms.ComboBox cbBookings;
        private System.Windows.Forms.TextBox textBoxAmount;
        private System.Windows.Forms.Label lblBooking;
        private System.Windows.Forms.Label lblAmmount;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblCurrency;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.ComboBox cbCurrencies;
        private System.Windows.Forms.Label lblBranch;
        private System.Windows.Forms.ComboBox cbBranches;
    }
}