// LICENSE PLACEHOLDER

using System.Data;
using System.Data.SqlClient;
using OpenCBS.CoreDomain;
using OpenCBS.DatabaseConnection;
using OpenCBS.Manager;
using NUnit.Framework;

namespace OpenCBS.Test.Manager
{
    public class TestQuestionnaireManager
    {
        [TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
		}

		[SetUp]
		public void SetUp()
		{
		}

        [Test]
        public void SaveQuestionnaire()
        {
            QuestionnaireManager qm = new QuestionnaireManager(DataUtil.TESTDB);
            SqlTransaction transaction = ConnectionManager.GetInstance().GetSqlTransaction(User.CurrentUser.Md5);

            SqlCommand cmdDelete = new SqlCommand("DELETE FROM Questionnaire", transaction.Connection, transaction);
            cmdDelete.ExecuteNonQuery();

            qm.SaveQuestionnaire("Test", "UK", "ww@xx.cc", "1", "", "yes", " ", "test", "test", "test", "test", transaction, false);
            transaction.Commit();
            MyInformation myInformation = qm.GetQuestionnaire();
            Assert.AreEqual("Test", myInformation.MfiName);
        }
		
    }
}
