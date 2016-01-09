using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public class AFAttribute : BaseObject
    {
        private string _UnitsName;
        private string _AttrType;
        private IEnumerable<string> _Categories;
        private string _ConfigString;
        private IAFAttribute _AttrLoader;

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

        public void CheckIn()
        {
            _AttrLoader.Update(this);
        }


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
