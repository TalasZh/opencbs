using Octopus.GUI.UserControl;

namespace Octopus.GUI.Accounting
{
    partial class StandardBooking
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StandardBooking));
            this.listBookings = new System.Windows.Forms.ListView();
            this.columnName = new System.Windows.Forms.ColumnHeader();
            this.columnDebit = new System.Windows.Forms.ColumnHeader();
            this.columnCredit = new System.Windows.Forms.ColumnHeader();
            this.gbBooking = new System.Windows.Forms.GroupBox();
            this.btnDelete = new Octopus.GUI.UserControl.SweetButton();
            this.btnEdit = new Octopus.GUI.UserControl.SweetButton();
            this.btnCreate = new Octopus.GUI.UserControl.SweetButton();
            this.lblCaption = new System.Windows.Forms.Label();
            this.buttonClose = new Octopus.GUI.UserControl.SweetButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.gbBooking.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBookings
            // 
            this.listBookings.AccessibleDescription = null;
            this.listBookings.AccessibleName = null;
            resources.ApplyResources(this.listBookings, "listBookings");
            this.listBookings.BackColor = System.Drawing.SystemColors.Window;
            this.listBookings.BackgroundImage = null;
            this.listBookings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBookings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnDebit,
            this.columnCredit});
            this.listBookings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.listBookings.FullRowSelect = true;
            this.listBookings.GridLines = true;
            this.listBookings.HideSelection = false;
            this.listBookings.Name = "listBookings";
            this.listBookings.UseCompatibleStateImageBehavior = false;
            this.listBookings.View = System.Windows.Forms.View.Details;
            // 
            // columnName
            // 
            resources.ApplyResources(this.columnName, "columnName");
            // 
            // columnDebit
            // 
            resources.ApplyResources(this.columnDebit, "columnDebit");
            // 
            // columnCredit
            // 
            resources.ApplyResources(this.columnCredit, "columnCredit");
            // 
            // gbBooking
            // 
            this.gbBooking.AccessibleDescription = null;
            this.gbBooking.AccessibleName = null;
            resources.ApplyResources(this.gbBooking, "gbBooking");
            this.gbBooking.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_gris_180;
            this.gbBooking.Controls.Add(this.btnDelete);
            this.gbBooking.Controls.Add(this.btnEdit);
            this.gbBooking.Controls.Add(this.btnCreate);
            this.gbBooking.Font = null;
            this.gbBooking.Name = "gbBooking";
            this.gbBooking.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleDescription = null;
            this.btnDelete.AccessibleName = null;
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.BackColor = System.Drawing.Color.Gainsboro;
            this.btnDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnDelete.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Delete;
            this.btnDelete.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_delete;
            this.btnDelete.Menu = null;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.AccessibleDescription = null;
            this.btnEdit.AccessibleName = null;
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.BackColor = System.Drawing.Color.Gainsboro;
            this.btnEdit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnEdit.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.None;
            this.btnEdit.Image = global::Octopus.GUI.Properties.Resources.theme1_1_view;
            this.btnEdit.Menu = null;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.AccessibleDescription = null;
            this.btnCreate.AccessibleName = null;
            resources.ApplyResources(this.btnCreate, "btnCreate");
            this.btnCreate.BackColor = System.Drawing.Color.Gainsboro;
            this.btnCreate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnCreate.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.New;
            this.btnCreate.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_new;
            this.btnCreate.Menu = null;
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // lblCaption
            // 
            this.lblCaption.AccessibleDescription = null;
            this.lblCaption.AccessibleName = null;
            resources.ApplyResources(this.lblCaption, "lblCaption");
            this.lblCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblCaption.ForeColor = System.Drawing.Color.White;
            this.lblCaption.Name = "lblCaption";
            // 
            // buttonClose
            // 
            this.buttonClose.AccessibleDescription = null;
            this.buttonClose.AccessibleName = null;
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonClose.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Close;
            this.buttonClose.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_close;
            this.buttonClose.Menu = null;
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AccessibleDescription = null;
            this.tableLayoutPanel1.AccessibleName = null;
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.BackgroundImage = null;
            this.tableLayoutPanel1.Controls.Add(this.gbBooking, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.listBookings, 0, 0);
            this.tableLayoutPanel1.Font = null;
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AccessibleDescription = null;
            this.tableLayoutPanel2.AccessibleName = null;
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.tableLayoutPanel2.BackgroundImage = null;
            this.tableLayoutPanel2.Controls.Add(this.lblCaption, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonClose, 1, 0);
            this.tableLayoutPanel2.Font = null;
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // StandardBooking
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Font = null;
            this.Name = "StandardBooking";
            this.gbBooking.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCaption;
        private SweetButton buttonClose;
        private System.Windows.Forms.ListView listBookings;
        private System.Windows.Forms.GroupBox gbBooking;
        private SweetButton btnCreate;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnDebit;
        private System.Windows.Forms.ColumnHeader columnCredit;
        private SweetButton btnDelete;
        private SweetButton btnEdit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}