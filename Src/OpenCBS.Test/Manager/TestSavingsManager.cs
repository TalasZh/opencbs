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
using NUnit.Framework;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Manager.Events;
using OpenCBS.Manager.Products;
using OpenCBS.Manager.Contracts;
using OpenCBS.Enums;
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.Shared.Settings;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.SearchResult;
using System.Collections.Generic;
using OpenCBS.CoreDomain.Accounting;

namespace OpenCBS.Test.Manager
{
    [TestFixture]
    public class TestSavingsManager : BaseManagerTest
    {
        private SavingManager _savingManager;
        private SavingProductManager _productManager;
        private SavingsBookProduct _savingsBookProduct;
        private SavingEventManager _savingEventManager;

        [SetUp]
        protected override void SetUp()
        {
            base.SetUp();

            _productManager = (SavingProductManager)container["SavingProductManager"];
            _savingManager = (SavingManager)container["SavingManager"];
            _savingEventManager = (SavingEventManager)container["SavingEventManager"];

            Assert.IsNotNull(_savingManager);
            Assert.IsNotNull(_productManager);

            _savingsBookProduct = _productManager.SelectSavingsBookProduct(1);
            

            Assert.IsNotNull(_savingsBookProduct);

            User.CurrentUser.Id = 1;
        }

        public void _compareSavings(ISavingsContract originalSaving, ISavingsContract retrievedSaving)
        {
            Assert.AreEqual(originalSaving.Id, retrievedSaving.Id);
            Assert.AreEqual(originalSaving.InterestRate, retrievedSaving.InterestRate);
            Assert.AreEqual(originalSaving.Product.Id, retrievedSaving.Product.Id);
            Assert.AreEqual(originalSaving.GetBalance(), retrievedSaving.GetBalance());
            Assert.AreEqual(originalSaving.Code, retrievedSaving.Code);
            Assert.AreEqual(originalSaving.CreationDate, retrievedSaving.CreationDate);
            Assert.AreEqual(originalSaving.Status, retrievedSaving.Status);
            
            Assert.AreEqual(((SavingBookContract)originalSaving).FlatWithdrawFees.HasValue, ((SavingBookContract)retrievedSaving).FlatWithdrawFees.HasValue);
            if (((SavingBookContract)originalSaving).FlatWithdrawFees.HasValue)
                Assert.AreEqual(((SavingBookContract)originalSaving).FlatWithdrawFees.Value, ((SavingBookContract)retrievedSaving).FlatWithdrawFees.Value);
            Assert.AreEqual(((SavingBookContract)originalSaving).RateWithdrawFees.HasValue, ((SavingBookContract)retrievedSaving).RateWithdrawFees.HasValue);
            if (((SavingBookContract)originalSaving).RateWithdrawFees.HasValue)
                Assert.AreEqual(((SavingBookContract)originalSaving).RateWithdrawFees.Value, ((SavingBookContract)retrievedSaving).RateWithdrawFees.Value);

            Assert.AreEqual(((SavingBookContract)originalSaving).FlatTransferFees.HasValue, ((SavingBookContract)retrievedSaving).FlatTransferFees.HasValue);
            if (((SavingBookContract)originalSaving).FlatTransferFees.HasValue)
                Assert.AreEqual(((SavingBookContract)originalSaving).FlatTransferFees.Value, ((SavingBookContract)retrievedSaving).FlatTransferFees.Value);
            Assert.AreEqual(((SavingBookContract)originalSaving).RateTransferFees.HasValue, ((SavingBookContract)retrievedSaving).RateTransferFees.HasValue);
            if (((SavingBookContract)originalSaving).RateTransferFees.HasValue)
                Assert.AreEqual(((SavingBookContract) originalSaving).RateTransferFees.Value,
                                ((SavingBookContract) retrievedSaving).RateTransferFees.Value);
        }

        [Test]
        public void AddSavingBook()
        {
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  
                new User() { Id = 1 }, new DateTime(2009, 01, 01), _savingsBookProduct, null)
            {
                Code = "S/CR/2009/SAVIN-1/BAR-1", Status = OSavingsStatus.Active, InterestRate = 0.01, FlatWithdrawFees = 3, FlatTransferFees = 3
            };
            saving.InitialAmount = 1000;
            saving.EntryFees = 10;
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            User user = new User {Id = 1};
            saving.Events[0].User = user;
            saving.Events[0].Id = 400;
            saving.SavingsOfficer = user;
            saving.Id = _savingManager.Add(saving, new Person { Id = 6 });
            _savingEventManager.Add(saving.Events[0], saving.Id);

            SavingBookContract retrievedSaving = (SavingBookContract)_savingManager.Select(saving.Id);

            _compareSavings(saving, retrievedSaving);
        }
        
        [Test]
        public void UpdateAccountsBalanceSaving()
        {
            Assert.Ignore(); // Ru55
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User() { Id = 1 },  
                new DateTime(2009, 01, 01), _savingsBookProduct, null)
            {
                Code = "S/CR/2009/SAVIN-1/BAR-2",
                Status = OSavingsStatus.Active,
                InterestRate = 0.01,
                FlatWithdrawFees = 3,
                FlatTransferFees = 3,
                AgioFees = 0.1
            };
            saving.Events[0].User = new User() { Id = 1 };
            saving.Id = _savingManager.Add(saving, new Person { Id = 6 });

            saving = (SavingBookContract)_savingManager.Select(saving.Id);

            saving.Closure(new DateTime(2009, 02, 01), new User { Id = 1 });
            saving.Withdraw(50, new DateTime(2009, 02, 01), "testWithdraw", new User { Id = 1 }, false, null);
            saving.Closure(new DateTime(2010, 01, 01), new User { Id = 1 });

            SavingBookContract retrievedSaving = (SavingBookContract)_savingManager.Select(saving.Id);
        }

       
        [Test]
        public void SearchSavingByCritere()
        {
            string query = "2007";

            Assert.AreEqual(_savingManager.GetNumberSavingContract(query, false,false), 1);
            List<SavingSearchResult> result = _savingManager.SearchSavingContractByCritere(1, query, false, false);

            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual("S/BC/2007/SAVIN-1/ELFA-6", result[0].ContractCode);
            Assert.AreEqual(new DateTime(2007, 01, 01), result[0].ContractStartDate);
            Assert.AreEqual(OClientTypes.Person, result[0].ClientType);
            Assert.AreEqual("Mariam EL FANIDI", result[0].ClientName);
            Assert.AreEqual(6, result[0].ClientId);
            Assert.AreEqual(OSavingsStatus.Active, result[0].Status);
            Assert.AreEqual(false, result[0].ContractEndDate.HasValue);
            Assert.AreEqual("B", result[0].ContractType);
        }

        [Test]
        public void SelectAll()
        {
            List<ISavingsContract> listSaving = _savingManager.SelectAll();
            Assert.AreEqual(listSaving.Count, 2);

            Assert.AreEqual(listSaving[0].Id, 1);
            Assert.AreEqual(listSaving[0].Code, "S/BC/2007/SAVIN-1/ELFA-6");
            Assert.AreEqual(listSaving[0].Status, OSavingsStatus.Active);
            Assert.AreEqual(listSaving[0].CreationDate, new DateTime(2007, 01, 01));
            Assert.AreEqual(listSaving[0].InterestRate, 0.25);
            Assert.AreEqual(listSaving[0].Product.Id, 1);
            Assert.AreEqual(listSaving[0].Product.Name, "SavingProduct1");
            Assert.AreEqual(listSaving[0].GetBalance(), 100);
            Assert.AreEqual(((SavingBookContract)listSaving[0]).FlatWithdrawFees, 3);
            Assert.AreEqual(((SavingBookContract)listSaving[0]).RateWithdrawFees.HasValue, false);
            Assert.AreEqual(((SavingBookContract)listSaving[0]).FlatTransferFees, 3);
            Assert.AreEqual(((SavingBookContract)listSaving[0]).RateTransferFees.HasValue, false);

            Assert.AreEqual(listSaving[1].Id, 2);
            Assert.AreEqual(listSaving[1].Code, "S/BC/2008/SAVIN-1/ELFA-7");
            Assert.AreEqual(listSaving[1].Status, OSavingsStatus.Closed);
            Assert.AreEqual(listSaving[1].CreationDate, new DateTime(2008, 01, 02));
            Assert.AreEqual(listSaving[1].ClosedDate, new DateTime(2008, 10, 01));
            Assert.AreEqual(listSaving[1].InterestRate, 0.30);
            Assert.AreEqual(listSaving[1].Product.Id, 1);
            Assert.AreEqual(listSaving[1].Product.Name, "SavingProduct1");
            Assert.AreEqual(listSaving[1].GetBalance(), 0);
            Assert.AreEqual(((SavingBookContract)listSaving[1]).FlatWithdrawFees, 3);
            Assert.AreEqual(((SavingBookContract)listSaving[1]).RateWithdrawFees.HasValue, false);
            Assert.AreEqual(((SavingBookContract)listSaving[1]).FlatTransferFees, 3);
            Assert.AreEqual(((SavingBookContract)listSaving[1]).RateTransferFees.HasValue, false);
        }

        [Test]
        public void CloseSaving()
        {
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  
                new User() { Id = 1 }, new DateTime(2009, 01, 01), _savingsBookProduct, null)
            {
                Code = "S/CR/2009/SAVIN-1/BAR-1",
                Status = OSavingsStatus.Active,
                InterestRate = 0.01, 
                FlatWithdrawFees = 3
            };
            saving.InitialAmount = 1000;
            saving.EntryFees = 10;
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            User user = new User {Id = 1};
            saving.Events[0].User = user;
            saving.SavingsOfficer = user;
            saving.Id = _savingManager.Add(saving, new Person() { Id = 6 });

            saving.Status = OSavingsStatus.Closed;
            saving.ClosedDate = new DateTime(2009, 10, 01);
            _savingManager.UpdateStatus(saving.Id, saving.Status, saving.ClosedDate.Value);

            SavingBookContract retrievedSaving = (SavingBookContract)_savingManager.Select(saving.Id);

            Assert.AreEqual(OSavingsStatus.Closed, retrievedSaving.Status);
            Assert.AreEqual(new DateTime(2009, 10, 01), retrievedSaving.ClosedDate.Value);
        }

        [Test]
        public void GetContractCodesForClient()
        {
            const int tiersId = 6;
            var savings = _savingManager.SelectClientSavingBookCodes(tiersId, null);
            Assert.AreEqual(1, savings.Length);
            var saving = savings[0];
            Assert.AreEqual(1, saving.Key);
            Assert.AreEqual("S/BC/2007/SAVIN-1/ELFA-6", saving.Value);
        }

        [Test]
        public void GetContractCodesForClientWithCurrency()
        {
            const int tiersId = 6;
            var savings = _savingManager.SelectClientSavingBookCodes(tiersId, 1);
            Assert.AreEqual(1, savings.Length);
            var saving = savings[0];
            Assert.AreEqual(1, saving.Key);
            Assert.AreEqual("S/BC/2007/SAVIN-1/ELFA-6", saving.Value);
        }
    }
}
