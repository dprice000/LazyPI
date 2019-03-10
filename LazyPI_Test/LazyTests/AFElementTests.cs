using LazyPI.LazyObjects;
using Dummies = LazyPI_Test.WebAPI.Dummies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LazyPI_Test.LazyTests
{
    [TestClass]
    public class AFElementTests
    {
        private static IAFServerController serverController = new Dummies.AFServerController();
        private static IAFDatabaseController dbController = new Dummies.AFDatabaseController();
        private static IAFElementController eleController = new Dummies.ElementController();
        private static AFServer server;
        private static AFDatabase AFDB;

        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {         
            server = serverController.FindByName(new TestConnection(), "Server1");
            server.ServerController = serverController;

            AFDB = server.Databases["Database1"];
            AFDB.DBController = dbController;
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

            Assert.IsFalse(element.IsNew);
            Assert.IsFalse(element.IsDirty);
            Assert.IsFalse(element.IsDeleted);
            Assert.IsNotNull(AFDB.Elements["Create New Element"]);
        }

        [TestMethod]
        public void CreateChildElement()
        {
            var ele = AFDB.Elements[0];
            ele.ElementController = eleController;

            AFElement ele2 = new AFElement();
            ele2.Name = "Test Child Element";
            ele2.Description = "Element Created to test child creation.";

            ele.Elements.Add(ele2);
            Assert.IsTrue(ele.IsDirty);
            ele.CheckIn();

            Assert.IsFalse(ele2.IsNew);
            Assert.IsFalse(ele2.IsDirty);
            Assert.IsFalse(ele2.IsDeleted);
            Assert.IsNotNull(ele.Elements["Test Child Element"]);
        }

        [TestMethod]
        public void RequestParent()
        {
            var ele = AFDB.Elements["Element1"];

            AFElement ele2 = new AFElement();
            ele2.Name = "Test Child Element";
            ele2.Description = "Element Created to test child creation.";

            ele.Elements.Add(ele2);
            ele.CheckIn();

            Assert.IsNotNull(ele2.Parent);
        }
    }
}
