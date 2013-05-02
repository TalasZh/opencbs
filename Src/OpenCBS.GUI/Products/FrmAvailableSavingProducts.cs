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
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Services;
using System.Security.Permissions;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.GUI.Configuration;
using OpenCBS.Shared.Settings;
using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Products
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class FrmAvailableSavingProducts : SweetForm
    {
        //private ISavingProduct _product;
        private int _productId;

        public FrmAvailableSavingProducts()
        {
            InitializeComponent();
            //_product = new SavingsBookProduct();
            InitializePackages();
            webBrowserPackage.ObjectForScripting = this;
        }

        private int PackageFormId
        {
            set
            {
                _productId = value;
            }
            get
            {
                return _productId;
            }
        }

        private void InitializePackages()
        {
            string templatePath = UserSettings.GetTemplatePath;

            string text = string.Format(
                @"<html>
                        <head>
                        <link href='{0}\cover.css' type='text/css' rel='stylesheet'/>
                        <meta http-equiv='pragma' content='no-cache'/>
                        <title>Covers list</title>
                        </head>
                        <script type='text/javascript' src='{0}\cover.js'></script>
                        <body>", templatePath);

            List<ISavingProduct> packageList = ServicesProvider.GetInstance().GetSavingProductServices().
                FindAllSavingsProducts(checkBoxShowDeletedProduct.Checked, OClientTypes.All);
            packageList.Sort((p1, p2) => p1.Name.CompareTo(p2.Name));
            foreach (ISavingProduct p in packageList)
            {
                if (p is SavingsBookProduct)
                    text += _CreateHtmlForShowingPackage((SavingsBookProduct)p);
            }
            text += "</body></html>";

            StreamWriter writer = new StreamWriter(string.Format(@"{0}\packages_list.html", templatePath), false, Encoding.UTF8);
            writer.Write(text);
            writer.Close();

            webBrowserPackage.Url = new Uri(string.Format(@"{0}\packages_list.html", templatePath), UriKind.Absolute);
        }

        private void AddSavingBookProduct()
        {
            FrmAddSavingBookProduct addPackageForm = new FrmAddSavingBookProduct();
            if (addPackageForm.ShowDialog() == DialogResult.OK)
            {
                InitializePackages();
                ((LotrasmicMainWindowForm)MdiParent).SetInfoMessage(GetString("SaveSavingProduct.Text"));
            }
        }

       private void EditProduct()
        {
            if (PackageFormId != 0)
            {
                ISavingProduct product = ServicesProvider.GetInstance().GetSavingProductServices().FindSavingProductById(PackageFormId);

                if (product is SavingsBookProduct)
                    EditSavingBookProduct((SavingsBookProduct)product);
            }
            else
            {
                MessageBox.Show(MultiLanguageStrings.GetString(Ressource.PackagesForm, "messageSelection.Text"),
                                MultiLanguageStrings.GetString(Ressource.PackagesForm, "title.Text"), MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void EditSavingBookProduct(SavingsBookProduct pProduct)
        {
            FrmAddSavingBookProduct editProductForm = new FrmAddSavingBookProduct(pProduct);
            if (editProductForm.ShowDialog() == DialogResult.OK)
            {
                InitializePackages();
                ((LotrasmicMainWindowForm)MdiParent).SetInfoMessage(GetString("ModifiSavingProduct.Text"));
            }
        }

        private void buttonDeletePackage_Click(object sender, EventArgs e)
        {
            try
            {
                ServicesProvider.GetInstance().GetSavingProductServices().DeleteSavingProduct(PackageFormId);
                InitializePackages();
                //_product = null;
                buttonDeleteProduct.Enabled = false;
                buttonEditProduct.Enabled = false;
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private string _CreateHtmlForShowingPackage(SavingsBookProduct pSavingsProduct)
        {
            string img = "package.png";
            if (pSavingsProduct.Delete) img = "package_delete.png";

            string productInitialAmount = String.Format(GetString("Between.Text") + " {0} "+ GetString("And.Text") + " {1}", pSavingsProduct.InitialAmountMin.GetFormatedValue(pSavingsProduct.UseCents), pSavingsProduct.InitialAmountMax.GetFormatedValue(pSavingsProduct.UseCents));
            string productBalance = String.Format(GetString("Between.Text") + " {0} " + GetString("And.Text") + " {1}", pSavingsProduct.BalanceMin.GetFormatedValue(pSavingsProduct.UseCents), pSavingsProduct.BalanceMax.GetFormatedValue(pSavingsProduct.UseCents));
            string productWithdrawing = String.Format(GetString("Between.Text") + " {0} " + GetString("And.Text") + " {1}", pSavingsProduct.WithdrawingMin.GetFormatedValue(pSavingsProduct.UseCents), pSavingsProduct.WithdrawingMax.GetFormatedValue(pSavingsProduct.UseCents));
            string productInterestRate = pSavingsProduct.InterestRate.HasValue
                                             ? ServicesHelper.ConvertNullableDoubleToString(pSavingsProduct.InterestRate.Value, true) + "%"
                                             : string.Format(GetString("Between.Text") + " {0} "+ GetString("And.Text") + " {1}%",
                                                             ServicesHelper.ConvertNullableDoubleToString(pSavingsProduct.InterestRateMin.Value, true),
                                                             ServicesHelper.ConvertNullableDoubleToString(pSavingsProduct.InterestRateMax.Value, true));
            string productDeposit = String.Format(GetString("Between.Text") + " {0} " + GetString("And.Text") + " {1}", pSavingsProduct.DepositMin.GetFormatedValue(true), pSavingsProduct.DepositMax.GetFormatedValue(pSavingsProduct.UseCents));
            string productEntryFees = pSavingsProduct.EntryFees.HasValue
                                        ? pSavingsProduct.EntryFees.GetFormatedValue(pSavingsProduct.UseCents)
                                        : string.Format(GetString("Between.Text") + " {0} " + GetString("And.Text") + " {1}", pSavingsProduct.EntryFeesMin.GetFormatedValue(pSavingsProduct.UseCents), pSavingsProduct.EntryFeesMax.GetFormatedValue(pSavingsProduct.UseCents));
            
            string productInterestBase = GetString(pSavingsProduct.InterestBase.ToString() + ".Text");
            string productInterestFrequency = GetString(pSavingsProduct.InterestFrequency.ToString() + ".Text");
            string productInterestCalculAmountBase = pSavingsProduct.CalculAmountBase.HasValue ? GetString(pSavingsProduct.CalculAmountBase.ToString() + ".Text") : "-----";
            string productTransfer = String.Format(GetString("Between.Text") + " {0} " + GetString("And.Text") + " {1}", pSavingsProduct.TransferMin.GetFormatedValue(pSavingsProduct.UseCents), pSavingsProduct.TransferMax.GetFormatedValue(pSavingsProduct.UseCents));

            string text = string.Format(@"
                            <form id='{1}' name='package{1}'>
                            <table id={1} cellpadding='0' cellspacing='0' border='0' class='list_content' onclick='click_book(this,{1});' onmouseenter='mouse_enter_book(this);' onmouseleave='mouse_leave_book(this);'>
	                        <tr>
		                        <td>
                                    <table class='book_list' cellpadding='0' cellspacing='0' border='0'>
	                                <tr>
		                                <td>
			                                <table cellpadding='0' cellspacing='0' border='0'>
				                                <tr>
					                                <td><img id='{1}' src='{0}'/></td>
                                                </tr>                                                
			                                </table>
		                                </td>
	                                </tr>
                                    </table>
		                        </td>
		                        <td style='width:100%'>
		                            <span >
		                                <span class='title_popup'>" + GetString("SavingBookProduct.Text") + @" : {2} ({13})</span>
		                                <table cellpadding='0' cellspacing='0' border='0'>
		                                <tr>
		                                    <td>			
		                                        <span class='details'>" + GetString("InitialAmount.Text") + @"</span> 
		                                    </td>
		                                    <td>
		                                        <span class='description'>&nbsp;&nbsp;{3}</span>
		                                    </td>
                                            <td><span style='margin-left:15px;'></span></td>
		                                    <td>			
		                                        <span class='details'>" + GetString("Balance.Text") + @"</span> 
		                                    </td>
		                                    <td>
		                                        <span class='description'>&nbsp;&nbsp;{4}</span>
		                                    </td>
                                            <td><span style='margin-left:15px;'></span></td>
		                                    <td>			
		                                        <span class='details'></span> 
		                                    </td>
		                                    <td>
		                                        <span class='description'>&nbsp;</span>
		                                    </td>
		                                </tr>
		                                <tr>
		                                    <td>			
		                                        <span class='details'>" + GetString("Withdrawing.Text") + @"</span> 
		                                    </td>
		                                    <td>
		                                        <span class='description'>&nbsp;&nbsp;{5}</span>
		                                    </td>
                                            <td><span style='margin-left:15px;'></span></td>
		                                    <td>			
		                                        <span class='details'>" + GetString("InterestRate.Text") + @"</span> 
		                                    </td>
		                                    <td>
		                                        <span class='description'>&nbsp;&nbsp;{6}</span>
		                                    </td>
                                            <td><span style='margin-left:15px;'></span></td>
		                                    <td>			
		                                        <span class='details'></span> 
		                                    </td>
		                                    <td>
		                                        <span class='description'>&nbsp;</span>
		                                    </td>
		                                </tr>
		                                <tr>
		                                    <td>			
		                                        <span class='details'>" + GetString("Deposit.Text") + @"</span> 
		                                    </td>
		                                    <td>
		                                        <span class='description'>&nbsp;&nbsp;{7}</span>
		                                    </td>
                                            <td><span style='margin-left:15px;'></span></td>
	                                        <td>			
		                                        <span class='details'>" + GetString("InterestFrequency.Text") + @"</span> 
		                                    </td>
		                                    <td>
		                                        <span class='description'>&nbsp;&nbsp;{8}</span>
		                                    </td>
                                            <td><span style='margin-left:15px;'></span></td>
		                                    <td colspan='5'></td>
		                                </tr>
                                        <tr>
		                                    <td>			
		                                        <span class='details'>" + GetString("InterestBase.Text") + @"</span> 
		                                    </td>
		                                    <td>
		                                        <span class='description'>&nbsp;&nbsp;{9}</span>
		                                    </td>
                                            <td><span style='margin-left:15px;'></span></td>
                                            <td>			
		                                        <span class='details'>"+GetString("BasedOn.Text")+@"</span> 
		                                    </td>
		                                    <td>
		                                        <span class='description'>&nbsp;&nbsp;{10}</span>
		                                    </td>
                                            <td><span style='margin-left:15px;'></span></td>
		                                    <td colspan='5'></td>
		                                </tr>
                                        <tr>
                                            <td>			
		                                        <span class='details'>" + GetString("Transfer.Text") + @"</span> 
		                                    </td>
		                                    <td>
		                                        <span class='description'>&nbsp;&nbsp;{11}</span>
		                                    </td>
                                            <td><span style='margin-left:15px;'></span></td>
		                                    <td>			
		                                        <span class='details'>" + GetString("EntryFees.Text") + @"</span> 
		                                    </td>
		                                    <td>
		                                        <span class='description'>&nbsp;&nbsp;{12}</span>
		                                    </td>
                                            <td><span style='margin-left:15px;'></span></td>
                                            <td colspan='8'>			
		                                    </td>
		                                </tr>
		                                </table>
                                    </span>
                                </td>
                            </tr>
                            </table></form>", Path.Combine(UserSettings.GetTemplatePath, img),
                                        pSavingsProduct.Id, pSavingsProduct.Name, productInitialAmount, productBalance,
                                        productWithdrawing, productInterestRate, productDeposit, productInterestFrequency,
                                        productInterestBase, productInterestCalculAmountBase, productTransfer, productEntryFees, pSavingsProduct.Code);
            return text;
        }

       private void checkBoxShowDeletedProduct_CheckedChanged(object sender, EventArgs e)
        {
            InitializePackages();
        }

        public void Package_DoubleClick(object sender, HtmlElementEventArgs e)
        {
            PackageFormId = Convert.ToInt32(((HtmlElement)sender).Id);
            EditProduct();
        }

        private void webBrowserPackage_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            foreach (HtmlElement image in webBrowserPackage.Document.Images)
                image.DoubleClick += Package_DoubleClick;

            foreach (HtmlElement form in webBrowserPackage.Document.Forms)
                form.Click += Package_Click;
        }

        public void Package_Click(object sender, HtmlElementEventArgs e)
        {
            HtmlElement tag = (HtmlElement)(sender);
            PackageFormId = Convert.ToInt32(tag.Id);
            //_product = ServicesProvider.GetInstance().GetSavingProductServices().FindSavingBookProductById(PackageFormId);
            buttonDeleteProduct.Enabled = true;
            buttonEditProduct.Enabled = true;
        }

        private void buttonEditProduct_Click(object sender, EventArgs e)
        {
            EditProduct();   
        }

        private void PackagesForm_Load(object sender, EventArgs e)
        {
            buttonDeleteProduct.Enabled = true;
            buttonEditProduct.Enabled = true;
        }

        private void buttonAddPackage_Click(object sender, EventArgs e)
        {
            menuBtnAddProduct.Show((Button)sender, 0 - menuBtnAddProduct.Size.Width, 0);
        }

        private void savingBookProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSavingBookProduct();
        }
    }
}