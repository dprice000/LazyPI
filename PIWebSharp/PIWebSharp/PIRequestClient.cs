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
        /// Finds an AFDB by its webID.
        /// </summary>
        /// <param name="webID">The unique ID for the AFDB.</param>
        /// <returns>A single AFDB object.</returns>
        public AFDB GetAFDB(string webID)
        {
            var request = new RestRequest("/assetdatabases");
            request.AddParameter("webId", webID);
            return client.Execute<AFDB>(request).Data;
        }

        /// <summary>
        /// Finds an AFDB using a path returned by the WebAPI.
        /// </summary>
        /// <param name="path">The path to the AFDB.</param>
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

        public ResponseList<AttributeCategory> GetAttributeCategories(string afdbWID)
        {
            var request = new RestRequest("/assetdatabases/{webId}/attributecategories");
            request.AddParameter("webId", afdbWID);
            return client.Execute<ResponseList<AttributeCategory>>(request).Data;
        }

        public ResponseList<ElementCategory> GetElementCategories(string afdbWID)
        {
            var request = new RestRequest("/assetdatabases/{webId}/elementcategories");
            request.AddParameter("webId", afdbWID);
            return client.Execute<ResponseList<ElementCategory>>(request).Data;
        }

        /// <summary>
        /// Retrieve elements based on the specified conditions. By default, this method selects immediate children of the current resource.
        /// </summary>
        /// <param name="rootWID">The ID of the resource to use as the root of the search. See WebID for more information.</param>
        /// <param name="nameFilter">The name query string used for finding objects. The default is no filter. See Query String for more information.</param>
        /// <param name="templateName">Specify that returned elements must have this template or a template derived from this template. The default is no template filter.</param>
        /// <returns></returns>
        public ResponseList<AFElement> GetAFElements(string rootWID, string nameFilter, string templateName = null)
        {
            var request = new RestRequest("/assetdatabases/{webId}/elements");

            request.AddUrlSegment("webId", rootWID);
            request.AddParameter("nameFilter", nameFilter);
            request.AddParameter("templateName", templateName);

            return client.Execute<ResponseList<AFElement>>(request).Data;
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
        public ResponseList<AFElement> GetAFElements(string rootWID, string nameFilter = null, string categoryName = null, string templateName = null, ElementType elementType = ElementType.Any, bool searchFullHierarchy = false, string sortField = "Name", string sortOrder = "Ascending", int startIndex = 0, int maxCount = 1000)
        {
            var request = new RestRequest("/assetdatabases/{webId}/elements");

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


            return client.Execute<ResponseList<AFElement>>(request).Data;
        }

        /// <summary>
        /// Retrieve element templates based on the specified criteria. By default, all element templates in the specified Asset Database are returned.
        /// </summary>
        /// <param name="afdbWID">The ID of the database to search. See WebID for more information.</param>
        /// <param name="query">The query string used for finding objects. The default is no query string. See Query String for more information.</param>
        /// <param name="field">Specifies which of the object's properties are searched. The default is 'Name'.</param>
        /// <param name="sortField">The field or property of the object used to sort the returned collection. The default is 'Name'.</param>
        /// <param name="sortOrder">The order that the returned collection is sorted. The default is 'Ascending'.</param>
        /// <param name="maxCount">The maximum number of objects to be returned per call (page size). The default is 1000.</param>
        /// <returns></returns>
        public ResponseList<AFElementTemplate> GetAFElementTemplates(string afdbWID, string query = null, string field = "Name", string sortField = "Name", string sortOrder = "Ascending", int maxCount = 1000)
        {
            var request = new RestRequest("/assetdatabases/{webId}/elementtemplates");

            request.AddParameter("webId", afdbWID);

            if(query != null)
                request.AddParameter("query", query);

            request.AddParameter("field", field);
            request.AddParameter("sortField", sortField);
            request.AddParameter("sortOrder", sortOrder);
            request.AddParameter("maxCount", maxCount);

            return client.Execute<ResponseList<AFElementTemplate>>(request).Data;
        }

        /// <summary>
        /// Retrieve enumeration sets for given asset database.
        /// </summary>
        /// <param name="webID">The ID of the AF database.See WebID for more information.</param>
        /// <returns></returns>
        public ResponseList<AFEnumerationSet> GetEnumerationSets(string webID)
        {
            var request = new RestRequest("/assetdatabases/{webId}/enumerationsets");

            request.AddUrlSegment("webId", webID);

            return client.Execute<ResponseList<AFEnumerationSet>>(request).Data;
        }

        /// <summary>
        /// Retrieve event frames based on the specified conditions. 
        /// By default, returns all children of the specified root resource with a start time in the past 8 hours.
        /// </summary>
        /// <param name="rootID">The ID of the resource to use as the root of the search. See WebID for more information.</param>
        /// <param name="searchMode">Determines how the startTime and endTime parameters are treated when searching for event frame objects to be included in the returned collection. If this parameter is one of the 'Backward*' or 'Forward*' values, none of endTime, sortField, or sortOrder may be specified. The default is 'Overlapped'. See Search Mode for more information.</param>
        /// <param name="startTime">The starting time for the search. startTime must be less than or equal to the endTime. The searchMode parameter will control whether the comparison will be performed against the event frame's startTime or endTime. The default is '*-8h'. See Time Strings for more information.</param>
        /// <param name="endTime">The ending time for the search. The endTime must be greater than or equal to the startTime. The searchMode parameter will control whether the comparison will be performed against the event frame's startTime or endTime. The default is '*' if searchMode is not one of the 'Backward*' or 'Forward*' values. See Time Strings for more information.</param>
        /// <param name="nameFilter">The name query string used for finding event frames. The default is no filter. See Query String for more information.</param>
        /// <param name="referencedElementName">The name query string which must match the name of a referenced element. The default is no filter. See Query String for more information.</param>
        /// <param name="categoryName">Specify that returned event frames must have this category. The default is no category filter.</param>
        /// <param name="templateName">Specify that returned event frames must have this template or a template derived from this template. The default is no template filter. Specify this parameter by name.</param>
        /// <param name="referencedElementTemplateName">Specify that returned event frames must have an element in the event frame's referenced elements collection that derives from the template. Specify this parameter by name.</param>
        /// <param name="searchFullHierarchy">Specifies whether the search should include objects nested further than the immediate children of the search root. The default is 'false'.</param>
        /// <param name="sortField">The field or property of the object used to sort the returned collection. The default is 'Name' if searchMode is not one of the 'Backward*' or 'Forward*' values.</param>
        /// <param name="sortOrder">The order that the returned collection is sorted. The default is 'Ascending' if searchMode is not one of the 'Backward*' or 'Forward*' values.</param>
        /// <param name="startIndex">The starting index (zero based) of the items to be returned. The default is 0.</param>
        /// <param name="maxCount">The maximum number of objects to be returned per call (page size). The default is 1000.</param>
        /// <returns></returns>
        public ResponseList<AFEventFrame> GetEventFrames(string rootID, SearchMode searchMode, string startTime = "*-8h", string endTime = "*", string nameFilter = null, string referencedElementName = null, string categoryName = null, string templateName = null, string referencedElementTemplateName = null, bool searchFullHierarchy = false, string sortField = "Name", string sortOrder = "Ascending", int startIndex = 0, int maxCount = 1000)
        {
            var request = new RestRequest("/assetdatabases/{webId}/eventframes");

            request.AddUrlSegment("webId", rootID);

            //TODO: Implement Search Mode to string
            request.AddParameter("searchMode", searchMode);
            request.AddParameter("starttime", startTime);
            request.AddParameter("endtime", endTime);

            if(nameFilter != null)
                request.AddParameter("nameFilter", nameFilter);

            if(referencedElementName != null)
                request.AddParameter("referencedElementNameFilter", referencedElementName);

            if (categoryName != null)
                request.AddParameter("categoryName", categoryName);

            if (templateName != null)
                request.AddParameter("templateName", templateName);

            if (referencedElementTemplateName != null)
                request.AddParameter("referencedElementTemplateName", referencedElementTemplateName);

            request.AddParameter("searchFullHierarchy", searchFullHierarchy);
            request.AddParameter("sortOrder", sortOrder);
            request.AddParameter("sortField", sortField);
            request.AddParameter("startIndex", startIndex);
            request.AddParameter("maxCount", maxCount);

            return client.Execute<ResponseList<AFEventFrame>>(request).Data;
        }

        /// <summary>
        /// Retrieve table categories for a given Asset Database.
        /// </summary>
        /// <param name="afdbID">The ID of the database.</param>
        /// <returns>List of AF Table Categories.</returns>
        public ResponseList<AFTableCategory> GetTableCategories(string afdbID)
        {
            var request = new RestRequest("/assetdatabases/{webId}/tablecategories");
            request.AddUrlSegment("webId", afdbID);

            return client.Execute<ResponseList<AFTableCategory>>(request).Data;
        }

        /// <summary>
        /// Retrieve tables for given Asset Database.
        /// </summary>
        /// <param name="afdbID">The ID of the database.</param>
        /// <returns>List of AF Tables.</returns>
        public ResponseList<AFTable> GetTables(string afdbID)
        {
            var request = new RestRequest("/assetdatabases/{webId}/tables");
            request.AddUrlSegment("webId", afdbID);

            return client.Execute<ResponseList<AFTable>>(request).Data;
        }
        #endregion

        #region "AssetServer"
        /// <summary>
        /// Gets a list of all AFServers
        /// </summary>
        /// <returns>List of AF servers</returns>
        public ResponseList<AFServer> GetAFServerList()
        {
            var request = new RestRequest("/assetservers");
            return client.Execute<ResponseList<AFServer>>(request).Data;
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
        public ResponseList<AFDB> GetAFDBs(string AFServerWID)
        {
            var request = new RestRequest("assetservers/{webId}/assetsdatabases");
            request.AddUrlSegment("webId", AFServerWID);
            return client.Execute<ResponseList<AFDB>>(request).Data;
        }

        /// <summary>
        /// Finds all unit classes defined on an AF server
        /// </summary>
        /// <param name="AFServerWID"></param>
        /// <returns></returns>
        public ResponseList<UnitClass> GetUnitClasses(string AFServerWID)
        {
            var request = new RestRequest("assetservers/{webId}/unitclasses");
            request.AddUrlSegment("webId", AFServerWID);
            return client.Execute<ResponseList<UnitClass>>(request).Data;
        }
        #endregion
    }
}
