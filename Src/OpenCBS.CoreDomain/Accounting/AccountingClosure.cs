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
using OpenCBS.CoreDomain.Events;
using OpenCBS.CoreDomain.Events.Loan;
using OpenCBS.CoreDomain.Events.Saving;
using OpenCBS.CoreDomain.Events.Teller;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Accounting
{
    public class AccountingClosure
    {
        public string ClosureStatus { get; set; }
        public string ClosureStatusInfo { get; set; }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CountOfTransactions { get; set; }
        public User User { get; set; }
        public bool Deleted { get; set; }

        private static ContractAccountingRule GetParentTellerAccount(int? tellerId, ContractAccountingRule rule,
                                                                     List<Teller> tellers)
        {
            foreach (Teller teller in tellers)
            {
                if (tellerId == teller.Id)
                {
                    if (teller.Account.ParentAccountId == rule.DebitAccount.Id) rule.DebitAccount = teller.Account;
                    if (teller.Account.ParentAccountId == rule.CreditAccount.Id) rule.CreditAccount = teller.Account;
                }
            }

            return rule;
        }

        private static ContractAccountingRule GetParentPaymentAccount(int? paymentMethodId, ContractAccountingRule rule,
                                                                      List<PaymentMethod> paymentMethods)
        {
            foreach (PaymentMethod paymentMethod in paymentMethods)
            {
                if (paymentMethodId == paymentMethod.Id)
                {
                    if (paymentMethod.Account.ParentAccountId == rule.DebitAccount.Id)
                        rule.DebitAccount = paymentMethod.Account;
                    if (paymentMethod.Account.ParentAccountId == rule.CreditAccount.Id)
                        rule.CreditAccount = paymentMethod.Account;
                }
            }

            return rule;
        }

        public List<Booking> GetBookings(AccountingRuleCollection rules, 
                                         EventStock eventStock, 
                                         List<Teller> tellers,
                                         List<PaymentMethod> paymentMethods,
                                         List<ExchangeRate> rates,
                                         List<FiscalYear> fiscalYears)
        {
            if (eventStock == null)
                return new List<Booking>();

            rules.SortByOrder();
            eventStock.SortEventsById();
            bool isExported = false;
            var bookings = new List<Booking>();

            foreach (Event eventItem in eventStock)
            {
                ClosureStatus = "Closure";
                ClosureStatusInfo = " ->" + eventItem.Code + "-" + eventItem.Id;

                var attributes = new List<EventAttribute>();
                List<ContractAccountingRule> rulesToApply =
                    rules.GetContractAccountingRules().Where(item => item.EventType.EventCode == eventItem.Code).ToList();

                rules.SortByOrder();
                List<ContractAccountingRule> orders = rulesToApply.GroupBy(a => a.Order).Select(g => g.Last()).ToList();

                foreach (ContractAccountingRule orderRule in orders)
                {
                    foreach (ContractAccountingRule rule in rulesToApply.Where(r => r.Order == orderRule.Order).ToList())
                    {
                        List<EventAttribute> evtAttributes = (from eventAtt in attributes
                                                              where eventAtt.Name == rule.EventAttribute.Name
                                                              select eventAtt).ToList();

                        if (rule.EventType.EventCode == eventItem.Code
                            && evtAttributes.Count <= rulesToApply.Count(r => r.Order == orderRule.Order) - 1)
                        {
                            ContractAccountingRule tempRule = rule.Copy();

                            if (paymentMethods != null && eventItem.PaymentMethod != null)
                                tempRule = GetParentPaymentAccount(eventItem.PaymentMethod.Id, tempRule, paymentMethods);

                            // teller must be last
                            if (tellers != null && eventItem.TellerId != null)
                            {
                                //that copy is very important because the rule might be over written by payment method 
                                tempRule = rule.Copy();
                                tempRule = GetParentTellerAccount(eventItem.TellerId, tempRule, tellers);
                            }

                            Booking b = GetBooking(tempRule, eventItem);
                            if (b != null && b.Amount > 0)
                            {
                                //setting fiscal year
                                if (fiscalYears != null)
                                    b.FiscalYear =
                                        fiscalYears
                                            .First(
                                                f =>
                                                f.OpenDate <= b.Date.Date &&
                                                (f.CloseDate == null || f.CloseDate >= b.Date.Date)
                                            );
                                //setting xrate
                                ExchangeRate rate = null;
                                if (rates != null)
                                    rate = rates.FirstOrDefault(r => r.Date.Date == b.Date.Date);

                                b.ExchangeRate = b.Currency.IsPivot ? 1 : rate == null ? 0 : rate.Rate;

                                isExported = true;
                                attributes.Add(tempRule.EventAttribute);
                                bookings.Add(b);
                            }
                        }
                    }
                }
                
                if(eventItem is TellerCashInEvent || eventItem is TellerCashOutEvent)
                {
                    bookings.Add(GetTellerBooking((TellerEvent) eventItem, tellers, fiscalYears));
                }

                eventItem.IsFired = false;
                eventItem.IsFired = isExported;
            }
            return bookings;
        }

        private Booking GetTellerBooking(TellerEvent eventItem, List<Teller> tellers, List<FiscalYear> fiscalYears)
        {
            if (eventItem is TellerCashInEvent)
            {
                Teller teller = tellers.Find(i => i.Id == eventItem.TellerId);
                return new Booking
                    {
                         Amount = eventItem.Amount,
                         CreditAccount = teller.Account,
                         DebitAccount = teller.Vault.Account,
                         ContractId = eventItem.ContracId,
                         Date = eventItem.Date,
                         EventId = eventItem.Id,
                         RuleId = 0,
                         ExchangeRate = 1,
                         Currency = eventItem.Currency,
                         Branch = eventItem.Branch,
                         EventType = eventItem.Code,
                         Name = eventItem.Description + " [" + eventItem.Date + "]",
                         Type = OMovementType.Teller,
                         FiscalYear = fiscalYears.First(f => f.OpenDate <= eventItem.Date.Date && f.CloseDate == null || f.CloseDate > eventItem.Date.Date)
                    };
            }

            if (eventItem is TellerCashOutEvent)
            {
                Teller teller = tellers.Find(i => i.Id == eventItem.TellerId);
                return new Booking
                    {
                         Amount = eventItem.Amount,
                         CreditAccount = teller.Vault.Account,
                         DebitAccount = teller.Account,
                         ContractId = eventItem.ContracId,
                         Date = eventItem.Date,
                         EventId = eventItem.Id,
                         RuleId = 0,
                         ExchangeRate = 1,
                         Currency = eventItem.Currency,
                         Branch = eventItem.Branch,
                         EventType = eventItem.Code,
                         Name = eventItem.Description + " [" + eventItem.Date + "]",
                         Type = OMovementType.Teller,
                         FiscalYear = fiscalYears.First(f => f.OpenDate <= eventItem.Date.Date && f.CloseDate == null || f.CloseDate > eventItem.Date.Date)
                    };
            }
            return null;
        }

        private static Booking GetBooking(ContractAccountingRule rule, Event eventItem)
        {
            if (eventItem.Code == rule.EventType.EventCode)
            {
                if ((rule.LoanProduct == null
                     || (eventItem.LoanProduct != null && eventItem.LoanProduct.Id == rule.LoanProduct.Id))
                    && (rule.SavingProduct == null
                        || (eventItem.SavingProduct != null && eventItem.SavingProduct.Id == rule.SavingProduct.Id))
                    && (rule.Currency == null
                        || (eventItem.Currency != null && eventItem.Currency.Id == rule.Currency.Id))
                    && (rule.ClientType == OClientTypes.All
                        || eventItem.ClientType == rule.ClientType)
                    && (rule.EconomicActivity == null
                        ||
                        (eventItem.EconomicActivity != null && eventItem.EconomicActivity.Id == rule.EconomicActivity.Id)))
                {
                    var booking = new Booking
                                      {
                                          Amount = GetValue(rule, eventItem),
                                          CreditAccount = rule.CreditAccount,
                                          DebitAccount = rule.DebitAccount,
                                          ContractId = eventItem.ContracId,
                                          Date = eventItem.Date,
                                          EventId = eventItem.Id,
                                          RuleId = rule.Id,
                                          ExchangeRate = 1,
                                          Currency = eventItem.Currency,
                                          Branch = eventItem.Branch,
                                          EventType = eventItem.Code,
                                          Name = eventItem.Description + " [" + eventItem.Date + "]",
                                          Type = eventItem is SavingEvent
                                                     ? OMovementType.Saving
                                                     : eventItem is TellerEvent
                                                           ? OMovementType.Teller
                                                           : OMovementType.Loan
                                      };
                    return booking;
                }
            }
            return null;
        }

        private static OCurrency GetValue(LoanDisbursmentEvent eventItem, ContractAccountingRule rule)
        {
            OCurrency amount = 0;
            if (rule.EventAttribute.Name.ToLower() == "amount")
            {
                amount = eventItem.Amount;
            }

            if (rule.EventAttribute.Name.ToLower() == "fees")
            {
                amount = eventItem.Fee;
            }

            if (rule.EventAttribute.Name.ToLower() == "interest")
            {
                amount = eventItem.Interest;
            }
            return amount;
        }

        private static OCurrency GetValue(RepaymentEvent eventItem, ContractAccountingRule rule)
        {
            OCurrency amount = 0;

            if (rule.EventAttribute.Name.ToLower() == "principal")
            {
                amount = eventItem.Principal;
            }

            if (rule.EventAttribute.Name.ToLower() == "interests")
            {
                amount = eventItem.Interests;
            }

            if (rule.EventAttribute.Name.ToLower() == "penalties")
            {
                amount = eventItem.Penalties;
            }

            if (rule.EventAttribute.Name.ToLower() == "commissions")
            {
                amount = eventItem.Commissions;
            }
            return amount;
        }

        private static OCurrency GetValue(RepaymentOverWriteOffEvent eventItem, ContractAccountingRule rule)
        {
            OCurrency amount = 0;

            if (rule.EventAttribute.Name.ToLower() == "principal")
            {
                amount = eventItem.Principal;
            }

            if (rule.EventAttribute.Name.ToLower() == "interests")
            {
                amount = eventItem.Interests;
            }

            if (rule.EventAttribute.Name.ToLower() == "penalties")
            {
                amount = eventItem.Penalties;
            }

            if (rule.EventAttribute.Name == "commissions")
            {
                amount = eventItem.Commissions;
            }
            return amount;
        }

        private static OCurrency GetValue(LoanEntryFeeEvent eventItem, ContractAccountingRule rule)
        {
            OCurrency amount = 0;

            if (rule.EventAttribute.Name.ToLower() == "fee")
            {
                amount = eventItem.Fee;
            }
            return amount;
        }

        private static OCurrency GetValue(CreditInsuranceEvent eventItem, ContractAccountingRule rule)
        {
            OCurrency amount = 0;

            if (rule.EventAttribute.Name.ToLower() == "principal")
            {
                amount = eventItem.Principal;
            }

            if (rule.EventAttribute.Name.ToLower() == "commission")
            {
                amount = eventItem.Commission;
            }
            return amount;
        }

        private static OCurrency GetValue(TrancheEvent eventItem, ContractAccountingRule rule)
        {
            OCurrency amount = 0;

            if (rule.EventAttribute.Name.ToLower() == "amount")
            {
                amount = eventItem.Amount;
            }

            return amount;
        }

        private static OCurrency GetValue(RescheduleLoanEvent eventItem, ContractAccountingRule rule)
        {
            OCurrency amount = 0;

            if (rule.EventAttribute.Name.ToLower() == "amount")
            {
                amount = eventItem.Amount;
            }

            if (rule.EventAttribute.Name.ToLower() == "interest")
            {
                amount = eventItem.Interest;
            }
            return amount;
        }

        private static OCurrency GetValue(WriteOffEvent eventItem, ContractAccountingRule rule)
        {
            OCurrency amount = 0;

            switch (rule.EventAttribute.Name.ToLower())
            {
                case "olb":
                    amount = eventItem.OLB;
                    break;
                case "accrued_interests":
                    amount = eventItem.AccruedInterests;
                    break;
                case "accrued_penalties":
                    amount = eventItem.AccruedPenalties;
                    break;
                case "past_due_days":
                    amount = eventItem.PastDueDays;
                    break;
                case "overdue_principal":
                    amount = eventItem.OverduePrincipal;
                    break;
            }
            return amount;
        }

        private static OCurrency GetValue(OverdueEvent eventItem, ContractAccountingRule rule)
        {
            OCurrency amount = 0;

            if (rule.EventAttribute.Name.ToLower() == "olb")
            {
                amount = eventItem.OLB;
            }

            if (rule.EventAttribute.Name.ToLower() == "overdue_principal")
            {
                amount = eventItem.OverduePrincipal;
            }
            return amount;
        }

        private static OCurrency GetValue(TellerEvent eventItem, ContractAccountingRule rule)
        {
            OCurrency amount = 0;
            if (rule.EventAttribute.Name.ToLower() == "amount")
            {
                amount = eventItem.Amount;
            }
            return amount;
        }

        private static OCurrency GetValue(AccruedInterestEvent eventItem, ContractAccountingRule rule)
        {
            OCurrency amount = 0;

            if (rule.EventAttribute.Name.ToLower() == "accrued_interest")
            {
                amount = eventItem.AccruedInterest;
            }
            return amount;
        }

        private static OCurrency GetValue(ProvisionEvent eventItem, ContractAccountingRule rule)
        {
            OCurrency amount = 0;

            if (rule.EventAttribute.Name.ToLower() == "amount")
            {
                amount = eventItem.Amount;
            }
            return amount;
        }

        private static OCurrency GetValue(ContractAccountingRule rule, Event eventItem)
        {
            OCurrency amount = 0;

            if (eventItem is SavingEvent)
            {
                if (rule.EventAttribute.Name.ToLower() == "amount")
                {
                    amount = (eventItem as SavingEvent).Amount;
                }

                if (rule.EventAttribute.Name.ToLower() == "fees")
                {
                    amount = (eventItem as SavingEvent).Fee;
                }
            }

            if (eventItem.Code == "LODE")
            {
                var e = (LoanDisbursmentEvent) eventItem;
                amount = GetValue(e, rule);
            }

            if (eventItem.Code == "RGLE" ||
                eventItem.Code == "RBLE" ||
                eventItem.Code == "APR" ||
                eventItem.Code == "ATR" ||
                eventItem.Code == "APTR" ||
                eventItem.Code == "RRLE")
            {
                var e = (RepaymentEvent) eventItem;
                amount = GetValue(e, rule);
            }

            if (eventItem.Code == "ROWO")
            {
                var e = (RepaymentOverWriteOffEvent) eventItem;
                amount = GetValue(e, rule);
            }

            if (eventItem.Code.Substring(0, 3) == "LEE")
            {
                var e = (LoanEntryFeeEvent) eventItem;
                amount = GetValue(e, rule);
            }

            if (eventItem.Code == "LCIP" || eventItem.Code == "LCIE" || eventItem.Code == "LCIW")
            {
                var e = (CreditInsuranceEvent) eventItem;
                amount = GetValue(e, rule);
            }

            if (eventItem.Code == "TEET")
            {
                var e = (TrancheEvent) eventItem;
                amount = GetValue(e, rule);
            }

            if (eventItem.Code == "ROLE")
            {
                var e = (RescheduleLoanEvent) eventItem;
                amount = GetValue(e, rule);
            }

            if (eventItem.Code == "WROE")
            {
                var e = (WriteOffEvent) eventItem;
                amount = GetValue(e, rule);
            }

            if (eventItem.Code == "GLBL" ||
                eventItem.Code == "LLBL" ||
                eventItem.Code == "BLGL" ||
                eventItem.Code == "GLLL" ||
                eventItem.Code == "BLLL" ||
                eventItem.Code == "BLRL" ||
                eventItem.Code == "LLGL" ||
                eventItem.Code == "BLRL" ||
                eventItem.Code == "GLRL" ||
                eventItem.Code == "LLRL")
            {
                var e = (OverdueEvent) eventItem;
                amount = GetValue(e, rule);
            }

            if (eventItem.Code == "LIAE")
            {
                var e = (AccruedInterestEvent) eventItem;
                amount = GetValue(e, rule);
            }

            if (eventItem.Code == "LLPE")
            {
                var e = (ProvisionEvent) eventItem;
                amount = GetValue(e, rule);
            }

            if (eventItem is TellerEvent)
            {
                var e = (TellerEvent) eventItem;
                amount = GetValue(e, rule);
            }

            return amount;
        }
    }
}
