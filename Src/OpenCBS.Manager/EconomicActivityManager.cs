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
using OpenCBS.CoreDomain;
using System.Data.SqlClient;
using OpenCBS.CoreDomain.EconomicActivities;

namespace OpenCBS.Manager
{
	/// <summary>
    /// Summary description for EconomicActivityManager.
	/// </summary>
	public class EconomicActivityManager : Manager
	{
		public EconomicActivityManager(string testDB) : base(testDB) {}

        public EconomicActivityManager(User pUser) : base(pUser) {}

		/// <summary>
		/// Add an economic activity in database
		/// </summary>
        /// <param name="pEconomicActivity">the economic activity object to add</param>
        /// <returns>the id of the economic activity added</returns>
        public int AddEconomicActivity(EconomicActivity pEconomicActivity)
		{
            const string sqlText = @"INSERT INTO EconomicActivities ([name] , [parent_id] , [deleted]) 
                        VALUES (@name,@parentId,@deleted) SELECT SCOPE_IDENTITY()";

		    using (SqlConnection connection = GetConnection())
            using (OpenCbsCommand insert = new OpenCbsCommand(sqlText, connection))
		    {
                insert.AddParam("@name", pEconomicActivity.Name);
                insert.AddParam("@deleted", pEconomicActivity.Deleted);
		        if (pEconomicActivity.Parent != null)
                    insert.AddParam("@parentId", pEconomicActivity.Parent.Id);
                else
                    insert.AddParam("@parentId", null);
                return int.Parse(insert.ExecuteScalar().ToString());
            }
		}
		
		/// <summary>
		/// This methods allows us to find all domains of application
		/// </summary>
		/// <returns>hierarchic collection of DomainOfApplication
		/// </returns>
		public List<EconomicActivity> SelectAllEconomicActivities()
		{
		    List<EconomicActivity> doaList = new List<EconomicActivity>();

            const string sqlText = "SELECT id FROM EconomicActivities WHERE parent_id IS NULL AND deleted = 0";

            using (SqlConnection connection = GetConnection())
            using (OpenCbsCommand selectAll = new OpenCbsCommand(sqlText, connection))
            {
                using (OpenCbsReader reader = selectAll.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        EconomicActivity domain = new EconomicActivity
                                                      {Id = reader.GetInt("id")};
                        doaList.Add(domain);
                    }
                }
            }

            for (int i = 0; i < doaList.Count; i++)
                doaList[i] = SelectEconomicActivity(doaList[i].Id);

            return doaList;
		}

		/// <summary>
		/// Update economic activity name and delete
		/// </summary>
        /// <param name="pEconomicActivity">EconomicActivity object</param>
        public void UpdateEconomicActivity(EconomicActivity pEconomicActivity)
		{
            const string sqlText = "UPDATE EconomicActivities SET name = @name,deleted = @wasDeleted WHERE id = @id";

            using (SqlConnection connection = GetConnection())
            using (OpenCbsCommand update = new OpenCbsCommand(sqlText, connection))
		    {
                update.AddParam("@id", pEconomicActivity.Id);
                update.AddParam("@name",  pEconomicActivity.Name);
                update.AddParam("@wasDeleted", pEconomicActivity.Deleted);
		        update.ExecuteNonQuery();
		    }
		}

	    private List<EconomicActivity> SelectChildren(int pParentId)
		{
            List<EconomicActivity> doaList = new List<EconomicActivity>();

            const string sqlText = "SELECT id, name, deleted FROM EconomicActivities WHERE parent_id = @id AND deleted = 0";

            using (SqlConnection connection = GetConnection())
            using (OpenCbsCommand sqlCommand = new OpenCbsCommand(sqlText, connection))
            {
                sqlCommand.AddParam("@id", pParentId);
                using (OpenCbsReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        EconomicActivity domain = new EconomicActivity
                                                      {
                                                          Id = reader.GetInt("id"),
                                                          Name = reader.GetString("name"),
                                                          Deleted = reader.GetBool("deleted")
                                                      };
                        doaList.Add(domain);
                    }
                }
            }
	        for (int i = 0; i < doaList.Count; i++)
                doaList[i].Childrens = SelectChildren(doaList[i].Id);

			return doaList;
		}

        public bool ThisActivityAlreadyExist(string pName, int pParentId)
        {
            int id = 0;
            const string sqlText = @"SELECT id, name, deleted FROM EconomicActivities WHERE parent_id = @id 
                    AND name = @name AND deleted = 0";
            using (SqlConnection connection = GetConnection())
            using (OpenCbsCommand sqlCommand = new OpenCbsCommand(sqlText, connection))
            {
                sqlCommand.AddParam("@name", pName);
                sqlCommand.AddParam("@id", pParentId);

                using (OpenCbsReader reader = sqlCommand.ExecuteReader())
                {
                    if (!reader.Empty)
                    {
                        reader.Read();
                        id = reader.GetInt("id");
                    }
                }
            }
            return id != 0;
        }

        /// <summary>
        /// This methods allows us to find a economic activity.
        /// We use recursive method to find parent
        /// </summary>
        /// <param name="id">the id searched</param>
        /// <returns>DomainOfApplication object</returns>
        public EconomicActivity SelectEconomicActivity(int pId)
        {
            EconomicActivity doa;

            const string sqlText = @"SELECT [id], [name], [deleted] 
                                     FROM EconomicActivities 
                                     WHERE id = @id";
            using (SqlConnection connection = GetConnection())
            using (OpenCbsCommand selectById = new OpenCbsCommand(sqlText, connection))
            {
                selectById.AddParam("@id", pId);
                using (OpenCbsReader reader = selectById.ExecuteReader())
                {
                    doa = GetEconomicActivity(reader);
                }
            }

            doa.Childrens = SelectChildren(doa.Id);
            return doa;
        }

        private static EconomicActivity GetEconomicActivity(OpenCbsReader pReader)
        {
            EconomicActivity doa = new EconomicActivity();
            if (pReader != null)
            {
                if (!pReader.Empty)
                {
                    pReader.Read();
                    doa.Id = pReader.GetInt("id");
                    doa.Name = pReader.GetString("name");
                    doa.Deleted = pReader.GetBool("deleted");
                }
            }
            return doa;
        }

	    public EconomicActivity SelectEconomicActivity(string pName)
	    {
            EconomicActivity doa;

            const string sqlText = "SELECT id, name, deleted FROM EconomicActivities WHERE name = @name";
            using (SqlConnection connection = GetConnection())
            using (OpenCbsCommand selectById = new OpenCbsCommand(sqlText, connection))
            {
                selectById.AddParam("@name", pName);
                using (OpenCbsReader reader = selectById.ExecuteReader())
                {
                    doa = GetEconomicActivity(reader);
                }
            }

            doa.Childrens = SelectChildren(doa.Id);
            return doa;
	    }

        public void AddEconomicActivityLoanHistory(EconomicActivityLoanHistory activityLoanHistory, SqlTransaction sqlTransaction)
        {
            const string sqlText = @"INSERT INTO EconomicActivityLoanHistory 
                                    ([contract_id],[person_id],[group_id],[economic_activity_id],[deleted]) 
                                    VALUES (@contract_id, @person_id, @group_id, @economic_activity_id, @deleted)";

            using (OpenCbsCommand insert = new OpenCbsCommand(sqlText, sqlTransaction.Connection, sqlTransaction))
            {
                insert.AddParam("@contract_id",  activityLoanHistory.Contract.Id);
                insert.AddParam("@person_id",  activityLoanHistory.Person.Id);
                if (activityLoanHistory.Group != null)
                    insert.AddParam("@group_id", activityLoanHistory.Group.Id);
                else
                    insert.AddParam("@group_id", null);
                insert.AddParam("@economic_activity_id", activityLoanHistory.EconomicActivity.Id);
                insert.AddParam("@deleted",  activityLoanHistory.Deleted);

                insert.ExecuteNonQuery();
            }
        }

        public void UpdateDeletedEconomicActivityLoanHistory(int contractId, int personId, int economicActivityId,
            SqlTransaction sqlTransaction, bool deleted)
        {
            const string sqlText = @"UPDATE EconomicActivityLoanHistory 
                                    SET deleted = @deleted, economic_activity_id = @economic_activity_id 
                                    WHERE contract_id = @contract_id AND person_id = @person_id";

            using (OpenCbsCommand update = new OpenCbsCommand(sqlText, sqlTransaction.Connection, sqlTransaction))
            {
                update.AddParam("@contract_id",  contractId);
                update.AddParam("@person_id",  personId);
                update.AddParam("@economic_activity_id",  economicActivityId);
                update.AddParam("@deleted",  deleted);
                update.ExecuteNonQuery();
            }
        }

        public bool EconomicActivityLoanHistoryExists(int contractId, int personId, SqlTransaction sqlTransaction)
        {
            int id = 0;
            const string sqlText = @"SELECT contract_id, person_id, group_id, economic_activity_id, deleted 
                                     FROM EconomicActivityLoanHistory 
                                     WHERE contract_id = @contract_id AND person_id = @person_id ";

            using (OpenCbsCommand sqlCommand = new OpenCbsCommand(sqlText, sqlTransaction.Connection, sqlTransaction))
            {
                sqlCommand.AddParam("@contract_id",  contractId);
                sqlCommand.AddParam("@person_id",  personId);

                using (OpenCbsReader reader = sqlCommand.ExecuteReader())
                {
                    if (!reader.Empty)
                    {
                        reader.Read();
                        id = reader.GetInt("contract_id");
                    }
                }
            }
            return id != 0;
        }

        public bool EconomicActivityLoanHistoryDeleted(int contractId, int personId, SqlTransaction sqlTransaction)
        {
            int id = 0;
            const string sqlText = @"SELECT contract_id, person_id, group_id, economic_activity_id, deleted 
                                     FROM EconomicActivityLoanHistory 
                                     WHERE contract_id = @contract_id AND person_id = @person_id AND deleted = 1";

            using (OpenCbsCommand sqlCommand = new OpenCbsCommand(sqlText, sqlTransaction.Connection, sqlTransaction))
            {
                sqlCommand.AddParam("@contract_id",  contractId);
                sqlCommand.AddParam("@person_id",  personId);

                using (OpenCbsReader reader = sqlCommand.ExecuteReader())
                {
                    if (!reader.Empty)
                    {
                        reader.Read();
                        id = reader.GetInt("contract_id");
                    }
                }
            }
            return id != 0;
        }

	}
}
