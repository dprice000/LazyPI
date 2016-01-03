using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    interface IAFAttributeTemplateLoader
    {
        public AFAttributeTemplate Find(string ID);
        public AFAttributeTemplate FindByPath(string path);
        public bool Update(AFAttributeTemplate attrTemp);
        public bool Delete(AFAttributeTemplate attrTemp);
        public bool Create(AFAttributeTemplate attrTemp);
        public IEnumerable<AFAttributeTemplate> GetChildAttributeTemplates(string ID);
    }
}
