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
using System.Text;
using NUnit.Framework;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.CoreDomain.Events;
using OpenCBS.Enums;
using OpenCBS.Manager;
using OpenCBS.Manager.Events;
using OpenCBS.Shared;
using OpenCBS.Test.Manager;

namespace OpenCBS.Test.EventsManager
{
	[TestFixture]
	public class TestSavingManager : BaseManagerTest
	{
        //private SavingManager savingManager;
        //private User user;

        //[TestFixtureSetUp]
        //public void TestFixtureSetUp()
        //{
        //    //base.InitManager();
        //    //GeneralSettings.GetInstance("").DeleteAllParameters();
        //    //GeneralSettings.GetInstance("").AddParameter(OGeneralSettings.BRANCHCODE, "BC");
        //    //GeneralSettings.GetInstance("").AddParameter(OGeneralSettings.DONOTSKIPWEEKENDSININSTALLMENTSDATE, false);
        //}

        //[Test]
        //public void TestSavingManagerInit()
        //{
        //    //savingManager = (SavingManager)container["SavingManager"];
        //    //Assert.IsNotNull(savingManager);
        //}

        //[Test]
        //public void TestAddSaving()
        //{

        //    //Saving savingContract = new Saving();
        //    //Client client = new Person { Id = 1 };

        //    //DateTime date = new DateTime(2008, 11, 18);
        //    //savingContract.Product = new OpenCBS.CoreDomain.Products.SavingProduct() { Id = 1 };
        //    //savingContract.CreationDate = date;
        //    //savingContract.User = new User { Id = 6 };
        //    //savingContract.InterestRate = 0.2;
        //    //savingContract.Description = "contract description";
        //    //savingContract.Code = "SV12";
        //    //savingContract.InterestRate = 0.2;
        //    //savingManager = (SavingManager)container["SavingManager"];

        //    //savingManager.AddContract(savingContract, client, null);

        //    //Assert.Greater(savingContract.Id, 0);
        //}

        //[Test]
        //public void TestGetSavingAfterSave()
        //{
        //    Saving savingContract = new Saving();
        //    Client client = new Person { Id = 1, LastName="Guigui" };

        //    DateTime date = new DateTime(2008, 11, 18);
        //    savingContract.Product = new OpenCBS.CoreDomain.Products.SavingProduct() { Id = 1, Name="SavingProduct" };
        //    savingContract.CreationDate = date;
        //    savingContract.User = new User { Id = 6 };
        //    savingContract.InterestRate = 0.2;
        //    savingContract.Description = "contract description";
        //    savingContract.Code = "SV12";
        //    savingContract.CreationDate = date;
        //    savingContract.InterestRate = 0.2;
        //    savingManager = (SavingManager)container["SavingManager"];
        //    savingManager.AddContract(savingContract, client, null);

        //    Saving loadedContract = savingManager.SelectSavingById(savingContract.Id);
        //    Assert.AreEqual(savingContract.Id, loadedContract.Id);
        //    Assert.AreEqual(savingContract.InterestRate, loadedContract.InterestRate);
        //    Assert.AreEqual(savingContract.Code, loadedContract.Code);
        //    Assert.AreEqual(savingContract.CreationDate, loadedContract.CreationDate);
        //    Assert.AreEqual(savingContract.Description, loadedContract.Description);
        //    Assert.AreEqual(savingContract.User.Id, loadedContract.User.Id);
        //}

        //[Test]
        //public void TestGetSaving()
        //{
        //    Saving loadedContract = savingManager.SelectSavingById(1);
        //    Assert.AreEqual(1, loadedContract.Id);
        //    Assert.AreEqual(0.25, loadedContract.InterestRate);
        //    Assert.AreEqual("S/BC/2007/SAVIN-1/ELFA-6", loadedContract.Code);
        //    Assert.AreEqual(2007, loadedContract.CreationDate.Year);
        //    Assert.AreEqual("contract description", loadedContract.Description);
        //    Assert.AreEqual(6, loadedContract.User.Id);
        //}

        //[Test]
        //public void TestGetSavingsByClient()
        //{
        //    GeneralSettings.GetInstance("").DeleteAllParameters();
        //    GeneralSettings.GetInstance("").AddParameter(OGeneralSettings.BRANCHCODE, "BC");
			
        //    Saving savingContract = new Saving();
        //    Client client = new Person { Id = 6, LastName = "EL Fanidi" };

        //    DateTime date = new DateTime(2008, 11, 18);
        //    savingContract.Product = new OpenCBS.CoreDomain.Products.SavingProduct() { Id = 1, Name="SavingProduct" };
        //    savingContract.CreationDate = date;
        //    savingContract.User = new User { Id = 6 };
        //    savingContract.InterestRate = 0.2;
        //    savingContract.Description = "contract description";
        //    savingContract.Code = "SV12";
        //    savingContract.GenerateSavingCode(client, 1, "BC");
        //    savingContract.CreationDate = date;
        //    savingContract.InterestRate = 0.2;
        //    savingManager = (SavingManager)container["SavingManager"];
        //    savingManager.AddContract(savingContract, client, null);

        //    List<Saving> loadedContracts = savingManager.SelectSavings(client);
        //    Assert.AreEqual(1, loadedContracts[0].Id);
        //    Assert.AreEqual(0.25, loadedContracts[0].InterestRate);
        //    Assert.AreEqual("S/BC/2007/SAVIN-1/ELFA-6", loadedContracts[0].Code);
        //    Assert.AreEqual(2007, loadedContracts[0].CreationDate.Year);
        //    Assert.AreEqual("contract description", loadedContracts[0].Description);
        //    Assert.AreEqual(6, loadedContracts[0].User.Id);

        //    Assert.AreEqual(savingContract.Id, loadedContracts[1].Id);
        //    Assert.AreEqual(savingContract.InterestRate, loadedContracts[1].InterestRate);
        //    Assert.AreEqual(savingContract.Code, loadedContracts[1].Code);
        //    Assert.AreEqual("S/BC/2008/SAVIN-2/ELFA-6", loadedContracts[1].Code);
        //    Assert.AreEqual(savingContract.CreationDate, loadedContracts[1].CreationDate);
        //    Assert.AreEqual(savingContract.Description, loadedContracts[1].Description);
        //    Assert.AreEqual(savingContract.User.Id, loadedContracts[1].User.Id);

        //}

        //[Test]
        //public void TestGetSavingsCountByClient()
        //{
        //    Saving savingContract = new Saving();
        //    Client client = new Person { Id = 6 };

        //    Assert.AreEqual(1, savingManager.GetNumberOfSavings(client));
        //    DateTime date = new DateTime(2008, 11, 18);
        //    savingContract.Product = new OpenCBS.CoreDomain.Products.SavingProduct() { Id = 1, Name="SavingProduct" };
        //    savingContract.CreationDate = date;
        //    savingContract.User = new User { Id = 6 };
        //    savingContract.InterestRate = 0.2;
        //    savingContract.Description = "contract description";
        //    savingContract.Code = "SV12";
        //    savingContract.CreationDate = date;
        //    savingContract.InterestRate = 0.2;
        //    savingManager = (SavingManager)container["SavingManager"];
        //    savingManager.AddContract(savingContract, client, null);

        //    Assert.AreEqual(2, savingManager.GetNumberOfSavings(client));
        //}

	}
}
