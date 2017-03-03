using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazyPI.LazyObjects;

namespace LazyPI.Test
{
    public class AFAttributeConnector
    {
        public AFAttribute Find(LazyPI.Common.Connection Connection, string ID)
        {
            AFAttribute attr = new AFAttribute(Connection, ID, "Fake Attribute", "This attribute does not exist", "Fake Path");

            return attr;
        }

        public AFAttribute FindByPath(LazyPI.Common.Connection Connection, string Path)
        {
            AFAttribute attr = new AFAttribute(Connection, "000-000", "Fake Attribute", "This attribute does not exist", Path);

            return attr;
        }
        public bool Update(LazyPI.Common.Connection Connection, AFAttribute Attr)
        {
            return true;
        }

        public bool Delete(LazyPI.Common.Connection Connection, string ID)
        {
            return true;
        }

        public bool Create(LazyPI.Common.Connection Connection, string ParentID, AFAttribute Attr)
        {
            return true;
        }

        public AFValue GetValue(LazyPI.Common.Connection Connection, string AttrID)
        {
            AFValue val = new AFValue(DateTime.Now, "Fake Value", "String", true, true, true);

            return val;
        }

        public bool SetValue(LazyPI.Common.Connection Connection, string AttrID, AFValue Value)
        {
            return true;
        }
    }
}
