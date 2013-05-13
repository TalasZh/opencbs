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
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;

namespace SyntaxHighlighter
{
	public class SyntaxRichTextBox : System.Windows.Forms.RichTextBox
	{
		private SyntaxSettings m_settings = new SyntaxSettings();
		private static bool m_bPaint = true;
		private string m_strLine = "";
		private int m_nContentLength = 0;
		private int m_nLineLength = 0;
		private int m_nLineStart = 0;
		private int m_nLineEnd = 0;
		private string m_strKeywords = "";
		private int m_nCurSelection = 0;
	    private bool textProcessed;
        private int sartPos = 0;

        public SyntaxRichTextBox()
        {
            this.TextProcessed = false;
        }
		/// <summary>
		/// The settings.
		/// </summary>
		public SyntaxSettings Settings
		{
			get { return m_settings; }
		}
		
		/// <summary>
		/// WndProc
		/// </summary>
		/// <param name="m"></param>
		protected override void WndProc(ref System.Windows.Forms.Message m)
		{
			if (m.Msg == 0x00f)
			{
				if (m_bPaint)
					base.WndProc(ref m);
				else
					m.Result = IntPtr.Zero;
			}
			else
				base.WndProc(ref m);
		}

        private bool TextProcessed
        {
            set { textProcessed = value; }
            get {return textProcessed;}
        }

        protected override void OnTextChanged(EventArgs e)
        {
            // Calculate shit here.
            m_nContentLength = this.TextLength;

            int nCurrentSelectionStart = SelectionStart;
            int nCurrentSelectionLength = SelectionLength;

            m_bPaint = false;

            // Find the start of the current line.
            if (TextProcessed)
            {
                m_nLineStart = nCurrentSelectionStart;
                while ((m_nLineStart > 0) && (Text[m_nLineStart - 1] != '\n'))
                    m_nLineStart--;
            }
            else
            {
                m_nLineStart = 0;
            }
            // Find the end of the current line.
            m_nLineEnd = nCurrentSelectionStart;
            while ((m_nLineEnd < Text.Length) && (Text[m_nLineEnd] != '\n'))
                m_nLineEnd++;
            // Calculate the length of the line.
            m_nLineLength = m_nLineEnd - m_nLineStart;
            // Get the current line.
            m_strLine = Text.Substring(m_nLineStart, m_nLineLength);

            // Process this line.
            if (TextProcessed)
            {
                ProcessLine();
            }
            else
            {
                ProcessAllLines();
            }

            m_bPaint = true;
        }

        delegate void SelectionStartDelegat(int val);
        private void setSelectionStart(int val)
        {
            if (this.InvokeRequired)
            {
                // this is worker thread
                SelectionStartDelegat del = new SelectionStartDelegat(setSelectionStart);
                this.Invoke(del, new object[] {val});
            }
            else
            {
                // this is UI thread
                this.SelectionStart = val;
            }
        }

        delegate void getSelectionStartDelegat();
        private void getSelectionStart()
        {
            if (this.InvokeRequired)
            {
                // this is worker thread
                getSelectionStartDelegat del = new getSelectionStartDelegat(getSelectionStart);
                this.Invoke(del);
            }
            else
            {
                // this is UI thread
                sartPos = this.SelectionStart;
            }
        }

        delegate void SelectionLengthDelegat(int val);
        private void setSelectionLength(int val)
        {
            if (this.InvokeRequired)
            {
                // this is worker thread
                SelectionLengthDelegat del = new SelectionLengthDelegat(setSelectionLength);
                this.Invoke(del, new object[] { val });
            }
            else
            {
                // this is UI thread
                this.SelectionLength = val;
            }
        }

        delegate void SelectionColorDelegat(Color val);
        private void setSelectionColor(Color val)
        {
            if (this.InvokeRequired)
            {
                // this is worker thread
                SelectionColorDelegat del = new SelectionColorDelegat(setSelectionColor);
                this.Invoke(del, new object[] { val });
            }
            else
            {
                // this is UI thread
                this.SelectionColor = val;
            }
        }

		/// <summary>
		/// Process a line.
		/// </summary>
		private void ProcessLine()
		{
		        // Save the position and make the whole line black
                getSelectionStart();
                int nPosition = sartPos;//getControlData();

                setSelectionStart(m_nLineStart);
                setSelectionLength(m_nLineLength);
                setSelectionColor(Color.Black);

                //Process the keywords
                ProcessRegex(m_strKeywords, Settings.KeywordColor);

                // Process numbers
                if (Settings.EnableIntegers)
                    ProcessRegex("\\b(?:[0-9]*\\.)?[0-9]+\\b", Settings.IntegerColor);

                // Process strings
                if (Settings.EnableStrings)
                    ProcessRegex("\"[^\"\\\\\\r\\n]*(?:\\\\.[^\"\\\\\\r\\n]*)*\"", Settings.StringColor);

                // Process comments
                if (Settings.EnableComments && !string.IsNullOrEmpty(Settings.Comment))
                    ProcessRegex(Settings.Comment + ".*$", Settings.CommentColor);

                setSelectionStart(nPosition);
                setSelectionLength(0);
                setSelectionColor(Color.Black);

                m_nCurSelection = nPosition;
		}
		/// <summary>
		/// Process a regular expression.
		/// </summary>
		/// <param name="strRegex">The regular expression.</param>
		/// <param name="color">The color.</param>
		private void ProcessRegex(string strRegex, Color color)
		{
			Regex regKeywords = new Regex(strRegex, RegexOptions.IgnoreCase | RegexOptions.Compiled);
			Match regMatch;

			for (regMatch = regKeywords.Match(m_strLine); regMatch.Success; regMatch = regMatch.NextMatch())
			{
				// Process the words
				int nStart = m_nLineStart + regMatch.Index;
				int nLenght = regMatch.Length;
				SelectionStart = nStart;
				SelectionLength = nLenght;
				SelectionColor = color;
			}
		}
		/// <summary>
		/// Compiles the keywords as a regular expression.
		/// </summary>
		public void CompileKeywords()
		{
			for (int i = 0; i < Settings.Keywords.Count; i++)
			{
				string strKeyword = Settings.Keywords[i];

				if (i == Settings.Keywords.Count-1)
					m_strKeywords += "\\b" + strKeyword + "\\b";
				else
					m_strKeywords += "\\b" + strKeyword + "\\b|";
			}
		}

        public void ProcessAllLines()
        {
            m_bPaint = false;

            int nStartPos = 0;
            SelectionStart = 0;
            int i = 0;
            
                int nOriginalPos = SelectionStart;
                while (i < Lines.Length)
                {
                    m_strLine = Lines[i];
                    m_nLineStart = nStartPos;
                    m_nLineEnd = m_nLineStart + m_strLine.Length;

                    if (m_strLine.Trim().Length > 0)
                    {
                        ProcessLine();
                    }
                    i++;

                    nStartPos += m_strLine.Length + 1;
                }

                m_bPaint = true;

                if (this.TextLength > 0)
                    TextProcessed = true;
            
        }
	}

	/// <summary>
	/// Class to store syntax objects in.
	/// </summary>
	public class SyntaxList
	{
		public List<string> m_rgList = new List<string>();
		public Color m_color = new Color();
	}

	/// <summary>
	/// Settings for the keywords and colors.
	/// </summary>
	public class SyntaxSettings
	{
		SyntaxList m_rgKeywords = new SyntaxList();
		string m_strComment = "";
		Color m_colorComment = Color.Green;
		Color m_colorString = Color.Gray;
		Color m_colorInteger = Color.Red;
		bool m_bEnableComments = true;
		bool m_bEnableIntegers = true;
		bool m_bEnableStrings = true;

		#region Properties
		/// <summary>
		/// A list containing all keywords.
		/// </summary>
		public List<string> Keywords
		{
			get { return m_rgKeywords.m_rgList; }
		}
		/// <summary>
		/// The color of keywords.
		/// </summary>
		public Color KeywordColor
		{
			get { return m_rgKeywords.m_color; }
			set { m_rgKeywords.m_color = value; }
		}
		/// <summary>
		/// A string containing the comment identifier.
		/// </summary>
		public string Comment
		{
			get { return m_strComment; }
			set { m_strComment = value; }
		}
		/// <summary>
		/// The color of comments.
		/// </summary>
		public Color CommentColor
		{
			get { return m_colorComment; }
			set { m_colorComment = value; }
		}
		/// <summary>
		/// Enables processing of comments if set to true.
		/// </summary>
		public bool EnableComments
		{
			get { return m_bEnableComments; }
			set { m_bEnableComments = value; }
		}
		/// <summary>
		/// Enables processing of integers if set to true.
		/// </summary>
		public bool EnableIntegers
		{
			get { return m_bEnableIntegers; }
			set { m_bEnableIntegers = value; }
		}
		/// <summary>
		/// Enables processing of strings if set to true.
		/// </summary>
		public bool EnableStrings
		{
			get { return m_bEnableStrings; }
			set { m_bEnableStrings = value; }
		}
		/// <summary>
		/// The color of strings.
		/// </summary>
		public Color StringColor
		{
			get { return m_colorString; }
			set { m_colorString = value; }
		}
		/// <summary>
		/// The color of integers.
		/// </summary>
		public Color IntegerColor
		{
			get { return m_colorInteger; }
			set { m_colorInteger = value; }
		}
		#endregion
	}
}
