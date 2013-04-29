using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Octopus.CoreDomain
{
    [Serializable]
    public class ReportAvailableForDownload
    {
        public string _guid;
        public string _title;
        public string _description;
        public string _size;
        public bool _exist;
        private List<ReportAvailableForDownload> _reportAvailableForDownload;

        public string Guid
        {
            get { return _guid; }
        }

        public string Title
        {
            get { return _title; }
        }

        public string Description
        {
            get { return _description; }
        }

        public string Size
        {
            get { return _size; }
        }

        public bool Exist
        {
            get { return _exist; }
        }

        public ReportAvailableForDownload(string pGuid, string pTitle, string pDescription, string pSize,bool pExist)
        {
            _guid = pGuid;
            _title = pTitle;
            _description = pDescription;
            _size = pSize;
            _exist = pExist;
        }

        public ReportAvailableForDownload()
        {
            _reportAvailableForDownload = new List<ReportAvailableForDownload>();
        }

        public List<ReportAvailableForDownload> AddReport
        {
            get { return _reportAvailableForDownload; }
            set { _reportAvailableForDownload = value; }
        }
    }
}
