// LICENSE PLACEHOLDER

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
