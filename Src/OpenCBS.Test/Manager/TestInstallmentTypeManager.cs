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

using System.Collections.Generic;
using NUnit.Framework;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.Manager;

namespace OpenCBS.Test.Manager
{
	[TestFixture]
	public class TestInstallmentTypeManager : BaseManagerTest
	{
        [Test]
        public void AddInstallmentTypeInDatabase()
        {
            InstallmentTypeManager installmentTypeManager = (InstallmentTypeManager)container["InstallmentTypeManager"];
            int id = installmentTypeManager.AddInstallmentType(new InstallmentType{Name = "Bi-Monthly", NbOfDays = 0, NbOfMonths = 2});

            Assert.AreNotEqual(0,id);
        }

	    [Test]
		public void SelectInstallmentType_Monthly()
		{
		    InstallmentTypeManager installmentTypeManager = (InstallmentTypeManager) container["InstallmentTypeManager"];
	        InstallmentType selectedInstallmentType = installmentTypeManager.SelectInstallmentType(1);

            _AssertInstallmentType(selectedInstallmentType, 1, "monthly", 0, 1);
		}


        [Test]
        public void SelectInstallmentType_Weekly()
        {
            InstallmentTypeManager installmentTypeManager = (InstallmentTypeManager)container["InstallmentTypeManager"];
            InstallmentType selectedInstallmentType = installmentTypeManager.SelectInstallmentType(2);

            _AssertInstallmentType(selectedInstallmentType,2,"weekly",7,0);
        }

	    private static void _AssertInstallmentType(InstallmentType selectedInstallmentType,int pId, string pName, int pNbOfDays, int pNbOfMonths)
	    {
            Assert.AreEqual(pId, selectedInstallmentType.Id);
            Assert.AreEqual(pName, selectedInstallmentType.Name);
            Assert.AreEqual(pNbOfDays, selectedInstallmentType.NbOfDays);
            Assert.AreEqual(pNbOfMonths, selectedInstallmentType.NbOfMonths);
	    }

	    [Test]
		public void SelectAllInstallmentTypes()
		{
            InstallmentTypeManager installmentTypeManager = (InstallmentTypeManager)container["InstallmentTypeManager"];

		    List<InstallmentType> selectedList = installmentTypeManager.SelectAllInstallmentTypes();
            Assert.AreEqual(2, selectedList.Count);

            _AssertInstallmentType(selectedList[0], 1, "monthly", 0, 1);
            _AssertInstallmentType(selectedList[1], 2, "weekly", 7, 0);
		}
	}
}
