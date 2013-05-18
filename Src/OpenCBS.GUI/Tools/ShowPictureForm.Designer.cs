namespace OpenCBS.GUI.Tools
{
    partial class ShowPictureForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowPictureForm));
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.addPhotoButton = new System.Windows.Forms.Button();
            this.deletePhotoButton = new System.Windows.Forms.Button();
            this.gpPicture = new System.Windows.Forms.GroupBox();
            this.UserPicture = new System.Windows.Forms.PictureBox();
            this.changePhotoButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelPersonName = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.gpPicture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UserPicture)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            resources.ApplyResources(this.pictureBox, "pictureBox");
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.TabStop = false;
            this.pictureBox.SizeChanged += new System.EventHandler(this.pictureBox_SizeChanged);
            // 
            // addPhotoButton
            // 
            resources.ApplyResources(this.addPhotoButton, "addPhotoButton");
            this.addPhotoButton.Name = "addPhotoButton";
            this.addPhotoButton.UseVisualStyleBackColor = true;
            this.addPhotoButton.Click += new System.EventHandler(this.AddPhotoButtonClick);
            // 
            // deletePhotoButton
            // 
            resources.ApplyResources(this.deletePhotoButton, "deletePhotoButton");
            this.deletePhotoButton.Name = "deletePhotoButton";
            this.deletePhotoButton.UseVisualStyleBackColor = true;
            this.deletePhotoButton.Click += new System.EventHandler(this.deletePhotoButton_Click);
            // 
            // gpPicture
            // 
            this.gpPicture.Controls.Add(this.UserPicture);
            resources.ApplyResources(this.gpPicture, "gpPicture");
            this.gpPicture.Name = "gpPicture";
            this.tableLayoutPanel2.SetRowSpan(this.gpPicture, 5);
            this.gpPicture.TabStop = false;
            // 
            // UserPicture
            // 
            resources.ApplyResources(this.UserPicture, "UserPicture");
            this.UserPicture.Name = "UserPicture";
            this.UserPicture.TabStop = false;
            this.UserPicture.SizeChanged += new System.EventHandler(this.pictureBox_SizeChanged);
            // 
            // changePhotoButton
            // 
            resources.ApplyResources(this.changePhotoButton, "changePhotoButton");
            this.changePhotoButton.Name = "changePhotoButton";
            this.changePhotoButton.UseVisualStyleBackColor = true;
            this.changePhotoButton.Click += new System.EventHandler(this.changePhotoButton_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.labelPersonName);
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Name = "panel1";
            // 
            // labelPersonName
            // 
            resources.ApplyResources(this.labelPersonName, "labelPersonName");
            this.labelPersonName.Name = "labelPersonName";
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            this.closeButton.BackColor = System.Drawing.SystemColors.Control;
            this.closeButton.Name = "closeButton";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(152)))));
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.closeButton, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.gpPicture, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.deletePhotoButton, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.changePhotoButton, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.addPhotoButton, 1, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // ShowPictureForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShowPictureForm";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.ShowPictureForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.gpPicture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UserPicture)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button addPhotoButton;
        private System.Windows.Forms.Button deletePhotoButton;
        private System.Windows.Forms.GroupBox gpPicture;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label labelPersonName;
        private System.Windows.Forms.Button changePhotoButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.PictureBox UserPicture;
    }
}