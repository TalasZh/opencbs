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
            catch (OpenCbsCustomExportException e)
            {
                Assert.AreEqual(OpenCbsCustomExportExceptionEnum.FileNameIsEmpty, e.Code);
            }

            file.Name = "Export File";
            file.Extension = string.Empty;

            try
            {
                _exportServices.ValidateFile(file);
                Assert.Fail("File Extension is incorrect");
            }
            catch (OpenCbsCustomExportException e)
            {
                Assert.AreEqual(OpenCbsCustomExportExceptionEnum.FileExtensionIsIncorrect, e.Code);
            }

            file.Extension = "txt";

            try
            {
                _exportServices.ValidateFile(file);
                Assert.Fail("File Extension is incorrect");
            }
            catch (OpenCbsCustomExportException e)
            {
                Assert.AreEqual(OpenCbsCustomExportExceptionEnum.FileExtensionIsIncorrect, e.Code);
            }

            file.Extension = "*.txt";

            try
            {
                _exportServices.ValidateFile(file);
                Assert.Fail("File Extension is incorrect");
            }
            catch (OpenCbsCustomExportException e)
            {
                Assert.AreEqual(OpenCbsCustomExportExceptionEnum.FileExtensionIsIncorrect, e.Code);
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
            catch (OpenCbsCustomExportException e)
            {
                Assert.AreEqual(OpenCbsCustomExportExceptionEnum.SomeRequiredFieldsAreMissing, e.Code);
            }

            file.SelectedFields.Add(file.DefaultList.OfType<Field>().First(item => item.IsRequired).Clone() as IField);

            try
            {
                _exportServices.ValidateFile(file);
                Assert.Fail("Required fields are missing");
            }
            catch (OpenCbsCustomExportException e)
            {
                Assert.AreEqual(OpenCbsCustomExportExceptionEnum.SomeRequiredFieldsAreMissing, e.Code);
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
