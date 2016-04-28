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
        private IAFDatabaseController _DBLoader;

        public AFElements Elements
        {
            get
            {
                return new AFElements(_DBLoader.GetElements(_Connection, _WebID));
            }
        }

        public AFEventFrames EventFrames
        {
            get
            {
                return new AFEventFrames(_DBLoader.GetEventFrames(_Connection, _WebID));
            }
        }

        #region "Constructors"
            internal AFDatabase(Connection Connection, string WebID, string ID, string Name, string Description, string Path) : base(Connection, WebID, ID, Name, Description, Path)
            {
                Initialize();
            }

            private void Initialize()
            {
                if(_Connection is LazyPI.WebAPI.WebAPIConnection)
                    _DBLoader = new LazyPI.WebAPI.AFDatabaseController();
            }
        #endregion

        #region "Interacitons"
            public bool CreateElement(AFElement Element)
            {
                return _DBLoader.CreateElement(_Connection, _WebID, Element);
            }

            public bool CreateEventFrame(AFEventFrame Frame)
            {
                return _DBLoader.CreateEventFrame(_Connection, _WebID, Frame);
            }

            public void CheckIn()
            {
                _DBLoader.Update(_Connection, this);
            }
        #endregion
    }

    public class AFDatabases : System.Collections.ObjectModel.ObservableCollection<AFDatabase>
    {
        internal AFDatabases(IEnumerable<AFDatabase> databases) : base(databases)
        {
        }

        #region "Properties"
            public AFDatabase this[string Name]
            {
                get
                {
                    return this.SingleOrDefault(x => x.Name == Name);
                }
            }
        #endregion
    }
}
