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
using System.Security.Permissions;
using OpenCBS.CoreDomain.Products.Collaterals;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Services;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Shared.Settings;
using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Products
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class FrmAvalaibleCollateralProducts : SweetForm
    {
        private int idPackage;
        private CollateralProduct product;

        public FrmAvalaibleCollateralProducts()
        {
            InitializeComponent();
            product = new CollateralProduct();
            InitializePackages();
            webBrowserPackage.ObjectForScripting = this;
        }

        private int PackageFormId
        {
            set { idPackage = value; }
            get { return idPackage; }
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

            List<CollateralProduct> productList = ServicesProvider.GetInstance().GetCollateralProductServices().SelectAllCollateralProducts(_showDeletedPackage);
            productList.Sort(new CollateralProductComparer<CollateralProduct>());
            foreach (CollateralProduct collateralProduct in productList)
            {
                text += _CreateHtmlForShowingPackage(collateralProduct);
            }
            text += "</body></html>";

            StreamWriter writer = new StreamWriter(string.Format(@"{0}\packages_list.html", templatePath), false, Encoding.UTF8);
            writer.Write(text);
            writer.Close();

            webBrowserPackage.Url = new Uri(string.Format(@"{0}\packages_list.html", templatePath), UriKind.Absolute);
        }

        private void DeletePackage()
        {
            try
            {
                ServicesProvider.GetInstance().GetCollateralProductServices().DeleteCollateralProduct(product.Id);
                InitializePackages();
                product = null;
                buttonDeletePackage.Enabled = false;
                buttonEditProduct.Enabled = false;
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void buttonDeletePackage_Click(object sender, EventArgs e)
        {
            DeletePackage();
        }

        private static string _CreateHtmlForShowingPackage(CollateralProduct collateralProduct)
        {
            string img = "package.png";
            if (collateralProduct.Delete) img = "package_delete.png";

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
		                                <span class='title_popup'>{2} ({3})</span>
                                    </span>
                                </td>
                            </tr>
                            </table></form>", Path.Combine(UserSettings.GetTemplatePath, img), collateralProduct.Id, collateralProduct.Name, collateralProduct.Description);

            return text;
        }

        private bool _showDeletedPackage = false;
        private void checkBoxShowDeletedProduct_CheckedChanged(object sender, EventArgs e)
        {
            _showDeletedPackage = checkBoxShowDeletedProduct.Checked;
            InitializePackages();
        }

        public void Package_DoubleClick(object sender, HtmlElementEventArgs e)
        {
            string sID = webBrowserPackage.Document.GetElementFromPoint(e.MousePosition).Id;
            CollateralProduct selectedProduct = ServicesProvider.GetInstance().GetCollateralProductServices().SelectCollateralProduct(Convert.ToInt32(sID));
            FrmAddCollateralProduct addCollateralProduct = new FrmAddCollateralProduct(selectedProduct);
            addCollateralProduct.ShowDialog();
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
            var tag = (HtmlElement)(sender);
            PackageFormId = Convert.ToInt32(tag.Id);
            product = ServicesProvider.GetInstance().GetCollateralProductServices().SelectCollateralProduct(PackageFormId);

            buttonDeletePackage.Enabled = true;
            buttonEditProduct.Enabled = true;
        }

        private void buttonEditProduct_Click(object sender, EventArgs e)
        {
            if (PackageFormId != 0)
            {
                CollateralProduct selectedProduct = ServicesProvider.GetInstance().GetCollateralProductServices().SelectCollateralProduct(PackageFormId);
                FrmAddCollateralProduct addCollateralProduct = new FrmAddCollateralProduct(selectedProduct);
                addCollateralProduct.ShowDialog();
            }
            else
            {
                MessageBox.Show(MultiLanguageStrings.GetString(Ressource.PackagesForm, "messageSelection.Text"),
                    MultiLanguageStrings.GetString(Ressource.PackagesForm, "title.Text"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void PackagesForm_Load(object sender, EventArgs e)
        {
            buttonDeletePackage.Enabled = true;
            buttonEditProduct.Enabled = true;
        }

        private void buttonAddProduct_Click(object sender, EventArgs e)
        {
            FrmAddCollateralProduct addCollateralProductForm = new FrmAddCollateralProduct();
            addCollateralProductForm.ShowDialog();
            InitializePackages();
            ((LotrasmicMainWindowForm)MdiParent).SetInfoMessage("Collateral product saved");
        }
    }
}