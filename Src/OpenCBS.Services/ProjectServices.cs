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
using OpenCBS.CoreDomain.Clients;
using OpenCBS.DatabaseConnection;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Manager;
using OpenCBS.CoreDomain.SearchResult;

namespace OpenCBS.Services
{
    public class ProjectServices : MarshalByRefObject
    {
        private readonly ProjectManager _ProjectManager;
        private readonly ClientServices _ClientServices;
        private readonly BranchService _branchService;
        private readonly User _user;

        public ProjectServices(User pUser)
        {
            _user = pUser;
            _ProjectManager = new ProjectManager(pUser, true);
            _ClientServices = new ClientServices(pUser);
            _branchService = new BranchService(pUser);
        }

        public ProjectServices(User pUser,string testDB)
        {
            _user = pUser;
            _ProjectManager = new ProjectManager(testDB, pUser);
            _ClientServices = new ClientServices(pUser, testDB);
            _branchService = new BranchService(pUser);
        }

        public ProjectServices(string testDB)
        {
            _ProjectManager = new ProjectManager(testDB);
        }

        public int SaveProject(Project pProject, IClient pClient)
        {
            if (pClient.Id == 0)
                throw new OpenCbsProjectSaveException(OpenCbsProjectSaveExceptionEnum.ClientIsEmpty);
            
            if (string.IsNullOrEmpty(pProject.Name))
                throw new OpenCbsProjectSaveException(OpenCbsProjectSaveExceptionEnum.NameIsEmpty);

            if (string.IsNullOrEmpty(pProject.Aim))
                throw new OpenCbsProjectSaveException(OpenCbsProjectSaveExceptionEnum.AimIsEmpty);

            if (pProject.BeginDate == DateTime.MinValue)
                throw new OpenCbsProjectSaveException(OpenCbsProjectSaveExceptionEnum.BeginDateEmpty);

            if (pProject.CorporateCA.HasValue && pProject.CorporateCA.Value == -1)
                throw new OpenCbsProjectSaveException(OpenCbsProjectSaveExceptionEnum.CAIsBad);

            if (pProject.CorporateFinancialPlanAmount.HasValue && pProject.CorporateFinancialPlanAmount.Value == -1)
                throw new OpenCbsProjectSaveException(OpenCbsProjectSaveExceptionEnum.FinancialPlanAmountIsBad);

            if (pProject.CorporateFinancialPlanTotalAmount.HasValue && pProject.CorporateFinancialPlanTotalAmount.Value == -1)
                throw new OpenCbsProjectSaveException(OpenCbsProjectSaveExceptionEnum.FinancialTotalPlanAmountIsBad);

            foreach (FollowUp up in pProject.FollowUps)
            {
                if(!up.CA.HasValue)
                    throw new OpenCbsProjectSaveException(OpenCbsProjectSaveExceptionEnum.CACannotBeNullInFollowUp);
            }

            using (SqlConnection conn = _ProjectManager.GetConnection())
            using (SqlTransaction t = conn.BeginTransaction())
            {
                try
                {
                    if (String.IsNullOrEmpty(pClient.Branch.Code))
                    {
                        pClient.Branch.Code = _branchService.FindBranchCodeByClientId(pClient.Id, t);
                    }
                    if (string.IsNullOrEmpty(pProject.Code))
                        pProject.Code = pProject.GenerateProjectCode(pClient.Branch.Code);
                    int id = 0;
                    if (pProject.Id == 0)
                    {
                        pProject.SetStatus();
                        pClient.SetStatus();

                        _ClientServices.UpdateClientStatus(pClient, t);
                        id = _ProjectManager.Add(pProject, pClient.Id, t);
                    }
                    else
                    {
                        _ProjectManager.UpdateProject(pProject, t);
                        id = pProject.Id;
                    }
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

        public List<string> FindAllProjectPurposes()
        {
            return _ProjectManager.SelectProjectPurposes();
        }

        public Project FindProjectById(int pProjectId)
        {
            return _ProjectManager.Select(pProjectId);
        }

        public List<ProjetSearchResult> FindProjectByCriteres(int currentlyPage, out int numbersTotalPage, out int numberOfRecords, string pQuery)
        {
            List<ProjetSearchResult> listProject;
            listProject = _ProjectManager.SelectProjectByCriteres(currentlyPage, pQuery);
            numberOfRecords = _ProjectManager.GetNumberProject(pQuery);
            numbersTotalPage = (numberOfRecords / 20) + 1; 
            return listProject;                      
        }

        private List<ProjetSearchResult> ExtractSearch(int current, List<ProjetSearchResult> tempList)
        {
            int start=0;
            int end=0;
            if (current>= 1)
            {
                start = (current - 1) * 20;
            }
            end = start + 20;
                
            int count = 0;
            List<ProjetSearchResult> listProject = tempList.FindAll(delegate(ProjetSearchResult p)
            {
                count++;
                return ((count >= start && count <= end) ? true : false);
            });
            return listProject;
        }

        public void UpdateProjectStatus(Project pProject)
        {
            using (SqlConnection conn = _ProjectManager.GetConnection())
            using (SqlTransaction transaction = conn.BeginTransaction())
            {
                UpdateProjectStatus(pProject, transaction);
                transaction.Commit();
            }
        }

        public void UpdateProjectStatus(Project pProject, SqlTransaction pTransaction)
        {
            _ProjectManager.UpdateProjectStatut(pProject, pTransaction);
        }

        public Project FindProjectByCode(string code)
        {
            return _ProjectManager.SelectProjectByCode(code);
        }
    }
}
