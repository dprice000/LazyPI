using LazyPI.Common;
using System.Collections.Generic;

namespace LazyPI.LazyObjects
{
    public class AFServer : BaseObject
    {
        private static IAFServerController _ServerController;
        private static AFDatabases _Databases;

        #region "Properties"

        public bool IsConnected { get; }

        public string ServerVersion { get; }

        public AFDatabases Databases
        {
            get
            {
                if (_Databases == null)
                {
                    _Databases = new AFDatabases(_ServerController.GetDatabases(_Connection, _WebID));
                }

                return _Databases;
            }
        }

        #endregion "Properties"

        #region "Constructors"

        internal AFServer(Connection Connection, string WebID, string ID, string Name, string Description, bool IsConnected, string ServerVersion, string Path) : base(Connection, WebID, ID, Name, Description, Path)
        {
            Initialize();
            this.IsConnected = IsConnected;
            this.ServerVersion = ServerVersion;
        }

        private void Initialize()
        {
            _ServerController = GetController(_Connection);
        }

        private static IAFServerController GetController(Connection Connection)
        {
            IAFServerController result = null;

            if (Connection is WebAPI.WebAPIConnection)
                result = new WebAPI.AFServerController();

            return result;
        }

        #endregion "Constructors"

        #region "Static Methods"

        /// <summary>
        /// Retrieves an AFServer object requested by name.
        /// </summary>
        /// <param name="Connection">Connection to API Endpoint.</param>
        /// <param name="Name">The ID of the AFServer requested.</param>
        /// <returns></returns>
        public static AFServer FindByName(Connection Connection, string Name)
        {
            return GetController(Connection).FindByName(Connection, Name);
        }

        /// <summary>
        /// Retrieves an AFServer object requested by ID.
        /// </summary>
        /// <param name="Connection">Connection to API Endpoint.</param>
        /// <param name="ID">The ID of the AFServer requested.</param>
        /// <returns></returns>
        public static AFServer Find(Connection Connection, string ID)
        {
            return GetController(Connection).Find(Connection, ID);
        }

        /// <summary>
        /// Retrieves a list of all existing AFServers.
        /// </summary>
        /// <param name="Connection">Connection to API Endpoint.</param>
        /// <returns></returns>
        public static List<AFServer> GetList(Connection Connection)
        {
            return new List<AFServer>(GetController(Connection).List(Connection));
        }

        #endregion "Static Methods"
    }
}