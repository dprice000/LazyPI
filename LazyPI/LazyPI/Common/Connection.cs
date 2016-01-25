using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.Common
{
    public abstract class Connection
    {
        protected string _Hostname;
        protected string _Username;
        

        #region "Properties"
            public string Hostname
            {
                get
                {
                    return _Hostname;
                }
            }

            public string Username
            {
                get
                {
                    return _Username;
                }
            }

            public abstract object Request
            {
                get;
            }
        #endregion

    }
}
