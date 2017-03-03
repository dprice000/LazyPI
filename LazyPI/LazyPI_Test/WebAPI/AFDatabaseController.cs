using LazyPI.Common;
using LazyPI.LazyObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI_Test.WebAPI
{
    public class AFDatabaseController
    {
        List<AFDatabase> _database = new List<AFDatabase>();
        List<AFElement> _elements = new List<AFElement>();
        List<AFEventFrame> _frames = new List<AFEventFrame>();

        public AFDatabase Find(Connection Connection, string ID)
        {
            return _database.Find(x => x.ID == ID);
        }

        public AFDatabase FindByPath(Connection Connection, string Path)
        {
            return _database.Find(x => x.Path == Path);
        }

        public bool Update(Connection Connection, AFDatabase AFDB)
        {
            int index = _database.IndexOf(AFDB);

            _database[index] = AFDB;

            return true;
        }

        public bool Delete(Connection Connection, string DatabaseID)
        {
           var db =  _database.Find(x => x.ID == DatabaseID);
            _database.Remove(db);

            return true;
        }

        public bool CreateElement(Connection Connection, string DatabaseID, AFElement Element)
        {
            _elements.Add(Element);

            return true;
        }

        public bool CreateEventFrame(Connection Connection, string DatabaseID, AFEventFrame EventFrame)
        {
            _frames.Add(EventFrame);

            return true;
        }

        public IEnumerable<AFElement> GetElements(Connection Connection, string DatabaseID)
        {
            return _elements;
        }

        public IEnumerable<AFEventFrame> GetEventFrames(Connection Connection, string DatabaseID)
        {
            return _frames;
        }
    }
}
