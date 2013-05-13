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
using System.Data.SqlClient;
using System.Diagnostics;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Events.Saving;
using OpenCBS.CoreDomain.Products;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Enums;
using OpenCBS.Manager.Products;

namespace OpenCBS.Manager.Events
{
	public class SavingEventManager : Manager
	{
        private readonly SavingProductManager _savingProductManager;

		public SavingEventManager(User pUser) : base(pUser)
		{
            _savingProductManager = new SavingProductManager(pUser);
		}

		public SavingEventManager(string db) : base(db)
		{
            _savingProductManager = new SavingProductManager(db);
		}

        public int Add(SavingEvent pSavingEvent, int pSavingContactId)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlTransaction t = conn.BeginTransaction())
                {
                    try
                    {
                        int id = Add(pSavingEvent, pSavingContactId, t);
                        t.Commit();
                        return id;
                    }
                    catch (Exception)
                    {
                        t.Rollback();
                        throw;
                    }
                }
            }
        }
		
		public int Add(SavingEvent pSavingEvent, int pSavingContractId, SqlTransaction sqlTransac)
		{
			const string q = @"INSERT INTO [SavingEvents](
                                       [user_id], 
                                       [contract_id], 
                                       [code], 
                                       [amount], 
                                       [description], 
				                       [deleted], 
                                       [creation_date], 
                                       [cancelable], 
                                       [is_fired], 
                                       [related_contract_code], 
                                       [fees],
                                       [savings_method], 
                                       [pending],
                                       [pending_event_id],
                                       [teller_id],
                                       [loan_event_id])
				                     VALUES(
                                       @user_id, 
                                       @contract_id, 
                                       @code, 
                                       @amount, 
                                       @description, 
                                       @deleted, 
                                       @creation_date, 
                                       @cancelable, 
                                       @is_fired, 
                                       @related_contract_code, 
                                       @fees,
                                       @savings_method,
                                       @pending,
                                       @pending_event_id,
                                       @teller_id,
                                       @loan_event_id)
				                     SELECT CONVERT(int, SCOPE_IDENTITY())";

            using (OpenCbsCommand c = new OpenCbsCommand(q, sqlTransac.Connection, sqlTransac))
            {
                SetInsertCommandForSavingEvent(c, pSavingEvent, pSavingContractId);
                pSavingEvent.Id = Convert.ToInt32(c.ExecuteScalar());
                return pSavingEvent.Id;
            }
		}

	    private static void SetInsertCommandForSavingEvent(OpenCbsCommand c, SavingEvent pSavingEvent, int pSavingContractId)
	    {
	        c.AddParam("@user_id", pSavingEvent.User.Id);
	        c.AddParam("@contract_id", pSavingContractId);
	        c.AddParam("@code", pSavingEvent.Code);
	        c.AddParam("@amount", pSavingEvent.Amount);
	        c.AddParam("@description", pSavingEvent.Description);
	        c.AddParam("@deleted", pSavingEvent.Deleted);
	        c.AddParam("@creation_date", pSavingEvent.Date);
	        c.AddParam("@cancelable", pSavingEvent.Cancelable);
	        c.AddParam("@is_fired", pSavingEvent.IsFired);
	        c.AddParam("@related_contract_code", pSavingEvent is SavingTransferEvent ?
	                                                                                                                           ((SavingTransferEvent)pSavingEvent).RelatedContractCode : null);
	        c.AddParam("@fees", pSavingEvent is ISavingsFees ? ((ISavingsFees)pSavingEvent).Fee : null);
	        if (pSavingEvent.SavingsMethod.HasValue)
	            c.AddParam("@savings_method", (int)pSavingEvent.SavingsMethod.Value);
	        else
	            c.AddParam("@savings_method", null);
	        c.AddParam("@pending", pSavingEvent.IsPending);
                
	        if (pSavingEvent.PendingEventId.HasValue)
	            c.AddParam("@pending_event_id", pSavingEvent.PendingEventId);
	        else
	            c.AddParam("@pending_event_id", null);

	        if (pSavingEvent.TellerId.HasValue && pSavingEvent.TellerId > 0)
	        {
	            if (pSavingEvent.TellerId != 0)
	            {
	                c.AddParam("@teller_id", pSavingEvent.TellerId);
	            }
	            else
	            {
	                c.AddParam("@teller_id", null);
	            }
	        }
	        else
	            c.AddParam("@teller_id", null);

	       c.AddParam("@loan_event_id", pSavingEvent.LoanEventId);
            
	    }

        public void FireEvent(int savingEventId)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlTransaction t = conn.BeginTransaction())
            {
                try
                {
                    FireEvent(savingEventId, t);
                    t.Commit();
                }
                catch (Exception)
                {
                    t.Rollback();
                    throw;
                }
            }

        }

	    public void FireEvent(int savingEventId, SqlTransaction sqlTransac)
        {
            const string q = @"UPDATE [SavingEvents] 
                                     SET [is_fired] = @is_fired 
                                     WHERE [id] = @id";

            using (OpenCbsCommand c = new OpenCbsCommand(q, sqlTransac.Connection, sqlTransac))
            {
                c.AddParam("@is_fired", true);
                c.AddParam("@id", savingEventId);

                c.ExecuteNonQuery();
            }
        }

        public void MakeEventExported(int pSavingEventId, SqlTransaction sqlTransac)
        {
            const string q = @"UPDATE [SavingEvents] 
                                     SET [is_exported] = @is_exported 
                                     WHERE [id] = @id";

            using (OpenCbsCommand c = new OpenCbsCommand(q, sqlTransac.Connection, sqlTransac))
            {
                c.AddParam("@is_exported", true);
                c.AddParam("@id", pSavingEventId);

                c.ExecuteNonQuery();
            }
        }

        public void ChangePendingEventStatus(int pSavingEventId, bool isPending)
        {
            const string q = @"UPDATE [SavingEvents] 
                                     SET [pending] = @pending 
                                     WHERE [id] = @id";

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@pending", isPending);
                c.AddParam("@id", pSavingEventId);

                c.ExecuteNonQuery();
            }
        }

        public void UpdateEventDescription(int pSavingEventId, string pDescription)
        {
            const string q = @"UPDATE [SavingEvents] 
                                     SET [description] = @description 
                                     WHERE [id] = @id";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@description", pDescription);
                c.AddParam("@id", pSavingEventId);

                c.ExecuteNonQuery();
            }
        }

        public List<SavingEvent> SelectEvents(int pSavingId, ISavingProduct pProduct)
		{
            const string q = @"SELECT  
                                        SavingEvents.id ,
                                        SavingEvents.user_id ,
                                        SavingEvents.code ,
                                        SavingEvents.amount ,
                                        SavingEvents.description + '  #' + sc.code + '-' + CONVERT(NVARCHAR(50), SavingEvents.id) AS description,
                                        SavingEvents.creation_date ,
                                        SavingEvents.contract_id,
                                        SavingEvents.cancelable ,
                                        SavingEvents.is_fired ,
                                        SavingEvents.deleted ,
                                        SavingEvents.related_contract_code ,
                                        SavingEvents.fees ,
                                        SavingEvents.savings_method ,
                                        SavingEvents.pending ,
                                        SavingEvents.pending_event_id ,
                                        SavingEvents.teller_id ,
                                        SavingEvents.loan_event_id ,
                                        SavingEvents.cancel_date,
                                        Users.id AS user_id ,
                                        Users.deleted ,
                                        Users.user_name ,
                                        Users.user_pass ,
                                        Users.role_code ,
                                        Users.first_name ,
                                        Users.last_name,
                                        0 AS branch_id,
                                        '' AS client_type_code,
                                        0 AS currency_id,
                                        0 AS product_id,
                                        sc.code AS contract_code,
                                        CAST(0 AS bit) AS is_pivot, 
                                        CAST(0 AS bit) AS is_swapped, 
                                        '' AS currency_code
                                FROM    SavingEvents
                                INNER JOIN Users ON SavingEvents.user_id = Users.id
                                INNER JOIN dbo.SavingContracts sc 
                                  ON SavingEvents.contract_id = sc.id
				                WHERE SavingEvents.contract_id = @id 
                                ORDER BY SavingEvents.id";
            using (SqlConnection conn = GetConnection())
            using(OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@id", pSavingId);

                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if(r == null || r.Empty) return new List<SavingEvent>();

                    List<SavingEvent> eventList = new List<SavingEvent>();
                    while (r.Read())
                    {
                        eventList.Add(ReadEvent(r, pProduct));
                    }
                    return eventList;
                }
            }
		}

        public List<SavingEvent> SelectEventsForClosure(DateTime beginDate, DateTime endDate, Branch branch)
        {
            const string q = @"SELECT 
                                       SavingEvents.id, 
                                       SavingEvents.user_id, 
                                       SavingEvents.code, 
                                       SavingEvents.contract_id,
                                       SavingEvents.amount, 
                                       SavingEvents.description + '  #' + sc.code + '-' + CONVERT(NVARCHAR(50), SavingEvents.id) AS description,
                                       SavingEvents.creation_date, 
                                       SavingEvents.cancelable, 
                                       SavingEvents.is_fired, 
                                       SavingEvents.deleted, 
                                       SavingEvents.related_contract_code, 
                                       SavingEvents.fees,
                                       SavingEvents.savings_method,
                                       SavingEvents.pending,
                                       SavingEvents.pending_event_id,
                                       SavingEvents.teller_id,
                                       SavingEvents.loan_event_id,
                                       SavingEvents.cancel_date,
				                       Users.id AS user_id, 
                                       Users.deleted, 
                                       Users.user_name, 
                                       Users.user_pass, 
                                       Users.role_code, 
                                       Users.first_name, 
                                       Users.last_name,
                                       t.branch_id,
                                       t.client_type_code,
                                       sp.currency_id,
                                       sp.id AS product_id,
                                       sc.code AS contract_code,
                                       curr.is_pivot, 
                                       curr.is_swapped, 
                                       curr.code AS currency_code
				                     FROM SavingEvents 
                                     INNER JOIN Users ON SavingEvents.user_id = Users.id
                                     INNER JOIN SavingContracts sc ON dbo.SavingEvents.contract_id = sc.id
                                     INNER JOIN SavingProducts sp ON sc.product_id = sp.id      
                                     INNER JOIN Tiers t ON t.id = sc.tiers_id
                                     INNER JOIN Currencies curr ON curr.id = sp.currency_id
				                     WHERE SavingEvents.deleted = 0 
                                       AND SavingEvents.is_exported = 0
                                       AND EXISTS(SELECT event_type 
                                                  FROM dbo.AccountingRules
                                                  GROUP BY event_type) 
                                       AND SavingEvents.creation_date BETWEEN @beginDate AND @endDate
                                       AND (t.branch_id = @branch_id OR @branch_id = 0)
                                     ORDER BY SavingEvents.id";

            List<SavingEvent> eventList = new List<SavingEvent>();
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@beginDate", beginDate);
                c.AddParam("@endDate", endDate);
                c.AddParam("@branch_id", branch.Id);

                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (r == null || r.Empty) return eventList;

                    while (r.Read())
                    {
                        eventList.Add(ReadEvent(r, null));
                    }
                }
            }

            foreach (SavingEvent savingEvent in eventList)
            {
                savingEvent.SavingProduct = _savingProductManager.SelectSavingProduct(savingEvent.SavingProduct.Id);
            }

            return eventList;
        }

        private static SavingEvent ReadEvent(OpenCbsReader r, ISavingProduct pProduct)
        {
            string code = r.GetString("code");
            SavingEvent e = GetSavingsEvent(code);
            SetSavingsEvent(r, e, pProduct);

            return e;
        }

	    private static void SetSavingsEvent(OpenCbsReader r, SavingEvent e, ISavingProduct pProduct)
	    {
	        e.Id = r.GetInt("id");
            e.ContracId = r.GetInt("contract_id");
	        e.Code = r.GetString("code");
	        e.Amount = r.GetMoney("amount");
	        e.Description = r.GetString("description");
	        e.Deleted = r.GetBool("deleted");
	        e.Date = r.GetDateTime("creation_date");
	        e.Cancelable = r.GetBool("cancelable");
	        e.IsFired = r.GetBool("is_fired");
	        e.CancelDate = r.GetNullDateTime("cancel_date");

	        if(pProduct != null)
	            e.ProductType = pProduct.GetType();

	        if (r.GetNullSmallInt("savings_method").HasValue)
	            e.SavingsMethod = (OSavingsMethods)r.GetNullSmallInt("savings_method").Value;
            
	        e.IsPending = r.GetBool("pending");
	        e.PendingEventId = r.GetNullInt("pending_event_id");
	        e.TellerId = r.GetNullInt("teller_id");
	        e.LoanEventId = r.GetNullInt("loan_event_id");
            
	        if (pProduct != null)
	        {
	            e.ProductType = pProduct.GetType();
	        }

	        if (e is SavingTransferEvent)
	        {
	            ((SavingTransferEvent)e).RelatedContractCode = r.GetString("related_contract_code");
	        }

	        if (e is ISavingsFees)
	        {
	            ((ISavingsFees) e).Fee = r.GetMoney("fees");
	        }

	        e.User = new User
	                     {
	                         Id = r.GetInt("user_id"),
	                         UserName = r.GetString("user_name"),
	                         Password = r.GetString("user_pass"),
	                         LastName = r.GetString("last_name"),
	                         FirstName = r.GetString("first_name")
	                     };
	        e.User.SetRole(r.GetString("role_code"));

            e.ClientType = OClientTypes.All;

            switch (r.GetString("client_type_code"))
            {
                case "I":
                    e.ClientType = OClientTypes.Person; break;
                case "C":
                    e.ClientType = OClientTypes.Corporate; break;
                case "G":
                    e.ClientType = OClientTypes.Group; break;
                case "V":
                    e.ClientType = OClientTypes.Village; break;
            }

            e.Branch = new Branch { Id = r.GetInt("branch_id") };
	        e.Currency = new Currency
	                         {
                                 Id = r.GetInt("currency_id"),
                                 Code = r.GetString("currency_code"),
                                 IsPivot = r.GetBool("is_pivot"),
                                 IsSwapped = r.GetBool("is_swapped")
	                         };
            e.SavingProduct = new SavingsBookProduct { Id = r.GetInt("product_id") };
	    }

	    private static SavingEvent GetSavingsEvent(string code)
	    {
	        SavingEvent e;
	        switch (code)
	        {
	            case OSavingEvents.Deposit:
	                e = new SavingDepositEvent();
	                break;
	            case OSavingEvents.Withdraw:
	                e = new SavingWithdrawEvent();
	                break;
	            case OSavingEvents.Accrual:
	                e = new SavingInterestsAccrualEvent();
	                break;
	            case OSavingEvents.Posting:
	                e = new SavingInterestsPostingEvent();
	                break;
	            case OSavingEvents.InitialDeposit:
	                e = new SavingInitialDepositEvent();
	                break;
	            case OSavingEvents.CreditTransfer:
	                e = new SavingCreditTransferEvent();
	                break;
	            case OSavingEvents.DebitTransfer:
	                e = new SavingDebitTransferEvent();
	                break;
	            case OSavingEvents.ManagementFee:
	                e = new SavingManagementFeeEvent();
	                break;
	            case OSavingEvents.SavingClosure:
	                e = new SavingClosureEvent();
	                break;
	            case OSavingEvents.Close:
	                e = new SavingCloseEvent();
	                break;
	            case OSavingEvents.OverdraftFees:
	                e = new SavingOverdraftFeeEvent();
	                break;
	            case OSavingEvents.Agio:
	                e = new SavingAgioEvent();
	                break;
	            case OSavingEvents.PendingDeposit:
	                e = new SavingPendingDepositEvent();
	                break;
	            case OSavingEvents.Reopen:
	                e = new SavingReopenEvent();
	                break;
	            case OSavingEvents.PendingDepositRefused:
	                e = new SavingPendingDepositRefusedEvent();
	                break;
	            case OSavingEvents.SpecialOperationCredit:
	                e = new SavingCreditOperationEvent();
	                break;
	            case OSavingEvents.SpecialOperationDebit:
	                e = new SavingDebitOperationEvent();
	                break;
	            case OSavingEvents.InterBranchCreditTransfer:
	                e = new SavingCreditInterBranchTransferEvent();
	                break;
	            case OSavingEvents.InterBranchDebitTransfer:
	                e = new SavingDebitInterBranchTransferEvent();
	                break;
	            case OSavingEvents.LoanDisbursement:
	                e = new SavingLoanDisbursementEvent();
	                break;
	            case OSavingEvents.SavingLoanRepayment:
	                e = new LoanRepaymentFromSavingEvent();
	                break;
                case OSavingEvents.BlockCompulsarySavings:
                    e = new SavingBlockCompulsarySavingsEvent();
                    break;
                case OSavingEvents.UnblockCompulsorySavings:
                    e = new SavingUnblockCompulsorySavingsEvent();
                    break;
	            default:
	                Debug.Fail("Failed to create saving event object");
	                throw new Exception();
	        }
	        return e;
	    }

	    public void DeleteEventInDatabase(SavingEvent pSavingEvent)
		{
			const string q = @"UPDATE [SavingEvents] SET 
                                                                    [deleted] = 1
                                                                    , is_exported = 0 
                                                                    ,[cancel_date] = @cancel_date
                                                                    WHERE id = @id";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@id", pSavingEvent.Id);
                c.AddParam("@cancel_date", pSavingEvent.CancelDate.Value);
                c.ExecuteNonQuery();
            }
		}

        public void DeleteSavingsEventByLoanEventId( int loanEventId, SqlTransaction sqlTransaction)
        {
            const string q =
                @"UPDATE [dbo].[SavingEvents] SET [deleted]=1, [is_exported]=0
                  WHERE loan_event_id = @loan_event_id";
            using (OpenCbsCommand c = new OpenCbsCommand(q, sqlTransaction.Connection, sqlTransaction))
            {
                c.AddParam("@loan_event_id", loanEventId);
                c.ExecuteNonQuery();
            }
        }

        public void DeleteLoanDisbursementSavingsEvent(int savingsId, int loanEventId)
        {
            const string q = @"UPDATE [SavingEvents] SET [deleted] = 1, is_exported = 0 
                                               WHERE contract_id = @savings_id AND loan_event_id = @loan_event_id ";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@savings_id", savingsId);
                c.AddParam("@loan_event_id", loanEventId);
                c.ExecuteNonQuery();
            }
        }
	}
}
