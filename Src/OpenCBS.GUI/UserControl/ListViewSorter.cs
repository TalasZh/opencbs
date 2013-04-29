using System;
using System.Windows.Forms;

namespace Octopus.GUI.UserControl
{
    public class ListViewSorter : System.Collections.IComparer
    {
        public int Compare(object o1, object o2)
        {
            if (!(o1 is ListViewItem))
                return (0);
            if (!(o2 is ListViewItem))
                return (0);

            int result=-1;
            bool resolved = false;
            if (!reset)
            {
                try
                {
                    switch (dataType)
                    {
                        case "Date":
                            // Parse the two objects passed as a parameter as a DateTime.
                            DateTime firstDate =
                                DateTime.Parse(((ListViewItem) o2).SubItems[ByColumn].Text);
                            DateTime secondDate =
                                DateTime.Parse(((ListViewItem) o1).SubItems[ByColumn].Text);
                            // Compare the two dates.
                            result = DateTime.Compare(firstDate, secondDate);
                            break;
                        case "Int32":
                            // Parse the two objects passed as a parameter as a int.
                            Int32 firstNum =
                                Int32.Parse(((ListViewItem) o2).SubItems[ByColumn].Text);
                            Int32 secondNum =
                                Int32.Parse(((ListViewItem) o1).SubItems[ByColumn].Text);
                            // Compare the two dates.
                            result = firstNum.CompareTo(secondNum);
                            break;
                        case "Double":
                            // Parse the two objects passed as a parameter as a int.
                            Double firsDouble =
                                Double.Parse(((ListViewItem) o2).SubItems[ByColumn].Text);
                            Double secondDouble =
                                Double.Parse(((ListViewItem) o1).SubItems[ByColumn].Text);
                            // Compare the two dates.
                            result = firsDouble.CompareTo(secondDouble);
                            break;
                        default:
                            result = String.Compare(((ListViewItem) o2).SubItems[ByColumn].Text,
                                                    ((ListViewItem) o1).SubItems[ByColumn].Text);
                            break;
                    }
                }
                catch
                {
                    result = String.Compare(((ListViewItem)o2).SubItems[ByColumn].Text,
                                            ((ListViewItem)o1).SubItems[ByColumn].Text);
                    dataType = "string";
                }
            }
            else
            {


                try
                {
                    // Parse the two objects passed as a parameter as a DateTime.
                    DateTime firstDate =
                        DateTime.Parse(((ListViewItem) o2).SubItems[ByColumn].Text);
                    DateTime secondDate =
                        DateTime.Parse(((ListViewItem) o1).SubItems[ByColumn].Text);
                    // Compare the two dates.
                    result = DateTime.Compare(firstDate, secondDate);
                    resolved = true;
                    dataType = "Date";
                }
                    // If neither compared object has a valid date format, compare
                    // as a string.
                catch
                {
                    ;
                }
                if (!resolved)
                {
                    try
                    {
                        // Parse the two objects passed as a parameter as a int.
                        Int32 firstNum =
                            Int32.Parse(((ListViewItem) o2).SubItems[ByColumn].Text);
                        Int32 secondNum =
                            Int32.Parse(((ListViewItem) o1).SubItems[ByColumn].Text);
                        // Compare the two dates.
                        result = firstNum.CompareTo(secondNum);
                        resolved = true;
                        dataType = "Int32";
                    }
                    catch
                    {
                        ;
                    }
                }
                if (!resolved)
                {
                    try
                    {
                        // Parse the two objects passed as a parameter as a int.
                        Double firstNum =
                            Double.Parse(((ListViewItem) o2).SubItems[ByColumn].Text);
                        Double secondNum =
                            Double.Parse(((ListViewItem) o1).SubItems[ByColumn].Text);
                        // Compare the two dates.
                        result = firstNum.CompareTo(secondNum);
                        resolved = true;
                        dataType = "Double";
                    }
                    catch
                    {
                        result = String.Compare(((ListViewItem) o2).SubItems[ByColumn].Text,
                                                ((ListViewItem) o1).SubItems[ByColumn].Text);
                        dataType = "string";
                    }
                }
            }
            //if (lvi1.ListView.Sorting == SortOrder.Ascending)
            //    result = String.Compare(str1, str2);
            //else
            //    result = String.Compare(str2, str1);

             //Determine whether the sort order is descending.
            if (((ListViewItem)o2).ListView.Sorting == SortOrder.Descending)
                // Invert the value returned by String.Compare.
                result *= -1;
            LastSort = ByColumn;
            reset = false;
            return (result);
        }


        public int ByColumn
        {
            get { return Column; }
            set { Column = value; }
        }
        int Column = 0;

        public int LastSort
        {
            get { return LastColumn; }
            set { LastColumn = value; }
        }
        int LastColumn = 0;
        public bool reset;
        private string dataType = "";
    }   
}
