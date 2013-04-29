//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright ?2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Octopus.Enums;
using Octopus.CoreDomain;
using Octopus.ExceptionsHandler;
using Octopus.GUI.Report_Browser;
using Octopus.GUI.UserControl;
using Octopus.MultiLanguageRessources;
using Octopus.Reports;
using Octopus.Reports.Forms;
using Octopus.Services;
using Octopus.CoreDomain.Accounting;
using Octopus.Services.Accounting;
using Octopus.Shared;
using BrightIdeasSoftware;
using System.Drawing.Drawing2D;
using System.Linq;
using Octopus.Shared.CSV;

namespace Octopus.GUI.Accounting
{
    /// <summary>
    /// Description résumée de AccountingSchemaAdminForm.
    /// </summary>
    public class ChartOfAccountsForm : SweetBaseForm
    {
        #region attributes
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private int sortColumn = -1;
        private IContainer components;
        private Account _account;
        private string _label;
        private string _number;
        private string _localNumber;
        #endregion

        private int _CurrencyId;
        private Panel panel1;
        private SweetButton btnClose;
        private Label _labelTitle;
        private TableLayoutPanel _tableLayoutPanel1;
        private SplitContainer splitContainer1;
        private TreeListView tlvAccounts;
        private OLVColumn olvColumnInternalAccountID;
        private OLVColumn olvColumnLabel;
        private GroupBox groupBoxActions;
        private SweetButton btnDeleteAccount;
        private SweetButton btnEditAccount;
        private SweetButton btnAddAccount;
        private SweetButton btnExport;
        private SweetButton btnImportAccounts;
        private OpenFileDialog fileDialog;
        private SweetButton btnExportAccounts;
        private OLVColumn olvColumnExportedBalance;

        public ChartOfAccountsForm(int pCurrency)
        {
            _CurrencyId = pCurrency;
            InitializeComponent();
            
            IntializeTreeViewChartOfAccounts();
        }

        private void IntializeTreeViewChartOfAccounts()
        {
            List<Account> accounts = ServicesProvider.GetInstance().GetChartOfAccountsServices().FindAllAccounts();
            List<AccountCategory> accountCategories = ServicesProvider.GetInstance().GetChartOfAccountsServices().SelectAccountCategories();

            tlvAccounts.CanExpandGetter = delegate(object o)
            {
                Account account = (Account)o;
                if (account.Id == -1)
                    return true;

                return accounts.FirstOrDefault(item => item.ParentAccountId == account.Id) != null;
            };

            tlvAccounts.ChildrenGetter = delegate(object o)
            {
                Account account = (Account)o;
                if (account.Id == -1)
                    return accounts.Where(item => item.AccountCategory == account.AccountCategory && item.ParentAccountId == null);
                
                return accounts.Where(item => item.ParentAccountId == account.Id);
            };

            tlvAccounts.RowFormatter = delegate(OLVListItem o)
            {
                Account account = (Account)o.RowObject;
                if (account.Id == -1)
                {
                    o.ForeColor = Color.FromArgb(0, 88, 56);
                    o.Font = new Font("Arial", 9);
                }
            };

            TreeListView.TreeRenderer renderer = tlvAccounts.TreeColumnRenderer;
            renderer.LinePen = new Pen(Color.Gray, 0.5f) {DashStyle = DashStyle.Dot};

            List<Account> list = new List<Account>();

            foreach(AccountCategory accountCategory in accountCategories)
            {
                string name = MultiLanguageStrings.GetString(
                    Ressource.ChartOfAccountsForm, accountCategory.Name + ".Text");
                name = name ?? accountCategory.Name;

                Account account = new Account
                    {
                        Number = name,
                        AccountCategory = (OAccountCategories) accountCategory.Id,
                        Id = -1
                    };
                
                list.Add(account);
            }
            tlvAccounts.Roots = list;
            tlvAccounts.ExpandAll();
        }

        private void AddAccount()
        {
            FrmAccount frmAddAccount = new FrmAccount(true);
            if (frmAddAccount.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    if (frmAddAccount.Mode == 0)
                    {
                        Account account = frmAddAccount.Account;
                        ServicesProvider.GetInstance().GetChartOfAccountsServices().Insert(account);
                        IntializeTreeViewChartOfAccounts();
                    }
                    else if(frmAddAccount.Mode == 1)
                    {
                        AccountCategory accountCategory = frmAddAccount.AccountCategory;
                        ServicesProvider.GetInstance().GetChartOfAccountsServices().InsertAccountCategory(accountCategory);
                        IntializeTreeViewChartOfAccounts();
                    }
                }
                catch (Exception ex)
                {
                    frmAddAccount.Show(this);
                    new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                    frmAddAccount.Hide();
                }
            }
        }

        private void EditAccount()
        {
            Account account = (Account)tlvAccounts.SelectedObject;
            if (account != null && account.Id != -1)
            {
                FrmAccount frmEditAccount = new FrmAccount (false) {Account = account};
                if (frmEditAccount.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        Account modifiedAccount = frmEditAccount.Account;
                        modifiedAccount.Id = account.Id;
                        modifiedAccount.TypeCode = account.TypeCode;
                        ServicesProvider.GetInstance().GetChartOfAccountsServices().UpdateAccount(modifiedAccount);
                        
                        IntializeTreeViewChartOfAccounts();
                    }
                    catch (Exception ex)
                    {
                        frmEditAccount.Show(this);
                        new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                        frmEditAccount.Hide();
                    }
                }
            }
        }

        private void DeleteAccount()
        {
            Account account = (Account)tlvAccounts.SelectedObject;
            if (account != null && account.Id != -1)
            {

                if (MessageBox.Show(MultiLanguageStrings.GetString(Ressource.ChartOfAccountsForm, "DeleteAccountMessage.Text"),
                        MultiLanguageStrings.GetString(Ressource.ChartOfAccountsForm, "DeleteAccountTitle.Text"),
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {

                        ServicesProvider.GetInstance().GetChartOfAccountsServices().DeleteAccount(account);
                        IntializeTreeViewChartOfAccounts();
                        MessageBox.Show(MultiLanguageStrings.GetString(Ressource.ChartOfAccountsForm, "AccountDeletedSuccessfully.Text"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                    }
                }
            }
            else if (account != null && account.Id == -1)
            {
                AccountCategory accountCategory = new AccountCategory() { Id = (int)account.AccountCategory, Name = account.Number };

                if (MessageBox.Show(MultiLanguageStrings.GetString(Ressource.ChartOfAccountsForm, "DeleteAccountCategoryMessage.Text"),
                        MultiLanguageStrings.GetString(Ressource.ChartOfAccountsForm, "DeleteAccountCategoryTitle.Text"),
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        ServicesProvider.GetInstance().GetChartOfAccountsServices().DeleteAccountCategory(accountCategory);
                        IntializeTreeViewChartOfAccounts();
                        MessageBox.Show(MultiLanguageStrings.GetString(Ressource.ChartOfAccountsForm, "AccountDeletedSuccessfully.Text"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                    }
                }
            }
        }
        
        #region Code génér?par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartOfAccountsForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tlvAccounts = new BrightIdeasSoftware.TreeListView();
            this.olvColumnInternalAccountID = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnLabel = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnExportedBalance = new BrightIdeasSoftware.OLVColumn();
            this.groupBoxActions = new System.Windows.Forms.GroupBox();
            this.btnExportAccounts = new Octopus.GUI.UserControl.SweetButton();
            this.btnImportAccounts = new Octopus.GUI.UserControl.SweetButton();
            this.btnDeleteAccount = new Octopus.GUI.UserControl.SweetButton();
            this.btnEditAccount = new Octopus.GUI.UserControl.SweetButton();
            this.btnAddAccount = new Octopus.GUI.UserControl.SweetButton();
            this.btnExport = new Octopus.GUI.UserControl.SweetButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new Octopus.GUI.UserControl.SweetButton();
            this._labelTitle = new System.Windows.Forms.Label();
            this._tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlvAccounts)).BeginInit();
            this.groupBoxActions.SuspendLayout();
            this.panel1.SuspendLayout();
            this._tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tlvAccounts);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBoxActions);
            // 
            // tlvAccounts
            // 
            this.tlvAccounts.AllColumns.Add(this.olvColumnInternalAccountID);
            this.tlvAccounts.AllColumns.Add(this.olvColumnLabel);
            this.tlvAccounts.AllColumns.Add(this.olvColumnExportedBalance);
            this.tlvAccounts.CheckBoxes = false;
            this.tlvAccounts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnInternalAccountID,
            this.olvColumnLabel});
            resources.ApplyResources(this.tlvAccounts, "tlvAccounts");
            this.tlvAccounts.FullRowSelect = true;
            this.tlvAccounts.HideSelection = false;
            this.tlvAccounts.MultiSelect = false;
            this.tlvAccounts.Name = "tlvAccounts";
            this.tlvAccounts.OwnerDraw = true;
            this.tlvAccounts.ShowGroups = false;
            this.tlvAccounts.UseCompatibleStateImageBehavior = false;
            this.tlvAccounts.View = System.Windows.Forms.View.Details;
            this.tlvAccounts.VirtualMode = true;
            // 
            // olvColumnInternalAccountID
            // 
            this.olvColumnInternalAccountID.AspectName = "Number";
            this.olvColumnInternalAccountID.IsEditable = false;
            resources.ApplyResources(this.olvColumnInternalAccountID, "olvColumnInternalAccountID");
            // 
            // olvColumnLabel
            // 
            this.olvColumnLabel.AspectName = "Label";
            resources.ApplyResources(this.olvColumnLabel, "olvColumnLabel");
            // 
            // olvColumnExportedBalance
            // 
            this.olvColumnExportedBalance.AspectName = "Id";
            resources.ApplyResources(this.olvColumnExportedBalance, "olvColumnExportedBalance");
            this.olvColumnExportedBalance.IsVisible = false;
            // 
            // groupBoxActions
            // 
            this.groupBoxActions.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_gris;
            resources.ApplyResources(this.groupBoxActions, "groupBoxActions");
            this.groupBoxActions.Controls.Add(this.btnExportAccounts);
            this.groupBoxActions.Controls.Add(this.btnImportAccounts);
            this.groupBoxActions.Controls.Add(this.btnDeleteAccount);
            this.groupBoxActions.Controls.Add(this.btnEditAccount);
            this.groupBoxActions.Controls.Add(this.btnAddAccount);
            this.groupBoxActions.Controls.Add(this.btnExport);
            this.groupBoxActions.MinimumSize = new System.Drawing.Size(190, 0);
            this.groupBoxActions.Name = "groupBoxActions";
            this.groupBoxActions.TabStop = false;
            // 
            // btnExportAccounts
            // 
            resources.ApplyResources(this.btnExportAccounts, "btnExportAccounts");
            this.btnExportAccounts.BackColor = System.Drawing.Color.Gainsboro;
            this.btnExportAccounts.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.btnExportAccounts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnExportAccounts.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Export;
            this.btnExportAccounts.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_delete;
            this.btnExportAccounts.Menu = null;
            this.btnExportAccounts.Name = "btnExportAccounts";
            this.btnExportAccounts.UseVisualStyleBackColor = false;
            this.btnExportAccounts.Click += new System.EventHandler(this.BtnExportAccountsClick);
            // 
            // btnImportAccounts
            // 
            resources.ApplyResources(this.btnImportAccounts, "btnImportAccounts");
            this.btnImportAccounts.BackColor = System.Drawing.Color.Gainsboro;
            this.btnImportAccounts.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.btnImportAccounts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnImportAccounts.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Import;
            this.btnImportAccounts.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_delete;
            this.btnImportAccounts.Menu = null;
            this.btnImportAccounts.Name = "btnImportAccounts";
            this.btnImportAccounts.UseVisualStyleBackColor = false;
            this.btnImportAccounts.Click += new System.EventHandler(this.BtnLoadClick);
            // 
            // btnDeleteAccount
            // 
            resources.ApplyResources(this.btnDeleteAccount, "btnDeleteAccount");
            this.btnDeleteAccount.BackColor = System.Drawing.Color.Gainsboro;
            this.btnDeleteAccount.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.btnDeleteAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnDeleteAccount.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Delete;
            this.btnDeleteAccount.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_delete;
            this.btnDeleteAccount.Menu = null;
            this.btnDeleteAccount.Name = "btnDeleteAccount";
            this.btnDeleteAccount.UseVisualStyleBackColor = false;
            this.btnDeleteAccount.Click += new System.EventHandler(this.buttonDeleteRule_Click);
            // 
            // btnEditAccount
            // 
            resources.ApplyResources(this.btnEditAccount, "btnEditAccount");
            this.btnEditAccount.BackColor = System.Drawing.Color.Gainsboro;
            this.btnEditAccount.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.btnEditAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnEditAccount.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Edit;
            this.btnEditAccount.Image = global::Octopus.GUI.Properties.Resources.theme1_1_view;
            this.btnEditAccount.Menu = null;
            this.btnEditAccount.Name = "btnEditAccount";
            this.btnEditAccount.UseVisualStyleBackColor = false;
            this.btnEditAccount.Click += new System.EventHandler(this.buttonEditRule_Click);
            // 
            // btnAddAccount
            // 
            resources.ApplyResources(this.btnAddAccount, "btnAddAccount");
            this.btnAddAccount.BackColor = System.Drawing.Color.Gainsboro;
            this.btnAddAccount.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.btnAddAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnAddAccount.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.New;
            this.btnAddAccount.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_new;
            this.btnAddAccount.Menu = null;
            this.btnAddAccount.Name = "btnAddAccount";
            this.btnAddAccount.UseVisualStyleBackColor = false;
            this.btnAddAccount.Click += new System.EventHandler(this.buttonAddAccount_Click);
            // 
            // btnExport
            // 
            resources.ApplyResources(this.btnExport, "btnExport");
            this.btnExport.BackColor = System.Drawing.Color.Gainsboro;
            this.btnExport.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.btnExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnExport.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.None;
            this.btnExport.Image = global::Octopus.GUI.Properties.Resources.theme1_1_export;
            this.btnExport.Menu = null;
            this.btnExport.Name = "btnExport";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.butExport_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this._labelTitle);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.BackColor = System.Drawing.Color.Gainsboro;
            this.btnClose.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnClose.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Close;
            this.btnClose.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_close;
            this.btnClose.Menu = null;
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // _labelTitle
            // 
            this._labelTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this._labelTitle, "_labelTitle");
            this._labelTitle.ForeColor = System.Drawing.Color.White;
            this._labelTitle.Image = global::Octopus.GUI.Properties.Resources.theme1_1_pastille_contrat;
            this._labelTitle.Name = "_labelTitle";
            // 
            // _tableLayoutPanel1
            // 
            resources.ApplyResources(this._tableLayoutPanel1, "_tableLayoutPanel1");
            this._tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this._tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this._tableLayoutPanel1.Name = "_tableLayoutPanel1";
            // 
            // fileDialog
            // 
            this.fileDialog.DefaultExt = "*.CSV";
            // 
            // ChartOfAccountsForm
            // 
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._tableLayoutPanel1);
            this.Name = "ChartOfAccountsForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlvAccounts)).EndInit();
            this.groupBoxActions.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this._tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

        #endregion

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void butExport_Click(object sender, EventArgs e)
        {
            double exchangeRate;
            Currency pivotCurrency = ServicesProvider.GetInstance().GetCurrencyServices().GetPivot();
            exchangeRate = 1;
            Currency indexCurrency = pivotCurrency;
            if (_CurrencyId > 0)
            {
                indexCurrency = ServicesProvider.GetInstance().GetCurrencyServices().GetCurrency(_CurrencyId);
                if (!pivotCurrency.Equals(indexCurrency))
                    exchangeRate =
                        ServicesProvider.GetInstance().GetExchangeRateServices().GetMostRecentlyRate(TimeProvider.Today,indexCurrency);
            }
            if (exchangeRate == 0)
                MessageBox.Show(@"Please enter an exchange rate", @"Information", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            else
            {
                ReportService rs = ReportService.GetInstance();
                Report report = rs.GetReportByName("Accounting_Balances.zip");
                if (!report.IsLoaded) return;
                report.AddParam("PIVOT_CURRENCY", pivotCurrency.Code);
                report.AddParam("INDEX_CURRENCY", indexCurrency.Code);
                report.AddParam("currencyId", _CurrencyId);
                rs.LoadReport(report);
                ReportViewerForm frmViewer = new ReportViewerForm(report);
                frmViewer.Show();
            }
        }

        private void buttonAddAccount_Click(object sender, EventArgs e)
        {
            AddAccount();
        }

        private void buttonEditRule_Click(object sender, EventArgs e)
        {
            EditAccount();
        }

        private void buttonDeleteRule_Click(object sender, EventArgs e)
        {
            DeleteAccount();
        }

        private void ImportAccounts()
        {
            fileDialog.InitialDirectory = Application.CommonAppDataPath;
            fileDialog.Filter = @"CSV (*.csv)|*.csv|All files (*.*)|*.*";
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    bool deleteRelated = Confirm(MultiLanguageStrings.GetString(Ressource.ChartOfAccountsForm, "ToDeleteTheChartOfAccounts.Text"));

                    CsvImportExport importer = new CsvImportExport();
                    string filePath = fileDialog.FileName;
                    List<Account> accounts = new List<Account>();
                    ChartOfAccountsServices coaServices = ServicesProvider.GetInstance().GetChartOfAccountsServices();
                    if(deleteRelated)
                    {
                        IEnumerable<string> relatedDatas = coaServices.HasRelatedDatas();
                        if(relatedDatas != null && relatedDatas.Any())
                        {
                            string[] domainObjects =
                                relatedDatas
                                    .Select(
                                        r => GetString(string.Format("RelatedData.{0}", r))
                                    ).Distinct().ToArray();

                            string message =
                                string.Format(
                                    GetString("RelatedDataExists.Text"),
                                    string.Join(",", domainObjects));
                            if(!Confirm(message)) return;
                        }
                    }
                    importer.Import(filePath, items =>
                                                  {
                                                      Account account = new Account
                                                      {
                                                          Id = Convert.ToInt32(items[0]),
                                                          Number = items[1],
                                                          Label = items[2],
                                                          DebitPlus = Convert.ToBoolean(items[3]),
                                                          TypeCode = items[4],
                                                          AccountCategory = (OAccountCategories)(Convert.ToInt32(items[5])),
                                                          Type = Convert.ToBoolean(items[6]),
                                                          ParentAccountId = items[5].ToUpper() == "0" ? null : (int?)Convert.ToInt32(items[7]),
                                                          Left = Convert.ToInt32(items[8]),
                                                          Right = Convert.ToInt32(items[9])
                                                      };
                                                      accounts.Add(account);
                                                  });
                    
                    coaServices.InsertCoa(accounts.ToArray(), deleteRelated);
                    IntializeTreeViewChartOfAccounts();
                }
                catch (Exception ex)
                {
                    new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                }
            }
        }

        private void BtnLoadClick(object sender, EventArgs e)
        {
            ImportAccounts();
        }

        private void ExportAccounts()
        {
            DataSet dataSet = ServicesProvider.GetInstance().GetChartOfAccountsServices().GetAccountsDataset();
            ExportFile.SaveToFile(dataSet, string.Empty, @",");
        }

        private void BtnExportAccountsClick(object sender, EventArgs e)
        {
            ExportAccounts();
        }
    }
}