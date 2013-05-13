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
using System.Xml;
using OpenCBS.CoreDomain;
using NUnit.Framework;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.Test.CoreDomain
{
	/// <summary>
	/// Summary description for TestAlert.
	/// </summary>
	[TestFixture]
	public class TestAlert
	{
		[Test]
		public void Get_Set_Type()
		{
			Alert alert = new Alert {Type = 'D'};
		    Assert.AreEqual('D',alert.Type);
		}

        [Test]
        public void Get_Set_ContractId()
        {
            Alert alert = new Alert {LoanId = 1};
            Assert.AreEqual(1, alert.LoanId);
        }

        [Test]
        public void Get_Set_ContractCode()
        {
            Alert alert = new Alert {LoanCode = "KJFUUU"};
            Assert.AreEqual("KJFUUU", alert.LoanCode);
        }

        [Test]
        public void Get_Set_ClientName()
        {
            Alert alert = new Alert {ClientName = "Nicolas"};
            Assert.AreEqual("Nicolas", alert.ClientName);
        }

		[Test]
        public void Get_Set_Amount()
		{

			Alert alert = new Alert();
			alert.Amount = 100.34m;
			Assert.AreEqual(100.34m, alert.Amount.Value);

		}


        [Test]
        public void Get_Set_OLB()
        {
            Alert alert = new Alert { OLB = 100.39m };
            Assert.AreEqual(100.39m, alert.OLB.Value);
        }

        [Test]
        public void Get_Set_InterestRate()
        {
            Alert alert = new Alert { InterestRate = 100.34 };
            Assert.AreEqual(100.34, alert.InterestRate);
        }

		[Test]
        public void Get_Set_EffectDate()
		{
			Alert alert = new Alert {EffectDate = new DateTime(2007, 1, 1)};
		    Assert.AreEqual(new DateTime(2007,1,1),alert.EffectDate);
        }

        [Test]
        public void Get_Set_CreationDate()
        {
            Alert alert = new Alert { CreationDate = new DateTime(2007, 1, 1) };
            Assert.AreEqual(new DateTime(2007, 1, 1), alert.CreationDate);
        }

        [Test]
        public void Get_Set_StartDate()
        {
            Alert alert = new Alert { StartDate = new DateTime(2007, 1, 1) };
            Assert.AreEqual(new DateTime(2007, 1, 1), alert.StartDate);
        }

        [Test]
        public void Get_Set_CloseDate()
        {
            Alert alert = new Alert { CloseDate = new DateTime(2007, 1, 1) };
            Assert.AreEqual(new DateTime(2007, 1, 1), alert.CloseDate);
        }

        [Test]
        public void Get_Set_OColor()
        {
            TimeProvider.SetToday(new DateTime(2008, 1, 1));

            Alert alert = new Alert {EffectDate = new DateTime(2006, 1, 1), Amount = 1000};
            Assert.AreEqual(OAlertColors.Color1, alert.Color);

            alert.EffectDate = new DateTime(2007, 4, 1);
            Assert.AreEqual(OAlertColors.Color2, alert.Color);

            alert.EffectDate = new DateTime(2007, 10, 1);
            Assert.AreEqual(OAlertColors.Color3, alert.Color);

            alert.EffectDate = new DateTime(2007, 11, 1);
            Assert.AreEqual(OAlertColors.Color4, alert.Color);

            alert.EffectDate = new DateTime(2007, 12, 1);
            Assert.AreEqual(OAlertColors.Color5, alert.Color);

            alert.EffectDate = new DateTime(2007, 12, 6);
            Assert.AreEqual(OAlertColors.Color6, alert.Color);

            alert.EffectDate = new DateTime(2008, 1, 1);
            Assert.AreEqual(OAlertColors.Color7, alert.Color);

            Alert alertSavings = new Alert { EffectDate = new DateTime(2006, 1, 1), Amount = -1000 };
            Assert.AreEqual(OAlertColors.Color1, alertSavings.Color);

            TimeProvider.SetToday(DateTime.Today);
        }

	}
}
