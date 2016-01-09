using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public interface IAFElementTemplate
    {
        AFElementTemplate Find(string templateID);
        AFElementTemplate FindByPath(string path);
        bool Update(AFElementTemplate template);
        bool Delete(string templateID);
        bool CreateElementTemplate(string parentID, AFElementTemplate template);
        IEnumerable<AFAttributeTemplate> GetAttributeTemplates(string elementID);
    }
}
