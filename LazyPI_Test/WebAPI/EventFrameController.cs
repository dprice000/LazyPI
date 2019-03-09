using LazyPI.Common;
using LazyPI.LazyObjects;
using System.Collections.Generic;

namespace LazyPI_Test.WebAPI
{
    public class EventFrameController : IAFEventFrameController
    {
        private List<AFEventFrame> _eventFrames = new List<AFEventFrame>();
        private List<AFAttribute> _attrs = new List<AFAttribute>();

        public AFEventFrame Find(Connection Connection, string FrameID)
        {
            return _eventFrames.Find(x => x.ID == FrameID);
        }

        public AFEventFrame FindByPath(Connection Connection, string Path)
        {
            return _eventFrames.Find(x => x.Path == Path);
        }

        public bool Update(Connection Connection, AFEventFrame EventFrame)
        {
            var frame = _eventFrames.Find(x => x.ID == EventFrame.ID);
            int index = _eventFrames.IndexOf(frame);

            _eventFrames[index] = frame;

            return true;
        }

        public bool Delete(Connection Connection, string FrameID)
        {
            var ele = _eventFrames.Find(x => x.ID == FrameID);

            _eventFrames.Remove(ele);

            return true;
        }

        public bool CaptureValues(Connection Connection, string FrameID)
        {
        }

        public bool CreateAttribute(Connection Connection, string FrameID, AFAttribute Attribute)
        {
            _attrs.Add(Attribute);

            return true;
        }

        public bool CreateEventFrame(Connection Connection, string ParentID, AFEventFrame EventFrame)
        {
            _eventFrames.Add(EventFrame);

            return true;
        }

        public string GetEventFrameTemplate(Connection Connection, string FrameID)
        {
            return "FakeEFTemplate";
        }

        public IEnumerable<AFAttribute> GetAttributes(Connection Connection, string FrameID, string NameFilter, string CategoryName, string TemplateName, string ValueType, bool SearchFullHierarchy, string SortField, string SortOrder, int StartIndex, bool ShowExcluded, bool ShowHidden, int MaxCount)
        {
        }

        public IEnumerable<string> GetCategories(Connection Connection, string FrameID)
        {
        }

        public IEnumerable<AFElement> GetReferencedElements(Connection Connection, string FrameID)
        {
        }

        public IEnumerable<AFEventFrame> GetEventFrames(Connection Connection, string FrameID)
        {
        }
    }
}