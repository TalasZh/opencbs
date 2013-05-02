// LICENSE PLACEHOLDER

using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Shared
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
