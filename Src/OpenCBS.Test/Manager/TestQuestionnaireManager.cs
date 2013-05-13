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
