using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazyPI.Common;

namespace LazyPI.LazyObjects
{
    public class AFAttributeCategory : BaseObject
    {
        private IAFAttributeCategoryController _CategoryLoader;

        private void Initialize()
        { 
        }

        public void CheckIn()
        {
            if (_IsDirty && !_IsDeleted)
            {
                _CategoryLoader.Update(_Connection, this);
            }
        }
    }
}
