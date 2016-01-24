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
        public AFEventFrameLoader()
        {
        }

        public BaseObject Find(WebAPIConnection Connection, string ID)
        {
            var request = new RestRequest("/eventframes/{webId}");
            request.AddUrlSegment("webId", ID);

            var result = Connection.Client.Execute<ResponseModels.AFEventFrame>(request).Data;
            return new BaseObject(result.ID, result.Name, result.Description, result.Path);
        }

        public BaseObject FindByPath(WebAPIConnection Connection, string Path)
        {
            var request = new RestRequest("/eventframes");
            request.AddParameter("path", Path);

            var result = Connection.Client.Execute<ResponseModels.AFEventFrame>(request).Data;
            return new BaseObject(result.ID, result.Name, result.Description, result.Path);
        }

        public bool Update(WebAPIConnection Connection, LazyObjects.AFEventFrame Eventframe)
        {
            var request = new RestRequest("/eventframes/{webId}", Method.PATCH);
            request.AddUrlSegment("webId", Eventframe.ID);
            request.AddBody(Eventframe);

            var statusCode = Connection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 204 ? true : false);
        }

        public bool Delete(WebAPIConnection Connection, string ID)
        {
            var request = new RestRequest("/eventframes/{webId}", Method.DELETE);
            request.AddUrlSegment("webId", ID);

            var statusCode = Connection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 204 ? true : false);
        }

        public bool CaptureValues(WebAPIConnection Connection, string ID)
        {
            var request = new RestRequest("/eventframes/{webId}/attributes/capture", Method.POST);
            request.AddUrlSegment("webId", ID);

            var statusCode = Connection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 204 ? true : false);
        }

        public bool CreateAttribute(WebAPIConnection Connection, string ID, LazyObjects.AFAttribute attribute)
        {
            var request = new RestRequest("/eventframes/{webId}/attributes", Method.POST);
            request.AddUrlSegment("webId", ID);
            request.AddBody(attribute);

            var statusCode = Connection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 201 ? true : false);
        }

        public bool CreateEventFrame(WebAPIConnection Connection, string ID, LazyObjects.AFEventFrame eventframe)
        {
            var request = new RestRequest("/eventframes/{webId}/eventframes", Method.POST);
            request.AddUrlSegment("webId", ID);
            request.AddBody(eventframe);

            var statusCode = Connection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 201 ? true : false);
        }

        public IEnumerable<LazyObjects.AFAttribute> GetAttributes(WebAPIConnection Connection, string webID, string nameFilter = "*", string categoryName = null, string templateName = null, string valueType = null, bool searchFullHierarchy = false, string sortField = "Name", string sortOrder = "Ascending", int startIndex = 0, bool showExcluded = false, bool showHidden = false, int maxCount = 1000)
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

            var list = Connection.Client.Execute<List<ResponseModels.AFAttribute>>(request).Data;

        }

        public IEnumerable<LazyObjects.AFElementCategory> GetCategories(WebAPIConnection Connection, string webID)
        {
            var request = new RestRequest("/eventframes/{webId}/categories");
            request.AddUrlSegment("webId", webID);

            var result = Connection.Client.Execute<List<ResponseModels.ElementCategory>>(request).Data;
        }

        public IEnumerable<BaseObject> GetReferencedElements(WebAPIConnection Connection, string webID)
        {
            var request = new RestRequest("/eventframes/{webId}/referencedelements");
            request.AddUrlSegment("webId", webID);

            var result = Connection.Client.Execute<List<ResponseModels.AFElement>>(request).Data;
        }

        public IEnumerable<BaseObject> GetEventFrames(WebAPIConnection Connection, string webID, SearchMode searchMode = SearchMode.Overlapped, string startTime = "-8h", string endTime = "*", string nameFilter = "*", string referencedElementNameFilter = "*", string categoryName = null, string templateName = null, string referencedElementTemplateName = null, bool searchFullHierarchy = false, string sortField = "Name", string sortOrder = "Ascending", int startIndex = 0, int maxCount = 1000)
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

            return Connection.Client.Execute<List<LazyObjects.AFEventFrame>>(request).Data;
        }
    }
}
