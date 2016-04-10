using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazyPI.Common;

namespace LazyPI.LazyObjects
{
    public class AFServer : BaseObject
    {
        private bool _IsConnected;
        private string _ServerVersion;
        private static IAFServer _ServerConnector;
        private static Lazy<AFDatabases> _Databases;

        #region "Properties"
            public bool IsConnected
            {
                get
                {
                    return _IsConnected;
                }
            }

            public string ServerVersion
            {
                get
                {
                    return _ServerVersion;
                }
            }

            public AFDatabases Databases
            {
                get
                {
                    return _Databases.Value;
                }
            }
        #endregion

        #region "Constructors"
            internal AFServer(Connection Connection, string ID, string Name, string Description, bool IsConnected , string ServerVersion, string Path) : base(Connection, ID, Name, Description, Path)
            {
                Initialize();
                _IsConnected = IsConnected;
                _ServerVersion = ServerVersion;
            }

            private void Initialize()
            {
                CreateLoader();

                _Databases = new Lazy<AFDatabases>(() =>
                {
                    return new AFDatabases(_ServerConnector.GetDatabases(_Connection, _ID));
                }, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);
            }

            private void CreateLoader()
            {
                if(_Connection is WebAPI.WebAPIConnection)
                {
                    _ServerConnector = new WebAPI.AFServerConnector();
                }
            }
        #endregion

        #region "Static Methods"
            /// <summary>
            /// Retrieves an AFServer object requested by name.
            /// </summary>
            /// <param name="Connection">Connection to API Endpoint.</param>
            /// <param name="Name">The ID of the AFServer requested.</param>
            /// <returns></returns>
            public static AFServer FindByName(Connection Connection, string Name)
            {
                return _ServerConnector.FindByName(Connection, Name);
            }
            
            /// <summary>
            /// Retrieves an AFServer object requested by ID.
            /// </summary>
            /// <param name="Connection">Connection to API Endpoint.</param>
            /// <param name="ID">The ID of the AFServer requested.</param>
            /// <returns></returns>
            public static AFServer Find(Connection Connection, string ID)
            {
                return _ServerConnector.Find(Connection, ID);
            }

            /// <summary>
            /// Retrieves a list of all existing AFServers.
            /// </summary>
            /// <param name="Connection">Connection to API Endpoint.</param>
            /// <returns></returns>
            public static List<AFServer> GetList(Connection Connection)
            {
                return new List<AFServer>(_ServerConnector.List(Connection));
            }
        #endregion
    }
}
