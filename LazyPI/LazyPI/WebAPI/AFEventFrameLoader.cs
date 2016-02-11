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
        public LazyObjects.AFEventFrame Find(WebAPIConnection Connection, string FrameID)
        {
            var request = new RestRequest("/eventframes/{webId}");
            request.AddUrlSegment("webId", FrameID);

            var response = Connection.Client.Execute<ResponseModels.AFEventFrame>(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Error finding event frame by ID. (See Inner Details)", response.ErrorException);
            }

            var data = response.Data;

            return new LazyObjects.AFEventFrame(Connection, data.WebID, data.Name, data.Description, data.Path);
        }

        public LazyObjects.AFEventFrame FindByPath(WebAPIConnection Connection, string Path)
        {
            var request = new RestRequest("/eventframes");
            request.AddParameter("path", Path);

            var response = Connection.Client.Execute<ResponseModels.AFEventFrame>(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Error finding element by path. (See Inner Details)", response.ErrorException);
            }

            var data = response.Data;

            return new LazyObjects.AFEventFrame(Connection, data.WebID, data.Name, data.Description, data.Path);
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

            var response = Connection.Client.Execute<List<ResponseModels.AFAttribute>>(request);

            if (response.ErrorException != null)
			{
				throw new ApplicationException("Error finding event frame attributes. (See Inner Details)", response.ErrorException);
			}

            var data = response.Data;

        }

        public IEnumerable<LazyObjects.AFElementCategory> GetCategories(WebAPIConnection Connection, string FrameID)
        {
            var request = new RestRequest("/eventframes/{webId}/categories");
            request.AddUrlSegment("webId", FrameID);

            var response = Connection.Client.Execute<List<ResponseModels.ElementCategory>>(request);

            if (response.ErrorException != null)
			{
				throw new ApplicationException("Error retrieving event frame categories. (See Inner Details)", response.ErrorException);
			}

            var data = response.Data;
        }

        public IEnumerable<LazyObjects.AFElement> GetReferencedElements(WebAPIConnection Connection, string FrameID)
        {
            var request = new RestRequest("/eventframes/{webId}/referencedelements");
            request.AddUrlSegment("webId", FrameID);

            var response = Connection.Client.Execute<ResponseModels.ResponseList<ResponseModels.AFElement>>(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Error retrieving elements referenced by event frame. (See Inner Details)", response.ErrorException);
            }

            var data = response.Data;

            List<LazyObjects.AFElement> results = new List<LazyObjects.AFElement>();

            //TODO: Think of a more efficient way to do this.
            foreach (var element in data.Items)
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

            var response = Connection.Client.Execute<ResponseModels.ResponseList<ResponseModels.AFEventFrame>>(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Error retrieving event frames child event frames. (See Inner Details)", response.ErrorException);
            }

            var data = response.Data;

            List<LazyObjects.AFEventFrame> results = new List<LazyObjects.AFEventFrame>();

            foreach (var frame in data.Items)
            {
                results.Add(new LazyObjects.AFEventFrame(Connection, frame.WebID, frame.Name, frame.Description, frame.Path));
            }

            return results;
        }
    }
}
