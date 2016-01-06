using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public interface IAFElementTemplate
    {
        public AFElementTemplate Find(string templateID);
        public AFElementTemplate FindByPath(string path);
        public bool Update(AFElementTemplate template);
        public bool Delete(string templateID);
        public bool CreateElementTemplate(string parentID, AFElementTemplate template);
        public IEnumerable<AFAttributeTemplate> GetAttributeTemplates(string elementID);
    }
}
