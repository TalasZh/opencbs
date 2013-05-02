// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.CoreDomain.Contracts.Savings;

namespace OpenCBS.CoreDomain.Clients
{
	/// <summary>
	/// Client is the beneficiary of a contract. Client can be a GroupsClient or a Person. 
	/// Id and Name (person's last name in the case of a single client) are attributes 
	/// of this class. df
	/// </summary>
	public interface IClient
	{
		int Id{get;set;}
        OClientTypes Type { get; set; }
        string Name { get; set; }
		double? Scoring{get;set;}
        int LoanCycle { get; set;}
		bool Active{get;set;}
        bool BadClient { get; set;}
		string OtherOrgName{get;set;}
		OCurrency OtherOrgAmount{get;set;}
        OCurrency OtherOrgDebts { get;set;}
		string Address{get;set;}
		string City{get;set;}
        string ZipCode { get; set; }
		District District{get;set;}
		string SecondaryAddress{get;set;}
		string SecondaryCity{get;set;}
        string HomePhone { get; set; }
        string PersonalPhone { get; set; }
        string Email { get; set; }
        string SecondaryEmail { get; set; }
        string SecondaryHomeType { get; set; }
        string SecondaryZipCode { get; set; }
        string HomeType { get; set; }
        string OtherOrgComment { get; set; }
        string SecondaryHomePhone { get; set; }
        string SecondaryPersonalPhone { get; set; }
		District SecondaryDistrict{get;set;}
		bool SecondaryAddressIsEmpty{get;}
		bool SecondaryAddressPartiallyFilled{get;}
		int? CashReceiptIn{get;set;}
        int? CashReceiptOut { get;set;}
        List<Project> Projects { get; }
        IList<ISavingsContract> Savings { get; }
        void AddProject();
        void AddProject(Project pProject);
	    void AddProjects(List<Project> pProjects);
	    Project SelectProject(int pContractId);
	    void SetStatus();
        int NbOfProjects { get; }
        int NbOfloans { get; }
        int NbOfGuarantees { get; }
        OClientStatus Status{ get;set;}
        string Sponsor1 { get; set; }
        string Sponsor2 { get; set; }
        string Sponsor1Comment { get; set; }
        string Sponsor2Comment { get; set; }
        string FollowUpComment { get; set; }
        DateTime CreationDate { get; set; }
        List<Loan> ActiveLoans { get; set; }
	    IClient Copy();
        Branch Branch { get; set; }
	}
}
