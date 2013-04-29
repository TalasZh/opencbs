using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Octopus.Test.Shared
{
    [TestFixture]
    public class CommonTest
    {
        public string name;
        public string district;
        [Test]
        public void SplitterString()
        {
            string chaine = "name=Natexis code=1";
            string delimeter = ",;=' ";
            char [] separateur = delimeter.ToCharArray();
            string [] split = chaine.Split(separateur);

            Assert.IsTrue(split.Length > 1);
            Dictionary<string, string> dico = new Dictionary<string, string>();
            int i = 0;
            string key=string.Empty;
            foreach (string s in split)
            {
                if (i == 0)
                {
                    key = s;
                    i++;
                }
                else
                {
                    dico.Add(key, s);
                    key = string.Empty;
                    i = 0;
                  
                }
            }
            Assert.IsTrue(dico.Count > 0);

            name = dico["name"];
            district = dico["code"];
            Assert.IsTrue(name != string.Empty);
            

        }
        [Test]
        public void TryToParseFromIntToString()
        {
            string str="12";
            int resultat;
            bool accept=int.TryParse(str, out resultat);
            Assert.AreEqual(resultat,12);
            Assert.IsTrue(accept==true);
           


        }
    }
}
