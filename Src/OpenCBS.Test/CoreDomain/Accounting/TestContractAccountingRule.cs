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

using NUnit.Framework;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.EconomicActivities;
using OpenCBS.Enums;
using OpenCBS.CoreDomain.Products;

namespace OpenCBS.Test.CoreDomain.Accounting
{
    [TestFixture]
    public class TestContractAccountingRule
    {
        [Test]
        public void Get_Set_Id()
        {
            ContractAccountingRule rule = new ContractAccountingRule { Id = 1 };
            Assert.AreEqual(1, rule.Id);
        }

        [Test]
        public void Get_Set_GenericAccount()
        {
            Account genericAccount = new Account { Id = 10 };
            ContractAccountingRule rule = new ContractAccountingRule { DebitAccount = genericAccount };
            Assert.AreEqual(genericAccount, rule.DebitAccount);
        }

        [Test]
        public void Get_Set_SpecificAccount()
        {
            Account specificAccount = new Account { Id = 20 };
            ContractAccountingRule rule = new ContractAccountingRule { CreditAccount = specificAccount };
            Assert.AreEqual(specificAccount, rule.CreditAccount);
        }

        [Test]
        public void Get_Set_ProductType()
        {
            ContractAccountingRule rule = new ContractAccountingRule { ProductType = OProductTypes.All };
            Assert.AreEqual(OProductTypes.All, rule.ProductType);

            rule.ProductType = OProductTypes.Saving;
            Assert.AreEqual(OProductTypes.Saving, rule.ProductType);
        }

        [Test]
        public void Get_Set_LoanProduct()
        {
            ContractAccountingRule rule = new ContractAccountingRule();
            Assert.AreEqual(null, rule.LoanProduct);

            LoanProduct loanProduct = new LoanProduct { Id = 20, Code = "B/L/A/B/L/A" };
            rule.LoanProduct = loanProduct;
            Assert.AreEqual(loanProduct, rule.LoanProduct);
        }

        [Test]
        public void Get_Set_ClientType()
        {
            ContractAccountingRule rule = new ContractAccountingRule { ClientType = OClientTypes.All };
            Assert.AreEqual(OClientTypes.All, rule.ClientType);

            rule.ClientType = OClientTypes.Person;
            Assert.AreEqual(OClientTypes.Person, rule.ClientType);
        }

        [Test]
        public void Get_Set_EconomicActivity()
        {
            ContractAccountingRule rule = new ContractAccountingRule();
            Assert.AreEqual(null, rule.EconomicActivity);

            EconomicActivity economicActivity = new EconomicActivity { Id = 1, Name = "Econimic Acvitity" };
            rule.EconomicActivity = economicActivity;
            Assert.AreEqual(economicActivity, rule.EconomicActivity);
        }

        [Test]
        public void Get_Set_Deleted()
        {
            ContractAccountingRule rule = new ContractAccountingRule { Deleted = true };
            Assert.AreEqual(true, rule.Deleted);

            rule = new ContractAccountingRule();
            Assert.AreEqual(false, rule.Deleted);
        }

        [Test]
        public void Get_Set_BookingDirection()
        {
            ContractAccountingRule rule = new ContractAccountingRule { BookingDirection = OBookingDirections.Credit };
            Assert.AreEqual(OBookingDirections.Credit, rule.BookingDirection);

            rule.BookingDirection = OBookingDirections.Both;
            Assert.AreEqual(OBookingDirections.Both, rule.BookingDirection);
        }
    }
}
