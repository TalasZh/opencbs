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
