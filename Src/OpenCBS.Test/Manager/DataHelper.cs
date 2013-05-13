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
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.EconomicActivities;
using OpenCBS.CoreDomain.FundingLines;
using OpenCBS.DatabaseConnection;
using OpenCBS.Enums;
using OpenCBS.Manager;
using System.Data.SqlClient;
using OpenCBS.Manager.Accounting;
using OpenCBS.Services;

namespace OpenCBS.Test.Manager
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

            OpenCbsCommand insert = new OpenCbsCommand(sqlText, SqlConnection);

            biWeekly.Id = int.Parse(insert.ExecuteScalar().ToString());
			return biWeekly;
		}

		public void DeleteInstallmentTypes()
		{
            OpenCbsCommand delete = new OpenCbsCommand("DELETE InstallmentTypes WHERE id != 1", SqlConnection);
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
            OpenCbsCommand delete = new OpenCbsCommand("DELETE EconomicActivities", SqlConnection);
            delete.ExecuteNonQuery();
		}

        public void DeleteAllBranches()
        {
            OpenCbsCommand delete = new OpenCbsCommand("DELETE FROM Branches", SqlConnection);
            delete.ExecuteNonQuery();

            OpenCbsCommand reset = new OpenCbsCommand("DBCC CHECKIDENT (Branches, RESEED, 0)", SqlConnection);
            reset.ExecuteNonQuery();
        }


		public void DeleteAllUser()
		{
		    DeleteAllBranches();
            DeleteAllUserRoles();
		    DeleteAllRoles();
            OpenCbsCommand delete = new OpenCbsCommand("DELETE Users WHERE id != 1", SqlConnection);
            delete.ExecuteNonQuery();
		}

        public void DeleteAllUserRoles()
        {
            OpenCbsCommand delete = new OpenCbsCommand("DELETE UserRole WHERE user_id != 1", SqlConnection);
            delete.ExecuteNonQuery();
        }
        public void DeleteAllRoles()
        {
            OpenCbsCommand delete = new OpenCbsCommand("DELETE ROLES WHERE id NOT IN (SELECT role_id FROM UserRole)", SqlConnection);
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
            OpenCbsCommand command = new OpenCbsCommand("INSERT INTO Provinces (name,deleted) VALUES ('Sugh',0) SELECT SCOPE_IDENTITY()", SqlConnection);
			Province province = new Province();
			province.Name = "Sugh";
            province.Id = int.Parse(command.ExecuteScalar().ToString());
			return province;
		}

		public void DeleteProvince()
		{
            OpenCbsCommand delete = new OpenCbsCommand("DELETE Provinces", SqlConnection);
            delete.ExecuteNonQuery();			
		}

		public District AddDistrictIntoDatabase()
		{
			Province province = AddProvinceIntoDatabase();
			District district = new District();
			district.Province = province;
			district.Name = "District";
            OpenCbsCommand command = new OpenCbsCommand("INSERT INTO Districts (name,province_id,deleted) VALUES ('" + district.Name + "',@provinceId,0) SELECT SCOPE_IDENTITY()", SqlConnection);
            command.AddParam("@provinceId", province.Id);
            district.Id = int.Parse(command.ExecuteScalar().ToString());
			return district;
		}

        public Branch AddBranchIntoDatabase()
        {
            Branch branch = new Branch();
            branch.Name = "Default";
            OpenCbsCommand command = new OpenCbsCommand("INSERT INTO Branches (name) VALUES ('@name') SELECT SCOPE_IDENTITY()", SqlConnection);
            command.AddParam("@name", branch.Name);
            command.ExecuteScalar().ToString();
            return branch;
        }
		
        public void DeletedProject()
        {
            OpenCbsCommand delete = new OpenCbsCommand("DELETE Projects", SqlConnection);
            delete.ExecuteNonQuery();
        }

	    public void DeleteDistrict()
        {
            OpenCbsCommand delete = new OpenCbsCommand("DELETE City", SqlConnection);
            delete.ExecuteNonQuery();
            delete = new OpenCbsCommand("DELETE Districts", SqlConnection);
            delete.ExecuteNonQuery();			
		}

		//"person" or "group" for type
		public int AddGenericTiersIntoDatabase(OClientTypes pClientType)
		{
			int districtId = AddDistrictIntoDatabase().Id;
            OpenCbsCommand command = new OpenCbsCommand("INSERT INTO Tiers (creation_date,client_type_code,active,loan_cycle,bad_client,district_id,city) VALUES ('1/1/2007','G',1,1,0,@districtId,'Tress') SELECT SCOPE_IDENTITY()", SqlConnection);
            command.AddParam("@districtId", districtId);
            int tiersId = int.Parse(command.ExecuteScalar().ToString());

            OpenCbsCommand insert = null;

            if (pClientType == OClientTypes.Group)
                insert = new OpenCbsCommand("INSERT INTO Groups (id,name) VALUES (" + tiersId + ",'SCG')", SqlConnection);
            else if (pClientType == OClientTypes.Person)
                insert = new OpenCbsCommand("INSERT INTO Persons (id,first_name,sex,identification_data,last_name,household_head) VALUES (" + tiersId + ",'Nicolas','M','123KH','BARON',1)", SqlConnection);

            if (insert != null) insert.ExecuteNonQuery();
			return tiersId;
		}

		public int AddGenericPackage()
		{
            OpenCbsCommand insert = new OpenCbsCommand("INSERT INTO Packages (deleted,name,code,client_type,installment_type,loan_type,charge_interest_within_grace_period,keep_expected_installment,anticipated_total_repayment_base, currency_id) VALUES (0,'Package','code','G',1,0,0,0,1,1) SELECT SCOPE_IDENTITY()", SqlConnection);
            return int.Parse(insert.ExecuteScalar().ToString());
		}

		public int AddGenericPackage(string packageName)
		{
            OpenCbsCommand insert = new OpenCbsCommand("INSERT INTO Packages (deleted,name,code,client_type,installment_type,loan_type,charge_interest_within_grace_period,keep_expected_installment,anticipated_total_repayment_base, currency_id) VALUES (0,'" + packageName + "','code','G',1,0,0,0,1,1) SELECT SCOPE_IDENTITY()", SqlConnection);
            return int.Parse(insert.ExecuteScalar().ToString());
		}

		public void DeletePackage()
		{
            OpenCbsCommand delete = new OpenCbsCommand("DELETE Packages", SqlConnection);
            delete.ExecuteNonQuery();			
		}

		public void DeleteSaving()
		{
            OpenCbsCommand deleteSVE = new OpenCbsCommand("DELETE SavingEvents", SqlConnection);
            deleteSVE.ExecuteNonQuery();
            OpenCbsCommand deleteSBC = new OpenCbsCommand("DELETE FROM SavingBookContracts", SqlConnection);
            deleteSBC.ExecuteNonQuery();
            OpenCbsCommand deleteSDC = new OpenCbsCommand("DELETE FROM SavingDepositContracts", SqlConnection);
            deleteSDC.ExecuteNonQuery();
            OpenCbsCommand deleteSVC = new OpenCbsCommand("DELETE SavingContracts", SqlConnection);
			deleteSVC.ExecuteNonQuery();
            OpenCbsCommand deleteSBP = new OpenCbsCommand("DELETE SavingBookProducts", SqlConnection);
            deleteSBP.ExecuteNonQuery();
            OpenCbsCommand deleteTDP = new OpenCbsCommand("DELETE TermDepositProducts", SqlConnection);
            deleteTDP.ExecuteNonQuery();
            OpenCbsCommand deleteSVP = new OpenCbsCommand("DELETE SavingProducts", SqlConnection);
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
            OpenCbsCommand insert = new OpenCbsCommand(sql, connectionManager.SqlConnection);
            insert.ExecuteNonQuery();

        }
		public void AddGenericFundingLine(string code,bool deleted)
		{
            OpenCbsCommand insert = new OpenCbsCommand(@"INSERT INTO FundingLines (name,begin_date,end_date,amount,purpose,deleted) 
                VALUES (@code,'11/11/2006','11/11/2006',1000,'TEST',@deleted) SELECT SCOPE_IDENTITY()", SqlConnection);

            insert.AddParam("@code",code);
            insert.AddParam("@deleted", deleted);

            insert.ExecuteNonQuery();
		}
		public int AddGenericFundingLine2()
		{
            OpenCbsCommand insert = new OpenCbsCommand(@"INSERT INTO FundingLines (name,begin_date,end_date,amount,purpose,deleted, currency_id) 
                VALUES ('AFD1302', @startDate, @endDate,1000,'TEST',0, 1) SELECT SCOPE_IDENTITY()", SqlConnection);
         insert.AddParam("@startDate",  DateTime.Now);
         insert.AddParam("@endDate",  DateTime.Now);

         int id = Convert.ToInt32(insert.ExecuteScalar());
         return id;
      }
      public int AddGenericFundingLine4()
      {
          OpenCbsCommand delete = new OpenCbsCommand(@"DELETE FROM FundingLineEvents 
                           where fundingline_id = (SELECT id FROM FundingLines where name = 'AFD_TEST')", SqlConnection);
         delete.ExecuteNonQuery();

         OpenCbsCommand delete2 = new OpenCbsCommand(@"DELETE FROM FundingLines WHERE name = 'AFD_TEST'", SqlConnection);
         delete2.ExecuteNonQuery();

         OpenCbsCommand insert = new OpenCbsCommand(@"INSERT INTO FundingLines (name,begin_date,end_date,amount,purpose,deleted, currency_id) 
                VALUES ('AFD_TEST',@startDate, @endDate,1000,'TEST',0,1) SELECT SCOPE_IDENTITY()", SqlConnection);
         insert.AddParam("@startDate",  DateTime.Now);
         insert.AddParam("@endDate",  DateTime.Now);

         return   Convert.ToInt32(insert.ExecuteScalar());
      }
		public void DeleteFundingLine()
		{
            OpenCbsCommand delete22 = new OpenCbsCommand("DELETE FundingLineEvents", SqlConnection);
            delete22.ExecuteNonQuery();
            OpenCbsCommand delete = new OpenCbsCommand("DELETE FundingLines", SqlConnection);
            delete.ExecuteNonQuery();
		}

		public void DeleteCreditContract()
		{
            OpenCbsCommand ContractAssignHistory = new OpenCbsCommand("DELETE ContractAssignHistory", SqlConnection);
            ContractAssignHistory.ExecuteNonQuery();
            OpenCbsCommand deleteLinkGuarantor3 = new OpenCbsCommand("DELETE LoanDisbursmentEvents", SqlConnection);
            deleteLinkGuarantor3.ExecuteNonQuery();
            OpenCbsCommand deleteLinkGuarantor2 = new OpenCbsCommand("DELETE ContractEvents", SqlConnection);
            deleteLinkGuarantor2.ExecuteNonQuery();
            OpenCbsCommand deleteLinkGuarantor = new OpenCbsCommand("DELETE LinkGuarantorCredit", SqlConnection);
            deleteLinkGuarantor.ExecuteNonQuery();
            OpenCbsCommand deleteCredit = new OpenCbsCommand("DELETE Credit", SqlConnection);
            deleteCredit.ExecuteNonQuery();
			DeletePackage();
			DeleteFundingLine();
            OpenCbsCommand deleteContract = new OpenCbsCommand("DELETE Contracts", SqlConnection);
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

            OpenCbsCommand insertContract = new OpenCbsCommand("INSERT INTO [Contracts]([contract_code], [branch_code], [creation_date], [start_date], [close_date], [closed], [project_id]) VALUES " +
                "('KH/130','SU','10/10/2004','10/10/2005','10/10/2006',0," + projectId + ") SELECT SCOPE_IDENTITY()", SqlConnection);
            int contractId = int.Parse(insertContract.ExecuteScalar().ToString());
            
			return contractId;
		}

	    private int AddGenericProjectIntoDatabase(int tiers_id)
	    {
            OpenCbsCommand insert = new OpenCbsCommand("INSERT INTO [Projects] ([tiers_id],[status],[name],[code],[aim],[begin_date]) VALUES ( " + tiers_id + ",1,'NotSET','NotSET','NotSET','01/01/2007') SELECT SCOPE_IDENTITY()", SqlConnection);

            return int.Parse(insert.ExecuteScalar().ToString());
	    }

	    public void DeleteInstallments()
		{
            OpenCbsCommand deleteHistoric = new OpenCbsCommand("DELETE InstallmentHistory", SqlConnection);
            deleteHistoric.ExecuteNonQuery();
            OpenCbsCommand delete = new OpenCbsCommand("DELETE Installments", SqlConnection);
            delete.ExecuteNonQuery();
		}

		public void DeleteExotics()
		{
            OpenCbsCommand deleteExoInstallments = new OpenCbsCommand("DELETE ExoticInstallments", SqlConnection);
            deleteExoInstallments.ExecuteNonQuery();
            OpenCbsCommand deleteExotics = new OpenCbsCommand("DELETE Exotics", SqlConnection);
            deleteExotics.ExecuteNonQuery();
		}

		public void DeleteTiers()
		{
            OpenCbsCommand deletePersonGroupBelonging = new OpenCbsCommand("DELETE PersonGroupBelonging", SqlConnection);
            deletePersonGroupBelonging.ExecuteNonQuery();
            OpenCbsCommand deletePersons = new OpenCbsCommand("DELETE Persons", SqlConnection);
            deletePersons.ExecuteNonQuery();
            OpenCbsCommand deleteGroups = new OpenCbsCommand("DELETE Groups", SqlConnection);
            deleteGroups.ExecuteNonQuery();
            OpenCbsCommand deleteTiers = new OpenCbsCommand("DELETE Tiers", SqlConnection);
            deleteTiers.ExecuteNonQuery();
		}

        public void DeleteCycles()
        {
            OpenCbsCommand deletePersonGroupBelonging = new OpenCbsCommand("DELETE AmountCycles", SqlConnection);
            deletePersonGroupBelonging.ExecuteNonQuery();
            OpenCbsCommand deletePersons = new OpenCbsCommand("DELETE Cycles", SqlConnection);
            deletePersons.ExecuteNonQuery();
        }

		public void DeleteExchangeRate()
		{
            OpenCbsCommand delete = new OpenCbsCommand("DELETE ExchangeRates", SqlConnection);
            delete.ExecuteNonQuery();	
		}

		public int AddGenericEvent()
		{
			int contractId = AddGenericCreditContractIntoDatabase(true);
			int userId = AddUserWithIntermediaryAttributs();
            OpenCbsCommand insert = new OpenCbsCommand("INSERT INTO [ContractEvents]([event_type], [contract_id], " +
				"[event_date],[is_deleted], [user_id]) VALUES('PDLE', "+contractId+", '10/10/2006',0, "+userId+")",SqlConnection);

            return int.Parse(insert.ExecuteScalar().ToString());
		}

		public void AddGenericEvent(int userId)
		{
			int contractId = AddGenericCreditContractIntoDatabase(true);
		    int id = 1;
            OpenCbsCommand insert = new OpenCbsCommand("INSERT INTO [ContractEvents]([id], [event_type], [contract_id], " +
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
            OpenCbsCommand deleteLIAE = new OpenCbsCommand("DELETE LoanInterestAccruingEvents", SqlConnection);
            deleteLIAE.ExecuteNonQuery();
            OpenCbsCommand deleteROLE = new OpenCbsCommand("DELETE ReschedulingOfALoanEvents", SqlConnection);
            deleteROLE.ExecuteNonQuery();
            OpenCbsCommand deleteWROE = new OpenCbsCommand("DELETE WriteOffEvents", SqlConnection);
            deleteWROE.ExecuteNonQuery();
            OpenCbsCommand deleteLODE = new OpenCbsCommand("DELETE LoanDisbursmentEvents", SqlConnection);
            deleteLODE.ExecuteNonQuery();
            OpenCbsCommand deleteRPE = new OpenCbsCommand("DELETE RepaymentEvents", SqlConnection);
            deleteRPE.ExecuteNonQuery();
            OpenCbsCommand delete = new OpenCbsCommand("DELETE ContractEvents", SqlConnection);
			delete.ExecuteNonQuery();
            OpenCbsCommand deleteSVE = new OpenCbsCommand("DELETE SavingEvents", SqlConnection);
			deleteSVE.ExecuteNonQuery();
            OpenCbsCommand deleteEVS = new OpenCbsCommand("DELETE Events", SqlConnection);
            deleteEVS.ExecuteNonQuery();
	        DeleteCreditContract();
		}

		public void DeleteProvisioningRules()
		{
            OpenCbsCommand delete = new OpenCbsCommand("DELETE ProvisioningRules", SqlConnection);
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
