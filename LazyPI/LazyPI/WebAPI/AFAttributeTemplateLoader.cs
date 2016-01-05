using LazyPI.LazyObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace LazyPI.WebAPI
{
	public class AFAttributeTemplateLoader : LazyPI.LazyObjects.IAFAttributeTemplateLoader
	{
		private string _serverAddress;
		private RestClient _client;

		public AFAttributeTemplateLoader()
		{
			_serverAddress = "https://localhost/webapi";
		   _client  = new RestClient(_serverAddress);
		}

		public AFAttributeTemplate Find(string ID)
		{
			var request = new RestRequest("/attributetemplates/{webId}");
			request.AddUrlSegment("webId", ID);

			var result = _client.Execute<LazyPI.WebAPI.ResponseModels.AFAttributeTemplate>(request).Data;
		}

		public AFAttributeTemplate FindByPath(string path)
		{
			var request = new RestRequest("/attributetemplates");
			request.AddParameter("path", path);

			var result = _client.Execute<LazyPI.WebAPI.ResponseModels.AFAttributeTemplate>(request).Data;
		}

		public bool Update(AFAttributeTemplate attrTemp)
		{
			var request = new RestRequest("/attributetemplates/{webId}", Method.PATCH);
			request.AddUrlSegment("webId", attrTemp.ID);
			request.AddBody(attrTemp);

			var statusCode = _client.Execute(request).StatusCode;

			return ((int)statusCode == 204);
		}

		public bool Delete(string attrTempID)
		{
			var request = new RestRequest("/attributetemplates/{webId}");
			request.AddParameter("webId", attrTempID);

			var statusCode = _client.Execute(request).StatusCode;

			return ((int)statusCode == 204);
		}

		//This really creates a childe attributetemplate
		//TODO: Something is wrong here ID should be a parent ID
		public bool Create(AFAttributeTemplate attrTemp)
		{
			var request = new RestRequest("/attributetemplates/{webId}/attributetemplates");
			request.AddUrlSegment("webId", attrTemp.ID);
			request.AddBody(attrTemp);

			var statusCode = _client.Execute(request).StatusCode;
			return ((int)statusCode == 201);
		}

		public IEnumerable<AFAttributeTemplate> GetChildAttributeTemplates(string ID)
		{
			var request = new RestRequest("/attributetemplates/{webId}/attributetemplates");
			request.AddUrlSegment("webId", ID);

			var results = _client.Execute<LazyPI.WebAPI.ResponseModels.ResponseList<LazyPI.WebAPI.ResponseModels.AFAttributeTemplate>>(request).Data;
		}       
	}
}
