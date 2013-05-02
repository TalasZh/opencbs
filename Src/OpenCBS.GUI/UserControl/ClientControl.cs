// LICENSE PLACEHOLDER

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
