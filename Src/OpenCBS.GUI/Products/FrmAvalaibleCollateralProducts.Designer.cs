using System.ComponentModel;
using System.Windows.Forms;
using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Products
{
    public partial class FrmAvalaibleCollateralProducts
    {
        private System.Windows.Forms.Button buttonDeletePackage;
        private System.Windows.Forms.Button buttonAddProduct;
        private System.Windows.Forms.Button buttonEditProduct;

        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private Container components = null;

        private GroupBox groupBox;
        private Panel pnlCollateralProducts;
        private CheckBox checkBoxShowDeletedProduct;
        private WebBrowser webBrowserPackage;

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

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAvalaibleCollateralProducts));
            this.webBrowserPackage = new System.Windows.Forms.WebBrowser();
            this.pnlCollateralProducts = new System.Windows.Forms.Panel();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.buttonEditProduct = new System.Windows.Forms.Button();
            this.checkBoxShowDeletedProduct = new System.Windows.Forms.CheckBox();
            this.buttonAddProduct = new System.Windows.Forms.Button();
            this.buttonDeletePackage = new System.Windows.Forms.Button();
            this.pnlCollateralProducts.SuspendLayout();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowserPackage
            // 
            resources.ApplyResources(this.webBrowserPackage, "webBrowserPackage");
            this.webBrowserPackage.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserPackage.Name = "webBrowserPackage";
            this.webBrowserPackage.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowserPackage_DocumentCompleted);
            // 
            // pnlCollateralProducts
            // 
            this.pnlCollateralProducts.Controls.Add(this.webBrowserPackage);
            this.pnlCollateralProducts.Controls.Add(this.groupBox);
            resources.ApplyResources(this.pnlCollateralProducts, "pnlCollateralProducts");
            this.pnlCollateralProducts.Name = "pnlCollateralProducts";
            // 
            // groupBox
            //
            resources.ApplyResources(this.groupBox, "groupBox");
            this.groupBox.Controls.Add(this.buttonEditProduct);
            this.groupBox.Controls.Add(this.checkBoxShowDeletedProduct);
            this.groupBox.Controls.Add(this.buttonAddProduct);
            this.groupBox.Controls.Add(this.buttonDeletePackage);
            this.groupBox.Name = "groupBox";
            this.groupBox.TabStop = false;
            // 
            // buttonEditProduct
            // 
            resources.ApplyResources(this.buttonEditProduct, "buttonEditProduct");
            this.buttonEditProduct.Name = "buttonEditProduct";
            this.buttonEditProduct.Click += new System.EventHandler(this.buttonEditProduct_Click);
            // 
            // checkBoxShowDeletedProduct
            // 
            resources.ApplyResources(this.checkBoxShowDeletedProduct, "checkBoxShowDeletedProduct");
            this.checkBoxShowDeletedProduct.Name = "checkBoxShowDeletedProduct";
            this.checkBoxShowDeletedProduct.CheckedChanged += new System.EventHandler(this.checkBoxShowDeletedProduct_CheckedChanged);
            // 
            // buttonAddProduct
            // 
            resources.ApplyResources(this.buttonAddProduct, "buttonAddProduct");
            this.buttonAddProduct.Name = "buttonAddProduct";
            this.buttonAddProduct.Click += new System.EventHandler(this.buttonAddProduct_Click);
            // 
            // buttonDeletePackage
            // 
            resources.ApplyResources(this.buttonDeletePackage, "buttonDeletePackage");
            this.buttonDeletePackage.Name = "buttonDeletePackage";
            this.buttonDeletePackage.Click += new System.EventHandler(this.buttonDeletePackage_Click);
            // 
            // FrmAvalaibleCollateralProducts
            //
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.pnlCollateralProducts);
            this.Name = "FrmAvalaibleCollateralProducts";
            this.Load += new System.EventHandler(this.PackagesForm_Load);
            this.Controls.SetChildIndex(this.pnlCollateralProducts, 0);
            this.pnlCollateralProducts.ResumeLayout(false);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
