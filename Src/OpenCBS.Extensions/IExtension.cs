// LICENSE PLACEHOLDER

using System;
using System.IO;
using System.Reflection;

namespace OpenCBS.Extensions
{
    public interface IExtension
    {
        Guid Guid { get; }
        string GetMeta(string key);
        object QueryInterface(Type t);
    }

    public class ExtensionInfo
    {
        private readonly string _extensionName;
        private readonly string _filePath;
        private readonly long _fileSize;
        private readonly string _octopusVersion;
        private readonly Guid _guid;
        private readonly string _version;
        private readonly string _vendor;

        public ExtensionInfo(IExtension extension)
        {
            _extensionName = extension.GetMeta("Name");

            Type extensionType = extension.GetType();
            _octopusVersion = extension.GetMeta("OctopusVersion");
            _guid = extension.Guid;
            
            Assembly extensionAssembly = extensionType.Assembly;
            _version = extensionAssembly.GetName().Version.ToString(3);
            _filePath = extensionAssembly.Location;            
            FileInfo fileInfo = new FileInfo(_filePath);
            _fileSize = fileInfo.Length;
            _vendor = extension.GetMeta("Vendor");
        }

        public string Version
        {
            get { return _version; }
        }

        public Guid Guid
        {
            get { return _guid; }
        }

        public string OctopusVersion
        {
            get { return _octopusVersion; }
        }

        public long FileSize
        {
            get { return _fileSize; }
        }

        public string FilePath
        {
            get { return _filePath; }
        }

        public string ExtensionName
        {
            get { return _extensionName; }
        }

        public string Vendor
        {
            get { return _vendor; }
        }
    }
}
