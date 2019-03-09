using RestSharp;
using System;
using System.Collections.Generic;
using Lazyobject = LazyPI.LazyObjects;

namespace LazyPI.WebAPI
{
    public class AFAttributeTemplateController : LazyObjects.IAFAttributeTemplateController
    {
        public LazyObjects.AFAttributeTemplate Find(LazyPI.Common.Connection Connection, string AttrTempID)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/attributetemplates/{webId}");
            request.AddUrlSegment("webId", AttrTempID);

            var response = webConnection.Client.Execute<ResponseModels.AFAttributeTemplate>(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Error finding attribute template by ID. (See Inner Details)", response.ErrorException);
            }

            var data = response.Data;

            return new LazyObjects.AFAttributeTemplate(Connection, data.WebId, data.Id, data.Name, data.Description, data.Path);
        }

        public LazyObjects.AFAttributeTemplate FindByPath(LazyPI.Common.Connection Connection, string Path)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/attributetemplates");
            request.AddParameter("path", Path, ParameterType.GetOrPost);

            var response = webConnection.Client.Execute<LazyPI.WebAPI.ResponseModels.AFAttributeTemplate>(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Error finding attribute template by path. (See Inner Details)", response.ErrorException);
            }

            var data = response.Data;
            return new LazyObjects.AFAttributeTemplate(Connection, data.WebId, data.Id, data.Name, data.Description, data.Path);
        }

        public bool Update(LazyPI.Common.Connection Connection, LazyObjects.AFAttributeTemplate AttrTemp)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/attributetemplates/{webId}", Method.PATCH);
            request.AddUrlSegment("webId", AttrTemp.WebID);

            request.AddBody(AttrTemp);

            var statusCode = webConnection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 204);
        }

        public bool Delete(LazyPI.Common.Connection Connection, string AttrTempID)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/attributetemplates/{webId}");
            request.AddParameter("webId", AttrTempID);

            var statusCode = webConnection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 204);
        }

        //This really creates a childe attributetemplate
        //TODO: Something is wrong here ID should be a parent ID
        public bool Create(LazyPI.Common.Connection Connection, LazyObjects.AFAttributeTemplate AttrTemp)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/attributetemplates/{webId}/attributetemplates");
            request.AddUrlSegment("webId", AttrTemp.WebID);
            request.AddBody(AttrTemp);

            var statusCode = webConnection.Client.Execute(request).StatusCode;
            return ((int)statusCode == 201);
        }

        public IEnumerable<LazyObjects.AFAttributeTemplate> GetChildAttributeTemplates(LazyPI.Common.Connection Connection, string AttrTempID)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/attributetemplates/{webId}/attributetemplates");
            request.AddUrlSegment("webId", AttrTempID);

            var response = webConnection.Client.Execute<ResponseModels.ResponseList<ResponseModels.AFAttributeTemplate>>(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Error finding attribute template children. (See Inner Details)", response.ErrorException);
            }

            var data = response.Data;
            List<LazyObjects.AFAttributeTemplate> templateList = new List<LazyObjects.AFAttributeTemplate>();

            foreach (var template in data.Items)
            {
                LazyObjects.AFAttributeTemplate attrTemplate = new Lazyobject.AFAttributeTemplate(Connection, template.WebId, template.Id, template.Name, template.Description, template.Path);
                templateList.Add(attrTemplate);
            }

            return templateList;
        }
    }
}