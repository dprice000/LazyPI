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

        public void CheckIn()
        {
            _AttrLoader.Update(this);
        }
    }
}
