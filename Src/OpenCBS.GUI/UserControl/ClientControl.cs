using System.Windows.Forms;
using System.Diagnostics;
using Octopus.CoreDomain.Clients;
using Octopus.CoreDomain.Products;
using Octopus.Enums;
using Octopus.MultiLanguageRessources;
using Octopus.Services;

namespace Octopus.GUI.UserControl
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