// LICENSE PLACEHOLDER

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
