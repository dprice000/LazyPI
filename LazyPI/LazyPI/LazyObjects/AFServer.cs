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
        private static IAFServer _ServerLoader;
        private static Lazy<List<AFDatabase>> _Databases;

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

            public List<AFDatabase> Database
            {
                get
                {
                    return _Databases.Value;
                }
            }
        #endregion

        #region "Constructors"
            public AFServer(Connection Connection, string ID, string Name, string Description, string Path) : base(Connection, ID, Name, Description, Path)
            {
            }

            private void CreateLoader(Connection Connection)
            {
                if(Connection is WebAPI.WebAPIConnection)
                {
                   // _ServerLoader = new WebAPI.AFS
                }
            }
        #endregion

        #region "Static Methods"
        #endregion
    }
}
