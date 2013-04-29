namespace Octopus.GUI
{
    using Octopus.GUI.UserControl;

    partial class FrmLocations
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLocations));
            this.buttonExit = new Octopus.GUI.UserControl.SweetButton();
            this.buttonUpdate = new Octopus.GUI.UserControl.SweetButton();
            this.buttonDelete = new Octopus.GUI.UserControl.SweetButton();
            this.tbName = new System.Windows.Forms.TextBox();
            this.buttonAdd = new Octopus.GUI.UserControl.SweetButton();
            this.treeViewLocations = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblType = new System.Windows.Forms.Label();
            this.lblAddType = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonExit, "buttonExit");
            this.buttonExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonExit.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.None;
            this.buttonExit.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_close;
            this.buttonExit.Menu = null;
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonUpdate, "buttonUpdate");
            this.buttonUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonUpdate.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Edit;
            this.buttonUpdate.Menu = null;
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.UseVisualStyleBackColor = false;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonDelete, "buttonDelete");
            this.buttonDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonDelete.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Delete;
            this.buttonDelete.Menu = null;
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // tbName
            // 
            resources.ApplyResources(this.tbName, "tbName");
            this.tbName.Name = "tbName";
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonAdd, "buttonAdd");
            this.buttonAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonAdd.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.New;
            this.buttonAdd.Menu = null;
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // treeViewLocations
            // 
            this.treeViewLocations.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            resources.ApplyResources(this.treeViewLocations, "treeViewLocations");
            this.treeViewLocations.Name = "treeViewLocations";
            this.treeViewLocations.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewLocations_AfterSelect);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblType);
            this.groupBox1.Controls.Add(this.buttonUpdate);
            this.groupBox1.Controls.Add(this.lblAddType);
            this.groupBox1.Controls.Add(this.tbName);
            this.groupBox1.Controls.Add(this.buttonAdd);
            this.groupBox1.Controls.Add(this.buttonDelete);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(56)))));
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // lblType
            // 
            resources.ApplyResources(this.lblType, "lblType");
            this.lblType.BackColor = System.Drawing.Color.Transparent;
            this.lblType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lblType.Name = "lblType";
            // 
            // lblAddType
            // 
            resources.ApplyResources(this.lblAddType, "lblAddType");
            this.lblAddType.BackColor = System.Drawing.Color.Transparent;
            this.lblAddType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lblAddType.Name = "lblAddType";
            // 
            // FrmLocations
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.treeViewLocations);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLocations";
            this.Load += new System.EventHandler(this.FrmProvinces_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SweetButton buttonExit;
        private SweetButton buttonAdd;
        private System.Windows.Forms.TreeView treeViewLocations;
        private System.Windows.Forms.TextBox tbName;
        private SweetButton buttonUpdate;
        private SweetButton buttonDelete;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblAddType;
        private System.Windows.Forms.Label lblType;
    }
}