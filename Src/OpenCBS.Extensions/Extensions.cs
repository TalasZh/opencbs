// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;

namespace OpenCBS.Extensions
{
    public class Extension : IDisposable
    {
        private static Extension _instance;

        public static Extension Instance
        {
            get { return _instance ?? (_instance = new Extension()); }
        }

        private Extension() {}

        public void Dispose()
        {
            Clear();
        }

        public void Clear()
        {
            _extensions = null;
        }

        [ImportMany(typeof(IExtension), AllowRecomposition = true)]
        private List<IExtension> _extensions;

        public void LoadExtensions()
        {
            LoadExtensionsFromFolder(ExtensionFolder);
        }

        public List<IExtension> Extensions
        {
            get
            {
                if (_extensions != null) return _extensions;
                LoadExtensions();
                return _extensions ?? (_extensions = new List<IExtension>());
            }
        }

        private static string ExtensionFolder
        {
            get { return Application.StartupPath + @"\Extensions"; }
        }

        private static string ExtensionStatusFile
        {
            get { return Path.Combine(ExtensionFolder, "Status.xml"); }
        }

        internal string[] LoadExtensionsFromFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath)) return new string[0];

            SafeDirectoryCatalog directoryCatalog = new SafeDirectoryCatalog(folderPath);

            var catalog = new AggregateCatalog(
                new AssemblyCatalog(Assembly.GetExecutingAssembly()),
                directoryCatalog);
            var batch = new CompositionBatch();
            batch.AddPart(this);

            CompositionContainer container = new CompositionContainer(catalog);
            container.Compose(batch);
            return directoryCatalog.FailedAssemblies;
        }

        public static bool Exists(string filePath)
        {
            var extensionPath = GetExtensionPath(filePath);
            return File.Exists(extensionPath);
        }

        private static string GetExtensionPath(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException(
                    string.Format("Given path: {0} does not contain file name.", filePath)
                    );
            string extensionPath = Path.Combine(ExtensionFolder, fileName);
            return extensionPath;
        }

        public static string[] GetDeletedExtensions()
        {
            if(!File.Exists(ExtensionStatusFile)) return new string[] {};

            XDocument document = XDocument.Load(ExtensionStatusFile);
            Debug.Assert(document.Root != null, "Root element: actions, should exist");
            IEnumerable<XElement> lastItems =
                from a in document.Root.Descendants(ActionTagName)
                group a by a.Value
                into g
                select g.Last();
            return lastItems.Select(a => a.Value).ToArray();
        }

        public static void AddExtension(string filePath)
        {
            var extensionPath = GetExtensionPath(filePath);
            if (File.Exists(extensionPath))
            {
                AddDeleteExtensionFile(extensionPath);

                string tempFolder = Path.GetTempPath();
                string tempFilePath = Path.Combine(tempFolder, Path.GetFileName(filePath));
                File.Copy(filePath, tempFilePath, true);
                AddNewExtensionFile(tempFilePath);
                return;
            }
            File.Copy(filePath, extensionPath);
        }

        public static void DeleteExtension(string filePath)
        {
            AddDeleteExtensionFile(filePath);
        }

        private static void AddNewExtensionFile(string newExtensionFilePath)
        {
            AddActionToStatusFile(newExtensionFilePath, ActionAddName);
        }

        private static void AddDeleteExtensionFile(string extensionPath)
        {
            AddActionToStatusFile(extensionPath, ActionDeleteName);
        }

        private const string ActionTagName = "action";
        private const string ActionAddName = "add";
        private const string ActionDeleteName = "delete";
        private const string TypeAttributeName = "type";

        private static void AddActionToStatusFile(string filePath, string type)
        {
            if (!File.Exists(ExtensionStatusFile))
                GenerateExtensionStatusFile();

            XDocument document = XDocument.Load(ExtensionStatusFile);
            XElement deleteElement =
                new XElement(ActionTagName,
                    new XAttribute(TypeAttributeName, type),
                    new XCData(filePath)
                    );

            Debug.Assert(document.Root != null, "Root element: actions, should exist");
            document.Root.Add(deleteElement);

            document.Save(ExtensionStatusFile);
        }

        public static void RunStatusActions()
        {
            if(!File.Exists(ExtensionStatusFile)) return;

            XDocument document = XDocument.Load(ExtensionStatusFile);
            XElement rootElement = document.Root;
            Debug.Assert(rootElement != null, "Root element: actions, should exist");

            foreach (XElement action in rootElement.Descendants(ActionTagName))
            {
                XAttribute typeAttribute = action.Attribute(TypeAttributeName);
                Debug.Assert(typeAttribute != null, "Type should exist");

                string filePath = action.Value;
                string type = typeAttribute.Value;
                switch (type)
                {
                    case ActionDeleteName:
                        if(File.Exists(filePath))
                            File.Delete(filePath);
                        break;
                    case ActionAddName:
                        var extensionPath = GetExtensionPath(filePath);
                        if (!File.Exists(extensionPath))
                            File.Copy(filePath, extensionPath);
                        break;
                    default:
                        throw new NotImplementedException(string.Format("No such action type: {0}.", type));
                }
            }

            rootElement.RemoveAll();

            document.Save(ExtensionStatusFile);
        }

        private static void GenerateExtensionStatusFile()
        {
            XDocument document = new XDocument(
                new XElement("actions")
                );

            document.Save(ExtensionStatusFile);
        }
    }

    public class OctopusSettings : IDisposable
    {
        public OctopusSettings()
        {
            LoadSettings();
        }

        public void Dispose()
        {
            Settings.Clear();
        }

        [ImportMany(typeof(ITechnicalSettings), AllowRecomposition = true)]
        public List<ITechnicalSettings> Settings { get; set; }

        public string[] LoadSettings()
        {
            string path = Application.StartupPath;
            //return Extension.LoadExtensionsFromFolder(path, this);
            return new string[]{};
        }
    }
}
