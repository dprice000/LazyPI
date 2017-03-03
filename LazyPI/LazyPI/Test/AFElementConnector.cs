using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazyPI.LazyObjects;

namespace LazyPI.Test
{
    /// <summary>
    /// This object is purely for testing
    /// </summary>
    public class ElementTester : LazyPI.LazyObjects.IAFElement
    {
        public AFElement Find(LazyPI.Common.Connection Connection, string ElementID)
        {
            AFElement ele = new AFElement(Connection, "000-000", "Fake Element", "This element is for testing only", "AFDB\\Fake Element");
            return ele;
        }

        public AFElement FindByPath(LazyPI.Common.Connection Connection, string Path)
        {
            AFElement ele = new AFElement();



            return ele;
        }

        public void Update(LazyPI.Common.Connection Connection, AFElement Element)
        {

        }

        public void Delete(LazyPI.Common.Connection Connection, string ElementID)
        {
        }

        public bool CreateAttribute(LazyPI.Common.Connection Connection, string ParentID, AFAttribute Attr)
        {
            return true;
        }


        public bool CreateChildElement(LazyPI.Common.Connection Connection, string ParentID, AFElement Element)
        {
            return true;
        }

        public string GetElementTemplate(LazyPI.Common.Connection Connection, string ElementID)
        {
            return "FakeTemplate";
        }

        public IEnumerable<string> GetCategories(LazyPI.Common.Connection Connection, string ElementID)
        {
        }

        public IEnumerable<AFAttribute> GetAttributes(LazyPI.Common.Connection Connection, string ElementID, string NameFilter = "*", string CategoryName = "*", string TemplateName = "*", string ValueType = "*", bool SearchFullHierarchy = false, string SortField = "Name", string SortOrder = "Ascending", int StartIndex = 0, bool ShowExcluded = false, bool ShowHidden = false, int MaxCount = 1000)
        {
        }

        public IEnumerable<AFElement> GetElements(LazyPI.Common.Connection Connection, string RootID, string NameFilter = "*", string CategoryName = "*", string TemplateName = "*", LazyPI.Common.ElementType ElementType = LazyPI.Common.ElementType.Any, bool SearchFullHierarchy = false, string SortField = "Name", string SortOrder = "Ascending", int StartIndex = 0, int MaxCount = 1000)
        { 
        }

        public IEnumerable<AFEventFrame> GetEventFrames(LazyPI.Common.Connection Connection, string ElementID, LazyPI.Common.SearchMode SearchMode, DateTime StartTime, DateTime EndTime, string NameFilter, string CategoryName, string TemplateName, string SortField, string SortOrder, int StartIndex, int MaxCount)
        {
        }
    }
}
