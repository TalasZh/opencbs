namespace OpenCBS.GUI.Tools
{
    partial class frmSQLTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSQLTool));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.myHighlighterRichTextBox = new SyntaxHighlighter.SyntaxRichTextBox();
            this.richTextBoxResult = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.AccessibleDescription = null;
            this.splitContainer.AccessibleName = null;
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.BackgroundImage = null;
            this.splitContainer.Font = null;
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.AccessibleDescription = null;
            this.splitContainer.Panel1.AccessibleName = null;
            resources.ApplyResources(this.splitContainer.Panel1, "splitContainer.Panel1");
            this.splitContainer.Panel1.BackgroundImage = null;
            this.splitContainer.Panel1.Controls.Add(this.myHighlighterRichTextBox);
            this.splitContainer.Panel1.Font = null;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.AccessibleDescription = null;
            this.splitContainer.Panel2.AccessibleName = null;
            resources.ApplyResources(this.splitContainer.Panel2, "splitContainer.Panel2");
            this.splitContainer.Panel2.BackgroundImage = null;
            this.splitContainer.Panel2.Controls.Add(this.richTextBoxResult);
            this.splitContainer.Panel2.Font = null;
            // 
            // myHighlighterRichTextBox
            // 
            this.myHighlighterRichTextBox.AcceptsTab = true;
            this.myHighlighterRichTextBox.AccessibleDescription = null;
            this.myHighlighterRichTextBox.AccessibleName = null;
            resources.ApplyResources(this.myHighlighterRichTextBox, "myHighlighterRichTextBox");
            this.myHighlighterRichTextBox.BackgroundImage = null;
            this.myHighlighterRichTextBox.Name = "myHighlighterRichTextBox";
            this.myHighlighterRichTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.myHighlighterRichTextBox_KeyDown);
            // 
            // richTextBoxResult
            // 
            this.richTextBoxResult.AccessibleDescription = null;
            this.richTextBoxResult.AccessibleName = null;
            resources.ApplyResources(this.richTextBoxResult, "richTextBoxResult");
            this.richTextBoxResult.BackgroundImage = null;
            this.richTextBoxResult.Font = null;
            this.richTextBoxResult.Name = "richTextBoxResult";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AccessibleDescription = null;
            this.toolStrip1.AccessibleName = null;
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.BackgroundImage = null;
            this.toolStrip1.Font = null;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.AccessibleDescription = null;
            this.toolStripButton1.AccessibleName = null;
            resources.ApplyResources(this.toolStripButton1, "toolStripButton1");
            this.toolStripButton1.BackgroundImage = null;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // frmSQLTool
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.toolStrip1);
            this.Font = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSQLTool";
            this.Load += new System.EventHandler(this.frmSQLTool_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.SplitContainer splitContainer;

        private SyntaxHighlighter.SyntaxRichTextBox myHighlighterRichTextBox;
        private System.Windows.Forms.RichTextBox richTextBoxResult;

    }
}