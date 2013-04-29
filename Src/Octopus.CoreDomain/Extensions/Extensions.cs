using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Octopus.Enums;
using Octopus.Extensions;

namespace Octopus.CoreDomain.Extensions
{
    public class Extension
    {
        public void Dispose()
        {
            Extensions.Clear();
            //FormExtensions.Clear();
        }

        private CompositionContainer _container;
        
        [ImportMany(typeof(IExtension), AllowRecomposition = true)]
        public List<IExtension> Extensions { get; set; }

        public void LoadExtensions()
        {
            string path = Application.StartupPath + @"\Extensions";
            if(Directory.Exists(path))
            {
                var catalog = new AggregateCatalog(
                    new AssemblyCatalog(Assembly.GetExecutingAssembly()),
                    new DirectoryCatalog(path));
                var batch = new CompositionBatch();
                batch.AddPart(this);

                _container = new CompositionContainer(catalog);

                try
                {
                    _container.Compose(batch);
                }
                catch (CompositionException compositionException)
                {
                    MessageBox.Show(compositionException.ToString());
                }
            }
        }

        public void PutControls(List<IExtension> extensions, OMenuItems menu, TabControl tabControl, int clientId, int contractId)
        {
            if (extensions != null)
            {
                foreach (var extension in extensions)
                {
                    if (extension.MainMenu == menu)
                    {
                        TabPage tabPageExtension = new TabPage();
                        tabPageExtension.SuspendLayout();
                        tabPageExtension.Name = extension.ExtensionName;
                        tabPageExtension.Text = extension.ExtensionName;
                        tabPageExtension.UseVisualStyleBackColor = true;
                        extension.ClientId = clientId;
                        extension.ContractId = contractId;
                        
                        UserControl control = (UserControl)extension;
                        control.Dock = DockStyle.Fill;
                        tabPageExtension.Controls.Add(control);
                        tabPageExtension.ResumeLayout(false);
                        tabControl.TabPages.Add(tabPageExtension);
                    }
                }
            }
        }
    }

    public class OctopusSettings
    {
        public OctopusSettings()
        {
            LoadSettings();
        }

        public void Dispose()
        {
            Settings.Clear();
        }

        private CompositionContainer _container;

        [ImportMany(typeof(ITechnicalSettings), AllowRecomposition = true)]
        public List<ITechnicalSettings> Settings { get; set; }

        public void LoadSettings()
        {
            string path = Application.StartupPath;
            if (Directory.Exists(path))
            {
                var catalog = new AggregateCatalog(
                    new AssemblyCatalog(Assembly.GetExecutingAssembly()),
                    new DirectoryCatalog(path));
                var batch = new CompositionBatch();
                batch.AddPart(this);

                _container = new CompositionContainer(catalog);

                try
                {
                    _container.Compose(batch);
                }
                catch (CompositionException compositionException)
                {
                    MessageBox.Show(compositionException.ToString());
                }
            }
        }
    }
}
