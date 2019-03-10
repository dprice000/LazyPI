using LazyPI.Common;
using LazyPI.LazyObjects;
using System;
using System.Collections.Generic;

namespace LazyPI_Test.WebAPI.Dummies
{
    public class ElementTemplateController : IAFElementTemplateContoller
    {

        public List<AFElementTemplate> Templates { get; private set; }

        public ElementTemplateController()
        {
            FixtureWrapper fixture = new FixtureWrapper();
            Templates = new List<AFElementTemplate>();

            var template1 = fixture.Create<AFElementTemplate>();
            template1.Name = "Template1";
            Templates.Add(template1);

            var template2 = fixture.Create<AFElementTemplate>();
            template2.Name = "Template2";
            Templates.Add(template2);

            var template3 = fixture.Create<AFElementTemplate>();
            template3.Name = "Template3";
            Templates.Add(template3);
        }


        public AFElementTemplate Find(Connection Connection, string TemplateID)
        {
            return Templates.Find(x => x.WebID == TemplateID);
        }

        public AFElementTemplate FindByPath(Connection Connection, string Path)
        {
            return Templates.Find(x => x.Path == Path);
        }

        public bool Update(Connection Connection, AFElementTemplate Template)
        {
            var template = Templates.Find(x => x.WebID == Template.WebID);
            int index = Templates.IndexOf(template);
            Templates[index] = Template;
            return true;
        }

        public bool Delete(Connection Connection, string TemplateID)
        {
            var template = Templates.Find(x => x.WebID == TemplateID);
            Templates.Remove(template);
            return true;
        }

        public bool CreateElementTemplate(Connection Connection, string ParentID, AFElementTemplate Template)
        {
            throw new NotImplementedException("CreateElementTemplate not implemented.");
        }

        public bool IsExtendible(Connection Connection, string TemplateID)
        {
            return Templates.Find(x => x.WebID == TemplateID).IsExtendable;
        }

        public IEnumerable<string> GetCategories(Connection Connection, string TemplateID)
        {
            return Templates.Find(x => x.WebID == TemplateID).Categories;
        }

        public IEnumerable<AFAttributeTemplate> GetAttributeTemplates(Connection Connection, string ElementID)
        {
            throw new NotImplementedException("GetAttributeTemplates not implemented.");
        }
    }
}
