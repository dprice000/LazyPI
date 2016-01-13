using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public interface IAFEventFrame
    {
        BaseObject Find(string ID);
        BaseObject FindByPath(string Path);
        bool Update(AFEventFrame EventFrame);
        bool Delete(string ID);
        bool CaptureValues(string ID);
        bool CreateAttribute(string EventFrameID, AFEventFrame EventFrame);
        bool CreateEventFrame(string ParentID, AFEventFrame EventFrame);
        bool GetAttributes(string ID, string NameFilter, string CategoryName, string TemplateName, string ValueType, bool SearchFullHierarchy, string SortField, string SortOrder, int StartIndex, bool ShowExcluded, bool ShowHidden, int MaxCount);
        IEnumerable<AFElementCategory> GetCategories(string ID);
        IEnumerable<AFElement> GetReferencedElements(string ID);
        IEnumerable<AFEventFrame> GetChildFrames(string ID, SearchMode SearchMode, string StartTime, string EndTime, string NameFilter, string ReferencedElementNameFilter, string CategoryName, string TemplateName, string ReferencedElementTemplateFilter, bool SearchFullHierarchy, string SortField, string SortOrder, int StartIndex, int MaxCount);
    }
}
