namespace OpenCBS.GUI.Projets
{
    partial class SearchProjectForm
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
            _theInstance = null;
            
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchProjectForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxButtonBottom = new System.Windows.Forms.GroupBox();
            this.textBoxCurrentlyPage = new System.Windows.Forms.TextBox();
            this.buttonPreview = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.listViewClient = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCode = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderProjectName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderClient = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderAim = new System.Windows.Forms.ColumnHeader();
            this.columnHeadeStatus = new System.Windows.Forms.ColumnHeader();
            this.imageListSort = new System.Windows.Forms.ImageList(this.components);
            this.groupBoxSearchParameters = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.buttonPrintReport = new System.Windows.Forms.Button();
            this.textBoxQuery = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelTitleResult = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxButtonBottom.SuspendLayout();
            this.groupBoxSearchParameters.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.groupBoxButtonBottom, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.listViewClient, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxSearchParameters, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // groupBoxButtonBottom
            //
            resources.ApplyResources(this.groupBoxButtonBottom, "groupBoxButtonBottom");
            this.groupBoxButtonBottom.Controls.Add(this.textBoxCurrentlyPage);
            this.groupBoxButtonBottom.Controls.Add(this.buttonPreview);
            this.groupBoxButtonBottom.Controls.Add(this.buttonNext);
            this.groupBoxButtonBottom.Controls.Add(this.buttonCancel);
            this.groupBoxButtonBottom.Name = "groupBoxButtonBottom";
            this.groupBoxButtonBottom.TabStop = false;
            // 
            // textBoxCurrentlyPage
            // 
            resources.ApplyResources(this.textBoxCurrentlyPage, "textBoxCurrentlyPage");
            this.textBoxCurrentlyPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.textBoxCurrentlyPage.Name = "textBoxCurrentlyPage";
            // 
            // buttonPreview
            // 
            resources.ApplyResources(this.buttonPreview, "buttonPreview");
            this.buttonPreview.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonPreview.Name = "buttonPreview";
            this.buttonPreview.UseVisualStyleBackColor = false;
            this.buttonPreview.Click += new System.EventHandler(this.buttonPreview_Click);
            // 
            // buttonNext
            // 
            resources.ApplyResources(this.buttonNext, "buttonNext");
            this.buttonNext.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.UseVisualStyleBackColor = false;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            // 
            // listViewClient
            // 
            this.listViewClient.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeaderCode,
            this.columnHeaderProjectName,
            this.columnHeaderClient,
            this.columnHeaderAim,
            this.columnHeadeStatus});
            resources.ApplyResources(this.listViewClient, "listViewClient");
            this.listViewClient.FullRowSelect = true;
            this.listViewClient.GridLines = true;
            this.listViewClient.Name = "listViewClient";
            this.listViewClient.SmallImageList = this.imageListSort;
            this.listViewClient.UseCompatibleStateImageBehavior = false;
            this.listViewClient.View = System.Windows.Forms.View.Details;
            this.listViewClient.DoubleClick += new System.EventHandler(this.listViewClient_DoubleClick);
            this.listViewClient.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewClient_ColumnClick);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeaderCode
            // 
            resources.ApplyResources(this.columnHeaderCode, "columnHeaderCode");
            // 
            // columnHeaderProjectName
            // 
            resources.ApplyResources(this.columnHeaderProjectName, "columnHeaderProjectName");
            // 
            // columnHeaderClient
            // 
            resources.ApplyResources(this.columnHeaderClient, "columnHeaderClient");
            // 
            // columnHeaderAim
            // 
            resources.ApplyResources(this.columnHeaderAim, "columnHeaderAim");
            // 
            // columnHeadeStatus
            // 
            resources.ApplyResources(this.columnHeadeStatus, "columnHeadeStatus");
            // 
            // imageListSort
            // 
            this.imageListSort.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSort.ImageStream")));
            this.imageListSort.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListSort.Images.SetKeyName(0, "flecheGD.bmp");
            this.imageListSort.Images.SetKeyName(1, "flecheDG.bmp");
            this.imageListSort.Images.SetKeyName(2, "theme1.1_bouton_down_small.png");
            this.imageListSort.Images.SetKeyName(3, "theme1.1_bouton_up_small.png");
            // 
            // groupBoxSearchParameters
            //
            this.groupBoxSearchParameters.Controls.Add(this.btnSearch);
            this.groupBoxSearchParameters.Controls.Add(this.buttonPrintReport);
            this.groupBoxSearchParameters.Controls.Add(this.textBoxQuery);
            this.groupBoxSearchParameters.Controls.Add(this.buttonSearch);
            resources.ApplyResources(this.groupBoxSearchParameters, "groupBoxSearchParameters");
            this.groupBoxSearchParameters.Name = "groupBoxSearchParameters";
            this.groupBoxSearchParameters.TabStop = false;
            // 
            // btnSearch
            // 
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.BackColor = System.Drawing.Color.Gainsboro;
            this.btnSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // buttonPrintReport
            // 
            resources.ApplyResources(this.buttonPrintReport, "buttonPrintReport");
            this.buttonPrintReport.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonPrintReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonPrintReport.Name = "buttonPrintReport";
            this.buttonPrintReport.UseVisualStyleBackColor = false;
            // 
            // textBoxQuery
            // 
            resources.ApplyResources(this.textBoxQuery, "textBoxQuery");
            this.textBoxQuery.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.textBoxQuery.Name = "textBoxQuery";
            this.textBoxQuery.TextChanged += new System.EventHandler(this.textBoxQuery_TextChanged);
            // 
            // buttonSearch
            // 
            resources.ApplyResources(this.buttonSearch, "buttonSearch");
            this.buttonSearch.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelTitleResult);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // labelTitleResult
            // 
            this.labelTitleResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            resources.ApplyResources(this.labelTitleResult, "labelTitleResult");
            this.labelTitleResult.Name = "labelTitleResult";
            // 
            // SearchProjectForm
            // 
            this.AcceptButton = this.btnSearch;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SearchProjectForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBoxButtonBottom.ResumeLayout(false);
            this.groupBoxButtonBottom.PerformLayout();
            this.groupBoxSearchParameters.ResumeLayout(false);
            this.groupBoxSearchParameters.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBoxSearchParameters;
        private System.Windows.Forms.Button buttonPrintReport;
        private System.Windows.Forms.TextBox textBoxQuery;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListView listViewClient;
        private System.Windows.Forms.ColumnHeader columnHeaderClient;
        private System.Windows.Forms.ColumnHeader columnHeaderAim;
        private System.Windows.Forms.ColumnHeader columnHeadeStatus;
        private System.Windows.Forms.ColumnHeader columnHeaderCode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelTitleResult;
        private System.Windows.Forms.GroupBox groupBoxButtonBottom;
        private System.Windows.Forms.TextBox textBoxCurrentlyPage;
        private System.Windows.Forms.Button buttonPreview;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ColumnHeader columnHeaderProjectName;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ImageList imageListSort;

    }
}