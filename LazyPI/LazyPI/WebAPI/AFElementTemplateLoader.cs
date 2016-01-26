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
	public class AFElementTemplateLoader : LazyObjects.IAFElementTemplate
	{
		private LazyObjects.ILazyFactory _Factory;

		public AFElementTemplateLoader()
		{
		}

		public LazyObjects.AFElementTemplate Find(WebAPIConnection Connection, string templateID)
		{
			var request = new RestRequest("/elementtemplates/{webId}");
			request.AddUrlSegment("webId", templateID);

			var response = Connection.Client.Execute<ResponseModels.AFElementTemplate>(request).Data;

			return (LazyObjects.AFElementTemplate)_Factory.CreateInstance(Connection, response.WebID, response.Name, response.Description, response.Path);

		}

		public LazyObjects.AFElementTemplate FindByPath(WebAPIConnection Connection, string path)
		{
			var request = new RestRequest("/elementtemplates");
			request.AddParameter("path", path);

			var response = Connection.Client.Execute<ResponseModels.AFElementTemplate>(request).Data;

			return (LazyObjects.AFElementTemplate)_Factory.CreateInstance(Connection, response.WebID, response.Name, response.Description, response.Path);
		}

		public bool Update(WebAPIConnection Connection, LazyObjects.AFElementTemplate template)
		{
			var request = new RestRequest("/elementtemplates/{webId}", Method.PATCH);
			request.AddUrlSegment("webId", template.ID);
			request.AddBody(template);
			var statusCode = Connection.Client.Execute(request).StatusCode;

			return ((int)statusCode == 204);
		}

		public bool Delete(WebAPIConnection Connection, string templateID)
		{
			var request = new RestRequest("/elementtemplates/{webId}", Method.DELETE);
			request.AddUrlSegment("webId", templateID);
			var statusCode = Connection.Client.Execute(request).StatusCode;

			return ((int)statusCode == 204);
		}


		public bool CreateElementTemplate(WebAPIConnection Connection, string parentWID, LazyObjects.AFAttributeTemplate template)
		{
			var request = new RestRequest("/elementtemplates/{webId}/attributetemplates", Method.POST);
			request.AddUrlSegment("webId", parentWID);
			request.AddBody(template);

			var statusCode = Connection.Client.Execute(request).StatusCode;

			return ((int)statusCode == 201);
		}

		public bool IsExtendible(WebAPIConnection Connection, string templateID)
		{
			var request = new RestRequest("/elementtemplates/{webId}");
			request.AddUrlSegment("webId", templateID);

			var result = Connection.Client.Execute<ResponseModels.AFElementTemplate>(request).Data;

			return result.AllowElementToExtend;
		}

		public IEnumerable<LazyObjects.AFAttributeTemplate> GetAttributeTemplates(WebAPIConnection Connection, string elementID)
		{
			var request = new RestRequest("/elementtemplates/{webId}/attributetemplates");
			request.AddUrlSegment("webId", elementID);

			var response = Connection.Client.Execute<ResponseModels.ResponseList<ResponseModels.AFAttributeTemplate>>(request).Data;

			List<LazyObjects.AFAttributeTemplate> results = new List<LazyObjects.AFAttributeTemplate>();

			foreach (var template in response.Items)
			{
				LazyObjects.AFAttributeTemplate attr = (LazyObjects.AFAttributeTemplate)_Factory.CreateInstance(Connection, template.WebID, template.Name, template.Description, template.Path);
				results.Add(attr);
			}

			return results;
		}
	}
}
