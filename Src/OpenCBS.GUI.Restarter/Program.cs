// LICENSE PLACEHOLDER

using System;
using System.Windows.Forms;

namespace OpenCBS.GUI.Restarter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] pArgs)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool onlineMode = (pArgs.Length > 0 && pArgs[0] == "-online");
            Application.Run(new MainForm(onlineMode));
        }
    }
}
