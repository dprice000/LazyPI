using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIWebSharp.LazyObjects
{
	public class AFElementTemplateLoader : IAFElementTemplateLoader
	{
		string _serverAddress;
		RestClient _client;

		public AFElementTemplateLoader()
		{
			_serverAddress = "https://localhost/webapi";
		   _client  = new RestClient(_serverAddress);
		}

		public AFElementTemplate Find(string templateWID)
		{
			var request = new RestRequest("/elementtemplates/{webId}");
			request.AddUrlSegment("webId", templateWID);

		   PIWebSharp.WebAPI.AFElementTemplate template = _client.Execute<PIWebSharp.WebAPI.AFElementTemplate>(request).Data

			return ;
		}

		public AFElementTemplate FindByPath(string path)
		{
			var request = new RestRequest("/elementtemplates");
			request.AddParameter("path", path);

			PIWebSharp.WebAPI.AFElementTemplate template = _client.Execute<PIWebSharp.WebAPI.AFElementTemplate>(request).Data;

			return;
		}

		public bool Update(AFElementTemplate template)
		{
			var request = new RestRequest("/elementtemplates/{webId}", Method.PATCH);
			request.AddUrlSegment("webId", template.WebID);
			request.AddBody(template);
			var statusCode = _client.Execute(request).StatusCode;

			return ((int)statusCode == 204);
		}

		public bool Delete(string templateWID)
		{
			var request = new RestRequest("/elementtemplates/{webId}", Method.DELETE);
			request.AddUrlSegment("webId", templateWID);
			var statusCode = _client.Execute(request).StatusCode;

			return ((int)statusCode == 204);
		}


		public bool CreateElementTemplate(string parentWID, AFAttributeTemplate template)
		{
			var request = new RestRequest("/elementtemplates/{webId}/attributetemplates", Method.POST);
			request.AddUrlSegment("webId", parentWID);
			request.AddBody(template);

			var statusCode = _client.Execute(request).StatusCode;

			return ((int)statusCode == 201);
		}

		public IEnumerable<AFAttributeTemplate> GetAttributeTemplates(string elementID)
		{
			var request = new RestRequest("/elementtemplates/{webId}/attributetemplates");
			request.AddUrlSegment("webId", elementID);

			return _client.Execute<IEnumerable<AttributeTemplate>>(request).Data;
		}
	}
}
