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

using System.Windows.Forms;
using System.Diagnostics;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Services;

namespace OpenCBS.GUI.UserControl
{
    public partial class ClientControl : System.Windows.Forms.UserControl
    {
        protected TabControl Tabs { get; set; }
        protected IClient Client { get; set; }
        
        public ClientControl()
        {
            InitializeComponent();
        }

        protected void InitDocuments()
        {
            Debug.Assert(Tabs != null, "TabControl is null");
            Debug.Assert(Client != null, "Client is null");

            PicturesServices ps = ServicesProvider.GetInstance().GetPicturesServices();
            if (!ps.IsEnabled()) return;

            
        
           
        }

   }
}
