using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenCBS.GUI.Contracts
{
    public partial class InstallmentCommentDialog : Form
    {
        public InstallmentCommentDialog()
        {
            InitializeComponent();
        }

        public string Comment
        {
            get { return textBoxComment.Text; }
            set { textBoxComment.Text = value; }
        }

    }
}
