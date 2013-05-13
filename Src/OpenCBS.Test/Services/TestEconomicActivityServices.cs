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
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Mocks;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.EconomicActivities;
using OpenCBS.Services;
using OpenCBS.Manager;
using OpenCBS.ExceptionsHandler;

namespace OpenCBS.Test.Services
{
	/// <summary>
    /// Summary description for TestEconomicActivityServices.
	/// </summary>
	
	[TestFixture]
    public class TestEconomicActivityServices : BaseServicesTest
	{
        private EconomicActivityManager _economicActivityManager;
        private EconomicActivityServices _economicActivityServices;

	    private DynamicMock _dynamicMock;

		[Test]
		public void FindAllEconomicActivitiesWhenResult()
        {
            _dynamicMock = new DynamicMock(typeof(EconomicActivityManager));
            EconomicActivity activityA1 = new EconomicActivity { Id = 3, Name = "ServicesA1" };
            EconomicActivity activityB2 = new EconomicActivity { Id = 4, Name = "ServicesB2" };
            EconomicActivity activityA = new EconomicActivity { Id = 1,Name = "ServicesA", Parent = null ,Childrens = new List<EconomicActivity>{activityA1,activityB2}};
            EconomicActivity activityB = new EconomicActivity { Id = 2, Name = "ServicesB", Parent = null };

		    List<EconomicActivity> activities = new List<EconomicActivity> {activityA, activityB};

            _dynamicMock.SetReturnValue("SelectAllEconomicActivities", activities);

            _economicActivityManager = (EconomicActivityManager)_dynamicMock.MockInstance;
            _economicActivityServices = new EconomicActivityServices(_economicActivityManager);

            Assert.AreEqual(2, _economicActivityServices.FindAllEconomicActivities().Count);
		}

        [Test]
        public void FindAllEconomicActivitiesWhithoutResult()
        {
            _dynamicMock = new DynamicMock(typeof(EconomicActivityManager));
            List<EconomicActivity> activities = new List<EconomicActivity> ();

            _dynamicMock.SetReturnValue("SelectAllEconomicActivities", activities);

            _economicActivityManager = (EconomicActivityManager)_dynamicMock.MockInstance;
            _economicActivityServices = new EconomicActivityServices(_economicActivityManager);

            Assert.AreEqual(0, _economicActivityServices.FindAllEconomicActivities().Count);
        }

        [Test]
        [ExpectedException(typeof(OpenCbsDoaSaveException))]
        public void AddEconomicActivityWhenNameIsEmpty()
        {
            _dynamicMock = new DynamicMock(typeof(EconomicActivityManager));
            EconomicActivity activity = new EconomicActivity { Name = String.Empty };

            _dynamicMock.ExpectAndReturn("AddEconomicActivity",0, activity);

            _economicActivityManager = (EconomicActivityManager)_dynamicMock.MockInstance;
            _economicActivityServices = new EconomicActivityServices(_economicActivityManager);

            _economicActivityServices.AddEconomicActivity(activity);
        }

        [Test]
        [ExpectedException(typeof(OpenCbsDoaSaveException))]
        public void AddEconomicActivityWhenNameAlreadyExist()
        {
            _dynamicMock = new DynamicMock(typeof(EconomicActivityManager));
            EconomicActivity activity = new EconomicActivity { Name = "Services", Parent = new EconomicActivity { Id = 1 } };

            _dynamicMock.ExpectAndReturn("ThisActivityAlreadyExist", true, "Services", 1);
            _dynamicMock.ExpectAndReturn("AddEconomicActivity", 0, activity);

            _economicActivityManager = (EconomicActivityManager)_dynamicMock.MockInstance;
            _economicActivityServices = new EconomicActivityServices(_economicActivityManager);

            _economicActivityServices.AddEconomicActivity(activity);
        }

        [Test]
        public void AddEconomicActivity()
        {
            _dynamicMock = new DynamicMock(typeof(EconomicActivityManager));
            EconomicActivity activity = new EconomicActivity { Name = "Services",Parent = new EconomicActivity {Id=1}};

            _dynamicMock.ExpectAndReturn("ThisActivityAlreadyExist", false, "Services", 1);
            _dynamicMock.ExpectAndReturn("AddEconomicActivity", 2, activity);

            _economicActivityManager = (EconomicActivityManager)_dynamicMock.MockInstance;
            _economicActivityServices = new EconomicActivityServices(_economicActivityManager);

            activity.Id = _economicActivityServices.AddEconomicActivity(activity);
            Assert.AreEqual(2,activity.Id);
        }

        [Test]
        [ExpectedException(typeof(OpenCbsDoaSaveException))]
        public void UpdateEconomicActivityButNewNameAlreadyExist()
        {
            _dynamicMock = new DynamicMock(typeof(EconomicActivityManager));
            EconomicActivity activity = new EconomicActivity { Name = "Services", Parent = new EconomicActivity { Id = 1 } };

            _dynamicMock.Expect("UpdateEconomicActivity", activity);
            _dynamicMock.ExpectAndReturn("ThisActivityAlreadyExist", true, "Services", 1);

            _economicActivityManager = (EconomicActivityManager)_dynamicMock.MockInstance;
            _economicActivityServices = new EconomicActivityServices(_economicActivityManager);
            
            _economicActivityServices.ChangeDomainOfApplicationName(activity, "Services");
        }

        [Test]
        public void UpdateEconomicActivity()
        {
            _dynamicMock = new DynamicMock(typeof(EconomicActivityManager));
            EconomicActivity activity = new EconomicActivity { Name = "Services", Parent = new EconomicActivity { Id = 1 } };

            _dynamicMock.Expect("UpdateEconomicActivity", activity);
            _dynamicMock.ExpectAndReturn("ThisActivityAlreadyExist", false, "Services", 1);

            _economicActivityManager = (EconomicActivityManager)_dynamicMock.MockInstance;
            _economicActivityServices = new EconomicActivityServices(_economicActivityManager);

            _economicActivityServices.ChangeDomainOfApplicationName(activity, "Services");
        }

        [Test]
        public void TestChangeDomainOfApplicationNameWhenNotInUse()
        {
            _dynamicMock = new DynamicMock(typeof(EconomicActivityManager));
            EconomicActivity activity = new EconomicActivity { Id = 2, Name = "Services",Parent = new EconomicActivity { Id = 1 }};

            _dynamicMock.Expect("UpdateEconomicActivity", activity);
            _dynamicMock.ExpectAndReturn("ThisActivityAlreadyExist", false, "GMO agriculture", 1);
            _economicActivityManager = (EconomicActivityManager)_dynamicMock.MockInstance;
            _economicActivityServices = new EconomicActivityServices(_economicActivityManager);
            Assert.AreEqual(true, _economicActivityServices.ChangeDomainOfApplicationName(activity, "GMO agriculture"));
        }

        [Test]
        [ExpectedException(typeof(OpenCbsDoaUpdateException))]
        public void TestNodeEditableWhenDOAIsNull()
        {
            EconomicActivity dom = null;

            _economicActivityServices.NodeEditable(dom);
        }

        [Test]
        [ExpectedException(typeof(OpenCbsDoaUpdateException))]
        public void TestNodeEditableWhenObjectNotADAO()
        {
            Person dom = new Person();

            _economicActivityServices.NodeEditable(dom);
        }

        [Test]
        public void TestNodeEditableWhenObjectIsNotNullAndDAO()
        {
            EconomicActivity dom = new EconomicActivity { Id = 1, Name = "Services", Parent = null };
            Assert.IsTrue(_economicActivityServices.NodeEditable(dom));
        }

        [Test]
        [ExpectedException(typeof(OpenCbsDoaUpdateException))]
        public void TestChangeDomainNameWhenNewNameIsEmpty()
        {
            EconomicActivity dom = new EconomicActivity
                                          {
                                              Id = 1,
                                              Name = "Services",
                                              Parent = new EconomicActivity()
                                          };
            _economicActivityServices.ChangeDomainOfApplicationName(dom, String.Empty);
        }

        [Test]
        [ExpectedException(typeof(OpenCbsDoaDeleteException))]
        public void TestDeleteDomainWhenDomainHasChildrens()
        {
            _dynamicMock = new DynamicMock(typeof(EconomicActivityManager));
            EconomicActivity activity = new EconomicActivity { Id = 1, Name = "Services", Childrens = new List<EconomicActivity> 
                                    {new EconomicActivity(),new EconomicActivity()} };

            _dynamicMock.Expect("UpdateEconomicActivity", activity);

            _economicActivityManager = (EconomicActivityManager)_dynamicMock.MockInstance;
            _economicActivityServices = new EconomicActivityServices(_economicActivityManager);

            _economicActivityServices.DeleteEconomicActivity(activity);
        }

        [Test]
        public void TestDeleteDomainWhenDomainHasNoChildrens()
        {
            _dynamicMock = new DynamicMock(typeof(EconomicActivityManager));
            EconomicActivity activity = new EconomicActivity { Id = 1, Name = "Services", Childrens = new List<EconomicActivity>()};

            _dynamicMock.Expect("UpdateEconomicActivity",activity);

            _economicActivityManager = (EconomicActivityManager)_dynamicMock.MockInstance;
            _economicActivityServices = new EconomicActivityServices(_economicActivityManager);

            _economicActivityServices.DeleteEconomicActivity(activity);
        }
	}
}
