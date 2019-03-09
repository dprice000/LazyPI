using LazyPI.Common;
using System.Collections.Generic;

namespace LazyPI.LazyObjects
{
    public interface IAFServerController
    {
        AFServer Find(Connection Connection, string ServerID);

        AFServer FindByPath(Connection Connection, string Path);

        AFServer FindByName(Connection Connection, string Name);

        IEnumerable<AFServer> List(Connection Connection);

        IEnumerable<AFDatabase> GetDatabases(Connection Connection, string AFServerID);

        IEnumerable<AFUnit> GetUnitClasses(Connection Connection, string AFServerID);

        bool CreateAssetDatabase(Connection Connection, string AFServerID, AFDatabase AFDB);

        bool CreateUnitClass(Connection Connection, string AFServerID, AFUnit UnitClass);
    }
}