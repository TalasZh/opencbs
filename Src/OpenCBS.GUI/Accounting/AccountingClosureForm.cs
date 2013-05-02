// LICENSE PLACEHOLDER

using System;
using System.Drawing;
using System.Windows.Forms;
using BrightIdeasSoftware;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Services;

namespace OpenCBS.GUI.Accounting
{
    public partial class AccountingClosureForm : Form
    {
        private void LoadClosureData()
        {
            olvClosures.SetObjects(ServicesProvider.GetInstance().GetAccountingServices().SelectAccountingClosures());
        }

        public AccountingClosureForm()
        {
            InitializeComponent();
            LoadClosureData();
        }

        private void BtnGenerateEventsClick(object sender, EventArgs e)
        {
            AccountingJournals accountingJournals = new AccountingJournals(1);
            accountingJournals.ShowDialog();
        }

        private void BtnAddRuleClick(object sender, EventArgs e)
        {
            AccountingJournals accountingJournals = new AccountingJournals(0);
            accountingJournals.ShowDialog();
            olvClosures.SetObjects(ServicesProvider.GetInstance().GetAccountingServices().SelectAccountingClosures());
        }

        private void SweetButton1Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnViewClick(object sender, EventArgs e)
        {
            if (olvClosures.SelectedObject != null)
            {
                ClosureBookings closureBookings =
                    new ClosureBookings(((AccountingClosure) olvClosures.SelectedObject).Id);
                closureBookings.ShowDialog();
            }
        }

        private void olvClosures_DoubleClick(object sender, EventArgs e)
        {
            if (olvClosures.SelectedObject != null)
            {
                ClosureBookings closureBookings =
                    new ClosureBookings(((AccountingClosure)olvClosures.SelectedObject).Id);
                closureBookings.ShowDialog();
            }
        }

        private void btnDeleteRule_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to delete closure?", "Closure", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                if (olvClosures.SelectedObject != null)
                {
                    ServicesProvider.GetInstance().GetAccountingServices().DeleteClosure(
                        ((AccountingClosure) olvClosures.SelectedObject).Id);
                    LoadClosureData();
                }
            }
        }

        private void olvClosures_FormatRow(object sender, FormatRowEventArgs e)
        {
            AccountingClosure closure = (AccountingClosure)e.Model;

            if(closure.Deleted)
                e.Item.ForeColor = Color.Gray;
        }
    }
}
