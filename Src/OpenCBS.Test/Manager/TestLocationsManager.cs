// LICENSE PLACEHOLDER

using System.Collections.Generic;
using NUnit.Framework;
using OpenCBS.CoreDomain;
using OpenCBS.Manager;

namespace OpenCBS.Test.Manager
{
    [TestFixture]
    public class TestLocationsManager : BaseManagerTest
    {
        [Test]
        public void AddProvince()
        {
            LocationsManager _locationManager = (LocationsManager)container["LocationsManager"]; 
            Province province = new Province("France");

            province.Id = _locationManager.AddProvince(province.Name);

            Assert.AreNotEqual(0, province.Id);
        }

        [Test]
        public void TestDeleteProvince()
        {
            LocationsManager _locationManager = (LocationsManager)container["LocationsManager"];
            Province province = new Province("France");

            province.Id = _locationManager.AddProvince(province.Name);

            Assert.AreNotEqual(0, province.Id);

            List<Province> provinces = _locationManager.GetProvinces();
            Assert.AreEqual(2, provinces.Count);
            _locationManager.DeleteProvinceById(province.Id);
            provinces = _locationManager.GetProvinces();
            Assert.AreEqual(1, provinces.Count);
        }

        [Test]
        public void TestUpdateProvince()
        {
            LocationsManager _locationManager = (LocationsManager)container["LocationsManager"];

            List<Province> provinces = _locationManager.GetProvinces();
            Assert.AreEqual(1, provinces.Count);

            Assert.AreEqual("FRANCE", provinces[0].Name);
            provinces[0].Name = "china";
            _locationManager.UpdateProvince(provinces[0]);
            provinces = _locationManager.GetProvinces();
            Assert.AreEqual("china", provinces[0].Name);
        }


        [Test]
        public void SelectProvincesOnly()
        {
            LocationsManager _locationManager = (LocationsManager)container["LocationsManager"];
            Province provinceA = new Province("France");
            Province provinceB = new Province("Tadjikistan");
            Province provinceC = new Province("Russian");

            provinceA.Id = _locationManager.AddProvince(provinceA.Name);
            provinceB.Id = _locationManager.AddProvince(provinceB.Name);
            provinceC.Id = _locationManager.AddProvince(provinceC.Name);

            List<Province> provinceList = _locationManager.GetProvinces();
            Assert.AreEqual(4, provinceList.Count); //provinceA, provinceB, provinceC + BaseManagerTest

            _Contains(provinceA, provinceList);
            _Contains(provinceB, provinceList);
            _Contains(provinceC, provinceList);
        }

        private static void _Contains(Province pProvince, IEnumerable<Province> pProvincesList)
        {
            Province selectedProvince = null;
            foreach (Province province in pProvincesList)
            {
                if (province.Name != pProvince.Name)
                {
                    continue;
                }
                selectedProvince = province;
                break;
            }
            if (selectedProvince == null) Assert.Fail();
            else
                Assert.AreEqual(selectedProvince.Name, pProvince.Name);
        }

        [Test]
        public void AddDistrict()
        {
            LocationsManager _locationManager = (LocationsManager)container["LocationsManager"];
            
            Province province = new Province("France");
            province.Id = _locationManager.AddProvince(province.Name);
            Assert.AreNotEqual(0, province.Id);

            District district = new District("Ile de France",province);
            district.Id = _locationManager.AddDistrict(district);

            Assert.AreNotEqual(0, district.Id);
        }

        [Test]
        public void TestUpdateDistrict()
        {
            LocationsManager _locationManager = (LocationsManager)container["LocationsManager"];

            Province province = new Province("France");
            province.Id = _locationManager.AddProvince(province.Name);
            Assert.AreNotEqual(0, province.Id);

            List<District> districts = _locationManager.GetDistricts();
            Assert.AreEqual(2, districts.Count);
            Assert.AreEqual("NANCY", districts[0].Name);
            Assert.AreEqual(2, districts[0].Id);
            Assert.AreEqual("PARIS", districts[1].Name);
            Assert.AreEqual(1, districts[1].Id);

            districts[0].Name = "china";
            _locationManager.UpdateDistrict(districts[0]);
            districts = _locationManager.GetDistricts();

            districts.Sort(new DistrictComparer());
            Assert.AreEqual(2, districts.Count);
            Assert.AreEqual("china", districts[0].Name);
            Assert.AreEqual(2, districts[0].Id);
            Assert.AreEqual("PARIS", districts[1].Name);
            Assert.AreEqual(1, districts[1].Id);
        }

        [Test]
        public void TestDeleteDistrict()
        {
            LocationsManager _locationManager = (LocationsManager)container["LocationsManager"];

            Province province = new Province("France");
            province.Id = _locationManager.AddProvince(province.Name);
            Assert.AreNotEqual(0, province.Id);

            District district = new District("Ile de France", province);
            district.Id = _locationManager.AddDistrict(district);

            Assert.AreNotEqual(0, district.Id);

            List<District> districts = _locationManager.GetDistricts();
            Assert.AreEqual(3, districts.Count);

            _locationManager.DeleteDistrictById(district.Id);
            districts = _locationManager.GetDistricts();
            Assert.AreEqual(2, districts.Count);
        }

        [Test]
        public void AddCity()
        {
            LocationsManager _locationManager = (LocationsManager)container["LocationsManager"];
            
            Province province = new Province("France");
            province.Id = _locationManager.AddProvince(province.Name);
            Assert.AreNotEqual(0, province.Id);

            District district = new District("Ile de France", province);
            district.Id = _locationManager.AddDistrict(district);
            Assert.AreNotEqual(0, district.Id);

            City city = new City {Name = "Paris", DistrictId = district.Id};
            _locationManager.AddCity(city);
        }

        [Test]
        public void TestUpdateCity()
        {
            LocationsManager _locationManager = (LocationsManager)container["LocationsManager"];

            Province province = new Province("France");
            province.Id = _locationManager.AddProvince(province.Name);
            Assert.AreNotEqual(0, province.Id);

            District district = new District("Ile de France", province);
            district.Id = _locationManager.AddDistrict(district);
            Assert.AreNotEqual(0, district.Id);

            City city = new City {Name = "Paris", DistrictId = district.Id};
            city.Id=_locationManager.AddCity(city);

            city.Name = "qsd";
            _locationManager.UpdateCity(city);
            List<City> cities = _locationManager.GetCities();
            Assert.AreEqual("qsd", cities[0].Name);
        }

        [Test]
        public void TestDeleteCity()
        {
            LocationsManager _locationManager = (LocationsManager)container["LocationsManager"];

            Province province = new Province("France");
            province.Id = _locationManager.AddProvince(province.Name);
            Assert.AreNotEqual(0, province.Id);

            District district = new District("Ile de France", province);
            district.Id = _locationManager.AddDistrict(district);
            Assert.AreNotEqual(0, district.Id);

            City city = new City { Name = "Paris", DistrictId = district.Id };
            city.Id=_locationManager.AddCity(city);
            Assert.IsTrue(city.Id > 0);

            List<City> cities = _locationManager.GetCities();
            Assert.AreEqual(1, cities.Count); 

           _locationManager.DeleteCityById(city.Id);
           cities = _locationManager.GetCities();
           Assert.AreEqual(0, cities.Count); 
        }
    }
}
