using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace LazyPI.WebAPI
{
    public class AFDatabaseController : LazyPI.LazyObjects.IAFDatabaseController
    {
        LazyPI.LazyObjects.AFDatabase Find(LazyPI.Common.Connection Connection, string ID)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/assetdatabases/{webId}");
            request.AddUrlSegment("webId", ID);
            var response = webConnection.Client.Execute<ResponseModels.AFAttribute>(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Error finding database by ID. (See Inner Details)", response.ErrorException);
            }

            var data = response.Data;
            return new LazyObjects.AFDatabase(Connection, data.WebID, data.Name, data.Description, data.Path);
        }

        public LazyObjects.AFAttribute FindByPath(LazyPI.Common.Connection Connection, string Path)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/assetdatabases");
            request.AddParameter("path", Path);
            var response = webConnection.Client.Execute<ResponseModels.AFAttribute>(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Error finding attribute by path. (See Inner Details)", response.ErrorException);
            }

            var data = response.Data;
            return new LazyObjects.AFAttribute(Connection, data.WebID, data.Name, data.Description, data.Path);
        }

        public bool Update(LazyPI.Common.Connection Connection, LazyPI.LazyObjects.AFDatabase AFDB)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/attributes/{webId}", Method.PATCH);
            request.AddUrlSegment("webId", AFDB.ID);
            request.AddBody(AFDB);

            var statusCode = webConnection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 204);
        }

        public bool Delete(LazyPI.Common.Connection Connection, string DatabaseID)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/attributes/{webId}", Method.DELETE);
            request.AddUrlSegment("webId", DatabaseID);

            var statusCode = webConnection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 204);
        }

        public bool CreateElement(LazyPI.Common.Connection Connection, LazyPI.LazyObjects.AFElement Element)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/assetdatabases/{webId}/elements", Method.POST);
            request.AddUrlSegment("webId", Element.ID);
            request.AddBody(Element);

            var statusCode = webConnection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 204);
        }

        public bool CreateEventFrame(LazyPI.Common.Connection Connection, string DatabaseID, LazyPI.LazyObjects.AFElement EventFrame)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/assetdatabases/{webId}/eventframes", Method.POST);
            request.AddUrlSegment("webId", DatabaseID);
            request.AddBody(EventFrame);

            var statusCode = webConnection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 204);
        }

        public IEnumerable<LazyPI.LazyObjects.AFElement> GetElements(LazyPI.Common.Connection Connection, string DatabaseID)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/assetdatabases/{webId}/elements", Method.POST);
            request.AddUrlSegment("webId", DatabaseID);

            var response = webConnection.Client.Execute<ResponseModels.ResponseList<ResponseModels.AFElement>>(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Error searching for elements. (See Inner Details)", response.ErrorException);
            }

            List<LazyObjects.AFElement> results = new List<LazyObjects.AFElement>();

            foreach (var element in response.Data.Items)
            {
                results.Add(new LazyObjects.AFElement(Connection, element.ID, element.Name, element.Description, element.Path));
            }

            return results;
        }

        public IEnumerable<LazyPI.LazyObjects.AFEventFrame> GetEventFrames(LazyPI.Common.Connection Connection, string DatabaseID)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/assetdatabases/{webId}/eventframes", Method.POST);
            request.AddUrlSegment("webId", DatabaseID);

            var response = webConnection.Client.Execute<ResponseModels.ResponseList<ResponseModels.AFEventFrame>>(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Error searching for eventframes. (See Inner Details)", response.ErrorException);
            }

            List<LazyObjects.AFEventFrame> results = new List<LazyObjects.AFEventFrame>();

            foreach (var eventframe in response.Data.Items)
            {
                results.Add(new LazyObjects.AFEventFrame(Connection, eventframe.ID, eventframe.Name, eventframe.Description, eventframe.Path));
            }

            return results;
        }
    }
}
