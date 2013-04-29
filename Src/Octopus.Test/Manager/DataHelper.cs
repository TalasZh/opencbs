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
using Octopus.CoreDomain;
using Octopus.CoreDomain.Accounting;
using Octopus.CoreDomain.Contracts.Loans.Installments;
using Octopus.CoreDomain.EconomicActivities;
using Octopus.CoreDomain.FundingLines;
using Octopus.DatabaseConnection;
using Octopus.Enums;
using Octopus.Manager;
using System.Data.SqlClient;
using Octopus.Manager.Accounting;
using Octopus.Services;

namespace Octopus.Test.Manager
{
	/// <summary>
	/// This class provides methods to prepare referential table for testing
	/// </summary>
	public class DataHelper
	{
        private readonly EconomicActivityManager _domainOfApplicationManagement;
        private readonly UserManager _userManager;

		public DataHelper()
        {
            _domainOfApplicationManagement = new EconomicActivityManager(DataUtil.TESTDB);
            _userManager = new UserManager(DataUtil.TESTDB);
		}

        //TODO:delete
	    private static SqlConnection SqlConnection
	    {
	        get
	        {
                return ConnectionManager.GetInstance(DataUtil.TESTDB).SqlConnection;
	        }
        }

		public InstallmentType AddBiWeeklyInstallmentType()
		{
			InstallmentType biWeekly = new InstallmentType();
			biWeekly.Name = "Bi-Weekly";
			biWeekly.NbOfDays = 14;
			biWeekly.NbOfMonths = 0;

            string sqlText = "INSERT INTO InstallmentTypes (name,nb_of_days,nb_of_months) VALUES ('" + biWeekly.Name + "'," + biWeekly.NbOfDays + "," + biWeekly.NbOfMonths + ") SELECT SCOPE_IDENTITY()";

            OctopusCommand insert = new OctopusCommand(sqlText, SqlConnection);

            biWeekly.Id = int.Parse(insert.ExecuteScalar().ToString());
			return biWeekly;
		}

		public void DeleteInstallmentTypes()
		{
            OctopusCommand delete = new OctopusCommand("DELETE InstallmentTypes WHERE id != 1", SqlConnection);
            delete.ExecuteNonQuery();
		}

		public EconomicActivity AddDomainOfApplicationAgriculture()
		{
			EconomicActivity agriculture = new EconomicActivity();
			agriculture.Name = "Agriculture";
			agriculture.Parent = null;
			agriculture.Deleted = false;
            agriculture.Id = _domainOfApplicationManagement.AddEconomicActivity(agriculture);

			return agriculture;
		}

	    public EconomicActivity AddEconomicActvityServices()
		{
			EconomicActivity services = new EconomicActivity();
			services.Name = "Services";
			services.Parent = null;
			services.Deleted = false;
            services.Id = _domainOfApplicationManagement.AddEconomicActivity(services);

			EconomicActivity shop = new EconomicActivity();
			shop.Name = "Services";
			shop.Parent = services;
			shop.Deleted = false;
            shop.Id = _domainOfApplicationManagement.AddEconomicActivity(shop);

			return services;
		}

		public EconomicActivity AddDomainOfApplicationFoodAndItsParentTrade()
		{
			EconomicActivity trade = new EconomicActivity();
			trade.Name = "Trade";
			trade.Parent = null;
			trade.Deleted = false;
            trade.Id = _domainOfApplicationManagement.AddEconomicActivity(trade);

			EconomicActivity food = new EconomicActivity();
			food.Name = "Food";
			food.Parent = trade;
			food.Deleted = false;
            food.Id = _domainOfApplicationManagement.AddEconomicActivity(food);

			return food;
		}

		public void DeleteEconomicActivities()
		{
            OctopusCommand delete = new OctopusCommand("DELETE EconomicActivities", SqlConnection);
            delete.ExecuteNonQuery();
		}

        public void DeleteAllBranches()
        {
            OctopusCommand delete = new OctopusCommand("DELETE FROM Branches", SqlConnection);
            delete.ExecuteNonQuery();

            OctopusCommand reset = new OctopusCommand("DBCC CHECKIDENT (Branches, RESEED, 0)", SqlConnection);
            reset.ExecuteNonQuery();
        }


		public void DeleteAllUser()
		{
		    DeleteAllBranches();
            DeleteAllUserRoles();
		    DeleteAllRoles();
            OctopusCommand delete = new OctopusCommand("DELETE Users WHERE id != 1", SqlConnection);
            delete.ExecuteNonQuery();
		}

        public void DeleteAllUserRoles()
        {
            OctopusCommand delete = new OctopusCommand("DELETE UserRole WHERE user_id != 1", SqlConnection);
            delete.ExecuteNonQuery();
        }
        public void DeleteAllRoles()
        {
            OctopusCommand delete = new OctopusCommand("DELETE ROLES WHERE id NOT IN (SELECT role_id FROM UserRole)", SqlConnection);
            delete.ExecuteNonQuery();
        }
		public int AddRussianUserWithMinimumAttributs()
		{
			User russianUser = new User();
			russianUser.UserName = "имя";
			russianUser.Password = "toto";
			russianUser.Role = User.Roles.LOF;
		    russianUser.Mail = "notSet";
			return _userManager.AddUser(russianUser);
		}
		public int AddUserWithMaximumAttributs()
		{
			User nicolas = new User();
			nicolas.UserName = "user";
			nicolas.Password = "password";
			nicolas.FirstName = "Nicolas";
			nicolas.LastName = "MANGIN";
            nicolas.Role = User.Roles.ADMIN;
            nicolas.Mail = "notSet";
			return _userManager.AddUser(nicolas);
		}
		public int AddUserWithIntermediaryAttributs()
		{
			User user = new User();
			user.UserName = "mariam";
			user.Password = "mariam";
			user.Role = User.Roles.ADMIN;
			user.FirstName = "marie";
		    user.Mail = "Not Set";
			return _userManager.AddUser(user);
		}

		public Province AddProvinceIntoDatabase()
		{
            OctopusCommand command = new OctopusCommand("INSERT INTO Provinces (name,deleted) VALUES ('Sugh',0) SELECT SCOPE_IDENTITY()", SqlConnection);
			Province province = new Province();
			province.Name = "Sugh";
            province.Id = int.Parse(command.ExecuteScalar().ToString());
			return province;
		}

		public void DeleteProvince()
		{
            OctopusCommand delete = new OctopusCommand("DELETE Provinces", SqlConnection);
            delete.ExecuteNonQuery();			
		}

		public District AddDistrictIntoDatabase()
		{
			Province province = AddProvinceIntoDatabase();
			District district = new District();
			district.Province = province;
			district.Name = "District";
            OctopusCommand command = new OctopusCommand("INSERT INTO Districts (name,province_id,deleted) VALUES ('" + district.Name + "',@provinceId,0) SELECT SCOPE_IDENTITY()", SqlConnection);
            command.AddParam("@provinceId", province.Id);
            district.Id = int.Parse(command.ExecuteScalar().ToString());
			return district;
		}

        public Branch AddBranchIntoDatabase()
        {
            Branch branch = new Branch();
            branch.Name = "Default";
            OctopusCommand command = new OctopusCommand("INSERT INTO Branches (name) VALUES ('@name') SELECT SCOPE_IDENTITY()", SqlConnection);
            command.AddParam("@name", branch.Name);
            command.ExecuteScalar().ToString();
            return branch;
        }
		
        public void DeletedProject()
        {
            OctopusCommand delete = new OctopusCommand("DELETE Projects", SqlConnection);
            delete.ExecuteNonQuery();
        }

	    public void DeleteDistrict()
        {
            OctopusCommand delete = new OctopusCommand("DELETE City", SqlConnection);
            delete.ExecuteNonQuery();
            delete = new OctopusCommand("DELETE Districts", SqlConnection);
            delete.ExecuteNonQuery();			
		}

		//"person" or "group" for type
		public int AddGenericTiersIntoDatabase(OClientTypes pClientType)
		{
			int districtId = AddDistrictIntoDatabase().Id;
            OctopusCommand command = new OctopusCommand("INSERT INTO Tiers (creation_date,client_type_code,active,loan_cycle,bad_client,district_id,city) VALUES ('1/1/2007','G',1,1,0,@districtId,'Tress') SELECT SCOPE_IDENTITY()", SqlConnection);
            command.AddParam("@districtId", districtId);
            int tiersId = int.Parse(command.ExecuteScalar().ToString());

            OctopusCommand insert = null;

            if (pClientType == OClientTypes.Group)
                insert = new OctopusCommand("INSERT INTO Groups (id,name) VALUES (" + tiersId + ",'SCG')", SqlConnection);
            else if (pClientType == OClientTypes.Person)
                insert = new OctopusCommand("INSERT INTO Persons (id,first_name,sex,identification_data,last_name,household_head) VALUES (" + tiersId + ",'Nicolas','M','123KH','BARON',1)", SqlConnection);

            if (insert != null) insert.ExecuteNonQuery();
			return tiersId;
		}

		public int AddGenericPackage()
		{
            OctopusCommand insert = new OctopusCommand("INSERT INTO Packages (deleted,name,code,client_type,installment_type,loan_type,charge_interest_within_grace_period,keep_expected_installment,anticipated_total_repayment_base, currency_id) VALUES (0,'Package','code','G',1,0,0,0,1,1) SELECT SCOPE_IDENTITY()", SqlConnection);
            return int.Parse(insert.ExecuteScalar().ToString());
		}

		public int AddGenericPackage(string packageName)
		{
            OctopusCommand insert = new OctopusCommand("INSERT INTO Packages (deleted,name,code,client_type,installment_type,loan_type,charge_interest_within_grace_period,keep_expected_installment,anticipated_total_repayment_base, currency_id) VALUES (0,'" + packageName + "','code','G',1,0,0,0,1,1) SELECT SCOPE_IDENTITY()", SqlConnection);
            return int.Parse(insert.ExecuteScalar().ToString());
		}

		public void DeletePackage()
		{
            OctopusCommand delete = new OctopusCommand("DELETE Packages", SqlConnection);
            delete.ExecuteNonQuery();			
		}

		public void DeleteSaving()
		{
            OctopusCommand deleteSVE = new OctopusCommand("DELETE SavingEvents", SqlConnection);
            deleteSVE.ExecuteNonQuery();
            OctopusCommand deleteSBC = new OctopusCommand("DELETE FROM SavingBookContracts", SqlConnection);
            deleteSBC.ExecuteNonQuery();
            OctopusCommand deleteSDC = new OctopusCommand("DELETE FROM SavingDepositContracts", SqlConnection);
            deleteSDC.ExecuteNonQuery();
            OctopusCommand deleteSVC = new OctopusCommand("DELETE SavingContracts", SqlConnection);
			deleteSVC.ExecuteNonQuery();
            OctopusCommand deleteSBP = new OctopusCommand("DELETE SavingBookProducts", SqlConnection);
            deleteSBP.ExecuteNonQuery();
            OctopusCommand deleteTDP = new OctopusCommand("DELETE TermDepositProducts", SqlConnection);
            deleteTDP.ExecuteNonQuery();
            OctopusCommand deleteSVP = new OctopusCommand("DELETE SavingProducts", SqlConnection);
			deleteSVP.ExecuteNonQuery();
		}

		public void AddGenericFundingLine()
		{
            //string fundingLineSql0 = "SET IDENTITY_INSERT FundingLines ON";
            //string fundingLineSql1 = "INSERT INTO [FundingLines]([id],[name], [deleted],[begin_date],[end_date],[amount],[purpose],[residual_amount]) VALUES(1,'AFD130',0,'','',10000,'JHT',10000)";
            //string fundingLineSql2 = "INSERT INTO [FundingLines]([id],[name], [deleted],[begin_date],[end_date],[amount],[purpose],[residual_amount]) VALUES(2,'GHJ',0,'','',10000,'JHT',10000)";
            //string fundingLineSql3 = "INSERT INTO [FundingLines]([id],[name], [deleted],[begin_date],[end_date],[amount],[purpose],[residual_amount]) VALUES(3,'FRR',0,'','',500,'JHT',10000)";
            //string fundingLineSql4 = "SET IDENTITY_INSERT FundingLines OFF";
            //InitScript(fundingLineSql0);
            //InitScript(fundingLineSql1);
            //InitScript(fundingLineSql2);
            //InitScript(fundingLineSql3);
            //InitScript(fundingLineSql4);
            //string CreditSql0 =
            //    "INSERT INTO [Credit]([id], [package_id], [amount], [interest_rate], [installment_type], [nb_of_installment],[anticipated_total_repayment_penalties], [disbursed], [loanofficer_id], [fundingLine_id], [grace_period], [written_off], [rescheduled], [collateral_amount], [bad_loan]) VALUES(1, 1, 1000, 2, 1, 6, 2, 1, 1, 1, 6, 0,0, 20000, 0)";
            //InitScript(CreditSql0);
		}
        public void AddGenericFundingLine3()
        {

            string fundingLineSql0 = "SET IDENTITY_INSERT FundingLines ON";
            string fundingLineSql1 = "INSERT INTO [FundingLines]([id],[name], [deleted],[begin_date],[end_date],[amount],[purpose], [currency_id]) VALUES(1,'AFD130',0,'','',10000,'JHT', 1)";
            string fundingLineSql2 = "INSERT INTO [FundingLines]([id],[name], [deleted],[begin_date],[end_date],[amount],[purpose], [currency_id]) VALUES(2,'GHJ',0,'','',10000,'JHT', 1)";
            string fundingLineSql3 = "INSERT INTO [FundingLines]([id],[name], [deleted],[begin_date],[end_date],[amount],[purpose], [currency_id]) VALUES(3,'FRR',0,'','',500,'JHT',1)";
            string fundingLineSql4 = "SET IDENTITY_INSERT FundingLines OFF";
            InitScript(fundingLineSql0);
            InitScript(fundingLineSql1);
            InitScript(fundingLineSql2);
            InitScript(fundingLineSql3);
            InitScript(fundingLineSql4);
            string CreditSql0 =
                "INSERT INTO [Credit]([id], [package_id], [amount], [interest_rate], [installment_type], [nb_of_installment],[anticipated_total_repayment_penalties], [disbursed], [loanofficer_id], [fundingLine_id], [grace_period], [written_off], [rescheduled], [collateral_amount], [bad_loan]) VALUES(1, 1, 1000, 2, 1, 6, 2, 1, 1, 1, 6, 0,0, 20000, 0)";
            InitScript(CreditSql0);

        }
        private void InitScript(string sql)
        {
            ConnectionManager connectionManager = ConnectionManager.GetInstance();
            OctopusCommand insert = new OctopusCommand(sql, connectionManager.SqlConnection);
            insert.ExecuteNonQuery();

        }
		public void AddGenericFundingLine(string code,bool deleted)
		{
            OctopusCommand insert = new OctopusCommand(@"INSERT INTO FundingLines (name,begin_date,end_date,amount,purpose,deleted) 
                VALUES (@code,'11/11/2006','11/11/2006',1000,'TEST',@deleted) SELECT SCOPE_IDENTITY()", SqlConnection);

            insert.AddParam("@code",code);
            insert.AddParam("@deleted", deleted);

            insert.ExecuteNonQuery();
		}
		public int AddGenericFundingLine2()
		{
            OctopusCommand insert = new OctopusCommand(@"INSERT INTO FundingLines (name,begin_date,end_date,amount,purpose,deleted, currency_id) 
                VALUES ('AFD1302', @startDate, @endDate,1000,'TEST',0, 1) SELECT SCOPE_IDENTITY()", SqlConnection);
         insert.AddParam("@startDate",  DateTime.Now);
         insert.AddParam("@endDate",  DateTime.Now);

         int id = Convert.ToInt32(insert.ExecuteScalar());
         return id;
      }
      public int AddGenericFundingLine4()
      {
          OctopusCommand delete = new OctopusCommand(@"DELETE FROM FundingLineEvents 
                           where fundingline_id = (SELECT id FROM FundingLines where name = 'AFD_TEST')", SqlConnection);
         delete.ExecuteNonQuery();

         OctopusCommand delete2 = new OctopusCommand(@"DELETE FROM FundingLines WHERE name = 'AFD_TEST'", SqlConnection);
         delete2.ExecuteNonQuery();

         OctopusCommand insert = new OctopusCommand(@"INSERT INTO FundingLines (name,begin_date,end_date,amount,purpose,deleted, currency_id) 
                VALUES ('AFD_TEST',@startDate, @endDate,1000,'TEST',0,1) SELECT SCOPE_IDENTITY()", SqlConnection);
         insert.AddParam("@startDate",  DateTime.Now);
         insert.AddParam("@endDate",  DateTime.Now);

         return   Convert.ToInt32(insert.ExecuteScalar());
      }
		public void DeleteFundingLine()
		{
            OctopusCommand delete22 = new OctopusCommand("DELETE FundingLineEvents", SqlConnection);
            delete22.ExecuteNonQuery();
            OctopusCommand delete = new OctopusCommand("DELETE FundingLines", SqlConnection);
            delete.ExecuteNonQuery();
		}

		public void DeleteCreditContract()
		{
            OctopusCommand ContractAssignHistory = new OctopusCommand("DELETE ContractAssignHistory", SqlConnection);
            ContractAssignHistory.ExecuteNonQuery();
            OctopusCommand deleteLinkGuarantor3 = new OctopusCommand("DELETE LoanDisbursmentEvents", SqlConnection);
            deleteLinkGuarantor3.ExecuteNonQuery();
            OctopusCommand deleteLinkGuarantor2 = new OctopusCommand("DELETE ContractEvents", SqlConnection);
            deleteLinkGuarantor2.ExecuteNonQuery();
            OctopusCommand deleteLinkGuarantor = new OctopusCommand("DELETE LinkGuarantorCredit", SqlConnection);
            deleteLinkGuarantor.ExecuteNonQuery();
            OctopusCommand deleteCredit = new OctopusCommand("DELETE Credit", SqlConnection);
            deleteCredit.ExecuteNonQuery();
			DeletePackage();
			DeleteFundingLine();
            OctopusCommand deleteContract = new OctopusCommand("DELETE Contracts", SqlConnection);
            deleteContract.ExecuteNonQuery();
			DeleteSaving();
			DeletedProject();
            DeleteTiers();
			DeleteDistrict();
			DeleteProvince();
		}

		public int AddGenericCreditContractIntoDatabase(bool disburse)
		{
			DeleteCreditContract();

			int tiersId = AddGenericTiersIntoDatabase(OClientTypes.Group);

		    int projectId = AddGenericProjectIntoDatabase(tiersId);

            OctopusCommand insertContract = new OctopusCommand("INSERT INTO [Contracts]([contract_code], [branch_code], [creation_date], [start_date], [close_date], [closed], [project_id]) VALUES " +
                "('KH/130','SU','10/10/2004','10/10/2005','10/10/2006',0," + projectId + ") SELECT SCOPE_IDENTITY()", SqlConnection);
            int contractId = int.Parse(insertContract.ExecuteScalar().ToString());
            
			return contractId;
		}

	    private int AddGenericProjectIntoDatabase(int tiers_id)
	    {
            OctopusCommand insert = new OctopusCommand("INSERT INTO [Projects] ([tiers_id],[status],[name],[code],[aim],[begin_date]) VALUES ( " + tiers_id + ",1,'NotSET','NotSET','NotSET','01/01/2007') SELECT SCOPE_IDENTITY()", SqlConnection);

            return int.Parse(insert.ExecuteScalar().ToString());
	    }

	    public void DeleteInstallments()
		{
            OctopusCommand deleteHistoric = new OctopusCommand("DELETE InstallmentHistory", SqlConnection);
            deleteHistoric.ExecuteNonQuery();
            OctopusCommand delete = new OctopusCommand("DELETE Installments", SqlConnection);
            delete.ExecuteNonQuery();
		}

		public void DeleteExotics()
		{
            OctopusCommand deleteExoInstallments = new OctopusCommand("DELETE ExoticInstallments", SqlConnection);
            deleteExoInstallments.ExecuteNonQuery();
            OctopusCommand deleteExotics = new OctopusCommand("DELETE Exotics", SqlConnection);
            deleteExotics.ExecuteNonQuery();
		}

		public void DeleteTiers()
		{
            OctopusCommand deletePersonGroupBelonging = new OctopusCommand("DELETE PersonGroupBelonging", SqlConnection);
            deletePersonGroupBelonging.ExecuteNonQuery();
            OctopusCommand deletePersons = new OctopusCommand("DELETE Persons", SqlConnection);
            deletePersons.ExecuteNonQuery();
            OctopusCommand deleteGroups = new OctopusCommand("DELETE Groups", SqlConnection);
            deleteGroups.ExecuteNonQuery();
            OctopusCommand deleteTiers = new OctopusCommand("DELETE Tiers", SqlConnection);
            deleteTiers.ExecuteNonQuery();
		}

        public void DeleteCycles()
        {
            OctopusCommand deletePersonGroupBelonging = new OctopusCommand("DELETE AmountCycles", SqlConnection);
            deletePersonGroupBelonging.ExecuteNonQuery();
            OctopusCommand deletePersons = new OctopusCommand("DELETE Cycles", SqlConnection);
            deletePersons.ExecuteNonQuery();
        }

		public void DeleteExchangeRate()
		{
            OctopusCommand delete = new OctopusCommand("DELETE ExchangeRates", SqlConnection);
            delete.ExecuteNonQuery();	
		}

		public int AddGenericEvent()
		{
			int contractId = AddGenericCreditContractIntoDatabase(true);
			int userId = AddUserWithIntermediaryAttributs();
            OctopusCommand insert = new OctopusCommand("INSERT INTO [ContractEvents]([event_type], [contract_id], " +
				"[event_date],[is_deleted], [user_id]) VALUES('PDLE', "+contractId+", '10/10/2006',0, "+userId+")",SqlConnection);

            return int.Parse(insert.ExecuteScalar().ToString());
		}

		public void AddGenericEvent(int userId)
		{
			int contractId = AddGenericCreditContractIntoDatabase(true);
		    int id = 1;
            OctopusCommand insert = new OctopusCommand("INSERT INTO [ContractEvents]([id], [event_type], [contract_id], " +
                "[event_date],[is_deleted], [user_id]) VALUES(" + id + ",'PDLE', " + contractId + ", '10/10/2006',1, " + userId + ") SELECT SCOPE_IDENTITY()", SqlConnection);

            insert.ExecuteScalar();
		}

        
      public FundingLine AddGenericFundingLineWithThreeEvents(DateTime startDate)
      {
         ChartOfAccounts _chartOfAccounts = ChartOfAccounts.GetInstance(new User { Id = 5 });
         AccountManager accountManager = new AccountManager(new User { Id = 5 });
         _chartOfAccounts.Accounts = accountManager.SelectAllAccounts();
        
         FundingLineServices service = new FundingLineServices(new User{Id = 5});
         int newFL_ID = AddGenericFundingLine4();
         FundingLine fundingLineInitial = service.SelectFundingLineById(newFL_ID, true);
         
         FundingLineEvent ev0 = new FundingLineEvent();
         ev0.Code = "6012";
         ev0.CreationDate = startDate.AddDays(-2);
         ev0.Amount = 1000;
         ev0.Movement = OBookingDirections.Credit;
         ev0.Type = OFundingLineEventTypes.Entry;
         ev0.FundingLine = fundingLineInitial;
         ev0.IsDeleted = false;
        
         fundingLineInitial.AddEvent( service.AddFundingLineEvent(ev0));

         FundingLineEvent ev1 = new FundingLineEvent();
         ev1.Code = "6013";
         ev1.CreationDate = startDate.AddDays(-1);
         ev1.Amount = 500;
         ev1.Movement = OBookingDirections.Debit;
         ev1.Type = OFundingLineEventTypes.Disbursment;
         ev1.FundingLine = fundingLineInitial;
         ev1.IsDeleted = false;
         fundingLineInitial.AddEvent(service.AddFundingLineEvent(ev1));

         FundingLineEvent ev2 = new FundingLineEvent();
         ev2.Code = "6014";
         ev2.CreationDate = startDate;
         ev2.Amount = 100;
         ev2.Movement = OBookingDirections.Credit;
         ev2.Type = OFundingLineEventTypes.Repay;
         ev2.FundingLine = fundingLineInitial;
         ev2.IsDeleted = false;
         
         fundingLineInitial.AddEvent(service.AddFundingLineEvent(ev2));

         return fundingLineInitial;
      }
	    public void DeleteEvents()
		{
            OctopusCommand deleteLIAE = new OctopusCommand("DELETE LoanInterestAccruingEvents", SqlConnection);
            deleteLIAE.ExecuteNonQuery();
            OctopusCommand deleteROLE = new OctopusCommand("DELETE ReschedulingOfALoanEvents", SqlConnection);
            deleteROLE.ExecuteNonQuery();
            OctopusCommand deleteWROE = new OctopusCommand("DELETE WriteOffEvents", SqlConnection);
            deleteWROE.ExecuteNonQuery();
            OctopusCommand deleteLODE = new OctopusCommand("DELETE LoanDisbursmentEvents", SqlConnection);
            deleteLODE.ExecuteNonQuery();
            OctopusCommand deleteRPE = new OctopusCommand("DELETE RepaymentEvents", SqlConnection);
            deleteRPE.ExecuteNonQuery();
            OctopusCommand delete = new OctopusCommand("DELETE ContractEvents", SqlConnection);
			delete.ExecuteNonQuery();
            OctopusCommand deleteSVE = new OctopusCommand("DELETE SavingEvents", SqlConnection);
			deleteSVE.ExecuteNonQuery();
            OctopusCommand deleteEVS = new OctopusCommand("DELETE Events", SqlConnection);
            deleteEVS.ExecuteNonQuery();
	        DeleteCreditContract();
		}

		public void DeleteProvisioningRules()
		{
            OctopusCommand delete = new OctopusCommand("DELETE ProvisioningRules", SqlConnection);
            delete.ExecuteNonQuery();
		}
        

        //static internal void PrepareDatabase()
        //{
        //    DatabaseManager dbmgr = new DatabaseManager(DataUtil.TESTDB);
        //    bool succesfull = false;
        //    int queriesCount = 0;
        //    string errorMessage = string.Empty, failedQuery = string.Empty;
        //    dbmgr.ExecuteCreateDatabaseScript(Path.Combine(PathUtil.GetSrcFolder(), @"Sql"), ref succesfull, ref queriesCount, ref errorMessage, ref failedQuery);
        //}
	}
}
