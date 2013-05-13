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
using System.Data.SqlClient;
using OpenCBS.CoreDomain;
using OpenCBS.Shared;

namespace OpenCBS.Manager
{
    /// <summary>
    /// Database manager for locations : Provinces, Districts and Cities.
    /// </summary>
    public class LocationsManager : Manager
    {
        public LocationsManager(User pUser) : base(pUser) { }

        public LocationsManager(string testDB) : base(testDB) { }

        public List<Province> GetProvinces()
        {
            List<Province> provinces = new List<Province>();
            const string q = "SELECT [id],[name] FROM [Provinces]  WHERE [deleted] = 0 ORDER BY name";

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (r != null)
                    {
                        while (r.Read())
                        {
                            Province province = new Province();
                            province.Id = r.GetInt("id");
                            province.Name = r.GetString("name");
                            provinces.Add(province);
                        }
                    }
                }
            }
            return provinces;
        }

        public List<City> GetCities()
        {
            List<City> cities = new List<City>();
            const string q = "SELECT [id], [name] ,[district_id]FROM [City] WHERE [deleted]=0 ORDER BY name ";

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            using (OpenCbsReader r = c.ExecuteReader())
            {
                if (r != null)
                {
                    while (r.Read())
                    {
                        City city = new City();
                        city.Id = r.GetInt("id");
                        city.Name = r.GetString("name");
                        city.DistrictId = r.GetInt("district_id");
                        cities.Add(city);
                    }
                }
            }
            
            return cities;
        }

        public List<District> GetDistricts()
        {
            List<Province> provinces = GetProvinces();

            List<District> districts = new List<District>();

            const string q = "SELECT [id], [name], [province_id] FROM [Districts]  WHERE [deleted]=0 ORDER BY name";

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            using (OpenCbsReader reader = c.ExecuteReader())
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        District district = new District();
                        district.Id = reader.GetInt("id");
                        district.Name = reader.GetString("name");
                        int province_id = reader.GetInt("province_id");
                        foreach (Province p in provinces)
                        {
                            if (p.Id == province_id)
                            {
                                district.Province = p;
                            }
                        }
                        districts.Add(district);
                    }
                }
            }
            
            return districts;
        }

        public int AddDistrict(string pName, int pProvinceId)
        {
            const string q = "INSERT INTO [Districts] ([name],[province_id],[deleted]) VALUES( @name, @province,0) SELECT SCOPE_IDENTITY()";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@name", pName);
                c.AddParam("@province", pProvinceId);
                c.AddParam("@deleted", false);
                return int.Parse(c.ExecuteScalar().ToString());
            }
        }

        public District SelectDistrictById(int pId)
        {
            District district = null;

            const string q = "SELECT Districts.id, Districts.name, Districts.province_id, " +
                                   "Provinces.id AS province_id, Provinces.name AS province_name " +
                                   "FROM Districts INNER JOIN " +
                                   "Provinces ON Districts.province_id = Provinces.id " +
                                   "WHERE Districts.id= @id ORDER BY Districts.name";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@id", pId);

                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (r != null)
                    {
                        if (!r.Empty)
                        {
                            r.Read();
                            district = new District();
                            district.Province = new Province();
                            district.Id = r.GetInt("id");
                            district.Name = r.GetString("name");
                            district.Province.Id = r.GetInt("province_id");
                            district.Province.Name = r.GetString("province_name");
                        }
                    }
                }
            }
            return district;
        }

        public District SelectDistrictByName(string name)
        {
            District district = null;

            const string q = "SELECT Districts.id, Districts.name, Districts.province_id, " +
                                   "Provinces.id AS province_id, Provinces.name AS province_name " +
                                   "FROM Districts INNER JOIN " +
                                   "Provinces ON Districts.province_id = Provinces.id " +
                                   "WHERE Districts.name= @name";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@name", name);

                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (r != null)
                    {
                        if (!r.Empty)
                        {
                            r.Read();
                            district = new District();
                            district.Province = new Province();
                            district.Id = r.GetInt("id");
                            district.Name = r.GetString("name");
                            district.Province.Id = r.GetInt("province_id");
                            district.Province.Name = r.GetString("province_name");
                        }
                    }
                }
            }
            return district;
        }

        public int AddProvince(string pName)
        {
            const string q = "INSERT INTO [Provinces] ([name], [deleted]) VALUES (@name,0) SELECT SCOPE_IDENTITY()";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@name", pName);
                c.AddParam("@deleted", false);
                return int.Parse(c.ExecuteScalar().ToString());
            }
        }

        public bool UpdateProvince(Province pProvince)
        {
            bool updateOk = false;
            try
            {
                const string q = "UPDATE [Provinces] SET [name]=@name WHERE id=@id";
                using (SqlConnection conn = GetConnection())
                using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
                {
                    c.AddParam("@id", pProvince.Id);
                    c.AddParam("@name", pProvince.Name);
                    c.ExecuteNonQuery();
                    updateOk = true;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return updateOk;
        }

        public void DeleteProvinceById(int pProvinceId)
        {
            const string q = "UPDATE [Provinces]  SET [deleted]=1 WHERE id=@id";

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@id", pProvinceId);
                c.ExecuteNonQuery();
            }
        }

        public int AddDistrict(District pDistrict)
        {
            const string q = "INSERT INTO [Districts]([name], [province_id],[deleted]) VALUES(@name,@provinceId,0) SELECT SCOPE_IDENTITY()";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@name", pDistrict.Name);
                c.AddParam("@provinceId", pDistrict.Province.Id);
                c.AddParam("@deleted", false);
                return int.Parse(c.ExecuteScalar().ToString());
            }
        }

        public bool UpdateDistrict(District pDistrict)
        {
            bool UpdateOk = false;
            try
            {
                const string q = "UPDATE [Districts] SET [name]=@name WHERE id=@id";
                using (SqlConnection conn = GetConnection())
                using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
                {
                    c.AddParam("@id", pDistrict.Id);
                    c.AddParam("@name", pDistrict.Name);
                    c.ExecuteNonQuery();
                    UpdateOk = true;
                }
            }
            catch (System.Exception ex)
            {
                throw ex; 
            }
            return UpdateOk;
        }

        public void DeleteDistrictById(int districtID)
        {
            const string q = "UPDATE [Districts]  SET [deleted]=1 WHERE id=@id";

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@id", districtID);
                c.ExecuteNonQuery();
            }
        }

        public int AddCity(City pCity)
        {
            const string q = "INSERT INTO [City] ([name], [district_id],[deleted]) VALUES (@name,@district_id,0) SELECT SCOPE_IDENTITY()";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@name", pCity.Name);
                c.AddParam("@district_id", pCity.DistrictId);
                c.AddParam("@deleted", pCity.Deleted);
                return int.Parse(c.ExecuteScalar().ToString());
            }

        }

        public bool UpdateCity(City pCity)
        {
                bool updateOk = false;
                try
                {
                    const string q = "UPDATE [City] SET [name]=@name WHERE id=@id";
                    using (SqlConnection conn = GetConnection())
                    using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
                    {
                        c.AddParam("@id", pCity.Id);
                        c.AddParam("@name", pCity.Name);
                        c.ExecuteNonQuery();
                        updateOk = true;
                    }
                }
                catch (System.Exception ex)
                {
                    throw ex;
                } 
               return updateOk;
            
        }

        public void DeleteCityById(int pCityId)
        {
            const string q = "UPDATE [City]  SET [deleted]=1 WHERE id=@id";

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@id", pCityId);
                c.ExecuteNonQuery();
            }
        }

       

        public List<District> SelectDistrictsByProvinceId(int pProvinceId)
        {
            List<District> districts = new List<District>();

            const string q = "SELECT Districts.id, Districts.name, Districts.province_id, " +
                                   "Provinces.id AS province_id, Provinces.name AS province_name " +
                                   "FROM Districts INNER JOIN " +
                                   "Provinces ON Districts.province_id = Provinces.id " +
                                   "WHERE Provinces.id= @id AND Districts.deleted = 0 ORDER BY Districts.name";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@id", pProvinceId);
                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (r != null)
                    {
                        while (r.Read())
                        {
                            District district = new District();
                            district.Province = new Province();
                            district.Id = r.GetInt("id");
                            district.Name = r.GetString("name");
                            district.Province.Id = r.GetInt("province_id");
                            district.Province.Name = r.GetString("province_name");
                            districts.Add(district);
                        }
                    }
                }
            }
            return districts;
        }

        public List<Province> SelectAllProvinces()
        {
            List<Province> provinces = new List<Province>();

            const string q = "SELECT id,name FROM Provinces ORDER BY name";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            using (OpenCbsReader r = c.ExecuteReader())
            {
                if (r != null)
                {
                    while (r.Read())
                    {
                        Province province = new Province
                                                {
                                                    Id = r.GetInt("id"),
                                                    Name = r.GetString("name")
                                                };
                        provinces.Add(province);
                    }
                }
            }
            
            return provinces;
        }

        public Province SelectProvinceByName(string name)
        {
            const string q = "SELECT id,name FROM Provinces WHERE name = @name";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@name", name);
                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (null == r || r.Empty) return null;
                    r.Read();
                    Province retval = new Province
                                          {
                                              Id = r.GetInt("id"),
                                              Name = r.GetString("name")
                                          };
                    return retval;
                }
            }
        }

        public List<City> SelectCityByDistrictId(int pDistrictId)
        {
            List<City> cities = new List<City>();

            const string q = "SELECT name, id FROM City WHERE district_id = @id and deleted = 0 ORDER BY name";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@id", pDistrictId);
                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (r != null)
                    {
                        while (r.Read())
                        {
                            City city = new City
                                            {
                                                Name = r.GetString("name"),
                                                Id = r.GetInt("id"),
                                                DistrictId = pDistrictId
                                            };
                            cities.Add(city);
                        }
                    }
                }
            }
            return cities;
        }

        public District SelectDistrictByCityName(string name)
        {
            string query = @"SELECT d.id district_id
	            , d.name district_name
	            , p.id province_id
	            , p.name province_name
            FROM dbo.Districts d
            INNER JOIN dbo.City c ON c.district_id = d.id
            INNER JOIN dbo.Provinces p ON p.id = d.province_id
            WHERE c.name LIKE N'%{0}%'";
            query = string.Format(query, name);
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand cmd = new OpenCbsCommand(query, conn))
            using (OpenCbsReader r = cmd.ExecuteReader())
            {
                if (null == r) return null;
                if (!r.Read()) return null;

                District retval = new District
                {
                    Id = r.GetInt("district_id"),
                    Name = r.GetString("district_name"),
                    Province = new Province
                    {
                        Id = r.GetInt("province_id"),
                        Name = r.GetString("province_name")
                    }
                };
                return retval;
            }
        }

        public bool IsDistrictUsed(int districtId)
        {
            const string query = "SELECT TOP 1 district_id FROM Tiers WHERE district_id = @districtId OR secondary_district_id = @districtId";
            using (var connection = GetConnection())
            using (var cmd = new OpenCbsCommand(query, connection))
            {
                cmd.AddParam("districtId", districtId);
                return cmd.ExecuteScalar() != null;
            }
        }

        public bool IsCityUsed(int cityId)
        {
            const string query = "SELECT c.id FROM City c " +
                                 "INNER JOIN Tiers t " +
                                 "    ON c.[name] = t.city OR c.[name] = t.secondary_city " +
                                 "WHERE c.id = @cityId";
            using (var connection = GetConnection())
            using (var cmd = new OpenCbsCommand(query, connection))
            {
                cmd.AddParam("cityId", cityId);
                return cmd.ExecuteScalar() != null;
            }            
        }
    }
}
