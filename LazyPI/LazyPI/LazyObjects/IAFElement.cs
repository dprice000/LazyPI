using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public interface IAFElement
    {
        BaseObject Find(string elementID);
        BaseObject FindByPath(string path);
        bool Update(AFElement element);
        bool Delete(string elementID);
        bool CreateAttribute(string parentID, AFAttribute attr);
        bool CreateChildElement(string parentID, AFElement element);
        string GetElementTemplate(string elementID);
        IEnumerable<string> GetCategories(string ID);
        IEnumerable<BaseObject> GetAttributes(string ID, string nameFilter = "*", string categoryName = "*", string templateName = "*", string valueType = "*", bool searchFullHierarchy = false, string sortField = "Name", string sortOrder = "Ascending", int startIndex = 0, bool showExcluded = false, bool showHidden = false, int maxCount = 1000);
        IEnumerable<BaseObject> GetElements(string ID, string nameFilter = "*", string categoryName = "*", string templateName = "*", ElementType elementType = ElementType.Any, bool searchFullHierarchy = false, string sortField = "Name", string sortOrder = "Ascending", int startIndex = 0, int maxCount = 1000);
        IEnumerable<BaseObject> GetEventFrames(string elementID, SearchMode searchMode, DateTime startTime, DateTime endTime, string nameFilter, string categoryName, string templateName, string sortField, string sortOrder, int startIndex, int maxCount);
    }
}
