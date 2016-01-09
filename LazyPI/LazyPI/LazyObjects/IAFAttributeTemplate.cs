using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    interface IAFAttributeTemplate
    {
        AFAttributeTemplate Find(string ID);
        AFAttributeTemplate FindByPath(string path);
        bool Update(AFAttributeTemplate attrTemp);
        bool Delete(string ID);
        bool Create(AFAttributeTemplate attrTemp);
        IEnumerable<AFAttributeTemplate> GetChildAttributeTemplates(string ID);
    }
}
