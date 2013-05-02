namespace OpenCBS.GUI.Accounting
{
    partial class ClosureBookings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClosureBookings));
            this.olvBookings = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn_Id = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_Date = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_Amount = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_DebitAccount = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_CreditAccount = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_EventId = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_EventType = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_Currency = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_ExchangeRate = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_Description = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_Branch = new BrightIdeasSoftware.OLVColumn();
            ((System.ComponentModel.ISupportInitialize)(this.olvBookings)).BeginInit();
            this.SuspendLayout();
            // 
            // olvBookings
            // 
            this.olvBookings.AllColumns.Add(this.olvColumn_Id);
            this.olvBookings.AllColumns.Add(this.olvColumn_Date);
            this.olvBookings.AllColumns.Add(this.olvColumn_Amount);
            this.olvBookings.AllColumns.Add(this.olvColumn_DebitAccount);
            this.olvBookings.AllColumns.Add(this.olvColumn_CreditAccount);
            this.olvBookings.AllColumns.Add(this.olvColumn_EventId);
            this.olvBookings.AllColumns.Add(this.olvColumn_EventType);
            this.olvBookings.AllColumns.Add(this.olvColumn_Currency);
            this.olvBookings.AllColumns.Add(this.olvColumn_ExchangeRate);
            this.olvBookings.AllColumns.Add(this.olvColumn_Description);
            this.olvBookings.AllColumns.Add(this.olvColumn_Branch);
            this.olvBookings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn_Id,
            this.olvColumn_Date,
            this.olvColumn_Amount,
            this.olvColumn_DebitAccount,
            this.olvColumn_CreditAccount,
            this.olvColumn_EventId,
            this.olvColumn_EventType,
            this.olvColumn_Currency,
            this.olvColumn_ExchangeRate,
            this.olvColumn_Description,
            this.olvColumn_Branch});
            this.olvBookings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.olvBookings.FullRowSelect = true;
            this.olvBookings.GridLines = true;
            this.olvBookings.HasCollapsibleGroups = false;
            this.olvBookings.Location = new System.Drawing.Point(0, 0);
            this.olvBookings.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.olvBookings.Name = "olvBookings";
            this.olvBookings.ShowGroups = false;
            this.olvBookings.Size = new System.Drawing.Size(879, 300);
            this.olvBookings.TabIndex = 45;
            this.olvBookings.UseCompatibleStateImageBehavior = false;
            this.olvBookings.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn_Id
            // 
            this.olvColumn_Id.AspectName = "Id";
            this.olvColumn_Id.Text = "Id";
            this.olvColumn_Id.Width = 61;
            // 
            // olvColumn_Date
            // 
            this.olvColumn_Date.AspectName = "Date";
            this.olvColumn_Date.Text = "Date";
            this.olvColumn_Date.Width = 97;
            // 
            // olvColumn_Amount
            // 
            this.olvColumn_Amount.AspectName = "Amount";
            this.olvColumn_Amount.Text = "Amount";
            this.olvColumn_Amount.Width = 80;
            // 
            // olvColumn_DebitAccount
            // 
            this.olvColumn_DebitAccount.AspectName = "DebitAccount";
            this.olvColumn_DebitAccount.Text = "Debit Account";
            this.olvColumn_DebitAccount.Width = 80;
            // 
            // olvColumn_CreditAccount
            // 
            this.olvColumn_CreditAccount.AspectName = "CreditAccount";
            this.olvColumn_CreditAccount.Text = "Credit Account";
            this.olvColumn_CreditAccount.Width = 80;
            // 
            // olvColumn_EventId
            // 
            this.olvColumn_EventId.AspectName = "EventId";
            this.olvColumn_EventId.Text = "Event Id";
            this.olvColumn_EventId.Width = 80;
            // 
            // olvColumn_EventType
            // 
            this.olvColumn_EventType.AspectName = "EventType";
            this.olvColumn_EventType.Text = "Event Type";
            this.olvColumn_EventType.Width = 80;
            // 
            // olvColumn_Currency
            // 
            this.olvColumn_Currency.AspectName = "Currency";
            this.olvColumn_Currency.Text = "Currency";
            this.olvColumn_Currency.Width = 80;
            // 
            // olvColumn_ExchangeRate
            // 
            this.olvColumn_ExchangeRate.AspectName = "ExchangeRate";
            this.olvColumn_ExchangeRate.Text = "Exchange Rate";
            this.olvColumn_ExchangeRate.Width = 80;
            // 
            // olvColumn_Description
            // 
            this.olvColumn_Description.AspectName = "Description";
            this.olvColumn_Description.Text = "Description";
            this.olvColumn_Description.Width = 80;
            // 
            // olvColumn_Branch
            // 
            this.olvColumn_Branch.AspectName = "Branch";
            this.olvColumn_Branch.Text = "Branch";
            this.olvColumn_Branch.Width = 80;
            // 
            // ClosureBookings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 300);
            this.Controls.Add(this.olvBookings);
            this.Name = "ClosureBookings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bookings";
            ((System.ComponentModel.ISupportInitialize)(this.olvBookings)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView olvBookings;
        private BrightIdeasSoftware.OLVColumn olvColumn_Id;
        private BrightIdeasSoftware.OLVColumn olvColumn_Date;
        private BrightIdeasSoftware.OLVColumn olvColumn_Amount;
        private BrightIdeasSoftware.OLVColumn olvColumn_DebitAccount;
        private BrightIdeasSoftware.OLVColumn olvColumn_CreditAccount;
        private BrightIdeasSoftware.OLVColumn olvColumn_EventId;
        private BrightIdeasSoftware.OLVColumn olvColumn_EventType;
        private BrightIdeasSoftware.OLVColumn olvColumn_Currency;
        private BrightIdeasSoftware.OLVColumn olvColumn_ExchangeRate;
        private BrightIdeasSoftware.OLVColumn olvColumn_Description;
        private BrightIdeasSoftware.OLVColumn olvColumn_Branch;
    }
}