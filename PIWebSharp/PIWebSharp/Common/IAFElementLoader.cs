using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIWebSharp
{
    public interface IAFElementLoader
    {
        AFElement Find(string elementWID);
        AFElement FindByPath(string path);
        bool Update(AFElement element);
        bool Delete(string elementWID);
        bool CreateAttribute(string parentWID, AFAttribute attr);
        bool CreateChildElement(string parentWID, AFElement element);
        public IEnumerable<AFAttribute> GetAttributes(string parentWID, string nameFilter = "*", string categoryName = "*", string templateName = "*", string valueType = "*", bool searchFullHierarchy = false, string sortField = "Name", string sortOrder = "Ascending", int startIndex = 0, bool showExcluded = false, bool showHidden = false, int maxCount = 1000);
        List<AFElement> GetElements(string rootWID, string nameFilter, string categoryName, string tempalateName, ElementType elementType, bool searchFullHierarchy, string sortField, string sortOrder, int startIndex, int maxCount);
        List<AFEventFrame> GetEventFrames(string elementWID, SearchMode searchMode, DateTime startTime, DateTime endTime, string nameFilter, string categoryName, string templateName, string sortField, string sortOrder, int startIndex, int maxCount);
    }
}
