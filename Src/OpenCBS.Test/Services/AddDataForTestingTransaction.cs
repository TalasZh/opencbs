// LICENSE PLACEHOLDER

using System.Data.SqlClient;
using System.Data;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.EconomicActivities;
using OpenCBS.DatabaseConnection;
using OpenCBS.Manager.Clients;
using OpenCBS.CoreDomain;
using OpenCBS.Manager;

namespace OpenCBS.Test.Services
{
	/// <summary>
	/// Summary description for AddDataForTestingTransaction.
	/// </summary>
	public class AddDataForTestingTransaction
	{
		private readonly SqlConnection _connection;
        private readonly EconomicActivityManager _domainOfApplicationManagement;
        private readonly ClientManager _clientManagement;
	    private Branch _branch;

		public AddDataForTestingTransaction()
		{
            ConnectionManager connectionManager = ConnectionManager.GetInstance(DataUtil.TESTDB);
			_connection = connectionManager.SqlConnection;
            _domainOfApplicationManagement = new EconomicActivityManager(DataUtil.TESTDB);
            _clientManagement = new ClientManager(DataUtil.TESTDB);
		    _branch = new Branch {Id = 1, Name = "Default"};
		}

		public Person AddPerson()
		{
			Person person = new Person();
			person.Active = true;
			person.Activity = AddDomainOfApplicationAgriculture();
		    person.LoanCycle = 1;
			person.City = "Dushambe";
			person.District = AddDistrictIntoDatabase();
			person.FirstName = "Nicolas";
			person.LastName = "BARON";
			person.Sex = 'M';
			person.IdentificationData = "123ARK3VC";
			person.HouseHoldHead = true;
		    person.Branch = _branch;
			person.Id = _clientManagement.AddPerson(person);
			return person;
		}

		public Person AddPersonTer()
		{
			Person person = new Person();
			person.Active = true;
			person.Activity = AddDomainOfApplicationTrade();
			person.City = "Dushambe";
            person.LoanCycle = 1;
			person.District = AddDistrictIntoDatabase();
			person.FirstName = "Nicolas";
			person.LastName = "MANGIN";
			person.Sex = 'F';
			person.IdentificationData = "123ARK3VC";
			person.HouseHoldHead = true;
		    person.Branch = _branch;
			person.Id = _clientManagement.AddPerson(person);
			return person;
		}

		public Person AddPersonQuater()
		{
			Person person = new Person();
			person.Active = true;
			person.Activity = AddDomainOfApplicationAgri();
			person.City = "Dushambe";
			person.District = AddDistrictIntoDatabase();
			person.FirstName = "Flo";
            person.LoanCycle = 1;
			person.LastName = "MANGIN";
			person.Sex = 'F';
			person.IdentificationData = "123ARK3VC";
			person.HouseHoldHead = true;
		    person.Branch = _branch;
			person.Id = _clientManagement.AddPerson(person);
			return person;
		}

		public int AddGenericTiersIntoDatabase(string type)
		{
			int districtId = AddDistrictIntoDatabase().Id;
            OpenCbsCommand command = new OpenCbsCommand("INSERT INTO Tiers (creation_date,client_type_code,active,bad_client,district_id,city,loan_cycle) VALUES ('1/1/2007','G',1,0,@districtId,'Tress',1) SELECT SCOPE_IDENTITY()", _connection);
            command.AddParam("@districtId", districtId);
			int tiersId = int.Parse(command.ExecuteScalar().ToString());

            OpenCbsCommand insert = null;
			
			if (type.Equals("group"))
                insert = new OpenCbsCommand("INSERT INTO Groups (id,name) VALUES (" + tiersId + ",'SCG')", _connection);
			else if (type.Equals("person"))
                insert = new OpenCbsCommand("INSERT INTO Persons (id,first_name,sex,identification_data,last_name,household_head) VALUES (" + tiersId + ",'Nicolas','M','123KH','BARON',1)", _connection);
			
            if (insert != null) insert.ExecuteNonQuery();

			return tiersId;
		}
		public void DeleteFundingLine()
		{
            //SqlCommand deleteFundingLines = new SqlCommand("DELETE FundingLines",_connection);
            //deleteFundingLines.ExecuteNonQuery();
		}
		public void DeletePackage()
		{
            OpenCbsCommand delete = new OpenCbsCommand("DELETE Packages", _connection);
			delete.ExecuteNonQuery();
		}
		public void DeleteCreditContract()
		{

            OpenCbsCommand deleteLinkSavings = new OpenCbsCommand("DELETE FROM [LoansLinkSavingsBook]", _connection);
		    deleteLinkSavings.ExecuteNonQuery();
            OpenCbsCommand deleteLinkGuarantor3 = new OpenCbsCommand("DELETE LoanDisbursmentEvents", _connection);
            deleteLinkGuarantor3.ExecuteNonQuery();
            OpenCbsCommand deleteInstallmentHistory = new OpenCbsCommand("DELETE InstallmentHistory", _connection);
            deleteInstallmentHistory.ExecuteNonQuery();
            OpenCbsCommand deleteLinkGuarantor2 = new OpenCbsCommand("DELETE ContractEvents", _connection);
            deleteLinkGuarantor2.ExecuteNonQuery();
            OpenCbsCommand deleteLinkGuarantor = new OpenCbsCommand("DELETE LinkGuarantorCredit", _connection);
			deleteLinkGuarantor.ExecuteNonQuery();
            OpenCbsCommand deleteCredit = new OpenCbsCommand("DELETE Credit", _connection);
			deleteCredit.ExecuteNonQuery();
			DeletePackage();
            DeleteFundingLine();
            OpenCbsCommand deleteContract = new OpenCbsCommand("DELETE Contracts", _connection);
			deleteContract.ExecuteNonQuery();
			DeleteTiers();
            DeleteCity();
			DeleteDistrict();
			DeleteProvince();
		}
        public void DeleteCorporatePerson()
        {
            OpenCbsCommand deleteCorporatePerson = new OpenCbsCommand("DELETE CorporatePersonBelonging", _connection);
            deleteCorporatePerson.ExecuteNonQuery();

        }
        public void DeleteCorporate()
        {
            OpenCbsCommand deleteCorporatePerson = new OpenCbsCommand("DELETE Corporates", _connection);
            deleteCorporatePerson.ExecuteNonQuery();

        }

		public int AddGenericPackage()
		{
            OpenCbsCommand insert = new OpenCbsCommand("INSERT INTO Packages (currency_id, deleted,name,client_type,installment_type,loan_type,charge_interest_within_grace_period,keep_expected_installment,anticipated_total_repayment_base) VALUES (" + AddGenericCurrency() + ", 0,'Package','G',1,1,0,0,1) SELECT SCOPE_IDENTITY()", _connection);
			return int.Parse(insert.ExecuteScalar().ToString());
		}
        public int AddGenericCurrency()
        {
            OpenCbsCommand insert = new OpenCbsCommand("INSERT INTO Currencies (name, code, is_pivot, is_swapped) VALUES ('USD', 'USD', 1, 0) SELECT SCOPE_IDENTITY()", _connection);
            return int.Parse(insert.ExecuteScalar().ToString());
        }

		public int AddGenericFundingLine()
		{
            OpenCbsCommand insert = new OpenCbsCommand("INSERT INTO FundingLines (name,amount, purpose, deleted, currency_id, begin_date, end_date) VALUES ('AFD130',0, 'Not Set', 0, " + AddGenericCurrency() + ", '20080101', '20120101') SELECT SCOPE_IDENTITY()", _connection);
            return int.Parse(insert.ExecuteScalar().ToString());
		}

		public int AddGenericCreditContractIntoDatabase()
		{
			DeleteCreditContract();

			int tiersId = AddGenericTiersIntoDatabase("group");

            OpenCbsCommand insertProject = new OpenCbsCommand(@"INSERT INTO Projects([tiers_id], [status], [name], [code], [aim], [begin_date]) VALUES 
    (" + tiersId + " , 2,'NOT SET','NOT SET','NOT SET','10/10/2005') SELECT SCOPE_IDENTITY()", _connection);
            int projectId = int.Parse(insertProject.ExecuteScalar().ToString());


            OpenCbsCommand insertContract = new OpenCbsCommand("INSERT INTO [Contracts]([contract_code], [branch_code], [creation_date], [start_date], [close_date], [closed], [project_id], [activity_id]) VALUES " +
                "('KH/130','SU','10/10/2004','10/10/2005','10/10/2006',0," + projectId + ", 1) SELECT SCOPE_IDENTITY()", _connection);
			int contractId = int.Parse(insertContract.ExecuteScalar().ToString());

			int packageId = AddGenericPackage();
			int fundingline_id = AddGenericFundingLine();
		    string sqlText =
		        string.Format(
                                @"INSERT INTO [Credit]
                                 (   [id]
                                    ,[package_id]
                                    ,[amount]
                                    ,[interest_rate]
                                    ,[installment_type]
                                    ,[nb_of_installment]
                                    ,[non_repayment_penalties_based_on_initial_amount]
                                    ,[non_repayment_penalties_based_on_olb]
                                    ,[non_repayment_penalties_based_on_overdue_interest]
                                    ,[non_repayment_penalties_based_on_overdue_principal]
                                    ,[anticipated_total_repayment_penalties]
                                    ,[disbursed]
                                    ,[loanofficer_id]
                                    ,[fundingLine_id]
                                    ,[grace_period]
                                    ,[written_off]
                                    ,[rescheduled]
                                    ,[bad_loan])
                                VALUES(@id, @packageId, 1000, 2, 1, 2, 2 ,2 ,2 ,3 ,2 ,0 ,1 ,{0} ,6 ,0 ,0 ,0)
                                    ", fundingline_id);

            OpenCbsCommand insertCredit = new OpenCbsCommand(sqlText, _connection);

            insertCredit.AddParam("@id", contractId);
            insertCredit.AddParam("@packageId", packageId);

			insertCredit.ExecuteNonQuery();

            OpenCbsCommand insertInstallment = new OpenCbsCommand(@"INSERT INTO [Installments]([expected_date],[interest_repayment],[capital_repayment],[contract_id],
                                [number],[paid_interest],[paid_capital],[fees_unpaid])
                                    VALUES (01/01/2007,100,200,@contractId,1,0,0,0)
                                                            INSERT INTO [Installments]([expected_date],[interest_repayment],[capital_repayment],[contract_id],
                                [number],[paid_interest],[paid_capital],[fees_unpaid])
                                    VALUES (01/02/2007,100,200,@contractId,2,0,0,0)", _connection);
            insertInstallment.AddParam("@contractId",  contractId);
            insertInstallment.ExecuteNonQuery();
			return contractId;
		}

		public Person AddPersonBis()
		{
			Person person = new Person();
			person.Active = true;
			person.Activity = AddEconomicActvityServices();
			person.City = "Dushambe";
			person.District = AddDistrictIntoDatabase();
			person.FirstName = "Nicolas";
			person.LastName = "BARON";
		    person.LoanCycle = 1;
			person.Sex = 'M';
			person.IdentificationData = "123ARK3VC";
			person.HouseHoldHead = true;
		    person.Branch = _branch;
			person.Id = _clientManagement.AddPerson(person);
			return person;
		}

		public void DeleteTiers()
		{
            OpenCbsCommand deleteClientBranchHistory = new OpenCbsCommand("DELETE ClientBranchHistory", _connection);
		    deleteClientBranchHistory.ExecuteNonQuery();
            OpenCbsCommand deletePersonGroupBelonging = new OpenCbsCommand("DELETE PersonGroupBelonging", _connection);
			deletePersonGroupBelonging.ExecuteNonQuery();
            OpenCbsCommand deletePersons = new OpenCbsCommand("DELETE Persons", _connection);
			deletePersons.ExecuteNonQuery();
            OpenCbsCommand deleteGroups = new OpenCbsCommand("DELETE Groups", _connection);
			deleteGroups.ExecuteNonQuery();
            OpenCbsCommand deleteProject = new OpenCbsCommand("DELETE Projects", _connection);
            deleteProject.ExecuteNonQuery();
            OpenCbsCommand deleteSavingEvents = new OpenCbsCommand("DELETE SavingEvents", _connection);
            deleteSavingEvents.ExecuteNonQuery();
            OpenCbsCommand deleteSavingBookContracts = new OpenCbsCommand("DELETE FROM SavingBookContracts", _connection);
            deleteSavingBookContracts.ExecuteNonQuery();
            OpenCbsCommand deleteSavingDepositContracts = new OpenCbsCommand("DELETE FROM SavingDepositContracts", _connection);
            deleteSavingDepositContracts.ExecuteNonQuery();
            OpenCbsCommand deleteSavingContracts = new OpenCbsCommand("DELETE SavingContracts", _connection);
            deleteSavingContracts.ExecuteNonQuery();
            OpenCbsCommand deleteTiers = new OpenCbsCommand("DELETE Tiers", _connection);
			deleteTiers.ExecuteNonQuery();
		}

		#region DOA (Add and Delete)
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

			return services;
		}
		public EconomicActivity AddDomainOfApplicationTrade()
		{
			EconomicActivity services = new EconomicActivity();
			services.Name = "Trade";
			services.Parent = null;
			services.Deleted = false;
            services.Id = _domainOfApplicationManagement.AddEconomicActivity(services);

			return services;
		}
		public EconomicActivity AddDomainOfApplicationAgri()
		{
			EconomicActivity services = new EconomicActivity();
			services.Name = "Agri";
			services.Parent = null;
			services.Deleted = false;
            services.Id = _domainOfApplicationManagement.AddEconomicActivity(services);

			return services;
		}

		public void DeleteEconomicActivities()
		{
            SqlCommand delete = new SqlCommand("DELETE EconomicActivities", _connection);
			delete.ExecuteNonQuery();
		}
		#endregion

		#region Address (Add / Delete) Province And District
		public Province AddProvinceIntoDatabase()
		{
            SqlCommand command = new SqlCommand("INSERT INTO Provinces (name,deleted) VALUES ('Sugh',0) SELECT SCOPE_IDENTITY()", _connection);
			Province province = new Province();
			province.Name = "Sugh";
			province.Id = int.Parse(command.ExecuteScalar().ToString());
			return province;
		}

		public void DeleteProvince()
		{
			SqlCommand delete = new SqlCommand("DELETE Provinces",_connection);
			delete.ExecuteNonQuery();			
		}

		public District AddDistrictIntoDatabase()
		{
			Province province = AddProvinceIntoDatabase();
			District district = new District();
			district.Province = province;
			district.Name = "District";
            SqlCommand command = new SqlCommand("INSERT INTO Districts (name,province_id,deleted) VALUES ('" + district.Name + "',@provinceId,0) SELECT SCOPE_IDENTITY()", _connection);
			command.Parameters.Add(new SqlParameter("@provinceId",SqlDbType.Int));
			command.Parameters["@provinceId"].Value = province.Id;
			district.Id = int.Parse(command.ExecuteScalar().ToString());
			return district;
		}

		public void DeleteDistrict()
		{
			SqlCommand delete = new SqlCommand("DELETE Districts",_connection);
			delete.ExecuteNonQuery();			
		}

        public void DeleteCity()
        {
            SqlCommand delete = new SqlCommand("DELETE City", _connection);
            delete.ExecuteNonQuery();
        }
		#endregion

		#region (Add / Delete) AmountCycle

//		public AmountCycle AddAmountCycleBis()
//		{
//			AmountCycle cycle = new AmountCycle(2,200.5m,1000.05m);
//			SqlCommand insert = new SqlCommand("INSERT INTO AmountCycle (id,amount_min,amount_max) VALUES (@id,@min,@max)",_connection);
//			DatabaseHelper.InsertInt32Param("@id",insert,2);
//			DatabaseHelper.InsertMoneyParam("@min",insert,200.5m);
//			DatabaseHelper.InsertMoneyParam("@max",insert,1000.05m);
//			insert.ExecuteNonQuery();
//			return cycle;
//		    return null;
//		}

//		public AmountCycle AddAmountCycleTer()
//		{
//			AmountCycle cycle = new AmountCycle(3,200.5m,1000.05m);
//			SqlCommand insert = new SqlCommand("INSERT INTO AmountCycle (id,amount_min,amount_max) VALUES (@id,@min,@max)",_connection);
//			DatabaseHelper.InsertInt32Param("@id",insert,3);
//			DatabaseHelper.InsertMoneyParam("@min",insert,200.5m);
//			DatabaseHelper.InsertMoneyParam("@max",insert,1000.05m);
//			insert.ExecuteNonQuery();
//			return cycle;
//		    return null;
//		}

		public void DeleteInstallments()
		{
			SqlCommand delete = new SqlCommand("DELETE Installments",_connection);
			delete.ExecuteNonQuery();
		}

//		public AmountCycle AddAmountCycleQuater()
//		{
//			AmountCycle cycle = new AmountCycle(4,200.5m,1000.05m);
//			SqlCommand insert = new SqlCommand("INSERT INTO AmountCycle (id,amount_min,amount_max) VALUES (@id,@min,@max)",_connection);
//			DatabaseHelper.InsertInt32Param("@id",insert,4);
//			DatabaseHelper.InsertMoneyParam("@min",insert,200.5m);
//			DatabaseHelper.InsertMoneyParam("@max",insert,1000.05m);
//			insert.ExecuteNonQuery();
//			return cycle;
//		    return null;
//		}
		#endregion

		public void DeleteSaving()
		{
            OpenCbsCommand deleteSVE = new OpenCbsCommand("DELETE SavingEvents", _connection);
			deleteSVE.ExecuteNonQuery();
            OpenCbsCommand deleteSBC = new OpenCbsCommand("DELETE FROM SavingBookContracts", _connection);
            deleteSBC.ExecuteNonQuery();
            OpenCbsCommand deleteSDC = new OpenCbsCommand("DELETE FROM SavingDepositContracts", _connection);
            deleteSDC.ExecuteNonQuery();
            OpenCbsCommand deleteSVC = new OpenCbsCommand("DELETE SavingContracts", _connection);
			deleteSVC.ExecuteNonQuery();
            OpenCbsCommand deleteSVP = new OpenCbsCommand("DELETE SavingBookProducts", _connection);
            deleteSVP.ExecuteNonQuery();
            OpenCbsCommand deleteSBP = new OpenCbsCommand("DELETE SavingProducts", _connection);
			deleteSVP.ExecuteNonQuery();
		}
        public void AddGeneralParameterIntoDatabase()
        {
            OpenCbsCommand insert = new OpenCbsCommand("INSERT INTO [GeneralParameters]([key], [value]) VALUES('ID_PATTERN', '/d/d/w/w') SELECT SCOPE_IDENTITY()", _connection);
            insert.ExecuteScalar();
        }
	}
}
