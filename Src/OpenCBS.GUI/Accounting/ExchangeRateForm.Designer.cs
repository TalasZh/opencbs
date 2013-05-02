using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using OpenCBS.GUI.UserControl;
using ZedGraph;

namespace OpenCBS.GUI.Accounting
{
    public partial class ExchangeRateForm
    {
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private GroupBox groupBox2;
        private Label labelInternalCurrency;
        private TextDecimalNumericUserControl textBoxRateValue;
        private Label labelExternalCurrency;
        private Label lbRateDetails;
        private ListView lvExchangeRate;
        private ColumnHeader chDate;
        private MonthCalendar mCRate;
        private ZedGraphControl zedGraphControlExchangeRateEvolution;
        private Label lbRateEvolution;

        private System.ComponentModel.Container components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExchangeRateForm));
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelSwapped = new System.Windows.Forms.Label();
            this.comboBoxCurrencies = new System.Windows.Forms.ComboBox();
            this.labelExternalCurrency = new System.Windows.Forms.Label();
            this.mCRate = new System.Windows.Forms.MonthCalendar();
            this.textBoxRateValue = new OpenCBS.GUI.UserControl.TextDecimalNumericUserControl();
            this.labelInternalCurrency = new System.Windows.Forms.Label();
            this.lbRateDetails = new System.Windows.Forms.Label();
            this.lvExchangeRate = new System.Windows.Forms.ListView();
            this.chDate = new System.Windows.Forms.ColumnHeader();
            this.zedGraphControlExchangeRateEvolution = new ZedGraph.ZedGraphControl();
            this.lbRateEvolution = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            //
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox2
            //
            this.groupBox2.Controls.Add(this.labelSwapped);
            this.groupBox2.Controls.Add(this.comboBoxCurrencies);
            this.groupBox2.Controls.Add(this.labelExternalCurrency);
            this.groupBox2.Controls.Add(this.mCRate);
            this.groupBox2.Controls.Add(this.textBoxRateValue);
            this.groupBox2.Controls.Add(this.buttonOK);
            this.groupBox2.Controls.Add(this.labelInternalCurrency);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // labelSwapped
            // 
            resources.ApplyResources(this.labelSwapped, "labelSwapped");
            this.labelSwapped.Name = "labelSwapped";
            // 
            // comboBoxCurrencies
            // 
            resources.ApplyResources(this.comboBoxCurrencies, "comboBoxCurrencies");
            this.comboBoxCurrencies.DisplayMember = "Currency.Name";
            this.comboBoxCurrencies.FormattingEnabled = true;
            this.comboBoxCurrencies.Name = "comboBoxCurrencies";
            this.comboBoxCurrencies.SelectedIndexChanged += new System.EventHandler(this.comboBoxCurrencies_SelectedIndexChanged);
            // 
            // labelExternalCurrency
            // 
            resources.ApplyResources(this.labelExternalCurrency, "labelExternalCurrency");
            this.labelExternalCurrency.Name = "labelExternalCurrency";
            // 
            // mCRate
            // 
            this.mCRate.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.mCRate, "mCRate");
            this.mCRate.MaxSelectionCount = 1;
            this.mCRate.Name = "mCRate";
            this.mCRate.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.mCRate_DateChanged);
            // 
            // textBoxRateValue
            // 
            resources.ApplyResources(this.textBoxRateValue, "textBoxRateValue");
            this.textBoxRateValue.Name = "textBoxRateValue";
            // 
            // labelInternalCurrency
            // 
            resources.ApplyResources(this.labelInternalCurrency, "labelInternalCurrency");
            this.labelInternalCurrency.Name = "labelInternalCurrency";
            // 
            // lbRateDetails
            // 
            resources.ApplyResources(this.lbRateDetails, "lbRateDetails");
            this.lbRateDetails.Name = "lbRateDetails";
            // 
            // lvExchangeRate
            // 
            this.lvExchangeRate.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDate});
            this.lvExchangeRate.FullRowSelect = true;
            this.lvExchangeRate.GridLines = true;
            resources.ApplyResources(this.lvExchangeRate, "lvExchangeRate");
            this.lvExchangeRate.MultiSelect = false;
            this.lvExchangeRate.Name = "lvExchangeRate";
            this.lvExchangeRate.UseCompatibleStateImageBehavior = false;
            this.lvExchangeRate.View = System.Windows.Forms.View.Details;
            // 
            // chDate
            // 
            resources.ApplyResources(this.chDate, "chDate");
            // 
            // zedGraphControlExchangeRateEvolution
            // 
            resources.ApplyResources(this.zedGraphControlExchangeRateEvolution, "zedGraphControlExchangeRateEvolution");
            this.zedGraphControlExchangeRateEvolution.BackColor = System.Drawing.Color.White;
            this.zedGraphControlExchangeRateEvolution.IsAutoScrollRange = false;
            this.zedGraphControlExchangeRateEvolution.IsEnableHPan = false;
            this.zedGraphControlExchangeRateEvolution.IsEnableHZoom = false;
            this.zedGraphControlExchangeRateEvolution.IsEnableVPan = false;
            this.zedGraphControlExchangeRateEvolution.IsEnableVZoom = false;
            this.zedGraphControlExchangeRateEvolution.IsScrollY2 = false;
            this.zedGraphControlExchangeRateEvolution.IsShowContextMenu = false;
            this.zedGraphControlExchangeRateEvolution.IsShowCursorValues = false;
            this.zedGraphControlExchangeRateEvolution.IsShowHScrollBar = false;
            this.zedGraphControlExchangeRateEvolution.IsShowPointValues = false;
            this.zedGraphControlExchangeRateEvolution.IsShowVScrollBar = false;
            this.zedGraphControlExchangeRateEvolution.IsZoomOnMouseCenter = false;
            this.zedGraphControlExchangeRateEvolution.Name = "zedGraphControlExchangeRateEvolution";
            this.zedGraphControlExchangeRateEvolution.PanButtons = System.Windows.Forms.MouseButtons.Left;
            this.zedGraphControlExchangeRateEvolution.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
            this.zedGraphControlExchangeRateEvolution.PanModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zedGraphControlExchangeRateEvolution.PointDateFormat = "g";
            this.zedGraphControlExchangeRateEvolution.PointValueFormat = "G";
            this.zedGraphControlExchangeRateEvolution.ScrollMaxX = 0;
            this.zedGraphControlExchangeRateEvolution.ScrollMaxY = 0;
            this.zedGraphControlExchangeRateEvolution.ScrollMaxY2 = 0;
            this.zedGraphControlExchangeRateEvolution.ScrollMinX = 0;
            this.zedGraphControlExchangeRateEvolution.ScrollMinY = 0;
            this.zedGraphControlExchangeRateEvolution.ScrollMinY2 = 0;
            this.zedGraphControlExchangeRateEvolution.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
            this.zedGraphControlExchangeRateEvolution.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
            this.zedGraphControlExchangeRateEvolution.ZoomModifierKeys = System.Windows.Forms.Keys.None;
            this.zedGraphControlExchangeRateEvolution.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zedGraphControlExchangeRateEvolution.ZoomStepFraction = 0.1;
            // 
            // lbRateEvolution
            // 
            resources.ApplyResources(this.lbRateEvolution, "lbRateEvolution");
            this.lbRateEvolution.Name = "lbRateEvolution";
            // 
            // ExchangeRateForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.lbRateEvolution);
            this.Controls.Add(this.zedGraphControlExchangeRateEvolution);
            this.Controls.Add(this.lvExchangeRate);
            this.Controls.Add(this.lbRateDetails);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ExchangeRateForm";
            this.Load += new System.EventHandler(this.ExchangeRateForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox comboBoxCurrencies;
        private Label labelSwapped;
    }
}
