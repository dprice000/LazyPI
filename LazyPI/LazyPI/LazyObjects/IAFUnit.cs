using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public interface IAFUnit
    {
        BaseObject Find(string ID);
        BaseObject FindByPath(string Path);
        bool Update(AFUnit unit);
        bool Delete(string ID);
    }
}
