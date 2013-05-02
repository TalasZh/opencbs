using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenCBS.Services.Export;
using NUnit.Mocks;
using OpenCBS.Manager.Export;
using OpenCBS.CoreDomain.Export;
using OpenCBS.ExceptionsHandler.Exceptions.ExportExceptions;
using OpenCBS.CoreDomain.Export.Files;
using OpenCBS.CoreDomain.Export.Fields;

namespace OpenCBS.Test.Services.Export
{
    [TestFixture]
    public class TestExportServices
    {
        private ExportServices _exportServices;
        private DynamicMock _exportManagerMock;

        [SetUp]
        public void SetUp()
        {
            _exportManagerMock = new DynamicMock(typeof(ExportManager));
            _exportServices = new ExportServices((ExportManager)_exportManagerMock.MockInstance);
        }

        [Test]
        public void TestValidateInstallmentExportFile()
        {
            InstallmentExportFile file = new InstallmentExportFile();

            try
            {
                _exportServices.ValidateFile(file);
                Assert.Fail("File Name can't be empty");
            }
            catch (OctopusCustomExportException e)
            {
                Assert.AreEqual(OctopusCustomExportExceptionEnum.FileNameIsEmpty, e.Code);
            }

            file.Name = "Export File";
            file.Extension = string.Empty;

            try
            {
                _exportServices.ValidateFile(file);
                Assert.Fail("File Extension is incorrect");
            }
            catch (OctopusCustomExportException e)
            {
                Assert.AreEqual(OctopusCustomExportExceptionEnum.FileExtensionIsIncorrect, e.Code);
            }

            file.Extension = "txt";

            try
            {
                _exportServices.ValidateFile(file);
                Assert.Fail("File Extension is incorrect");
            }
            catch (OctopusCustomExportException e)
            {
                Assert.AreEqual(OctopusCustomExportExceptionEnum.FileExtensionIsIncorrect, e.Code);
            }

            file.Extension = "*.txt";

            try
            {
                _exportServices.ValidateFile(file);
                Assert.Fail("File Extension is incorrect");
            }
            catch (OctopusCustomExportException e)
            {
                Assert.AreEqual(OctopusCustomExportExceptionEnum.FileExtensionIsIncorrect, e.Code);
            }

            file.Extension = ".txt";
            Assert.AreEqual(true, _exportServices.ValidateFile(file));
        }

        [Test]
        public void TestValidateReimbursementImportFile()
        {
            ReimbursementImportFile file = new ReimbursementImportFile
            {
                Name = "Import File",
                Extension = ".txt"
            };

            try
            {
                _exportServices.ValidateFile(file);
                Assert.Fail("Required fields are missing");
            }
            catch (OctopusCustomExportException e)
            {
                Assert.AreEqual(OctopusCustomExportExceptionEnum.SomeRequiredFieldsAreMissing, e.Code);
            }

            file.SelectedFields.Add(file.DefaultList.OfType<Field>().First(item => item.IsRequired).Clone() as IField);

            try
            {
                _exportServices.ValidateFile(file);
                Assert.Fail("Required fields are missing");
            }
            catch (OctopusCustomExportException e)
            {
                Assert.AreEqual(OctopusCustomExportExceptionEnum.SomeRequiredFieldsAreMissing, e.Code);
            }

            file.SelectedFields.Clear();
            foreach (var field in file.DefaultList.OfType<Field>().Where(item => item.IsRequired))
            {
                file.SelectedFields.Add(field.Clone() as IField);
            }

            Assert.AreEqual(true, _exportServices.ValidateFile(file));
        }
    }
}
