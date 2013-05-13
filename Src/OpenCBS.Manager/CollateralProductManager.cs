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
using System.Data.SqlClient;
using System.Collections.Generic;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Products.Collaterals;
using OpenCBS.Enums;

namespace OpenCBS.Manager
{
    /// <summary>
    /// This class provides all the methods required to manages Package datas in database
    /// </summary>
    public class CollateralProductManager : Manager
    {

        public CollateralProductManager(User pUser) : base(pUser)
        {
        }

        public CollateralProductManager(string testDB) : base(testDB)
        {
        }

        /// <summary>
        /// Method to add a package into database. We use the NullableTypes to make the correspondance between
        /// nullable int, decimal and double types in database and our own objects
        /// </summary>
        /// <param name="colProduct">Package Object</param>
        /// <returns>The id of the package which has been added</returns>
        public int AddCollateralProduct(CollateralProduct colProduct)
        {
            string sqlText = @"INSERT INTO [CollateralProducts] 
                                (
                                    [name]
                                    ,[desc]
                                    ,[deleted]
                                ) 
                                VALUES 
                                (
                                    @name
                                    ,@desc
                                    ,@deleted
                                 ) 
                                 SELECT CONVERT(int, SCOPE_IDENTITY())";

            using (SqlConnection connection = GetConnection())
            using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, connection))
            {
                cmd.AddParam("@name", colProduct.Name);
                cmd.AddParam("@desc", colProduct.Description);
                cmd.AddParam("@deleted", colProduct.Delete);
                colProduct.Id = int.Parse(cmd.ExecuteScalar().ToString());
            }

            foreach (CollateralProperty collateralProperty in colProduct.Properties)
                AddCollateralProperty(colProduct.Id, collateralProperty);

            return colProduct.Id;
        }

        public int AddCollateralProperty(int collateralProductId, CollateralProperty collateralProperty)
        {
            string sqlText = @"INSERT INTO [CollateralProperties] 
                                        (
                                         [product_id]
                                        ,[type_id]
                                        ,[name]
                                        ,[desc]
                                        )
 
                               VALUES 
                                        (
                                         @product_id
                                        ,@type_id
                                        ,@prop_name
                                        ,@prop_desc
                                        )
                             SELECT CONVERT(int, SCOPE_IDENTITY())";

            using (SqlConnection connection = GetConnection())
            using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, connection))
            {
                cmd.AddParam("@product_id", collateralProductId);
                cmd.AddParam("@prop_name", collateralProperty.Name);
                cmd.AddParam("@prop_desc", collateralProperty.Description);
                cmd.AddParam("@type_id", (int)Enum.Parse(typeof(OCollateralPropertyTypes), collateralProperty.Type.ToString()));
                collateralProperty.Id = int.Parse(cmd.ExecuteScalar().ToString());

                if (collateralProperty.Type == OCollateralPropertyTypes.Collection)
                    foreach (string colItem in collateralProperty.Collection)
                        AddCollateralPropertyCollectionItem(collateralProperty.Id, colItem);
            }

            return collateralProperty.Id;
        }

        public void AddCollateralPropertyCollectionItem(int collateralPropertyId, string colItem)
        {
            string sqlListText = @"INSERT INTO [CollateralPropertyCollections] 
                                                ([property_id]
                                                ,[value]) 
                                   VALUES 
                                            (@property_id
                                            , @col_item)";

            using (SqlConnection connection = GetConnection())
            using (OpenCbsCommand cmd = new OpenCbsCommand(sqlListText, connection))
            {
                cmd.AddParam("@property_id", collateralPropertyId);
                cmd.AddParam("@col_item", colItem);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Select all packages in database
        /// </summary>
        /// <param name="pShowAlsoDeleted"></param>
        /// <returns>a list contains all packages</returns>
        public List<CollateralProduct> SelectAllCollateralProducts(bool pShowAlsoDeleted)
        {
            List<CollateralProduct> packagesList = new List<CollateralProduct>();
            string sqlText = @"SELECT id 
                               FROM CollateralProducts 
                               WHERE 1 = 1";

            if (!pShowAlsoDeleted)
                sqlText += " AND deleted = 0";

            using (SqlConnection connection = GetConnection())
            using (OpenCbsCommand selectPackages = new OpenCbsCommand(sqlText, connection))
            {
                using (OpenCbsReader reader = selectPackages.ExecuteReader())
                {
                    if (reader.Empty) return new List<CollateralProduct>();
                    while (reader.Read())
                    {
                        CollateralProduct pack = new CollateralProduct { Id = reader.GetInt("id") };
                        packagesList.Add(pack);
                    }
                }
            }
            for (int i = 0; i < packagesList.Count; i++)
            {
                packagesList[i] = SelectCollateralProduct(packagesList[i].Id);
            }
            return packagesList;
        }

        /// <summary>
        /// This method allows us to select a package from database.  We use the NullableTypes to make the correspondance between
        /// nullable int, decimal and double types in database and our own objects
        /// </summary>
        /// <param name="colProductId">id's of package searched</param>
        /// <returns>A package Object if id matches with datas in database, null if not</returns>
        public CollateralProduct SelectCollateralProduct(int colProductId)
        {
            const string sqlText = @"SELECT 
                                             [name]
                                            ,[desc]
                                            ,[deleted]
                                    FROM CollateralProducts 
                                    WHERE id = @id";

            CollateralProduct colProduct = new CollateralProduct();
            using (SqlConnection connection = GetConnection())
            using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, connection))
            {
                cmd.AddParam("@id", colProductId);
                using (OpenCbsReader reader = cmd.ExecuteReader())
                {
                    if (reader.Empty) return null;
                    reader.Read();
                    colProduct.Id = colProductId;
                    colProduct.Name = reader.GetString("name");
                    colProduct.Description = reader.GetString("desc");
                    colProduct.Delete = reader.GetBool("deleted");
                    reader.Dispose();
                }
            }

            List<CollateralProperty> properties = new List<CollateralProperty>();
                    const string sqlPropertyText = @"SELECT 
                                                             id
                                                            ,type_id
                                                            ,[name]
                                                            ,[desc]
                                                     FROM CollateralProperties 
                                                     WHERE product_id = @product_id";

            using (SqlConnection connection = GetConnection())
            using (OpenCbsCommand cmd = new OpenCbsCommand(sqlPropertyText, connection))
            {
                cmd.AddParam("@product_id", colProduct.Id);
                using (OpenCbsReader reader = cmd.ExecuteReader())
                {
                    if (reader.Empty) return null;

                    while (reader.Read())
                    {
                        CollateralProperty collateralProperty = new CollateralProperty();
                        collateralProperty.Id = reader.GetInt("id");
                        collateralProperty.Type = (OCollateralPropertyTypes)Enum.ToObject(typeof(OCollateralPropertyTypes), 
                            reader.GetInt("type_id"));
                        collateralProperty.Name = reader.GetString("name");
                        collateralProperty.Description = reader.GetString("desc");

                        if (collateralProperty.Type == OCollateralPropertyTypes.Collection)
                        {
                            List<string> propertyList = new List<string>();
                            const string sqlListText = @"SELECT [value] 
                                                         FROM CollateralPropertyCollections 
                                                         WHERE property_id = @property_id";
                            using (SqlConnection conn = GetConnection())
                            using (OpenCbsCommand selectList = new OpenCbsCommand(sqlListText, conn))
                            {
                                selectList.AddParam("@property_id", collateralProperty.Id);
                                using (OpenCbsReader listReader = selectList.ExecuteReader())
                                {
                                    if (listReader.Empty) return null;

                                    while (listReader.Read())
                                    {
                                        propertyList.Add(listReader.GetString("value"));
                                    }
                                    collateralProperty.Collection = propertyList;
                                }
                            }
                        }
                        properties.Add(collateralProperty);
                    }
                    colProduct.Properties = properties;
                }
            }

            return colProduct;
        }
        

        public CollateralProduct SelectCollateralProductByPropertyId(int propertyId)
        {
            int productId;
            const string sqlProductIdText = @"SELECT product_id 
                                             FROM [CollateralProperties] 
                                             WHERE id = @id ";

           using (SqlConnection connection = GetConnection())
           using (OpenCbsCommand cmd = new OpenCbsCommand(sqlProductIdText, connection))
            {
                cmd.AddParam("@id", propertyId);
                using (OpenCbsReader reader = cmd.ExecuteReader())
                {
                    if (reader.Empty) return null; // nothing is coming... (c)
                    reader.Read();
                    productId = reader.GetInt("product_id");
                }
            }
            
            const string sqlText = @"SELECT 
                                     [name]
                                    ,[desc]
                                    ,[deleted] 
                                    FROM CollateralProducts 
                                    WHERE id = @id";

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand selectProduct = new OpenCbsCommand(sqlText, conn))
            {
                selectProduct.AddParam("@id", productId);
                using (OpenCbsReader reader = selectProduct.ExecuteReader())
                {
                    if (reader.Empty) return null;
                    reader.Read();

                    CollateralProduct colProduct = new CollateralProduct
                        {
                            Id = productId,
                            Name = reader.GetString("name"),
                            Description = reader.GetString("desc"),
                            Delete = reader.GetBool("deleted")
                        };

                    return colProduct;
                }
            }
        }

        public CollateralProperty SelectCollateralProperty(int propertyId)
        {
            const string sqlPropertyText = @"SELECT 
                                                  [type_id]
                                                , [name]
                                                , [desc]
                                             FROM CollateralProperties 
                                             WHERE id = @id";

            CollateralProperty collateralProperty = new CollateralProperty();

            using (SqlConnection connection = GetConnection())
            using (OpenCbsCommand selectProperty = new OpenCbsCommand(sqlPropertyText, connection))
            {
                selectProperty.AddParam("@id", propertyId);
                using (OpenCbsReader propertyReader = selectProperty.ExecuteReader())
                {
                    if (propertyReader.Empty) return null; // nothing is coming! (c)
                    propertyReader.Read();

                    collateralProperty.Id = propertyId;
                    collateralProperty.Type = (OCollateralPropertyTypes)Enum.ToObject(typeof(OCollateralPropertyTypes),
                        propertyReader.GetInt("type_id"));
                    collateralProperty.Name = propertyReader.GetString("name");
                    collateralProperty.Description = propertyReader.GetString("desc");

                    if (collateralProperty.Type == OCollateralPropertyTypes.Collection)
                    {
                        List<string> propertyList = new List<string>();
                        const string sqlListText = @"SELECT [value] 
                                                     FROM CollateralPropertyCollections 
                                                     WHERE property_id = @property_id";

                        using (SqlConnection conn = GetConnection())
                        using (OpenCbsCommand selectList = new OpenCbsCommand(sqlListText, conn))
                        {
                            selectList.AddParam("@property_id", collateralProperty.Id);
                            using (OpenCbsReader listReader = selectList.ExecuteReader())
                            {
                                if (listReader.Empty) return null;

                                while (listReader.Read())
                                {
                                    propertyList.Add(listReader.GetString("value"));
                                }

                                collateralProperty.Collection = propertyList;
                            }
                        }
                    }
                }
            }

            return collateralProperty;
        }

        public void DeleteCollateralProduct(int colProductId)
        {
            const string sqlText = @"UPDATE CollateralProducts SET deleted = 1 
                                    WHERE [id] = @id";

            using (SqlConnection connection = GetConnection())
            using (OpenCbsCommand command = new OpenCbsCommand(sqlText, connection))
            {
                command.AddParam("@id", colProductId);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateCollateralProduct(int productId, string name, string description)
        {
            const string sqlText = @"UPDATE [CollateralProducts] 
                                     SET [name] = @name, 
                                         [desc] = @desc 
                                    WHERE id = @product_id";
            using (SqlConnection connection  = GetConnection())
            using (OpenCbsCommand updateProduct = new OpenCbsCommand(sqlText, connection))
            {
                updateProduct.AddParam("@product_id", productId);
                updateProduct.AddParam("@name", name);
                updateProduct.AddParam("@desc", description);
                updateProduct.ExecuteNonQuery();
            }
        }

        public bool PorductExists(string productName)
        {
            const string sqlText = @"SELECT 1 FROM [CollateralProducts] WHERE [name] = @name";
            using (var connection  = GetConnection())
            using (var cmd = new OpenCbsCommand(sqlText, connection))
            {
                cmd.AddParam("name", productName);
                return cmd.ExecuteScalar() != null;
            }
        }
    }
}
