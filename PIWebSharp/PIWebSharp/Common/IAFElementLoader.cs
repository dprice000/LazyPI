using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIWebSharp
{
    public interface IAFElementLoader
    {
        PIWebSharp.WebAPI.AFElement Find(string elementWID);
        PIWebSharp.WebAPI.AFElement FindByPath(string path);
        bool Update(AFElement element);
        bool Delete(string elementWID);
        bool CreateAttribute(string parentWID, AFAttribute attr);
        bool CreateChildElement(string parentWID, AFElement element);
        List<AFAttribute> GetAttributes(string parentWID, string nameFilter, string categoryName, string templateName, string valueType, bool searchFullHierarchy, string SortField, string sortOrder, int startIndex, bool showExcluded, bool showHidden, int maxCount);
        List<PIWebSharp.WebAPI.AFElement> GetElements(string rootWID, string nameFilter, string categoryName, string tempalateName, ElementType elementType, bool searchFullHierarchy, string sortField, string sortOrder, int startIndex, int maxCount);
        List<AFEventFrame> GetEventFrames(string elementWID, SearchMode searchMode, DateTime startTime, DateTime endTime, string nameFilter, string categoryName, string templateName, string sortField, string sortOrder, int startIndex, int maxCount);
    }
}
