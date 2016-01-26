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
        BaseObject FindByPath(Connection Connection, string Path);
        bool Update(Connection Connection, AFAttributeTemplate AttrTemp);
        bool Delete(Connection Connection, string ID);
        bool Create(Connection Connection, AFAttributeTemplate AttrTemp);
        IEnumerable<AFAttributeTemplate> GetChildAttributeTemplates(Connection Connection, string ID);
    }
}
