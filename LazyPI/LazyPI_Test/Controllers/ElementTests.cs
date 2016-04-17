using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazyPI.LazyObjects;
using LazyPI.WebAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace LazyPI_Test.Controllers
{
    [TestClass]
    public class ElementTests
    {
        AFElementConnector _elementLoader;
        AFDatabaseConnector _dbLoader;
        AFServer _server;
        AFDatabase _db;
        WebAPIConnection _conn;

        [TestInitialize]
        public void Initialize()
        {
            _elementLoader = new AFElementConnector();
            _dbLoader = new AFDatabaseConnector();
            _conn = new WebAPIConnection(AuthType.Kerberos);
            _server = AFServer.FindByName(_conn, "ServerName");
            _db = _server.Databases["DatabaseName"];
        }

        [TestMethod]
        public void CreateElement()
        {
            AFElement element = new AFElement();

            Console.WriteLine("Test Element Intial Configuration");
            string name = "Test Element 1";
            element.Name = name;
            Assert.Equals(element.Name, name);

            string desc = "Lazy PI Unit Test Element";
            element.Description = desc;
            Assert.Equals(element.Description, desc);

            Console.WriteLine("Test element creation.");

            Assert.IsTrue(_db.CreateElement(element), "Assert creation passed");
            
            //Check that the the element can be found through the AFDB
            Assert.IsNotNull(_db.Elements[element.Name], "Check AFDB element collection for new element.");

            //TODO: There should be more tests for finding the element

            Assert.IsTrue(_elementLoader.Delete(_conn, element.ID), "Delete new element.");
        }

        [TestMethod]
        public void FindByName()
        {
            Console.WriteLine("Test Finding Element by Name");
            string name = "Test Element 1";

            AFElement elem = _elementLoader.Find(_conn, name);

            Console.WriteLine("Test that found element is fully constructed");
            Assert.Equals(elem.Name, name);
            Assert.IsNotNull(elem.ID, "Check element has an ID");
            Assert.IsNotNull(elem.Description, "Check element has a description.");
            Assert.IsNotNull(elem.Path, "Check element has a path.");

            Assert.IsNotNull(elem.Parent, "Check element has a parent.");
        }

        [TestMethod]
        public void FindByPath()
        {
            Console.WriteLine("Test Finding Element by Path");
            string path = "";

            AFElement elem = _elementLoader.FindByPath(_conn, path);

            Assert.Equals(elem.Path, path);
            Assert.IsNotNull(elem.Name, "Check names exists.");
            Assert.IsNotNull(elem.ID, "Check ID exists.");
            Assert.IsNotNull(elem.Description, "Check description exists.");
            Assert.IsNotNull(elem.Parent, "Check element has parent.");
        }

        [TestMethod]
        public void Update()
        {
            Console.WriteLine("Test updating element settings.");
            string name = "", desc = "";

            AFElement elem = _elementLoader.Find(_conn, name);
            Assert.Equals(elem.Name, name);
            Random ran = new Random();
            name += ran.Next(100).ToString();
            desc += ran.Next(100).ToString();

            Assert.IsTrue(_elementLoader.Update(_conn, elem));

            elem = _elementLoader.Find(_conn, name);

            Assert.Equals(elem.Name, name);
            Assert.Equals(elem.Description, desc);
        }

        [TestMethod]
        public void CreateChild()
        {
            Console.WriteLine("Test adding a child element.");

            AFElement parent = new AFElement();
            parent.Name = "Parent Element";
            parent.Description = "Parent Desciption";

            Assert.IsTrue(_elementLoader.CreateChildElement(_conn, "", parent));
            parent = _elementLoader.Find(_conn, parent.Name);

            Assert.IsNotNull(parent.ID);
            Assert.IsNotNull(parent.Path);

            AFElement child = new AFElement();
            child.Name = "Child Element";
            child.Description = "Child Description";

            Assert.IsTrue(_elementLoader.CreateChildElement(_conn, parent.ID, child));

            child = _elementLoader.FindByPath(_conn, child.Path);
   
            Assert.IsNotNull(child.Name);
            Assert.IsNotNull(child.ID);
            Assert.IsNotNull(child.Path);
            Assert.IsNotNull(child.Description);
            Assert.Equals(child.Parent.ID, parent.ID);

            parent = _elementLoader.FindByPath(_conn, parent.Path);

            //Assert.(parent.Elements.Count, 1);

            AFElement refChild = parent.Elements[0];

            Console.WriteLine("Test that original child and referenced child are identical.");
            Assert.Equals(child.Name, refChild.Name);
            Assert.Equals(child.Path, refChild.Path);

            Assert.IsTrue(Delete(parent.ID), "Parent Deleted");
            Assert.IsNull(_elementLoader.Find(_conn, parent.ID), "Assert that parent no longer exists");
            Assert.IsNull(_elementLoader.Find(_conn, child.ID), "Assert that child no longer exists.");
        }

        [TestMethod]
        public bool Delete(string ID)
        {
           return _elementLoader.Delete(_conn, ID);
        }
    }
}
