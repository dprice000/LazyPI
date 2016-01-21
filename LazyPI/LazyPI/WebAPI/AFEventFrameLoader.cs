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
        string _serverAddress;
        RestClient _client;

        public AFEventFrameLoader()
        {
            _serverAddress = "https://localhost/webapi";
            _client = new RestClient(_serverAddress);
        }

        public BaseObject Find(string ID)
        {
            var request = new RestRequest("/eventframes/{webId}");
            request.AddUrlSegment("webId", ID);

            var result = _client.Execute<ResponseModels.AFEventFrame>(request).Data;
            return new BaseObject(result.ID, result.Name, result.Description, result.Path);
        }

        public BaseObject FindByPath(string Path)
        {
            var request = new RestRequest("/eventframes");
            request.AddParameter("path", Path);

            var result = _client.Execute<ResponseModels.AFEventFrame>(request).Data;
            return new BaseObject(result.ID, result.Name, result.Description, result.Path);
        }

        public bool Update(LazyObjects.AFEventFrame eventframe)
        {
            var request = new RestRequest("/eventframes/{webId}", Method.PATCH);
            request.AddUrlSegment("webId", eventframe.ID);
            request.AddBody(eventframe);

            var statusCode = _client.Execute(request).StatusCode;

            return ((int)statusCode == 204 ? true : false);
        }

        public bool Delete(string ID)
        {
            var request = new RestRequest("/eventframes/{webId}", Method.DELETE);
            request.AddUrlSegment("webId", ID);

            var statusCode = _client.Execute(request).StatusCode;

            return ((int)statusCode == 204 ? true : false);
        }

        public bool CaptureValues(string ID)
        {
            var request = new RestRequest("/eventframes/{webId}/attributes/capture", Method.POST);
            request.AddUrlSegment("webId", ID);

            var statusCode = _client.Execute(request).StatusCode;

            return ((int)statusCode == 204 ? true : false);
        }

        public bool CreateAttribute(string ID, LazyObjects.AFAttribute attribute)
        {
            var request = new RestRequest("/eventframes/{webId}/attributes", Method.POST);
            request.AddUrlSegment("webId", ID);
            request.AddBody(attribute);

            var statusCode = _client.Execute(request).StatusCode;

            return ((int)statusCode == 201 ? true : false);
        }

        public bool CreateEventFrame(string ID, LazyObjects.AFEventFrame eventframe)
        {
            var request = new RestRequest("/eventframes/{webId}/eventframes", Method.POST);
            request.AddUrlSegment("webId", ID);
            request.AddBody(eventframe);

            var statusCode = _client.Execute(request).StatusCode;

            return ((int)statusCode == 201 ? true : false);
        }

        public IEnumerable<LazyObjects.AFAttribute> GetAttributes(string webID, string nameFilter = "*", string categoryName = null, string templateName = null, string valueType = null, bool searchFullHierarchy = false, string sortField = "Name", string sortOrder = "Ascending", int startIndex = 0, bool showExcluded = false, bool showHidden = false, int maxCount = 1000)
        {
            var request = new RestRequest("/eventframes/{webId}/attributes");
            request.AddUrlSegment("webId", webID);
            request.AddParameter("nameFilter", nameFilter);

            if (categoryName != null)
                request.AddParameter("categoryName", categoryName);

            if (templateName != null)
                request.AddParameter("templateName", templateName);

            if (valueType != null)
                request.AddParameter("valueType", valueType);

            request.AddParameter("searchFullHierarchy", searchFullHierarchy);
            request.AddParameter("sortOrder", sortOrder);
            request.AddParameter("sortField", sortField);
            request.AddParameter("startIndex", startIndex);
            request.AddParameter("showExcluded", showExcluded);
            request.AddParameter("showHidden", showHidden);
            request.AddParameter("maxCount", maxCount);

            var list = _client.Execute<List<ResponseModels.AFAttribute>>(request).Data;

            return 
        }

        public IEnumerable<LazyObjects.AFElementCategory> GetCategories(string webID)
        {
            var request = new RestRequest("/eventframes/{webId}/categories");
            request.AddUrlSegment("webId", webID);

            return _client.Execute<List<ResponseModels.ElementCategory>>(request).Data;
        }

        public IEnumerable<BaseObject> GetReferencedElements(string webID)
        {
            var request = new RestRequest("/eventframes/{webId}/referencedelements");
            request.AddUrlSegment("webId", webID);

            return _client.Execute<List<ResponseModels.AFElement>>(request).Data;
        }

        public IEnumerable<BaseObject> GetEventFrames(string webID, SearchMode searchMode = SearchMode.Overlapped, string startTime = "-8h", string endTime = "*", string nameFilter = "*", string referencedElementNameFilter = "*", string categoryName = null, string templateName = null, string referencedElementTemplateName = null, bool searchFullHierarchy = false, string sortField = "Name", string sortOrder = "Ascending", int startIndex = 0, int maxCount = 1000)
        {
            var request = new RestRequest("/eventframes/{webId}/eventframes");
            request.AddUrlSegment("webId", webID);
            request.AddParameter("searchMode", searchMode);
            request.AddParameter("startTime", startTime);
            request.AddParameter("endTime", endTime);
            request.AddParameter("nameFilter", nameFilter);
            request.AddParameter("referencedElementNameFilter", referencedElementNameFilter);


            if (categoryName != null)
                request.AddParameter("categoryName", categoryName);
            if (templateName != null)
                request.AddParameter("templateName", templateName);
            if (referencedElementTemplateName != null)
                request.AddParameter("referencedElementTemplateName", referencedElementTemplateName);

            request.AddParameter("searchFullHierarchy", searchFullHierarchy);
            request.AddParameter("sortField", sortField);
            request.AddParameter("sortOrder", sortOrder);
            request.AddParameter("startIndex", startIndex);
            request.AddParameter("maxCount", maxCount);

            return _client.Execute<List<LazyObjects.AFEventFrame>>(request).Data;
        }
    }
}
