
using NUnit.Framework;
using Octopus.CoreDomain;
using Octopus.CoreDomain.Accounting;
using Octopus.CoreDomain.Contracts.Loans;
using Octopus.CoreDomain.Contracts.Loans.CalculateInstallments;
using Octopus.Enums;
using Octopus.Shared;
using Octopus.Shared.Settings;

namespace Octopus.Test.CoreDomain
{
	/// <summary>
	/// Description of TestCalculateInstallmentsOptions.
	/// </summary>
	[TestFixtureAttribute]
	public class TestCalculateInstallmentsOptions
	{
	    private CalculateInstallmentsOptions _installmentsOptions;
		[TestFixtureSetUpAttribute]
		public void testFixtureSetUp()
		{
			Loan credit = new Loan(new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), 
                ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
		    credit.Id = 4;

            _installmentsOptions = new CalculateInstallmentsOptions(credit.StartDate, OLoanTypes.Flat, true, credit, false);
		}
		
		[Test]
		public void Get_Set_Credit()
		{
			Assert.AreEqual(4,_installmentsOptions.Contract.Id);
		}

        [Test]
        public void Get_Set_ChangeDate()
        {
            Assert.IsFalse(_installmentsOptions.ChangeDate);
        }

        [Test]
        public void Get_Set_LoanType()
        {
            Assert.AreEqual(OLoanTypes.Flat, _installmentsOptions.LoanType);
        }

        [Test]
        public void Get_Set_IsExotic()
        {
            Assert.IsTrue(_installmentsOptions.IsExotic);
        }
	}
}
