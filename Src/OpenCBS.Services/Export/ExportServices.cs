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
using System.Linq;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Manager.Export;
using OpenCBS.CoreDomain;
using OpenCBS.ExceptionsHandler.Exceptions.ExportExceptions;
using OpenCBS.CoreDomain.Export.Files;
using OpenCBS.CoreDomain.Export.Fields;

namespace OpenCBS.Services.Export
{
    public class ExportServices : MarshalByRefObject
    {
        private ExportManager _exportManager;
        private User _user;

        public ExportServices(User pUser)
        {
            _user = pUser;
            _exportManager = new ExportManager(_user);
        }

        public ExportServices(string pTestDB)
        {
            _exportManager = new ExportManager(pTestDB);
        }

        public ExportServices(ExportManager pExportManager)
        {
            _exportManager = pExportManager;
        }

        public bool ValidateFile(IFile pFile)
        {
            if (string.IsNullOrEmpty(pFile.Name))
                throw new OpenCbsCustomExportException(OpenCbsCustomExportExceptionEnum.FileNameIsEmpty);
            if (string.IsNullOrEmpty(pFile.Extension))
                throw new OpenCbsCustomExportException(OpenCbsCustomExportExceptionEnum.FileExtensionIsIncorrect);
            if (!System.Text.RegularExpressions.Regex.IsMatch(pFile.Extension, @"^\.[a-zA-Z0-9]+$"))
                throw new OpenCbsCustomExportException(OpenCbsCustomExportExceptionEnum.FileExtensionIsIncorrect);
            if (!pFile.IsExportFile && !_verifyRequiredFields(pFile))
                throw new OpenCbsCustomExportException(OpenCbsCustomExportExceptionEnum.SomeRequiredFieldsAreMissing);

            return true;
        }

        private bool _verifyRequiredFields(IFile pFile)
        {
            foreach (var field in pFile.DefaultList.OfType<Field>().Where(item => item.IsRequired))
            {
                if (pFile.SelectedFields.FirstOrDefault(item => item.Name == field.Name) == null)
                    return false;
            }
            
            return true;
        }

        public List<Installment> GetInstallmentData(DateTime pStartDate, DateTime pEndDate)
        {
            return _exportManager.SelectInstallment(pStartDate, pEndDate);
        }

        public bool SetInstallmentAsPending(Installment installment, PaymentMethod pPaymentMethod)
        {
            ClientServices clientServices = new ClientServices(_user);
            LoanServices loanServices = new LoanServices(_user);

            var client = clientServices.FindTiers(installment.ClientId, OpenCBS.Enums.OClientTypes.Person);
            var loan = loanServices.SelectLoan(installment.ContractId, true, true, true);

            loanServices.Repay(loan, client, installment.InstallmentNumber, installment.InstallmentDate, installment.InstallmentAmount,
                false, 0, 0, false, 0, true, false, pPaymentMethod, "", true);

            return true;
        }

        public void ImportInstallmentRepayment(List<Installment> pInstallments)
        {
            LoanServices loanServices = new LoanServices(_user);
            ClientServices clientServices = new ClientServices(_user);
            foreach (var installment in pInstallments)
            {
                int loanId = loanServices.SelectLoanId(installment.ContractCode);
                var loan = loanServices.SelectLoan(loanId, true, true, true);
                var client = clientServices.FindTiersByContractId(loanId);
                if (installment.RepaymentStatus == 1)
                {
                    if (loan.InstallmentList[installment.InstallmentNumber].IsPending)
                        loanServices.ConfirmPendingRepayment(loan, client);
                    else if (!loan.InstallmentList[installment.InstallmentNumber].IsRepaid)
                    {
                        PaymentMethod paymentMethod =
                            ServicesProvider.GetInstance().GetPaymentMethodServices().GetPaymentMethodById(5);
                        loanServices.Repay(loan, client, installment.InstallmentNumber, installment.InstallmentDate, 
                            installment.InstallmentAmount, false,
                            0, 0, false, 0, true, false, paymentMethod, "", false);
                    }
                        
                }
                else
                {
                    if (loan.InstallmentList[installment.InstallmentNumber].IsPending)
                        loanServices.CancelPendingInstallments(loan);
                }
            }
        }
    }
}
