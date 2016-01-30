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

		public AFElementTemplateLoader(LazyObjects.ILazyFactory Factory)
		{
			_Factory = Factory;
		}

		public LazyObjects.AFElementTemplate Find(WebAPIConnection Connection, string TemplateID)
		{
			var request = new RestRequest("/elementtemplates/{webId}");
			request.AddUrlSegment("webId", TemplateID);

			var response = Connection.Client.Execute<ResponseModels.AFElementTemplate>(request).Data;

			return (LazyObjects.AFElementTemplate)_Factory.CreateInstance(Connection, response.WebID, response.Name, response.Description, response.Path);

		}

		public LazyObjects.AFElementTemplate FindByPath(WebAPIConnection Connection, string Path)
		{
			var request = new RestRequest("/elementtemplates");
			request.AddParameter("path", Path);

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

		public bool Delete(WebAPIConnection Connection, string TemplateID)
		{
			var request = new RestRequest("/elementtemplates/{webId}", Method.DELETE);
			request.AddUrlSegment("webId", TemplateID);
			var statusCode = Connection.Client.Execute(request).StatusCode;

			return ((int)statusCode == 204);
		}


		public bool CreateElementTemplate(WebAPIConnection Connection, string ParentID, LazyObjects.AFAttributeTemplate Template)
		{
			var request = new RestRequest("/elementtemplates/{webId}/attributetemplates", Method.POST);
			request.AddUrlSegment("webId", ParentID);
			request.AddBody(Template);

			var statusCode = Connection.Client.Execute(request).StatusCode;

			return ((int)statusCode == 201);
		}

		public bool IsExtendible(WebAPIConnection Connection, string TemplateID)
		{
			var request = new RestRequest("/elementtemplates/{webId}");
			request.AddUrlSegment("webId", TemplateID);

			var result = Connection.Client.Execute<ResponseModels.AFElementTemplate>(request).Data;

			return result.AllowElementToExtend;
		}

		public IEnumerable<LazyObjects.AFAttributeTemplate> GetAttributeTemplates(WebAPIConnection Connection, string ElementID)
		{
			var request = new RestRequest("/elementtemplates/{webId}/attributetemplates");
			request.AddUrlSegment("webId", ElementID);

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
