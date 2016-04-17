using LazyPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public interface IAFElementController
    {
        AFElement Find(Connection Connection, string ElementID);
        AFElement FindByPath(Connection Connection, string Path);
        bool Update(Connection Connection, AFElement Element);
        bool Delete(Connection Connection, string ElementID);
        bool CreateAttribute(Connection Connection, string ParentID, AFAttribute Attr);
        bool CreateChildElement(Connection Connection, string ParentID, AFElement Element);
        string GetElementTemplate(Connection Connection, string ElementID);
        IEnumerable<string> GetCategories(Connection Connection, string ElementID);
        IEnumerable<AFAttribute> GetAttributes(Connection Connection, string ElementID, string NameFilter = "*", string CategoryName = "*", string TemplateName = "*", string ValueType = "*", bool SearchFullHierarchy = false, string SortField = "Name", string SortOrder = "Ascending", int StartIndex = 0, bool ShowExcluded = false, bool ShowHidden = false, int MaxCount = 1000);
        IEnumerable<AFElement> GetElements(Connection Connection, string RootID, string NameFilter = "*", string CategoryName = "*", string TemplateName = "*", ElementType ElementType = ElementType.Any, bool SearchFullHierarchy = false, string SortField = "Name", string SortOrder = "Ascending", int StartIndex = 0, int MaxCount = 1000);
        IEnumerable<AFEventFrame> GetEventFrames(Connection Connection, string ElementID, SearchMode SearchMode, DateTime StartTime, DateTime EndTime, string NameFilter, string CategoryName, string TemplateName, string SortField, string SortOrder, int StartIndex, int MaxCount);
    }
}
