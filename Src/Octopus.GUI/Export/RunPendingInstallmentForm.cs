using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Octopus.CoreDomain.Accounting;
using Octopus.CoreDomain.Export.Files;
using Octopus.Services;
using Octopus.Enums;

namespace Octopus.GUI.Export
{
    public partial class RunPendingInstallmentForm : Form
    {
        private List<Installment> _installments;
        private List<Installment> _failedInstallments;
        private PaymentMethod _paymentMethod;

        public RunPendingInstallmentForm(List<Installment> pInstallments, PaymentMethod pPaymentMethod)
        {
            _installments = pInstallments;
            _paymentMethod = pPaymentMethod;
            InitializeComponent();
        }

        public void Run()
        {
            buttonRun.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
        }

        public List<Installment> FailedInstallments
        {
            get { return _failedInstallments; }
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            Run();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            _failedInstallments = new List<Installment>();
            var exportServices = ServicesProvider.GetInstance().GetExportServices();
            for (int i = 0; i < _installments.Count; i++)
            {
                progressBar.Invoke(new MethodInvoker(delegate
                    {
                        progressBar.Text = string.Format("{0} / {1}", i + 1, _installments.Count);
                        progressBar.Value = i * 100 / _installments.Count;
                    }));

                labelDetails.Invoke(new MethodInvoker(delegate
                    {
                        labelDetails.Text = string.Format("{0}", _installments[i].ContractCode);
                    }));

                try
                {
                    exportServices.SetInstallmentAsPending(_installments[i], _paymentMethod);
                }
                catch
                {
                    _failedInstallments.Add(_installments[i]);
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
