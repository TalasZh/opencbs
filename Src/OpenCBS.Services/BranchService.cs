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
using OpenCBS.CoreDomain;
using OpenCBS.Manager;
using OpenCBS.ExceptionsHandler;

namespace OpenCBS.Services
{
    public class BranchService : Services
    {
        private static List<Branch> _branches;
        private readonly BranchManager _manager;

        public BranchService(User user)
        {
            _manager = new BranchManager(user);
        }

        private void LoadBranches()
        {
            if (_branches != null) return;
            _branches = _manager.SelectAll();
        }

        public List<Branch> FindAllNonDeleted()
        {
            LoadBranches();
            return _branches.FindAll(item => !item.Deleted);
        }

        public List<Branch> FindAllNonDeletedWithVault()
        {
            return _manager.SelectAllWithVault();
        }

        public Branch FindById(int id)
        {
            LoadBranches();
            return _branches.Find(item => item.Id == id);
        }

        public void ValidateBranch(Branch branch)
        {
            if (string.IsNullOrEmpty(branch.Name))
            {
                throw new OpenCbsBranchNameIsEmptyException();
            }
            if (string.IsNullOrEmpty(branch.Code))
            {
                throw new OpenCbsBranchCodeIsEmptyException();
            }
            if (string.IsNullOrEmpty(branch.Address))
            {
                throw new OpenCbsBranchAddressIsEmptyException();
            }
            if (_manager.NameExists(branch.Id, branch.Name))
            {
                throw new OpenCbsBranchSameNameException();
            }
            if (_manager.CodeExists(branch.Id, branch.Code))
            {
                throw new OpenCbsBranchSameCodeException();
            }
        }

        public Branch Add(Branch branch)
        {
            using (SqlConnection conn = _manager.GetConnection())
            {
                SqlTransaction t = conn.BeginTransaction();
                try
                {
                    ValidateBranch(branch);       
                    LoadBranches();
                    _manager.Add(branch, t);
                    _branches.Add(branch);
                    if (t!= null) t.Commit();
                    return branch;
                }
                catch (Exception)
                {
                    if (t != null) t.Rollback();
                    throw;
                }
            }
        }

        public Branch Update(Branch branch, string previousName, string previousCode, string previousAddress, string previousDescription)
        {
            using (SqlConnection conn = _manager.GetConnection())
            {
                SqlTransaction t = conn.BeginTransaction();
                try
                {
                    ValidateBranch(branch);
                    _manager.Update(branch, t);
                    if (t != null) t.Commit();
                    return branch;
                }
                catch (Exception)
                {
                    if (t != null) t.Rollback();
                    branch.Name = previousName;
                    branch.Code = previousCode;
                    branch.Address = previousAddress;
                    branch.Description = previousDescription;
                    throw;
                }
            }
        }

        public Branch Delete(Branch branch)
        {
            _branches.Remove(branch);
            _manager.Delete(branch.Id);
            return branch;
        }

        public string FindBranchCodeByClientId(int clientId)
        {
            return _manager.GetBranchCodeByClientId(clientId);
        }

        public string FindBranchCodeByClientId(int clientId, SqlTransaction sqlTransaction)
        {
            return _manager.GetBranchCodeByClientId(clientId, sqlTransaction);
        }

        public Branch FindBranchByName(string name)
        {
            return _manager.SelectBranchByName(name);
        }
    }
}
