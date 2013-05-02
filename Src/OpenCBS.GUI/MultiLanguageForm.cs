using System.Windows.Forms;
using OpenCBS.MultiLanguageRessources;

namespace OpenCBS.GUI
{
    public class MultiLanguageForm : Form
    {
        public string GetString(string key)
        {
            return MultiLanguageStrings.GetString(Res, key);
        }

        protected virtual string Res
        {
            get
            {
                return GetType().Name;
            }
        }
    }
}