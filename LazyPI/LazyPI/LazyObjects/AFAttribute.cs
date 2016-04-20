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
                return this.Single(x => x.Name == Name);
            }
        }

        internal AFAttributes(IList<AFAttribute> attributes) : base(attributes)
        {
        }
    }

    public class AFAttribute : BaseObject
    {
        private string _UnitsName;
        private string _AttrType;
        private IEnumerable<string> _Categories;
        private string _ConfigString;
        private static IAFAttributeController _AttrLoader;

        #region "Properties"
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
        #endregion

        #region "Constructors"

        public AFAttribute()
        {
        }

        internal AFAttribute(LazyPI.Common.Connection Connection,string WebID, string ID, string Name, string Description, string Path) : base(Connection, WebID, ID, Name, Description, Path)
        {
            Initialize();
        }

        /// <summary>
        /// Initialize all lazy loaded objects
        /// </summary>
        private void Initialize()
        {
            CreateLoader();
        }

        private void CreateLoader()
        {
            if (_Connection is WebAPI.WebAPIConnection)
            {
                _AttrLoader = new WebAPI.AFAttributeController();
            }
        }
        #endregion

        #region "Interactions"
        /// <summary>
        /// Gets the an AFValue with the current value held in the attribute.
        /// </summary>
        /// <returns>Returns a complete AFAttribute.</returns>
        public AFValue GetValue()
        {
            return _AttrLoader.GetValue(_Connection, _WebID);
        }

        /// <summary>
        /// Sets the value of the attribute.
        /// </summary>
        /// <param name="Value">A partial AFValue that should be created.</param>
        /// <returns>Returns true if no errors occur.</returns>
        public bool SetValue(AFValue Value)
        {
           return _AttrLoader.SetValue(_Connection, _WebID, Value);
        }

        #endregion

        #region "Static Functions"
            /// <summary>
            /// Finds attribute with the ID specified.
            /// </summary>
            /// <param name="ID">The unique ID of the attribute.</param>
            /// <returns>Returns a complete AFAttribute.</returns>
            public static AFAttribute Find(Connection Connection,string ID)
            {
                return _AttrLoader.Find(Connection, ID);
            }

            /// <summary>
            /// Uses Path to find AFAttribute.
            /// </summary>
            /// <param name="Path">The full Path to the AFAttribute.</param>
            /// <returns></returns>
            public static AFAttribute FindByPath(Connection Connection, string Path)
            {
                return _AttrLoader.FindByPath(Connection, Path);
            }


            /// <summary>
            /// Deletes attribute specified by ID.
            /// </summary>
            /// <param name="ID">The unique ID of the attribute to be deleted.</param>
            /// <returns></returns>
            public static bool Delete(Connection Connection, string ID)
            {
                return _AttrLoader.Delete(Connection, ID);
            }
        #endregion
    }
}
