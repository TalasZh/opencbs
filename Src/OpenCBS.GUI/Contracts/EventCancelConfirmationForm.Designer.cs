using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI
{
    partial class EventCancelConfirmationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventCancelConfirmationForm));
            this.listViewRepayments = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.lblComeBackToState = new System.Windows.Forms.Label();
            this.cbShowCurrentState = new System.Windows.Forms.CheckBox();
            this.lblConfirmEventDelete = new System.Windows.Forms.Label();
            this.listViewEvents = new System.Windows.Forms.ListView();
            this.columnHeaderDate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderType = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderPrincipal = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderInterest = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderFees = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.textBoxComments = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listViewRepayments
            // 
            this.listViewRepayments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            resources.ApplyResources(this.listViewRepayments, "listViewRepayments");
            this.listViewRepayments.FullRowSelect = true;
            this.listViewRepayments.GridLines = true;
            this.listViewRepayments.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
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
            // buttonCancel
            //
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            // 
            // buttonSave
            //
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // lblComeBackToState
            // 
            resources.ApplyResources(this.lblComeBackToState, "lblComeBackToState");
            this.lblComeBackToState.Name = "lblComeBackToState";
            // 
            // cbShowCurrentState
            // 
            resources.ApplyResources(this.cbShowCurrentState, "cbShowCurrentState");
            this.cbShowCurrentState.Name = "cbShowCurrentState";
            this.cbShowCurrentState.CheckedChanged += new System.EventHandler(this.cbShowCurrentState_CheckedChanged);
            // 
            // lblConfirmEventDelete
            // 
            resources.ApplyResources(this.lblConfirmEventDelete, "lblConfirmEventDelete");
            this.lblConfirmEventDelete.Name = "lblConfirmEventDelete";
            // 
            // listViewEvents
            // 
            this.listViewEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderDate,
            this.columnHeaderType,
            this.columnHeaderPrincipal,
            this.columnHeaderInterest,
            this.columnHeaderFees,
            this.columnHeader10});
            this.listViewEvents.FullRowSelect = true;
            this.listViewEvents.GridLines = true;
            resources.ApplyResources(this.listViewEvents, "listViewEvents");
            this.listViewEvents.Name = "listViewEvents";
            this.listViewEvents.UseCompatibleStateImageBehavior = false;
            this.listViewEvents.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderDate
            // 
            resources.ApplyResources(this.columnHeaderDate, "columnHeaderDate");
            // 
            // columnHeaderType
            // 
            resources.ApplyResources(this.columnHeaderType, "columnHeaderType");
            // 
            // columnHeaderPrincipal
            // 
            resources.ApplyResources(this.columnHeaderPrincipal, "columnHeaderPrincipal");
            // 
            // columnHeaderInterest
            // 
            resources.ApplyResources(this.columnHeaderInterest, "columnHeaderInterest");
            // 
            // columnHeaderFees
            // 
            resources.ApplyResources(this.columnHeaderFees, "columnHeaderFees");
            // 
            // columnHeader10
            // 
            resources.ApplyResources(this.columnHeader10, "columnHeader10");
            // 
            // textBoxComments
            // 
            resources.ApplyResources(this.textBoxComments, "textBoxComments");
            this.textBoxComments.Name = "textBoxComments";
            this.textBoxComments.TextChanged += new System.EventHandler(this.textBoxComments_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // EventCancelConfirmationForm
            // 
            this.AcceptButton = this.buttonSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxComments);
            this.Controls.Add(this.lblConfirmEventDelete);
            this.Controls.Add(this.cbShowCurrentState);
            this.Controls.Add(this.lblComeBackToState);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.listViewEvents);
            this.Controls.Add(this.listViewRepayments);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EventCancelConfirmationForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewRepayments;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label lblComeBackToState;
        private System.Windows.Forms.CheckBox cbShowCurrentState;
        private System.Windows.Forms.Label lblConfirmEventDelete;
        private System.Windows.Forms.ListView listViewEvents;
        private System.Windows.Forms.ColumnHeader columnHeaderDate;
        private System.Windows.Forms.ColumnHeader columnHeaderType;
        private System.Windows.Forms.ColumnHeader columnHeaderPrincipal;
        private System.Windows.Forms.ColumnHeader columnHeaderInterest;
        private System.Windows.Forms.ColumnHeader columnHeaderFees;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.TextBox textBoxComments;
        private System.Windows.Forms.Label label1;
    }
}