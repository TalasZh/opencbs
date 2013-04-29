using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Reflection;

namespace Octopus.Shared.Settings.Remote
{
    public class XmlRemoteServerSettings : IRemoteServerSettings
    {
        XmlDocument _document;

        public XmlRemoteServerSettings()
        {
            _document = new XmlDocument();
            _document.Load(_path);
        }

        private string _path
        {
            get { return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"RemoteServerSettings.xml"); }
        }

        #region IRemoteServerSettings Members

        public string ServerName
        {
            get
            {
                return _document.SelectSingleNode("//ServerName").InnerText;
            }
            set
            {
                _document.SelectSingleNode("//ServerName").InnerText = value;
                _document.Save(_path);
            }
        }

        public string LoginName
        {
            get
            {
                return _document.SelectSingleNode("//LoginName").InnerText;
            }
            set
            {
                _document.SelectSingleNode("//LoginName").InnerText = value;
                _document.Save(_path);
            }
        }

        public string Password
        {
            get
            {
                return _document.SelectSingleNode("//Password").InnerText;
            }
            set
            {
                _document.SelectSingleNode("//Password").InnerText = value;
                _document.Save(_path);
            }
        }

        #endregion
    }
}
