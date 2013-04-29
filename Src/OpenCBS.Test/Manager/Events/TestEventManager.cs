//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright © 2006,2007 OCTO Technology & OXUS Development Network
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
using System.Collections.Generic;
using System.Data.SqlClient;
using NUnit.Framework;
using Octopus.CoreDomain;
using Octopus.CoreDomain.Accounting;
using Octopus.CoreDomain.Events;
using Octopus.CoreDomain.Events.Loan;
using Octopus.Manager.Events;
using Octopus.Shared;

namespace Octopus.Test.Manager.Events
{
    [TestFixture]
    public class TestEventManager : BaseManagerTest
    {
        [Test]
        public void SelectEvents_NoResult()
        {
            EventManager eventManager = (EventManager)container["EventManager"];
            Assert.AreEqual(0, eventManager.SelectEvents(-30).GetNumberOfEvents);
        }

        [Test]
        public void SelectEvents()
        {
            EventManager eventManager = (EventManager)container["EventManager"];
            EventStock eventStock = eventManager.SelectEvents(1);
            eventStock.SortEventsByDate();

            Assert.AreEqual(8, eventStock.GetNumberOfEvents);
            _AssertLoanDisbursmentEvent((LoanDisbursmentEvent)eventStock.GetEvent(0), new DateTime(2009, 1, 1), 1000, 10);
            _AssertBadLoanRepaymentEvent((BadLoanRepaymentEvent)eventStock.GetEvent(1), new DateTime(2009, 2, 1), 10, 1000, 24, 2, 1);
            _AssertRepaymentEvent((RepaymentEvent)eventStock.GetEvent(3), "RGLE", new DateTime(2009, 4, 1), 10, 1000, 24, 2, 1);
            _AssertWriteOffEvent((WriteOffEvent)eventStock.GetEvent(4), new DateTime(2009, 5, 1), 1000, 20, 24, 1);
            _AssertReschedulingLoanEvent((RescheduleLoanEvent)eventStock.GetEvent(5), new DateTime(2009, 6, 1), 1000, 2, 0);
            _AssertLoanInterestAccruingEvent((AccruedInterestEvent) eventStock.GetEvent(6), new DateTime(2009, 7, 1), 342, 22, true, 3,false);
            _AssertRescheduledLoanRepaymentEvent((RescheduledLoanRepaymentEvent)eventStock.GetEvent(7), new DateTime(2009, 8, 1), 105, 544, 5, 7, 8);
        }

        private static void _AssertRescheduledLoanRepaymentEvent(RescheduledLoanRepaymentEvent pEvent, DateTime pDate, int pPastDueDays, OCurrency pPrincipal,
                                                         OCurrency pInterest, OCurrency pFees, int pInstallmentNumber)
        {
            _AssertRepaymentEvent(pEvent, "RRLE", pDate, pPastDueDays, pPrincipal, pInterest, pFees, pInstallmentNumber);
        }

        private static void _AssertLoanInterestAccruingEvent(AccruedInterestEvent pEvent, DateTime pDate, OCurrency pInterestPrepayment, OCurrency pAccruedInterest, bool pRescheduled, 
            int pInstallmentNumber, bool pDeleted)
        {
            Assert.AreEqual("LIAE", pEvent.Code);
            Assert.AreEqual(pDate, pEvent.Date);
            Assert.AreEqual(pInterestPrepayment.Value, pEvent.Interest.Value);
            Assert.AreEqual(pAccruedInterest.Value, pEvent.AccruedInterest.Value);
            Assert.AreEqual(pRescheduled, pEvent.Rescheduled);
            Assert.AreEqual(pInstallmentNumber, pEvent.InstallmentNumber);
            Assert.AreEqual(pDeleted, pEvent.Deleted);
        }

        private static void _AssertRegEvent(RegEvent pEvent, DateTime pDate, bool pDeleted)
        {
            Assert.AreEqual("REGE", pEvent.Code);
            Assert.AreEqual(pDate, pEvent.Date);
            Assert.AreEqual(pDeleted, pEvent.Deleted);
        }

        private static void _AssertReschedulingLoanEvent(RescheduleLoanEvent pEvent, DateTime pDate, OCurrency pAmount, int pNbOfMaturity, int dateOffset)
        {
            Assert.AreEqual("ROLE", pEvent.Code);
            Assert.AreEqual(pDate, pEvent.Date);
            Assert.AreEqual(pAmount.Value, pEvent.Amount.Value);
            Assert.AreEqual(pNbOfMaturity, pEvent.NbOfMaturity);
            Assert.AreEqual(dateOffset, pEvent.DateOffset);
        }

        private static void _AssertWriteOffEvent(WriteOffEvent pEvent, DateTime pDate, OCurrency pOLB, OCurrency pAccruedInterests, OCurrency pAccruedPenalties,
            int pPastDueDays)
        {
            Assert.AreEqual("WROE", pEvent.Code);
            Assert.AreEqual(pDate, pEvent.Date);
            Assert.AreEqual(pPastDueDays, pEvent.PastDueDays);
            Assert.AreEqual(pOLB.Value, pEvent.OLB.Value);
            Assert.AreEqual(pAccruedInterests.Value, pEvent.AccruedInterests.Value);
            Assert.AreEqual(pAccruedPenalties.Value, pEvent.AccruedPenalties.Value);
        }

        private static void _AssertRepaymentEvent(RepaymentEvent pEvent, string pCode, DateTime pDate, int pPastDueDays, OCurrency pPrincipal,
                                                         OCurrency pInterest, OCurrency pFees, int pInstallmentNumber)
        {
            Assert.AreEqual(pCode, pEvent.Code);
            Assert.AreEqual(pDate, pEvent.Date);
            Assert.AreEqual(pPastDueDays, pEvent.PastDueDays);
            Assert.AreEqual(pPrincipal.Value, pEvent.Principal.Value);
            Assert.AreEqual(pInterest.Value, pEvent.Interests.Value);
            Assert.AreEqual(pFees.Value, pEvent.Fees.Value);
            Assert.AreEqual(pInstallmentNumber, pEvent.InstallmentNumber);
        }

        private static void _AssertBadLoanRepaymentEvent(BadLoanRepaymentEvent pEvent, DateTime pDate, int pPastDueDays, OCurrency pPrincipal,
                                                         OCurrency pInterest, OCurrency pFees, int pInstallmentNumber)
        {
            _AssertRepaymentEvent(pEvent, "RBLE", pDate, pPastDueDays, pPrincipal, pInterest, pFees, pInstallmentNumber);
        }

        private static void _AssertLoanDisbursmentEvent(LoanDisbursmentEvent pEvent, DateTime pDate, OCurrency pAmount, OCurrency pCommission)
        {
            Assert.AreEqual("LODE", pEvent.Code);
            Assert.AreEqual(pDate, pEvent.Date);
            Assert.AreEqual(pAmount.Value, pEvent.Amount.Value);
        }

        [Test]
        public void AddLoanDisbursmentEvent()
        {
            EventManager eventManager = (EventManager)container["EventManager"];

            LoanDisbursmentEvent loanDisbursmentEvent = new LoanDisbursmentEvent
                                            {
                                                Id = 10,
                                                User = new User { Id = 1 },
                                                Date = new DateTime(2006, 7, 21),
                                                Amount = 100,
                                            };
            loanDisbursmentEvent.Commissions = new List<LoanEntryFeeEvent>();
            LoanEntryFeeEvent commission = new LoanEntryFeeEvent();
            commission.Fee = 10;
            loanDisbursmentEvent.Commissions.Add(commission);
            
            PaymentMethod method = new PaymentMethod(1, "Savings", "Savings method", false);
            loanDisbursmentEvent.PaymentMethod = method;

            eventManager.AddLoanEvent(loanDisbursmentEvent, 1);
            Assert.AreNotEqual(0, loanDisbursmentEvent.Id);
        }

        [Test]
        public void Select_Added_loanDisbursmentEvent()
        {
            EventManager eventManager = (EventManager)container["EventManager"];

            LoanDisbursmentEvent loanDisbursmentEvent = new LoanDisbursmentEvent
            {
                Id = 100,
                User = new User { Id = 1 },
                Date = new DateTime(2006, 7, 21),
                Amount = 100,
            };
            var commission = new LoanEntryFeeEvent();
            commission.Fee = 10;
            loanDisbursmentEvent.Commissions = new List<LoanEntryFeeEvent>();
            loanDisbursmentEvent.Commissions.Add(commission);

            PaymentMethod method = new PaymentMethod(1, "Savings", "Savings method", false);
            loanDisbursmentEvent.PaymentMethod = method;

            eventManager.AddLoanEvent(loanDisbursmentEvent, 2);
            EventStock eventStock = eventManager.SelectEvents(2);

            foreach (Event e in eventStock.GetEvents())
            {
                if (e is LoanDisbursmentEvent)
                    _AssertLoanDisbursmentEvent(e as LoanDisbursmentEvent, new DateTime(2006, 7, 21), 100, 10);
            }
        }

        [Test]
        public void Select_Added_RepaymentEvent()
        {
           EventManager eventManager = (EventManager)container["EventManager"];

            RepaymentEvent repaymentEvent = new RepaymentEvent
                                     {
                                         Id = 130,
                                         Code = "RGLE",
                                         User = new User {Id = 1},
                                         Date = new DateTime(2006, 7, 21),
                                         PastDueDays =  3,
                                         Principal = 1999,
                                         Interests = 2333,
                                         Commissions = 23,
                                         InstallmentNumber = 3,
                                         PaymentMethod = new PaymentMethod(1, "Cash", "", false)
                                     };
            using (SqlTransaction tran = eventManager.GetConnection().BeginTransaction())
            eventManager.AddLoanEvent(repaymentEvent, 1, tran);

            EventStock eventStock = eventManager.SelectEvents(2);
            foreach (Event e in eventStock.GetEvents())
            {
                if (e is RepaymentEvent)
                    _AssertRepaymentEvent(e as RepaymentEvent, "RGLE", new DateTime(2006, 7, 21), 3, 1999, 2333, 23, 3);
            }
        }

        [Test]
        public void Add_RepaymentEvent()
        {
            EventManager eventManager = (EventManager)container["EventManager"];

            RepaymentEvent repaymentEvent = new RepaymentEvent
            {
                Id = 13,
                User = new User { Id = 1 },
                Date = new DateTime(2006, 7, 21),
                PastDueDays = 3,
                Principal = 1999,
                Interests = 2333,
                Commissions = 23,
                InstallmentNumber = 3,
                PaymentMethod = new PaymentMethod(1, "Cash", "", false)
            };
            using(SqlTransaction tran = eventManager.GetConnection().BeginTransaction())
                eventManager.AddLoanEvent(repaymentEvent, 1, tran);

            Assert.AreNotEqual(0, repaymentEvent.Id);
        }

        [Test]
        public void Add_ReschedulingLoanEvent()
        {
            EventManager eventManager = (EventManager)container["EventManager"];

            RescheduleLoanEvent rescheduleLoanEvent = new RescheduleLoanEvent
                                               {
                                                   Id = 14,
                                                   User = new User { Id = 1 },
                                                   Date = new DateTime(2006, 7, 21),
                                                   Amount = 2345,
                                                   NbOfMaturity = 4,
                                                   DateOffset = 0,
                                                   Interest = 100
                                               };
            eventManager.AddLoanEvent(rescheduleLoanEvent, 1);
            Assert.AreNotEqual(0, rescheduleLoanEvent.Id);
        }

        [Test]
        public void Select_Added_ReschedulingLoanEvent()
        {
            EventManager eventManager = (EventManager)container["EventManager"];

            RescheduleLoanEvent rescheduleLoanEvent = new RescheduleLoanEvent
            {
                Id = 14,
                User = new User { Id = 1 },
                Date = new DateTime(2006, 7, 21),
                Amount = 2345,
                NbOfMaturity = 4,
                DateOffset = 0,
                Interest = 100
            };
            eventManager.AddLoanEvent(rescheduleLoanEvent, 1);
            
            EventStock eventStock = eventManager.SelectEvents(2);
            foreach (Event e in eventStock.GetEvents())
            {
                if (e is RescheduleLoanEvent)
                    _AssertReschedulingLoanEvent(e as RescheduleLoanEvent, new DateTime(2006, 7, 21), 2345, 3, 4);
            }
        }
        
        [Test]
        public void Add_WriteOffEvent()
        {
            EventManager eventManager = (EventManager)container["EventManager"];

            WriteOffEvent writeOffEvent = new WriteOffEvent
            {
                Id = 15,
                User = new User { Id = 1 },
                Date = new DateTime(2006, 7, 21),
                OLB = 34,
                AccruedInterests = 355,
                AccruedPenalties = 433,
                PastDueDays = 3,
                OverduePrincipal = 0
            };

            eventManager.AddLoanEvent(writeOffEvent, 1);
            Assert.AreNotEqual(0, writeOffEvent.Id);
        }

        [Test]
        public void Select_Added_WriteOffEvent()
        {
            EventManager eventManager = (EventManager)container["EventManager"];

            WriteOffEvent writeOffEvent = new WriteOffEvent
            {
                Id = 15,
                User = new User { Id = 1 },
                Date = new DateTime(2006, 7, 21),
                OLB = 34,
                AccruedInterests = 355,
                AccruedPenalties = 433,
                PastDueDays = 3,
                OverduePrincipal = 0
            };

            eventManager.AddLoanEvent(writeOffEvent, 1);

            EventStock eventStock = eventManager.SelectEvents(2);
            foreach (Event e in eventStock.GetEvents())
            {
                if (e is WriteOffEvent)
                    _AssertWriteOffEvent(e as WriteOffEvent, new DateTime(2006, 7, 21), 34, 355, 433, 3);
            }
        }

        [Test]
        public void Add_RegEvent()
        {
            EventManager eventManager = (EventManager)container["EventManager"];
            RegEvent regEvent = new RegEvent
                                    {
                                        Id = 15,
                                        User = new User { Id = 1 },
                                        Date = new DateTime(2006, 7, 21)
                                    };

            eventManager.AddLoanEvent(regEvent, 1);
            Assert.AreNotEqual(0, regEvent.Id);
        }

        [Test]
        public void Select_Added_RegEvent()
        {
            EventManager eventManager = (EventManager)container["EventManager"];
            RegEvent regEvent = new RegEvent
            {
                Id = 15,
                User = new User { Id = 1 },
                Date = new DateTime(2006, 7, 21)
            };

            eventManager.AddLoanEvent(regEvent, 1);

            EventStock eventStock = eventManager.SelectEvents(2);
            foreach (Event e in eventStock.GetEvents())
            {
                if (e is RegEvent)
                    _AssertRegEvent(e as RegEvent, new DateTime(2006, 7, 21),false);
            }
        }

        [Test]
        public void Add_LoanInterestAccruingEvent()
        {
            EventManager eventManager = (EventManager)container["EventManager"];
            AccruedInterestEvent loanInterestAccruingEvent = new AccruedInterestEvent
            {
                Id = 15,
                User = new User { Id = 1 },
                Date = new DateTime(2006, 7, 21),
                Interest = 99,
                AccruedInterest = 3,
                Rescheduled = true,
                InstallmentNumber = 4
            };
            eventManager.AddLoanEvent(loanInterestAccruingEvent, 1);
            Assert.AreNotEqual(0, loanInterestAccruingEvent.Id);
        }

        [Test]
        public void Delete_Event()
        {
            EventManager eventManager = (EventManager)container["EventManager"];
            //I choose LoanInterestAccruingEvent, but delete work with any event
            AccruedInterestEvent loanInterestAccruingEvent = new AccruedInterestEvent
            {
                Id = 15,
                User = new User { Id = 1 },
                Date = new DateTime(2006, 7, 21),
                Interest = 99,
                AccruedInterest = 3,
                Rescheduled = true,
                InstallmentNumber = 4
            };
            eventManager.AddLoanEvent(loanInterestAccruingEvent, 3);
            loanInterestAccruingEvent.CancelDate = DateTime.Now;
            eventManager.DeleteLoanEvent(loanInterestAccruingEvent);

            EventStock eventStock = eventManager.SelectEvents(3);
            foreach (Event e in eventStock.GetEvents())
            {
                if (e is AccruedInterestEvent)
                    _AssertLoanInterestAccruingEvent(e as AccruedInterestEvent, new DateTime(2006, 7, 21), 99, 3, true, 4,true);
            }
        }

        [Test]
        public void Select_Added_LoanInterestAccruingEvent()
        {
            EventManager eventManager = (EventManager)container["EventManager"];
            AccruedInterestEvent loanInterestAccruingEvent = new AccruedInterestEvent
            {
                Id = 15,
                User = new User { Id = 1 },
                Date = new DateTime(2006, 7, 21),
                Interest = 99,
                AccruedInterest = 3,
                Rescheduled = true,
                InstallmentNumber = 4
            };
            eventManager.AddLoanEvent(loanInterestAccruingEvent, 1);

            EventStock eventStock = eventManager.SelectEvents(2);
            foreach (Event e in eventStock.GetEvents())
            {
                if (e is AccruedInterestEvent)
                    _AssertLoanInterestAccruingEvent(e as AccruedInterestEvent, new DateTime(2006, 7, 21), 99, 3, true, 4,false);
            }
        }
    }
}