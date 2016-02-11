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
		public LazyObjects.AFElementTemplate Find(WebAPIConnection Connection, string TemplateID)
		{
			var request = new RestRequest("/elementtemplates/{webId}");
			request.AddUrlSegment("webId", TemplateID);

			var response = Connection.Client.Execute<ResponseModels.AFElementTemplate>(request);

			if (response.ErrorException != null)
			{
				throw new ApplicationException("Error finding element template by ID. (See Inner Details)", response.ErrorException);
			}

			var data = response.Data;

			return new LazyObjects.AFElementTemplate(Connection, data.WebID, data.Name, data.Description, data.Path);
		}

		public LazyObjects.AFElementTemplate FindByPath(WebAPIConnection Connection, string Path)
		{
			var request = new RestRequest("/elementtemplates");
			request.AddParameter("path", Path);

			var response = Connection.Client.Execute<ResponseModels.AFElementTemplate>(request);

			if (response.ErrorException != null)
			{
				throw new ApplicationException("Error finding element template by path. (See Inner Details)", response.ErrorException);
			}

			var data = response.Data;

			return new LazyObjects.AFElementTemplate(Connection, data.WebID, data.Name, data.Description, data.Path);
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

            var response = Connection.Client.Execute<ResponseModels.AFElementTemplate>(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Error checking if element template is extendible. (See Inner Details)", response.ErrorException);
            }

			var data = response.Data;

			return data.AllowElementToExtend;
		}

		public IEnumerable<LazyObjects.AFAttributeTemplate> GetAttributeTemplates(WebAPIConnection Connection, string ElementID)
		{
			var request = new RestRequest("/elementtemplates/{webId}/attributetemplates");
			request.AddUrlSegment("webId", ElementID);

            var response = Connection.Client.Execute<ResponseModels.ResponseList<ResponseModels.AFAttributeTemplate>>(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Error finding element templates for element. (See Inner Details)", response.ErrorException);
            }

			var data = response.Data;

			List<LazyObjects.AFAttributeTemplate> results = new List<LazyObjects.AFAttributeTemplate>();

			foreach (var template in data.Items)
			{
				LazyObjects.AFAttributeTemplate attr = new LazyObjects.AFAttributeTemplate(Connection, template.WebID, template.Name, template.Description, template.Path);
				results.Add(attr);
			}

			return results;
		}
	}
}
