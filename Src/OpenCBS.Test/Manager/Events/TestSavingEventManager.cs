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
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.Manager.Events;
using OpenCBS.Shared;
using OpenCBS.Test.Manager;
using OpenCBS.Shared.Settings;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Manager.Contracts;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Enums;
using OpenCBS.CoreDomain.Events.Saving;

namespace OpenCBS.Test.EventsManager
{
	[TestFixture]
	public class TestSavingEventManager : BaseManagerTest
	{
		private SavingEventManager _savingEventManager;
        private SavingManager _savingManager;
        private Client _client;
		private User _user;
        private SavingsBookProduct _product;
        private SavingBookContract _saving;
        private SavingBookContract _savingTarget;

        [SetUp]
        protected override void SetUp()
        {
            base.SetUp();

            ChartOfAccounts.SuppressAll();

            _savingManager = (SavingManager)container["SavingManager"];
            _savingEventManager = (SavingEventManager)container["SavingEventManager"];

            _client = new Person { Id = 6 };
            _user = new User { Id = 1 };
            User.CurrentUser = _user;
            _product = new SavingsBookProduct
            {
                Id = 1,
                Name = "SavingProduct1",
                InitialAmountMin = 100,
                InitialAmountMax = 500,
                BalanceMin = 0,
                BalanceMax = 1000,
                WithdrawingMin = 100,
                WithdrawingMax = 150,
                DepositMin = 200,
                DepositMax = 250,
                TransferMin = 100,
                TransferMax = 200,
                InterestRateMin = 0.2,
                InterestRateMax = 0.3,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                Currency = new Currency { Id = 1 },
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 10,
                TransferFeesType = OSavingsFeesType.Rate,
                RateTransferFees = 0.1
            };

            _saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  _user, new DateTime(2009, 01, 01), _product, null)
            {
                Code = "S/CR/2009/SAVIN-1/BAR-1",
                InterestRate = 0.2,
                FlatWithdrawFees = 10,
                RateTransferFees = 0.1
            };
            _saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            _savingTarget = _saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  _user, new DateTime(2009, 01, 01), _product, null)
            {
                Code = "S/CR/2009/SAVIN-1/BAR-2",
                InterestRate = 0.2,
            };

            _saving.InitialAmount = 1000;
            _saving.EntryFees = 10;
            _saving.SavingsOfficer = _user;
            _saving.NumberOfPeriods = 0;
            _saving.Id = _savingManager.Add(_saving, _client);
            _savingTarget.Id = _savingManager.Add(_savingTarget, _client);

            Branch branch = new Branch {Id = 1, Name = "Default", Code = "Default"};
            _saving.Branch = branch;
            _savingTarget.Branch = branch;
        }

        public void CompareEvent(SavingEvent originalEvent, SavingEvent retrievedEvent)
        {
            Assert.AreEqual(originalEvent.Amount, retrievedEvent.Amount);
            Assert.AreEqual(originalEvent.User.Id, retrievedEvent.User.Id);
            Assert.AreEqual(originalEvent.Code, retrievedEvent.Code);
            Assert.AreEqual(originalEvent.Date, retrievedEvent.Date);
            Assert.AreEqual(originalEvent.Cancelable, retrievedEvent.Cancelable);
            Assert.AreEqual(originalEvent.IsFired, retrievedEvent.IsFired);
            if (originalEvent is SavingTransferEvent)
                Assert.AreEqual(((SavingTransferEvent)originalEvent).RelatedContractCode, ((SavingTransferEvent)retrievedEvent).RelatedContractCode);
            if (originalEvent is ISavingsFees)
                if (((ISavingsFees)originalEvent).Fee.HasValue || ((ISavingsFees)retrievedEvent).Fee.HasValue)
                    Assert.AreEqual(((ISavingsFees)originalEvent).Fee, ((ISavingsFees)retrievedEvent).Fee);
        }

        [Test]
        public void TestInitialDepositEvent()
        {
            Assert.Ignore();
            Assert.AreEqual(_savingEventManager.Add(_saving.Events[0], _saving.Id), _saving.Events[0].Id);
            SavingEvent retrievedSavingEvent = _savingEventManager.SelectEvents(_saving.Id, _saving.Product)[0];
            CompareEvent(_saving.Events[0], retrievedSavingEvent);
        }

        [Test]
        public void TestDepositEvent()
        {
            List<SavingEvent> savingEventList = _saving.Deposit(100, TimeProvider.Today, "testDepot", _user, false, false, OSavingsMethods.Cash, null, null);
            foreach (SavingEvent savingEvent in savingEventList)
            {
                savingEvent.Id = _savingEventManager.Add(savingEvent, _saving.Id);
                SavingEvent retrievedSavingEvent = _savingEventManager.SelectEvents(_saving.Id, _saving.Product).Find(item => item.Id == savingEvent.Id);
                CompareEvent(savingEvent, retrievedSavingEvent);
            }
        }

        [Test]
        public void TestWithdrawEvent()
        {
            _saving.FlatWithdrawFees = 10;
            List<SavingEvent> savingEvents = _saving.Withdraw(100, TimeProvider.Today, "testDepot", _user, false, null);
            savingEvents[0].Id = 100;
            _savingEventManager.Add(savingEvents[0], _saving.Id);
            SavingEvent retrievedSavingEvent = _savingEventManager.SelectEvents(_saving.Id, _saving.Product).Find(item => item.Id == savingEvents[0].Id);

            CompareEvent(savingEvents[0], retrievedSavingEvent);
        }

        [Test]
        public void TestAccrualEvent()
        {
            int i = 10;
            foreach (SavingEvent savingEvent in _saving.CalculateInterest(new DateTime(2009, 01, 05), _user))
            {
                savingEvent.Id = i;
                savingEvent.Id = _savingEventManager.Add(savingEvent, _saving.Id);
                SavingEvent retrievedEvent = _savingEventManager.SelectEvents(_saving.Id, _saving.Product).Find(item => item.Id == savingEvent.Id);

                CompareEvent(savingEvent, retrievedEvent);

                i++;
            }
        }

        [Test]
        public void Transfer_Events()
        {
            Assert.Ignore();
            List<SavingEvent> events = _saving.Transfer(_savingTarget, 50, 0, DateTime.Today, "Transfer description");
            events[0].Id = _savingEventManager.Add(events[0], _saving.Id);
            events[1].Id = _savingEventManager.Add(events[1], _saving.Id);
            SavingEvent e1 = _savingEventManager.SelectEvents(_saving.Id, _saving.Product).Find(item => item.Id == events[0].Id);
            SavingEvent e2 = _savingEventManager.SelectEvents(_saving.Id, _saving.Product).Find(item => item.Id == events[1].Id);
            CompareEvent(events[0], e1);
            CompareEvent(events[1], e2);
        }

        [Test]
        public void TestFireEvent()
        {
            //SavingEvent savingEvent = _saving.Deposit(100, new DateTime(2009, 02, 01), "testDepot", _user, false);
//            List<SavingEvent> savingEventList = _saving.Deposit(100, new DateTime(2009, 02, 01), "testDepot", _user, false, false, OPaymentMethods.Cash, null, null);
            List<SavingEvent> savingEventList = _saving.Deposit(100, new DateTime(2009, 02, 01), "testDepot", _user, false, false, OSavingsMethods.Cash, null, null);
            foreach (var savingEvent in savingEventList)
            {
                savingEvent.Id = _savingEventManager.Add(savingEvent, _saving.Id);
                _savingEventManager.FireEvent(savingEvent.Id);
                SavingEvent retrievedEvent = _savingEventManager.SelectEvents(_saving.Id, _saving.Product).Find(item => item.Id == savingEvent.Id);
                Assert.AreEqual(retrievedEvent.IsFired, true);
            }
        }

        [Test]
        public void TestDeleteEvent()
        {
            _saving.FlatWithdrawFees = 0;
            List<SavingEvent> savingEvents = _saving.Withdraw(100, new DateTime(2009, 02, 01), "testRetrait", _user, false, null);
            savingEvents[0].Id = _savingEventManager.Add(savingEvents[0], _saving.Id);
            savingEvents[0].CancelDate = DateTime.Now;

            _savingEventManager.DeleteEventInDatabase(savingEvents[0]);

            SavingEvent retrievedEvent = _savingEventManager.SelectEvents(_saving.Id, _saving.Product).Find(item => item.Id == savingEvents[0].Id);
            Assert.AreEqual(retrievedEvent.Deleted, true);
        }
	}
}
