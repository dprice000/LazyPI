using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.Common
{
    abstract class ObjectCollection<T>
    {
        protected List<T> _objects;

        #region "Constructors"
            public ObjectCollection(IEnumerable<T> objects)
            {
                _objects = new List<T>(objects);
            }
        #endregion

        #region "Properties"

        public T this[int Index]
        {
            get
            {
                return _objects[Index];
            }
        }
        #endregion
    }
}
