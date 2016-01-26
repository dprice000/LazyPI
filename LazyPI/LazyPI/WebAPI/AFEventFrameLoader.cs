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

        public LazyObjects.AFEventFrame Find(WebAPIConnection Connection, string ID)
        {
            var request = new RestRequest("/eventframes/{webId}");
            request.AddUrlSegment("webId", ID);

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

        public IEnumerable<LazyObjects.AFElement> GetReferencedElements(WebAPIConnection Connection, string webID)
        {
            var request = new RestRequest("/eventframes/{webId}/referencedelements");
            request.AddUrlSegment("webId", webID);

            var response = Connection.Client.Execute<ResponseModels.ResponseList<ResponseModels.AFElement>>(request).Data;

            List<LazyObjects.AFElement> results = new List<LazyObjects.AFElement>();

            //TODO: Think of a more efficient way to do this.
            foreach (var element in response.Items)
            {
                results.Add(LazyObjects.AFElement.Find(Connection, element.WebID));
            }

            return results;
        }

        public IEnumerable<LazyObjects.AFEventFrame> GetEventFrames(WebAPIConnection Connection, string webID, SearchMode searchMode = SearchMode.Overlapped, string startTime = "-8h", string endTime = "*", string nameFilter = "*", string referencedElementNameFilter = "*", string categoryName = null, string templateName = null, string referencedElementTemplateName = null, bool searchFullHierarchy = false, string sortField = "Name", string sortOrder = "Ascending", int startIndex = 0, int maxCount = 1000)
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
