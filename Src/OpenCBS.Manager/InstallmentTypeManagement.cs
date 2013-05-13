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
using System.Data;
using System.Data.SqlClient;
using OpenCBS.CoreDomain;
using System.Collections;
using OpenCBS.Shared;

namespace OpenCBS.Manager
{
	/// <summary>
	/// Summary description for InstallmentTypeManagement.
	/// </summary>
	public class InstallmentTypeManagement : Manager
	{
        public InstallmentTypeManagement(User pUser) : base(pUser) { }

	    public InstallmentTypeManagement(string testDB) : base(testDB) {}

		public int AddInstallmentType(InstallmentType installmentType)
		{
			string sqlText = "INSERT INTO [InstallmentTypes]([name], [nb_of_days], [nb_of_months]) "+
				"VALUES(@name,@days,@months) SELECT @@IDENTITY";
			
			SqlCommand insert = new SqlCommand(sqlText,CurrentConnection);
			DatabaseHelper.InsertStringNVarCharParam("@name",insert,installmentType.Name);
			DatabaseHelper.InsertInt32Param("@days",insert,installmentType.NbOfDays);
			DatabaseHelper.InsertInt32Param("@months",insert,installmentType.NbOfMonths);

            return int.Parse(insert.ExecuteScalar().ToString());
		}

		/// <summary>
		/// This methods gives us all installmentType in database
		/// </summary>
		/// <returns>Collection of InstallmentType objects</returns>
        public List<InstallmentType> SelectAllInstallmentTypes()
		{
			string SqlText = "SELECT id, name, nb_of_days, nb_of_months FROM InstallmentTypes";

			SqlCommand selectInstallments = new SqlCommand(SqlText,CurrentConnection);

            using (SqlDataReader reader = selectInstallments.ExecuteReader())
			{
                List<InstallmentType> installmentTypeList = new List<InstallmentType>();

				while(reader.Read())
				{
					InstallmentType installmentType = new InstallmentType();
			
					installmentType.Id = DatabaseHelper.GetInt32("id",reader);
					installmentType.Name = DatabaseHelper.GetString("name",reader);
					installmentType.NbOfDays = DatabaseHelper.GetInt32("nb_of_days",reader);
					installmentType.NbOfMonths = DatabaseHelper.GetInt32("nb_of_months",reader);
			
					installmentTypeList.Add(installmentType);
				}
				return installmentTypeList;
			}
		}

		/// <summary>
		/// InstallmentType Finder by id
		/// </summary>
		/// <param name="instId">id searched</param>
		/// <returns></returns>
		public InstallmentType SelectInstallmentTypeById(int instId)
		{
			string SqlText = "SELECT id, name, nb_of_days, nb_of_months FROM InstallmentTypes WHERE id = @id";

			SqlCommand selectInstallments = new SqlCommand(SqlText,CurrentConnection);	

			selectInstallments.Parameters.Add("@id",SqlDbType.Int,4);
			selectInstallments.Parameters["@id"].Value = instId;

            using (SqlDataReader reader = selectInstallments.ExecuteReader())
			{
				if(reader.HasRows)
				{
					reader.Read();
			
					InstallmentType installmentType = new InstallmentType();
			
					installmentType.Id = DatabaseHelper.GetInt32("id",reader);
					installmentType.Name = DatabaseHelper.GetString("name",reader);
					installmentType.NbOfDays = DatabaseHelper.GetInt32("nb_of_days",reader);
					installmentType.NbOfMonths = DatabaseHelper.GetInt32("nb_of_months",reader);
			
					return installmentType;
				}
				else
				{
					return null;
				}
			}
		}
	}
}
