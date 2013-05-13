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
using System.Diagnostics;
using System.Windows.Forms;
using OpenCBS.CoreDomain;
using OpenCBS.GUI.UserControl;
using OpenCBS.Reports;
using OpenCBS.Reports.Forms;
using OpenCBS.Services.Events;
using OpenCBS.Shared;
using OpenCBS.Services;
using OpenCBS.CoreDomain.Events;

namespace OpenCBS.GUI.AuditTrail
{
    public partial class AuditTrailForm : SweetForm
    {
        private delegate void UpdateListViewDelegate(List<AuditTrailEvent> list);

        public AuditTrailForm()
        {
            InitializeComponent();
            InitDates();

            colEvents_Date.AspectToStringConverter = delegate(object value)
            {
                DateTime date = (DateTime)value;
                return date.ToShortDateString();
            };

            colEvents_EntryDate.AspectToStringConverter = delegate(object value)
            {
                DateTime date = (DateTime)value;
                return date.ToShortDateString();
            };
        }

        private void InitEventTypes()
        {
            EventProcessorServices s = ServicesProvider.GetInstance().GetEventProcessorServices();
            List<EventType> types = s.SelectEventTypes();
            foreach (EventType t in types)
            {
                ListViewItem item = new ListViewItem
                {
                    Text = t.EventCode
                    , Tag = t.EventCode
                };
                item.SubItems.Add(GetString("EventTypes", t.EventCode));
                lvEventType.Items.Add(item);
            }
            lvEventType.Columns[0].Width = -1; // auto size
            lvEventType.Columns[1].Width = -1; // auto size
        }

        private void InitUsers()
        {
            cbUser.Items.Clear();

            List<User> users = ServicesProvider.GetInstance().GetUserServices().FindAll(false);

            cbUser.Items.Add(GetString("all"));

            foreach (User user in users)
            {
                cbUser.Items.Add(user);
            }
            cbUser.SelectedIndex = 0;
        }

        private void InitBranches()
        {
            cbBranch.Items.Clear();
            cbBranch.Items.Add(GetString("allBranches"));
            List<Branch> branches = ServicesProvider.GetInstance().GetBranchService().FindAllNonDeleted();
            foreach (Branch branch in branches)
            {
                cbBranch.Items.Add(branch);
            }
            cbBranch.SelectedIndex = 0;
        }

        private void InitDates()
        {
            dtTo.Value = TimeProvider.Today;
            dtFrom.Value = dtTo.Value.AddMonths(-1);
        }

        private void OnLoad(object sender, EventArgs e)
        {
            InitEventTypes();
            InitUsers();
            InitBranches();
        }

        private void OnPrintClick(object sender, EventArgs e)
        {
            //btnPrint.StartProgress();

            User u = cbUser.SelectedItem as User;
            Branch b = cbBranch.SelectedItem as Branch;
            AuditTrailFilter filter;
            filter.From = dtFrom.Value;
            filter.To = dtTo.Value;
            filter.UserId = null == u ? 0 : u.Id;
            filter.BranchId = null == b ? 0 : b.Id;
            filter.Types = EventTypesToString();
            filter.IncludeDeleted = chkIncludeDeleted.Checked;
            string userName = null == u ? GetString("all") : u.Name;
            bwReport.RunWorkerAsync(new object[] {filter, userName});
        }

        private void OnRefreshClick(object sender, EventArgs e)
        {
            //btnRefresh.StartProgress();

            User u = cbUser.SelectedItem as User;
            Branch b = cbBranch.SelectedItem as Branch;
            AuditTrailFilter filter;
            filter.From = dtFrom.Value;
            filter.To = dtTo.Value;
            filter.UserId = null == u ? 0 : u.Id;
            filter.BranchId = null == b ? 0 : b.Id;
            filter.Types = EventTypesToString();
            filter.IncludeDeleted = chkIncludeDeleted.Checked;

            bwRefresh.RunWorkerAsync(filter);
        }

        private string EventTypesToString()
        {
            List<string> list = new List<string>();

            foreach (ListViewItem item in lvEventType.Items)
            {
                if (!item.Checked) continue;
                list.Add(item.Tag.ToString());
            }

            return string.Join(",", list.ToArray());
        }

        private void UpdateEvents(IEnumerable<AuditTrailEvent> events)
        {
            // The trick below is necessary if you intend to call this
            // method from a non-GUI thread (which is actually the case).
            if (InvokeRequired)
            {
                Invoke(new UpdateListViewDelegate(UpdateEvents), new object[] {events});
                return;
            }

            olvEvents.SetObjects(events);
            UpdateTitle();
        }

        private void UpdateTitle()
        {
            int count = olvEvents.Items.Count;
            Text = string.Format(GetString("title"), count);
        }

        private void OnCheckAllClick(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvEventType.Items)
            {
                item.Checked = true;
            }
        }

        private void OnUncheckAllClick(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvEventType.Items)
            {
                item.Checked = false;
            }
        }

        private void OnPrintDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            ReportService rs = ReportService.GetInstance();
            Debug.Assert(rs != null, "Report service not found");
            Report r = rs.GetReportByName("Audit_Trail.zip");
            Debug.Assert(r != null, "Report not found");
            Debug.Assert(r.IsLoaded, "Report not loaded");

            object[] arr = (object[]) e.Argument;
            AuditTrailFilter filter = (AuditTrailFilter) arr[0];
            string userName = arr[1].ToString();
            r.RemoveParams();
            r.AddParam("from", filter.From);
            r.AddParam("to", filter.To);
            r.AddParam("user", filter.UserId);
            r.AddParam("user_name", userName);
            r.AddParam("events", filter.Types);
            r.AddParam("include_deleted", filter.IncludeDeleted);
            rs.LoadReport(r);
            e.Result = r;
        }

        private void OnPrintCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Report r = e.Result as Report;
            Debug.Assert(r != null, "Report not loaded");
            ReportViewerForm frm = new ReportViewerForm(r);
            frm.Show();

            //btnPrint.StopProgress();
        }

        private void OnRefreshDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Debug.Assert(e.Argument != null, "Argument is null");
            AuditTrailFilter filter = (AuditTrailFilter) e.Argument;
            EventProcessorServices s = ServicesProvider.GetInstance().GetEventProcessorServices();
            List<AuditTrailEvent> events = s.SelectAuditTrailEvents(filter);
            UpdateEvents(events);
        }

        private void OnRefreshCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //btnRefresh.StopProgress();
        }
    }
}
