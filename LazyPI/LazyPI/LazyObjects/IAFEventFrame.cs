using LazyPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public interface IAFEventFrame
    {
        AFEventFrame Find(Connection Connection, string FrameID);
        AFEventFrame FindByPath(Connection Connection, string Path);
        bool Update(Connection Connection, AFEventFrame EventFrame);
        bool Delete(Connection Connection, string FrameID);
        bool CaptureValues(Connection Connection, string FrameID);
        bool CreateAttribute(Connection Connection, string FrameID, AFAttribute EventFrame);
        bool CreateEventFrame(Connection Connection, string ParentID, AFEventFrame EventFrame);
        string GetEventFrameTemplate(Connection Connection, string FrameID);
        IEnumerable<AFAttribute> GetAttributes(Connection Connection, string FrameID, string NameFilter, string CategoryName, string TemplateName, string ValueType, bool SearchFullHierarchy, string SortField, string SortOrder, int StartIndex, bool ShowExcluded, bool ShowHidden, int MaxCount);
        IEnumerable<string> GetCategories(Connection Connection, string FrameID);
        IEnumerable<AFElement> GetReferencedElements(Connection Connection, string FrameID);
        //IEnumerable<AFEventFrame> GetChildFrames(Connection Connection, string FrameID, SearchMode SearchMode, string StartTime, string EndTime, string NameFilter, string ReferencedElementNameFilter, string CategoryName, string TemplateName, string ReferencedElementTemplateFilter, bool SearchFullHierarchy, string SortField, string SortOrder, int StartIndex, int MaxCount);
    }
}
