using System.Windows.Forms;
using Octopus.MultiLanguageRessources;

namespace Octopus.GUI
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