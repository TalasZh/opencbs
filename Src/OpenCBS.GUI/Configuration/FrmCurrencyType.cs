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

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.ExceptionsHandler;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Services;

namespace OpenCBS.GUI.Configuration
{
    public partial class FrmCurrencyType : Form
    {
        private Currency _currency;
        public FrmCurrencyType()
        {
            InitializeComponent();
            LoadCurrencies();
            _currency = null;
        }

        private void ButtonAddClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show(MultiLanguageStrings.GetString(Ressource.FrmCurrencyType, "EmptyName.Text"));
                return;
            }
            if (string.IsNullOrEmpty(textBoxCode.Text))
            {
                MessageBox.Show(MultiLanguageStrings.GetString(Ressource.FrmCurrencyType, "EmptyCode.Text"));
                return;
            }
            if (_currency == null)
            {
                _currency = new Currency();
            }
            _currency.Name = textBoxName.Text;
            _currency.Code = textBoxCode.Text;
            _currency.IsPivot = radioButtonYes.Checked;
            _currency.IsSwapped = radioButtonSwappedYes.Checked;
            _currency.UseCents = chkUseCents.Checked;

            try
            {
                if (_currency.Id > 0)
                ServicesProvider.GetInstance().GetCurrencyServices().UpdateNewCurrency(_currency);                    
                
                LoadCurrencies();
                
                if (_currency.IsPivot)
                {
                    radioButtonNo.Checked = true;
                    radioButtonYes.Checked = false;
                    groupBoxSetAsPivot.Enabled = radioButtonYes.Enabled = radioButtonNo.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }

            textBoxName.Text = "";
            textBoxCode.Text = "";
            _currency = null;
            btnUpdate.Enabled = false;
            btnAdd.Enabled = true;
        }

        private void LoadCurrencies()
        {
            listViewCurrencies.Items.Clear();
            List<Currency> list = ServicesProvider.GetInstance().GetCurrencyServices().FindAllCurrencies();
            foreach (Currency currency in list)
            {
                ListViewItem listView = new ListViewItem(currency.Name);
                listView.SubItems.Add(currency.Code);
                listView.SubItems.Add(currency.IsPivot.ToString());
                listView.SubItems.Add(currency.IsSwapped.ToString());
                listView.SubItems.Add(currency.UseCents.ToString());
                
                if(currency.IsPivot)
                {
                    groupBoxSetAsPivot.Enabled = false;
                    radioButtonNo.Enabled = false;
                    radioButtonYes.Enabled = false;
                }
                listView.Tag = currency;
                listViewCurrencies.Items.Add(listView);
            }
            btnUpdate.Enabled = false;
            btnAdd.Enabled = false;

            if (_currency != null && _currency.Id > 0)
                btnUpdate.Enabled = true;
            else
                btnAdd.Enabled = true;

            if (list.Count > 1)
                btnAdd.Enabled = false;
        }

        private void ButtonExitClick(object sender, EventArgs e)
        {  
            Close();
        }

        private void ListViewCurrenciesClick(object sender, EventArgs e)
        {
            Currency currency = (Currency)listViewCurrencies.SelectedItems[0].Tag;
            if (currency != null)
            {
                textBoxName.Text = currency.Name;
                textBoxCode.Text = currency.Code;
                radioButtonYes.Checked = currency.IsPivot;
                radioButtonSwappedYes.Checked = currency.IsSwapped;
                chkUseCents.Checked = currency.UseCents;
                _currency = currency;
            }

            btnUpdate.Enabled = false;
            btnAdd.Enabled = false;

            if (currency != null)
            {
                chkUseCents.Visible = !ServicesProvider.GetInstance().GetCurrencyServices().IsCurrencyUsed(currency.Id);
                if(currency.Id > 0)
                    btnUpdate.Enabled = true;
                else
                {
                    btnAdd.Enabled = true;
                }
            }
        }

        private void FrmCurrencyType_Load(object sender, EventArgs e)
        {
            listViewCurrencies.Items[0].Selected = true;
            ListViewCurrenciesClick(sender, e);
        }

        private void BtnAddClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show(MultiLanguageStrings.GetString(Ressource.FrmCurrencyType, "EmptyName.Text"));
                return;
            }
            if (string.IsNullOrEmpty(textBoxCode.Text))
            {
                MessageBox.Show(MultiLanguageStrings.GetString(Ressource.FrmCurrencyType, "EmptyCode.Text"));
                return;
            }
            if (_currency == null)
            {
                _currency = new Currency();
            }
            _currency.Name = textBoxName.Text;
            _currency.Code = textBoxCode.Text;
            _currency.IsPivot = radioButtonYes.Checked ? true : false;
            _currency.IsSwapped = radioButtonSwappedYes.Checked ? true : false;
            _currency.UseCents = chkUseCents.Checked;

            try
            {
                if (_currency.Id < 1)
                    _currency.Id = ServicesProvider.GetInstance().GetCurrencyServices().AddNewCurrency(_currency);

                LoadCurrencies();

                if (_currency.IsPivot)
                {
                    radioButtonNo.Checked = true;
                    radioButtonYes.Checked = false;
                    groupBoxSetAsPivot.Enabled = radioButtonYes.Enabled = radioButtonNo.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }

            textBoxName.Text = "";
            textBoxCode.Text = "";
            _currency = null;
            btnUpdate.Enabled = false;
            btnAdd.Enabled = false;
        }
    }
}
