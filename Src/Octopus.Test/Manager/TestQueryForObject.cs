using System;
using System.Collections.Generic;
using System.Text;
using Octopus.Manager.QueryForObject;
using NUnit.Framework;
using Octopus.Shared;

namespace Octopus.Test.Manager
{
    [TestFixture]
    public class TestQueryForObject
    {
        [Test]
        public void TestConstructSQL()
        {
            string search = "ASSDER,DER,1";
            string SELECT_FROM_PROJET_ = @" SELECT TOP 100 percent Contracts.id , Contracts.contract_code, Contracts.start_date, Contracts.close_date,Persons.identification_data as identification_data,
                            Credit.amount, Credit.loanofficer_id,Tiers.client_type_code, ISNULL(Users.first_name + SPACE(1) + Users.last_name,
                            Users.user_name) AS user_name ,ISNULL(Groups.name,Persons.first_name + SPACE(1) + Persons.last_name) AS client_name ,
                            ISNULL((SELECT TOP 1 Persons.first_name + SPACE(1) + Persons.last_name FROM Persons WHERE Persons.id=Credit.loanofficer_id),'-') as loanofficer_name
                            FROM Contracts INNER JOIN Credit ON Contracts.id = Credit.id 
	                        INNER JOIN Projects ON Contracts.project_id = Projects.id
	                        INNER JOIN Tiers ON Projects.tiers_id = Tiers.id 
                            INNER JOIN Users ON Users.id = Credit.loanofficer_id LEFT OUTER JOIN Persons ON Tiers.id = Persons.id LEFT OUTER JOIN 
                            Groups ON Tiers.id = Groups.id ) maTable ";

            string CloseWhere = @" WHERE (contract_code LIKE %@contractCode% OR client_name LIKE %@clientName% OR user_name LIKE %@userName% OR identification_data LIKE %@numberPasseport% OR loanofficer_name LIKE %@loanofficerName% )) maTable";
            QueryEntity q = new QueryEntity(search, SELECT_FROM_PROJET_, CloseWhere);
            string resultat = q.ConstructSQLEntityNumberProxy();
           
             
            Assert.IsTrue(q.DynamiqParameters().Count > 0);

        }
        [Test]
        public void TestConstructSQLByCriteres()
        {
            string search = "ASSDER,DER,1";
            string SELECT_FROM_PROJET_ = @" SELECT TOP 100 percent Contracts.id , Contracts.contract_code, Contracts.start_date, Contracts.close_date,Persons.identification_data as identification_data,
                            Credit.amount, Credit.loanofficer_id,Tiers.client_type_code, ISNULL(Users.first_name + SPACE(1) + Users.last_name,
                            Users.user_name) AS user_name ,ISNULL(Groups.name,Persons.first_name + SPACE(1) + Persons.last_name) AS client_name ,
                            ISNULL((SELECT TOP 1 Persons.first_name + SPACE(1) + Persons.last_name FROM Persons WHERE Persons.id=Credit.loanofficer_id),'-') as loanofficer_name
                            FROM Contracts INNER JOIN Credit ON Contracts.id = Credit.id 
	                        INNER JOIN Projects ON Contracts.project_id = Projects.id
	                        INNER JOIN Tiers ON Projects.tiers_id = Tiers.id 
                            INNER JOIN Users ON Users.id = Credit.loanofficer_id LEFT OUTER JOIN Persons ON Tiers.id = Persons.id LEFT OUTER JOIN 
                            Groups ON Tiers.id = Groups.id ) maTable ";

            string CloseWhere = @" WHERE (contract_code LIKE %@contractCode% OR client_name LIKE %@clientName% OR user_name LIKE %@userName% OR identification_data LIKE %@numberPasseport% OR loanofficer_name LIKE %@loanofficerName% )) maTable";
            QueryEntity q = new QueryEntity(search, SELECT_FROM_PROJET_, CloseWhere);
            string resultat = q.ConstructSQLEntityByCriteresProxy(15, 0);
           
            Assert.IsTrue(q.DynamiqParameters().Count > 0);
        }
      
    }
}
