using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace PIWebSharp.LazyObjects
{
	public class AFElementLoader : IAFElementLoader
	{
		string _serverAddress;
		RestClient _client;

		public AFElementLoader()
		{
			_serverAddress = "https://localhost/webapi";
		   _client  = new RestClient(_serverAddress);
		}

		// These functions have direct references to WebAPI calls
		#region "Public Methods"
			public PIWebSharp.WebAPI.AFElement Find(string elementWID)
			{
				var request = new RestRequest("/elements/{webId}");
				request.AddUrlSegment("webId", elementWID);

				return _client.Execute<PIWebSharp.WebAPI.AFElement>(request).Data;
			}

			public PIWebSharp.WebAPI.AFElement FindByPath(string path)
			{
				var request = new RestRequest("/elements");
				request.AddParameter("path", path);

				return _client.Execute<PIWebSharp.WebAPI.AFElement>(request).Data;
			}

			public bool Update(PIWebSharp.WebAPI.AFElement element)
			{
				var request = new RestRequest("/elements/{webId}", Method.PATCH);
				request.AddUrlSegment("webId", element.WebID);
				request.AddBody(element);

				var statusCode = _client.Execute(request).StatusCode;

				return ((int)statusCode == 204);
			}

			public bool Delete(string elementWID)
			{
				var request = new RestRequest("/elements/{webId}", Method.DELETE);
				request.AddUrlSegment("webId", elementWID);

				var statusCode = _client.Execute(request).StatusCode;

				return ((int)statusCode == 204);
			}

			public bool CreateAttribute(string parentWID, AFAttribute attr)
			{
				var request = new RestRequest("/elements/{webId}/attributes", Method.POST);
				request.AddUrlSegment("webId", parentWID);
				request.AddBody(attr);

				var statusCode = _client.Execute(request).StatusCode;

				return ((int)statusCode == 201);
			}

			public bool CreateChildElement(string parentWID, AFElement element)
			{
				var request = new RestRequest("/elements/{webId}/elements", Method.POST);
				request.AddUrlSegment("webId", parentWID);
				request.AddBody(element);

				var statusCode = _client.Execute(request).StatusCode;

				return ((int)statusCode == 201);
			}

			public PIWebSharp.WebAPI.ResponseList<PIWebSharp.WebAPI.AFAttribute> GetAttributes(string parentWID, string nameFilter, string categoryName, string templateName, string valueType, bool searchFullHierarchy, string sortField, string sortOrder, int startIndex, bool showExcluded, bool showHidden, int maxCount)
			{
				var request = new RestRequest("/elements/{webId}/attributes");
				request.AddUrlSegment("webId", parentWID);
				request.AddParameter("nameFilter", nameFilter);
				request.AddParameter("categoryName", categoryName);
				request.AddParameter("templateName", templateName);
				request.AddParameter("valueType", valueType);
				request.AddParameter("searchFullHierarchy", searchFullHierarchy);
				request.AddParameter("sortField", sortField);
				request.AddParameter("sortOrder", sortOrder);
				request.AddParameter("startIndex", startIndex);
				request.AddParameter("showExcluded", showExcluded);
				request.AddParameter("showHidden", showHidden);
				request.AddParameter("maxCount", maxCount);

				return _client.Execute<PIWebSharp.WebAPI.ResponseList<AFAttribute>>(request).Data;
			}

			public PIWebSharp.WebAPI.ResponseList<ElementCategory> GetElementCategories(string elementWID)
			{
				var request = new RestRequest("/elements/{webId}/categories");
				request.AddUrlSegment("webId", parentWID);

				return _client.Execute<ResponseList<ElementCategory>>(request).Data;
			}

			public PIWebSharp.WebAPI.ResponseList<AFElement> GetElements(string rootWID, string nameFilter, string categoryName, string templateName, ElementType elementType, bool searchFullHierarchy, string sortField, string sortOrder, int startIndex, int maxCount)
			{
				var request = new RestRequest("/elements/{webId}/elements");
				request.AddUrlSegment("webId", rootWID);
				request.AddParameter("nameFilter", nameFilter);
				request.AddParameter("templateName", templateName);
				request.AddParameter("elementType", elementType);
				request.AddParameter("searchFullHierarchy", searchFullHierarchy);
				request.AddParameter("sortField", sortField);
				request.AddParameter("sortOrder", sortOrder);
				request.AddParameter("startIndex", startIndex);
				request.AddParameter("maxCount", maxCount);

				return _client.Execute<PIWebSharp.WebAPI.ResponseList<AFElement>>(request).Data;
			}

			public PIWebSharp.WebAPI.ResponseList<AFEventFrame> GetEventFrames(string elementWID, PIWebSharp.WebAPI.SearchMode searchMode, DateTime startTime, DateTime endTime, string nameFilter, string categoryName, string templateName, string sortField, string sortOrder, int startIndex, int maxCount)
			{
				var request = new RestRequest("/elements/{webId}/eventframes");
				request.AddUrlSegment("webId", elementWID);
				request.AddParameter("searchMode", searchMode);
				request.AddParameter("startTime", startTime);
				request.AddParameter("endTime", endTime);
				request.AddParameter("nameFilter", nameFilter);
				request.AddParameter("categoryName", categoryName);
				request.AddParameter("templateName", templateName);
				request.AddParameter("sortField", sortField);
				request.AddParameter("sortOrder", sortOrder);
				request.AddParameter("startIndex", startIndex);
				request.AddParameter("maxCount", maxCount);

				return _client.Execute<ResponseList<AFEventFrame>>(request).Data;
			}
		#endregion
	}
}