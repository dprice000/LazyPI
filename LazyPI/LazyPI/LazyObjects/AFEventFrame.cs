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
        private DateTime _StartTime;
        private DateTime _EndTime;
        private Lazy<AFElementTemplate> _Template;
        private Lazy<AFEventFrames> _EventFrames;
        private Lazy<AFAttributes> _Attributes;
        private ObservableCollection<string> _CategoryNames;
        private static IAFEventFrameController _EventFrameLoader;

        #region "Properties"
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

            internal AFEventFrame(Connection Connection, string ID, string Name, string Description, string Path) 
                : base(Connection, ID, Name, Description, Path)
            {
                Initialize();
            }

            private void Initialize()
            {
                CreateLoader();

                _Template = new Lazy<AFElementTemplate>(() =>
                {
                   var templateName = _EventFrameLoader.GetEventFrameTemplate(_Connection, this._ID);
                   return AFElementTemplate.FindByName(templateName);
                }, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

                _EventFrames = new Lazy<AFEventFrames>(() => {
                    List<AFEventFrame> frames = _EventFrameLoader.GetEventFrames(_Connection, _ID).ToList();
                    AFEventFrames obsList = new AFEventFrames(frames);

                    return obsList;
                }, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

                _Attributes = new Lazy<AFAttributes>(() => { 
                    var attrs = _EventFrameLoader.GetAttributes(_Connection, this._ID, "*", "*", "*", "*", false, "Name", "Ascending", 0, false, false, 1000);
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

            public static bool Delete(Connection Connection, string FrameID)
            {
                return _EventFrameLoader.Delete(Connection, FrameID);
            }
        #endregion

        #region "Public Methods"
            public void CheckIn()
            {
                _EventFrameLoader.Update(_Connection, this);
            }
        #endregion

            #region "Callbacks"
            private void AttributesChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                AFAttribute.Create(_Connection, this._ID, (AFAttribute)sender);
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                AFAttribute obj = (AFAttribute)sender;
                AFAttribute.Delete(_Connection, obj.ID);
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
            {
                throw new NotImplementedException("Replace is not supported by LazyPI.");
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
            {
                throw new NotImplementedException("Reset is not supported by LazyPI.");
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Move)
            {
                throw new NotImplementedException("Move is not supported by LazyPI.");
            }
        }

        private void ChildrenChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                _EventFrameLoader.CreateEventFrame(_Connection, this._ID, (AFEventFrame)sender);
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                AFEventFrame obj = (AFEventFrame)sender;
                _EventFrameLoader.Delete(_Connection, obj._ID);
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
            {
                throw new NotImplementedException("Replace is not supported by LazyPI.");
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
            {
                throw new NotImplementedException("Reset is not supported by LazyPI.");
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Move)
            {
                throw new NotImplementedException("Move is not supported by LazyPI.");
            }
        }
        #endregion
    }
}
