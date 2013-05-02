using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenCBS.CoreDomain;

namespace OpenCBS.Test.CoreDomain
{
    [TestFixture]
    public class TestAmountCycleStock
    {
//        private AmountCycle _cycle1;
//        private AmountCycle _cycle2;
//        private AmountCycle _cycle3;
//        private AmountCycle _cycle4;

        [Test]
        public void Get_Enumerator()
        {
           Assert.Ignore();
            int i = 1;
//            foreach (AmountCycle cycle in _SetAmountCycleStockWith4Cycles())
//            {
//                Assert.AreEqual(i++, cycle.Number);
//            }
        }

        [Test]
        public void AddAmountCycle_Object()
        {
            Assert.Ignore();
//            object cycle = new AmountCycle(0,10,100);
//            AmountCycleStock list = new AmountCycleStock {cycle};

//            Assert.AreEqual(1,list.GetNumberOfLoanCycles);
        }

        [Test]
		public void TestMoveDecreaseAmountCycle()
        {
            Assert.Ignore();
//            AmountCycleStock amountCycles = _SetAmountCycleStockWith4Cycles();
//
//            Assert.AreEqual(4, amountCycles.GetNumberOfLoanCycles);
//
//            amountCycles.Move(_cycle4,1);
//
//            Assert.AreEqual(4, amountCycles.GetNumberOfLoanCycles);
//
//            Assert.AreEqual(1, amountCycles.GetAmountCycle(0).Number);
//            Assert.AreEqual(2, amountCycles.GetAmountCycle(1).Number);
//            Assert.AreEqual(3, amountCycles.GetAmountCycle(2).Number);
//            Assert.AreEqual(4, amountCycles.GetAmountCycle(3).Number);
//
//            Assert.AreEqual(10000m, amountCycles.GetAmountCycle(0).Min.Value);
//            Assert.AreEqual(10m, amountCycles.GetAmountCycle(1).Min.Value);
//            Assert.AreEqual(100m, amountCycles.GetAmountCycle(2).Min.Value);
//            Assert.AreEqual(1000m, amountCycles.GetAmountCycle(3).Min.Value);
        }

//        private AmountCycleStock _SetAmountCycleStockWith4Cycles()
//        {
//            _cycle1 = new AmountCycle(0, 10, 100);
//            _cycle2 = new AmountCycle(0, 100, 1000);
//            _cycle3 = new AmountCycle(0, 1000, 10000);
//            _cycle4 = new AmountCycle(0, 10000, 100000);
//            return new AmountCycleStock { _cycle1, _cycle2, _cycle3, _cycle4 };
//            return null;
//        }

        [Test]
        public void TestMoveIncreaseAmountCycle()
        {
            Assert.Ignore();
//            AmountCycleStock amountCycles = _SetAmountCycleStockWith4Cycles();
//
//            Assert.AreEqual(4, amountCycles.GetNumberOfLoanCycles);
//
//            amountCycles.Move(_cycle1, 3);
//
//            Assert.AreEqual(4, amountCycles.GetNumberOfLoanCycles);
//
//            Assert.AreEqual(1, amountCycles.GetAmountCycle(0).Number);
//            Assert.AreEqual(2, amountCycles.GetAmountCycle(1).Number);
//            Assert.AreEqual(3, amountCycles.GetAmountCycle(2).Number);
//            Assert.AreEqual(4, amountCycles.GetAmountCycle(3).Number);
//
//            Assert.AreEqual(100m, amountCycles.GetAmountCycle(0).Min.Value);
//            Assert.AreEqual(1000m, amountCycles.GetAmountCycle(1).Min.Value);
//            Assert.AreEqual(10m, amountCycles.GetAmountCycle(2).Min.Value);
//            Assert.AreEqual(10000m, amountCycles.GetAmountCycle(3).Min.Value);
        }

        [Test]
        public void TestMoveAmountCycleIncreaseWhenitsTheLast()
        {
            Assert.Ignore();
//            AmountCycleStock amountCycles = _SetAmountCycleStockWith4Cycles();
//
//            Assert.AreEqual(4, amountCycles.GetNumberOfLoanCycles);
//
//            amountCycles.Move(_cycle3, 4);
//
//            Assert.AreEqual(4, amountCycles.GetNumberOfLoanCycles);
//
//            Assert.AreEqual(1, amountCycles.GetAmountCycle(0).Number);
//            Assert.AreEqual(2, amountCycles.GetAmountCycle(1).Number);
//            Assert.AreEqual(3, amountCycles.GetAmountCycle(2).Number);
//            Assert.AreEqual(4, amountCycles.GetAmountCycle(3).Number);
//
//            Assert.AreEqual(10m, amountCycles.GetAmountCycle(0).Min.Value);
//            Assert.AreEqual(100m, amountCycles.GetAmountCycle(1).Min.Value);
//            Assert.AreEqual(10000m, amountCycles.GetAmountCycle(2).Min.Value);
//            Assert.AreEqual(1000m, amountCycles.GetAmountCycle(3).Min.Value);
        }

        [Test]
        public void TestRemoveAmountCycle()
        {
            Assert.Ignore();
//            AmountCycleStock amountCycles = _SetAmountCycleStockWith4Cycles();
//
//            Assert.AreEqual(4, amountCycles.GetNumberOfLoanCycles);
//
//            amountCycles.Remove(_cycle3);
//
//            Assert.AreEqual(3, amountCycles.GetNumberOfLoanCycles);
//
//            Assert.AreEqual(1, amountCycles.GetAmountCycle(0).Number);
//            Assert.AreEqual(2, amountCycles.GetAmountCycle(1).Number);
//            Assert.AreEqual(3, amountCycles.GetAmountCycle(2).Number);
//
//            Assert.AreEqual(10m, amountCycles.GetAmountCycle(0).Min.Value);
//            Assert.AreEqual(100m, amountCycles.GetAmountCycle(1).Min.Value);
//            Assert.AreEqual(10000m, amountCycles.GetAmountCycle(2).Min.Value);
        }
    }
}
