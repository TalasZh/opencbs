//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
using Octopus.MultiLanguageRessources;
using Octopus.Services;
using Octopus.Shared;
using Octopus.Shared.Settings;
using SyntaxHighlighter;

namespace Octopus.GUI.Tools
{
    public partial class frmSQLTool : Form
    {
        public frmSQLTool()
        {
            InitializeComponent();
        }
        
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (myHighlighterRichTextBox.Text.Length > 0)
            {
                SQLToolServices sqlToolService = ServicesProvider.GetInstance().GetSQLToolServices();

                richTextBoxResult.Clear();
                richTextBoxResult.AppendText(sqlToolService.RunSQL(myHighlighterRichTextBox.Text));
            }
        }

        private void myHighlighterRichTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                toolStripButton1_Click(this, null);
            }
        }

        private void frmSQLTool_Load(object sender, EventArgs e)
        {
            string StFilePath = Path.Combine(UserSettings.GetTemplatePath, "operators.txt");
            StringBuilder s = new StringBuilder();
            // Add the keywords to the list.
            if (File.Exists(StFilePath))
            {
                try
                {
                    // obtain reader and file contents
                    StreamReader stream = new StreamReader(StFilePath);
                    s.Append(stream.ReadToEnd());
                }
                // handle exception if StreamReader is unavailable
                catch (IOException)
                {
                    //s.Append("Error reading from file");
                    MessageBox.Show("File does not exist");
                }
            }

            string[] operators = s.ToString().Replace("\r\n", "").Split(',');

            foreach (string key in operators)
            {
                myHighlighterRichTextBox.Settings.Keywords.Add(key);
            }

            // Set the comment identifier. (--). 
            myHighlighterRichTextBox.Settings.Comment = "--";

            // Set the colors that will be used.
            myHighlighterRichTextBox.Settings.KeywordColor = Color.Blue;
            myHighlighterRichTextBox.Settings.CommentColor = Color.Green;
            myHighlighterRichTextBox.Settings.StringColor = Color.Maroon;
            myHighlighterRichTextBox.Settings.IntegerColor = Color.Red;

            // Let's not process strings and integers.
            myHighlighterRichTextBox.Settings.EnableStrings = true;
            myHighlighterRichTextBox.Settings.EnableIntegers = true;

            // Let's make the settings we just set valid by compiling
            // the keywords to a regular expression.
            myHighlighterRichTextBox.CompileKeywords();  
        }
    }
}
