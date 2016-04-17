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
	public class AFElementController : LazyObjects.IAFElementController
	{
		// These functions have direct references to WebAPI calls
		#region "Public Methods"
			public LazyObjects.AFElement Find(LazyPI.Common.Connection Connection,string ElementID)
			{
				WebAPIConnection webConnection = (WebAPIConnection)Connection;
				var request = new RestRequest("/elements/{webId}");
				request.AddUrlSegment("webId", ElementID);

				var response = webConnection.Client.Execute<LazyPI.WebAPI.ResponseModels.AFElement>(request);

				if (response.ErrorException != null)
				{
					throw new ApplicationException("Error finding element by ID. (See Inner Details)", response.ErrorException);
				}

				ResponseModels.AFElement data = response.Data;
				return new LazyObjects.AFElement(Connection, data.WebID, data.Name, data.Description, data.Path);
			}

			public LazyObjects.AFElement FindByPath(LazyPI.Common.Connection Connection, string Path)
			{
				WebAPIConnection webConnection = (WebAPIConnection)Connection;
				var request = new RestRequest("/elements");
				request.AddParameter("path", Path);

				var response = webConnection.Client.Execute<LazyPI.WebAPI.ResponseModels.AFElement>(request);
				var data = response.Data;

				if (response.ErrorException != null)
				{
					throw new ApplicationException("Error finding element by path. (See Inner Details)", response.ErrorException);
				}

				return new LazyObjects.AFElement(Connection, data.WebID, data.Name, data.Description, data.Path);
			}

			public bool Update(LazyPI.Common.Connection Connection, LazyObjects.AFElement Element)
			{
				WebAPIConnection webConnection = (WebAPIConnection)Connection;
				var request = new RestRequest("/elements/{webId}", Method.PATCH);
				request.AddUrlSegment("webId", Element.ID);

				ResponseModels.AFElement temp = new ResponseModels.AFElement();
				temp.WebID = Element.ID;
				temp.Name = Element.Name;
				temp.Description = Element.Description;
				temp.Path = Element.Path;
				temp.CategoryNames = Element.Categories.ToList();
				temp.TemplateName = Element.Template.Name;

				request.AddBody(temp);

				var statusCode = webConnection.Client.Execute(request).StatusCode;

				return ((int)statusCode == 204);
			}

			public bool Delete(LazyPI.Common.Connection Connection, string ElementID)
			{
				WebAPIConnection webConnection = (WebAPIConnection)Connection;
				var request = new RestRequest("/elements/{webId}", Method.DELETE);
				request.AddUrlSegment("webId", ElementID);

				var statusCode = webConnection.Client.Execute(request).StatusCode;

				return ((int)statusCode == 204);
			}

			public bool CreateAttribute(LazyPI.Common.Connection Connection, string ParentID, LazyObjects.AFAttribute Attr)
			{
				WebAPIConnection webConnection = (WebAPIConnection)Connection;
				var request = new RestRequest("/elements/{webId}/attributes", Method.POST);
				request.AddUrlSegment("webId", ParentID);
				request.AddBody(Attr);

				var statusCode = webConnection.Client.Execute(request).StatusCode;

				return ((int)statusCode == 201);
			}

			public bool CreateChildElement(LazyPI.Common.Connection Connection, string ParentID, LazyObjects.AFElement Element)
			{
				WebAPIConnection webConnection = (WebAPIConnection)Connection;
				var request = new RestRequest("/elements/{webId}/elements", Method.POST);
				request.AddUrlSegment("webId", ParentID);
				request.AddBody(Element);

				var statusCode = webConnection.Client.Execute(request).StatusCode;

				return ((int)statusCode == 201);
			}

			public string GetElementTemplate(LazyPI.Common.Connection Connection, string ElementID)
			{
				WebAPIConnection webConnection = (WebAPIConnection)Connection;
				var request = new RestRequest("/elements/{webId}");
				request.AddUrlSegment("webId", ElementID);
				var response = webConnection.Client.Execute<LazyPI.WebAPI.ResponseModels.AFElement>(request);

				if (response.ErrorException != null)
				{
					throw new ApplicationException("Error searching for element template. (See Inner Details)", response.ErrorException);
				}

				var data = response.Data;
				return data.TemplateName;
			}

			public IEnumerable<LazyObjects.AFAttribute> GetAttributes(LazyPI.Common.Connection Connection, string ParentID, string NameFilter = "*", string CategoryName = "*", string TemplateName = "*", string ValueType = "*", bool SearchFullHierarchy = false, string SortField = "Name", string SortOrder = "Ascending", int StartIndex = 0, bool ShowExcluded = false, bool ShowHidden = false, int MaxCount = 1000)
			{
				WebAPIConnection webConnection = (WebAPIConnection)Connection;
				var request = new RestRequest("/elements/{webId}/attributes");
				request.AddUrlSegment("webId", ParentID);

				if(NameFilter != "*")
					request.AddParameter("nameFilter", NameFilter);

				if(CategoryName != "*")
					request.AddParameter("categoryName", CategoryName);

				if(TemplateName != "*")
					request.AddParameter("templateName", TemplateName);

				if(ValueType != "*")
					request.AddParameter("valueType", ValueType);

				if(SearchFullHierarchy)
					request.AddParameter("searchFullHierarchy", SearchFullHierarchy);

				if(SortField != "Name")
					request.AddParameter("sortField", SortField);

				if(SortOrder != "Ascending")
					request.AddParameter("sortOrder", SortOrder);

				if(StartIndex != 0)
					request.AddParameter("startIndex", StartIndex);

				if(ShowExcluded)
					request.AddParameter("showExcluded", ShowExcluded);

				if(ShowHidden)
					request.AddParameter("showHidden", ShowHidden);

				if(MaxCount != 1000)
					request.AddParameter("maxCount", MaxCount);

				var response = webConnection.Client.Execute<ResponseModels.ResponseList<ResponseModels.AFAttribute>>(request);

				if (response.ErrorException != null)
				{
					throw new ApplicationException("Error searching for element attributes. (See Inner Details)", response.ErrorException);
				}
				
				var data = response.Data;

				List<LazyObjects.AFAttribute> resultList = new List<LazyObjects.AFAttribute>();

				foreach(var result in data.Items)
				{
					resultList.Add(LazyObjects.AFAttribute.Find(Connection, result.WebID));
				}

				return resultList;
			}

			public IEnumerable<string> GetCategories(LazyPI.Common.Connection Connection, string ElementID)
			{
				WebAPIConnection webConnection = (WebAPIConnection)Connection;
				var request = new RestRequest("/elements/{webId}/categories");
				request.AddUrlSegment("webId", ElementID);

				var response = webConnection.Client.Execute<ResponseModels.ResponseList<ResponseModels.ElementCategory>>(request);

				if (response.ErrorException != null)
				{
					throw new ApplicationException("Error searching for element categories. (See Inner Details)", response.ErrorException);
				}

				var data = response.Data;
				List<string> results = new List<string>();
				
				foreach(ResponseModels.ElementCategory category in data.Items)
				{
					results.Add(category.Name);
				}

				return results;
			}

			public IEnumerable<LazyObjects.AFElement> GetElements(LazyPI.Common.Connection Connection, string RootID, string NameFilter = "*", string CategoryName = "*", string TemplateName = "*", LazyPI.Common.ElementType ElementType = LazyPI.Common.ElementType.Any, bool SearchFullHierarchy = false, string SortField = "Name", string SortOrder = "Ascending", int StartIndex = 0, int MaxCount = 1000)
			{
				WebAPIConnection webConnection = (WebAPIConnection)Connection;
				var request = new RestRequest("/elements/{webId}/elements");
				request.AddUrlSegment("webId", RootID);

				if(NameFilter != "*")
					request.AddParameter("nameFilter", NameFilter);

				if(TemplateName != "*")
					request.AddParameter("templateName", TemplateName);

				if(ElementType != Common.ElementType.Any)
					request.AddParameter("elementType", ElementType);

				if(SearchFullHierarchy)
					request.AddParameter("searchFullHierarchy", SearchFullHierarchy);

				if(SortField != "Name")
					request.AddParameter("sortField", SortField);

				if(SortOrder != "Ascending")
					request.AddParameter("sortOrder", SortOrder);

				if(StartIndex != 0)
					request.AddParameter("startIndex", StartIndex);

				if(MaxCount != 1000)
					request.AddParameter("maxCount", MaxCount);

				var response = webConnection.Client.Execute<ResponseModels.ResponseList<ResponseModels.AFElement>>(request);

				if (response.ErrorException != null)
				{
					throw new ApplicationException("Error searching for child elements of and element. (See Inner Details)", response.ErrorException);
				}
				
				var data = response.Data;

				List<LazyObjects.AFElement> results = new List<LazyObjects.AFElement>();

				foreach (var element in data.Items)
				{
					results.Add(new LazyObjects.AFElement(Connection, element.WebID, element.Name, element.Description, element.Path));
				}

				return results;
			}

			public IEnumerable<LazyObjects.AFEventFrame> GetEventFrames(LazyPI.Common.Connection Connection, string ElementID, LazyPI.Common.SearchMode SearchMode, DateTime StartTime, DateTime EndTime, string NameFilter, string CategoryName, string TemplateName, string SortField, string SortOrder, int StartIndex, int MaxCount)
			{
				WebAPIConnection webConnection = (WebAPIConnection)Connection;
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

				var response = webConnection.Client.Execute<ResponseModels.ResponseList<ResponseModels.AFEventFrame>>(request);

				if (response.ErrorException != null)
				{
					throw new ApplicationException("Error searching for element template. (See Inner Details)", response.ErrorException);
				}
				
				var data = response.Data;

				List<LazyObjects.AFEventFrame> results = new List<LazyObjects.AFEventFrame>();

				foreach (ResponseModels.AFEventFrame frame in data.Items)
				{
					results.Add(new LazyObjects.AFEventFrame(Connection, frame.WebID, frame.Name, frame.Description, frame.Path));
				}

				return results;
			}
		#endregion
	}
}