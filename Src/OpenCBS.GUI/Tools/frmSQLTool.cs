// LICENSE PLACEHOLDER

using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Services;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;
using SyntaxHighlighter;

namespace OpenCBS.GUI.Tools
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
