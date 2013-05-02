// LICENSE PLACEHOLDER

using System.Collections.Generic;
using OpenCBS.Manager;
using NUnit.Framework;
using OpenCBS.CoreDomain.SearchResult;
using OpenCBS.CoreDomain;


namespace OpenCBS.Test.Manager
{
    [TestFixture]
    public class TestProjectManagerProxy : BaseManagerTest
    {
        [Test]
        public void SearchProjectByCriteres()
        {

            ProjectManager projecManager = (ProjectManager)container["ProjectManager"];
            List<ProjetSearchResult> list = projecManager.SelectProjectByCriteres(1, "DE;DE");
            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 0);
        }
    }
}
