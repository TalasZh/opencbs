// LICENSE PLACEHOLDER

using NUnit.Framework;

namespace OpenCBS.Test
{
    [SetUpFixture]
    public class TestSetup
    {
        [SetUp]
        public void Setup()
        {
            OpenCBS.CoreDomain.DatabaseConnection.IsProductionDatabase = false;
        }
    }
}
