using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public class IAFAttributeCategory
    {
        public AFAttributeCategory Find(string ID);
        public AFAttributeCategory FindByPath(string Path);
        public bool Update(AFAttributeCategory Category);
        public bool Delete(string ID);
    }
}
