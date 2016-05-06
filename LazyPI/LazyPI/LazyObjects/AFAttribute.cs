using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazyPI.Common;

namespace LazyPI.LazyObjects
{
    public class AFAttributes : System.Collections.ObjectModel.Collection<AFAttribute>
    {
        public AFAttribute this[string Name]
        {
            get
            {
                return this.SingleOrDefault(x => x.Name == Name);
            }
        }

        internal AFAttributes(IList<AFAttribute> attributes) : base(attributes)
        {
        }

        protected override void InsertItem(int index, AFAttribute item)
        {
            item.IsNew = true;
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            this[index].IsDeleted = true;
        }
    }

    public class AFAttribute : BaseObject
    {
        private bool _IsNew;
        private bool _IsDirty;
        private bool _IsDeleted;
        private string _UnitsName;
        private string _AttrType;
        private IEnumerable<string> _Categories;
        private string _DataReferencePlugin;
        private string _ConfigString;
        private AFAttributes _Attributes;
        private static IAFAttributeController _AttrController;

        #region "Properties"
        public bool IsNew
        {
            get
            {
                return _IsNew;
            }
            internal set
            {
                _IsNew = value;
            }
        }

        public bool IsDeleted
        {
            get
            {
                return _IsDeleted;
            }
            internal set
            {
                _IsDeleted = value;
            }
        }

        public bool IsDirty
        {
            get
            {
                return _IsDirty; 
            }
            internal set
            {
                _IsDirty = value;
            }
        }

        public IEnumerable<string> Categories
        {
            get
            {
                return _Categories;
            }

            //TODO: Implement setter
        }

        public string ConfigString
        {
            get
            {
                return _ConfigString;
            }
            set
            {
                _ConfigString = value;
            }
        }

        public string UnitsName
        {
            get
            {
                return _UnitsName;
            }
            set
            {
                _UnitsName = value;
            }
        }

        public string DataReferencePlugIn
        {
            get
            {
                return _DataReferencePlugin;
            }
            set
            {
                _DataReferencePlugin = value;
            }
        }

        public string Type
        {
            get
            {
                return _AttrType;
            }
            set
            {
                _AttrType = value;
            }
        }

        public AFAttributes Attributes
        {
            get
            {
                //if(_Attributes == null)
                    //  _Attributes = Get Attributes

                return _Attributes;
            }
            set
            {
                _Attributes = value;
                _IsDirty = true;
            }
        }
        #endregion

        #region "Constructors"

        public AFAttribute()
        {
        }

        internal AFAttribute(LazyPI.Common.Connection Connection,string WebID, string ID, string Name, string Description, string Path) : base(Connection, WebID, ID, Name, Description, Path)
        {
            Initialize();
        }

        internal AFAttribute(LazyPI.Common.Connection Connection, string WebID, string ID, string Name, string Description, string Path, string UnitsName, string ConfigString, string DataReferencePlugin, string AttrType, IEnumerable<string> Categories)
            : base(Connection, WebID, ID, Name, Description, Path)
        {
            Initialize();

            _Categories = Categories;
            _ConfigString = ConfigString;
            _AttrType = AttrType;
            _DataReferencePlugin = DataReferencePlugin;
            _UnitsName = UnitsName;
        }

        /// <summary>
        /// Initialize all lazy loaded objects
        /// </summary>
        private void Initialize()
        {
            _AttrController = GetController(_Connection);
        }

        private static IAFAttributeController GetController(Connection Connection)
        {
            IAFAttributeController result = null;

            if (Connection is WebAPI.WebAPIConnection)
                result = new WebAPI.AFAttributeController();

            return result;
        }
        #endregion

        #region "Interactions"

        public PIPoint GetPoint()
        {
            return _AttrController.GetPoint(_Connection, _WebID);
        }

        /// <summary>
        /// Gets the an AFValue with the current value held in the attribute.
        /// </summary>
        /// <returns>Returns a complete AFAttribute.</returns>
        public AFValue GetValue()
        {
            return _AttrController.GetValue(_Connection, _WebID);
        }

        /// <summary>
        /// Sets the value of the attribute.
        /// </summary>
        /// <param name="Value">A partial AFValue that should be created.</param>
        /// <returns>Returns true if no errors occur.</returns>
        public bool SetValue(AFValue Value)
        {
           return _AttrController.SetValue(_Connection, _WebID, Value);
        }

        public void Delete()
        {
            _AttrController.Delete(_Connection, _WebID);
        }

        public void CheckIn()
        {
            if (_IsDirty)
            {
                _AttrController.Update(_Connection, this);

                if (_IsDirty)
                {
                    foreach (AFAttribute attr in _Attributes.Where(x => x.IsNew))
                    {
                        _AttrController.CreateChild(_Connection, _WebID, attr);
                    }
                }
            }
        }
        #endregion

        #region "Static Functions"     
            public static void Create(Connection Connection, string ElementID, AFAttribute Attr)
            {
                GetController(Connection).Create(Connection, ElementID, Attr);
            }

            /// <summary>
            /// Finds attribute with the ID specified.
            /// </summary>
            /// <param name="ID">The unique ID of the attribute.</param>
            /// <returns>Returns a complete AFAttribute.</returns>
            public static AFAttribute Find(Connection Connection,string ID)
            {
                return GetController(Connection).Find(Connection, ID);
            }

            /// <summary>
            /// Uses Path to find AFAttribute.
            /// </summary>
            /// <param name="Path">The full Path to the AFAttribute.</param>
            /// <returns></returns>
            public static AFAttribute FindByPath(Connection Connection, string Path)
            {
                return GetController(Connection).FindByPath(Connection, Path);
            }    

            /// <summary>
            /// Deletes attribute specified by ID.
            /// </summary>
            /// <param name="ID">The unique ID of the attribute to be deleted.</param>
            /// <returns></returns>
            public static bool Delete(Connection Connection, string ID)
            {
                return GetController(Connection).Delete(Connection, ID);
            }
        #endregion
    }
}
