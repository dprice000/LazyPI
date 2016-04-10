using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.Common
{
    abstract class ObjectCollection<T>
    {
        private List<T> _objects;

        #region "Constructors"
        //internal ObjectCollection<T>(IEnumerable<T> elements)
        //{
        //    _elements = new List<AFElement>(elements);
        //}
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
