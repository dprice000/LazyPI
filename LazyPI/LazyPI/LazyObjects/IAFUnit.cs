using LazyPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public interface IAFUnit
    {
        BaseObject Find(Connection Connection, string ID);
        BaseObject FindByPath(Connection Connection, string Path);
        bool Update(Connection Connection, AFUnit unit);
        bool Delete(Connection Connection, string ID);
    }
}
