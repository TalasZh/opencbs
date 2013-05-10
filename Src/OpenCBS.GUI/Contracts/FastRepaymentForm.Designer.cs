using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Contracts
{
    partial class FastRepaymentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FastRepaymentForm));
            this.tbTotal = new System.Windows.Forms.TextBox();
            this.pnlButton = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbItem = new System.Windows.Forms.ComboBox();
            this.lvContracts = new OpenCBS.GUI.UserControl.ListViewEx();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.colContract = new System.Windows.Forms.ColumnHeader();
            this.colPrincipal = new System.Windows.Forms.ColumnHeader();
            this.colInterest = new System.Windows.Forms.ColumnHeader();
            this.colPenalties = new System.Windows.Forms.ColumnHeader();
            this.colTotal = new System.Windows.Forms.ColumnHeader();
            this.colOLB = new System.Windows.Forms.ColumnHeader();
            this.colDueInterest = new System.Windows.Forms.ColumnHeader();
            this.colCurrency = new System.Windows.Forms.ColumnHeader();
            this.colPaymentOption = new System.Windows.Forms.ColumnHeader();
            this.colComment = new System.Windows.Forms.ColumnHeader();
            this.pnlButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbTotal
            // 
            resources.ApplyResources(this.tbTotal, "tbTotal");
            this.tbTotal.Name = "tbTotal";
            // 
            // pnlButton
            // 
            resources.ApplyResources(this.pnlButton, "pnlButton");
            this.pnlButton.Controls.Add(this.btnOK);
            this.pnlButton.Controls.Add(this.btnCancel);
            this.pnlButton.Name = "pnlButton";
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new System.EventHandler(this.BtnOkClick);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            // 
            // cbItem
            // 
            this.cbItem.FormattingEnabled = true;
            this.cbItem.Items.AddRange(new object[] {
            resources.GetString("cbItem.Items"),
            resources.GetString("cbItem.Items1"),
            resources.GetString("cbItem.Items2")});
            resources.ApplyResources(this.cbItem, "cbItem");
            this.cbItem.Name = "cbItem";
            // 
            // lvContracts
            // 
            this.lvContracts.CheckBoxes = true;
            this.lvContracts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colContract,
            this.colPrincipal,
            this.colInterest,
            this.colPenalties,
            this.colTotal,
            this.colOLB,
            this.colDueInterest,
            this.colCurrency,
            this.colPaymentOption,
            this.colComment});
            resources.ApplyResources(this.lvContracts, "lvContracts");
            this.lvContracts.DoubleClickActivation = true;
            this.lvContracts.FullRowSelect = true;
            this.lvContracts.GridLines = true;
            this.lvContracts.MultiSelect = false;
            this.lvContracts.Name = "lvContracts";
            this.lvContracts.UseCompatibleStateImageBehavior = false;
            this.lvContracts.View = System.Windows.Forms.View.Details;
            this.lvContracts.SubItemClicked += new OpenCBS.GUI.UserControl.SubItemEventHandler(this.lvContracts_SubItemClicked);
            this.lvContracts.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvContracts_ItemChecked);
            this.lvContracts.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvContracts_ItemCheck);
            this.lvContracts.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvContracts_MouseDown);
            this.lvContracts.SubItemEndEditing += new OpenCBS.GUI.UserControl.SubItemEndEditingEventHandler(this.lvContracts_SubItemEndEditing);
            // 
            // colName
            // 
            resources.ApplyResources(this.colName, "colName");
            // 
            // colContract
            // 
            resources.ApplyResources(this.colContract, "colContract");
            // 
            // colPrincipal
            // 
            resources.ApplyResources(this.colPrincipal, "colPrincipal");
            // 
            // colInterest
            // 
            resources.ApplyResources(this.colInterest, "colInterest");
            // 
            // colPenalties
            // 
            resources.ApplyResources(this.colPenalties, "colPenalties");
            // 
            // colTotal
            // 
            resources.ApplyResources(this.colTotal, "colTotal");
            // 
            // colOLB
            // 
            resources.ApplyResources(this.colOLB, "colOLB");
            // 
            // colDueInterest
            // 
            resources.ApplyResources(this.colDueInterest, "colDueInterest");
            // 
            // colCurrency
            // 
            resources.ApplyResources(this.colCurrency, "colCurrency");
            // 
            // colPaymentOption
            // 
            resources.ApplyResources(this.colPaymentOption, "colPaymentOption");
            // 
            // colComment
            // 
            resources.ApplyResources(this.colComment, "colComment");
            // 
            // FastRepaymentForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbItem);
            this.Controls.Add(this.tbTotal);
            this.Controls.Add(this.lvContracts);
            this.Controls.Add(this.pnlButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FastRepaymentForm";
            this.pnlButton.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlButton;
        private ListViewEx lvContracts;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colContract;
        private System.Windows.Forms.ColumnHeader colPrincipal;
        private System.Windows.Forms.ColumnHeader colInterest;
        private System.Windows.Forms.ColumnHeader colPenalties;
        private System.Windows.Forms.ColumnHeader colTotal;
        private System.Windows.Forms.TextBox tbTotal;
        private System.Windows.Forms.ColumnHeader colOLB;
        private System.Windows.Forms.ColumnHeader colCurrency;
        private System.Windows.Forms.ComboBox cbItem;
        private System.Windows.Forms.ColumnHeader colPaymentOption;
        private System.Windows.Forms.ColumnHeader colDueInterest;
        private System.Windows.Forms.ColumnHeader colComment;
    }
}