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
using OpenCBS.CoreDomain;
using NUnit.Framework;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.Shared;
using OpenCBS.Enums;

namespace OpenCBS.Test.CoreDomain
{
	/// <summary>
	/// Tests for domain object
	/// </summary>
	[TestFixture]
	public class TestGroup
	{

		private Group testGroup;
	
		[SetUp]
		public void SetUp()
		{
			testGroup = new Group();
		}

        [Test]
        public void Copy_MembersAreCopied()
        {
            Person person = new Person {Id = 1, Active = true, BadClient = true};
            Member member = new Member {Tiers = person, IsLeader = true, CurrentlyIn = true};
            Group group = new Group {Id = 3,BadClient = false, Active = true, Members = new List<Member> {member}};

            Group clonedGroup = (Group)group.Copy();
            Assert.AreEqual(3, clonedGroup.Id);
            
            clonedGroup.Id = 7;
            Assert.AreEqual(3,group.Id);

            Assert.AreEqual(true, clonedGroup.Members[0].Tiers.BadClient);
            clonedGroup.Members[0].Tiers.BadClient = false;
            Assert.AreEqual(true, group.Members[0].Tiers.BadClient);
        }

	    [Test]
		public void IdCorrectlySetAndRetrieved()
		{
			testGroup.Id = 1;
			Assert.AreEqual(1,testGroup.Id);
		}

		[Test]
		public void TypeCorrectlySetAndRetrieved()
		{
            testGroup.Type = OClientTypes.Group;
            Assert.AreEqual(OClientTypes.Group, testGroup.Type);
		}

		[Test]
		public void TestIfScoringCorrectlySetAndRetrieved()
		{
			testGroup.Scoring = 13.5;
			Assert.AreEqual(13.5,testGroup.Scoring.Value);
		}

		[Test]
		public void TestIfAmountCycleCorrectlySetAndRetrieved()
		{
			testGroup.LoanCycle = 1;
			Assert.AreEqual(1,testGroup.LoanCycle);
		}

		[Test]
		public void TestIfActiveIsCorrectlySetAndRetrieved()
		{
			testGroup.Active = true;
			Assert.IsTrue(testGroup.Active);
		}

		[Test]
		public void TestIfOtherOrgNameIsCorrectlySetAndRetrieved()
		{
			testGroup.OtherOrgName = "Planet Finance";
			Assert.AreEqual("Planet Finance" , testGroup.OtherOrgName);
		}

		[Test]
		public void TestIfOtherOrgAmountIsCorrectlySetAndRetrieved()
		{
			testGroup.OtherOrgAmount = 200.5m;
			Assert.AreEqual(200.5m,testGroup.OtherOrgAmount.Value);
		}

		[Test]
		public void TestIfOtherOrgDebtsIsCorrectlySetAndRetrieved()
		{
			testGroup.OtherOrgDebts = 200.5m;
			Assert.AreEqual(200.5m,testGroup.OtherOrgDebts.Value);
		}

		[Test]
		public void TestIfAddressIsCorrectlySetAndRetrieved()
		{
			testGroup.Address = "50 avenue des Champs Elys�es";
			Assert.AreEqual("50 avenue des Champs Elys�es",testGroup.Address);
		}

		[Test]
		public void TestIfCityIsCorrectlySetAndRetrieved()
		{
			testGroup.City = "Paris";
			Assert.AreEqual("Paris",testGroup.City);
		}

		[Test]
		public void TestIfDistrictIsCorrectlySetAndRetrieved()
		{
			District district = new District(1,"Qath",new Province(1,"Khatlon"));
			testGroup.District = district;

			Assert.AreEqual(1,testGroup.District.Id);
			Assert.AreEqual("Qath",testGroup.District.Name);
			Assert.AreEqual(1,testGroup.District.Province.Id);
			Assert.AreEqual("Khatlon",testGroup.District.Province.Name);
		}
		
		[Test]
		public void TestIfSecondaryAddressIsCorrectlySetAndRetrieved()
		{
			testGroup.SecondaryAddress = "50 avenue des Champs Elys�es";
			Assert.AreEqual("50 avenue des Champs Elys�es",testGroup.SecondaryAddress);
		}

		[Test]
		public void TestIfSecondaryCityIsCorrectlySetAndRetrieved()
		{
			testGroup.SecondaryCity = "Paris";
			Assert.AreEqual("Paris",testGroup.SecondaryCity);
		}

		[Test]
		public void TestIfSecondaryDistrictIsCorrectlySetAndRetrieved()
		{
			District district = new District(1,"tress",new Province(1,"Sugh"));
			testGroup.SecondaryDistrict = district;
			Assert.AreEqual(district.Id,testGroup.SecondaryDistrict.Id);
			Assert.AreEqual(district.Name,testGroup.SecondaryDistrict.Name);
			Assert.AreEqual(1,testGroup.SecondaryDistrict.Province.Id);
			Assert.AreEqual("Sugh",testGroup.SecondaryDistrict.Province.Name);
		}

		[Test]
		public void TestIfSecondaryAddressIsEmpty()
		{
			Group group = new Group();
			District district = new District(1,"tress",new Province(1,"Sugh"));

			group.SecondaryDistrict = null;
			group.SecondaryCity = null;
			Assert.IsTrue(group.SecondaryAddressIsEmpty);

			group.SecondaryDistrict = district;
			Assert.IsFalse(group.SecondaryAddressIsEmpty);

			group.SecondaryCity = "Paris";
			Assert.IsFalse(group.SecondaryAddressIsEmpty);

			group.SecondaryDistrict = null;
			group.SecondaryCity = null;
			Assert.IsTrue(group.SecondaryAddressIsEmpty);
		}

		[Test]
		public void TestIfSecondaryAddressPartiallyFilled()
		{
			Group group = new Group();
			District district = new District(1,"tress",new Province(1,"Sugh"));

			group.SecondaryDistrict = null;
			group.SecondaryCity = null;
			Assert.IsFalse(group.SecondaryAddressPartiallyFilled);

			group.SecondaryDistrict = district;
			Assert.IsTrue(group.SecondaryAddressPartiallyFilled);

			group.SecondaryCity = "city";
			Assert.IsFalse(group.SecondaryAddressPartiallyFilled);

			group.SecondaryDistrict = null;
			group.SecondaryCity = null;
			Assert.IsFalse(group.SecondaryAddressPartiallyFilled);

			group.SecondaryCity = "paris";
			Assert.IsTrue(group.SecondaryAddressPartiallyFilled);
		}

		[Test]
		public void TestIfNameIsCorrectlySetAndRetrieved()
		{
			testGroup.Name = "Maravalavo";
			Assert.AreEqual("Maravalavo",testGroup.Name);
		}

		[Test]
		public void TestIfEstablishmentDateIsCorrectlySetAndRetrieved()
		{
			testGroup.EstablishmentDate = new DateTime(2006,7,6);
			Assert.AreEqual(new DateTime(2006,7,6),testGroup.EstablishmentDate.Value);
		}

		[Test]
		public void TestIfCommentsIsCorrectlySetAndRetrieved()
		{
			testGroup.Comments = "This group has always paid correctly due installments";
			Assert.AreEqual("This group has always paid correctly due installments",testGroup.Comments);
		}

        [Test]
        public void TestIfOtherMFICommentsIsCorrectlySetAndRetrieved()
        {
            testGroup.OtherOrgComment = "This group has always paid correctly due installments,comment by other orgs";
            Assert.AreEqual("This group has always paid correctly due installments,comment by other orgs", testGroup.OtherOrgComment);
        }

		[Test]
		public void TestIfLeaderIsCorrectlySetAndRetrieved()
		{
			Person person = new Person {FirstName = "nicolas"};
            testGroup.Leader = new Member { Tiers = person, LoanShareAmount = 1000, CurrentlyIn = true, IsLeader = true, JoinedDate = TimeProvider.Today };
			Assert.AreEqual("nicolas",((Person)testGroup.Leader.Tiers).FirstName);
		}

		[Test]
		public void TestIfArrayListOfPersonsIsCorrectlySetAndRetrieved()
		{
		    testGroup.AddMember(new Member{Tiers = new Person(),LoanShareAmount = 1000,CurrentlyIn = true,IsLeader = false,JoinedDate = TimeProvider.Today});
			Assert.AreEqual(1,testGroup.Members.Count);
		}

		[Test]
		public void TestIfGroupIsMemberOfAnOtherOrganization()
		{
            var newGroup = new Group {OtherOrgAmount = null, OtherOrgDebts = null, OtherOrgName = null};
		    Assert.IsFalse(newGroup.HasOtherOrganization());

			newGroup.OtherOrgAmount = 123;
			Assert.IsTrue(newGroup.HasOtherOrganization());

			newGroup.OtherOrgDebts = 1233;
			Assert.IsTrue(newGroup.HasOtherOrganization());

			newGroup.OtherOrgName = "planet finance";
			Assert.IsTrue(newGroup.HasOtherOrganization());
            
			newGroup.OtherOrgAmount = null;
			Assert.IsTrue(newGroup.HasOtherOrganization());

			newGroup.OtherOrgDebts = null;
			Assert.IsTrue(newGroup.HasOtherOrganization());

			newGroup.OtherOrgName = null;
			Assert.IsFalse(newGroup.HasOtherOrganization());
		}

		[Test]
		public void TestNumberOfMemberCorrectlyGet()
		{
			Group group = new Group();
			Assert.AreEqual(0,group.GetNumberOfMembers);
            group.AddMember(new Member { Tiers = new Person(), LoanShareAmount = 10, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today });
			Assert.AreEqual(1,group.GetNumberOfMembers);
		}

		[Test]
		public void TestGetTotalLoanAmount()
		{
			Group group = new Group();
			Person pers1 = new Person {Id = 1};
		    Person pers2 = new Person {Id = 2};
            group.AddMember(new Member { Tiers = pers1, LoanShareAmount = 100, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today });
            group.AddMember(new Member { Tiers = pers2, LoanShareAmount = 76.78m, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today });

			Assert.AreEqual(176.78m,group.GetTotalLoanAmount.Value);
		}
	}
}
