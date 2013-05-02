using OpenCBS.CoreDomain.SearchResult;
using OpenCBS.Enums;
using OpenCBS.GUI.UserControl;
using OpenCBS.Services;

namespace OpenCBS.GUI.Search
{
    public class CFSearchClientForm : SearchClientForm
    {
        public CFSearchClientForm() : base(OClientTypes.Person, true)
        {
            Value = CustomClientField.Empty;
        }

        protected override void HandleTierSelect(ClientSearchResult pClient)
        {
            var clientServices = ServicesProvider.GetInstance().GetClientServices();
            var person = clientServices.FindPersonById(pClient.Id);
            Value = new CustomClientField(person);
        }

        public CustomClientField Value { get; private set; }

        protected override string Res
        {
            get
            {
                return typeof(SearchClientForm).Name;
            }
        }
    }
}
