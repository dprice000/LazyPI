using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazyPI.Common;

namespace LazyPI.LazyObjects
{
    public class AFDatabase : BaseObject
    {
        #region "Constructors"
            internal AFDatabase(Connection Connection, string ID, string Name, string Description, string Path) : base(Connection, ID, Name, Description, Path)
            {
            }

            private void Intialize()
            {

            }
        #endregion
    }

    public class AFDatabases : LazyPI.Common.AFObjectCollection<AFDatabase>
    {
        internal AFDatabases(IEnumerable<AFDatabase> databases) : base(databases)
        {
        }

        #region "Properties"

        public AFDatabase this[string Name]
        {
            get
            {
                return _objects.Single(x => x.Name == Name);
            }
        }
        #endregion
    }
}
