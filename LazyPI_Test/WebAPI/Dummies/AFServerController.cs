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
            FixtureWrapper fixture = new FixtureWrapper();

            var server1 = fixture.Create<AFServer>();
            server1.Name = "Server1";
            Servers.Add(server1);

            var server2 = fixture.Create<AFServer>();
            server2.Name = "Server2";
            Servers.Add(server2);

            var db1 = fixture.Create<AFDatabase>();
            db1.Name = "Database1";
            Databases.Add(db1);

            var db2 = fixture.Create<AFDatabase>();
            db2.Name = "Database2";
            Databases.Add(db2);

            var unit1 = fixture.Create<AFUnit>();
            unit1.Name = "Unit1";
            Units.Add(unit1);
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