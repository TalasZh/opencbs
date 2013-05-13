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
using OpenCBS.Shared;

namespace OpenCBS.GUI.Tools
{
    public partial class OMonthCalendar : MonthCalendar
    {
        public OMonthCalendar()
        {
            InitializeComponent();
        }

        // Override WndProc and force a call to OnPaint when we get a WM_PAINT 
        protected static int WM_PAINT = 0x000F;

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_PAINT)
            {
                Graphics graphics = Graphics.FromHwnd(this.Handle);
                PaintEventArgs pe = new PaintEventArgs(graphics, new Rectangle(0, 0, this.Width, this.Height));
                OnPaint(pe);
            }
        }

        private List<DateTime> _warningDates = new List<DateTime>();

        public List<DateTime> WarningDates
        {
            get
            {
                return _warningDates;
            }
            set
            {
                _warningDates = value;
            }
        }

        private Color _warningBackColor = Color.Red;

        public Color WarningBackColor
        {
            get { return _warningBackColor; }
            set { _warningBackColor = value; }
        }

        private List<DateTime> _warningDates1 = new List<DateTime>();

        public List<DateTime> WarningDates1
        {
            get
            {
                return _warningDates1;
            }
            set
            {
                _warningDates1 = value;
            }
        }

        private Color _warningBackColor1 = Color.Red;

        public Color WarningBackColor1
        {
            get { return _warningBackColor1; }
            set { _warningBackColor1 = value; }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;

            int dayBoxWidth = 0;
            float dayBoxHeight = 0;
            int firstWeekPosition = 0;
            int lastWeekPosition = Height;

            if ( (WarningDates.Count > 0) || (WarningDates1.Count > 0))
            {
                SelectionRange calendarRange = GetDisplayRange(false);

                // Create a list of those dates that actually should be marked as warnings. 
                List<DateTime> visibleWarningDates = new List<DateTime>();
                foreach (DateTime date in WarningDates)
                {
                    if (date >= calendarRange.Start && date <= calendarRange.End)
                    {
                        visibleWarningDates.Add(date);
                    }
                }

                // Create a list of those dates that actually should be marked as warnings. 
                List<DateTime> visibleWarningDates1 = new List<DateTime>();
                foreach (DateTime date in WarningDates1)
                {
                    if (date >= calendarRange.Start && date <= calendarRange.End)
                    {
                        visibleWarningDates1.Add(date);
                    }
                }

                if ((visibleWarningDates.Count > 0) || (visibleWarningDates1.Count > 0))
                {
                    HitArea hit = HitTest(25, firstWeekPosition).HitArea;
                    while ((HitTest(25, firstWeekPosition).HitArea != HitArea.PrevMonthDate
                        && HitTest(25, firstWeekPosition).HitArea != HitArea.Date)
                        && firstWeekPosition < Height)
                    {
                        firstWeekPosition++;
                    }
                    //graphics.DrawLine(Pens.Red, 0, firstWeekPosition, 100, firstWeekPosition);

                    while ((HitTest(25, lastWeekPosition).HitArea != HitArea.NextMonthDate && HitTest(25, lastWeekPosition).HitArea != HitArea.Date) && lastWeekPosition >= 0)
                    {
                        lastWeekPosition--;
                    }

                    //graphics.DrawLine(Pens.Red, 0, lastWeekPosition, 100, lastWeekPosition);

                    if (firstWeekPosition > 0 && lastWeekPosition > 0)
                    {
                        dayBoxWidth = Width / (ShowWeekNumbers ? 8 : 7);
                        dayBoxHeight = (((float)(lastWeekPosition - firstWeekPosition)) / 6.0f);

                        using (Brush brushWarning = new SolidBrush(Color.FromArgb(255, WarningBackColor)))
                        {
                            using (Brush brushWarning1 = new SolidBrush(Color.FromArgb(255, WarningBackColor1)))
                            {
                                foreach (DateTime visDate in visibleWarningDates)
                                {
                                    DrawCell(graphics, dayBoxWidth, dayBoxHeight, firstWeekPosition, calendarRange, brushWarning,
                                             visDate);
                                }
                                foreach (DateTime visDate in visibleWarningDates1)
                                {
                                    DrawCell(graphics, dayBoxWidth, dayBoxHeight, firstWeekPosition, calendarRange,
                                             brushWarning1, visDate);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void DrawCell(Graphics graphics, int dayBoxWidth, float dayBoxHeight, int firstWeekPosition, SelectionRange calendarRange, Brush brushWarning, DateTime visDate)
        {
            int row = 0;
            int col = 0;

            TimeSpan span = visDate.Subtract(calendarRange.Start);
            row = span.Days / 7;
            col = span.Days % 7;


            int x = (col + (ShowWeekNumbers ? 1 : 0)) * dayBoxWidth + 2;
            int y = firstWeekPosition + Convert.ToInt32(row * dayBoxHeight) + 1;

            if (HitTest(x + 3, y + 3).HitArea != HitArea.Date) return;

            Rectangle fillRect = new Rectangle(x, y, dayBoxWidth - 2, Convert.ToInt32(dayBoxHeight) - 2);
            graphics.FillRectangle(brushWarning, fillRect);

            // Check if the date is in the bolded dates array 
            bool makeDateBolded = false;
            foreach (DateTime boldDate in BoldedDates)
            {
                if (boldDate == visDate)
                {
                    makeDateBolded = true;
                }
            }

            using (Font textFont = new Font(Font, (makeDateBolded ? FontStyle.Bold : FontStyle.Regular)))
            {
                TextRenderer.DrawText(graphics, visDate.Day.ToString(), textFont, fillRect, Color.FromArgb(255, 128, 0, 0), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }

            if ((ShowTodayCircle) && (visDate == TimeProvider.Today))
            {
                graphics.DrawRectangle(Pens.Red, x - 1, y - 1, dayBoxWidth , Convert.ToInt32(dayBoxHeight));
                graphics.DrawRectangle(Pens.Red, x - 2, y - 2, dayBoxWidth, Convert.ToInt32(dayBoxHeight));
            }
        }

    }
}
