using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public interface IAFAttribute
    {
        AFAttribute Find(string ID);
        AFAttribute FindByPath(string path);
        bool Update(AFAttribute attr);
        bool Delete(string webID);
        bool Create(string parentID, AFAttribute attr);
        AFValue GetValue(string attrID);
        bool SetValue(string attrID, AFValue value);
    }
}
