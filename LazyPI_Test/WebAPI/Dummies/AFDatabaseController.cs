using LazyPI.Common;
using LazyPI.LazyObjects;
using System.Collections.Generic;

namespace LazyPI_Test.WebAPI.Dummies
{
    public class AFDatabaseController : IAFDatabaseController
    {
        public List<AFDatabase> Databases { get; private set; }
        public List<AFElement> Elements { get; private set; }
        public List<AFEventFrame> EventFrames { get; private set; }

        public AFDatabaseController()
        {
            Databases = DataGenerator.Databases;
            Elements = DataGenerator.Elements;
            EventFrames = DataGenerator.EventFrames;
        }

        public AFDatabase Find(Connection Connection, string ID)
        {
            return Databases.Find(x => x.WebID == ID);
        }

        public AFDatabase FindByPath(Connection Connection, string Path)
        {
            return Databases.Find(x => x.Path == Path);
        }

        public bool Update(Connection Connection, AFDatabase AFDB)
        {
            int index = Databases.IndexOf(AFDB);

            Databases[index] = AFDB;

            return true;
        }

        public bool Delete(Connection Connection, string DatabaseID)
        {
            var db = Databases.Find(x => x.ID == DatabaseID);
            Databases.Remove(db);
            return true;
        }

        public bool CreateElement(Connection Connection, string DatabaseID, AFElement Element)
        {
            Elements.Add(Element);
            return true;
        }

        public bool CreateEventFrame(Connection Connection, string DatabaseID, AFEventFrame EventFrame)
        {
            EventFrames.Add(EventFrame);
            return true;
        }

        public IEnumerable<AFElement> GetElements(Connection Connection, string DatabaseID)
        {
            return Elements;
        }

        public IEnumerable<AFEventFrame> GetEventFrames(Connection Connection, string DatabaseID)
        {
            return EventFrames;
        }
    }
}