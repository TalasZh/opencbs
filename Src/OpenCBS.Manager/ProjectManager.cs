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
using OpenCBS.CoreDomain.Contracts.Guarantees;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.Enums;
using OpenCBS.Manager.Contracts;
using OpenCBS.Shared;
using OpenCBS.CoreDomain.SearchResult;
using OpenCBS.Manager.QueryForObject;
using OpenCBS.Manager.Clients;

namespace OpenCBS.Manager
{
    public class ProjectManager : Manager
    {
        private readonly LoanManager _creditManager;
        private readonly LocationsManager _locations;
        private readonly ClientManager _clientManager;


        public ProjectManager(string testDB)
            : base(testDB)
        {
          
            _creditManager = new LoanManager(testDB);
            _locations = new LocationsManager(testDB);
        }

        public ProjectManager(string testDB, User pUser)
            : base(testDB)
        {
            _creditManager = new LoanManager(testDB, pUser);
            _locations = new LocationsManager(testDB);
        }

        public ProjectManager(User pUser, bool pInitializeAll): base(pUser)
        {
            if (pInitializeAll)
            {
                _creditManager = new LoanManager(pUser);
                _locations = new LocationsManager(pUser);
            }
            else
                _clientManager = new ClientManager(pUser, false, false);
        }

        public int Add(Project project, int tiersId)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlTransaction t = conn.BeginTransaction())
            {
                int id = Add(project, tiersId, t);
                t.Commit();
                return id;
            }
        }

        public int Add(Project pProject, int pTiersId, SqlTransaction pSqlTransac)
        {
            const string q = @"INSERT INTO Projects([tiers_id],[status],[name],[code],[aim],[begin_date],
                [abilities],[experience],[market],[concurrence],[purpose],[corporate_name],[corporate_juridicStatus]
                ,[corporate_FiscalStatus],[corporate_siret],[corporate_CA],[corporate_nbOfJobs],[corporate_financialPlanType]
                ,[corporateFinancialPlanAmount],[corporate_financialPlanTotalAmount],[address],[city],[zipCode],[district_id]
                ,[home_phone],[personalPhone],[Email],[hometype],[corporate_registre]) 
                VALUES(@tiersId,@status,@name,@code,@aim,@beginDate,@abilities,@experience,@market,@concurrence,@purpose,
                @corporateName,@corporateJuridicStatus,@corporateFiscalStatus,@corporateSiret,@corporateCA,@corporateNbOfJobs,
                @corporateFinancialPlanType, @corporateFinancialPlanAmount, @corporateFinancialPlanTotalAmount,
                @address,@city, @zipCode, @districtId, @homePhone, @personalPhone,@Email,@hometype,@corporateRegistre) SELECT SCOPE_IDENTITY()";

            using (OpenCbsCommand c = new OpenCbsCommand(q, pSqlTransac.Connection, pSqlTransac))
            {
                c.AddParam("@tiersId", pTiersId);
                c.AddParam("@status", (int) pProject.ProjectStatus);
                c.AddParam("@name", pProject.Name);
                c.AddParam("@code", pProject.Code);
                c.AddParam("@aim", pProject.Aim);
                c.AddParam("@beginDate", pProject.BeginDate);
                c.AddParam("@abilities", pProject.Abilities);
                c.AddParam("@experience", pProject.Experience);
                c.AddParam("@market", pProject.Market);
                c.AddParam("@concurrence",  pProject.Concurrence);
                c.AddParam("@purpose", pProject.Purpose);

                c.AddParam("@corporateName", pProject.CorporateName);
                c.AddParam("@corporateJuridicStatus", pProject.CorporateJuridicStatus);
                c.AddParam("@corporateFiscalStatus", pProject.CorporateFiscalStatus);
                c.AddParam("@corporateRegistre", pProject.CorporateRegistre);
                c.AddParam("@corporateSiret", pProject.CorporateSIRET);
                c.AddParam("@corporateCA", pProject.CorporateCA);
                c.AddParam("@corporateNbOfJobs", pProject.CorporateNbOfJobs);
                c.AddParam("@corporateFinancialPlanType", pProject.CorporateFinancialPlanType);
                c.AddParam("@corporateFinancialPlanAmount", pProject.CorporateFinancialPlanAmount);
                c.AddParam("@corporateFinancialPlanTotalAmount", pProject.CorporateFinancialPlanTotalAmount);
                c.AddParam("@address", pProject.Address);
                c.AddParam("@city", pProject.City);
                c.AddParam("@zipCode", pProject.ZipCode);
                if (pProject.District != null)
                    c.AddParam("@districtId", pProject.District.Id);
                else
                    c.AddParam("@districtId", null);
                c.AddParam("@homePhone", pProject.HomePhone);
                c.AddParam("@personalPhone", pProject.PersonalPhone);
                c.AddParam("@Email", pProject.Email);
                c.AddParam("@hometype", pProject.HomeType);

                pProject.Id = Convert.ToInt32(c.ExecuteScalar());
            }
            foreach (FollowUp followUp in pProject.FollowUps)
            {
                _AddFollowUp(followUp, pProject.Id, pSqlTransac);
            }

            foreach (Loan credit in pProject.Credits)
            {
                _creditManager.Add(credit, pProject.Id, pSqlTransac);
            }
            return pProject.Id;
        }

        private void _AddFollowUp(FollowUp pUp, int pId, SqlTransaction pTransac)
        {
            const string q = @"INSERT INTO [FollowUp] ([project_id],[year],[CA],[Jobs1],[Jobs2],[PersonalSituation],[activity]
                ,[comment]) VALUES(@projectId,@year,@CA,@jobs1,@jobs2,@personalSituation,@activity,@comment)
                SELECT SCOPE_IDENTITY()";

            using (OpenCbsCommand c = new OpenCbsCommand(q, pTransac.Connection, pTransac))
            {
                c.AddParam("@projectId", pId);
                c.AddParam("@year", pUp.Year);
                c.AddParam("@CA", pUp.CA);
                c.AddParam("@jobs1", pUp.Jobs1);
                c.AddParam("@jobs2", pUp.Jobs2);
                c.AddParam("@personalSituation", pUp.PersonalSituation);
                c.AddParam("@activity", pUp.Activity);
                c.AddParam("@comment", pUp.Comment);
                pUp.Id = Convert.ToInt32(c.ExecuteScalar());
            }
        }

        private Project SelectProject(int pId)
        {
            Project project = null;

            string sqlText = "SELECT * FROM Projects WHERE id = @id";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
            {
                select.AddParam("@id", pId);

                using (OpenCbsReader reader = select.ExecuteReader())
                {
                    if (reader != null)
                        if (!reader.Empty)
                        {
                            reader.Read();
                            project = GetProject(reader);
                        }
                        else
                            return project;
                }

                if (project.District != null)
                {
                    project.District = _locations.SelectDistrictById(project.District.Id);
                }

                if (project.Id != 0)
                {
                    project.FollowUps.AddRange(SelectFollowUps(project.Id));
                }

                if (project.Id != 0)
                {
                    project.AddCredits(_creditManager.SelectLoansByProject(project.Id));
                }
            }
            return project;
        }

        private Project GetProject(OpenCbsReader reader)
        {
            Project project = new Project();
            project.Id = reader.GetInt("id");
            project.ProjectStatus = (OProjectStatus)reader.GetSmallInt("status");
            project.Code = reader.GetString("code");
            project.Name = reader.GetString("name");
            project.Aim = reader.GetString("aim");
            project.BeginDate = reader.GetDateTime("begin_date");
            project.Abilities = reader.GetString("abilities");
            project.Experience = reader.GetString("experience");
            project.Market = reader.GetString("market");
            project.Concurrence = reader.GetString("concurrence");
            project.Purpose = reader.GetString("purpose");

            project.CorporateName = reader.GetString("corporate_name");
            project.CorporateJuridicStatus = reader.GetString("corporate_juridicStatus");
            project.CorporateFiscalStatus = reader.GetString("corporate_FiscalStatus");
            project.CorporateSIRET = reader.GetString("corporate_siret");
            project.CorporateRegistre = reader.GetString("corporate_registre");
            project.CorporateCA = reader.GetMoney("corporate_CA");

            project.CorporateNbOfJobs = reader.GetNullInt("corporate_nbOfJobs");
            project.CorporateFinancialPlanType = reader.GetString("corporate_financialPlanType");
            project.CorporateFinancialPlanAmount = reader.GetMoney("corporateFinancialPlanAmount");
            project.CorporateFinancialPlanTotalAmount = reader.GetMoney("corporate_financialPlanTotalAmount");
            project.Address = reader.GetString("address");
            project.City = reader.GetString("city");
            project.ZipCode = reader.GetString("zipCode");
            int? districtId = reader.GetNullInt("district_id");
            if (districtId.HasValue)
                project.District = new District { Id = districtId.Value };

            project.HomePhone = reader.GetString("home_phone");
            project.PersonalPhone = reader.GetString("personalPhone");
            project.Email = reader.GetString("Email");
            project.HomeType = reader.GetString("hometype");

            return project;
        }

        private IEnumerable<FollowUp> SelectFollowUps(int pProjectId)
        {
            List<FollowUp> list = new List<FollowUp>();
            const string sqlText = "SELECT * FROM FollowUp WHERE project_id = @pId";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
            {
                select.AddParam("@pId", pProjectId);
                using (OpenCbsReader reader = select.ExecuteReader())
                {
                    if (reader == null || reader.Empty) return new List<FollowUp>();
                    while (reader.Read())
                    {
                        FollowUp followUp = new FollowUp();
                        followUp.Id = reader.GetInt("id");
                        followUp.Year = reader.GetInt("year");
                        followUp.Jobs1 = reader.GetInt("Jobs1");
                        followUp.Jobs2 = reader.GetInt("Jobs2");
                        followUp.CA = reader.GetMoney("CA");
                        followUp.PersonalSituation = reader.GetString("PersonalSituation");
                        followUp.Activity = reader.GetString("activity");
                        followUp.Comment = reader.GetString("comment");
                        list.Add(followUp);
                    }
                    return list;
                }
            }
        }

        public Project Select(int pProjectId)
        {
            return SelectProject(pProjectId);
        }

        public void UpdateProject(Project pProject, SqlTransaction pSqlTransaction)
        {
            const string q = @"UPDATE Projects SET status = @status,name = @name,code = @code,aim = @aim,
                begin_date = @beginDate, abilities = @abilities, experience = @experience, market = @market, concurrence = @concurrence,
                purpose = @purpose ,[corporate_name] = @corporateName, [corporate_juridicStatus] = @corporateJuridicStatus, 
                [corporate_FiscalStatus] = @corporateFiscalStatus,[corporate_siret] = @corporateSiret,[corporate_CA] = @corporateCA,
                [corporate_nbOfJobs] = @corporateNbOfJobs,[corporate_financialPlanType] = @corporateFinancialPlanType,
                [corporateFinancialPlanAmount] = @corporateFinancialPlanAmount,[corporate_financialPlanTotalAmount] = @corporateFinancialPlanTotalAmount,
                [address] = @address,[city] = @city, [zipCode] = @zipCode, [district_id] = @districtId, [home_phone] = @homePhone, 
                [personalPhone] = @personalPhone, [Email] = @Email, [hometype] = @hometype,
                [corporate_registre] = @corporateRegistre WHERE id = @id";

            using (OpenCbsCommand c = new OpenCbsCommand(q, pSqlTransaction.Connection, pSqlTransaction))
            {
                c.AddParam("@status", (int) pProject.ProjectStatus);
                c.AddParam("@id", pProject.Id);
                c.AddParam("@name", pProject.Name);
                c.AddParam("@code", pProject.Code);
                c.AddParam("@aim", pProject.Aim);
                c.AddParam("@beginDate", pProject.BeginDate);
                c.AddParam("@abilities", pProject.Abilities);
                c.AddParam("@experience", pProject.Experience);
                c.AddParam("@market", pProject.Market);
                c.AddParam("@concurrence", pProject.Concurrence);
                c.AddParam("@purpose", pProject.Purpose);

                c.AddParam("@corporateName", pProject.CorporateName);
                c.AddParam("@corporateJuridicStatus", pProject.CorporateJuridicStatus);
                c.AddParam("@corporateFiscalStatus", pProject.CorporateFiscalStatus);
                c.AddParam("@corporateRegistre", pProject.CorporateRegistre);
                c.AddParam("@corporateSiret", pProject.CorporateSIRET);
                c.AddParam("@corporateCA", pProject.CorporateCA);
                c.AddParam("@corporateNbOfJobs", pProject.CorporateNbOfJobs);
                c.AddParam("@corporateFinancialPlanType", pProject.CorporateFinancialPlanType);
                c.AddParam("@corporateFinancialPlanAmount", pProject.CorporateFinancialPlanAmount);
                c.AddParam("@corporateFinancialPlanTotalAmount", pProject.CorporateFinancialPlanTotalAmount);
                c.AddParam("@address", pProject.Address);
                c.AddParam("@city", pProject.City);
                c.AddParam("@zipCode", pProject.ZipCode);
                if (pProject.District != null)
                    c.AddParam("@districtId", pProject.District.Id);
                else
                    c.AddParam("@districtId", null);
                c.AddParam("@homePhone", pProject.HomePhone);
                c.AddParam("@personalPhone", pProject.PersonalPhone);
                c.AddParam("@Email", pProject.Email);
                c.AddParam("@hometype", pProject.HomeType);

                c.ExecuteNonQuery();
            }
            foreach (FollowUp up in pProject.FollowUps)
            {
                if (up.Id == 0)
                    _AddFollowUp(up, pProject.Id, pSqlTransaction);
                else
                    _UpdateFollowUp(up, pSqlTransaction);
            }
        }

        private void _UpdateFollowUp(FollowUp pUp, SqlTransaction pTransaction)
        {
            const string q = @"UPDATE [FollowUp] SET [year] = @year,[CA] = @CA,[Jobs1] = @Jobs1 ,[Jobs2] = @Jobs2
                ,[PersonalSituation] = @PersonalSituation ,[activity] = @activity ,[comment] = @comment WHERE id = @id";

            using (OpenCbsCommand c = new OpenCbsCommand(q, pTransaction.Connection, pTransaction))
            {
                c.AddParam("@id", pUp.Id);
                c.AddParam("@year", pUp.Year);
                c.AddParam("@CA", pUp.CA);
                c.AddParam("@jobs1", pUp.Jobs1);
                c.AddParam("@jobs2", pUp.Jobs2);
                c.AddParam("@personalSituation", pUp.PersonalSituation);
                c.AddParam("@activity", pUp.Activity);
                c.AddParam("@comment", pUp.Comment);
                c.ExecuteNonQuery();
            }
        }

        public List<Project> SelectProjectsByClientId(int pClientId)
        {
            var list = new List<Project>();
            var listIds = new List<int>();

            const string query = "SELECT id FROM Projects WHERE tiers_id = @tiersId";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand select = new OpenCbsCommand(query, conn))
            {
                select.AddParam("@tiersId", pClientId);

                using (OpenCbsReader reader = select.ExecuteReader())
                {
                    if (reader != null)
                        if (!reader.Empty)
                        {
                            while (reader.Read())
                            {
                                listIds.Add(reader.GetInt("id"));
                            }
                        }
                }

                foreach (int id in listIds)
                {
                    list.Add(SelectProject(id));
                }
            }
            return list;
        }

        public Project SelectProjectByContractId(int pContractId)
        {
            string sqlText = @"SELECT Projects.*
                               FROM Projects
                               INNER JOIN Contracts ON Projects.id = Contracts.project_id
                               WHERE Contracts.id = @contractId";

            Project project = null;
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
            {
                select.AddParam("@contractId", pContractId);

                using (OpenCbsReader reader = select.ExecuteReader())
                {
                    if (!reader.Empty)
                    {
                        reader.Read();
                        project = GetProject(reader); 

                    }
                }
            }

            if (_clientManager != null)
                project.Client = _clientManager.SelectClientByProjectId(project.Id);

            return project;
        }

        public List<ProjetSearchResult> SelectProjectByCriteres(int pageNumber, string pQuery)
        {
            List<ProjetSearchResult> list = new List<ProjetSearchResult>();


            string SELECT_FROM_PROJET_ = @" SELECT DISTINCT pro.id,pro.code,pro.name as name_project,pro.aim,pers.first_name,
                        pers.last_name,tie.client_type_code,tie.id as tiers_id,corp.name as companyName
						FROM (Projects as pro
						INNER JOIN Tiers tie on pro.tiers_id=tie.id )
						LEFT JOIN Corporates corp on corp.id=tie.id
						LEFT JOIN Persons pers on pers.id=tie.id ) maTable";

            string CloseWhere = @" WHERE ( companyName LIKE @companyName OR code LIKE @code OR name_project LIKE @nameProject OR aim LIKE @aim OR last_name LIKE @lastName OR first_name LIKE @firtName )) maTable";

            QueryEntity q = new QueryEntity(pQuery, SELECT_FROM_PROJET_, CloseWhere);
            string pSqlText = q.ConstructSQLEntityByCriteresProxy(20, (pageNumber - 1) * 20);
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand select = new OpenCbsCommand(pSqlText, conn))
            {

                foreach (var item in q.DynamiqParameters())
                {
                    select.AddParam(item.Key, string.Format("%{0}%", item.Value));
                }
                using (OpenCbsReader reader = select.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProjetSearchResult resultat = new ProjetSearchResult();
                        resultat.Id = reader.GetInt("id");
                        resultat.Code = reader.GetString("code");
                        resultat.ProjectName = reader.GetString("name_project");
                        resultat.CompanyName = reader.GetString("companyName");
                        resultat.Aim = reader.GetString("aim");
                        resultat.TiersId = reader.GetInt("tiers_id");
                        resultat.Status = reader.GetChar("client_type_code");
                        resultat.LastName = reader.GetString("last_name");
                        resultat.FirstName = reader.GetString("first_name");
                        //resultat.ContractCode = reader.GetString("contract_code");
                        list.Add(resultat);
                    }
                }
            }
            return list;

        }

        public int GetNumberProject(string pQuery)
        {
            string SELECT_FROM_PROJET_ = @" SELECT DISTINCT  pro.id,pro.code,pro.name as name_project,pro.aim,pers.first_name,
                        pers.last_name,tie.client_type_code,tie.id as tiers_id,corp.name as companyName
						FROM (Projects as pro 
						INNER JOIN Tiers tie on pro.tiers_id=tie.id )
						LEFT JOIN Corporates corp on corp.id=tie.id
						LEFT JOIN Persons pers on pers.id=tie.id ) maTable";

            string CloseWhere = @" WHERE ( companyName LIKE @companyName OR code LIKE @code OR name_project LIKE @nameProject OR aim LIKE @aim OR last_name LIKE @lastName OR first_name LIKE @firtName )) maTable ";

            QueryEntity q = new QueryEntity(pQuery, SELECT_FROM_PROJET_, CloseWhere);
            string pSqlText = q.ConstructSQLEntityNumberProxy();
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand select = new OpenCbsCommand(pSqlText, conn))
            {

                foreach (var item in q.DynamiqParameters())
                {
                    select.AddParam(item.Key, string.Format("%{0}%", item.Value));
                }
                using (OpenCbsReader reader = select.ExecuteReader())
                {
                    reader.Read();
                    return reader.GetInt(0);
                }
            }
        }

        public List<string> SelectProjectPurposes()
        {
            List<string> list = new List<string>();
            const string sqlText = "SELECT * FROM ProjectPurposes";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
            {

                using (OpenCbsReader reader = select.ExecuteReader())
                {
                    if (!reader.Empty)
                    {
                        while (reader.Read())
                        {
                            list.Add(reader.GetString("name"));
                        }
                    }
                }
            }
            return list;
        }

        public void UpdateProjectStatut(Project pProject, SqlTransaction pTransaction)
        {
            const string q = "UPDATE [Projects] SET [status] = @status WHERE id = @id";
            
            using (OpenCbsCommand c = new OpenCbsCommand(q, pTransaction.Connection, pTransaction))
            {
                c.AddParam("@id", pProject.Id);
                c.AddParam("@status", (int) pProject.ProjectStatus);
                c.ExecuteNonQuery();   
            }
        }

        public Project SelectProjectByCode(string code)
        {
            const string q = @"SELECT id FROM dbo.Projects
            WHERE code = @code";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@code", code);
                object val = c.ExecuteScalar();
                return null == val ? null : SelectProject(Convert.ToInt32(val));
            }
        }
    }
}
