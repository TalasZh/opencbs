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
using System.Drawing;
using System.Windows.Forms;

namespace OpenCBS.Shared
{
	public class ImageComboBox : ComboBox
	{
		private ImageList imgs = new ImageList();

		// constructor
		public ImageComboBox()
		{
			// set draw mode to owner draw
			DrawMode = DrawMode.OwnerDrawFixed;	
		}

		// ImageList property
		public ImageList ImageList 
		{
			get 
			{
				return imgs;
			}
			set 
			{
				imgs = value;
			}
		}

		// customized drawing process
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			// draw background & focus rect
			e.DrawBackground();
			e.DrawFocusRectangle();

			// check if it is an item from the Items collection
			if (e.Index < 0)

				// not an item, draw the text (indented)
				e.Graphics.DrawString(Text, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + imgs.ImageSize.Width, e.Bounds.Top);

			else
			{
				
				// check if item is an ImageComboBoxItem
				if (Items[e.Index].GetType() == typeof(ImageComboBoxItem)) 
				{															

					// get item to draw
					ImageComboBoxItem item = (ImageComboBoxItem) this.Items[e.Index];

					// get forecolor & font
					Color forecolor = (item.ForeColor != Color.FromKnownColor(KnownColor.Transparent)) ? item.ForeColor : e.ForeColor;
					Font font = item.Mark ? new Font(e.Font, FontStyle.Regular) : e.Font;

					// -1: no image
					if (item.ImageIndex != -1) 
					{
						// draw image, then draw text next to it
						ImageList.Draw(e.Graphics, e.Bounds.Left, e.Bounds.Top + 2, item.ImageIndex);
						e.Graphics.DrawString(item.Text, font, new SolidBrush(forecolor), e.Bounds.Left + imgs.ImageSize.Width + 5, e.Bounds.Top + 2);
					}
					else
						// draw text (indented)
						e.Graphics.DrawString(item.Text, font, new SolidBrush(forecolor), e.Bounds.Left + imgs.ImageSize.Width +5, e.Bounds.Top + 2);

				}
				else
				
					// it is not an ImageComboBoxItem, draw it
					e.Graphics.DrawString(Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + imgs.ImageSize.Width + 5, e.Bounds.Top + 2);
				
			}

			base.OnDrawItem (e);
		}
		
	}

}
