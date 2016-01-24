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
    class AFAttributeLoader : LazyObjects.IAFAttribute
    {
        public AFAttributeLoader()
        {
        }

        /// <summary>
        /// Returns the AF Attribute specified by the WebID.
        /// </summary>
        /// <param name="webID">The unique ID of the AF attribute</param>
        /// <returns></returns>
        public BaseObject Find(WebAPIConnection Connection, string ID)
        {
            var request = new RestRequest("/attributes/{webId}");
            request.AddUrlSegment("webId", ID);
            var attr = Connection.Client.Execute<ResponseModels.AFAttribute>(request).Data;
        }

        /// <summary>
        /// Gets AFattribute specified by the path.
        /// </summary>
        /// <param name="path">The path provided by the WebAPI.</param>
        /// <returns>A specific AF Attribute.</returns>
        /// <remarks>It is recommended to use Get By ID over path.</remarks>
        public BaseObject FindByPath(WebAPIConnection Connection,  string path)
        {
            var request = new RestRequest("/attributes");
            request.AddParameter("path", path);
            var Attr = Connection.Client.Execute<ResponseModels.AFAttribute>(request).Data;
        }

        /// <summary>
        /// Update an attribute by replacing items in its definition.
        /// </summary>
        /// <param name="attr">A partial attribute that contains the WebID and any properties to be updated.</param>
        /// <returns>Returns true if update completed.</returns>
        public bool Update(WebAPIConnection Connection, LazyObjects.AFAttribute attr)
        {
            var request = new RestRequest("/attributes/{webId}", Method.PATCH);
            request.AddUrlSegment("webId", attr.ID);
            request.AddBody(attr);

            var statusCode = Connection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 204);
        }

        /// <summary>
        /// Delete an attribute.
        /// </summary>
        /// <param name="webID">The WebID of the AFAttribute to be deleted</param>
        /// <returns>Returns true if delete completed.</returns>
        public bool Delete(WebAPIConnection Connection,  string ID)
        {
            var request = new RestRequest("/attributes/{webId}", Method.DELETE);
            request.AddUrlSegment("webId", ID);

            var statusCode = Connection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 204);
        }

        /// <summary>
        /// Creates a new AFAttribute in the parent element referenced by parentWID.
        /// </summary>
        /// <param name="parentWID"></param>
        /// <param name="attr">The definition of the new attribute.</param>
        /// <returns>Returns true if create completed.</returns>
        public bool Create(string parentWID, LazyObjects.AFAttribute attr)
        {
            var request = new RestRequest("/attributes/{webId}", Method.POST);
            request.AddUrlSegment("webId", parentWID);

            //Copy to api object
            ResponseModels.AFAttribute clientAttr = new ResponseModels.AFAttribute();
            clientAttr.Name = attr.Name;
            clientAttr.Description = attr.Description;

            request.AddBody(clientAttr);

            var statusCode = _client.Execute(request).StatusCode;

            return ((int)statusCode == 201);
        }

        //TODO: Implement GetAttributes

        //TODO: Implement GetCategories

        /// <summary>
        /// Get the attribute's value. This call is intended for use with attributes that have no data reference only. For attributes with a data reference, consult the documentation for Streams.
        /// </summary>
        /// <param name="attrWID">The WebID of the AF Attribute to be read.</param>
        /// <returns></returns>
        public LazyObjects.AFValue GetValue(WebAPIConnection Connection, string attrID)
        {
            var request = new RestRequest("/attributes/{webId}/value");

            request.AddUrlSegment("webId", attrID);

            var response = Connection.Client.Execute<ResponseModels.AFValue>(request).Data;
        }

        /// <summary>
        /// Set the value of a configuration item attribute. For attributes with a data reference or non-configuration item attributes, consult the documentation for streams.
        /// </summary>
        /// <param name="attrWID">The WebID of the AF Attribute to be updated.</param>
        /// <param name="value">The AFValue to be applied to the Attribute.</param>
        /// <returns>Returns true if the value update completes.</returns>
        /// <remarks>Users must be aware of the value type that the attribute takes before changing the value. If a value entered by the user does not match the value type expressed in the attribute, it will not work or it will return an error.</remarks>
        public bool SetValue(WebAPIConnection Connection, string attrWID, LazyObjects.AFValue value)
        {
            var request = new RestRequest("/attributes/{webId}/value", Method.PUT);

            request.AddUrlSegment("webId", attrWID);
            request.AddBody(value);

            var statusCode = Connection.Client.Execute(request).StatusCode;

            return ((int)statusCode == 204);
        }
    }
}
