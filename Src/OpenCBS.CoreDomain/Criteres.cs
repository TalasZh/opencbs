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
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.FundingLines;

namespace OpenCBS.CoreDomain
{
    [Serializable]
    public class Criteres
    {
        public string name;
        public string city;
        public string distric;
        public string chaine = string.Empty;
        public int rows = 0;
        public int page=1;
        public int rowsPage=20;


        public void SplitterString()
        {
            if (chaine != string.Empty)
            {
                chaine.Replace("'", "");
                string delimeter = ",;=' ";
                char[] separateur = delimeter.ToCharArray();
                string[] split = chaine.Split(separateur);
                Dictionary<string, string> dico = new Dictionary<string, string>();
                int i = 0;
                string key = string.Empty;
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
                name = dico["name"];
                city = dico["city"];
                distric = dico["disctric"];
            }

        }
    }

          
}

