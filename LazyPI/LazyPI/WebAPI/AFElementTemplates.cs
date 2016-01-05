using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.WebAPI
{
    public static class AFElementTemplates
    {
        private IAFElementTemplateLoader _loader;

        public AFElementTemplates()
        {
        }

        public AFElementTemplate Find(string ID)
        {
            return _loader.Find(ID);
        }

        public AFElementTemplate FindByPath(string path)
        {
            return _loader.FindByPath(path);
        }

        public bool Delete(string ID)
        {
            return _loader.Delete(ID);
        }

        public bool CreateElementTemplate(string parentID, AFElementTemplate template)
        {
            return _loader.CreateElementTemplate(parentID, template);
        }

        public IEnumerable<AFAttributeTemplate> GetAttributesTemplates(string elementID)
        {
            return _loader.GetAttributeTemplates(elementID);
        }
    }
}
