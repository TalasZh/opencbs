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
using System.Collections.Generic;
using NUnit.Framework;

namespace OpenCBS.Test.Shared
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
