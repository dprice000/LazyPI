using LazyPI.Common;
using LazyPI.LazyObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI_Test.WebAPI
{
    public class AFServerController : IAFServerController
    {
        List<AFServer> _servers = new List<AFServer>();
        List<AFDatabase> _databases = new List<AFDatabase>();
        List<AFUnit> _units = new List<AFUnit>();

        public AFServer Find(Connection Connection, string ServerID)
        {
            return _servers.Find(x => x.ID == ServerID);
        }

        public AFServer FindByPath(Connection Connection, string Path)
        {
            return _servers.Find(x => x.Path == Path);
        }

        public AFServer FindByName(Connection Connection, string Name)
        {
            return _servers.Find(x => x.Name == Name);
        }

        public IEnumerable<AFServer> List(Connection Connection)
        {
            return _servers;
        }

        public IEnumerable<AFDatabase> GetDatabases(Connection Connection, string AFServerID)
        {
            return _databases;
        }

        public IEnumerable<AFUnit> GetUnitClasses(Connection Connection, string AFServerID)
        {
            return _units;
        }

        public bool CreateAssetDatabase(Connection Connection, string AFServerID, AFDatabase AFDB)
        {
            _databases.Add(AFDB);

            return true;
        }

        public bool CreateUnitClass(Connection Connection, string AFServerID, AFUnit UnitClass)
        {
            _units.Add(UnitClass);

            return true;
        }
    }
}
