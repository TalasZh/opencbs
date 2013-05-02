namespace OpenCBS.GUI.Configuration
{
   partial class FrmFundingLineEvent
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      public System.ComponentModel.ComponentResourceManager resources = null;
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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFundingLineEvent));
          this.splitContainer1 = new System.Windows.Forms.SplitContainer();
          this.groupBoxData = new System.Windows.Forms.GroupBox();
          this.dateTimePickerEvent = new System.Windows.Forms.DateTimePicker();
          this.comboBoxDirection = new System.Windows.Forms.ComboBox();
          this.labelFLEventDate = new System.Windows.Forms.Label();
          this.labelAmount = new System.Windows.Forms.Label();
          this.textBoxAmount = new System.Windows.Forms.TextBox();
          this.labelDirection = new System.Windows.Forms.Label();
          this.textBoxCode = new System.Windows.Forms.TextBox();
          this.labelCode = new System.Windows.Forms.Label();
          this.groupBoxButtonArea = new System.Windows.Forms.GroupBox();
          this.buttonAddRate = new System.Windows.Forms.Button();
          this.buttonCancel = new System.Windows.Forms.Button();
          this.buttonSave = new System.Windows.Forms.Button();
          this.splitContainer1.Panel1.SuspendLayout();
          this.splitContainer1.Panel2.SuspendLayout();
          this.splitContainer1.SuspendLayout();
          this.groupBoxData.SuspendLayout();
          this.groupBoxButtonArea.SuspendLayout();
          this.SuspendLayout();
          // 
          // splitContainer1
          // 
          this.splitContainer1.BackColor = System.Drawing.Color.White;
          this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
          resources.ApplyResources(this.splitContainer1, "splitContainer1");
          this.splitContainer1.Name = "splitContainer1";
          // 
          // splitContainer1.Panel1
          // 
          this.splitContainer1.Panel1.Controls.Add(this.groupBoxData);
          // 
          // splitContainer1.Panel2
          // 
          this.splitContainer1.Panel2.Controls.Add(this.groupBoxButtonArea);
          // 
          // groupBoxData
          //
          resources.ApplyResources(this.groupBoxData, "groupBoxData");
          this.groupBoxData.Controls.Add(this.dateTimePickerEvent);
          this.groupBoxData.Controls.Add(this.comboBoxDirection);
          this.groupBoxData.Controls.Add(this.labelFLEventDate);
          this.groupBoxData.Controls.Add(this.labelAmount);
          this.groupBoxData.Controls.Add(this.textBoxAmount);
          this.groupBoxData.Controls.Add(this.labelDirection);
          this.groupBoxData.Controls.Add(this.textBoxCode);
          this.groupBoxData.Controls.Add(this.labelCode);
          this.groupBoxData.Name = "groupBoxData";
          this.groupBoxData.TabStop = false;
          // 
          // dateTimePickerEvent
          // 
          this.dateTimePickerEvent.Format = System.Windows.Forms.DateTimePickerFormat.Short;
          resources.ApplyResources(this.dateTimePickerEvent, "dateTimePickerEvent");
          this.dateTimePickerEvent.Name = "dateTimePickerEvent";
          this.dateTimePickerEvent.ValueChanged += new System.EventHandler(this.dateTimePickerEvent_ValueChanged);
          // 
          // comboBoxDirection
          // 
          this.comboBoxDirection.DisplayMember = "Key";
          this.comboBoxDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
          this.comboBoxDirection.FormattingEnabled = true;
          this.comboBoxDirection.Items.AddRange(new object[] {
            resources.GetString("comboBoxDirection.Items"),
            resources.GetString("comboBoxDirection.Items1")});
          resources.ApplyResources(this.comboBoxDirection, "comboBoxDirection");
          this.comboBoxDirection.Name = "comboBoxDirection";
          this.comboBoxDirection.ValueMember = "Key";
          // 
          // labelFLEventDate
          // 
          resources.ApplyResources(this.labelFLEventDate, "labelFLEventDate");
          this.labelFLEventDate.BackColor = System.Drawing.Color.Transparent;
          this.labelFLEventDate.Name = "labelFLEventDate";
          // 
          // labelAmount
          // 
          resources.ApplyResources(this.labelAmount, "labelAmount");
          this.labelAmount.BackColor = System.Drawing.Color.Transparent;
          this.labelAmount.Name = "labelAmount";
          // 
          // textBoxAmount
          // 
          resources.ApplyResources(this.textBoxAmount, "textBoxAmount");
          this.textBoxAmount.Name = "textBoxAmount";
          this.textBoxAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxAmount_KeyPress);
          // 
          // labelDirection
          // 
          resources.ApplyResources(this.labelDirection, "labelDirection");
          this.labelDirection.BackColor = System.Drawing.Color.Transparent;
          this.labelDirection.Name = "labelDirection";
          // 
          // textBoxCode
          // 
          resources.ApplyResources(this.textBoxCode, "textBoxCode");
          this.textBoxCode.Name = "textBoxCode";
          // 
          // labelCode
          // 
          resources.ApplyResources(this.labelCode, "labelCode");
          this.labelCode.BackColor = System.Drawing.Color.Transparent;
          this.labelCode.Name = "labelCode";
          // 
          // groupBoxButtonArea
          //
          resources.ApplyResources(this.groupBoxButtonArea, "groupBoxButtonArea");
          this.groupBoxButtonArea.Controls.Add(this.buttonAddRate);
          this.groupBoxButtonArea.Controls.Add(this.buttonCancel);
          this.groupBoxButtonArea.Controls.Add(this.buttonSave);
          this.groupBoxButtonArea.Name = "groupBoxButtonArea";
          this.groupBoxButtonArea.TabStop = false;
          // 
          // buttonAddRate
          //
          resources.ApplyResources(this.buttonAddRate, "buttonAddRate");
          this.buttonAddRate.Name = "buttonAddRate";
          this.buttonAddRate.Click += new System.EventHandler(this.buttonAddRate_Click);
          // 
          // buttonCancel
          // 
          resources.ApplyResources(this.buttonCancel, "buttonCancel");
          this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
          this.buttonCancel.Name = "buttonCancel";
          this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
          // 
          // buttonSave
          // 
          resources.ApplyResources(this.buttonSave, "buttonSave");
          this.buttonSave.Name = "buttonSave";
          this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
          // 
          // FrmFundingLineEvent
          //
          resources.ApplyResources(this, "$this");
          this.Controls.Add(this.splitContainer1);
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
          this.MaximizeBox = false;
          this.MinimizeBox = false;
          this.Name = "FrmFundingLineEvent";
          this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmFundingLineEvent_FormClosing);
          this.splitContainer1.Panel1.ResumeLayout(false);
          this.splitContainer1.Panel2.ResumeLayout(false);
          this.splitContainer1.ResumeLayout(false);
          this.groupBoxData.ResumeLayout(false);
          this.groupBoxData.PerformLayout();
          this.groupBoxButtonArea.ResumeLayout(false);
          this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Label labelCode;
      private System.Windows.Forms.TextBox textBoxCode;
      private System.Windows.Forms.GroupBox groupBoxData;
      private System.Windows.Forms.Label labelDirection;
      private System.Windows.Forms.TextBox textBoxAmount;
      private System.Windows.Forms.Label labelAmount;
      private System.Windows.Forms.ComboBox comboBoxDirection;
      private System.Windows.Forms.GroupBox groupBoxButtonArea;
      private System.Windows.Forms.SplitContainer splitContainer1;
      private System.Windows.Forms.Button buttonSave;
      private System.Windows.Forms.Button buttonCancel;
      private System.Windows.Forms.Label labelFLEventDate;
      private System.Windows.Forms.DateTimePicker dateTimePickerEvent;
      private System.Windows.Forms.Button buttonAddRate;
   }
}
