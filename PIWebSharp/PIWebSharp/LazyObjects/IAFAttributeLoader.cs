using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public interface IAFAttributeLoader
    {
        public AFAttribute Find(string ID);
        public AFAttribute FindByPath(string path);
        public bool Update(AFAttribute attr);
        public bool Delete(string webID);
        public bool Create(string parentID, AFAttribute attr);
        public AFValue GetValue(string attrID);
        public bool SetValue(string attrID, AFValue value);
    }
}
