using LazyPI.Common;
using System.Collections.Generic;

namespace LazyPI.LazyObjects
{
    internal interface IAFAttributeTemplateController
    {
        AFAttributeTemplate Find(Connection Connection, string AttrTempID);

        AFAttributeTemplate FindByPath(Connection Connection, string Path);

        bool Update(Connection Connection, AFAttributeTemplate AttrTemp);

        bool Delete(Connection Connection, string AttrTempID);

        bool Create(Connection Connection, AFAttributeTemplate AttrTemp);

        IEnumerable<AFAttributeTemplate> GetChildAttributeTemplates(Connection Connection, string AttrTempID);
    }
}