using LazyPI.Common;
using System.Collections.Generic;
using System.Linq;

namespace LazyPI.LazyObjects
{
    public class AFDatabase : CheckInAble
    {
        private IAFDatabaseController _DBController;

        private AFElements _Elements;
        private AFEventFrames _EventFrames;

        /// <summary>
        /// Only for testing purposes.
        /// </summary>
        internal IAFDatabaseController DBController
        {
            get
            {
                return _DBController;
            }
            set
            {
                _DBController = value;
            }
        }

        public AFElements Elements
        {
            get
            {
                if (_Elements == null)
                {
                    _Elements = new AFElements(_DBController.GetElements(_Connection, WebID));
                }

                return _Elements;
            }
        }

        public AFEventFrames EventFrames
        {
            get
            {
                if (_EventFrames == null)
                {
                    _EventFrames = new AFEventFrames(_DBController.GetEventFrames(_Connection, WebID));
                }

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

            if (Connection is WebAPI.WebAPIConnection)
                result = new WebAPI.AFDatabaseController();

            return result;
        }

        #endregion "Constructors"

        #region "Interactions"

        public bool CreateElement(AFElement Element)
        {
            return _DBController.CreateElement(_Connection, WebID, Element);
        }

        public bool CreateEventFrame(AFEventFrame Frame)
        {
            return _DBController.CreateEventFrame(_Connection, WebID, Frame);
        }

        public override void CheckIn()
        {
            if (IsDirty && !IsDeleted)
            {
                _DBController.Update(_Connection, this);

                if (_Elements != null)
                {
                    foreach (AFElement ele in _Elements.Where(x => x.IsNew))
                    {
                        _DBController.CreateElement(_Connection, WebID, ele);
                    }
                }

                if (_EventFrames != null)
                {
                    foreach (AFEventFrame frame in _EventFrames.Where(x => x.IsNew))
                    {
                        _DBController.CreateEventFrame(_Connection, WebID, frame);
                    }
                }

                ResetState();
            }    
        }

        protected override void ResetState()
        {
            IsNew = false;
            IsDirty = false;
            _Elements = null;
            _EventFrames = null;
        }

        #endregion "Interactions"
    }

    public class AFDatabases : System.Collections.ObjectModel.Collection<AFDatabase>
    {
        internal AFDatabases(IEnumerable<AFDatabase> databases) : base(databases.ToList())
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

        #endregion "Properties"
    }
}