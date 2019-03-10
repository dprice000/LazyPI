using LazyPI.Common;
using LazyPI.LazyObjects;
using System;
using System.Collections.Generic;

namespace LazyPI_Test.WebAPI.Dummies
{
    public class ElementController : IAFElementController
    {
        public List<AFElement> Elements { get; private set; }
        public List<AFAttribute> Attributes { get; private set; }
        public List<AFEventFrame> EventFrames { get; private set; }

        public ElementController()
        {
            Elements = DataGenerator.Elements;
            Attributes = DataGenerator.Attributes;
            EventFrames = DataGenerator.EventFrames;
        }

        public AFElement Find(Connection Connection, string ElementID)
        {
            var ele = Elements.Find(x => x.ID == ElementID);
            return ele;
        }

        public AFElement FindByPath(Connection Connection, string Path)
        {
            var ele = Elements.Find(x => x.Path == Path);
            return ele;
        }

        public bool Update(Connection Connection, AFElement Element)
        {
            var org = Elements.Find(x => x.ID == Element.ID);
            int index = Elements.IndexOf(org);

            Elements[index] = org;

            return true;
        }

        public bool Delete(Connection Connection, string ElementID)
        {
            var ele = Elements.Find(x => x.ID == ElementID);

            Elements.Remove(ele);

            return true;
        }

        public bool CreateAttribute(Connection Connection, string ParentID, AFAttribute Attr)
        {
            Attributes.Add(Attr);

            return true;
        }

        public bool CreateChildElement(Connection Connection, string ParentID, AFElement Element)
        {
            Elements.Add(Element);

            return true;
        }

        public string GetElementTemplate(Connection Connection, string ElementID)
        {
            return "Template1";
        }

        public IEnumerable<string> GetCategories(Connection Connection, string ElementID)
        {
            return new string[] { "FakeCat1", "FakeCat2", "FakeCat3" };
        }

        public IEnumerable<AFAttribute> GetAttributes(Connection Connection, string ElementID, string NameFilter = "*", string CategoryName = "*", string TemplateName = "*", string ValueType = "*", bool SearchFullHierarchy = false, string SortField = "Name", string SortOrder = "Ascending", int StartIndex = 0, bool ShowExcluded = false, bool ShowHidden = false, int MaxCount = 1000)
        {
            return Attributes;
        }

        public IEnumerable<AFElement> GetElements(Connection Connection, string RootID, string NameFilter = "*", string CategoryName = "*", string TemplateName = "*", ElementType ElementType = ElementType.Any, bool SearchFullHierarchy = false, string SortField = "Name", string SortOrder = "Ascending", int StartIndex = 0, int MaxCount = 1000)
        {
            return Elements;
        }

        public IEnumerable<AFEventFrame> GetEventFrames(Connection Connection, string ElementID, SearchMode SearchMode, DateTime StartTime, DateTime EndTime, string NameFilter, string CategoryName, string TemplateName, string SortField, string SortOrder, int StartIndex, int MaxCount)
        {
            return EventFrames;
        }
    }
}