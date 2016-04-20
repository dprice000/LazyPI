using LazyPI.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public class AFEventFrames : ObservableCollection<AFEventFrame>
    {
        public AFEventFrame this[string Name]
        {
            get
            {
                return this.Single(x => x.Name == Name);
            }
        }

        internal AFEventFrames(IEnumerable<AFEventFrame> frames)
            : base(frames)
        {
        }
    }

    public class AFEventFrame : BaseObject
    {
        private bool _IsNew;
        private bool _IsDirty;
        private bool _IsDeleted;
        private DateTime _StartTime;
        private DateTime _EndTime;
        private Lazy<AFElementTemplate> _Template;
        private Lazy<AFEventFrames> _EventFrames;
        private Lazy<AFAttributes> _Attributes;
        private ObservableCollection<string> _CategoryNames;
        private static IAFEventFrameController _EventFrameLoader;

        #region "Properties"
        public bool IsNew
        {
            get
            {
                return _IsNew;
            }
        }

        public bool IsDirty
        {
            get
            {
                return _IsDirty;
            }
        }

        public bool IsDeleted
        {
            get
            {
                return _IsDeleted;
            }
        }

        public DateTime StartTime
        {
            get
            {
                return _StartTime;
            }
            set
            {
                _StartTime = value;
            }
        }

        public DateTime EndTime
        {
            get
            {
                return _EndTime;
            }
            set
            {
                _EndTime = value;
            }
        }

        public AFElementTemplate Template
        {
            get
            {
                return _Template.Value;
            }
        }

        public IEnumerable<string> CategoryNames
        {
            get
            {
                return _CategoryNames;
            }
        }
        #endregion

        #region "Constructors"
            public AFEventFrame()
            {
            }

            internal AFEventFrame(Connection Connection, string WebID, string ID, string Name, string Description, string Path) 
                : base(Connection, WebID, ID, Name, Description, Path)
            {
                Initialize();
            }

            private void Initialize()
            {
                CreateLoader();

                _Template = new Lazy<AFElementTemplate>(() =>
                {
                   var templateName = _EventFrameLoader.GetEventFrameTemplate(_Connection, _WebID);
                   return AFElementTemplate.FindByName(templateName);
                }, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

                _EventFrames = new Lazy<AFEventFrames>(() => {
                    List<AFEventFrame> frames = _EventFrameLoader.GetEventFrames(_Connection, _WebID).ToList();
                    AFEventFrames obsList = new AFEventFrames(frames);

                    return obsList;
                }, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

                _Attributes = new Lazy<AFAttributes>(() => { 
                    var attrs = _EventFrameLoader.GetAttributes(_Connection, _WebID, "*", "*", "*", "*", false, "Name", "Ascending", 0, false, false, 1000);
                    AFAttributes obsList = new AFAttributes(attrs);

                    return obsList;
                }, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);
            }

            private void CreateLoader()
            {
                if (_Connection is WebAPI.WebAPIConnection)
                {
                    _EventFrameLoader = new WebAPI.AFEventFrameController();
                }
            }
        #endregion

        #region "Static Methods"
            public static AFEventFrame Find(Connection Connection, string ID)
            {
                return _EventFrameLoader.Find(Connection, ID);
            }

            public static AFEventFrame FindByPath(Connection Connection, string Path)
            {
                return _EventFrameLoader.FindByPath(Connection, Path);
            }
        #endregion

        #region "Public Methods"
            public void CheckIn()
            {
                if (_IsDeleted)
                {
                    _EventFrameLoader.Delete(_Connection, _WebID);
                }
                else if(_IsDirty)
                {
                    _EventFrameLoader.Update(_Connection, this);
                }
            }      

            /// <summary>
            /// Sets the 
            /// </summary>
            /// <returns></returns>
            public void Delete()
            {
                _IsDeleted = true;
            }
        #endregion
    }
}
