using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using LazyObjects = LazyPI.LazyObjects;
using ResponseModels = LazyPI.WebAPI.ResponseModels;

namespace LazyPI.WebAPI
{
	public class AFElementLoader : LazyObjects.IAFElement
	{
		private LazyPI.LazyObjects.ILazyFactory _Factory;

		public AFElementLoader()
		{
		}

		// These functions have direct references to WebAPI calls
		#region "Public Methods"
			public LazyObjects.AFElement Find(LazyPI.WebAPI.WebAPIConnection Connection,string elementWID)
			{
				var request = new RestRequest("/elements/{webId}");
				request.AddUrlSegment("webId", elementWID);

				var result = Connection.Client.Execute<LazyPI.WebAPI.ResponseModels.AFElement>(request).Data;
				return (LazyObjects.AFElement)_Factory.CreateInstance(Connection, result.ID, result.Name, result.Description, result.Path);
			}

			public LazyObjects.AFElement FindByPath(LazyPI.WebAPI.WebAPIConnection Connection, string path)
			{
				var request = new RestRequest("/elements");
				request.AddParameter("path", path);

				var result = Connection.Client.Execute<LazyPI.WebAPI.ResponseModels.AFElement>(request).Data;
				return (LazyObjects.AFElement)_Factory.CreateInstance(Connection, result.ID, result.Name, result.Description, result.Path);
			}

			public bool Update(LazyPI.WebAPI.WebAPIConnection Connection, LazyObjects.AFElement element)
			{
				var request = new RestRequest("/elements/{webId}", Method.PATCH);
				request.AddUrlSegment("webId", element.ID);

				//TODO: You need to convert this object before putting it in the request
				//request.AddBody(element);

				var statusCode = Connection.Client.Execute(request).StatusCode;

				return ((int)statusCode == 204);
			}

			public bool Delete(LazyPI.WebAPI.WebAPIConnection Connection, string elementWID)
			{
				var request = new RestRequest("/elements/{webId}", Method.DELETE);
				request.AddUrlSegment("webId", elementWID);

				var statusCode = Connection.Client.Execute(request).StatusCode;

				return ((int)statusCode == 204);
			}

			public bool CreateAttribute(LazyPI.WebAPI.WebAPIConnection Connection, string parentWID, LazyObjects.AFAttribute attr)
			{
				var request = new RestRequest("/elements/{webId}/attributes", Method.POST);
				request.AddUrlSegment("webId", parentWID);
				request.AddBody(attr);

				var statusCode = Connection.Client.Execute(request).StatusCode;

				return ((int)statusCode == 201);
			}

			public bool CreateChildElement(LazyPI.WebAPI.WebAPIConnection Connection, string parentWID, LazyObjects.AFElement element)
			{
				var request = new RestRequest("/elements/{webId}/elements", Method.POST);
				request.AddUrlSegment("webId", parentWID);
				request.AddBody(element);

				var statusCode = Connection.Client.Execute(request).StatusCode;

				return ((int)statusCode == 201);
			}

			public string GetElementTemplate(LazyPI.WebAPI.WebAPIConnection Connection, string elementID)
			{
				var request = new RestRequest("/elements/{webId}");
				request.AddUrlSegment("webId", elementID);

				var result = Connection.Client.Execute<LazyPI.WebAPI.ResponseModels.AFElement>(request).Data;
				return result.TemplateName;
			}

			public IEnumerable<LazyObjects.AFAttribute> GetAttributes(LazyPI.WebAPI.WebAPIConnection Connection, string parentWID, string nameFilter = "*", string categoryName = "*", string templateName = "*", string valueType = "*", bool searchFullHierarchy = false, string sortField = "Name", string sortOrder = "Ascending", int startIndex = 0, bool showExcluded = false, bool showHidden = false, int maxCount = 1000)
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

				var response = Connection.Client.Execute<ResponseModels.ResponseList<ResponseModels.AFAttribute>>(request).Data;

				List<LazyObjects.AFAttribute> resultList = new List<LazyObjects.AFAttribute>();

				foreach(var result in response.Items)
				{
					resultList.Add(LazyObjects.AFAttribute.Find(Connection, result.ID));
				}

				return resultList;
			}

			public IEnumerable<BaseObject> GetElementCategories(LazyPI.WebAPI.WebAPIConnection Connection, string elementWID)
			{
				var request = new RestRequest("/elements/{webId}/categories");
				request.AddUrlSegment("webId", elementWID);

				var results = Connection.Client.Execute<ResponseModels.ResponseList<ResponseModels.ElementCategory>>(request).Data;

				//return results.Items.Cast<BaseObject>();
			}

			IEnumerable<LazyObjects.AFElement> GetElements(LazyPI.WebAPI.WebAPIConnection Connection, string rootWID, string nameFilter = "*", string categoryName = "*", string templateName = "*", ElementType elementType = ElementType.Any, bool searchFullHierarchy = false, string sortField = "Name", string sortOrder = "Ascending", int startIndex = 0, int maxCount = 1000)
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

				var response = Connection.Client.Execute<ResponseModels.ResponseList<ResponseModels.AFElement>>(request).Data;

				List<LazyObjects.AFElement> results = new List<LazyObjects.AFElement>();

				foreach (var element in response.Items)
				{
					results.Add((LazyObjects.AFElement)_Factory.CreateInstance(Connection, element.ID, element.Name, element.Description, element.Path));
				}

				return results;
			}

			public IEnumerable<LazyObjects.AFEventFrame> GetEventFrames(LazyPI.WebAPI.WebAPIConnection Connection, string elementWID, LazyPI.SearchMode searchMode, DateTime startTime, DateTime endTime, string nameFilter, string categoryName, string templateName, string sortField, string sortOrder, int startIndex, int maxCount)
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

				var response = Connection.Client.Execute<ResponseModels.ResponseList<ResponseModels.AFEventFrame>>(request).Data;

				List<LazyObjects.AFEventFrame> results = new List<LazyObjects.AFEventFrame>();

				foreach (var frame in response.Items)
				{
					//TODO: Need EventFrame Search functions
					//LazyObjects.AFEventFrame.
				}

				//return results.Items.Cast<BaseObject>();
			}
		#endregion
	}
}