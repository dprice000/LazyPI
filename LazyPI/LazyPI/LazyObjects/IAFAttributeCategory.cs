using LazyPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public class IAFAttributeCategory
    {
        public AFAttributeCategory Find(Connection Connection, string ID);
        public AFAttributeCategory FindByPath(Connection Connection, string Path);
        public bool Update(Connection Connection, AFAttributeCategory Category);
        public bool Delete(Connection Connection, string ID);
    }
}
