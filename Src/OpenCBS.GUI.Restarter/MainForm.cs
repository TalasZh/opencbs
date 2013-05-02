// LICENSE PLACEHOLDER

using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace OpenCBS.GUI.Restarter
{
    public partial class MainForm : Form
    {
        private static readonly string OCTOPUSEXE = "OpenCBS.GUI.exe";
        private static readonly string ONLINE = " -online";
        private bool _onlineMode;
        public MainForm(bool pOnlineMode)
        {
            _onlineMode = pOnlineMode;
            InitializeComponent();
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            // Is Octopus.Gui.Exe still running ?
            Process[] procs  = Process.GetProcessesByName(OCTOPUSEXE);
            if (procs.Length == 0)
            {
                string path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                path = Path.Combine(path, OCTOPUSEXE);
                if (_onlineMode)
                    Process.Start(path, ONLINE);
                else
                    Process.Start(path);

                Application.Exit();
            }
        }

    }
}
