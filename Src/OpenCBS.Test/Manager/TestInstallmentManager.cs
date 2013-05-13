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
using NUnit.Framework;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.Manager;
using OpenCBS.Shared;

namespace OpenCBS.Test.Manager
{
	/// <summary>
	/// Description r�sum�e de TestInstallmentManagement.
	/// </summary>
	[TestFixture]
	public class TestInstallmentManager : BaseManagerTest
	{
        [Test]
        public void AddInstallmentsInDatabase()
        {
            InstallmentManager installmentManager = (InstallmentManager) container["InstallmentManager"];

            Assert.AreEqual(3, installmentManager.SelectInstallments(1).Count);
            List<Installment> list = new List<Installment>
                                         {
                                             new Installment
                                                 {
                                                     Number = 4,
                                                     CapitalRepayment = 10,
                                                     ExpectedDate = new DateTime(2009, 1, 1),
                                                     InterestsRepayment = 3,
                                                     FeesUnpaid = 0,
                                                     StartDate = new DateTime(2008, 12, 1),
                                                     OLB = 0
                                                 },
                                             new Installment
                                                 {
                                                     Number = 5,
                                                     CapitalRepayment = 100,
                                                     InterestsRepayment = 3,
                                                     ExpectedDate = new DateTime(2009, 1, 1),
                                                     PaidCapital = 3,
                                                     PaidInterests = 4,
                                                     FeesUnpaid = 0,
                                                     PaidDate = new DateTime(2009, 1, 1),
                                                     StartDate = new DateTime(2008, 12, 1),
                                                     OLB = 0
                                                 }
                                         };
            installmentManager.AddInstallments(list,1);
            Assert.AreEqual(5, installmentManager.SelectInstallments(1).Count);
        }

        [Test]
        public void AddInstallmentsInDatabase_NoInstallment()
        {
            InstallmentManager installmentManager = (InstallmentManager)container["InstallmentManager"];

            Assert.AreEqual(3, installmentManager.SelectInstallments(1).Count);
            List<Installment> list = new List<Installment>();
                                         
            installmentManager.AddInstallments(list, 1);
            Assert.AreEqual(3, installmentManager.SelectInstallments(1).Count);
        }

        [Test]
        public void DeleteInstallments()
        {
            InstallmentManager installmentManager = (InstallmentManager)container["InstallmentManager"];

            Assert.AreEqual(3, installmentManager.SelectInstallments(1).Count);

            installmentManager.DeleteInstallments(1);
            Assert.AreEqual(0, installmentManager.SelectInstallments(1).Count);
        }

        [Test]
        public void SelectInstallments()
        {
            InstallmentManager installmentManager = (InstallmentManager)container["InstallmentManager"];
            List<Installment> list = installmentManager.SelectInstallments(1);

            Assert.AreEqual(3, list.Count);
            _AssertSelectedInstallment(list[0], 1, new DateTime(2007, 1, 1), 2, 20, 2, 10, null, 0);
            _AssertSelectedInstallment(list[1], 2, new DateTime(2007, 2, 1), 2, 20, 2, 10, null, 0);
            _AssertSelectedInstallment(list[2], 3, new DateTime(2007, 3, 1), 2, 20, 2, 10, null, 0);
        }

	    private static void _AssertSelectedInstallment(Installment pInstallment, int pNumber, DateTime pExpectedDate,OCurrency pInterestRepayment, OCurrency pCapitalRepayment,
            OCurrency pPaidInterest, OCurrency pPaidCapital, DateTime? pPaidDate,OCurrency pUnpaidFees)
	    {
            Assert.AreEqual(pNumber, pInstallment.Number);
            Assert.AreEqual(pExpectedDate, pInstallment.ExpectedDate);
            Assert.AreEqual(pInterestRepayment.Value, pInstallment.InterestsRepayment.Value);
            Assert.AreEqual(pCapitalRepayment.Value, pInstallment.CapitalRepayment.Value);
	        Assert.AreEqual(pPaidInterest.Value, pInstallment.PaidInterests.Value);
            Assert.AreEqual(pPaidCapital.Value, pInstallment.PaidCapital.Value);
            Assert.AreEqual(pPaidDate, pInstallment.PaidDate);
            Assert.AreEqual(pUnpaidFees.Value, pInstallment.FeesUnpaid.Value);
	    }

	    [Test]
        public void UpdateInstallment_NoReschedule()
        {
            InstallmentManager installmentManager = (InstallmentManager)container["InstallmentManager"];
            List<Installment> list = installmentManager.SelectInstallments(1);

            _AssertSelectedInstallment(list[0], 1, new DateTime(2007, 1, 1), 2, 20, 2, 10, null, 0);

            list[0].PaidDate = new DateTime(2009,3,3);
	        list[0].FeesUnpaid = 7;

	        installmentManager.UpdateInstallment(list[0], 1, 1);

            List<Installment> updatedList = installmentManager.SelectInstallments(1);
            _AssertSelectedInstallment(updatedList[0], 1, new DateTime(2007, 1, 1), 2, 20, 2, 10, new DateTime(2009, 3, 3), 7);
        }

        [Test]
        public void UpdateInstallment_Reschedule()
        {
            InstallmentManager installmentManager = (InstallmentManager)container["InstallmentManager"];
            List<Installment> list = installmentManager.SelectInstallments(1);

            _AssertSelectedInstallment(list[0], 1, new DateTime(2007, 1, 1), 2, 20, 2, 10, null, 0);

            list[0].PaidDate = new DateTime(2009, 3, 3);
            list[0].FeesUnpaid = 7;

            installmentManager.UpdateInstallment(list[0], 1, 1, true);

            List<Installment> updatedList = installmentManager.SelectInstallments(1);
            _AssertSelectedInstallment(updatedList[0], 1, new DateTime(2007, 1, 1), 2, 20, 2, 10, new DateTime(2009, 3, 3), 7);

        }
	}
}
