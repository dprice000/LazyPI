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
		string _serverAddress;
		RestClient _client;

		public AFElementTemplateLoader()
		{
			_serverAddress = "https://localhost/webapi";
		   _client  = new RestClient(_serverAddress);
		}

		public LazyObjects.AFElementTemplate Find(string templateID)
		{
			var request = new RestRequest("/elementtemplates/{webId}");
			request.AddUrlSegment("webId", templateID);

			var result = _client.Execute<ResponseModels.AFElementTemplate>(request).Data;
			//return ;
		}

		public LazyObjects.AFElementTemplate FindByPath(string path)
		{
			var request = new RestRequest("/elementtemplates");
			request.AddParameter("path", path);

			var result = _client.Execute<ResponseModels.AFElementTemplate>(request).Data;

			//return;
		}

		public bool Update(LazyObjects.AFElementTemplate template)
		{
			var request = new RestRequest("/elementtemplates/{webId}", Method.PATCH);
			request.AddUrlSegment("webId", template.ID);
			request.AddBody(template);
			var statusCode = _client.Execute(request).StatusCode;

			return ((int)statusCode == 204);
		}

		public bool Delete(string templateID)
		{
			var request = new RestRequest("/elementtemplates/{webId}", Method.DELETE);
			request.AddUrlSegment("webId", templateID);
			var statusCode = _client.Execute(request).StatusCode;

			return ((int)statusCode == 204);
		}


		public bool CreateElementTemplate(string parentWID, LazyObjects.AFAttributeTemplate template)
		{
			var request = new RestRequest("/elementtemplates/{webId}/attributetemplates", Method.POST);
			request.AddUrlSegment("webId", parentWID);
			request.AddBody(template);

			var statusCode = _client.Execute(request).StatusCode;

			return ((int)statusCode == 201);
		}

		public IEnumerable<LazyObjects.AFAttributeTemplate> GetAttributeTemplates(string elementID)
		{
			var request = new RestRequest("/elementtemplates/{webId}/attributetemplates");
			request.AddUrlSegment("webId", elementID);

			var result = _client.Execute<ResponseModels.ResponseList<ResponseModels.AFAttributeTemplate>>(request).Data;

		}
	}
}
