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

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Mocks;
using OpenCBS.CoreDomain;
using OpenCBS.Services;
using OpenCBS.Manager;

namespace OpenCBS.Test.Services
{
    [TestFixture]
    public class TestLocationsServices
    {
        [Test]
        public void GetProvincesWithoutResult()
        {
            DynamicMock dynamicMock = new DynamicMock(typeof(LocationsManager));
            dynamicMock.SetReturnValue("GetProvinces",new List<Province>());

            LocationsManager locationsManager = (LocationsManager)dynamicMock.MockInstance;
            LocationServices locationServices = new LocationServices(locationsManager);

            Assert.AreEqual(0,locationServices.GetProvinces().Count);
        }

        [Test]
        public void GetProvincesWhithResults()
        {
            List<Province> provinces = new List<Province>
                                           {
                                               new Province(), 
                                               new Province(), 
                                               new Province(), 
                                               new Province()
                                           };

            DynamicMock dynamicMock = new DynamicMock(typeof(LocationsManager));
            dynamicMock.SetReturnValue("GetProvinces", provinces);

            LocationsManager locationsManager = (LocationsManager)dynamicMock.MockInstance;
            LocationServices locationServices = new LocationServices(locationsManager);

            Assert.AreEqual(4, locationServices.GetProvinces().Count);
        }

        [Test]
        public void TestGetDistrictsWithoutResult()
        {
            DynamicMock dynamicMock = new DynamicMock(typeof(LocationsManager));
            dynamicMock.SetReturnValue("GetDistricts", new List<District>());

            LocationsManager locationsManager = (LocationsManager)dynamicMock.MockInstance;
            LocationServices locationServices = new LocationServices(locationsManager);

            Assert.AreEqual(0, locationServices.GetDistricts().Count);

        }

        [Test]
        public void TestGetDistrictsWhithResults()
        {
            List<District> districts = new List<District>
                                           {
                                               new District(), 
                                               new District(), 
                                               new District()
                                           };

            DynamicMock dynamicMock = new DynamicMock(typeof(LocationsManager));
            dynamicMock.SetReturnValue("GetDistricts", districts);

            LocationsManager locationsManager = (LocationsManager)dynamicMock.MockInstance;
            LocationServices locationServices = new LocationServices(locationsManager);

            Assert.AreEqual(3, locationServices.GetDistricts().Count);
        }

        [Test]
        public void TestGetCitiesWithoutResult()
        {
            DynamicMock dynamicMock = new DynamicMock(typeof(LocationsManager));
            dynamicMock.SetReturnValue("GetCities", new List<City>());

            LocationsManager locationsManager = (LocationsManager)dynamicMock.MockInstance;
            LocationServices locationServices = new LocationServices(locationsManager);

            Assert.AreEqual(0, locationServices.GetCities().Count);

        }

        [Test]
        public void TestGetCitiesWhithResults()
        {
            List<City> cities = new List<City>
                                           {
                                               new City(), 
                                               new City(), 
                                               new City()
                                           };

            DynamicMock dynamicMock = new DynamicMock(typeof(LocationsManager));
            dynamicMock.SetReturnValue("GetCities", cities);

            LocationsManager locationsManager = (LocationsManager)dynamicMock.MockInstance;
            LocationServices locationServices = new LocationServices(locationsManager);

            Assert.AreEqual(3, locationServices.GetCities().Count);
        }

        [Test]
        public void TestDeleteCity()
        {
            List<City> cities = new List<City>();
            City city = new City { Name = "New York", DistrictId = 12 };
            DynamicMock dynamicMock = new DynamicMock(typeof(LocationsManager));
            dynamicMock.ExpectAndReturn("AddCity", 3,city);
            dynamicMock.SetReturnValue("GetCities", cities);
            dynamicMock.Expect("DeleteCityById", 2);

            LocationsManager mocklocationManager = (LocationsManager)dynamicMock.MockInstance;
            LocationServices locationService = new LocationServices(mocklocationManager);
            locationService.DeleteCity(2);
            Assert.AreEqual(3, locationService.AddCity(city));
            Assert.AreEqual(0, locationService.GetCities().Count);
        }

        private static List<City> _GetCities()
        {
            return new List<City>
                                           {
                                               new City {Name = "Paris",DistrictId = 24},
                                               new City {Name = "Pekin",DistrictId = 4},
                                               new City {Name = "Pau",DistrictId = 2}
                                           };
        }

        [Test]
        public void TestDeleteDistrictWithCityIn()
        {
            List<City> cities = _GetCities();

            DynamicMock dynamicMock = new DynamicMock(typeof(LocationsManager));
            dynamicMock.SetReturnValue("GetCities", cities);
            dynamicMock.Expect("DeleteDistrictById", 2);

            
            LocationsManager mocklocationManager = (LocationsManager)dynamicMock.MockInstance;
            LocationServices locationService = new LocationServices(mocklocationManager);
            Assert.AreEqual(false, locationService.DeleteDistrict(2));
          
        }

        [Test]
        public void TestDeleteDistrictWithOutCityIn()
        {
            List<City> cities = _GetCities();


            DynamicMock dynamicMock = new DynamicMock(typeof(LocationsManager));
            dynamicMock.SetReturnValue("GetCities", cities);
            dynamicMock.Expect("DeleteDistrictById", 8);


            LocationsManager mocklocationManager = (LocationsManager)dynamicMock.MockInstance;
            LocationServices locationService = new LocationServices(mocklocationManager);
            Assert.AreEqual(true, locationService.DeleteDistrict(8));

        }

        [Test]
        public void TestDeleteProvinceWithoutDistrictIn()
        {
            Province provinceOne = new Province(2, "Pekin");
            Province provinceTwo = new Province(8, "Qhinghua");

            List<District> districts = new List<District>
                                           {
                                               new District("Paris",provinceOne),
                                               new District("Pekin",provinceOne), 
                                               new District("Pau",provinceOne)
                                           };


            DynamicMock dynamicMock = new DynamicMock(typeof(LocationsManager));
            dynamicMock.SetReturnValue("GetDistricts", districts);
            dynamicMock.Expect("DeleteProvinceById", 8);


            LocationsManager mocklocationManager = (LocationsManager)dynamicMock.MockInstance;
            LocationServices locationService = new LocationServices(mocklocationManager);
            Assert.AreEqual(true, locationService.DeleteProvince(provinceTwo));

        }

        [Test]
        public void TestDeleteProvinceWithDistrictIn()
        {
            Province provinceOne = new Province(2, "Pekin");
            Province provinceTwo = new Province(8, "Qhinghua");

            List<District> districts = new List<District>
                                           {
                                               new District("Paris",provinceOne),
                                               new District("Pekin",provinceOne), 
                                               new District("Pau",provinceOne)
                                           };


            DynamicMock dynamicMock = new DynamicMock(typeof(LocationsManager));
            dynamicMock.SetReturnValue("GetDistricts", districts);
            dynamicMock.Expect("DeleteProvinceById", 8);


            LocationsManager mocklocationManager = (LocationsManager)dynamicMock.MockInstance;
            LocationServices locationService = new LocationServices(mocklocationManager);
            Assert.AreEqual(false, locationService.DeleteProvince(provinceOne));
        }

        [Test]
        public void TestUpdateCity()
        {
            City city = new City {Name = "Pekin", DistrictId = 12};
            List<City> cities = new List<City> {city};

            DynamicMock mockLocationsManager = new DynamicMock(typeof(LocationsManager));
            mockLocationsManager.ExpectAndReturn("AddCity", 3, city);
            mockLocationsManager.SetReturnValue("GetCities", cities);
            mockLocationsManager.ExpectAndReturn("UpdateCity", true, city);


            LocationsManager mocklocationManager = (LocationsManager)mockLocationsManager.MockInstance;
            LocationServices locationService = new LocationServices(mocklocationManager);
            Assert.AreEqual("Pekin", locationService.GetCities()[0].Name);
            Assert.AreEqual(true, locationService.UpdateCity(city));
        }

        [Test]
        public void TestUpdateDistrict()
        {
            Province provinceOne = new Province(2, "Pekin");
            District district = new District("Pekin", provinceOne);
            List<District> districts = new List<District> {district};


            DynamicMock mockLocationsManager = new DynamicMock(typeof (LocationsManager));
            mockLocationsManager.SetReturnValue("GetDistricts", districts);
            mockLocationsManager.ExpectAndReturn("UpdateDistrict", true, district);


            LocationsManager mocklocationManager = (LocationsManager) mockLocationsManager.MockInstance;
            LocationServices locationService = new LocationServices(mocklocationManager);

            Assert.AreEqual("Pekin", locationService.GetDistricts()[0].Name);
            Assert.AreEqual(true, locationService.UpdateDistrict(district));
        }

        [Test]
        public void TestUpdateProvince()
        {

            Province provinceOne = new Province(2, "Pekin");
            List<Province> provinces = new List<Province> {provinceOne};


            DynamicMock mockLocationsManager = new DynamicMock(typeof(LocationsManager));
            mockLocationsManager.SetReturnValue("GetProvinces", provinces);
            mockLocationsManager.ExpectAndReturn("UpdateProvince", true, provinceOne);


            LocationsManager mocklocationManager = (LocationsManager)mockLocationsManager.MockInstance;
            LocationServices locationService = new LocationServices(mocklocationManager);

            Assert.AreEqual("Pekin", locationService.GetProvinces()[0].Name);
            Assert.AreEqual(true, locationService.UpdateProvince(provinceOne));
        }
    }
}
