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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenCBS.GUI.Tools
{
    public partial class frmProgress : Form
    {
        public frmProgress(string pTitle, Object pTag)
        {
            _title = pTitle;
            _tag = pTag;

            InitializeComponent();
        }

        private string _title;
        private object _tag;


        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ProgressEventArg arg = new ProgressEventArg();
            arg.Worker = backgroundWorker;
            arg.Tag = _tag;
            if (DoWork != null) DoWork(this, arg);
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }

        private void frmProgress_Load(object sender, EventArgs e)
        {
            lblTitle.Text = _title;
            timer.Enabled = true;
        }

        public sealed class ProgressEventArg : EventArgs
        {
            public BackgroundWorker Worker;
            public Object Tag;
        }

        public EventHandler<ProgressEventArg> DoWork;

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;
            backgroundWorker.RunWorkerAsync();
        }
    }
}
