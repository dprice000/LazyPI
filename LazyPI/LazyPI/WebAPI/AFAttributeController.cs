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
    public class AFAttributeController : LazyObjects.IAFAttributeController
    {
        /// <summary>
        /// Returns the AF Attribute specified by the WebID.
        /// </summary>
        /// <param name="ID">The unique ID of the AF attribute</param>
        /// <returns></returns>
        public LazyObjects.AFAttribute Find(LazyPI.Common.Connection Connection, string ID)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/attributes/{webId}");
            request.AddUrlSegment("webId", ID);
            var response = webConnection.Client.Execute<ResponseModels.AFAttribute>(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Error finding attribute by ID. (See Inner Details)", response.ErrorException);
            }

            var data = response.Data;
            return new LazyObjects.AFAttribute(Connection, data.WebId, data.Id, data.Name, data.Description, data.Path, data.DefaultUnitsName, data.ConfigString, data.DataReferencePlugIn, data.Type, data.CategoryNames);
        }

        /// <summary>
        /// Gets AFattribute specified by the path.
        /// </summary>
        /// <param name="Path">The path provided by the WebAPI.</param>
        /// <returns>A specific AF Attribute.</returns>
        /// <remarks>It is recommended to use Get By ID over path.</remarks>
        public LazyObjects.AFAttribute FindByPath(LazyPI.Common.Connection Connection,  string Path)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/attributes");
            request.AddParameter("path", Path, ParameterType.GetOrPost);
            var response = webConnection.Client.Execute<ResponseModels.AFAttribute>(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Error finding attribute by path. (See Inner Details)", response.ErrorException);
            }
            
            var data = response.Data;
            return new LazyObjects.AFAttribute(Connection, data.WebId, data.Id, data.Name, data.Description, data.Path, data.DefaultUnitsName, data.ConfigString, data.DataReferencePlugIn, data.Type, data.CategoryNames);
        }

        /// <summary>
        /// Update an attribute by replacing items in its definition.
        /// </summary>
        /// <param name="Attr">A partial attribute that contains the WebID and any properties to be updated.</param>
        /// <returns>Returns true if update completed.</returns>
        public bool Update(LazyPI.Common.Connection Connection, LazyObjects.AFAttribute Attr)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/attributes/{webId}", Method.PATCH);
            request.AddUrlSegment("webId", Attr.WebID);

            ResponseModels.AFAttribute body = DataConversions.Convert(Attr);
            request.AddParameter("application/json; charset=utf-8", Newtonsoft.Json.JsonConvert.SerializeObject(body), ParameterType.RequestBody);

            var statusCode = webConnection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 204);
        }

        /// <summary>
        /// Delete an attribute.
        /// </summary>
        /// <param name="webID">The WebID of the AFAttribute to be deleted</param>
        /// <returns>Returns true if delete completed.</returns>
        public bool Delete(LazyPI.Common.Connection Connection,  string ID)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/attributes/{webId}", Method.DELETE);
            request.AddUrlSegment("webId", ID);

            var statusCode = webConnection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 204);
        }

        /// <summary>
        /// Creates a new AFAttribute in the parent element referenced by parentWID.
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="Attr">The definition of the new attribute.</param>
        /// <returns>Returns true if create completed.</returns>
        public bool Create(LazyPI.Common.Connection Connection, string ParentID, LazyObjects.AFAttribute Attr)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/attributes/{webId}", Method.POST);
            request.AddUrlSegment("webId", ParentID);

            ResponseModels.AFAttribute body = DataConversions.Convert(Attr);
            request.AddParameter("application/json; charset=utf-8", Newtonsoft.Json.JsonConvert.SerializeObject(body), ParameterType.RequestBody);

            var statusCode = webConnection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 201);
        }

        public bool CreateChild(LazyPI.Common.Connection Connection, string AttrID, LazyObjects.AFAttribute Attr)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/attributes/{webId}", Method.POST);
            request.AddUrlSegment("webId", AttrID);

            ResponseModels.AFAttribute body = DataConversions.Convert(Attr);
            request.AddParameter("application/json; charset=utf-8", Newtonsoft.Json.JsonConvert.SerializeObject(body), ParameterType.RequestBody);

            var statusCode = webConnection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 201);
        }

        //TODO: Implement GetAttributes

        //TODO: Implement GetCategories

        /// <summary>
        /// Assumes that the attribute is of type PIPoint and returns the PIPoint.
        /// </summary>
        /// <param name="Connection"></param>
        /// <param name="AttrID"></param>
        /// <returns></returns>
        /// <remarks>This is kind of hacky...</remarks>
        public LazyObjects.PIPoint GetPoint(LazyPI.Common.Connection Connection, string AttrID)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/attributes/{webId}");
            request.AddUrlSegment("webId", AttrID);
            var attrResponse = webConnection.Client.Execute<ResponseModels.AFAttribute>(request);

            string pointLink = attrResponse.Data.Links["Point"];
            int pos = pointLink.LastIndexOf("/") + 1;
            string webID = pointLink.Substring(pos, pointLink.Length - pos);

            request = new RestRequest("/points/{webId}");
            request.AddUrlSegment("webId", webID);
            var pointResponse = webConnection.Client.Execute<ResponseModels.DataPoint>(request);
            ResponseModels.DataPoint point = pointResponse.Data;

            LazyObjects.PIPoint result = new LazyObjects.PIPoint(Connection, point.WebId, point.Id, point.Name, point.Description, point.Path, point.PointType, point.PointClass, point.Future);
            return result;  
        }

        /// <summary>
        /// Get the attribute's value. This call is intended for use with attributes that have no data reference only. For attributes with a data reference, consult the documentation for Streams.
        /// </summary>
        /// <param name="attrWID">The WebID of the AF Attribute to be read.</param>
        /// <returns></returns>
        public LazyObjects.AFValue GetValue(LazyPI.Common.Connection Connection, string AttrID)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/attributes/{webId}/value");
            request.AddUrlSegment("webId", AttrID);
            var response = webConnection.Client.Execute<ResponseModels.AFValue>(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Error retrieving value from attribute. (See Inner Details)", response.ErrorException);
            }

            ResponseModels.AFValue data = response.Data;

            return new LazyObjects.AFValue(data.Timestamp, data.Value, data.UnitsAbbreviation, data.Good, data.Questionable, data.Substituted);
        }

        /// <summary>
        /// Set the value of a configuration item attribute. For attributes with a data reference or non-configuration item attributes, consult the documentation for streams.
        /// </summary>
        /// <param name="AttrID">The WebID of the AF Attribute to be updated.</param>
        /// <param name="Value">The AFValue to be applied to the Attribute.</param>
        /// <returns>Returns true if the value update completes.</returns>
        /// <remarks>Users must be aware of the value type that the attribute takes before changing the value. If a value entered by the user does not match the value type expressed in the attribute, it will not work or it will return an error.</remarks>
        public bool SetValue(LazyPI.Common.Connection Connection, string AttrID, LazyObjects.AFValue Value)
        {
            WebAPIConnection webConnection = (WebAPIConnection)Connection;
            var request = new RestRequest("/attributes/{webId}/value", Method.PUT);

            request.AddUrlSegment("webId", AttrID);
            request.AddBody(Value);

            var statusCode = webConnection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 204);
        }
    }
}
