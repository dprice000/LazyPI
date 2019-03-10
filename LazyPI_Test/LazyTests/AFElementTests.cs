using LazyPI.LazyObjects;
using Dummies = LazyPI_Test.WebAPI.Dummies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LazyPI_Test.LazyTests
{
    [TestClass]
    public class AFElementTests
    {
        private static IAFServerController serverController = new Dummies.AFServerController();
        private static AFServer server;
        private static AFDatabase AFDB;


        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {         
            server = serverController.FindByName(new TestConnection(), "Server1");
            server.ServerController = serverController;

            AFDB = server.Databases["Database1"];
            AFDB.DBController = new Dummies.AFDatabaseController();
        }

        [TestMethod]
        public void InitializeElement()
        {
            AFElement ele = new AFElement();

            Assert.IsTrue(ele.IsNew);
            Assert.IsFalse(ele.IsDirty);
            Assert.IsFalse(ele.IsDeleted);
        }

        [TestMethod]
        public void CreateNewElement()
        {
            AFElement element = new AFElement();
            element.Name = "Create New Element";
            element.Description = "Testing the creation of a new element";

            AFDB.Elements.Add(element);
            AFDB.CheckIn();
        }

        [TestMethod]
        public void RequestParent()
        {
            AFElement ele = new AFElement();

        }
    }
}
