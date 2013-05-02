using System;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Services;
using OpenCBS.Services.Accounting;

namespace OpenCBS.GUI.Accounting
{
    public partial class StandardBooking : MultiLanguageForm
    {
        private StandardBookingServices standardBookingServices;

        public StandardBooking()
        {
            standardBookingServices = ServicesProvider.GetInstance().GetStandardBookingServices();
            InitializeComponent();
            LoadBookings();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddStandardBooking()
        {
            AddStandardBooking addStandardBooking = new AddStandardBooking();
            while (addStandardBooking.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    standardBookingServices.CreateStandardBooking(addStandardBooking.StandardBooking);
                    LoadBookings();
                    string message = GetString("Created");
                    string caption = GetString("Message");
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
                catch (Exception e)
                {
                    addStandardBooking.Show(this);
                    new frmShowError(CustomExceptionHandler.ShowExceptionText(e)).ShowDialog();
                    addStandardBooking.Hide();
                }
            }
        }

        private void EditStandardBooking()
        {
            if (listBookings.SelectedItems.Count == 1)
            {
                Booking booking = (Booking)listBookings.SelectedItems[0].Tag;
                AddStandardBooking editStandardBooking = new AddStandardBooking { StandardBooking = booking };
                while (editStandardBooking.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        var modifiedBooking = editStandardBooking.StandardBooking;
                        modifiedBooking.Id = booking.Id;
                        standardBookingServices.UpdateStandardBookings(modifiedBooking);
                        LoadBookings();
                        string message = GetString("ChangesSaved");
                        string caption = GetString("Message");
                        MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                    catch (Exception e)
                    {
                        editStandardBooking.Show(this);
                        new frmShowError(CustomExceptionHandler.ShowExceptionText(e)).ShowDialog();
                        editStandardBooking.Hide();
                    }
                }
            }
        }

        private void DeleteStandardBooking()
        {
            string message = GetString("ConfirmDelete");
            string caption = GetString("Confirmation");
            if (listBookings.SelectedItems.Count > 0)
            {
                if (MessageBox.Show(message, caption, 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        Booking booking = (Booking)listBookings.SelectedItems[0].Tag;
                        standardBookingServices.DeleteStandardBooking(booking.Id);
                        LoadBookings();
                        message = GetString("Deleted");
                        caption = GetString("Message");
                        MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception e)
                    {
                        new frmShowError(CustomExceptionHandler.ShowExceptionText(e)).ShowDialog();
                    }
                }
            }
        }

        private void AddListViewItem(Booking booking)
        {
            ListViewItem listViewItem = new ListViewItem(booking.Name);
            listViewItem.SubItems.Add(booking.DebitAccount.Number + " : " + booking.DebitAccount.Label);
            listViewItem.SubItems.Add(booking.CreditAccount.Number + " : " + booking.CreditAccount.Label);
            listViewItem.Tag = booking;
            listBookings.Items.Add(listViewItem);
        }

        private void LoadBookings()
        {
            listBookings.Items.Clear();

            foreach (Booking booking in standardBookingServices.SelectAllStandardBookings())
            {
                AddListViewItem(booking);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteStandardBooking();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditStandardBooking();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            AddStandardBooking();
        }
    }
}
