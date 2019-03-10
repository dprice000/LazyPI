using LazyPI.Common;
using LazyPI.LazyObjects;
using System.Collections.Generic;

namespace LazyPI_Test.WebAPI.Dummies
{
    public class AFServerController : IAFServerController
    {
        private List<AFServer> Servers { get; set; }
        private List<AFDatabase> Databases { get; set; }
        private List<AFUnit> Units { get; set; }

        public AFServerController()
        {
            Servers = DataGenerator.Servers;
            Databases = DataGenerator.Databases;
            Units = DataGenerator.Units;
        }

        public AFServer Find(Connection Connection, string ServerID)
        {
            return Servers.Find(x => x.WebID == ServerID);
        }

        public AFServer FindByPath(Connection Connection, string Path)
        {
            return Servers.Find(x => x.Path == Path);
        }

        public AFServer FindByName(Connection Connection, string Name)
        {
            return Servers.Find(x => x.Name == Name);
        }

        public IEnumerable<AFServer> List(Connection Connection)
        {
            return Servers;
        }

        public IEnumerable<AFDatabase> GetDatabases(Connection Connection, string AFServerID)
        {
            return Databases;
        }

        public IEnumerable<AFUnit> GetUnitClasses(Connection Connection, string AFServerID)
        {
            return Units;
        }

        public bool CreateAssetDatabase(Connection Connection, string AFServerID, AFDatabase AFDB)
        {
            Databases.Add(AFDB);
            return true;
        }

        public bool CreateUnitClass(Connection Connection, string AFServerID, AFUnit UnitClass)
        {
            Units.Add(UnitClass);
            return true;
        }
    }
}