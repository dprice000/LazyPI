using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Serializers;
using RestSharp.Extensions;

namespace PIWebSharp
{
    public class PIRequestClient
    {
        string _serverAddress = "";
        RestClient client;

        public PIRequestClient(string apiServerName)
        {
            _serverAddress = "https://" + apiServerName + "/webapi";
            client = new RestClient(_serverAddress);
        }

        #region "Home"
        /// <summary>
        /// Set of entry points for the webAPI
        /// </summary>
        /// <returns></returns>
        public HomeResponse Home()
        {
            var request = new RestRequest("/home");
            return client.Execute<HomeResponse>(request).Data;
        }
        #endregion

        #region "AssetDatabase"
        /// <summary>
        /// Finds an AFDB by its webID
        /// </summary>
        /// <param name="webID"></param>
        /// <returns></returns>
        public AFDB GetAFDB(string webID)
        {
            var request = new RestRequest("/assetdatabases");
            request.AddParameter("webId", webID);
            return client.Execute<AFDB>(request).Data;
        }

        /// <summary>
        /// Finds an AFDB using a path returned by the WebAPI
        /// </summary>
        /// <param name="path">The path to the AFDB</param>
        /// <returns></returns>
        /// <remarks>Suggested to use webID over path.</remarks>
        public AFDB GetAFDBByPath(string path)
        {
            var request = new RestRequest("/assetdatabases");
            request.AddParameter("path", path);
            return client.Execute<AFDB>(request).Data;
        }

        /// <summary>
        /// Delete AFDB with by specified WebID 
        /// </summary>
        /// <param name="webID"></param>
        public void DeleteAFDB(string webID)
        {
            var request = new RestRequest("/assetdatabases", Method.DELETE);
            request.AddParameter("webId", webID);
            client.Execute(request);
        }

        //TODO: Implement CreateAttributeCategory

        //TODO: Implement CreateElement

        //TODO: Implement CreateElementCategory
        
        //TODO: Implement CreateElementTemplate

        //TODO: Implement CreateEnumerationSet

        //TODO: Implement CreateEventFrame

        //TODO: CreateTable

        //TODO: CreateTableCategory

        public AttributeCategoryList GetAttributeCategories(string afdbWID)
        {
            var request = new RestRequest("/assetdatabases/{webId}/attributecategories");
            request.AddParameter("webId", afdbWID);
            return client.Execute<AttributeCategoryList>(request).Data;
        }

        public ElementCategoryList GetElementCategories(string afdbWID)
        {
            var request = new RestRequest("/assetdatabases/{webId}/elementcategories");
            request.AddParameter("webId", afdbWID);
            return client.Execute<ElementCategoryList>(request).Data;
        }

        /// <summary>
        /// Retrieve elements based on the specified conditions. By default, this method selects immediate children of the current resource.
        /// </summary>
        /// <param name="rootWID">The ID of the resource to use as the root of the search. See WebID for more information.</param>
        /// <param name="nameFilter">The name query string used for finding objects. The default is no filter. See Query String for more information.</param>
        /// <param name="categoryName">Specify that returned elements must have this category. The default is no category filter.</param>
        /// <param name="templateName">Specify that returned elements must have this template or a template derived from this template. The default is no template filter.</param>
        /// <param name="elementType">Specify that returned elements must have this type. The default type is 'Any'. See Element Type for more information.</param>
        /// <param name="searchFullHierarchy">Specifies if the search should include objects nested further than the immediate children of the searchRoot. The default is 'false'.</param>
        /// <param name="sortField">The field or property of the object used to sort the returned collection. The default is 'Name'.</param>
        /// <param name="sortOrder">The field or property of the object used to sort the returned collection. The default is 'Name'.</param>
        /// <param name="startIndex">The starting index (zero based) of the items to be returned. The default is 0.</param>
        /// <param name="maxCount">The maximum number of objects to be returned per call (page size). The default is 1000.</param>
        /// <returns></returns>
        public AFElementList GetAFElements(string rootWID = null, string nameFilter = null, string categoryName = null, string templateName = null, ElementType elementType = ElementType.Any, bool searchFullHierarchy = false, string sortField = "Name", string sortOrder = "Ascending", int startIndex = 0, int maxCount = 1000)
        {
            var request = new RestRequest("/assetdatabases/{webId}/elements");

            if(rootWID != null)
                request.AddParameter("webId", rootWID);
            if (nameFilter != null)
                request.AddParameter("nameFilter", nameFilter);
            if (categoryName != null)
                request.AddParameter("categoryName", categoryName);
            if (templateName != null)
                request.AddParameter("templateName", templateName);

            request.AddParameter("searchFullHierarchy", searchFullHierarchy);
            request.AddParameter("sortField", sortField);
            request.AddParameter("sortOrder", sortOrder);
            request.AddParameter("startIndex", startIndex);
            request.AddParameter("maxCount", maxCount);


            return client.Execute<AFElementList>(request).Data;
        }
        #endregion

        #region "AssetServer"
        /// <summary>
        /// Gets a list of all AFServers
        /// </summary>
        /// <returns>List of AF servers</returns>
        public AFServerList GetAFServerList()
        {
            var request = new RestRequest("/assetservers");
            return client.Execute<AFServerList>(request).Data;
        }

        /// <summary>
        /// Gets a specific AF server by its webID
        /// </summary>
        /// <param name="webID">The unique identifire</param>
        /// <returns></returns>
        public AFServer GetAFServer(string WID)
        {
            var request = new RestRequest("/assetservers");
            request.AddParameter("webId", WID);
            return client.Execute<AFServer>(request).Data;
        }

        /// <summary>
        /// Uses a path from a webAPI response to find a server.
        /// </summary>
        /// <param name="path">Path to the AFServer</param>
        /// <returns></returns>
        /// <remarks>Only to be used with a path received from the server. Suggested to primarily use webID search.</remarks>
        public AFServer GetAFServerByPath(string path)
        {
            var request = new RestRequest("/assetservers");
            request.AddParameter("path", path);
            return client.Execute<AFServer>(request).Data;
        }

        //TODO: Implement CreateAssetDatabase

        //TODO: Implement CreateUnitClass

        /// <summary>
        /// Finds AF server by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AFServer GetAFServerByName(string name)
        {
            var request = new RestRequest("/assetservers");
            request.AddParameter("name", name);
            return client.Execute<AFServer>(request).Data;
        }

        /// <summary>
        /// Finds list AFDBs associated with AF Servers
        /// </summary>
        /// <param name="AFServerWID"></param>
        /// <returns></returns>
        public AFDBList GetAFDBs(string AFServerWID)
        {
            var request = new RestRequest("assetservers/{webId}/assetsdatabases");
            request.AddUrlSegment("webId", AFServerWID);
            return client.Execute<AFDBList>(request).Data;
        }

        /// <summary>
        /// Finds all unit classes defined on an AF server
        /// </summary>
        /// <param name="AFServerWID"></param>
        /// <returns></returns>
        public UnitClassList GetUnitClasses(string AFServerWID)
        {
            var request = new RestRequest("assetservers/{webId}/unitclasses");
            request.AddUrlSegment("webId", AFServerWID);
            return client.Execute<UnitClassList>(request).Data;
        }
        #endregion
    }
}
