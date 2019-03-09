using LazyPI.Common;
using System.Collections.Generic;

namespace LazyPI.LazyObjects
{
    public interface IAFDatabaseController
    {
        AFDatabase Find(Connection Connection, string ID);

        AFDatabase FindByPath(Connection Connection, string Path);

        bool Update(Connection Connection, AFDatabase AFDB);

        bool Delete(Connection Connection, string DatabaseID);

        bool CreateElement(Connection Connection, string DatabaseID, AFElement Element);

        bool CreateEventFrame(Connection Connection, string DatabaseID, AFEventFrame EventFrame);

        IEnumerable<AFElement> GetElements(Connection Connection, string DatabaseID);

        IEnumerable<AFEventFrame> GetEventFrames(Connection Connection, string DatabaseID);
    }
}