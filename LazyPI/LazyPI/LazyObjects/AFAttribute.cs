using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public class AFAttribute : BaseObject
    {
        private LazyPI.Common.Connection _Connection;
        private string _UnitsName;
        private string _AttrType;
        private IEnumerable<string> _Categories;
        private string _ConfigString;
        private static IAFAttribute _AttrLoader;

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

        /// <summary>
        /// Current value held by the attribute.
        /// </summary>
        public AFValue Value
        {
            get
            {
                return _AttrLoader.GetValue(this.ID);
            }
            set
            {
                _AttrLoader.SetValue(this.ID, value);
            }
        }
        #endregion

        #region "Constructors"

        private AFAttribute(string ID, string Name, string Description, string Path)
        {
            this._ID = ID;
            this._Name = Name;
            this._Description = Description;
            this._Path = Path;
        }

        public AFAttribute(string ID)
        {
            BaseObject baseObj = _AttrLoader.Find(ID);
            this._ID = baseObj.ID;
            this._Name = baseObj.Name;
            this._Description = baseObj.Description;
            this._Path = baseObj.Path;
        }

        /// <summary>
        /// Initialize all lazy loaded objects
        /// </summary>
        public void Initialize()
        {

        }
        #endregion

        #region "Interactions"

        /// <summary>
        /// Updates the AF Database to match all changes made to attribute.
        /// </summary>
        public void CheckIn()
        {
            _AttrLoader.Update(this);
        }

        /// <summary>
        /// Gets the an AFValue with the current value held in the attribute.
        /// </summary>
        /// <returns>Returns a complete AFAttribute.</returns>
        public AFValue GetValue()
        {
            return _AttrLoader.GetValue(this._ID);
        }

        /// <summary>
        /// Sets the value of the attribute.
        /// </summary>
        /// <param name="Value">A partial AFValue that should be created.</param>
        /// <returns>Returns true if no errors occur.</returns>
        public bool SetValue(AFValue Value)
        {
           return _AttrLoader.SetValue(this._ID, Value);
        }

        #endregion

        #region "Static Functions"
            /// <summary>
            /// Creates element using the partial Attribute provided.
            /// </summary>
            /// <param name="ElementID">The element that will hold the attribute.</param>
            /// <param name="Attr">The partial attribute holding information to create attribute.</param>
            /// <returns>Returns true if creation completes properly.</returns>
            /// <remarks>It is expected that the attribute will not have ID and Path</remarks>
            public static bool Create(string ElementID, AFAttribute Attr)
            {
                return _AttrLoader.Create(ElementID, Attr);
            }
            
            /// <summary>
            /// Finds attribute with the ID specified.
            /// </summary>
            /// <param name="ID">The unique ID of the attribute.</param>
            /// <returns>Returns a complete AFAttribute.</returns>
            public static AFAttribute Find(string ID)
            {
                return AttributeFactory.CreateInstance(_AttrLoader.Find(ID));
            }

            /// <summary>
            /// Uses Path to find AFAttribute.
            /// </summary>
            /// <param name="Path">The full Path to the AFAttribute.</param>
            /// <returns></returns>
            public static AFAttribute FindByPath(string Path)
            {
                return AttributeFactory.CreateInstance(_AttrLoader.FindByPath(Path));
            }


            /// <summary>
            /// Deletes attribute specified by ID.
            /// </summary>
            /// <param name="ID">The unique ID of the attribute to be deleted.</param>
            /// <returns></returns>
            public static bool Delete(string ID)
            {
                return _AttrLoader.Delete(ID);
            }
        #endregion

        /// <summary>
        /// Factory for creating raw AFAttribute objects.
        /// </summary>
        /// <remarks>
        /// This should be the only way to create a complete AFAttribute.
        /// </remarks>
        public class AttributeFactory
        {
            public static AFAttribute CreateInstance(BaseObject bObj)
            {
                return new AFAttribute(bObj.ID, bObj.Name, bObj.Description, bObj.Path);
            }

            public static AFAttribute CreateInstance(string ID, string Name, string Description, string Path)
            {
                return new AFAttribute(ID, Name, Description, Path);
            }
        }
    }
}
