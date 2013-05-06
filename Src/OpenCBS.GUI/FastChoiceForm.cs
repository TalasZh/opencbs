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
using System.Linq;

namespace OpenCBS.GUI
{
    public partial class FastChoiceForm : SweetBaseForm
    {
        private Chart _portfolioChart;
        private Chart _parChart;

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
                return date.ToString("dd.MM.yyyy HH:mm");
            };

            activityAmountColumn.AspectToStringConverter = value =>
            {
                var amount = (decimal) value;
                return amount.ToString("N2", numberFormatInfo);
            };

            //CreatePortfolioPieChart();
            //CreateParPieChart();
            //CreateDisbursementsRepaymentsChart();
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

            var olb = dashboard.Portfolios.Sum(item => item.Olb);
            var par = dashboard.Portfolios.Sum(item => item.Par);
            var parPercentage = Math.Round(100*par/olb, 1);
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

            //var title = new Title
            //{
            //    Text = "Portfolio",
            //    ForeColor = Color.FromArgb(45, 45, 48),
            //    Font = new Font("Arial", 9.75f)
            //};
            //chart.Titles.Add(title);

            var legend = new Legend
            {
                Docking = Docking.Right, 
                Alignment = StringAlignment.Center,
            };
            _portfolioChart.Legends.Add(legend);

            _portfolioChart.Series.Add(series);
            _portfolioChart.Location = new Point(0, 0);
            //chart.Size = new Size(250, 150);
            _portfolioChart.Dock = DockStyle.Fill;

            portfolioPanel.Controls.Add(_portfolioChart);
        }

        private void RefreshParPieChart(Dashboard dashboard)
        {
            if (_parChart != null)
            {
                parPanel.Controls.Remove(_parChart);
            }
            _parChart = new Chart();
            var chartArea = new ChartArea();
            _parChart.ChartAreas.Add(chartArea);

            var par = dashboard.Portfolios.Sum(item => item.Par);
            var values = new[]
            {
                dashboard.Portfolios.Sum(item => item.Par1To30),
                dashboard.Portfolios.Sum(item => item.Par31To60),
                dashboard.Portfolios.Sum(item => item.Par61To90),
                dashboard.Portfolios.Sum(item => item.Par91To180),
                dashboard.Portfolios.Sum(item => item.Par181To365),
                dashboard.Portfolios.Sum(item => item.Par365),
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
                var value = Math.Round(100*values[i]/par, 1);
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
            //chart.Size = new Size(250, 150);
            _parChart.Dock = DockStyle.Fill;

            parPanel.Controls.Add(_parChart);
        }

        private void CreateDisbursementsRepaymentsChart()
        {
            var chart = new Chart();
            var chartArea = new ChartArea();
            chart.ChartAreas.Add(chartArea);

            var series = new Series();
            series.Points.Add(1000);
            series.Points.Add(2000);
            series.Points.Add(1700);

            var series2 = new Series();
            series2.Points.Add(-700);
            series2.Points.Add(-1000);
            series2.Points.Add(-2000);


            var title = new Title
            {
                Text = "Disbursements & Repayments",
                ForeColor = Color.FromArgb(45, 45, 48),
                Font = new Font("Arial", 9.75f)
            };
            chart.Titles.Add(title);

            //var legend = new Legend
            //{
            //    Docking = Docking.Right,
            //    Alignment = StringAlignment.Center,
            //};
            //chart.Legends.Add(legend);

            chart.Series.Add(series);
            chart.Series.Add(series2);
            chart.Location = new Point(540, 10);
            chart.Size = new Size(250, 150);

            infoPanel.Controls.Add(chart);
            
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDashboard();
        }
    }
}
