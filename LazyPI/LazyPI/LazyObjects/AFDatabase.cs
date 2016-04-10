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
        //private Lazy<List<AFElement>> _Elements;
        //private Lazy<List<AFEventFrame>> _EventFrames;

        #region "Constructors"
            internal AFDatabase(Connection Connection, string ID, string Name, string Description, string Path) : base(Connection, ID, Name, Description, Path)
            {
            }

            private void Intialize()
            {

            }
        #endregion

    }

    internal class AFDatabaseCollection
    {
        private List<AFDatabase> _Databases;

        #region "Constructors"
            internal AFDatabaseCollection(IEnumerable<AFDatabase> databases)
            {
                _Databases = new List<AFDatabase>(databases);
            }
        #endregion

        #region "Properties"

        public AFDatabase this[string Name]
        {
            get
            {
                return _Databases.Single(x => x.Name == Name);
            }
        }

        public AFDatabase this[int Index]
        {
            get
            {
                return _Databases[Index];
            }
        }
        #endregion
    }
}
