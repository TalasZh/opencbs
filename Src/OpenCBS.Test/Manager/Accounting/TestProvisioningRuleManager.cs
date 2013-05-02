// LICENSE PLACEHOLDER

using System.Collections.Generic;
using System.Data.SqlClient;
using NUnit.Framework;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Manager.Accounting;

namespace OpenCBS.Test.Manager.Accounting
{
    [TestFixture]
    public class TestProvisioningRuleManager : BaseManagerTest
    {
        [Test]
        public void AddProvisionningRateInDatabase()
        {
            ProvisioningRuleManager provisioningRuleManager = (ProvisioningRuleManager) container["ProvisioningRuleManager"];
            ProvisioningRate rate = new ProvisioningRate {Number = 12, NbOfDaysMin = 1000, NbOfDaysMax = 1111, Rate = 2};
            SqlTransaction transaction = provisioningRuleManager.GetConnection().BeginTransaction();
            provisioningRuleManager.AddProvisioningRate(rate, transaction);
            transaction.Commit();
        }

        [Test]
        public void SelectProvisioningRates()
        {
            ProvisioningRuleManager provisioningRuleManager = (ProvisioningRuleManager)container["ProvisioningRuleManager"];
            List<ProvisioningRate> list = provisioningRuleManager.SelectAllProvisioningRates();

            Assert.AreEqual(7, list.Count);

            _AssertProvisioningRule(list[0], 1, 0, 0, 0.02);
            _AssertProvisioningRule(list[1], 2, 1, 30, 0.1);
            _AssertProvisioningRule(list[2], 3, 31, 60, 0.25);
            _AssertProvisioningRule(list[3], 4, 61, 90, 0.5);
            _AssertProvisioningRule(list[4], 5, 91, 180, 0.75);
            _AssertProvisioningRule(list[5], 6, 181, 365, 1);
            _AssertProvisioningRule(list[6], 7, 366, 99999, 1);
        }

        private static void _AssertProvisioningRule(ProvisioningRate pProvisioningRate, int pNumber, int pNbOfDaysMin, int pNbOfDaysMax, double pRate)
        {
            Assert.AreEqual(pNumber, pProvisioningRate.Number);
            Assert.AreEqual(pNbOfDaysMin, pProvisioningRate.NbOfDaysMin);
            Assert.AreEqual(pNbOfDaysMax, pProvisioningRate.NbOfDaysMax);
            Assert.AreEqual(pRate, pProvisioningRate.Rate);
        }

        [Test]
        public void TestDeleteProvisionningRules()
        {
            ProvisioningRuleManager provisioningRuleManager = (ProvisioningRuleManager)container["ProvisioningRuleManager"];
            SqlTransaction transaction = provisioningRuleManager.GetConnection().BeginTransaction();
            provisioningRuleManager.DeleteAllProvisioningRules(transaction);
            transaction.Commit();

            List<ProvisioningRate> list = provisioningRuleManager.SelectAllProvisioningRates();

            Assert.AreEqual(0, list.Count);
        }
    }
}
