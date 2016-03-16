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
        #endregion

        #region "Constructors"
            public AFServer(Connection Connection, string ID, string Name, string Description, string Path) : base(Connection, ID, Name, Description, Path)
            {
            }
        #endregion
    }
}
