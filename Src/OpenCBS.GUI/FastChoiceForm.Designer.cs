

namespace OpenCBS.GUI
{
    partial class FastChoiceForm
    {
        private System.ComponentModel.IContainer components;

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


        #region Code généré par le Concepteur Windows Form
        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FastChoiceForm));
            this.generalInfoPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // generalInfoPanel
            // 
            resources.ApplyResources(this.generalInfoPanel, "generalInfoPanel");
            this.generalInfoPanel.Name = "generalInfoPanel";
            // 
            // FastChoiceForm
            // 
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.generalInfoPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FastChoiceForm";
            this.ShowInTaskbar = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel generalInfoPanel;



    }
}
