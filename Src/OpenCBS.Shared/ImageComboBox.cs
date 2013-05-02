// LICENSE PLACEHOLDER

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
