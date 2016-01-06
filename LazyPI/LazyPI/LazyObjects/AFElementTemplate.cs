using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public class AFElementTemplate : BaseObject
    {
        private IEnumerable<string> _Categories;

        public IEnumerable<string> Categories
        {
            get
            {
                return _Categories;
            }
        }
    }
}
