// LICENSE PLACEHOLDER

using System.Windows.Forms;

namespace OpenCBS.Extensions
{
    public class ExtensionMenuItem
    {
        public string InsertAfter { get; set; }
        public ToolStripMenuItem MenuItem { get; set; }
    }

    public interface IMenu
    {
        ExtensionMenuItem[] GetItems();
    }
}
