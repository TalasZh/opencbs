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
using OpenCBS.Shared.Settings;
using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Products
{
    /// <summary>
    /// Description r�sum�e de PackagesForm.
    /// </summary>
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class FrmAvalaibleLoanProducts : SweetForm
    {
        private int _idPackage;
        private LoanProduct _package;

        public FrmAvalaibleLoanProducts()
        {
            InitializeComponent();
            _package = new LoanProduct();
            InitializePackages();
            webBrowserPackage.ObjectForScripting = this;
        }

        private int PackageFormId
        {
            set { _idPackage = value; }
            get { return _idPackage; }
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

            List<LoanProduct> packageList = ServicesProvider.GetInstance().GetProductServices().FindAllPackages(_showDeletedPackage, OClientTypes.All);
            packageList.Sort(new ProductComparer<LoanProduct>());
            foreach (LoanProduct p in packageList)
            {
                text += _CreateHtmlForShowingPackage(p);
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
                ServicesProvider.GetInstance().GetProductServices().DeletePackage(_package);
                InitializePackages();
                _package = null;
                buttonDeletePackage.Enabled = false;
                buttonEditProduct.Enabled = false;
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void SavePackage()
        {
            var addPackageForm = new FrmAddLoanProduct();
            addPackageForm.ShowDialog();
            InitializePackages();
            ((LotrasmicMainWindowForm) MdiParent).SetInfoMessage("Product saved");
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SavePackage();
        }

        private void buttonDeletePackage_Click(object sender, EventArgs e)
        {
            string msg = GetString("DeleteConfirmation.Text");
            if (DialogResult.Yes == MessageBox.Show(msg,"", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                DeletePackage();
        }

        private string _CreateHtmlForShowingPackage(LoanProduct pPackage)
        {
            string img = "package.png";
            if (pPackage.Delete) img = "package_delete.png";

            string packageAnticipatedTotalRepaymentPenaltiesBase = pPackage.AnticipatedTotalRepaymentPenaltiesBase ==
                                                              OAnticipatedRepaymentPenaltiesBases.RemainingOLB
                                                                  ? GetString("RemainingOLB.Text")
                                                                  : GetString("RemainingInterest.Text");

            string packageLateFeesInitialAmount = pPackage.NonRepaymentPenalties.InitialAmount.HasValue
                                                      ? ServicesHelper.ConvertNullableDoubleToString(pPackage.NonRepaymentPenalties.InitialAmount.Value, true) + "%"
                                                      : string.Format(GetString("Between.Text"),
                                                                      ServicesHelper.ConvertNullableDoubleToString(pPackage.NonRepaymentPenaltiesMin.InitialAmount.Value,true),
                                                                      ServicesHelper.ConvertNullableDoubleToString(pPackage.NonRepaymentPenaltiesMax.InitialAmount.Value,true),
                                                                      "%");

            string packageLateFeesOLB = pPackage.NonRepaymentPenalties.OLB.HasValue
                                            ? ServicesHelper.ConvertNullableDoubleToString(pPackage.NonRepaymentPenalties.OLB.Value, true) + "%"
                                            : string.Format(GetString("Between.Text"),
                                                            ServicesHelper.ConvertNullableDoubleToString(pPackage.NonRepaymentPenaltiesMin.OLB.Value, true),
                                                            ServicesHelper.ConvertNullableDoubleToString(pPackage.NonRepaymentPenaltiesMax.OLB.Value, true),
                                                            "%");

            string packageLateFeesOverduePrincipal = pPackage.NonRepaymentPenalties.OverDuePrincipal.HasValue
                                                         ? ServicesHelper.ConvertNullableDoubleToString(pPackage.NonRepaymentPenalties.OverDuePrincipal.Value, true) +"%"
                                                         : string.Format(GetString("Between.Text"),
                                                                         ServicesHelper.ConvertNullableDoubleToString(pPackage.NonRepaymentPenaltiesMin.OverDuePrincipal.Value, true),
                                                                         ServicesHelper.ConvertNullableDoubleToString(pPackage.NonRepaymentPenaltiesMax.OverDuePrincipal.Value, true),
                                                                         "%");

            string packageLateFeesOverdueInterest = pPackage.NonRepaymentPenalties.OverDueInterest.HasValue
                                                        ? ServicesHelper.ConvertNullableDoubleToString(pPackage.NonRepaymentPenalties.OverDueInterest.Value, true) + "%"
                                                        : string.Format(GetString("Between.Text"),
                                                                        ServicesHelper.ConvertNullableDoubleToString(pPackage.NonRepaymentPenaltiesMin.OverDueInterest.Value, true),
                                                                        ServicesHelper.ConvertNullableDoubleToString(pPackage.NonRepaymentPenaltiesMax.OverDueInterest.Value, true),
                                                                        "%");

            string packageInterestRate = pPackage.InterestRate.HasValue
                                             ? ServicesHelper.ConvertNullableDecimalToString(pPackage.InterestRate, true) + "%"
                                             : string.Format(GetString("Between.Text"),
                                                             ServicesHelper.ConvertNullableDecimalToString(pPackage.InterestRateMin, true),
                                                             ServicesHelper.ConvertNullableDecimalToString(pPackage.InterestRateMax, true),
                                                             "%");
            
//            string packageEntryFees = pPackage.EntryFees.HasValue
//                                          ? ServicesHelper.ConvertNullableDoubleToString(pPackage.EntryFees, pPackage.EntryFeesPercentage) + entryFeesUnits
//                                          : string.Format(MultiLanguageStrings.GetString(Ressource.PackagesForm, "Between.Text"),
//                                                          ServicesHelper.ConvertNullableDoubleToString(pPackage.EntryFeesMin, pPackage.EntryFeesPercentage),
//                                                          ServicesHelper.ConvertNullableDoubleToString(pPackage.EntryFeesMax, pPackage.EntryFeesPercentage),
//                                                          entryFeesUnits);

            string packageAnticipatedRepaymentPenalties = pPackage.AnticipatedTotalRepaymentPenalties.HasValue
                                                              ? ServicesHelper.ConvertNullableDoubleToString(pPackage.AnticipatedTotalRepaymentPenalties, true) + "%"
                                                              : string.Format(GetString("Between.Text"),
                                                                              ServicesHelper.ConvertNullableDoubleToString(pPackage.AnticipatedTotalRepaymentPenaltiesMin,true),
                                                                              ServicesHelper.ConvertNullableDoubleToString(pPackage.AnticipatedTotalRepaymentPenaltiesMax,true),
                                                                              "%");

            string packageNbOfInstallment = pPackage.NbOfInstallments.HasValue
                                                ? pPackage.NbOfInstallments.Value + " " + GetString("Periods.Text")
                                                : string.Format(GetString("Between.Text"),
                                                                pPackage.NbOfInstallmentsMin.Value,
                                                                pPackage.NbOfInstallmentsMax.Value,
                                                                GetString("Periods.Text"));

            string packageGracePeriod = pPackage.GracePeriod.HasValue
                                            ? pPackage.GracePeriod.Value + " " + GetString("Periods.Text")
                                            : string.Format(GetString("Between.Text"),
                                                            pPackage.GracePeriodMin.Value,
                                                        pPackage.GracePeriodMax.Value,
                                                            GetString("Periods.Text"));
            string packageCurrency = pPackage.Currency.Name; 

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
		                                <span class='title_popup'>{2}</span>
		                                <table cellpadding='0' cellspacing='0' border='0'>

                                        <tr>
		                                    <td>			
		                                        <span class='details'>" + GetString("Currency.Text") + @"</span> 
		                                    </td>
		                                    <td>
		                                        <span class='description'>&nbsp;&nbsp;{14}</span>
		                                    </td>
                                            <td><span style='margin-left:15px;'></span></td>
                                            <td>			
		                                        <span class='details'>" + GetString("Anticipatedrepaymentpenalties.Text") + @"</span> 
		                                    </td>
		                                    <td>
		                                        <span class='description'>&nbsp;&nbsp;{8}</span>
		                                    </td>
                                            <td><span style='margin-left:15px;'></span></td>
		                                    <td>			
		                                        <span class='details'>" + GetString("Basedon.Text") + @"</span> 
		                                    </td>
		                                    <td>
		                                        <span class='description'>&nbsp;&nbsp;{13}</span>
		                                    </td>
                                        </tr>
		                                <tr style='vertical-align:top'>
		                                    <td>
		                                        <table cellpadding='0' cellspacing='0' border = '0'>
                                                    <tr><td><span class='details'>" + GetString("Interestrate.Text") + @"</span></td></tr>
                                                    <tr><td><span class='details'>" + GetString("Periodicity.Text") + @"</span></td></tr>    
		                                            <tr><td><span class='details'>" + GetString("Maturity.Text") + @"</span></td></tr>                                                    
		                                            <tr><td><span class='details'>" + GetString("Graceperiod.Text") + @"</span></td></tr>
		                                        </table>
		                                    </td>
		                                    <td>
		                                        <table cellpadding='0' cellspacing='0' border = '0'>
                                                    <tr><td><span class='description'>&nbsp;&nbsp;{3}</span></td></tr>
                                                    <tr><td><span class='description'>&nbsp;&nbsp;{4}</span></td></tr>
		                                            <tr><td><span class='description'>&nbsp;&nbsp;{5}</span></td></tr>
		                                            <tr><td><span class='description'>&nbsp;&nbsp;{6}</span></td></tr>
		                                        </table>
		                                    </td>
                                            <td><span style='margin-left:15px;'></span></td>
		                                    <td>			
		                                        <span class='details'>" + GetString("Laterepaymentspenalties.Text") + @"</span> 
		                                    </td>
		                                    <td>
		                                        <table cellpadding='0' cellspacing='0' border = '0'>
		                                            <tr><td><span class='description'>&nbsp;&nbsp;{9}</span></td></tr>
		                                            <tr><td><span class='description'>&nbsp;&nbsp;{10}</span></td></tr>
		                                            <tr><td><span class='description'>&nbsp;&nbsp;{11}</span></td></tr>
		                                            <tr><td><span class='description'>&nbsp;&nbsp;{12}</span></td></tr>
		                                        </table>
		                                    </td>
                                            <td><span style='margin-left:15px;'></span></td>
		                                    <td>
		                                        <table cellpadding='0' cellspacing='0' border = '0'>
		                                            <tr><td><span class='details'>" + GetString("Basedon.Text") + @"</span></td></tr>
		                                            <tr><td><span class='details'>" + GetString("Basedon.Text") + @"</span></td></tr>
		                                            <tr><td><span class='details'>" + GetString("Basedon.Text") + @"</span></td></tr>
		                                            <tr><td><span class='details'>" + GetString("Basedon.Text") + @"</span></td></tr>
		                                        </table>
		                                    </td>
		                                    <td>
		                                        <table cellpadding='0' cellspacing='0' border = '0'>
		                                            <tr><td><span class='description'>&nbsp;&nbsp;" + GetString("Totalloanamount.Text") + @"</span></td></tr>
		                                            <tr><td><span class='description'>&nbsp;&nbsp;" + GetString("OLB.Text") + @"</span></td></tr>
		                                            <tr><td><span class='description'>&nbsp;&nbsp;" + GetString("Overdueprincipal.Text") + @"</span></td></tr>
		                                            <tr><td><span class='description'>&nbsp;&nbsp;" + GetString("Overdueinterest.Text") + @"</span></td></tr>
		                                        </table>
		                                    </td>
		                                </tr>
		                                </table>
                                    </span>
                                </td>
                            </tr>
                            </table></form>", Path.Combine(UserSettings.GetTemplatePath, img), pPackage.Id, pPackage.Name, packageInterestRate,
                                        pPackage.InstallmentType.Name, packageNbOfInstallment, packageGracePeriod, 0/*packageEntryFees*/,
                                        packageAnticipatedRepaymentPenalties, packageLateFeesInitialAmount, packageLateFeesOLB,
                                        packageLateFeesOverduePrincipal, packageLateFeesOverdueInterest, packageAnticipatedTotalRepaymentPenaltiesBase, packageCurrency);

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
            
            LoanProduct selectedPackage = ServicesProvider.GetInstance().GetProductServices().FindPackage(System.Convert.ToInt32(sID));

            var addPackageForm = new FrmAddLoanProduct(selectedPackage);
            addPackageForm.ShowDialog();
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
            this.PackageFormId = Convert.ToInt32(tag.Id);
            _package = ServicesProvider.GetInstance().GetProductServices().FindPackage(PackageFormId);
            buttonDeletePackage.Enabled = true;
            buttonEditProduct.Enabled = true;
        }

        private void buttonEditProduct_Click(object sender, EventArgs e)
        {
            if (PackageFormId != 0)
            {
                LoanProduct selectedPackage = ServicesProvider.GetInstance().GetProductServices().FindPackage(PackageFormId);

                var addPackageForm = new FrmAddLoanProduct(selectedPackage);
                addPackageForm.ShowDialog();
            }
            else
            {
                MessageBox.Show(GetString("messageSelection.Text"),
                                GetString("title.Text"), 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void PackagesForm_Load(object sender, EventArgs e)
        {
            buttonDeletePackage.Enabled = true;
            buttonEditProduct.Enabled = true;
        }
    }
}