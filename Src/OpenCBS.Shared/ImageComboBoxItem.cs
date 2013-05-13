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

namespace OpenCBS.Shared
{
	public class ImageComboBoxItem : object
	{
		// forecolor: transparent = inherit
		private Color forecolor = Color.FromKnownColor(KnownColor.Transparent);
		private bool mark;
		private int imageindex = -1;
		private object tag;
		private string text;		
		
		// constructors
		public ImageComboBoxItem()
		{
		}

		public ImageComboBoxItem(string Text) 
		{
			text = Text;	
		}

		public ImageComboBoxItem(string Text, int ImageIndex)
		{
			text = Text;
			imageindex = ImageIndex;
		}

		public ImageComboBoxItem(string Text, int ImageIndex, bool Mark)
		{
			text = Text;
			imageindex = ImageIndex;
			mark = Mark;
		}

		public ImageComboBoxItem(string Text, int ImageIndex, bool Mark, Color ForeColor)
		{
			text = Text;
			imageindex = ImageIndex;
			mark = Mark;
			forecolor = ForeColor;
		}

		public ImageComboBoxItem(string Text, int ImageIndex, bool Mark, Color ForeColor, object Tag)
		{
			text = Text;
			imageindex = ImageIndex;
			mark = Mark;
			forecolor = ForeColor;
			tag = Tag;
		}

		// forecolor
		public Color ForeColor 
		{
			get 
			{
				return forecolor;
			}
			set
			{
				forecolor = value;
			}
		}

		// image index
		public int ImageIndex 
		{
			get 
			{
				return imageindex;
			}
			set 
			{
				imageindex = value;
			}
		}

		// mark (bold)
		public bool Mark
		{
			get
			{
				return mark;
			}
			set
			{
				mark = value;
			}
		}

		// tag
		public object Tag
		{
			get
			{
				return tag;
			}
			set
			{
				tag = value;
			}
		}

		// item text
		public string Text 
		{
			get
			{
				return text;
			}
			set
			{
				text = value;
			}
		}
		
		// ToString() should return item text
		public override string ToString() 
		{
			return text;
		}

	}

}
