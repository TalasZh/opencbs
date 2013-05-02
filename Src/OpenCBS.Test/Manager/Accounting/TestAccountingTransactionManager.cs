//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright ?2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using System;
using System.Data;
using NUnit.Framework;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Enums;
using OpenCBS.Manager.Accounting;
using OpenCBS.Manager.Currencies;
using OpenCBS.Shared;

namespace OpenCBS.Test.Manager.Accounting
{
    [TestFixture]
    public class TestAccountingTransactionManager : BaseManagerTest
    {
        [Test]
        public void AddAccoutingTransaction_WithBooking()
        {
            AccountingTransactionManager accountingTransactionManager = (AccountingTransactionManager)container["AccountingTransactionManager"];

            AccountingTransaction accountingTransaction = new AccountingTransaction
            {
                Date = new DateTime(2009, 2, 3),
                User = new User { Id = 1 }
            };
            accountingTransaction.AddBooking(new Booking
                                                 {
                                                     Number = 1,
                                                     Amount = 1000,
                                                     CreditAccount = new Account {Id = 1},
                                                     DebitAccount = new Account {Id = 2},
                                                     Label = OAccountingLabels.Capital
                                                 });
            accountingTransaction.AddBooking(new Booking
                                                 {
                                                     Number = 2,
                                                     Amount = 1000,
                                                     CreditAccount = new Account {Id = 2},
                                                     DebitAccount = new Account {Id = 4},
                                                     Label = OAccountingLabels.Fees
                                                 });
            
        }

        private static void _AssertSelectedBooking(Booking pBooking,int pId, int pNumber, string pDebitAccount, 
            string pCreditAccount, OCurrency pAmount,OAccountingLabels pLabel, bool pIsExported)
        {
            Assert.AreEqual(pId, pBooking.Id);
            Assert.AreEqual(pNumber, pBooking.Number);
            Assert.AreEqual(pDebitAccount, pBooking.DebitAccount.Number);
            Assert.AreEqual(pCreditAccount, pBooking.CreditAccount.Number);
            Assert.AreEqual(pAmount.Value, pBooking.Amount.Value);
            Assert.AreEqual(pLabel, pBooking.Label);
            Assert.AreEqual(pIsExported, pBooking.IsExported);
        }

        private static void _AssertSelectedBooking(Booking pBooking, int pNumber, string pDebitAccount,
            string pCreditAccount, OCurrency pAmount, OAccountingLabels pLabel)
        {
            Assert.AreEqual(pNumber, pBooking.Number);
            Assert.AreEqual(pDebitAccount, pBooking.DebitAccount.Number);
            Assert.AreEqual(pCreditAccount, pBooking.CreditAccount.Number);
            Assert.AreEqual(pAmount.Value, pBooking.Amount.Value);
            Assert.AreEqual(pLabel, pBooking.Label);
        }

        private static void _AssertSelectedAccountingTransaction(AccountingTransaction pAccountingTransaction, DateTime pDate, User pUser)
        {
            Assert.AreEqual(pDate, pAccountingTransaction.Date);
            _AssertSelectedUser(pUser, pAccountingTransaction.User);
        }

        private static void _AssertSelectedUser(User pExpectedUser, User pActualUser)
        {
            Assert.AreEqual(pExpectedUser.UserName, pActualUser.UserName);
            Assert.AreEqual(pExpectedUser.FirstName, pActualUser.FirstName);
            Assert.AreEqual(pExpectedUser.LastName, pActualUser.LastName);
        }

        [Test]
        public void SelectExportableBooking_AllBookingsHasBeenSelected_NoExchangeRate()
        {
            AccountingTransactionManager accountingTransactionManager = (AccountingTransactionManager)container["AccountingTransactionManager"];
            DataTable idTable;
            DataTable table = accountingTransactionManager.SelectBookingsToExport("ExportAccounting_Transactions", null, out idTable);

            Assert.AreEqual(0, table.Rows.Count);
        }

        [Test]
        public void SelectExportableBooking_AllBookingsHasBeenSelected_ExchangeRate()
        {
            ExchangeRateManager exchangeRateManager = (ExchangeRateManager) container["ExchangeRateManager"];
            AccountingTransactionManager accountingTransactionManager = (AccountingTransactionManager)container["AccountingTransactionManager"];

            exchangeRateManager.Add(new DateTime(2009,1,1),3,new Currency{Id = 2});
            DataTable idTable;
            DataTable table = accountingTransactionManager.SelectBookingsToExport("ExportAccounting_Transactions", null, out idTable);

            Assert.AreEqual(0, table.Rows.Count);
        }

        private static void _AssertSelectedExportingBooking(BookingToExport pExport, DateTime pDate, string pDebitAccountLabel, string pCreditAccountLabel, 
            OCurrency pAmount, double? pExchangeRate, string pUserName, string pEventCode, string pContractCode, string pFundingLine, int pNumber,
            OAccountingLabels pAccountingLabel)
        {
            Assert.AreEqual(pDate, pExport.Date);
            Assert.AreEqual(pDebitAccountLabel, pExport.DebitLocalAccountNumber);
            Assert.AreEqual(pCreditAccountLabel, pExport.CreditLocalAccountNumber);
            Assert.AreEqual(pAmount.Value, pExport.InternalAmount.Value);
            Assert.AreEqual(pExchangeRate, pExport.ExchangeRate);
            Assert.AreEqual(pUserName, pExport.UserName);
            Assert.AreEqual(pEventCode, pExport.EventCode);
            Assert.AreEqual(pContractCode, pExport.ContractCode);
            Assert.AreEqual(pFundingLine, pExport.FundingLine);
            Assert.AreEqual(pNumber, pExport.Number);
            Assert.AreEqual(pAccountingLabel, pExport.AccountingLabel);
        }
    
        private static void _AssertSelectedViewableBooking(BookingToView pBooking, OBookingDirections pDirection, OCurrency pAmount, DateTime pDate, 
            double? pExchangeRate, string pContractCode, string pEventCode)
        {
            Assert.AreEqual(pDirection, pBooking.Direction);
            Assert.AreEqual(pAmount.Value, pBooking.AmountInternal.Value);
            Assert.AreEqual(pDate, pBooking.Date);
            Assert.AreEqual(pExchangeRate, pBooking.ExchangeRate);
            Assert.AreEqual(pContractCode, pBooking.ContractCode);
            Assert.AreEqual(pEventCode, pBooking.EventCode);
        }
    }
}