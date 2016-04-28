using LazyObjects = LazyPI.LazyObjects;
using ResponseModels = LazyPI.WebAPI.ResponseModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.WebAPI
{
	public class AFElementTemplateController : LazyObjects.IAFElementTemplateContoller
	{
		public LazyObjects.AFElementTemplate Find(LazyPI.Common.Connection Connection, string TemplateID)
		{
			WebAPIConnection webConnection = (WebAPIConnection)Connection;
			var request = new RestRequest("/elementtemplates/{webId}");
			request.AddUrlSegment("webId", TemplateID);

			var response = webConnection.Client.Execute<ResponseModels.AFElementTemplate>(request);

			if (response.ErrorException != null)
			{
				throw new ApplicationException("Error finding element template by ID. (See Inner Details)", response.ErrorException);
			}

			var data = response.Data;

			return new LazyObjects.AFElementTemplate(Connection, data.WebId, data.Id, data.Name, data.Description, data.Path);
		}

		public LazyObjects.AFElementTemplate FindByPath(LazyPI.Common.Connection Connection, string Path)
		{
			WebAPIConnection webConnection = (WebAPIConnection)Connection;
			var request = new RestRequest("/elementtemplates");
			request.AddParameter("path", Path, ParameterType.GetOrPost);

			var response = webConnection.Client.Execute<ResponseModels.AFElementTemplate>(request);

			if (response.ErrorException != null)
			{
				throw new ApplicationException("Error finding element template by path. (See Inner Details)", response.ErrorException);
			}

			var data = response.Data;

			return new LazyObjects.AFElementTemplate(Connection, data.WebId, data.Id, data.Name, data.Description, data.Path);
		}

		public bool Update(LazyPI.Common.Connection Connection, LazyObjects.AFElementTemplate template)
		{
			WebAPIConnection webConnection = (WebAPIConnection)Connection;
			var request = new RestRequest("/elementtemplates/{webId}", Method.PATCH);
			request.AddUrlSegment("webId", template.WebID);
            ResponseModels.AFElementTemplate temp = new ResponseModels.AFElementTemplate(template.ID, template.WebID, template.Name, template.Description, template.Path);
            temp.AllowElementToExtend = template.IsExtendable;

			request.AddBody(temp);
			var statusCode = webConnection.Client.Execute(request).StatusCode;

			return ((int)statusCode == 204);
		}

		public bool Delete(LazyPI.Common.Connection Connection, string TemplateID)
		{
			WebAPIConnection webConnection = (WebAPIConnection)Connection;
			var request = new RestRequest("/elementtemplates/{webId}", Method.DELETE);
			request.AddUrlSegment("webId", TemplateID);
			var statusCode = webConnection.Client.Execute(request).StatusCode;

			return ((int)statusCode == 204);
		}


		public bool CreateElementTemplate(LazyPI.Common.Connection Connection, string ParentID, LazyObjects.AFElementTemplate Template)
		{
			WebAPIConnection webConnection = (WebAPIConnection)Connection;
			var request = new RestRequest("/elementtemplates/{webId}/attributetemplates", Method.POST);
			request.AddUrlSegment("webId", ParentID);

			ResponseModels.AFElementTemplate temp = new ResponseModels.AFElementTemplate();

			temp.WebId = temp.Id;
			temp.Name = Template.Name;
			temp.Description = Template.Description;
			temp.Path = Template.Path;
			temp.CategoryNames = Template.Categories.ToList();
			temp.AllowElementToExtend = Template.IsExtendable;

			request.AddBody(temp);

			var statusCode = webConnection.Client.Execute(request).StatusCode;

			return ((int)statusCode == 201);
		}

		public bool IsExtendible(LazyPI.Common.Connection Connection, string TemplateID)
		{
			WebAPIConnection webConnection = (WebAPIConnection)Connection;
			var request = new RestRequest("/elementtemplates/{webId}");
			request.AddUrlSegment("webId", TemplateID);

			var response = webConnection.Client.Execute<ResponseModels.AFElementTemplate>(request);

			if (response.ErrorException != null)
			{
				throw new ApplicationException("Error checking if element template is extendible. (See Inner Details)", response.ErrorException);
			}

			var data = response.Data;

			return data.AllowElementToExtend;
		}

		public IEnumerable<string> GetCategories(LazyPI.Common.Connection Connection, string TemplateID)
		{
			WebAPIConnection webConnection = (WebAPIConnection)Connection;
			var request = new RestRequest("/elementtemplates/{webId}");
			request.AddUrlSegment("webId", TemplateID);

			var response = webConnection.Client.Execute<ResponseModels.AFElementTemplate>(request);

			if (response.ErrorException != null)
			{
				throw new ApplicationException("Error finding element template by ID. (See Inner Details)", response.ErrorException);
			}

			return response.Data.CategoryNames;
		}

		public IEnumerable<LazyObjects.AFAttributeTemplate> GetAttributeTemplates(LazyPI.Common.Connection Connection, string ElementID)
		{
			WebAPIConnection webConnection = (WebAPIConnection)Connection;
			var request = new RestRequest("/elementtemplates/{webId}/attributetemplates");
			request.AddUrlSegment("webId", ElementID);

			var response = webConnection.Client.Execute<ResponseModels.ResponseList<ResponseModels.AFAttributeTemplate>>(request);

			if (response.ErrorException != null)
			{
				throw new ApplicationException("Error finding element templates for element. (See Inner Details)", response.ErrorException);
			}

			var data = response.Data;

			List<LazyObjects.AFAttributeTemplate> results = new List<LazyObjects.AFAttributeTemplate>();

			foreach (var template in data.Items)
			{
				LazyObjects.AFAttributeTemplate attr = new LazyObjects.AFAttributeTemplate(Connection, template.WebId, template.Id, template.Name, template.Description, template.Path);
				results.Add(attr);
			}

			return results;
		}
	}
}
