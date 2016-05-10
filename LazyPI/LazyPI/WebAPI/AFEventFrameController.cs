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
	public class AFEventFrameController : LazyObjects.IAFEventFrameController
	{
		public LazyObjects.AFEventFrame Find(LazyPI.Common.Connection Connection, string FrameID)
		{
			WebAPIConnection webConnection = (WebAPIConnection)Connection;
			var request = new RestRequest("/eventframes/{webId}");
			request.AddUrlSegment("webId", FrameID);

			var response = webConnection.Client.Execute<ResponseModels.AFEventFrame>(request);

			if (response.ErrorException != null)
			{
				throw new ApplicationException("Error finding event frame by ID. (See Inner Details)", response.ErrorException);
			}

			var data = response.Data;

			return new LazyObjects.AFEventFrame(Connection, data.WebId, data.Id, data.Name, data.Description, data.Path);
		}

		public LazyObjects.AFEventFrame FindByPath(LazyPI.Common.Connection Connection, string Path)
		{
			WebAPIConnection webConnection = (WebAPIConnection)Connection;
			var request = new RestRequest("/eventframes");
			request.AddParameter("path", Path, ParameterType.GetOrPost);

			var response = webConnection.Client.Execute<ResponseModels.AFEventFrame>(request);

			if (response.ErrorException != null)
			{
				throw new ApplicationException("Error finding element by path. (See Inner Details)", response.ErrorException);
			}

			var data = response.Data;

			return new LazyObjects.AFEventFrame(Connection, data.WebId, data.Id, data.Name, data.Description, data.Path);
		}

		public bool Update(LazyPI.Common.Connection Connection, LazyObjects.AFEventFrame Eventframe)
		{
			WebAPIConnection webConnection = (WebAPIConnection)Connection;
			var request = new RestRequest("/eventframes/{webId}", Method.PATCH);
			request.AddUrlSegment("webId", Eventframe.WebID);

            ResponseModels.AFEventFrame body = DataConversions.Convert(Eventframe);
            request.AddParameter("application/json; charset=utf-8", Newtonsoft.Json.JsonConvert.SerializeObject(body), ParameterType.RequestBody);

			var statusCode = webConnection.Client.Execute(request).StatusCode;

			return ((int)statusCode == 204 ? true : false);
		}

		public bool Delete(LazyPI.Common.Connection Connection, string FrameID)
		{
			WebAPIConnection webConnection = (WebAPIConnection)Connection;
			var request = new RestRequest("/eventframes/{webId}", Method.DELETE);
			request.AddUrlSegment("webId", FrameID);

			var statusCode = webConnection.Client.Execute(request).StatusCode;

			return ((int)statusCode == 204 ? true : false);
		}

		public bool CaptureValues(LazyPI.Common.Connection Connection, string FrameID)
		{
			WebAPIConnection webConnection = (WebAPIConnection)Connection;
			var request = new RestRequest("/eventframes/{webId}/attributes/capture", Method.POST);
			request.AddUrlSegment("webId", FrameID);

			var statusCode = webConnection.Client.Execute(request).StatusCode;

			return ((int)statusCode == 204 ? true : false);
		}

		public bool CreateAttribute(LazyPI.Common.Connection Connection, string FrameID, LazyObjects.AFAttribute attribute)
		{
			WebAPIConnection webConnection = (WebAPIConnection)Connection;
			var request = new RestRequest("/eventframes/{webId}/attributes", Method.POST);
			request.AddUrlSegment("webId", FrameID);
            ResponseModels.AFAttribute body = DataConversions.Convert(attribute);
            request.AddParameter("application/json; charset=utf-8", Newtonsoft.Json.JsonConvert.SerializeObject(body), ParameterType.RequestBody);

			var statusCode = webConnection.Client.Execute(request).StatusCode;

			return ((int)statusCode == 201 ? true : false);
		}

		public bool CreateEventFrame(LazyPI.Common.Connection Connection, string FrameID, LazyObjects.AFEventFrame Eventframe)
		{
			WebAPIConnection webConnection = (WebAPIConnection)Connection;
			var request = new RestRequest("/eventframes/{webId}/eventframes", Method.POST);
			request.AddUrlSegment("webId", FrameID);

            ResponseModels.AFEventFrame body = DataConversions.Convert(Eventframe);
            request.AddParameter("application/json; charset=utf-8", Newtonsoft.Json.JsonConvert.SerializeObject(body), ParameterType.RequestBody);

			var statusCode = webConnection.Client.Execute(request).StatusCode;

			return ((int)statusCode == 201 ? true : false);
		}

		public IEnumerable<LazyObjects.AFAttribute> GetAttributes(LazyPI.Common.Connection Connection, string FrameID, string NameFilter = "*", string CategoryName = null, string TemplateName = null, string ValueType = null, bool SearchFullHierarchy = false, string SortField = "Name", string SortOrder = "Ascending", int StartIndex = 0, bool ShowExcluded = false, bool ShowHidden = false, int MaxCount = 1000)
		{
			WebAPIConnection webConnection = (WebAPIConnection)Connection;
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

			var response = webConnection.Client.Execute<List<ResponseModels.AFAttribute>>(request);

			if (response.ErrorException != null)
			{
				throw new ApplicationException("Error finding event frame attributes. (See Inner Details)", response.ErrorException);
			}

			var data = response.Data;
			List<LazyObjects.AFAttribute> results = new List<LazyObjects.AFAttribute>();

			foreach(ResponseModels.AFAttribute attr in data)
			{
				results.Add(new LazyObjects.AFAttribute(Connection, attr.WebId, attr.Id, attr.Name, attr.Description, attr.Path));
			}

			return results;
		}

		public IEnumerable<string> GetCategories(LazyPI.Common.Connection Connection, string FrameID)
		{
			WebAPIConnection webConnection = (WebAPIConnection)Connection;
			var request = new RestRequest("/eventframes/{webId}/categories");
			request.AddUrlSegment("webId", FrameID);

			var response = webConnection.Client.Execute<List<ResponseModels.ElementCategory>>(request);

			if (response.ErrorException != null)
			{
				throw new ApplicationException("Error retrieving event frame categories. (See Inner Details)", response.ErrorException);
			}

			var data = response.Data;
			List<string> results = new List<string>();

			foreach (ResponseModels.ElementCategory category in data)
			{
				results.Add(category.Name);
			}

			return results;
		}

		public IEnumerable<LazyObjects.AFElement> GetReferencedElements(LazyPI.Common.Connection Connection, string FrameID)
		{
			WebAPIConnection webConnection = (WebAPIConnection)Connection;
			var request = new RestRequest("/eventframes/{webId}/referencedelements");
			request.AddUrlSegment("webId", FrameID);

			var response = webConnection.Client.Execute<ResponseModels.ResponseList<ResponseModels.AFElement>>(request);

			if (response.ErrorException != null)
			{
				throw new ApplicationException("Error retrieving elements referenced by event frame. (See Inner Details)", response.ErrorException);
			}

			var data = response.Data;

			List<LazyObjects.AFElement> results = new List<LazyObjects.AFElement>();

			//TODO: Think of a more efficient way to do this.
			foreach (var element in data.Items)
			{
				results.Add(LazyObjects.AFElement.Find(Connection, element.WebId));
			}

			return results;
		}

		public string GetEventFrameTemplate(LazyPI.Common.Connection Connection, string FrameID)
		{
			WebAPIConnection webConnection = (WebAPIConnection)Connection;
			var request = new RestRequest("/eventframes/{webId}");
			request.AddUrlSegment("webId", FrameID);

			var response = webConnection.Client.Execute<ResponseModels.AFEventFrame>(request);

			if (response.ErrorException != null)
			{
				throw new ApplicationException("Error finding event frame by ID. (See Inner Details)", response.ErrorException);
			}

			ResponseModels.AFEventFrame data = response.Data;

			return data.TemplateName;
		}

		public IEnumerable<LazyObjects.AFEventFrame> GetEventFrames(LazyPI.Common.Connection Connection, string FrameID)
		{
			WebAPIConnection webConnection = (WebAPIConnection)Connection;
			var request = new RestRequest("/eventframes/{webId}/eventframes");
			request.AddUrlSegment("webId", FrameID);
			request.AddParameter("searchMode", LazyPI.Common.SearchMode.BackwardFromStartTime);
			request.AddParameter("startTime", "*");

			var response = webConnection.Client.Execute<ResponseModels.ResponseList<ResponseModels.AFEventFrame>>(request);

			if (response.ErrorException != null)
			{
				throw new ApplicationException("Error retrieving event frames child event frames. (See Inner Details)", response.ErrorException);
			}

			var data = response.Data;

			List<LazyObjects.AFEventFrame> results = new List<LazyObjects.AFEventFrame>();

			foreach (var frame in data.Items)
			{
				results.Add(new LazyObjects.AFEventFrame(Connection, frame.WebId, frame.Id, frame.Name, frame.Description, frame.Path));
			}

			return results;
		}
	}
}
