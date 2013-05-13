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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Export.Files;
using OpenCBS.Services;
using OpenCBS.Enums;

namespace OpenCBS.GUI.Export
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
