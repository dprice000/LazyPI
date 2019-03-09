using LazyPI.Common;
using LazyPI.LazyObjects;
using System;
using System.Collections.Generic;

namespace LazyPI_Test.WebAPI
{
    public class ElementController : IAFElementController
    {
        private List<AFElement> _elements = new List<AFElement>();
        private List<AFAttribute> _attrs = new List<AFAttribute>();
        private List<AFEventFrame> _eventframes = new List<AFEventFrame>();

        public AFElement Find(Connection Connection, string ElementID)
        {
            var ele = _elements.Find(x => x.ID == ElementID);

            return ele;
        }

        public AFElement FindByPath(Connection Connection, string Path)
        {
            var ele = _elements.Find(x => x.Path == Path);

            return ele;
        }

        public bool Update(Connection Connection, AFElement Element)
        {
            var org = _elements.Find(x => x.ID == Element.ID);

            int index = _elements.IndexOf(org);

            _elements[index] = org;

            return true;
        }

        public bool Delete(Connection Connection, string ElementID)
        {
            var ele = _elements.Find(x => x.ID == ElementID);

            _elements.Remove(ele);

            return true;
        }

        public bool CreateAttribute(Connection Connection, string ParentID, AFAttribute Attr)
        {
            _attrs.Add(Attr);

            return true;
        }

        public bool CreateChildElement(Connection Connection, string ParentID, AFElement Element)
        {
            _elements.Add(Element);

            return true;
        }

        public string GetElementTemplate(Connection Connection, string ElementID)
        {
            return "FakeTemplateName";
        }

        public IEnumerable<string> GetCategories(Connection Connection, string ElementID)
        {
            //TODO: we need to mock this
            return null;
        }

        public IEnumerable<AFAttribute> GetAttributes(Connection Connection, string ElementID, string NameFilter = "*", string CategoryName = "*", string TemplateName = "*", string ValueType = "*", bool SearchFullHierarchy = false, string SortField = "Name", string SortOrder = "Ascending", int StartIndex = 0, bool ShowExcluded = false, bool ShowHidden = false, int MaxCount = 1000)
        {
            return _attrs;
        }

        public IEnumerable<AFElement> GetElements(Connection Connection, string RootID, string NameFilter = "*", string CategoryName = "*", string TemplateName = "*", ElementType ElementType = ElementType.Any, bool SearchFullHierarchy = false, string SortField = "Name", string SortOrder = "Ascending", int StartIndex = 0, int MaxCount = 1000)
        {
            return _elements;
        }

        public IEnumerable<AFEventFrame> GetEventFrames(Connection Connection, string ElementID, SearchMode SearchMode, DateTime StartTime, DateTime EndTime, string NameFilter, string CategoryName, string TemplateName, string SortField, string SortOrder, int StartIndex, int MaxCount)
        {
            return _eventframes;
        }
    }
}