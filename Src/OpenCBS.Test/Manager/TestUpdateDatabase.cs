// LICENSE PLACEHOLDER

using OpenCBS.Shared.FilesSearch;
using NUnit.Framework;
using OpenCBS.Manager;

namespace OpenCBS.Test.Manager
{
    [TestFixture]
    public class TestUpdateDatabase:BaseUpdateDatabaseTest
    {
        [Test]
        public void TestCreateDatabase()
        {

            
            DatabaseManager manager = (DatabaseManager)container["DatabaseManager"];

            //Recherche Fichier Schema
            string current;
            string expected;
            string dropview;
           

            //variable resulat query
            string resultat_req_create_sql_n_1;
            string resultat_req_create_sql_n;
            string resultat_req_update_sql;

            //variable resultat create
            string req_create_sql_n_1;
            string req_create_sql_n;
            string req_update_sql;
            string req_delete_view;

            //variable key
            string key_create_sql_n_1;
            string key_create_sql_n;
            string key_update_sql;
            

        

            QueryXML qXML=new QueryXML(ConfigSettings.GetInstance().GetDirectory);
            current = qXML.FindValueOfFileXML("upgrade", "current", UPGRADE_SCHEMA_FILE_NAME);
            expected = qXML.FindValueOfFileXML("upgrade", "expected", UPGRADE_SCHEMA_FILE_NAME);

            QueryFilesSQL qSQLCreateN_1 = new QueryFilesSQL(ConfigSettings.GetInstance().GetDirectory);
            QueryFilesSQL qSQLUpdateN= new QueryFilesSQL(ConfigSettings.GetInstance().GetDirectory);
            QueryFilesSQL qSQLDelete=new QueryFilesSQL(ConfigSettings.GetInstance().GetDirectory);
            


            key_create_sql_n_1 = string.Concat(CREATE_DATABASE, current);
            key_create_sql_n = string.Concat(CREATE_DATABASE, expected);
            key_update_sql = string.Concat(UPDATE_DATABASE, current, AND, expected);
            

            req_create_sql_n_1 = qSQLCreateN_1.SelectedFileInDirectoryToString(key_create_sql_n_1);
            req_update_sql = qSQLUpdateN.SelectedFileInDirectoryToString(key_update_sql);
            req_delete_view=qSQLDelete.SelectedFileInDirectoryToString(DROP_VIEW_TABLE);







            
            Assert.IsNotNull(manager);
            //Creation Base N-1
            bool success=true;
            int querieCount=0;
            string error="";
            string failedQuery="";

            manager.ExecuteScript(req_delete_view, false, ref success, ref querieCount, ref error, ref failedQuery, null);
            Assert.IsTrue(success);

            manager.ExecuteScript(req_create_sql_n_1, false, ref success, ref querieCount, ref error, ref failedQuery, null);
            Assert.IsTrue(success);

            manager.ExecuteScript(req_update_sql, false, ref success, ref querieCount, ref error, ref failedQuery, null);
            Assert.IsTrue(success);




            
          


            


            




            
            //Attribute Current et Old et  version N-1 et N
            //Selection SQL N-1 Create
            //Execute SQL N-1 Create
            //Execute SQL N Update
            //Chargement du schema N en base 
            //Comparaison avec le shema de base de donnee commitee




        }
    }
}
