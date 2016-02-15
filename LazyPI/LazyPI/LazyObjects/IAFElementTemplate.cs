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
        AFElementTemplate Find(Connection Connection, string TemplateID);
        AFElementTemplate FindByPath(Connection Connection, string Path);
        bool Update(Connection Connection, AFElementTemplate Template);
        bool Delete(Connection Connection, string TemplateID);
        bool CreateElementTemplate(Connection Connection, string ParentID, AFElementTemplate Template);
        bool IsExtendible(Connection Connection, string TemplateID);
        IEnumerable<AFAttributeTemplate> GetAttributeTemplates(Connection Connection, string ElementID);
    }
}
