using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Octopus.CoreDomain.Accounting;
using Octopus.Services;

namespace Octopus.GUI.Accounting
{
    public partial class ManualEntries : Form
    {
        public ManualEntries()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAddRule_Click(object sender, EventArgs e)
        {
            AddBooking addBooking =  new AddBooking();
            addBooking.ShowDialog();
            InititaliseEntries();
        }

        private void InititaliseEntries()
        {
            lvlMovements.Items.Clear();
            List<CoreDomain.Accounting.FiscalYear> fiscalYears = ServicesProvider.GetInstance().GetChartOfAccountsServices().SelectFiscalYears();
            List<Booking> bookings = ServicesProvider.GetInstance().GetAccountingServices().SelectMovements(true, null, fiscalYears);

            if(bookings != null)
                foreach (Booking booking in bookings)
                {
                    ListViewItem item = new ListViewItem(booking.Id.ToString());

                    item.SubItems.Add(booking.DebitAccount.ToString());
                    item.SubItems.Add(booking.CreditAccount.ToString());
                    item.SubItems.Add(booking.Amount.GetFormatedValue(booking.Currency.UseCents));
                    item.SubItems.Add(booking.Currency.Code);
                    item.SubItems.Add(booking.Description);
                    item.SubItems.Add(booking.Date.ToShortDateString());
                    item.SubItems.Add(booking.ExchangeRate.ToString());
                    item.SubItems.Add(booking.User.ToString());
                    item.SubItems.Add(booking.Branch.Code);
                    lvlMovements.Items.Add(item);
                }
        }

        private void ManualEntries_Load(object sender, EventArgs e)
        {
            InititaliseEntries();
        }
    }
}
