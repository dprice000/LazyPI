using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LazyPI.WebAPI;
using LazyPI.LazyObjects;

namespace LazyPI_Test.Controllers
{
    [TestClass]
    public class EventFrameTests
    {
        AFEventFrameController _frameLoader;
        AFDatabaseController _dbLoader;
        AFServer _server;
        AFDatabase _db;
        WebAPIConnection _conn;

        [TestInitialize]
        public void Initialize()
        {
            _frameLoader = new AFEventFrameController();
            _dbLoader = new AFDatabaseController();
            _conn = new WebAPIConnection(AuthType.Kerberos);
            _server = AFServer.FindByName(_conn, "ServerName");
            _db = _server.Databases["DatabaseName"];

            Console.WriteLine("Test interactions with EventFrames");
        }

        public static AFEventFrame GenerateFrame(DateTime? start, DateTime? end)
        {
            AFEventFrame newFrame = new AFEventFrame();
            newFrame.Name = "Test Frame";
            newFrame.Description = "This is a test frame";

            if(start.HasValue)
                newFrame.StartTime = start.Value;

            if(end.HasValue)
                newFrame.EndTime = end.Value;

            return newFrame;
        }

        /// <summary>
        /// Creates an eventframe that is not referenced by any element.
        /// </summary>
        [TestMethod]
        public void CreateFreeEventFrame()
        {
            AFEventFrame frame = GenerateFrame(DateTime.Now, null);
            _db.CreateEventFrame(frame);

            AFEventFrame foundFrame = _db.EventFrames[frame.Name];

            Assert.IsNotNull(foundFrame, "Assert frame exists in AFDB frame list");

            Assert.Equals(frame.Name, foundFrame.Name);
            Assert.Equals(frame.Description, foundFrame.Description);
            Assert.Equals(frame.StartTime, foundFrame.StartTime);
            Assert.IsNull(frame.EndTime, "Assert EndTime is null.");

            
            Assert.IsTrue(_frameLoader.Delete(_conn, foundFrame.ID), "Assert frame is deleted.");
        }

        /// <summary>
        /// Create EventFrame that is bound to an elements
        /// </summary>
        [TestMethod]
        public void CreateBoundEventFrame()
        {
            AFElement ele = ElementTests.GenerateElement();

            _db.CreateElement(ele);

            ele = _db.Elements[ele.Name];

            Assert.IsNotNull(ele, "Assert that element exists in AFDB");

            //TODO: Determine the flow of the AFSDK and implement
        }

        public void UpdateEventFrame()
        {
            AFEventFrame frame = GenerateFrame(DateTime.UtcNow, null);
            _db.CreateEventFrame(frame);

            frame = _db.EventFrames[frame.Name];

            Assert.IsNotNull(frame, "Assert frame exists in AFDB frame list");

            Assert.Equals(frame.Name, frame.Name);
            Assert.Equals(frame.Description, frame.Description);
            Assert.Equals(frame.StartTime, frame.StartTime);
            Assert.IsNull(frame.EndTime, "Assert EndTime is null.");

            frame.EndTime = DateTime.UtcNow.AddMinutes(5);
            frame.Name += " (Updated)";

            Assert.IsTrue(_frameLoader.Update(_conn, frame), "Assert update passes");
            Assert.IsTrue(_frameLoader.Delete(_conn, frame.ID), "Assert frame is deleted.");
        }
    }
}
