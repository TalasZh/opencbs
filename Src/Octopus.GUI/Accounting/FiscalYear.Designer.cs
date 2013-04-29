namespace Octopus.GUI.Accounting
{
    partial class FiscalYear
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FiscalYear));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.olvYears = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn_Id = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_Name = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_OpenDate = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_EndDate = new BrightIdeasSoftware.OLVColumn();
            this.imageListSort = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.sweetButton1 = new Octopus.GUI.UserControl.SweetButton();
            this.btnClose = new Octopus.GUI.UserControl.SweetButton();
            this.lblTitle = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.sweetButton3 = new Octopus.GUI.UserControl.SweetButton();
            this.sweetButton2 = new Octopus.GUI.UserControl.SweetButton();
            this.btnGenerateEvents = new Octopus.GUI.UserControl.SweetButton();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvYears)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.AccessibleDescription = null;
            this.splitContainer1.AccessibleName = null;
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.BackgroundImage = null;
            this.splitContainer1.Font = null;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AccessibleDescription = null;
            this.splitContainer1.Panel1.AccessibleName = null;
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            this.splitContainer1.Panel1.BackgroundImage = null;
            this.splitContainer1.Panel1.Controls.Add(this.olvYears);
            this.splitContainer1.Panel1.Font = null;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AccessibleDescription = null;
            this.splitContainer1.Panel2.AccessibleName = null;
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            this.splitContainer1.Panel2.BackgroundImage = null;
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Font = null;
            // 
            // olvYears
            // 
            this.olvYears.AccessibleDescription = null;
            this.olvYears.AccessibleName = null;
            resources.ApplyResources(this.olvYears, "olvYears");
            this.olvYears.AllColumns.Add(this.olvColumn_Id);
            this.olvYears.AllColumns.Add(this.olvColumn_Name);
            this.olvYears.AllColumns.Add(this.olvColumn_OpenDate);
            this.olvYears.AllColumns.Add(this.olvColumn_EndDate);
            this.olvYears.BackgroundImage = null;
            this.olvYears.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn_Id,
            this.olvColumn_Name,
            this.olvColumn_OpenDate,
            this.olvColumn_EndDate});
            this.olvYears.EmptyListMsg = null;
            this.olvYears.Font = null;
            this.olvYears.FullRowSelect = true;
            this.olvYears.GridLines = true;
            this.olvYears.GroupWithItemCountFormat = null;
            this.olvYears.GroupWithItemCountSingularFormat = null;
            this.olvYears.HasCollapsibleGroups = false;
            this.olvYears.Name = "olvYears";
            this.olvYears.OverlayText.Text = null;
            this.olvYears.ShowGroups = false;
            this.olvYears.UseCompatibleStateImageBehavior = false;
            this.olvYears.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn_Id
            // 
            this.olvColumn_Id.AspectName = "Id";
            this.olvColumn_Id.GroupWithItemCountFormat = null;
            this.olvColumn_Id.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.olvColumn_Id, "olvColumn_Id");
            this.olvColumn_Id.ToolTipText = null;
            // 
            // olvColumn_Name
            // 
            this.olvColumn_Name.AspectName = "Name";
            this.olvColumn_Name.GroupWithItemCountFormat = null;
            this.olvColumn_Name.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.olvColumn_Name, "olvColumn_Name");
            this.olvColumn_Name.ToolTipText = null;
            // 
            // olvColumn_OpenDate
            // 
            this.olvColumn_OpenDate.AspectName = "OpenDate";
            this.olvColumn_OpenDate.GroupWithItemCountFormat = null;
            this.olvColumn_OpenDate.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.olvColumn_OpenDate, "olvColumn_OpenDate");
            this.olvColumn_OpenDate.ToolTipText = null;
            // 
            // olvColumn_EndDate
            // 
            this.olvColumn_EndDate.AspectName = "CloseDate";
            this.olvColumn_EndDate.GroupWithItemCountFormat = null;
            this.olvColumn_EndDate.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.olvColumn_EndDate, "olvColumn_EndDate");
            this.olvColumn_EndDate.ToolTipText = null;
            // 
            // imageListSort
            // 
            this.imageListSort.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSort.ImageStream")));
            this.imageListSort.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListSort.Images.SetKeyName(0, "theme1.1_bouton_down_small.png");
            this.imageListSort.Images.SetKeyName(1, "theme1.1_bouton_up_small.png");
            // 
            // panel1
            // 
            this.panel1.AccessibleDescription = null;
            this.panel1.AccessibleName = null;
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackgroundImage = null;
            this.panel1.Controls.Add(this.sweetButton1);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Font = null;
            this.panel1.Name = "panel1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AccessibleDescription = null;
            this.tableLayoutPanel1.AccessibleName = null;
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.BackgroundImage = null;
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Font = null;
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // sweetButton1
            // 
            this.sweetButton1.AccessibleDescription = null;
            this.sweetButton1.AccessibleName = null;
            resources.ApplyResources(this.sweetButton1, "sweetButton1");
            this.sweetButton1.BackColor = System.Drawing.Color.Gainsboro;
            this.sweetButton1.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.sweetButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.sweetButton1.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Close;
            this.sweetButton1.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_close;
            this.sweetButton1.Menu = null;
            this.sweetButton1.Name = "sweetButton1";
            this.sweetButton1.UseVisualStyleBackColor = false;
            this.sweetButton1.Click += new System.EventHandler(this.SweetButton1Click);
            // 
            // btnClose
            // 
            this.btnClose.AccessibleDescription = null;
            this.btnClose.AccessibleName = null;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.BackColor = System.Drawing.Color.Gainsboro;
            this.btnClose.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnClose.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Close;
            this.btnClose.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_close;
            this.btnClose.Menu = null;
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AccessibleDescription = null;
            this.lblTitle.AccessibleName = null;
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Image = global::Octopus.GUI.Properties.Resources.theme1_1_pastille_contrat;
            this.lblTitle.Name = "lblTitle";
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = null;
            this.groupBox1.AccessibleName = null;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_gris;
            this.groupBox1.Controls.Add(this.sweetButton3);
            this.groupBox1.Controls.Add(this.sweetButton2);
            this.groupBox1.Controls.Add(this.btnGenerateEvents);
            this.groupBox1.Font = null;
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // sweetButton3
            // 
            this.sweetButton3.AccessibleDescription = null;
            this.sweetButton3.AccessibleName = null;
            resources.ApplyResources(this.sweetButton3, "sweetButton3");
            this.sweetButton3.BackColor = System.Drawing.Color.Gainsboro;
            this.sweetButton3.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.sweetButton3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.sweetButton3.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Edit;
            this.sweetButton3.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_new;
            this.sweetButton3.Menu = null;
            this.sweetButton3.Name = "sweetButton3";
            this.sweetButton3.UseVisualStyleBackColor = false;
            this.sweetButton3.Click += new System.EventHandler(this.SweetButton3Click);
            // 
            // sweetButton2
            // 
            this.sweetButton2.AccessibleDescription = null;
            this.sweetButton2.AccessibleName = null;
            resources.ApplyResources(this.sweetButton2, "sweetButton2");
            this.sweetButton2.BackColor = System.Drawing.Color.Gainsboro;
            this.sweetButton2.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.sweetButton2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.sweetButton2.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Close;
            this.sweetButton2.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_new;
            this.sweetButton2.Menu = null;
            this.sweetButton2.Name = "sweetButton2";
            this.sweetButton2.UseVisualStyleBackColor = false;
            this.sweetButton2.Click += new System.EventHandler(this.SweetButton2Click);
            // 
            // btnGenerateEvents
            // 
            this.btnGenerateEvents.AccessibleDescription = null;
            this.btnGenerateEvents.AccessibleName = null;
            resources.ApplyResources(this.btnGenerateEvents, "btnGenerateEvents");
            this.btnGenerateEvents.BackColor = System.Drawing.Color.Gainsboro;
            this.btnGenerateEvents.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.btnGenerateEvents.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnGenerateEvents.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.New;
            this.btnGenerateEvents.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_new;
            this.btnGenerateEvents.Menu = null;
            this.btnGenerateEvents.Name = "btnGenerateEvents";
            this.btnGenerateEvents.UseVisualStyleBackColor = false;
            this.btnGenerateEvents.Click += new System.EventHandler(this.BtnGenerateEventsClick);
            // 
            // FiscalYear
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = null;
            this.Name = "FiscalYear";
            this.Load += new System.EventHandler(this.FiscalYearLoad);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvYears)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageListSort;
        private BrightIdeasSoftware.ObjectListView olvYears;
        private BrightIdeasSoftware.OLVColumn olvColumn_Id;
        private BrightIdeasSoftware.OLVColumn olvColumn_Name;
        private BrightIdeasSoftware.OLVColumn olvColumn_OpenDate;
        private BrightIdeasSoftware.OLVColumn olvColumn_EndDate;
        private System.Windows.Forms.Panel panel1;
        private Octopus.GUI.UserControl.SweetButton sweetButton1;
        private Octopus.GUI.UserControl.SweetButton btnClose;
        private System.Windows.Forms.Label lblTitle;
        private Octopus.GUI.UserControl.SweetButton btnGenerateEvents;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Octopus.GUI.UserControl.SweetButton sweetButton3;
        private Octopus.GUI.UserControl.SweetButton sweetButton2;
    }
}