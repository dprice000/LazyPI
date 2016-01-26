using Lazyobject = LazyPI.LazyObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace LazyPI.WebAPI
{
	public class AFAttributeTemplateLoader : LazyObjects.IAFAttributeTemplate
	{
		private LazyPI.LazyObjects.ILazyFactory _Factory;

		public AFAttributeTemplateLoader()
		{
		}

		public LazyObjects.AFAttributeTemplate Find(WebAPIConnection Connection, string AttrTempID)
		{
			var request = new RestRequest("/attributetemplates/{webId}");
			request.AddUrlSegment("webId", AttrTempID);

			var result = Connection.Client.Execute<ResponseModels.AFAttributeTemplate>(request).Data;

			return (LazyObjects.AFAttributeTemplate)_Factory.CreateInstance(Connection, result.WebID, result.Name, result.Description, result.Path);
		}

		public LazyObjects.AFAttributeTemplate FindByPath(WebAPIConnection Connection, string Path)
		{
			var request = new RestRequest("/attributetemplates");
			request.AddParameter("path", Path);

			var result = Connection.Client.Execute<LazyPI.WebAPI.ResponseModels.AFAttributeTemplate>(request).Data;
			return (LazyObjects.AFAttributeTemplate)_Factory.CreateInstance(Connection, result.WebID, result.Name, result.Description, result.Path);
		}

		public bool Update(WebAPIConnection Connection, LazyObjects.AFAttributeTemplate AttrTemp)
		{
			var request = new RestRequest("/attributetemplates/{webId}", Method.PATCH);
			request.AddUrlSegment("webId", AttrTemp.ID);
			request.AddBody(AttrTemp);

			var statusCode = Connection.Client.Execute(request).StatusCode;

			return ((int)statusCode == 204);
		}

		public bool Delete(WebAPIConnection Connection, string AttrTempID)
		{
			var request = new RestRequest("/attributetemplates/{webId}");
			request.AddParameter("webId", AttrTempID);

			var statusCode = Connection.Client.Execute(request).StatusCode;

			return ((int)statusCode == 204);
		}

		//This really creates a childe attributetemplate
		//TODO: Something is wrong here ID should be a parent ID
		public bool Create(WebAPIConnection Connection, LazyObjects.AFAttributeTemplate AttrTemp)
		{
			var request = new RestRequest("/attributetemplates/{webId}/attributetemplates");
			request.AddUrlSegment("webId", AttrTemp.ID);
			request.AddBody(AttrTemp);

			var statusCode = Connection.Client.Execute(request).StatusCode;
			return ((int)statusCode == 201);
		}

		public IEnumerable<LazyObjects.AFAttributeTemplate> GetChildAttributeTemplates(WebAPIConnection Connection, string AttrTempID)
		{
			var request = new RestRequest("/attributetemplates/{webId}/attributetemplates");
			request.AddUrlSegment("webId", AttrTempID);
			
			var results = Connection.Client.Execute<ResponseModels.ResponseList<ResponseModels.AFAttributeTemplate>>(request).Data;
			List<LazyObjects.AFAttributeTemplate> templateList = new List<LazyObjects.AFAttributeTemplate>();

			foreach(var template in results.Items)
			{
				LazyObjects.AFAttributeTemplate attrTemplate = new Lazyobject.AFAttributeTemplate();
				templateList.Add(attrTemplate);
			}

			return templateList;
		}       
	}
}
