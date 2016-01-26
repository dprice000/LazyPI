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
			public LazyObjects.AFElement Find(LazyPI.WebAPI.WebAPIConnection Connection,string ElementID)
			{
				var request = new RestRequest("/elements/{webId}");
				request.AddUrlSegment("webId", ElementID);

				var result = Connection.Client.Execute<LazyPI.WebAPI.ResponseModels.AFElement>(request).Data;
				return (LazyObjects.AFElement)_Factory.CreateInstance(Connection, result.WebID, result.Name, result.Description, result.Path);
			}

			public LazyObjects.AFElement FindByPath(LazyPI.WebAPI.WebAPIConnection Connection, string Path)
			{
				var request = new RestRequest("/elements");
				request.AddParameter("path", Path);

				var result = Connection.Client.Execute<LazyPI.WebAPI.ResponseModels.AFElement>(request).Data;
				return (LazyObjects.AFElement)_Factory.CreateInstance(Connection, result.WebID, result.Name, result.Description, result.Path);
			}

			public bool Update(LazyPI.WebAPI.WebAPIConnection Connection, LazyObjects.AFElement Element)
			{
				var request = new RestRequest("/elements/{webId}", Method.PATCH);
				request.AddUrlSegment("webId", Element.ID);

				//TODO: You need to convert this object before putting it in the request
				//request.AddBody(element);

				var statusCode = Connection.Client.Execute(request).StatusCode;

				return ((int)statusCode == 204);
			}

			public bool Delete(LazyPI.WebAPI.WebAPIConnection Connection, string ElementID)
			{
				var request = new RestRequest("/elements/{webId}", Method.DELETE);
				request.AddUrlSegment("webId", ElementID);

				var statusCode = Connection.Client.Execute(request).StatusCode;

				return ((int)statusCode == 204);
			}

			public bool CreateAttribute(LazyPI.WebAPI.WebAPIConnection Connection, string ParentID, LazyObjects.AFAttribute Attr)
			{
				var request = new RestRequest("/elements/{webId}/attributes", Method.POST);
				request.AddUrlSegment("webId", ParentID);
				request.AddBody(Attr);

				var statusCode = Connection.Client.Execute(request).StatusCode;

				return ((int)statusCode == 201);
			}

			public bool CreateChildElement(LazyPI.WebAPI.WebAPIConnection Connection, string ParentID, LazyObjects.AFElement Element)
			{
				var request = new RestRequest("/elements/{webId}/elements", Method.POST);
				request.AddUrlSegment("webId", ParentID);
				request.AddBody(Element);

				var statusCode = Connection.Client.Execute(request).StatusCode;

				return ((int)statusCode == 201);
			}

			public string GetElementTemplate(LazyPI.WebAPI.WebAPIConnection Connection, string ElementID)
			{
				var request = new RestRequest("/elements/{webId}");
				request.AddUrlSegment("webId", ElementID);

				var result = Connection.Client.Execute<LazyPI.WebAPI.ResponseModels.AFElement>(request).Data;
				return result.TemplateName;
			}

			public IEnumerable<LazyObjects.AFAttribute> GetAttributes(LazyPI.WebAPI.WebAPIConnection Connection, string ParentID, string NameFilter = "*", string CategoryName = "*", string TemplateName = "*", string ValueType = "*", bool SearchFullHierarchy = false, string SortField = "Name", string SortOrder = "Ascending", int StartIndex = 0, bool ShowExcluded = false, bool ShowHidden = false, int MaxCount = 1000)
			{
				var request = new RestRequest("/elements/{webId}/attributes");
				request.AddUrlSegment("webId", ParentID);
				request.AddParameter("nameFilter", NameFilter);
				request.AddParameter("categoryName", CategoryName);
				request.AddParameter("templateName", TemplateName);
				request.AddParameter("valueType", ValueType);
				request.AddParameter("searchFullHierarchy", SearchFullHierarchy);
				request.AddParameter("sortField", SortField);
				request.AddParameter("sortOrder", SortOrder);
				request.AddParameter("startIndex", StartIndex);
				request.AddParameter("showExcluded", ShowExcluded);
				request.AddParameter("showHidden", ShowHidden);
				request.AddParameter("maxCount", MaxCount);

				var response = Connection.Client.Execute<ResponseModels.ResponseList<ResponseModels.AFAttribute>>(request).Data;

				List<LazyObjects.AFAttribute> resultList = new List<LazyObjects.AFAttribute>();

				foreach(var result in response.Items)
				{
					resultList.Add(LazyObjects.AFAttribute.Find(Connection, result.WebID));
				}

				return resultList;
			}

			public IEnumerable<BaseObject> GetElementCategories(LazyPI.WebAPI.WebAPIConnection Connection, string ElementID)
			{
				var request = new RestRequest("/elements/{webId}/categories");
				request.AddUrlSegment("webId", ElementID);

				var results = Connection.Client.Execute<ResponseModels.ResponseList<ResponseModels.ElementCategory>>(request).Data;

				//return results.Items.Cast<BaseObject>();
			}

			IEnumerable<LazyObjects.AFElement> GetElements(LazyPI.WebAPI.WebAPIConnection Connection, string RootID, string NameFilter = "*", string CategoryName = "*", string TemplateName = "*", ElementType ElementType = ElementType.Any, bool SearchFullHierarchy = false, string SortField = "Name", string SortOrder = "Ascending", int StartIndex = 0, int MaxCount = 1000)
			{
				var request = new RestRequest("/elements/{webId}/elements");
				request.AddUrlSegment("webId", RootID);
				request.AddParameter("nameFilter", NameFilter);
				request.AddParameter("templateName", TemplateName);
				request.AddParameter("elementType", ElementType);
				request.AddParameter("searchFullHierarchy", SearchFullHierarchy);
				request.AddParameter("sortField", SortField);
				request.AddParameter("sortOrder", SortOrder);
				request.AddParameter("startIndex", StartIndex);
				request.AddParameter("maxCount", MaxCount);

				var response = Connection.Client.Execute<ResponseModels.ResponseList<ResponseModels.AFElement>>(request).Data;

				List<LazyObjects.AFElement> results = new List<LazyObjects.AFElement>();

				foreach (var element in response.Items)
				{
					results.Add((LazyObjects.AFElement)_Factory.CreateInstance(Connection, element.WebID, element.Name, element.Description, element.Path));
				}

				return results;
			}

			public IEnumerable<LazyObjects.AFEventFrame> GetEventFrames(LazyPI.WebAPI.WebAPIConnection Connection, string ElementID, LazyPI.SearchMode SearchMode, DateTime StartTime, DateTime EndTime, string NameFilter, string CategoryName, string TemplateName, string SortField, string SortOrder, int StartIndex, int MaxCount)
			{
				var request = new RestRequest("/elements/{webId}/eventframes");
				request.AddUrlSegment("webId", ElementID);
				request.AddParameter("searchMode", SearchMode);
				request.AddParameter("startTime", StartTime);
				request.AddParameter("endTime", EndTime);
				request.AddParameter("nameFilter", NameFilter);
				request.AddParameter("categoryName", CategoryName);
				request.AddParameter("templateName", TemplateName);
				request.AddParameter("sortField", SortField);
				request.AddParameter("sortOrder", SortOrder);
				request.AddParameter("startIndex", StartIndex);
				request.AddParameter("maxCount", MaxCount);

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