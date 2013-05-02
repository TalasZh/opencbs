using System;
using System.Windows.Forms;
using System.Collections;

namespace OpenCBS.GUI
{
    // Implements the manual sorting of items by column.
    class ListViewItemComparer : IComparer
    {
        private int _col;
        private SortOrder _order;

        public ListViewItemComparer()
        {
            _col = 0;
            _order = SortOrder.Ascending;
        }
        public ListViewItemComparer(int column, SortOrder order)
        {
            _col = column;
            this._order = order;
        }
        public int Compare(object x, object y) 
    {
        int returnVal= -1;
        returnVal = String.Compare(((ListViewItem)x).SubItems[_col].Text,
                                ((ListViewItem)y).SubItems[_col].Text);
        // Determine whether the sort order is descending.
        if (_order == SortOrder.Descending)
            // Invert the value returned by String.Compare.
            returnVal *= -1;
        return returnVal;
    }

    }
}
