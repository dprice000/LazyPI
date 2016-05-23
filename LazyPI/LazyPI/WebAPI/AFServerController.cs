using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.WebAPI
{
    public class AFServerController : RestRequester<ResponseModels.AFServer>, LazyObjects.IAFServerController
    {
        public LazyPI.LazyObjects.AFServer Find(LazyPI.Common.Connection Connection, string ID)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var endpoint = "/assetservers/{webId}";
            ResponseModels.AFServer data = base.Read(webConnection, endpoint, ID);

            return new LazyObjects.AFServer(Connection, data.WebId, data.Id, data.Name, data.Description, data.IsConnected, data.ServerVersion, data.Path);
        }

        public LazyObjects.AFServer FindByPath(LazyPI.Common.Connection Connection, string Path)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var endpoint = "/assetservers";
            ResponseModels.AFServer data = base.ReadByPath(webConnection, endpoint, Path);

            return new LazyObjects.AFServer(Connection, data.WebId, data.Id, data.Name, data.Description, data.IsConnected, data.ServerVersion, data.Path);
        }

        public LazyObjects.AFServer FindByName(LazyPI.Common.Connection Connection, string Name)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/assetservers");
            request.AddParameter("name", Name, ParameterType.GetOrPost);
            var response = webConnection.Client.Execute<ResponseModels.AFServer>(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Error finding attribute by name. (See Inner Details)", response.ErrorException);
            }

            var data = response.Data;
            return new LazyObjects.AFServer(Connection, data.WebId, data.Id, data.Name, data.Description, data.IsConnected, data.ServerVersion, data.Path);
        }

        public IEnumerable<LazyObjects.AFServer> List(LazyPI.Common.Connection Connection)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/assetservers");
            var response = webConnection.Client.Execute<ResponseModels.ResponseList<ResponseModels.AFServer>>(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Error finding database by ID. (See Inner Details)", response.ErrorException);
            }

            List<LazyObjects.AFServer> results = new List<LazyObjects.AFServer>();

            foreach(var server in response.Data.Items)
            {
                results.Add(new LazyObjects.AFServer(Connection, server.WebId, server.Id, server.Name, server.Description, server.IsConnected, server.ServerVersion, server.Path));
            }

            return results;
        }

        public IEnumerable<LazyPI.LazyObjects.AFDatabase> GetDatabases(LazyPI.Common.Connection Connection, string AFServerID)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/assetservers/{webId}/assetdatabases");
            request.AddUrlSegment("webId", AFServerID);

            var response = webConnection.Client.Execute<ResponseModels.ResponseList<ResponseModels.AFDB>>(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Error searching for servers. (See Inner Details)", response.ErrorException);
            }

            List<LazyObjects.AFDatabase> results = new List<LazyObjects.AFDatabase>();

            foreach (var element in response.Data.Items)
            {
                results.Add(new LazyObjects.AFDatabase(Connection, element.WebId, element.Id, element.Name, element.Description, element.Path));
            }

            return results;
        }

        public IEnumerable<LazyPI.LazyObjects.AFUnit> GetUnitClasses(LazyPI.Common.Connection Connection, string AFServerID)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/assetservers/{webId}/unitclasses");
            request.AddUrlSegment("webId", AFServerID);

            var response = webConnection.Client.Execute<ResponseModels.ResponseList<ResponseModels.UnitClass>>(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Error searching for servers. (See Inner Details)", response.ErrorException);
            }

            List<LazyObjects.AFUnit> results = new List<LazyObjects.AFUnit>();

            foreach (var element in response.Data.Items)
            {
                results.Add(new LazyObjects.AFUnit(Connection, element.WebId, element.Id, element.Name, element.Description, element.Path));
            }

            return results;
        }

        public bool CreateAssetDatabase(LazyPI.Common.Connection Connection, string AFServerID, LazyObjects.AFDatabase AFDB)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/assetservers/{webId}/assetdatabases", Method.POST);
            request.AddUrlSegment("webId", AFServerID);
            ResponseModels.AFDB body = DataConversions.Convert(AFDB);
            request.AddParameter("application/json; charset=utf-8", Newtonsoft.Json.JsonConvert.SerializeObject(body), ParameterType.RequestBody);

            var statusCode = webConnection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 201);
        }

        public bool CreateUnitClass(LazyPI.Common.Connection Connection, string AFServerID, LazyObjects.AFUnit UnitClass)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/assetservers/{webId}/unitclasses", Method.POST);
            request.AddUrlSegment("webId", AFServerID);
            ResponseModels.UnitClass body = DataConversions.Convert(UnitClass);
            request.AddParameter("application/json; charset=utf-8", Newtonsoft.Json.JsonConvert.SerializeObject(body), ParameterType.RequestBody);

            var statusCode = webConnection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 201);
        }
    }
}
