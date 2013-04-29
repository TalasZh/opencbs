using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Octopus.CoreDomain.Accounting;
using Octopus.Services;

namespace Octopus.GUI.Accounting
{
    public partial class AddStandardBooking : Form
    {
        public AddStandardBooking()
        {
            InitializeComponent();
            InitializeAccounts();
        }

        public Booking StandardBooking
        {
            get { return GetStandardBooking(); }
            set { SetStandardBooking(value); }
        }

        private void InitializeAccounts()
        {
            List<Account> accounts =
                ServicesProvider.GetInstance().GetChartOfAccountsServices().FindAllAccounts().ToList();
            comboBoxCredit.DataSource = accounts;
            comboBoxDebit.DataSource = accounts.ToList();
        }

        private void SetStandardBooking(Booking pBooking)
        {
            textBoxName.Text = pBooking.Name;
            comboBoxCredit.SelectedItem = comboBoxCredit.Items.OfType<Account>().FirstOrDefault(item => item.Number == pBooking.CreditAccount.Number);
            comboBoxDebit.SelectedItem = comboBoxDebit.Items.OfType<Account>().FirstOrDefault(item => item.Number == pBooking.DebitAccount.Number);
        }

        private Booking GetStandardBooking()
        {
            return new Booking
            {
                Name = textBoxName.Text,
                CreditAccount = comboBoxCredit.SelectedItem as Account,
                DebitAccount = comboBoxDebit.SelectedItem as Account
            }; 
        }
    }
}
