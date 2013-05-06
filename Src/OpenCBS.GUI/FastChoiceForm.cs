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
            var chart = new Chart();
            var chartArea = new ChartArea();
            chart.ChartAreas.Add(chartArea);

            var olb = dashboard.Portfolios.Sum(item => item.Olb);
            var par = dashboard.Portfolios.Sum(item => item.Par);
            var parPercentage = Math.Round(par/olb, 1);
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
            chart.Legends.Add(legend);

            chart.Series.Add(series);
            chart.Location = new Point(0, 0);
            //chart.Size = new Size(250, 150);
            chart.Dock = DockStyle.Fill;

            portfolioPanel.Controls.Add(chart);
        }

        private void RefreshParPieChart(Dashboard dashboard)
        {
            var chart = new Chart();
            var chartArea = new ChartArea();
            chart.ChartAreas.Add(chartArea);

            var series = new Series();
            series.ChartType = SeriesChartType.Pie;
            var point = series.Points.Add(60);
            point.LegendText = "1-30: 60%";
            point.Color = Color.FromArgb(28, 151, 234);
            point = series.Points.Add(7);
            point.LegendText = "31-60: 7%";
            point.Color = Color.FromArgb(28, 198, 234);
            point = series.Points.Add(15);
            point.LegendText = "61-90: 15%";
            point.Color = Color.FromArgb(234, 217, 28);
            point = series.Points.Add(5);
            point.LegendText = "91-180: 5%";
            point.Color = Color.FromArgb(234, 178, 28);
            point = series.Points.Add(10);
            point.LegendText = "181-365: 10%";
            point.Color = Color.FromArgb(234, 106, 28);
            point = series.Points.Add(3);
            point.LegendText = ">365: 3%";
            point.Color = Color.FromArgb(234, 28, 28);

            //var title = new Title
            //{
            //    Text = "PAR",
            //    ForeColor = Color.FromArgb(45, 45, 48),
            //    Font = new Font("Arial", 9.75f)
            //};
            //chart.Titles.Add(title);

            var legend = new Legend();
            legend.Docking = Docking.Right;
            legend.Alignment = StringAlignment.Center;
            chart.Legends.Add(legend);

            chart.Series.Add(series);
            chart.Location = new Point(0, 0);
            //chart.Size = new Size(250, 150);
            chart.Dock = DockStyle.Fill;

            parPanel.Controls.Add(chart);
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
        }

        private void RefreshDashboard()
        {
            var us = ServicesProvider.GetInstance().GetUserServices();
            var dashboard = us.GetDashboard();

            RefreshActivityStream(dashboard);
            RefreshPortfolioPieChart(dashboard);
            RefreshParPieChart(dashboard);
        }
    }
}
