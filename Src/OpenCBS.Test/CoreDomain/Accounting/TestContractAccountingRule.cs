using NUnit.Framework;
using Octopus.CoreDomain.Accounting;
using Octopus.CoreDomain.EconomicActivities;
using Octopus.Enums;
using Octopus.CoreDomain.Products;

namespace Octopus.Test.CoreDomain.Accounting
{
    [TestFixture]
    public class TestContractAccountingRule
    {
        [Test]
        public void Get_Set_Id()
        {
            ContractAccountingRule rule = new ContractAccountingRule { Id = 1 };
            Assert.AreEqual(1, rule.Id);
        }

        [Test]
        public void Get_Set_GenericAccount()
        {
            Account genericAccount = new Account { Id = 10 };
            ContractAccountingRule rule = new ContractAccountingRule { DebitAccount = genericAccount };
            Assert.AreEqual(genericAccount, rule.DebitAccount);
        }

        [Test]
        public void Get_Set_SpecificAccount()
        {
            Account specificAccount = new Account { Id = 20 };
            ContractAccountingRule rule = new ContractAccountingRule { CreditAccount = specificAccount };
            Assert.AreEqual(specificAccount, rule.CreditAccount);
        }

        [Test]
        public void Get_Set_ProductType()
        {
            ContractAccountingRule rule = new ContractAccountingRule { ProductType = OProductTypes.All };
            Assert.AreEqual(OProductTypes.All, rule.ProductType);

            rule.ProductType = OProductTypes.Saving;
            Assert.AreEqual(OProductTypes.Saving, rule.ProductType);
        }

        [Test]
        public void Get_Set_LoanProduct()
        {
            ContractAccountingRule rule = new ContractAccountingRule();
            Assert.AreEqual(null, rule.LoanProduct);

            LoanProduct loanProduct = new LoanProduct { Id = 20, Code = "B/L/A/B/L/A" };
            rule.LoanProduct = loanProduct;
            Assert.AreEqual(loanProduct, rule.LoanProduct);
        }

        [Test]
        public void Get_Set_ClientType()
        {
            ContractAccountingRule rule = new ContractAccountingRule { ClientType = OClientTypes.All };
            Assert.AreEqual(OClientTypes.All, rule.ClientType);

            rule.ClientType = OClientTypes.Person;
            Assert.AreEqual(OClientTypes.Person, rule.ClientType);
        }

        [Test]
        public void Get_Set_EconomicActivity()
        {
            ContractAccountingRule rule = new ContractAccountingRule();
            Assert.AreEqual(null, rule.EconomicActivity);

            EconomicActivity economicActivity = new EconomicActivity { Id = 1, Name = "Econimic Acvitity" };
            rule.EconomicActivity = economicActivity;
            Assert.AreEqual(economicActivity, rule.EconomicActivity);
        }

        [Test]
        public void Get_Set_Deleted()
        {
            ContractAccountingRule rule = new ContractAccountingRule { Deleted = true };
            Assert.AreEqual(true, rule.Deleted);

            rule = new ContractAccountingRule();
            Assert.AreEqual(false, rule.Deleted);
        }

        [Test]
        public void Get_Set_BookingDirection()
        {
            ContractAccountingRule rule = new ContractAccountingRule { BookingDirection = OBookingDirections.Credit };
            Assert.AreEqual(OBookingDirections.Credit, rule.BookingDirection);

            rule.BookingDirection = OBookingDirections.Both;
            Assert.AreEqual(OBookingDirections.Both, rule.BookingDirection);
        }
    }
}
