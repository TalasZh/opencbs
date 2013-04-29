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

using System.Data.SqlClient;
using System.Data;
using Octopus.CoreDomain.Clients;
using Octopus.CoreDomain.EconomicActivities;
using Octopus.DatabaseConnection;
using Octopus.Manager.Clients;
using Octopus.CoreDomain;
using Octopus.Manager;

namespace Octopus.Test.Services
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
            OctopusCommand command = new OctopusCommand("INSERT INTO Tiers (creation_date,client_type_code,active,bad_client,district_id,city,loan_cycle) VALUES ('1/1/2007','G',1,0,@districtId,'Tress',1) SELECT SCOPE_IDENTITY()", _connection);
            command.AddParam("@districtId", districtId);
			int tiersId = int.Parse(command.ExecuteScalar().ToString());

            OctopusCommand insert = null;
			
			if (type.Equals("group"))
                insert = new OctopusCommand("INSERT INTO Groups (id,name) VALUES (" + tiersId + ",'SCG')", _connection);
			else if (type.Equals("person"))
                insert = new OctopusCommand("INSERT INTO Persons (id,first_name,sex,identification_data,last_name,household_head) VALUES (" + tiersId + ",'Nicolas','M','123KH','BARON',1)", _connection);
			
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
            OctopusCommand delete = new OctopusCommand("DELETE Packages", _connection);
			delete.ExecuteNonQuery();
		}
		public void DeleteCreditContract()
		{

            OctopusCommand deleteLinkSavings = new OctopusCommand("DELETE FROM [LoansLinkSavingsBook]", _connection);
		    deleteLinkSavings.ExecuteNonQuery();
            OctopusCommand deleteLinkGuarantor3 = new OctopusCommand("DELETE LoanDisbursmentEvents", _connection);
            deleteLinkGuarantor3.ExecuteNonQuery();
            OctopusCommand deleteInstallmentHistory = new OctopusCommand("DELETE InstallmentHistory", _connection);
            deleteInstallmentHistory.ExecuteNonQuery();
            OctopusCommand deleteLinkGuarantor2 = new OctopusCommand("DELETE ContractEvents", _connection);
            deleteLinkGuarantor2.ExecuteNonQuery();
            OctopusCommand deleteLinkGuarantor = new OctopusCommand("DELETE LinkGuarantorCredit", _connection);
			deleteLinkGuarantor.ExecuteNonQuery();
            OctopusCommand deleteCredit = new OctopusCommand("DELETE Credit", _connection);
			deleteCredit.ExecuteNonQuery();
			DeletePackage();
            DeleteFundingLine();
            OctopusCommand deleteContract = new OctopusCommand("DELETE Contracts", _connection);
			deleteContract.ExecuteNonQuery();
			DeleteTiers();
            DeleteCity();
			DeleteDistrict();
			DeleteProvince();
		}
        public void DeleteCorporatePerson()
        {
            OctopusCommand deleteCorporatePerson = new OctopusCommand("DELETE CorporatePersonBelonging", _connection);
            deleteCorporatePerson.ExecuteNonQuery();

        }
        public void DeleteCorporate()
        {
            OctopusCommand deleteCorporatePerson = new OctopusCommand("DELETE Corporates", _connection);
            deleteCorporatePerson.ExecuteNonQuery();

        }

		public int AddGenericPackage()
		{
            OctopusCommand insert = new OctopusCommand("INSERT INTO Packages (currency_id, deleted,name,client_type,installment_type,loan_type,charge_interest_within_grace_period,keep_expected_installment,anticipated_total_repayment_base) VALUES (" + AddGenericCurrency() + ", 0,'Package','G',1,1,0,0,1) SELECT SCOPE_IDENTITY()", _connection);
			return int.Parse(insert.ExecuteScalar().ToString());
		}
        public int AddGenericCurrency()
        {
            OctopusCommand insert = new OctopusCommand("INSERT INTO Currencies (name, code, is_pivot, is_swapped) VALUES ('USD', 'USD', 1, 0) SELECT SCOPE_IDENTITY()", _connection);
            return int.Parse(insert.ExecuteScalar().ToString());
        }

		public int AddGenericFundingLine()
		{
            OctopusCommand insert = new OctopusCommand("INSERT INTO FundingLines (name,amount, purpose, deleted, currency_id, begin_date, end_date) VALUES ('AFD130',0, 'Not Set', 0, " + AddGenericCurrency() + ", '20080101', '20120101') SELECT SCOPE_IDENTITY()", _connection);
            return int.Parse(insert.ExecuteScalar().ToString());
		}

		public int AddGenericCreditContractIntoDatabase()
		{
			DeleteCreditContract();

			int tiersId = AddGenericTiersIntoDatabase("group");

            OctopusCommand insertProject = new OctopusCommand(@"INSERT INTO Projects([tiers_id], [status], [name], [code], [aim], [begin_date]) VALUES 
    (" + tiersId + " , 2,'NOT SET','NOT SET','NOT SET','10/10/2005') SELECT SCOPE_IDENTITY()", _connection);
            int projectId = int.Parse(insertProject.ExecuteScalar().ToString());


            OctopusCommand insertContract = new OctopusCommand("INSERT INTO [Contracts]([contract_code], [branch_code], [creation_date], [start_date], [close_date], [closed], [project_id], [activity_id]) VALUES " +
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

            OctopusCommand insertCredit = new OctopusCommand(sqlText, _connection);

            insertCredit.AddParam("@id", contractId);
            insertCredit.AddParam("@packageId", packageId);

			insertCredit.ExecuteNonQuery();

            OctopusCommand insertInstallment = new OctopusCommand(@"INSERT INTO [Installments]([expected_date],[interest_repayment],[capital_repayment],[contract_id],
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
            OctopusCommand deleteClientBranchHistory = new OctopusCommand("DELETE ClientBranchHistory", _connection);
		    deleteClientBranchHistory.ExecuteNonQuery();
            OctopusCommand deletePersonGroupBelonging = new OctopusCommand("DELETE PersonGroupBelonging", _connection);
			deletePersonGroupBelonging.ExecuteNonQuery();
            OctopusCommand deletePersons = new OctopusCommand("DELETE Persons", _connection);
			deletePersons.ExecuteNonQuery();
            OctopusCommand deleteGroups = new OctopusCommand("DELETE Groups", _connection);
			deleteGroups.ExecuteNonQuery();
            OctopusCommand deleteProject = new OctopusCommand("DELETE Projects", _connection);
            deleteProject.ExecuteNonQuery();
            OctopusCommand deleteSavingEvents = new OctopusCommand("DELETE SavingEvents", _connection);
            deleteSavingEvents.ExecuteNonQuery();
            OctopusCommand deleteSavingBookContracts = new OctopusCommand("DELETE FROM SavingBookContracts", _connection);
            deleteSavingBookContracts.ExecuteNonQuery();
            OctopusCommand deleteSavingDepositContracts = new OctopusCommand("DELETE FROM SavingDepositContracts", _connection);
            deleteSavingDepositContracts.ExecuteNonQuery();
            OctopusCommand deleteSavingContracts = new OctopusCommand("DELETE SavingContracts", _connection);
            deleteSavingContracts.ExecuteNonQuery();
            OctopusCommand deleteTiers = new OctopusCommand("DELETE Tiers", _connection);
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
            OctopusCommand deleteSVE = new OctopusCommand("DELETE SavingEvents", _connection);
			deleteSVE.ExecuteNonQuery();
            OctopusCommand deleteSBC = new OctopusCommand("DELETE FROM SavingBookContracts", _connection);
            deleteSBC.ExecuteNonQuery();
            OctopusCommand deleteSDC = new OctopusCommand("DELETE FROM SavingDepositContracts", _connection);
            deleteSDC.ExecuteNonQuery();
            OctopusCommand deleteSVC = new OctopusCommand("DELETE SavingContracts", _connection);
			deleteSVC.ExecuteNonQuery();
            OctopusCommand deleteSVP = new OctopusCommand("DELETE SavingBookProducts", _connection);
            deleteSVP.ExecuteNonQuery();
            OctopusCommand deleteSBP = new OctopusCommand("DELETE SavingProducts", _connection);
			deleteSVP.ExecuteNonQuery();
		}
        public void AddGeneralParameterIntoDatabase()
        {
            OctopusCommand insert = new OctopusCommand("INSERT INTO [GeneralParameters]([key], [value]) VALUES('ID_PATTERN', '/d/d/w/w') SELECT SCOPE_IDENTITY()", _connection);
            insert.ExecuteScalar();
        }
	}
}
