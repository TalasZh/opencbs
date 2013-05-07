// LICENSE PLACEHOLDER

using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using OpenCBS.CoreDomain.Dashboard;
using OpenCBS.Enums;
using OpenCBS.GUI.Clients;
using OpenCBS.GUI.UserControl;
using OpenCBS.Services;

namespace OpenCBS.GUI
{
    public partial class FastChoiceForm : SweetBaseForm
    {
        private Chart _portfolioChart;
        private Chart _parChart;
        private Chart _disbursementsChart;
        private Chart _olbTrendChart;

        public FastChoiceForm()
		{
            InitializeComponent();
		}

        private void OnLoad(object sender, EventArgs e)
        {
            var numberFormatInfo = new NumberFormatInfo
            {
                NumberGroupSeparator = " ",
                NumberDecimalSeparator = ".",
            };

            activityPefrormedAtColumn.AspectToStringConverter = value =>
            {
                var date = (DateTime) value;
                return date.ToString("dd.MM.yyyy");
            };

            activityAmountColumn.AspectToStringConverter = value =>
            {
                var amount = (decimal) value;
                return amount.ToString("N2", numberFormatInfo);
            };

            //CreatePortfolioPieChart();
            //CreateParPieChart();
            //RefreshDisbursementsChart();
            //FillActivityStream();
            RefreshDashboard();
        }

        private void RefreshPortfolioPieChart(Dashboard dashboard)
        {
            if (_portfolioChart != null)
            {
                portfolioPanel.Controls.Remove(_portfolioChart);
            }
            _portfolioChart = new Chart();
            var chartArea = new ChartArea();
            _portfolioChart.ChartAreas.Add(chartArea);

            var parPercentage = 0 == dashboard.Olb ? 0 : Math.Round(100*dashboard.Par/dashboard.Olb, 1);
            var performingPercentage = 100 - parPercentage;

            var numberFormatInfo = new NumberFormatInfo
            {
                NumberGroupSeparator = " ",
                NumberDecimalSeparator = ",",
            };

            var series = new Series();
            series.ChartType = SeriesChartType.Pie;
            var point = series.Points.Add(Convert.ToDouble(performingPercentage));
            point.LegendText = string.Format("Performing: {0} %", performingPercentage.ToString("N1", numberFormatInfo));
            point.Color = Color.FromArgb(28, 151, 234);
            
            point = series.Points.Add(Convert.ToDouble(parPercentage));
            point.LegendText = string.Format("PAR: {0} %", parPercentage.ToString("N1", numberFormatInfo));
            point.Color = Color.FromArgb(234, 28, 28);

            var legend = new Legend
            {
                Docking = Docking.Right, 
                Alignment = StringAlignment.Center,
            };
            _portfolioChart.Legends.Add(legend);

            _portfolioChart.Series.Add(series);
            _portfolioChart.Location = new Point(0, 0);
            _portfolioChart.Dock = DockStyle.Fill;

            portfolioPanel.Controls.Add(_portfolioChart);
        }

        private void RefreshParPieChart(Dashboard dashboard)
        {
            if (_parChart != null)
            {
                parPanel.Controls.Remove(_parChart);
            }
            if (0 == dashboard.Par) return;

            _parChart = new Chart();
            var chartArea = new ChartArea();
            _parChart.ChartAreas.Add(chartArea);

            var values = new[]
            {
                dashboard.Par1To30,
                dashboard.Par31To60,
                dashboard.Par61To90,
                dashboard.Par91To180,
                dashboard.Par181To365,
                dashboard.Par365,
            };

            var legends = new[]
            {
                "1-30",
                "31-60",
                "61-90",
                "91-180",
                "181-365%",
                ">365",
            };

            var colors = new[]
            {
                Color.FromArgb(234, 200, 28),
                Color.FromArgb(234, 160, 28),
                Color.FromArgb(234, 120, 28),
                Color.FromArgb(234, 80, 28),
                Color.FromArgb(234, 40, 28),
                Color.FromArgb(234, 0, 28),
            };

            var series = new Series();
            series.ChartType = SeriesChartType.Pie;
            for (var i = 0; i < 6; i++)
            {
                var value = Math.Round(0 == dashboard.Par ? 0 : 100*values[i]/dashboard.Par, 1);
                var point = series.Points.Add(Convert.ToDouble(value));
                var numberFormat = Math.Round(value) == value ? "N0" : "N1";
                point.LegendText = string.Format("{0}: {1}%", legends[i], value.ToString(numberFormat));
                point.Color = colors[i];
            }

            var legend = new Legend
            {
                Docking = Docking.Right,
                Alignment = StringAlignment.Center,
                Font = new Font("Arial", 8f),
            };
            _parChart.Legends.Add(legend);

            _parChart.Series.Add(series);
            _parChart.Location = new Point(0, 0);
            _parChart.Dock = DockStyle.Fill;

            parPanel.Controls.Add(_parChart);
        }

        private void RefreshDisbursementsChart(Dashboard dashboard)
        {
            //if (_disbursementsChart != null)
            //{
            //    disbursementsPanel.Controls.Remove(_disbursementsChart);
            //}
            //_disbursementsChart = new Chart();
            //var chartArea = new ChartArea();
            //chartArea.AxisX.LabelAutoFitMaxFontSize = 8;
            //chartArea.AxisX.LabelAutoFitMinFontSize = 8;
            //_disbursementsChart.ChartAreas.Add(chartArea);

            //var series = new Series();
            //var series2 = new Series();
            //foreach (var actionStat in dashboard.ActionStats)
            //{
            //    var point = series.Points.Add(actionStat.NumberDisbursed);
            //    point.AxisLabel = actionStat.Date.ToString("dd.MM");
            //    point = series2.Points.Add(-actionStat.NumberRepaid);
            //}

            //_disbursementsChart.Series.Add(series);
            //_disbursementsChart.Dock = DockStyle.Fill;

            //disbursementsPanel.Controls.Add(_disbursementsChart);
        }

        private void RefreshOlbTrendChart(Dashboard dashboard)
        {
            //if (_olbTrendChart != null)
            //{
            //    olbTrendPanel.Controls.Remove(_olbTrendChart);
            //}
            //_olbTrendChart = new Chart();
            //var chartArea = new ChartArea();
            //chartArea.AxisX.LabelAutoFitMaxFontSize = 8;
            //chartArea.AxisX.LabelAutoFitMinFontSize = 8;
            //_olbTrendChart.ChartAreas.Add(chartArea);

            //var series = new Series();
            //var series2 = new Series();
            //foreach (var actionStat in dashboard.ActionStats)
            //{
            //    var point = series.Points.Add(Convert.ToDouble(actionStat.OlbGrowth));
            //    point.AxisLabel = actionStat.Date.ToString("dd.MM");
            //    //point = series2.Points.Add(Convert.ToDouble(-actionStat.AmountRepaid));
            //}

            //_olbTrendChart.Series.Add(series);
            ////_olbTrendChart.Series.Add(series2);
            //_olbTrendChart.Dock = DockStyle.Fill;

            //olbTrendPanel.Controls.Add(_olbTrendChart);
        }



        private void OpenClientForm(OClientTypes clientType)
        {
            var parent = Application.OpenForms[0];
            var form = new ClientForm(clientType, parent, false) { MdiParent = parent };
            form.Show();            
        }

        private void OnNewIndividualClientLinkLabelClick(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenClientForm(OClientTypes.Person);
        }

        private void OnNewSolidarityGroupLinkLabelLinkClick(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenClientForm(OClientTypes.Group);
        }

        private void OnNewNonSolidairtyGroupLinkLabelLinkClick(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new NonSolidaryGroupForm { MdiParent = Application.OpenForms[0] };
            form.Show();
        }

        private void OnCorporateClientLinkLabelLinkClick(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenClientForm(OClientTypes.Corporate);
        }

        private void RefreshActivityStream(Dashboard dashboard)
        {
            activityListView.SetObjects(dashboard.Actions);
            activityStreamLabel.Text = string.Format("ACTIVITY STREAM ({0:d0})", dashboard.Actions.Count);
        }

        private void RefreshDashboard()
        {
            var us = ServicesProvider.GetInstance().GetUserServices();
            var dashboard = us.GetDashboard();

            RefreshActivityStream(dashboard);
            RefreshPortfolioPieChart(dashboard);
            RefreshParPieChart(dashboard);
            RefreshDisbursementsChart(dashboard);
            RefreshOlbTrendChart(dashboard);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDashboard();
        }
    }
}
