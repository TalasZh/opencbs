// LICENSE PLACEHOLDER

using System.Windows.Forms;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Services;
using System.Collections.Generic;

namespace OpenCBS.GUI.Accounting
{
    public partial class ClosureBookings : Form
    {
        public ClosureBookings()
        {
            InitializeComponent();
        }

        public ClosureBookings(int closureId)
        {
            InitializeComponent();
            olvBookings.SetObjects(ServicesProvider.GetInstance().GetAccountingServices().SelectClosureMovments(closureId));
        }
    }
}
