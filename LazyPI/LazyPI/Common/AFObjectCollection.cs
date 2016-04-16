using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.Common
{
    public abstract class AFObjectCollection<T>
    {
        protected ObservableCollection<T> _objects;

        #region "Constructors"
            public AFObjectCollection(IEnumerable<T> objects)
            {
                _objects = new ObservableCollection<T>(objects);
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
