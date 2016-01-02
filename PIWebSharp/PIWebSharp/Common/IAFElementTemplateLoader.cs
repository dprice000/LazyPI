using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIWebSharp
{
    public interface IAFElementTemplateLoader
    {
        public AFElementTemplate Find(string templateWID);
        public AFElementTemplate FindByPath(string path);
        public bool Update(AFElementTemplate template);
        public bool Delete(string templateWID);
        public bool CreateElementTemplate(string parentWID, AFElementTemplate template);
        public IEnumerable<AFAttributeTemplate> GetAttributeTemplates(string elementID);
    }
}
