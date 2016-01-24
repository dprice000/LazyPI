using LazyPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public interface IAFElementTemplate
    {
        AFElementTemplate Find(Connection Connection, string templateID);
        AFElementTemplate FindByPath(Connection Connection, string path);
        bool Update(Connection Connection, AFElementTemplate template);
        bool Delete(Connection Connection, string templateID);
        bool CreateElementTemplate(Connection Connection, string parentID, AFElementTemplate template);
        bool IsExtendible(Connection Connection, string templateID);
        IEnumerable<AFElementCategory> GetCategories(Connection Connection, string templateID);
        IEnumerable<AFAttributeTemplate> GetAttributeTemplates(Connection Connection, string elementID);
    }
}
