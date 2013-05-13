// Octopus MFS is an integrated suite for managing a Micro Finance Institution: 
// clients, contracts, accounting, reporting and risk
// Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using OpenCBS.CoreDomain.FundingLines;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Services;
using OpenCBS.Shared;
using ZedGraph;

namespace OpenCBS.GUI
{
    /// <summary>
    /// Description r�sum�e de CashPrevisionForm.
    /// </summary>
    public class CashPrevisionForm : Form
    {
        /// <summary>
        /// Variable n�cessaire au concepteur.
        /// </summary>
        private Container components = null;

        private ZedGraphControl zedGraphControlCashPrevision;
        private Splitter splitter1;
        private Panel panel1;
        private Label labelPrevision;
        private ComboBox comboBoxForecastDays;
        private GroupBox groupBoxButton;
        private Button buttonPreview;
        private Button buttonNext;
        private int forecastDays;
        private Button buttonRepaymentExit;
        private DateTime date;
        private CheckBox checkBoxIncludeLateLoans;
       private bool includeDeleted;

       public FundingLine _cashForFundingLine;
       

        public CashPrevisionForm()
        {
            InitializeComponent();
            forecastDays = 5;
            date = TimeProvider.Today;

            DrawZedGraphForCashPrevision();
        }
        public CashPrevisionForm(FundingLine pfundingLine, bool pIncludeDeleted)
        {
           InitializeComponent();
           date = TimeProvider.Today;
           _cashForFundingLine = pfundingLine;
           forecastDays = 5;           
           includeDeleted = pIncludeDeleted;
           checkBoxIncludeLateLoans.Checked = pIncludeDeleted;
           DrawZedGraphForCashPrevision();

        }

        /// <summary>
        /// Nettoyage des ressources utilis�es.
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

        #region Code g�n�r?par le Concepteur Windows Form

        /// <summary>
        /// M�thode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette m�thode avec l'�diteur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CashPrevisionForm));
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.zedGraphControlCashPrevision = new ZedGraph.ZedGraphControl();
            this.groupBoxButton = new System.Windows.Forms.GroupBox();
            this.checkBoxIncludeLateLoans = new System.Windows.Forms.CheckBox();
            this.buttonRepaymentExit = new System.Windows.Forms.Button();
            this.buttonPreview = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.labelPrevision = new System.Windows.Forms.Label();
            this.comboBoxForecastDays = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.groupBoxButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            this.splitter1.AccessibleDescription = null;
            this.splitter1.AccessibleName = null;
            resources.ApplyResources(this.splitter1, "splitter1");
            this.splitter1.BackgroundImage = null;
            this.splitter1.Font = null;
            this.splitter1.Name = "splitter1";
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.AccessibleDescription = null;
            this.panel1.AccessibleName = null;
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackgroundImage = null;
            this.panel1.Controls.Add(this.zedGraphControlCashPrevision);
            this.panel1.Font = null;
            this.panel1.Name = "panel1";
            // 
            // zedGraphControlCashPrevision
            // 
            this.zedGraphControlCashPrevision.AccessibleDescription = null;
            this.zedGraphControlCashPrevision.AccessibleName = null;
            resources.ApplyResources(this.zedGraphControlCashPrevision, "zedGraphControlCashPrevision");
            this.zedGraphControlCashPrevision.BackgroundImage = null;
            this.zedGraphControlCashPrevision.Font = null;
            this.zedGraphControlCashPrevision.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.zedGraphControlCashPrevision.IsAutoScrollRange = false;
            this.zedGraphControlCashPrevision.IsEnableHPan = false;
            this.zedGraphControlCashPrevision.IsEnableHZoom = false;
            this.zedGraphControlCashPrevision.IsEnableVPan = false;
            this.zedGraphControlCashPrevision.IsEnableVZoom = false;
            this.zedGraphControlCashPrevision.IsScrollY2 = false;
            this.zedGraphControlCashPrevision.IsShowContextMenu = true;
            this.zedGraphControlCashPrevision.IsShowCursorValues = false;
            this.zedGraphControlCashPrevision.IsShowHScrollBar = false;
            this.zedGraphControlCashPrevision.IsShowPointValues = false;
            this.zedGraphControlCashPrevision.IsShowVScrollBar = false;
            this.zedGraphControlCashPrevision.IsZoomOnMouseCenter = false;
            this.zedGraphControlCashPrevision.Name = "zedGraphControlCashPrevision";
            this.zedGraphControlCashPrevision.PanButtons = System.Windows.Forms.MouseButtons.Left;
            this.zedGraphControlCashPrevision.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
            this.zedGraphControlCashPrevision.PanModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zedGraphControlCashPrevision.PointDateFormat = "g";
            this.zedGraphControlCashPrevision.PointValueFormat = "G";
            this.zedGraphControlCashPrevision.ScrollMaxX = 0;
            this.zedGraphControlCashPrevision.ScrollMaxY = 0;
            this.zedGraphControlCashPrevision.ScrollMaxY2 = 0;
            this.zedGraphControlCashPrevision.ScrollMinX = 0;
            this.zedGraphControlCashPrevision.ScrollMinY = 0;
            this.zedGraphControlCashPrevision.ScrollMinY2 = 0;
            this.zedGraphControlCashPrevision.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
            this.zedGraphControlCashPrevision.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
            this.zedGraphControlCashPrevision.ZoomModifierKeys = System.Windows.Forms.Keys.None;
            this.zedGraphControlCashPrevision.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zedGraphControlCashPrevision.ZoomStepFraction = 0.1;
            // 
            // groupBoxButton
            // 
            this.groupBoxButton.AccessibleDescription = null;
            this.groupBoxButton.AccessibleName = null;
            resources.ApplyResources(this.groupBoxButton, "groupBoxButton");
            this.groupBoxButton.Controls.Add(this.checkBoxIncludeLateLoans);
            this.groupBoxButton.Controls.Add(this.buttonRepaymentExit);
            this.groupBoxButton.Controls.Add(this.buttonPreview);
            this.groupBoxButton.Controls.Add(this.buttonNext);
            this.groupBoxButton.Controls.Add(this.labelPrevision);
            this.groupBoxButton.Controls.Add(this.comboBoxForecastDays);
            this.groupBoxButton.Font = null;
            this.groupBoxButton.Name = "groupBoxButton";
            this.groupBoxButton.TabStop = false;
            // 
            // checkBoxIncludeLateLoans
            // 
            this.checkBoxIncludeLateLoans.AccessibleDescription = null;
            this.checkBoxIncludeLateLoans.AccessibleName = null;
            resources.ApplyResources(this.checkBoxIncludeLateLoans, "checkBoxIncludeLateLoans");
            this.checkBoxIncludeLateLoans.Name = "checkBoxIncludeLateLoans";
            this.checkBoxIncludeLateLoans.CheckedChanged += new System.EventHandler(this.checkBoxIncludeLateLoans_CheckedChanged);
            // 
            // buttonRepaymentExit
            // 
            this.buttonRepaymentExit.AccessibleDescription = null;
            this.buttonRepaymentExit.AccessibleName = null;
            resources.ApplyResources(this.buttonRepaymentExit, "buttonRepaymentExit");
            this.buttonRepaymentExit.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonRepaymentExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonRepaymentExit.Name = "buttonRepaymentExit";
            this.buttonRepaymentExit.UseVisualStyleBackColor = false;
            this.buttonRepaymentExit.Click += new System.EventHandler(this.buttonRepaymentExit_Click);
            // 
            // buttonPreview
            // 
            this.buttonPreview.AccessibleDescription = null;
            this.buttonPreview.AccessibleName = null;
            resources.ApplyResources(this.buttonPreview, "buttonPreview");
            this.buttonPreview.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonPreview.Name = "buttonPreview";
            this.buttonPreview.UseVisualStyleBackColor = false;
            this.buttonPreview.Click += new System.EventHandler(this.buttonPreview_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.AccessibleDescription = null;
            this.buttonNext.AccessibleName = null;
            resources.ApplyResources(this.buttonNext, "buttonNext");
            this.buttonNext.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.UseVisualStyleBackColor = false;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // labelPrevision
            // 
            this.labelPrevision.AccessibleDescription = null;
            this.labelPrevision.AccessibleName = null;
            resources.ApplyResources(this.labelPrevision, "labelPrevision");
            this.labelPrevision.BackColor = System.Drawing.Color.Transparent;
            this.labelPrevision.Name = "labelPrevision";
            // 
            // comboBoxForecastDays
            // 
            this.comboBoxForecastDays.AccessibleDescription = null;
            this.comboBoxForecastDays.AccessibleName = null;
            resources.ApplyResources(this.comboBoxForecastDays, "comboBoxForecastDays");
            this.comboBoxForecastDays.BackgroundImage = null;
            this.comboBoxForecastDays.Font = null;
            this.comboBoxForecastDays.Items.AddRange(new object[] {
            resources.GetString("comboBoxForecastDays.Items"),
            resources.GetString("comboBoxForecastDays.Items1"),
            resources.GetString("comboBoxForecastDays.Items2"),
            resources.GetString("comboBoxForecastDays.Items3"),
            resources.GetString("comboBoxForecastDays.Items4"),
            resources.GetString("comboBoxForecastDays.Items5")});
            this.comboBoxForecastDays.Name = "comboBoxForecastDays";
            this.comboBoxForecastDays.SelectionChangeCommitted += new System.EventHandler(this.comboBoxForecastDays_SelectionChangeCommitted);
            // 
            // CashPrevisionForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.groupBoxButton);
            this.Font = null;
            this.Name = "CashPrevisionForm";
            this.panel1.ResumeLayout(false);
            this.groupBoxButton.ResumeLayout(false);
            this.groupBoxButton.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        //Zedgraph control
        private void DrawZedGraphForCashPrevision()
        {
           GraphPane myPane = zedGraphControlCashPrevision.GraphPane;

           // Set the title and axis labels
           myPane.Title = MultiLanguageStrings.GetString(Ressource.CashPrevisionForm, "graphTitle.Text");

           myPane.XAxis.Title = MultiLanguageStrings.GetString(Ressource.CashPrevisionForm, "graphXAxisTitle.Text");
           myPane.YAxis.Title = MultiLanguageStrings.GetString(Ressource.CashPrevisionForm, "graphYAxisTitle.Text");
           myPane.CurveList = new CurveList();
           myPane.FontSpec.FontColor = Color.FromArgb(0, 88, 56);

           // Make up some data points
           string[] labels = ServicesProvider.GetInstance().GetGraphServices().CalculateDate(date, forecastDays);
           double[] y = null;
           if (_cashForFundingLine != null)
              y = ServicesProvider.GetInstance().GetGraphServices().CalculateChartForFundingLine(_cashForFundingLine, date, forecastDays, includeDeleted);
           else
              y = ServicesProvider.GetInstance().GetGraphServices().CalculateRealPrevisionCurve(date, forecastDays);



           // Generate a red curve with diamond
           // symbols, and "My Curve" in the legend
           LineItem myCurve = myPane.AddCurve(MultiLanguageStrings.GetString(Ressource.CashPrevisionForm, "graphCashAtHandCurve.Text"),
                                              null, y, Color.Black, SymbolType.Circle);


           myCurve.Line.Fill = new Fill(Color.White, Color.FromArgb(0, 88, 56), -45F);

           //Make the curve smooth
           myCurve.Line.IsSmooth = true;
           myPane.AxisFill = new Fill(Color.White, Color.FromArgb(255, 255, 166), 90F);

           // Set the XAxis to Text type
           myPane.XAxis.Type = AxisType.Text;
           // Set the XAxis labels
           myPane.XAxis.TextLabels = labels;
           // Set the labels at an angle so they don't overlap
           myPane.XAxis.ScaleFontSpec.Angle = 40;
           myPane.XAxis.IsShowGrid = true;
           myPane.YAxis.IsShowGrid = true;
           myPane.PaneFill = new Fill(Color.White);
           // Calculate the Axis Scale Ranges
           zedGraphControlCashPrevision.AxisChange();
           zedGraphControlCashPrevision.Refresh();
        }

        private void comboBoxForecastDays_SelectionChangeCommitted(object sender, EventArgs e)
        {
            forecastDays = Convert.ToInt32(comboBoxForecastDays.SelectedItem);
            DrawZedGraphForCashPrevision();
        }

        private void buttonPreview_Click(object sender, EventArgs e)
        {
            DateTime _date = date.AddDays(-forecastDays);
            date = _date;
            DrawZedGraphForCashPrevision();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            DateTime _date = date.AddDays(forecastDays);
            date = _date;
            DrawZedGraphForCashPrevision();
        }

        private void buttonRepaymentExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkBoxIncludeLateLoans_CheckedChanged(object sender, EventArgs e)
        {
           includeDeleted = checkBoxIncludeLateLoans.Checked;
           DrawZedGraphForCashPrevision();

        }
    }
}
