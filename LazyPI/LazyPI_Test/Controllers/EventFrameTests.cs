using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LazyPI.WebAPI;
using LazyPI.LazyObjects;

namespace LazyPI_Test.Controllers
{
    [TestClass]
    public class EventFrameTests
    {
        AFEventFrameConnector _frameLoader;
        AFDatabaseConnector _dbLoader;
        AFServer _server;
        AFDatabase _db;
        WebAPIConnection _conn;

        [TestInitialize]
        public void Initialize()
        {
            _frameLoader = new AFEventFrameConnector();
            _dbLoader = new AFDatabaseConnector();
            _conn = new WebAPIConnection(AuthType.Kerberos);
            _server = AFServer.FindByName(_conn, "ServerName");
            _db = _server.Databases["DatabaseName"];
        }

        private AFEventFrame GenerateFrame(DateTime? start, DateTime? end)
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
        public void CreateFreeEventFramer()
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
        }
    }
}
