// LICENSE PLACEHOLDER

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
