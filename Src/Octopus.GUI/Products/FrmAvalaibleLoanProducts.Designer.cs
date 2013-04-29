using System.ComponentModel;
using System.Windows.Forms;
using Octopus.GUI.UserControl;

namespace Octopus.GUI.Products
{
    public partial class FrmAvalaibleLoanProducts
    {
        private SweetButton buttonDeletePackage;
        private SweetButton buttonAddPackage;
        private SweetButton buttonEditProduct;

        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private Container components = null;
        private Panel pnlLoanProducts;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAvalaibleLoanProducts));
            this.webBrowserPackage = new System.Windows.Forms.WebBrowser();
            this.buttonEditProduct = new Octopus.GUI.UserControl.SweetButton();
            this.checkBoxShowDeletedProduct = new System.Windows.Forms.CheckBox();
            this.buttonAddPackage = new Octopus.GUI.UserControl.SweetButton();
            this.buttonDeletePackage = new Octopus.GUI.UserControl.SweetButton();
            this.pnlLoanProducts = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlLoanProducts.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowserPackage
            // 
            resources.ApplyResources(this.webBrowserPackage, "webBrowserPackage");
            this.webBrowserPackage.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserPackage.Name = "webBrowserPackage";
            this.webBrowserPackage.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowserPackage_DocumentCompleted);
            // 
            // buttonEditProduct
            // 
            this.buttonEditProduct.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonEditProduct, "buttonEditProduct");
            this.buttonEditProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonEditProduct.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.None;
            this.buttonEditProduct.Menu = null;
            this.buttonEditProduct.Name = "buttonEditProduct";
            this.buttonEditProduct.UseVisualStyleBackColor = false;
            this.buttonEditProduct.Click += new System.EventHandler(this.buttonEditProduct_Click);
            // 
            // checkBoxShowDeletedProduct
            // 
            resources.ApplyResources(this.checkBoxShowDeletedProduct, "checkBoxShowDeletedProduct");
            this.checkBoxShowDeletedProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.checkBoxShowDeletedProduct.Name = "checkBoxShowDeletedProduct";
            this.checkBoxShowDeletedProduct.UseVisualStyleBackColor = true;
            this.checkBoxShowDeletedProduct.CheckedChanged += new System.EventHandler(this.checkBoxShowDeletedProduct_CheckedChanged);
            // 
            // buttonAddPackage
            // 
            this.buttonAddPackage.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonAddPackage, "buttonAddPackage");
            this.buttonAddPackage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonAddPackage.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.New;
            this.buttonAddPackage.Menu = null;
            this.buttonAddPackage.Name = "buttonAddPackage";
            this.buttonAddPackage.UseVisualStyleBackColor = false;
            this.buttonAddPackage.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonDeletePackage
            // 
            this.buttonDeletePackage.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonDeletePackage, "buttonDeletePackage");
            this.buttonDeletePackage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonDeletePackage.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Delete;
            this.buttonDeletePackage.Menu = null;
            this.buttonDeletePackage.Name = "buttonDeletePackage";
            this.buttonDeletePackage.UseVisualStyleBackColor = false;
            this.buttonDeletePackage.Click += new System.EventHandler(this.buttonDeletePackage_Click);
            // 
            // pnlLoanProducts
            // 
            this.pnlLoanProducts.Controls.Add(this.webBrowserPackage);
            this.pnlLoanProducts.Controls.Add(this.flowLayoutPanel1);
            resources.ApplyResources(this.pnlLoanProducts, "pnlLoanProducts");
            this.pnlLoanProducts.Name = "pnlLoanProducts";
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.checkBoxShowDeletedProduct);
            this.flowLayoutPanel1.Controls.Add(this.buttonAddPackage);
            this.flowLayoutPanel1.Controls.Add(this.buttonEditProduct);
            this.flowLayoutPanel1.Controls.Add(this.buttonDeletePackage);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // FrmAvalaibleLoanProducts
            // 
            this.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.pnlLoanProducts);
            this.Name = "FrmAvalaibleLoanProducts";
            this.Load += new System.EventHandler(this.PackagesForm_Load);
            this.Controls.SetChildIndex(this.pnlLoanProducts, 0);
            this.pnlLoanProducts.ResumeLayout(false);
            this.pnlLoanProducts.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private FlowLayoutPanel flowLayoutPanel1;
    }
}
