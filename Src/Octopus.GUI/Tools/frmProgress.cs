using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Octopus.GUI.Tools
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