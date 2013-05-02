namespace OpenCBS.GUI.Accounting
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
            this.olvColumn_Id = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn_Name = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn_OpenDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn_EndDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.sweetButton3 = new System.Windows.Forms.Button();
            this.sweetButton2 = new System.Windows.Forms.Button();
            this.btnGenerateEvents = new System.Windows.Forms.Button();
            this.imageListSort = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.sweetButton1 = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvYears)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.olvYears);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            // 
            // olvYears
            // 
            this.olvYears.AllColumns.Add(this.olvColumn_Id);
            this.olvYears.AllColumns.Add(this.olvColumn_Name);
            this.olvYears.AllColumns.Add(this.olvColumn_OpenDate);
            this.olvYears.AllColumns.Add(this.olvColumn_EndDate);
            this.olvYears.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn_Id,
            this.olvColumn_Name,
            this.olvColumn_OpenDate,
            this.olvColumn_EndDate});
            resources.ApplyResources(this.olvYears, "olvYears");
            this.olvYears.FullRowSelect = true;
            this.olvYears.GridLines = true;
            this.olvYears.HasCollapsibleGroups = false;
            this.olvYears.Name = "olvYears";
            this.olvYears.ShowGroups = false;
            this.olvYears.UseCompatibleStateImageBehavior = false;
            this.olvYears.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn_Id
            // 
            this.olvColumn_Id.AspectName = "Id";
            resources.ApplyResources(this.olvColumn_Id, "olvColumn_Id");
            // 
            // olvColumn_Name
            // 
            this.olvColumn_Name.AspectName = "Name";
            resources.ApplyResources(this.olvColumn_Name, "olvColumn_Name");
            // 
            // olvColumn_OpenDate
            // 
            this.olvColumn_OpenDate.AspectName = "OpenDate";
            resources.ApplyResources(this.olvColumn_OpenDate, "olvColumn_OpenDate");
            // 
            // olvColumn_EndDate
            // 
            this.olvColumn_EndDate.AspectName = "CloseDate";
            resources.ApplyResources(this.olvColumn_EndDate, "olvColumn_EndDate");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.sweetButton3);
            this.groupBox1.Controls.Add(this.sweetButton2);
            this.groupBox1.Controls.Add(this.btnGenerateEvents);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // sweetButton3
            // 
            resources.ApplyResources(this.sweetButton3, "sweetButton3");
            this.sweetButton3.Name = "sweetButton3";
            this.sweetButton3.Click += new System.EventHandler(this.SweetButton3Click);
            // 
            // sweetButton2
            // 
            resources.ApplyResources(this.sweetButton2, "sweetButton2");
            this.sweetButton2.Name = "sweetButton2";
            this.sweetButton2.Click += new System.EventHandler(this.SweetButton2Click);
            // 
            // btnGenerateEvents
            // 
            resources.ApplyResources(this.btnGenerateEvents, "btnGenerateEvents");
            this.btnGenerateEvents.Name = "btnGenerateEvents";
            this.btnGenerateEvents.Click += new System.EventHandler(this.BtnGenerateEventsClick);
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
            this.panel1.Controls.Add(this.sweetButton1);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.lblTitle);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // sweetButton1
            // 
            resources.ApplyResources(this.sweetButton1, "sweetButton1");
            this.sweetButton1.Name = "sweetButton1";
            this.sweetButton1.Click += new System.EventHandler(this.SweetButton1Click);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(152)))));
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // FiscalYear
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FiscalYear";
            this.Load += new System.EventHandler(this.FiscalYearLoad);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvYears)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
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
        private System.Windows.Forms.Button sweetButton1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnGenerateEvents;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button sweetButton3;
        private System.Windows.Forms.Button sweetButton2;
    }
}