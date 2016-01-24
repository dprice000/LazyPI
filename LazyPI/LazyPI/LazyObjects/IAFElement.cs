using LazyPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public interface IAFElement
    {
        BaseObject Find(Connection Connection, string elementID);
        BaseObject FindByPath(Connection Connection, string path);
        bool Update(Connection Connection, AFElement element);
        bool Delete(Connection Connection, string elementID);
        bool CreateAttribute(Connection Connection, string parentID, AFAttribute attr);
        bool CreateChildElement(Connection Connection, string parentID, AFElement element);
        string GetElementTemplate(Connection Connection, string elementID);
        IEnumerable<string> GetCategories(Connection Connection, string ID);
        IEnumerable<BaseObject> GetAttributes(Connection Connection, string ID, string nameFilter = "*", string categoryName = "*", string templateName = "*", string valueType = "*", bool searchFullHierarchy = false, string sortField = "Name", string sortOrder = "Ascending", int startIndex = 0, bool showExcluded = false, bool showHidden = false, int maxCount = 1000);
        IEnumerable<BaseObject> GetElements(Connection Connection, string ID, string nameFilter = "*", string categoryName = "*", string templateName = "*", ElementType elementType = ElementType.Any, bool searchFullHierarchy = false, string sortField = "Name", string sortOrder = "Ascending", int startIndex = 0, int maxCount = 1000);
        IEnumerable<BaseObject> GetEventFrames(Connection Connection, string elementID, SearchMode searchMode, DateTime startTime, DateTime endTime, string nameFilter, string categoryName, string templateName, string sortField, string sortOrder, int startIndex, int maxCount);
    }
}
