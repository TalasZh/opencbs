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

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using OpenCBS.Enums;
using OpenCBS.Extensions;

namespace OpenCBS.CoreDomain.Extensions
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
