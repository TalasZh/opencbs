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
using OpenCBS.ExceptionsHandler;
using OpenCBS.Manager;
using OpenCBS.Shared;

namespace OpenCBS.Services
{
    public class TellerServices : Services
    {
        private static List<Teller> _tellers;
        private readonly TellerManager _manager;

        public TellerServices(User user)
        {
            _manager = new TellerManager(user);
        }

        private void LoadTellers()
        {
            if (_tellers != null) return;
            _tellers = _manager.SelectAll();
            _tellers = SelectVaultOfTellers(_tellers);
        }

        public List<Teller> FindAllTellers()
        {
            LoadTellers();
            return _tellers;
        }

        public Teller Find(string name)
        {
            LoadTellers();
            Debug.Assert(_tellers != null, "Teller list is null");
            Teller t = _tellers.Find(item => item.Name == name && !item.Deleted);

            return t;
        }

        public List<Teller> FindAllNonDeleted()
        {
            LoadTellers();
            return _tellers.FindAll(item => !item.Deleted);
        }

        public List<Teller> FindAllNonDeletedTellersOfUser(User user)
        {
            _tellers = _manager.SelectAllOfUser(user.Id).FindAll(item => !item.Deleted);
            return SelectVaultOfTellers(_tellers);
        }

        public List<Teller> SelectVaultOfTellers(List<Teller> tellers)
        {
            foreach (var teller in tellers)
            {
                if (teller.User.Id != 0)
                    teller.Vault = _manager.SelectVault(teller.Branch.Id);
            }
            return tellers;
        }

        public  List<Teller> FindAllNonDeletedTellersOfBranch(Branch branch)
        {
            var tellers = _manager.SelectAll().FindAll(item => item.Branch.Id == branch.Id && !item.Deleted && item.User.Id != 0);
            return SelectVaultOfTellers(tellers);
        }

        public Teller FindById(int id)
        {
            LoadTellers();
            return _tellers.Find(item => item.Id == id);
        }

        public Teller Add(Teller teller)
        {
            using (SqlConnection conn = _manager.GetConnection())
            {
                SqlTransaction t = conn.BeginTransaction();
                try
                {
                    LoadTellers();
                    _manager.Add(teller, t);
                    _tellers.Add(teller);
                    t.Commit();
                    return teller;
                }
                catch (Exception)
                {
                    t.Rollback();
                    throw;
                }
            }
        }

        public Teller Update(Teller teller)
        {
            using (SqlConnection conn =_manager.GetConnection())
            {
                SqlTransaction t = conn.BeginTransaction();
                try
                {
                    _manager.Update(teller, t);
                    t.Commit();
                    return teller;
                }
                catch (Exception)
                {
                    t.Rollback();
                    throw;
                }
            }
        }

        public Teller Delete(Teller teller)
        {
            _tellers.Remove(teller);
            _manager.Delete(teller.Id);
            return teller;
        }

        private static bool CheckMinMaxAndValueCorrectlyFilled(OCurrency min, OCurrency max, OCurrency fixedValue)
        {
            bool returned = false;

            if (min.HasValue && max.HasValue && !fixedValue.HasValue)
            {
                returned = min.Value > max.Value;
            }

            else if (!min.HasValue && !max.HasValue && fixedValue.HasValue)
                returned = true;

            return returned;
        }

        public OCurrency GetTellerBalance(Teller teller)
        {
            return  _manager.GetTellerBalance(teller);
        }

        public void ValidateTeller(Teller teller)
        {
            if (string.IsNullOrEmpty(teller.Name))
                throw new OpenCbsTellerException(OpenCbsTellerExceptionEnum.NameIsEmpty);
            
            if (teller.Account == null)
                throw new OpenCbsTellerException(OpenCbsTellerExceptionEnum.AccountIsEmpty);
            
            if (teller.User == null)
                throw new OpenCbsTellerException(OpenCbsTellerExceptionEnum.UserIsEmpty);

            if (teller.Branch == null)
                throw new OpenCbsTellerException(OpenCbsTellerExceptionEnum.BranchIsEmpty);

            if (teller.Currency == null)
                throw new OpenCbsTellerException(OpenCbsTellerExceptionEnum.CurrencyIsEmpty);

            if (CheckMinMaxAndValueCorrectlyFilled(teller.MinAmountDeposit, teller.MaxAmountDeposit, null))
                throw new OpenCbsTellerException(OpenCbsTellerExceptionEnum.MinMaxAmountIsInvalid);

            if (CheckMinMaxAndValueCorrectlyFilled(teller.MinAmountWithdrawal, teller.MaxAmountWithdrawal, null))
                throw new OpenCbsTellerException(OpenCbsTellerExceptionEnum.MinMaxAmountIsInvalid);

            if (CheckMinMaxAndValueCorrectlyFilled(teller.MinAmountTeller, teller.MaxAmountTeller, null))
                throw new OpenCbsTellerException(OpenCbsTellerExceptionEnum.MinMaxAmountIsInvalid);

            if (teller.Id == null && Find(teller.Name) != null)
                throw new OpenCbsTellerException(OpenCbsTellerExceptionEnum.NameIsExists);

            if (teller.User.Id == 0 && _manager.SelectVault(teller.Branch.Id) != null)
                throw new OpenCbsTellerException(OpenCbsTellerExceptionEnum.VaultExists);
        }

         public OCurrency GetLatestTellerOpenBalance(Teller teller)
         {
             return _manager.GetLatestTellerOpenBalance(teller);
         }
    }
}
