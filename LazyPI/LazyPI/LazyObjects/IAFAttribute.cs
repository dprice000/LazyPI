using LazyPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public interface IAFAttribute
    {
        BaseObject Find(Connection Connection, string ID);
        BaseObject FindByPath(Connection Connection, string path);
        bool Update(Connection Connection, AFAttribute attr);
        bool Delete(Connection Connection, string webID);
        bool Create(Connection Connection, string parentID, AFAttribute attr);
        AFValue GetValue(Connection Connection, string attrID);
        bool SetValue(Connection Connection, string attrID, AFValue value);
    }
}
