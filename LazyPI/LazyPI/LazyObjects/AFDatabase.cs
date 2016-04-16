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
        private IAFDatabase _DBLoader;

        public AFElements Elements
        {
            get
            {
                return new AFElements(_DBLoader.GetElements(_Connection));
            }
        }

        public AFEventFrames EventFrames
        {
            get
            {
                return new AFEventFrames(_DBLoader.GetEventFrames(_Connection));
            }
        }

        #region "Constructors"
            internal AFDatabase(Connection Connection, string ID, string Name, string Description, string Path) : base(Connection, ID, Name, Description, Path)
            {
                Initialize();
            }

            private void Initialize()
            {
                if(_Connection is LazyPI.WebAPI.WebAPIConnection)
                    _DBLoader = new LazyPI.WebAPI.AFDatabaseConnector();
            }
        #endregion

        #region "Interacitons"
            public bool CreateElement(AFElement Element)
            {
                return _DBLoader.CreateElement(_Connection, Element);
            }

            public bool CreateEventFrame(AFEventFrame Frame)
            {
                return _DBLoader.CreateEventFrame(_Connection, Frame);
            }

            public void CheckIn()
            {
                _DBLoader.Update(_Connection, this);
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
