using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazyPI.LazyObjects;
using LazyPI.WebAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace LazyPI_Test.Stubs.Interfaces
{
    [TestClass]
    public class ElementTests
    {
        AFElementConnector elementLoader;
        WebAPIConnection conn;

        [TestInitialize]
        public void Initialize()
        {
            elementLoader = new AFElementConnector();
            conn = new WebAPIConnection(AuthenticationType.Kerberos);
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
            Assert.IsTrue(elementLoader.CreateChildElement(conn, "", element));
        }

        [TestMethod]
        public void FindByName()
        {
            Console.WriteLine("Test Finding Element by Name");
            string name = "Test Element 1";

            AFElement elem = elementLoader.Find(conn, name);

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

            AFElement elem = elementLoader.FindByPath(conn, path);

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

            AFElement elem = elementLoader.Find(conn, name);
            Assert.Equals(elem.Name, name);
            Random ran = new Random();
            name += ran.Next(100).ToString();
            desc += ran.Next(100).ToString();

            Assert.IsTrue(elementLoader.Update(conn, elem));

            elem = elementLoader.Find(conn, name);

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

            Assert.IsTrue(elementLoader.CreateChildElement(conn, "", parent));
            parent = elementLoader.Find(conn, parent.Name);

            Assert.IsNotNull(parent.ID);
            Assert.IsNotNull(parent.Path);

            AFElement child = new AFElement();
            child.Name = "Child Element";
            child.Description = "Child Description";

            Assert.IsTrue(elementLoader.CreateChildElement(conn, parent.ID, child));

            child = elementLoader.FindByPath(conn, child.Path);
   
            Assert.IsNotNull(child.Name);
            Assert.IsNotNull(child.ID);
            Assert.IsNotNull(child.Path);
            Assert.IsNotNull(child.Description);
            Assert.Equals(child.Parent.ID, parent.ID);

            parent = elementLoader.FindByPath(conn, parent.Path);

            Assert.Equals(parent.Children.Count, 1);

            AFElement refChild = parent.Children.First();

            Console.WriteLine("Test that original child and referenced child are identical.");
            Assert.Equals(child.Name, refChild.Name);
            Assert.Equals(child.Path, refChild.Path);
        }
    }
}
