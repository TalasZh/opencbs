using System;
using System.Globalization;
using System.Linq;
using NUnit.Framework;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;
using TechTalk.SpecFlow;

namespace OpenCBS.Specflow
{
    [Binding]
    public class SimpleFixedInstallmentScheduleSteps
    {
        private static readonly ApplicationSettings Settings = ApplicationSettings.GetInstance("");
        private static readonly NonWorkingDateSingleton NonWorkingDays = NonWorkingDateSingleton.GetInstance("");

        private LoanProduct _loanProduct;
        private Loan _loan;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Settings.AddParameter("INCREMENTAL_DURING_DAYOFFS", true);
            Settings.AddParameter("DONOT_SKIP_WEEKENDS_IN_INSTALLMENTS_DATE", false);
            Settings.AddParameter("USE_CENTS", false);
            Settings.AddParameter("PAY_FIRST_INSTALLMENT_REAL_VALUE", true);
            Settings.AddParameter("ALIGN_INSTALLMENTS_ON_REAL_DISBURSEMENT_DATE", false);
            Settings.AddParameter("WEEK_END_DAY1", 6);
            Settings.AddParameter("WEEK_END_DAY2", 0);

            NonWorkingDays.WeekEndDay1 = 6;
            NonWorkingDays.WeekEndDay2 = 0;            
        }

        [Given(@"the ""(.*)"" loan product")]
        public void GivenTheLoanProduct(string loanProductName)
        {
            _loanProduct = LoanProducts.Instance[loanProductName];
        }
        
        private decimal TableToAmount(Table attributes)
        {
            return (
                from row in attributes.Rows 
                where row["Name"] == "Amount"
                select Convert.ToDecimal(row["Value"])
            ).FirstOrDefault();
        }

        private decimal TableToInterestRate(Table attributes)
        {
            return (
                from row in attributes.Rows
                where row["Name"] == "Interest rate"
                select Convert.ToDecimal(row["Value"])
            ).FirstOrDefault();
        }

        private int TableToInstallments(Table attributes)
        {
            return (
                from row in attributes.Rows
                where row["Name"] == "Installments"
                select Convert.ToInt32(row["Value"])
            ).FirstOrDefault();
        }

        private int TableToGracePeriod(Table attributes)
        {
            return (
                from row in attributes.Rows
                where row["Name"] == "Grace period"
                select Convert.ToInt32(row["Value"])
            ).FirstOrDefault();
        }

        private DateTime TableToStartDate(Table attributes)
        {
            var russianCultureInfo = new CultureInfo("ru-RU");
            return (
                from row in attributes.Rows
                where row["Name"] == "Start date"
                select DateTime.Parse(row["Value"], russianCultureInfo, DateTimeStyles.AssumeLocal)
            ).FirstOrDefault();
        }

        [When(@"I create a loan with the attributes")]
        public void WhenICreateALoanWithTheAttributes(Table attributes)
        {
            _loan = new Loan(
                _loanProduct,
                TableToAmount(attributes),
                TableToInterestRate(attributes),
                TableToInstallments(attributes),
                TableToGracePeriod(attributes),
                TableToStartDate(attributes),
                new User(), 
                Settings,
                NonWorkingDays,
                ProvisionTable.GetInstance(new User()),
                ChartOfAccounts.GetInstance(new User())
            );
        }
        
        [Then(@"the schedule is")]
        public void ThenTheScheduleIs(Table table)
        {
            var russianCultureInfo = new CultureInfo("ru-RU");
            var i = 0;
            foreach (var row in table.Rows)
            {
                var installment = _loan.InstallmentList[i];
                int number = Convert.ToInt32(row["Number"]);
                Assert.That(installment.Number, Is.EqualTo(number));

                var expectedDate = DateTime.Parse(row["Expected Date"], russianCultureInfo, DateTimeStyles.AssumeLocal);
                Assert.That(installment.ExpectedDate, Is.EqualTo(expectedDate));

                i++;
            }
        }
    }
}
