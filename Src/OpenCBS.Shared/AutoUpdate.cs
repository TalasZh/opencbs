//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 
using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using Octopus.Shared.Settings;

namespace Octopus.Shared
{
    [Serializable]
    public class AutoUpdate
    {
        private static readonly string VERSION = "<version>";
        private static readonly string PING_TEST_WEB_SITE = "www.octopusnetwork.org";
        private static readonly string PING_WEBSERVICE = "http://www.octopusnetwork.org/services/version.aspx";

        /// <summary>
        /// Checks available version.
        /// </summary>
        public static string CheckForNewVersion()
        {
            try
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send(PING_TEST_WEB_SITE);
                if (reply.Status == IPStatus.Success)
                {
                    string httpQuery = PING_WEBSERVICE + "?v=" + TechnicalSettings.SoftwareVersion;
                    WebRequest web = System.Net.HttpWebRequest.Create(httpQuery);
                    WebResponse response = web.GetResponse();
                    StreamReader sr = new StreamReader(response.GetResponseStream());
                    string xml = sr.ReadToEnd();
                    response.Close();
                    if (xml.Contains(VERSION))
                    {
                        int a = xml.IndexOf(VERSION);
                        int b = xml.IndexOf("</version>");
                        string version = xml.Substring(a + VERSION.Length, b - a - VERSION.Length);
                        if (TechnicalSettings.IsThisVersionNewer(version))
                        {
                            return version;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("AppMain.PingWeb failed. \n" + ex.Message);
            }
            return null;
        }
    }
}
