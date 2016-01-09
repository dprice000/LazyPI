using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public static class AFElementTemplates
    {
        private static IAFElementTemplate _loader;

        public static AFElementTemplate Find(string ID)
        {
            return _loader.Find(ID);
        }

        public static AFElementTemplate FindByPath(string path)
        {
            return _loader.FindByPath(path);
        }

        public static bool Delete(string ID)
        {
            return _loader.Delete(ID);
        }

        public static bool CreateElementTemplate(string parentID, AFElementTemplate template)
        {
            return _loader.CreateElementTemplate(parentID, template);
        }

        public static IEnumerable<AFAttributeTemplate> GetAttributesTemplates(string elementID)
        {
            return _loader.GetAttributeTemplates(elementID);
        }
    }
}
