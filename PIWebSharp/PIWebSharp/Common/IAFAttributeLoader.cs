using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIWebSharp
{
    public interface IAFAttributeLoader
    {
        public AFAttribute Find(string webID);
        public AFAttribute FindByPath(string path);
        public bool Update(AFAttribute attr);
        public bool Delete(string webID);
        public bool Create(string parentWID, AFAttribute attr);
        public AFValue GetValue(string attrWID);
        public bool SetValue(string attrWID, AFValue value);
    }
}
