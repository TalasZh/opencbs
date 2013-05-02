// LICENSE PLACEHOLDER

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using OpenCBS.GUI.Search;

namespace OpenCBS.GUI.UserControl
{
    public class CustomizableFieldClientEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var formsEditorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            if (formsEditorService != null)
            {                
                using (var form = new CFSearchClientForm())
                {
                    if (formsEditorService.ShowDialog(form) == DialogResult.OK)
                        return form.Value;
                }
            }
            return value; // can also replace the wrapper object here
        }
    }
}
