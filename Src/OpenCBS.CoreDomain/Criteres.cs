// LICENSE PLACEHOLDER

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

