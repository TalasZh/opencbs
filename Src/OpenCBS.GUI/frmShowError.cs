// LICENSE PLACEHOLDER

using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Services;
using OpenCBS.Shared;

namespace OpenCBS.GUI
{
	/// <summary>
	/// Summary description for frmShowError.
	/// </summary>
	public class frmShowError : Form
	{
		private Button buttonShowDetail;
		private Button buttonOK;
		private Splitter splitter2;
		private GroupBox groupBox1;
		private Splitter splitter1;
		private Label labelExceptionText;
		private RichTextBox richTextBoxExceptionDetail;
		private ExceptionStatus _expectionStatus;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public frmShowError(ExceptionStatus pExceptionStatus)
		{
			InitializeComponent();
			_expectionStatus = pExceptionStatus;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShowError));
            this.labelExceptionText = new System.Windows.Forms.Label();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.richTextBoxExceptionDetail = new System.Windows.Forms.RichTextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonShowDetail = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelExceptionText
            // 
            resources.ApplyResources(this.labelExceptionText, "labelExceptionText");
            this.labelExceptionText.Name = "labelExceptionText";
            // 
            // splitter2
            // 
            resources.ApplyResources(this.splitter2, "splitter2");
            this.splitter2.Name = "splitter2";
            this.splitter2.TabStop = false;
            // 
            // splitter1
            // 
            resources.ApplyResources(this.splitter1, "splitter1");
            this.splitter1.Name = "splitter1";
            this.splitter1.TabStop = false;
            // 
            // richTextBoxExceptionDetail
            // 
            this.richTextBoxExceptionDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.richTextBoxExceptionDetail, "richTextBoxExceptionDetail");
            this.richTextBoxExceptionDetail.ForeColor = System.Drawing.Color.White;
            this.richTextBoxExceptionDetail.Name = "richTextBoxExceptionDetail";
            // 
            // buttonOK
            // 
            this.buttonOK.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.Image = global::OpenCBS.GUI.Properties.Resources.theme1_1_bouton_validity;
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = false;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // groupBox1
            //
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.buttonShowDetail);
            this.groupBox1.Controls.Add(this.buttonOK);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // buttonShowDetail
            // 
            this.buttonShowDetail.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonShowDetail, "buttonShowDetail");
            this.buttonShowDetail.Image = global::OpenCBS.GUI.Properties.Resources.theme1_1_view;
            this.buttonShowDetail.Name = "buttonShowDetail";
            this.buttonShowDetail.UseVisualStyleBackColor = false;
            this.buttonShowDetail.Click += new System.EventHandler(this.buttonShowDetail_Click);
            // 
            // frmShowError
            // 
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.buttonOK;
            this.ControlBox = false;
            this.Controls.Add(this.richTextBoxExceptionDetail);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.labelExceptionText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmShowError";
            this.Load += new System.EventHandler(this.frmShowError_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private void buttonShowDetail_Click(object sender, EventArgs e)
		{
			if (!(bool) buttonShowDetail.Tag)
			{
				Size = new Size(700, 400);
				buttonShowDetail.Tag = true;
			}
			else
			{
				Size = new Size(700, 137);
				buttonShowDetail.Tag = false;
			}
		}

		private void frmShowError_Load(object sender, EventArgs e)
		{
			Size = new Size(700, 137);
			buttonShowDetail.Tag = false;

            if (_expectionStatus.Ex is OpenCbsException)
            {
                if (((OpenCbsException) _expectionStatus.Ex).AdditionalOptions != null)
                {
                    if (((OpenCbsException) _expectionStatus.Ex).AdditionalOptions.Count > 0)
                    {
                        _expectionStatus.Message = string.Format(_expectionStatus.Message,
                                                                 ((OpenCbsException) _expectionStatus.Ex).
                                                                     AdditionalOptions.
                                                                     ToArray());
                    }
                }
            }

		    labelExceptionText.Text = _expectionStatus.Message;
			richTextBoxExceptionDetail.Text = _expectionStatus.Ex.Message;
			richTextBoxExceptionDetail.Text += @"\n\n" +  _expectionStatus.Ex.StackTrace;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
