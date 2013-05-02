// LICENSE PLACEHOLDER

using System.Collections.Generic;
using NUnit.Framework;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.Manager;
using OpenCBS.Manager.Contracts;

namespace OpenCBS.Test.Manager
{
    public class TestInstallmentManagementProxy : BaseManagerTest
    {
        [Test]
        public void TestInitInstallmentManagement()
        {
            InstallmentManager installmentManagement = (InstallmentManager)container["InstallmentManager"];
            Assert.IsNotNull(installmentManagement);
            Assert.IsTrue(container.Count > 0);
        }

        [Test]
        public void TestDeleteInstallmentsByCredit()
        {
            InstallmentManager installmentManagement = (InstallmentManager)container["InstallmentManager"];
            LoanManager creditContractManagement = (LoanManager)container["LoanManager"];

            Loan myCredit = creditContractManagement.SelectLoan(1, true, true, true);
            List<Installment> list = installmentManagement.SelectInstallments(1);
            Assert.AreEqual(3, list.Count);
            
            installmentManagement.DeleteInstallments(myCredit.Id);
            list = installmentManagement.SelectInstallments(1);
            Assert.AreEqual(0, list.Count);
        }
    }
}
