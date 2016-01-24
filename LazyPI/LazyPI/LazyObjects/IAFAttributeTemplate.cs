using LazyPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    interface IAFAttributeTemplate
    {
        BaseObject Find(Connection Connection, string ID);
        BaseObject FindByPath(Connection Connection, string path);
        bool Update(Connection Connection, AFAttributeTemplate attrTemp);
        bool Delete(Connection Connection, string ID);
        bool Create(Connection Connection, AFAttributeTemplate attrTemp);
        IEnumerable<AFAttributeTemplate> GetChildAttributeTemplates(Connection Connection, string ID);
    }
}
