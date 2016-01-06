using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public interface IAFElement
    {
        AFElement Find(string elementID);
        AFElement FindByPath(string path);
        bool Update(AFElement element);
        bool Delete(string elementID);
        bool CreateAttribute(string parentID, AFAttribute attr);
        bool CreateChildElement(string parentID, AFElement element);
        public IEnumerable<AFAttribute> GetAttributes(string ID, string nameFilter = "*", string categoryName = "*", string templateName = "*", string valueType = "*", bool searchFullHierarchy = false, string sortField = "Name", string sortOrder = "Ascending", int startIndex = 0, bool showExcluded = false, bool showHidden = false, int maxCount = 1000);
        IEnumerable<AFElement> GetElements(string ID, string nameFilter = "*", string categoryName = "*", string templateName = "*", ElementType elementType = ElementType.Any, bool searchFullHierarchy = false, string sortField = "Name", string sortOrder = "Ascending", int startIndex = 0, int maxCount = 1000);
        IEnumerable<AFEventFrame> GetEventFrames(string elementID, SearchMode searchMode, DateTime startTime, DateTime endTime, string nameFilter, string categoryName, string templateName, string sortField, string sortOrder, int startIndex, int maxCount);
    }
}
