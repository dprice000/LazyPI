using LazyPI.LazyObjects;
using System.Collections.Generic;

namespace LazyPI_Test.WebAPI.Dummies
{
    public static class DataGenerator
    {
        private static FixtureWrapper _fixture = new FixtureWrapper();
        private static List<AFServer> _servers = null;
        private static List<AFDatabase> _databases = null;
        private static List<AFElement> _elements = null;
        private static List<AFAttribute> _attributes = null;
        private static List<AFEventFrame> _eventFrames = null;
        private static List<AFUnit> _units = null;


        #region "Properties"

        public static List<AFServer> Servers
        {
            get
            {
                if(_servers == null)
                {
                    CreateServers();
                }

                return _servers;
            }
        }

        public static List<AFDatabase> Databases
        {
            get
            {
                if(_databases == null)
                {
                    CreateDatabases();
                }

                return _databases;
            }
        }

        public static List<AFElement> Elements
        {
            get
            {
                if(_elements == null)
                {
                    CreateElements();
                }

                return _elements;
            }
        }

        public static List<AFAttribute> Attributes
        {
            get
            {
                if(_attributes == null)
                {
                    CreateAttributes();
                }

                return _attributes;
            }
        }

        public static List<AFEventFrame> EventFrames
        {
            get
            {
                if (_eventFrames == null)
                {
                    CreateEventFrames();
                }

                return _eventFrames;
            }
        }

        public static List<AFUnit> Units
        {
            get
            {
                if(_units == null)
                {
                    CreateUnits();
                }

                return _units;
            }
        }

        #endregion

        #region "Data Creators"

        private static void CreateServers()
        {
            
            _servers = new List<AFServer>();

            var server1 = _fixture.Create<AFServer>();
            server1.Name = "Server1";
            _servers.Add(server1);

            var server2 = _fixture.Create<AFServer>();
            server2.Name = "Server2";
            _servers.Add(server2);
        }

        private static void CreateDatabases()
        {
            _databases = new List<AFDatabase>();

            var db1 = _fixture.Create<AFDatabase>();
            db1.Name = "Database1";
            _databases.Add(db1);

            var db2 = _fixture.Create<AFDatabase>();
            db2.Name = "Database2";
            _databases.Add(db2);
        }

        private static void CreateElements()
        {
            _elements = new List<AFElement>();

            var ele1 = _fixture.Create<AFElement>();
            ele1.Name = "Element1";
            _elements.Add(ele1);

            var ele2 = _fixture.Create<AFElement>();
            ele2.Name = "Element2";
            _elements.Add(ele2);

            var ele3 = _fixture.Create<AFElement>();
            ele1.Name = "Element3";
            _elements.Add(ele3);

            var ele4 = _fixture.Create<AFElement>();
            ele1.Name = "Element4";
            _elements.Add(ele4);

            var ele5 = _fixture.Create<AFElement>();
            ele5.Name = "Element5";
            _elements.Add(ele5);
        }


        private static void CreateAttributes()
        {
            //throw new NotImplementedException();
        }

        private static void CreateEventFrames()
        {
            //throw new NotImplementedException();
        }

        private static void CreateUnits()
        {
            _units = new List<AFUnit>();

            var unit1 = _fixture.Create<AFUnit>();
            unit1.Name = "Unit1";
            _units.Add(unit1);

            var unit2 = _fixture.Create<AFUnit>();
            unit2.Name = "Unit2";
            _units.Add(unit2);

            var unit3 = _fixture.Create<AFUnit>();
            unit3.Name = "Unit3";
            _units.Add(unit3);
        }
        #endregion
    }
}
