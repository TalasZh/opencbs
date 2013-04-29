using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace Octopus.Test.Shared
{
    [TestFixture]
    public class TestValidateUpdateDataBase
    {
            
        [Test]
        public void TestApplyRegularExpressions0()
        {
            string fileName = "v1.10.9.sql";
            Regex regExp = new Regex(@"\D+(\d+)\.(\d+)\.(\d+)\.sql");
            Match match = regExp.Match(fileName);

            
            Assert.IsNotNull(match);
            Assert.IsTrue(match.Groups[1].ToString()=="1");
            Assert.IsTrue(match.Groups[2].ToString() =="10");
            Assert.IsTrue(match.Groups[3].ToString() =="9");
        }
        [Test]
        public void TestApplyRegularExpressions1()
        {
            string fileName = @"D:\Octopus\DatabaseUpdate\CreateDatabase_v1.0.10.sql";
            Regex regExp = new Regex(@"\D+(\d+)\.(\d+)\.(\d+)\.sql");
            Match match = regExp.Match(fileName);
            
            Assert.IsTrue(match.Groups[1].ToString() == "1");
            Assert.IsTrue(match.Groups[2].ToString() == "0");
            Assert.IsTrue(match.Groups[3].ToString() == "10");
            Assert.IsNotNull(match);
        }
        [Test]
        public void TestApplyRegularExpressions2()
        {
            string fileName = "Database_Update_v1.0.1_to_v1.0.2.sql";
            Regex regExp = new Regex(@"\D+(\d+)\.(\d+)\.(\d+).*(\d+)\.(\d+)\.(\d+)\.sql");
            Match match = regExp.Match(fileName);
            
            Assert.IsTrue(match.Groups[1].ToString() == "1");
            Assert.IsTrue(match.Groups[2].ToString() == "0");
            Assert.IsTrue(match.Groups[3].ToString() == "1");
            Assert.IsTrue(match.Groups[4].ToString() == "1");
            Assert.IsTrue(match.Groups[5].ToString() == "0");
            Assert.IsTrue(match.Groups[6].ToString() == "2");
        }
        [Test]
        public void TestApplyRegularExpressions4()
        {
            string fileName = "CreateDatabase_v1.10.9.sql";
            Regex regExp = new Regex(@"\D+(\d+)\.(\d+)\.(\d+)\.sql");
            Match match = regExp.Match(fileName);

            
            Assert.IsTrue(match.Groups[1].ToString() == "1");
            Assert.IsTrue(match.Groups[2].ToString() == "10");
            Assert.IsTrue(match.Groups[3].ToString() == "9");
        }

        private bool SearchRegEx(string s)
        {

            Regex regExp = new Regex(@"\D+(\d+)\.(\d+)\.(\d+)\.sql");
            Match match = regExp.Match(s);
            
            bool trouve=false;
            if (match.Success)
                trouve = true;
            return trouve;

        }
        private bool SearchRegEx(string s,string reg)
        {
            
            Regex regExp = new Regex(reg);
            Match match = regExp.Match(s);

            bool trouve = false;
            if (match.Success)
                trouve = true;
            return trouve;

        }
        private bool SearchSubstring(string s, string name)
        {
            bool trouve = false;
            if (name.Contains(s))
                trouve = true;

            return trouve;

        }
       
    }
   
}
