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
        AFServer _server;
        AFDatabase _db;
        WebAPIConnection _conn;

        [TestInitialize]
        public void Initialize()
        {
            _conn = new WebAPIConnection(AuthType.Kerberos);
            _server = AFServer.FindByName(_conn, "ServerName");
            _db = _server.Databases["DatabaseName"];

            Console.WriteLine("Test Element Intial Configuration");
        }

        public static AFElement GenerateElement()
        {
            string name = "Test Element";
            string desc = "This is a unit test element.";
            AFElement ele = new AFElement();
            ele.Name = name;
            ele.Description = desc;

            Assert.Equals(ele.Name, name);
            Assert.Equals(ele.Description, desc);

            return ele;
        }

        [TestMethod]
        public void CreateElement()
        {
            AFElement element = GenerateElement();

            Console.WriteLine("Test element creation.");
            Assert.IsTrue(_db.CreateElement(element), "Assert creation passed");
            
            //Check that the the element can be found through the AFDB
            Assert.IsNotNull(_db.Elements[element.Name], "Check AFDB element collection for new element.");
            Assert.IsNotNull(AFElement.Find(_conn, element.WebID));
            Assert.IsNotNull(AFElement.FindByPath(_conn, element.Path));
            //Assert.IsNotNull(AFElement.FindByTemplate());
            //Assert.IsNotNull(AFElement.FindByCategory(_conn, element.Categories.First()));

            //TODO: There should be more tests for finding the element
            element.Delete();
            Assert.IsTrue(element.IsDeleted);
            element.CheckIn();

            Assert.IsNull(AFElement.Find(_conn, element.WebID));
        }

        [TestMethod]
        public void Update()
        {
            Console.WriteLine("Test updating element settings.");

            string name = "Updated element", desc = "Updated description";
            AFElement element = GenerateElement();

            Console.WriteLine("Test element creation.");
            Assert.IsTrue(_db.CreateElement(element), "Assert creation passed");

            AFElement elem = AFElement.Find(_conn, name);
            Assert.Equals(elem.Name, name);
            Random ran = new Random();
            elem.Name = name;
            elem.Description = desc;
            Assert.IsTrue(elem.IsDirty);
            elem.CheckIn();

            elem = AFElement.Find(_conn, name);
            Assert.Equals(elem.Name, name);
            Assert.Equals(elem.Description, desc);

            elem.Delete();
            elem.CheckIn();
        }

        [TestMethod]
        public void CreateChild()
        {
            Console.WriteLine("Test adding a child element.");

            AFElement parent = new AFElement();
            parent.Name = "Parent Element";
            parent.Description = "Parent Desciption";

            _db.CreateElement(parent);
            parent = AFElement.Find(_conn, parent.Name);

            Assert.IsNotNull(parent.ID);
            Assert.IsNotNull(parent.Path);

            AFElement child = new AFElement();
            child.Name = "Child Element";
            child.Description = "Child Description";

            parent.Elements.Add(child);

            child = AFElement.FindByPath(_conn, child.Path);
   
            Assert.IsNotNull(child.Name);
            Assert.IsNotNull(child.ID);
            Assert.IsNotNull(child.Path);
            Assert.IsNotNull(child.Description);
            Assert.Equals(child.Parent.ID, parent.ID);

            parent = AFElement.FindByPath(_conn, parent.Path);

            //Assert.(parent.Elements.Count, 1);

            AFElement refChild = parent.Elements[0];

            Console.WriteLine("Test that original child and referenced child are identical.");
            Assert.Equals(child.Name, refChild.Name);
            Assert.Equals(child.Path, refChild.Path);

            parent.Delete();
            Assert.IsTrue(parent.IsDeleted);

            parent.CheckIn();
            Assert.IsNull(AFElement.Find(_conn, parent.ID), "Assert that parent no longer exists");
            Assert.IsNull(AFElement.Find(_conn, child.ID), "Assert that child no longer exists.");
        }

        [TestMethod]
        public void CreateAttribute()
        {
            AFElement element = new AFElement();

            string name = "Test Element 1";
            element.Name = name;
            Assert.Equals(element.Name, name);

            string desc = "Lazy PI Unit Test Element";
            element.Description = desc;
            Assert.Equals(element.Description, desc);

            Console.WriteLine("Test element creation.");

            Assert.IsTrue(_db.CreateElement(element), "Assert creation passed");

            element = _db.Elements[element.Name];

            //Check that the the element can be found through the AFDB
            Assert.IsNotNull(element, "Check AFDB element collection for new element.");

            AFAttribute attr = new AFAttribute();
            attr.Name = "Test Attribute";
            attr.Description = "Created by WebAPI tests";

            element.Attributes.Add(attr);
            element.CheckIn();

            Assert.Equals(element.Attributes.Count, 1);
            attr = element.Attributes[attr.Name];

            Assert.IsNotNull(attr);
            Assert.IsNotNull(attr.ID);
            Assert.IsNotNull(attr.Name);
            Assert.IsNotNull(attr.Description);
            Assert.IsNotNull(attr.Path);

            string val = "Test string";

            // Test set and get of AFValue object
            attr.SetValue(new AFValue(val));
            AFValue valObj = attr.GetValue();
            Assert.Equals(valObj.Value, val);

            element.Delete();
            Assert.IsTrue(element.IsDeleted);

            element.CheckIn();
            Assert.IsNull(AFElement.Find(_conn, element.WebID));
        }
    }
}
