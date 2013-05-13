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
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace OpenCBS.GUI.Restarter
{
    public partial class MainForm : Form
    {
        private static readonly string OPENCBSEXE = "OpenCBS.GUI.exe";
        private static readonly string ONLINE = " -online";
        private bool _onlineMode;
        public MainForm(bool pOnlineMode)
        {
            _onlineMode = pOnlineMode;
            InitializeComponent();
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            Process[] procs  = Process.GetProcessesByName(OPENCBSEXE);
            if (procs.Length == 0)
            {
                string path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                path = Path.Combine(path, OPENCBSEXE);
                if (_onlineMode)
                    Process.Start(path, ONLINE);
                else
                    Process.Start(path);

                Application.Exit();
            }
        }

    }
}
