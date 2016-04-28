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
        private IAFDatabaseController _DBController;

        private AFElements _Elements;
        private AFEventFrames _EventFrames;

        public AFElements Elements
        {
            get
            {
                if(_Elements == null)
                    _Elements = new AFElements(_DBController.GetElements(_Connection, _WebID));

                return _Elements;
            }
        }

        public AFEventFrames EventFrames
        {
            get
            {
                if(_EventFrames == null)
                    _EventFrames = new AFEventFrames(_DBController.GetEventFrames(_Connection, _WebID));

                return _EventFrames;
            }
        }

        #region "Constructors"
            internal AFDatabase(Connection Connection, string WebID, string ID, string Name, string Description, string Path) : base(Connection, WebID, ID, Name, Description, Path)
            {
                Initialize();
            }

            private void Initialize()
            {
                _DBController = GetController(_Connection);
            }

            private static IAFDatabaseController GetController(Connection Connection)
            {
                IAFDatabaseController result = null;

                if(Connection is WebAPI.WebAPIConnection)
                    result = new WebAPI.AFDatabaseController();

                return result;
            }
        #endregion

        #region "Interacitons"
            public bool CreateElement(AFElement Element)
            {
                return _DBController.CreateElement(_Connection, _WebID, Element);
            }

            public bool CreateEventFrame(AFEventFrame Frame)
            {
                return _DBController.CreateEventFrame(_Connection, _WebID, Frame);
            }

            public void CheckIn()
            {
                _DBController.Update(_Connection, this);
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
