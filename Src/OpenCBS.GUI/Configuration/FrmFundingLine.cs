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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.FundingLines;
using OpenCBS.Enums;
using OpenCBS.GUI.Configuration;
using OpenCBS.GUI.UserControl;
using OpenCBS.Services;
using OpenCBS.Shared;
using OpenCBS.ExceptionsHandler;
using ZedGraph;

namespace OpenCBS.GUI
{
    public partial class FrmFundingLine : SweetForm
    {
        private FundingLine _fundingLine;

        public FrmFundingLine()
        {
            InitializeComponent();
            InitializeFundingLines();

            tabControlFundingLines.TabPages.Remove(tabPageFundingLineDetails);
        }

        public void InitializeFundingLines()
        {
            listViewFundingLine.Items.Clear();
            List<FundingLine> list = ServicesProvider.GetInstance().GetFundingLinesServices().SelectFundingLines();
            foreach (FundingLine line in list)
            {
                ListViewItem item = new ListViewItem(line.Name) {Tag = line};
                item.SubItems.Add(line.StartDate.ToShortDateString());
                item.SubItems.Add(line.EndDate.ToShortDateString());
                item.SubItems.Add(line.Amount.GetFormatedValue(line.Currency.UseCents));
                item.SubItems.Add(line.RealRemainingAmount.GetFormatedValue(line.Currency.UseCents));
                item.SubItems.Add(line.AnticipatedRemainingAmount.GetFormatedValue(line.Currency.UseCents));
                item.SubItems.Add(line.Currency.Name);
                listViewFundingLine.Items.Add(item);
            }
        }

        private void DrawCashPrevisionGraph()
        {
            GraphPane myPane = zedGraphControlCashPrevision.GraphPane;

            int numDays = 5;
            // Set the title and axis labels
            myPane.Title = GetString("graph");
            myPane.XAxis.Title = GetString("graphX");
            myPane.YAxis.Title = GetString("graphY");
            myPane.CurveList = new CurveList();
            myPane.FontSpec.FontColor = Color.FromArgb(0, 88, 56);
            // Make up some data points
            string[] labels = ServicesProvider.GetInstance().GetGraphServices().CalculateDate(TimeProvider.Today,
                                                                                              numDays);
            double[] y = null;
            //_cashForFundingLine.CalculateCashProvisionChart(out labels, out y);
            y = ServicesProvider.GetInstance().GetGraphServices().CalculateChartForFundingLine(_fundingLine,
                                                                                               TimeProvider.Today,
                                                                                               numDays,
                                                                                               checkBoxIncludeLateLoans.
                                                                                                   Checked);

            // Generate a red curve with diamond
            // symbols, and "My Curve" in the legend
            LineItem myCurve = myPane.AddCurve(GetString("graphCurve"), null, y, Color.Black, SymbolType.Circle);

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

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            tabControlFundingLines.TabPages.Remove(tabPageFundingLineDetails);
            tabControlFundingLines.TabPages.Add(tabPageFundingLineDetails);
            tabControlFundingLines.SelectedTab = tabPageFundingLineDetails;
            _fundingLine = new FundingLine();
            InitializeTabPageFundingLineDetails(_fundingLine);
        }

        private void listViewFundingLine_DoubleClick(object sender, EventArgs e)
        {
            tabControlFundingLines.TabPages.Remove(tabPageFundingLineDetails);
            tabControlFundingLines.TabPages.Add(tabPageFundingLineDetails);
            tabControlFundingLines.SelectedTab = tabPageFundingLineDetails;

            _fundingLine = (FundingLine) listViewFundingLine.SelectedItems[0].Tag;
            InitializeTabPageFundingLineDetails(_fundingLine);
        }

        private void InitializeTabPageFundingLineDetails(FundingLine fl)
        {
            btnSave.Text = GetString(0 == fl.Id ? "save" : "update");
            textBoxFundingLineCode.Text = fl.Name;
            textBoxFundingLineName.Text = fl.Purpose;
            dateTimePickerFundingLineBeginDate.Value = fl.StartDate == DateTime.MinValue
                                                           ? TimeProvider.Today
                                                           : fl.StartDate;

            dateTimePickerFundingLineEndDate.Value = fl.EndDate == DateTime.MinValue
                                                         ? TimeProvider.Today
                                                         : fl.EndDate;
            _InitializeComboBoxCurrencies();
            if (fl.Currency != null)
            {
                comboBoxCurrencies.Text = fl.Currency.Name;
                comboBoxCurrencies.Enabled = false;
            }
            else
                comboBoxCurrencies.Enabled = true;
            RefreshView();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                _fundingLine.Name = textBoxFundingLineCode.Text;
                _fundingLine.Purpose = textBoxFundingLineName.Text;
                _fundingLine.StartDate = dateTimePickerFundingLineBeginDate.Value;
                _fundingLine.EndDate = dateTimePickerFundingLineEndDate.Value;
                _fundingLine.Currency = comboBoxCurrencies.SelectedItem as Currency;
                if (_fundingLine.Id != 0)
                    ServicesProvider.GetInstance().GetFundingLinesServices().UpdateFundingLine(_fundingLine);
                else
                    _fundingLine = ServicesProvider.GetInstance().GetFundingLinesServices().Create(_fundingLine);

                InitializeFundingLines();
                InitializeTabPageFundingLineDetails(_fundingLine);
                btnSave.Text = GetString("update");
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listViewFundingLine.SelectedItems != null)
            {
                _fundingLine = (FundingLine) listViewFundingLine.SelectedItems[0].Tag;

                ServicesProvider.GetInstance().GetFundingLinesServices().DeleteFundingLine(_fundingLine);
                InitializeFundingLines();
            }
        }

        private void buttonAddFundingLineEvent_Click(object sender, EventArgs e)
        {
            if (_fundingLine.Id == 0)
            {
                Notify("notSaved");
                return;
            }
            FrmFundingLineEvent frmEvent = new FrmFundingLineEvent(_fundingLine);
            frmEvent.ShowDialog();

            if (null == frmEvent.FundingLineEvent) return;
            try
            {
                FundingLineEvent newFundingLineEvent = frmEvent.FundingLineEvent;
                newFundingLineEvent =
                    ServicesProvider.GetInstance().GetFundingLinesServices().AddFundingLineEvent(newFundingLineEvent);
                _fundingLine.AddEvent(newFundingLineEvent);

                RefreshView();
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void _DisplayListViewFundingLineEvent(IEnumerable<FundingLineEvent> events)
        {
            listViewFundingLineEvent.Items.Clear();
            foreach (FundingLineEvent e in events)
            {
                ListViewItem item = new ListViewItem(e.Code)
                {
                    BackColor = (e.IsDeleted ? Color.Gray : Color.White)
                };

                item.SubItems.Add(e.CreationDate.ToShortDateString());
                item.SubItems.Add(e.Movement.ToString());
                item.SubItems.Add(e.Amount.GetFormatedValue(true));
                item.SubItems.Add(GetString("type" + e.Type));
                item.Tag = e;
                listViewFundingLineEvent.Items.Add(item);
            }
            listViewFundingLineEvent.Invalidate();
        }

        private void buttonDeleteFundingLineEvent_Click(object sender, EventArgs e)
        {
            if (listViewFundingLineEvent.SelectedItems.Count != 0)
            {
                FundingLineEvent fundingLineEventBody = (FundingLineEvent) listViewFundingLineEvent.SelectedItems[0].Tag;
                if (fundingLineEventBody.Type != OFundingLineEventTypes.Entry)
                {
                    Fail("wrongType");
                    return;
                }
                if (fundingLineEventBody.Amount > _fundingLine.AnticipatedRemainingAmount)
                {
                    Fail("amountCommitted");
                    return;
                }
                ServicesProvider.GetInstance().GetFundingLinesServices().DeleteFundingLineEvent(fundingLineEventBody);
                _fundingLine.RemoveEvent(fundingLineEventBody);
                RefreshView();
            }
        }

        private void RefreshView()
        {
            _DisplayListViewFundingLineEvent(_fundingLine.Events);
            _DisplayAmounts(_fundingLine);
            InitializeFundingLines();
            DrawCashPrevisionGraph();
        }

        private void _DisplayAmounts(FundingLine fl)
        {
            bool useCents = null == fl.Currency ? true : fl.Currency.UseCents;
            tbInitialAmt.Text = fl.Amount.GetFormatedValue(useCents);
            tbRealAmt.Text = fl.RealRemainingAmount.GetFormatedValue(useCents);
            tbAnticipatedAmt.Text = fl.AnticipatedRemainingAmount.GetFormatedValue(useCents);
        }

        private void zedGraphControlCashPrevision_Click(object sender, EventArgs e)
        {
            InitializeFinancialPrevisionGraph();
        }

        private void InitializeFinancialPrevisionGraph()
        {
            CashPrevisionForm cashPrevisionForm = new CashPrevisionForm(_fundingLine,
                                                                        this.checkBoxIncludeLateLoans.Checked)
                                                      {MdiParent = this.ParentForm};

            cashPrevisionForm.Show();
        }

        private void checkBoxIncludeLateLoans_CheckedChanged(object sender, EventArgs e)
        {
            DrawCashPrevisionGraph();
        }

        private void _InitializeComboBoxCurrencies()
        {
            comboBoxCurrencies.Items.Clear();
            comboBoxCurrencies.Text = GetString("select");
            List<Currency> currencies = ServicesProvider.GetInstance().GetCurrencyServices().FindAllCurrencies();
            Currency line = new Currency
                                {
                                    Name = GetString("select"),
                                    Id = 0
                                };
            comboBoxCurrencies.Items.Add(line);

            foreach (Currency cur in currencies)
            {
                comboBoxCurrencies.Items.Add(cur);
            }
        }

        private void comboBoxCurrencies_SelectedIndexChanged(object sender, EventArgs e)
        {
            _fundingLine.Currency = (Currency) comboBoxCurrencies.SelectedItem;
        }
    }
}
