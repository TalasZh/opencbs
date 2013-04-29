using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Octopus.CoreDomain;
using Octopus.CoreDomain.Clients;
using Octopus.CoreDomain.Contracts.Savings;
using Octopus.CoreDomain.Events;
using Octopus.Enums;
using Octopus.Manager;
using Octopus.Manager.Events;
using Octopus.Shared;
using Octopus.Test.Manager;

namespace Octopus.Test.EventsManager
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
        //    //savingContract.Product = new Octopus.CoreDomain.Products.SavingProduct() { Id = 1 };
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
        //    savingContract.Product = new Octopus.CoreDomain.Products.SavingProduct() { Id = 1, Name="SavingProduct" };
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
        //    savingContract.Product = new Octopus.CoreDomain.Products.SavingProduct() { Id = 1, Name="SavingProduct" };
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
        //    savingContract.Product = new Octopus.CoreDomain.Products.SavingProduct() { Id = 1, Name="SavingProduct" };
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
