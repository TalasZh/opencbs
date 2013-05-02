// LICENSE PLACEHOLDER

using NUnit.Framework;
using System.Collections;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Contracts;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Events;
using OpenCBS.Shared;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Test.CoreDomain
{
	[TestFixture]
	public class TestClosureContractStock
	{
		[Test]
		public void TestAddContract()
		{
			ClosureContractStock closureContractStock = new ClosureContractStock();
			closureContractStock.AddContract(new Loan(new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User())));
			Assert.AreEqual(1,closureContractStock.Contracts.Count);
		}

		[Test]
		public void TestDeleteContract()
		{
			ClosureContractStock closureContractStock = new ClosureContractStock();
            Loan contract1 = new Loan(new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()))
                                   {Id = 1};
            Loan contract2 = new Loan(new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()))
		                           {Id = 2};
		    closureContractStock.AddContract(contract1);
			closureContractStock.AddContract(contract2);
			Assert.AreEqual(2,closureContractStock.Contracts.Count);
			closureContractStock.DeleteContract(contract1.Id);
			Assert.AreEqual(1,closureContractStock.Contracts.Count);
		}

		[Test]
		public void TestGetWriteOffContracts()
		{
			ClosureContractStock closureContractStock = new ClosureContractStock();
            Loan contract1 = new Loan(new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()))
                                   {Id = 1};
		    Loan contract2 = new Loan(new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
			contract2.Events.Add(new WriteOffEvent());
			contract2.Id = 2;
		    contract2.WrittenOff = true;
			closureContractStock.AddContract(contract1);
			closureContractStock.AddContract(contract2);
			Assert.AreEqual(1,closureContractStock.WriteOffContracts.Count);
		}

		[Test]
		public void TestIsWriteOffContractWhenItIs()
		{
			ClosureContractStock closureContractStock = new ClosureContractStock();
            Loan contract1 = new Loan(new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()))
                                   {Id = 1};
		    contract1.Events.Add(new WriteOffEvent());
			closureContractStock.AddContract(contract1);
			
			Assert.IsTrue(closureContractStock.IsWriteOffContract(contract1));
		}

		[Test]
		public void TestIsWriteOffContractWhenItIsNot()
		{
			ClosureContractStock closureContractStock = new ClosureContractStock();
            Loan contract1 = new Loan(new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()))
                                   {Id = 1};
		    closureContractStock.AddContract(contract1);
			Assert.IsFalse(closureContractStock.IsWriteOffContract(contract1));
		}
	}
}
