using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazyObjects = LazyPI.LazyObjects;
using ResponseModels = LazyPI.WebAPI.ResponseModels;

namespace LazyPI.WebAPI
{
    public class AFEventFrameLoader : LazyObjects.IAFEventFrame
    {
        private LazyObjects.ILazyFactory _Factory;

        public AFEventFrameLoader()
        {
        }

        public LazyObjects.AFEventFrame Find(WebAPIConnection Connection, string FrameID)
        {
            var request = new RestRequest("/eventframes/{webId}");
            request.AddUrlSegment("webId", FrameID);

            var response = Connection.Client.Execute<ResponseModels.AFEventFrame>(request).Data;

            return (LazyObjects.AFEventFrame)_Factory.CreateInstance(Connection, response.WebID, response.Name, response.Description, response.Path);
        }

        public LazyObjects.AFEventFrame FindByPath(WebAPIConnection Connection, string Path)
        {
            var request = new RestRequest("/eventframes");
            request.AddParameter("path", Path);

            var response = Connection.Client.Execute<ResponseModels.AFEventFrame>(request).Data;

            return (LazyObjects.AFEventFrame)_Factory.CreateInstance(Connection, response.WebID, response.Name, response.Description, response.Path);
        }

        public bool Update(WebAPIConnection Connection, LazyObjects.AFEventFrame Eventframe)
        {
            var request = new RestRequest("/eventframes/{webId}", Method.PATCH);
            request.AddUrlSegment("webId", Eventframe.ID);
            request.AddBody(Eventframe);

            var statusCode = Connection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 204 ? true : false);
        }

        public bool Delete(WebAPIConnection Connection, string FrameID)
        {
            var request = new RestRequest("/eventframes/{webId}", Method.DELETE);
            request.AddUrlSegment("webId", FrameID);

            var statusCode = Connection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 204 ? true : false);
        }

        public bool CaptureValues(WebAPIConnection Connection, string FrameID)
        {
            var request = new RestRequest("/eventframes/{webId}/attributes/capture", Method.POST);
            request.AddUrlSegment("webId", FrameID);

            var statusCode = Connection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 204 ? true : false);
        }

        public bool CreateAttribute(WebAPIConnection Connection, string FrameID, LazyObjects.AFAttribute attribute)
        {
            var request = new RestRequest("/eventframes/{webId}/attributes", Method.POST);
            request.AddUrlSegment("webId", FrameID);
            request.AddBody(attribute);

            var statusCode = Connection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 201 ? true : false);
        }

        public bool CreateEventFrame(WebAPIConnection Connection, string FrameID, LazyObjects.AFEventFrame Eventframe)
        {
            var request = new RestRequest("/eventframes/{webId}/eventframes", Method.POST);
            request.AddUrlSegment("webId", FrameID);
            request.AddBody(Eventframe);

            var statusCode = Connection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 201 ? true : false);
        }

        public IEnumerable<LazyObjects.AFAttribute> GetAttributes(WebAPIConnection Connection, string FrameID, string NameFilter = "*", string CategoryName = null, string TemplateName = null, string ValueType = null, bool SearchFullHierarchy = false, string SortField = "Name", string SortOrder = "Ascending", int StartIndex = 0, bool ShowExcluded = false, bool ShowHidden = false, int MaxCount = 1000)
        {
            var request = new RestRequest("/eventframes/{webId}/attributes");
            request.AddUrlSegment("webId", FrameID);
            request.AddParameter("nameFilter", NameFilter);

            if (CategoryName != null)
                request.AddParameter("categoryName", CategoryName);

            if (TemplateName != null)
                request.AddParameter("templateName", TemplateName);

            if (ValueType != null)
                request.AddParameter("valueType", ValueType);

            request.AddParameter("searchFullHierarchy", SearchFullHierarchy);
            request.AddParameter("sortOrder", SortOrder);
            request.AddParameter("sortField", SortField);
            request.AddParameter("startIndex", StartIndex);
            request.AddParameter("showExcluded", ShowExcluded);
            request.AddParameter("showHidden", ShowHidden);
            request.AddParameter("maxCount", MaxCount);

            var list = Connection.Client.Execute<List<ResponseModels.AFAttribute>>(request).Data;

        }

        public IEnumerable<LazyObjects.AFElementCategory> GetCategories(WebAPIConnection Connection, string FrameID)
        {
            var request = new RestRequest("/eventframes/{webId}/categories");
            request.AddUrlSegment("webId", FrameID);

            var result = Connection.Client.Execute<List<ResponseModels.ElementCategory>>(request).Data;
        }

        public IEnumerable<LazyObjects.AFElement> GetReferencedElements(WebAPIConnection Connection, string FrameID)
        {
            var request = new RestRequest("/eventframes/{webId}/referencedelements");
            request.AddUrlSegment("webId", FrameID);

            var response = Connection.Client.Execute<ResponseModels.ResponseList<ResponseModels.AFElement>>(request).Data;

            List<LazyObjects.AFElement> results = new List<LazyObjects.AFElement>();

            //TODO: Think of a more efficient way to do this.
            foreach (var element in response.Items)
            {
                results.Add(LazyObjects.AFElement.Find(Connection, element.WebID));
            }

            return results;
        }

        public IEnumerable<LazyObjects.AFEventFrame> GetEventFrames(WebAPIConnection Connection, string FrameID, SearchMode SearchMode = SearchMode.Overlapped, string StartTime = "-8h", string EndTime = "*", string NameFilter = "*", string ReferencedElementNameFilter = "*", string CategoryName = null, string TemplateName = null, string ReferencedElementTemplateName = null, bool SearchFullHierarchy = false, string SortField = "Name", string SortOrder = "Ascending", int StartIndex = 0, int MaxCount = 1000)
        {
            var request = new RestRequest("/eventframes/{webId}/eventframes");
            request.AddUrlSegment("webId", FrameID);
            request.AddParameter("searchMode", SearchMode);
            request.AddParameter("startTime", StartTime);
            request.AddParameter("endTime", EndTime);
            request.AddParameter("nameFilter", NameFilter);
            request.AddParameter("referencedElementNameFilter", ReferencedElementNameFilter);


            if (CategoryName != null)
                request.AddParameter("categoryName", CategoryName);
            if (TemplateName != null)
                request.AddParameter("templateName", TemplateName);
            if (ReferencedElementTemplateName != null)
                request.AddParameter("referencedElementTemplateName", ReferencedElementTemplateName);

            request.AddParameter("searchFullHierarchy", SearchFullHierarchy);
            request.AddParameter("sortField", SortField);
            request.AddParameter("sortOrder", SortOrder);
            request.AddParameter("startIndex", StartIndex);
            request.AddParameter("maxCount", MaxCount);

            var response = Connection.Client.Execute<ResponseModels.ResponseList<ResponseModels.AFEventFrame>>(request).Data;

            List<LazyObjects.AFEventFrame> results = new List<LazyObjects.AFEventFrame>();

            foreach (var frame in response.Items)
            {
                results.Add((LazyObjects.AFEventFrame)_Factory.CreateInstance(Connection, frame.WebID, frame.Name, frame.Description, frame.Path));
            }

            return results;
        }
    }
}
