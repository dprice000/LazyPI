using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public class AFAttribute : BaseObject
    {
        private IAFAttributeLoader _AttrLoader;

        #region "Properties"
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
