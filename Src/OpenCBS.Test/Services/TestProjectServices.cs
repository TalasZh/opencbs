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
using NUnit.Framework;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Services;
using System.Collections.Generic;
using OpenCBS.CoreDomain.SearchResult;
using NUnit.Mocks;

namespace OpenCBS.Test.Services
{
    [TestFixture]
    public class TestProjectServices:BaseServicesTest
    {
        [Test]
        [ExpectedException(typeof(OpenCbsProjectSaveException))]
        public void SaveProject_CodeNull_ThrowException()
        {
            var project = new Project("NotSet") {Code = null};
            IClient client = new Person {Id = 1};

            new ProjectServices(new User()).SaveProject(project, client);
        }

        [Test]
        [ExpectedException(typeof(OpenCbsProjectSaveException))]
        public void SaveProject_CodeEmpty_ThrowException()
        {
            var project = new Project("NotSet") { Code = String.Empty };
            IClient client = new Person { Id = 1 };

            new ProjectServices(new User()).SaveProject(project, client);
        }

        [Test]
        [ExpectedException(typeof(OpenCbsProjectSaveException))]
        public void SaveProject_NameNull_ThrowException()
        {
            var project = new Project("NotSet") {Code = "NotSet", Name = null};
            IClient client = new Person { Id = 1 };
            new ProjectServices(new User()).SaveProject(project, client);
        }

        [Test]
        [ExpectedException(typeof(OpenCbsProjectSaveException))]
        public void SaveProject_NameEmpty_ThrowException()
        {
            var project = new Project("NotSet") {Code = "NotSet", Name = String.Empty};
            IClient client = new Person { Id = 1 };

            new ProjectServices(new User()).SaveProject(project, client);
        }

        [Test]
        [ExpectedException(typeof(OpenCbsProjectSaveException))]
        public void SaveProject_AimNull_ThrowException()
        {
            var project = new Project("NotSet") {Code = "NotSet", Name = "NotSet", Aim = null};
            IClient client = new Person { Id = 1 };

            new ProjectServices(new User()).SaveProject(project, client);
        }

        [Test]
        [ExpectedException(typeof(OpenCbsProjectSaveException))]
        public void SaveProject_AimEmpty_ThrowException()
        {
            var project = new Project("NotSet") {Name = "NotSet", Code = "NotSet", Aim = String.Empty};
            IClient client = new Person { Id = 1 };

            new ProjectServices(new User()).SaveProject(project, client);
        }
      
        [Test]
        public void SearchProjectByCritere()
        {
            ProjectServices service = (ProjectServices)container["ProjectServices"];
            int total;
            int current=1;
            int records=0;
            List<ProjetSearchResult> list = service.FindProjectByCriteres(current, out total, out records, "DE;DE");
            Assert.IsNotNull(list);
            Assert.IsTrue(records > 0);
        }

        [Test]
        public void SearchProjectByCritereWithoutQuery()
        {
            ProjectServices service = (ProjectServices)container["ProjectServices"];
            int total;
            int current = 1;
            int records = 0;
            List<ProjetSearchResult> list = service.FindProjectByCriteres(current, out total, out records,string.Empty);
            Assert.IsNotNull(list);
            Assert.IsTrue(records > 0);
        }
    }
}
