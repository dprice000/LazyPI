using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public interface IAFUnit
    {
        public BaseObject Find(string ID);
        public BaseObject FindByPath(string ID);
        public bool Update(AFUnit unit);
        public bool Delete(string ID);
    }
}
