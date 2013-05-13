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
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;

using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.FundingLines;
using OpenCBS.DatabaseConnection;
using OpenCBS.Enums;
using OpenCBS.Manager;
using OpenCBS.ExceptionsHandler.Exceptions.FundingLineExceptions;
using OpenCBS.Manager.Clients;
using OpenCBS.Services.Events;
using OpenCBS.Services.Rules;
using OpenCBS.Shared;
using FundingLine = OpenCBS.CoreDomain.FundingLines.FundingLine;

namespace OpenCBS.Services
{
    public class FundingLineServices : MarshalByRefObject
    {
        private readonly FundingLineManager _fundingLineManager;
        private EventProcessorServices _ePS;
        private Hashtable fundingLinesWithAmounts;

        private User _user = new User();

        public FundingLineServices(User user)
        {
            _user = user;
            fundingLinesWithAmounts = new Hashtable();
            
            _fundingLineManager = new FundingLineManager(_user);
            _ePS = new EventProcessorServices(user);

        }

        public FundingLineServices(string testB)
        {
            _fundingLineManager = new FundingLineManager(testB);
        }

        public FundingLineServices(FundingLineManager fundingLineManager, ClientManager clientManagement)
        {
            _fundingLineManager = fundingLineManager;
        }

        public FundingLine SelectFundingLineById(int id, bool pAddOptionalEventInformations)
        {
            FundingLine fund = _fundingLineManager.SelectFundingLineById(id, pAddOptionalEventInformations);
            return fund;
        }

        public FundingLine SelectFundingLineByName(string name)
        {
            return _fundingLineManager.SelectFundingLineByName(name);
        }

        public FundingLine SelectFundingLineById(int id, SqlTransaction sqlTransac)
        {
            FundingLine fund = _fundingLineManager.SelectFundingLineById(id, sqlTransac);
            return fund;
        }

        public int SelectFundingLineEventId(FundingLineEvent lookupFundingLineEvent, SqlTransaction sqlTransac)
        {
            int id = _fundingLineManager.SelectFundingLineEventId(lookupFundingLineEvent, sqlTransac);
            return id;
        }

        public List<FundingLine> SelectFundingLines()
        {
            List<FundingLine> fundingLineList = _fundingLineManager.SelectFundingLines();
            return fundingLineList;
        }

        public FundingLine Create(FundingLine fund)
        {
            using (SqlConnection conn = _fundingLineManager.GetConnection())
            using (SqlTransaction sqlTransac = conn.BeginTransaction())
            try
            {
                if (string.IsNullOrEmpty(fund.Purpose))
                    throw new OpenCbsFundingLineException(OpenCbsFundingLineExceptionEnum.CodeIsEmpty);
                if (fund.StartDate > fund.EndDate)
                    throw new OpenCbsFundingLineException(OpenCbsFundingLineExceptionEnum.BeginDateGreaterEndDate);
                if (string.IsNullOrEmpty(fund.Name))
                    throw new OpenCbsFundingLineException(OpenCbsFundingLineExceptionEnum.NameIsEmpty);
                if (fund.Currency == null)
                    throw new OpenCbsFundingLineException(OpenCbsFundingLineExceptionEnum.CurrencyIsEmpty);
                if (fund.Currency.Id == 0)
                    throw new OpenCbsFundingLineException(OpenCbsFundingLineExceptionEnum.CurrencyIsEmpty);
                FundingLine tempfund = _fundingLineManager.SelectFundingLineByNameAndPurpose(fund, sqlTransac, true);

                if (tempfund.Id > 0)
                    if (tempfund.Deleted)
                    {
                        fund.Deleted = false;
                        UpdateFundingLine(fund, sqlTransac);
                        return fund;
                    }
                    else
                        throw new OpenCbsFundingLineException(OpenCbsFundingLineExceptionEnum.FundingLineNameExists);

                fund.Id = _fundingLineManager.AddFundingLine(fund, sqlTransac);

                sqlTransac.Commit();
                return fund;
            }
            catch (Exception ex)
            {
                sqlTransac.Rollback();
                throw ex;
            }
        }

        public void UpdateFundingLine(FundingLine fund)
        {
            using (SqlConnection conn = _fundingLineManager.GetConnection())
            using (SqlTransaction sqlTransac = conn.BeginTransaction())
            try
            {
                UpdateFundingLine(fund, sqlTransac);
                sqlTransac.Commit();
            }
            catch (Exception ex)
            {
                sqlTransac.Rollback();
                throw ex;
            }
        }
        public void UpdateFundingLine(FundingLine fund, SqlTransaction sqlTransac)
        {
            if (fund.Purpose == string.Empty)
                throw new OpenCbsFundingLineException(OpenCbsFundingLineExceptionEnum.NameIsEmpty);
            if (fund.StartDate > fund.EndDate)
                throw new OpenCbsFundingLineException(OpenCbsFundingLineExceptionEnum.BeginDateGreaterEndDate);
            if (fund.Name == string.Empty)
                throw new OpenCbsFundingLineException(OpenCbsFundingLineExceptionEnum.CodeIsEmpty);

            _fundingLineManager.UpdateFundingLine(fund, sqlTransac);
        }
        public FundingLineEvent AddFundingLineEvent(FundingLineEvent newFundingLineEvent)
        {
            SqlTransaction sqlTransac = ConnectionManager.GetInstance().GetSqlTransaction(_user.Md5);
            try
            {
                newFundingLineEvent = AddFundingLineEvent(newFundingLineEvent, sqlTransac);
                sqlTransac.Commit();
            }
            catch (Exception ex)
            {
                sqlTransac.Rollback();
                throw ex;
            }
            return newFundingLineEvent;
        }

        public FundingLineEvent AddFundingLineEvent(FundingLineEvent newFundingLineEvent, SqlTransaction sqlTransac)
        {
            if (newFundingLineEvent.FundingLine == null)
                throw new OpenCbsFundingLineException(OpenCbsFundingLineExceptionEnum.BadFundingLineID);

            ApplyRulesAmountEventFundingLine(newFundingLineEvent);
            newFundingLineEvent.Id = _fundingLineManager.AddFundingLineEvent(newFundingLineEvent, sqlTransac);
            newFundingLineEvent.User = _user;
            if (newFundingLineEvent.Type == OFundingLineEventTypes.Entry)
                _ePS.FireFundingLineEvent(newFundingLineEvent, newFundingLineEvent.FundingLine, sqlTransac);

            return newFundingLineEvent;
        }

        public void UpdateFundingLineEvent(FundingLineEvent newFundingLineEvent, SqlTransaction sqlTransac)
        {
            _fundingLineManager.UpdateFundingLineEvent(newFundingLineEvent, sqlTransac);
        }
        public void DeleteFundingLine(FundingLine fund)
        {
            _fundingLineManager.DeleteFundingLine(fund);
        }
        public void DeleteFundingLineEvent(FundingLineEvent deleteFundingLineEvent)
        {
            using (SqlConnection conn = _fundingLineManager.GetConnection())
            using (SqlTransaction sqlTransac = conn.BeginTransaction())
            try
            {
                DeleteFundingLineEvent(deleteFundingLineEvent, sqlTransac);
                sqlTransac.Commit();
            }
            catch (Exception ex)
            {
                sqlTransac.Rollback();
                throw ex;
            }
        }

        public void DeleteFundingLineEvent(FundingLineEvent deleteFundingLineEvent, SqlTransaction sqlTransac)
        {

            _fundingLineManager.DeleteFundingLineEvent(deleteFundingLineEvent, sqlTransac);
            deleteFundingLineEvent.IsDeleted = true;
            deleteFundingLineEvent.User = _user;

            if (deleteFundingLineEvent.Type == OFundingLineEventTypes.Entry)
                _ePS.FireFundingLineEvent(deleteFundingLineEvent, deleteFundingLineEvent.FundingLine, sqlTransac);


        }

        public void ApplyRulesAmountEventFundingLine(FundingLineEvent e)
        {
            Context rule = new Context(new ValidateAmountofEventFundingLine());
            rule.SetEventFundingLine(e);
            rule.ValidateRulesForFundingLine();
        }

        /* this method returns
         * estimated amount of funding line for several loans (ex. village members)
         * to check whether financial commitment will be enough
        */
        public decimal CheckIfAmountIsEnough(FundingLine fL, decimal amount)
        {
            decimal prevAmount = fundingLinesWithAmounts[fL.Id] == null ? 0 : (decimal)fundingLinesWithAmounts[fL.Id];
            if (fundingLinesWithAmounts.ContainsKey(fL.Id))
            {
                fundingLinesWithAmounts[fL.Id] = prevAmount + amount;
            }
            else
            {
                fundingLinesWithAmounts.Add(fL.Id, amount);
            }
            return fL.AnticipatedRemainingAmount.Value - (decimal)fundingLinesWithAmounts[fL.Id];
        }

        public void EmptyTemporaryFLAmountsStorage()
        {
            fundingLinesWithAmounts.Clear();
        }
    }
}

  

