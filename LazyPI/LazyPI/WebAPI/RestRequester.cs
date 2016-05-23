using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazyPI.WebAPI.ResponseModels;

namespace LazyPI.WebAPI
{
    /// <summary>
    /// A class 
    /// </summary>
    public abstract class RestRequester<T> where T : new()
    {
        //public bool Create(WebAPIConnection Connection, string EndPoint, string ID) 
        //{
        //    var request = new RestRequest("/attributes/{webId}", Method.POST);
        //    request.AddUrlSegment("webId", ID);

        //    ResponseModels.AFAttribute body = DataConversions.Convert(Attr);
        //    request.AddParameter("application/json; charset=utf-8", Newtonsoft.Json.JsonConvert.SerializeObject(body), ParameterType.RequestBody);

        //    var statusCode = webConnection.Client.Execute(request).StatusCode;

        //    return ((int)statusCode == 201);
        //}

        public T Read(WebAPIConnection Connection,string EndPoint, string ID)
        {
            var request = new RestRequest(EndPoint);
            request.AddUrlSegment("webId", ID);
            var response = Connection.Client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Error finding attribute by ID. (See Inner Details)", response.ErrorException);
            }

            return response.Data;
        }

        public T ReadByPath(WebAPIConnection Connection, string EndPoint, string Path)
        {
            var request = new RestRequest(EndPoint);
            request.AddParameter("path", Path, ParameterType.GetOrPost);
            var response = Connection.Client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Error finding attribute by path. (See Inner Details)", response.ErrorException);
            }

            return response.Data;
        }

        public bool Update(WebAPIConnection Connection, string Endpoint, T AFObject)
        {
            var request = new RestRequest(Endpoint, Method.PATCH);
            request.AddUrlSegment("webId", AFObject.ID);


            request.AddParameter("application/json; charset=utf-8", Newtonsoft.Json.JsonConvert.SerializeObject(AFObject), ParameterType.RequestBody);

            var statusCode = Connection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 204);
        }

        public bool Delete(WebAPIConnection Connection, string Endpoint, string ID)
        {
            var request = new RestRequest(Endpoint, Method.DELETE);
            request.AddUrlSegment("webId", ID);

            var statusCode = Connection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 204);
        }
    }
}
