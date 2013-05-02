
using NUnit.Framework;
using OpenCBS.CoreDomain.Events;
using OpenCBS.CoreDomain.Events.Loan;

namespace OpenCBS.Test.CoreDomain.Events
{
    [TestFixture]
    public class TestEventStock
    {
        private EventStock _eventStock;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _eventStock = new EventStock();
            _eventStock.Add(new RepaymentEvent());
            _eventStock.Add(new RescheduledLoanRepaymentEvent());
            _eventStock.Add(new BadLoanRepaymentEvent());
            _eventStock.Add(new StatisticalProvisionningEvent());
            _eventStock.Add(new LoanDisbursmentEvent());
            _eventStock.Add(new RescheduleLoanEvent());
            _eventStock.Add(new WriteOffEvent());
        }

        [Test]
        public void GetEventByType_Event()
        {
            Assert.AreEqual(0, _eventStock.GetEventsByType(typeof(Event)).Count);
        }

        [Test]
        public void GetEventByType_RepaymentEvent()
        {
            Assert.AreEqual(1, _eventStock.GetEventsByType(typeof(RepaymentEvent)).Count);
        }

        [Test]
        public void GetEvents()
        {
            Assert.AreEqual(7, _eventStock.GetEvents().Count);
        }

        [Test]
        public void GetRepaymentEvents()
        {
            Assert.AreEqual(3, _eventStock.GetRepaymentEvents().Count);
        }
    }
}