// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Clients;

namespace OpenCBS.GUI.UserControl
{
    public partial class SimilarIDForm : Form
    {
        public SimilarIDForm(List<Person> pPersons)
        {
            InitializeComponent();
            _InitializeList(pPersons);
        }
        private void _InitializeList(List<Person> pPersons)
        {
            foreach (Person person in pPersons)
            {
                ListViewItem li = new ListViewItem(person.IdentificationData);
                li.SubItems.Add(person.FirstName);
                li.SubItems.Add(person.LastName);
                li.Tag = person;
                listViewPersons.Items.Add(li);
            }

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
