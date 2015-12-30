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
        RestClient _client;

        public PIRequestClient(string apiServerName)
        {
            _serverAddress = "https://" + apiServerName + "/webapi";
            _client = new RestClient(_serverAddress);
        }

        #region "Home"
        /// <summary>
        /// Set of entry points for the webAPI
        /// </summary>
        /// <returns></returns>
        public HomeResponse Home()
        {
            var request = new RestRequest("/home");
            return _client.Execute<HomeResponse>(request).Data;
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
            var request = new RestRequest("/assetdatabases/{webId}");
            request.AddUrlSegment("webId", webID);
            return _client.Execute<AFDB>(request).Data;
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
            return _client.Execute<AFDB>(request).Data;
        }

        /// <summary>
        /// Delete AFDB with by specified WebID 
        /// </summary>
        /// <param name="webID"></param>
        public void DeleteAFDB(string webID)
        {
            var request = new RestRequest("/assetdatabases/{webId}", Method.DELETE);
            request.AddUrlSegment("webId", webID);
            _client.Execute(request);
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
            request.AddUrlSegment("webId", afdbWID);
            return _client.Execute<ResponseList<AttributeCategory>>(request).Data;
        }

        public ResponseList<ElementCategory> GetElementCategories(string afdbWID)
        {
            var request = new RestRequest("/assetdatabases/{webId}/elementcategories");
            request.AddUrlSegment("webId", afdbWID);
            return _client.Execute<ResponseList<ElementCategory>>(request).Data;
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

            return _client.Execute<ResponseList<AFElement>>(request).Data;
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

            request.AddUrlSegment("webId", rootWID);

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


            return _client.Execute<ResponseList<AFElement>>(request).Data;
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

            request.AddUrlSegment("webId", afdbWID);

            if(query != null)
                request.AddParameter("query", query);

            request.AddParameter("field", field);
            request.AddParameter("sortField", sortField);
            request.AddParameter("sortOrder", sortOrder);
            request.AddParameter("maxCount", maxCount);

            return _client.Execute<ResponseList<AFElementTemplate>>(request).Data;
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

            return _client.Execute<ResponseList<AFEnumerationSet>>(request).Data;
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

            return _client.Execute<ResponseList<AFEventFrame>>(request).Data;
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

            return _client.Execute<ResponseList<AFTableCategory>>(request).Data;
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

            return _client.Execute<ResponseList<AFTable>>(request).Data;
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
            return _client.Execute<ResponseList<AFServer>>(request).Data;
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
            return _client.Execute<AFServer>(request).Data;
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
            return _client.Execute<AFServer>(request).Data;
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
            return _client.Execute<AFServer>(request).Data;
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
            return _client.Execute<ResponseList<AFDB>>(request).Data;
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
            return _client.Execute<ResponseList<UnitClass>>(request).Data;
        }
        #endregion

        #region "Attribute"
        /// <summary>
        /// Returns the AF Atrribute specified by the WebID.
        /// </summary>
        /// <param name="webID">The unique ID of the AF attribute</param>
        /// <returns></returns>
        public AFAtrribute GetAttributeByID(string webID)
        {
            var request = new RestRequest("/attributes/{webId}");
            request.AddUrlSegment("webId", webID);
            return _client.Execute<AFAtrribute>(request).Data;
        }

        /// <summary>
        /// Gets AFattribute specified by the path.
        /// </summary>
        /// <param name="path">The path provided by the WebAPI.</param>
        /// <returns>A specific AF Attribute.</returns>
        /// <remarks>It is recommended to use Get By ID over path.</remarks>
        public AFAtrribute GetAttributeByPath(string path)
        {
            var request = new RestRequest("/attributes");
            request.AddParameter("path", path);
            return _client.Execute<AFAtrribute>(request).Data;
        }

        /// <summary>
        /// Update an attribute by replacing items in its definition.
        /// </summary>
        /// <param name="attr">A partial attribute that contains the WebID and any properties to be updated.</param>
        /// <returns>Returns true if update completed.</returns>
        public bool UpdateAFAttribute(AFAtrribute attr)
        {
            var request = new RestRequest("/attributes/{webId}", Method.PATCH);
            request.AddUrlSegment("webId", attr.WebID);
            request.AddBody(attr);

            var statusCode  = _client.Execute(request).StatusCode;

            return ((int)statusCode == 204 ? true : false);
        }

        /// <summary>
        /// Delete an attribute.
        /// </summary>
        /// <param name="webID">The WebID of the AFAttribute to be deleted</param>
        /// <returns>Returns true if delete completed.</returns>
        public bool DeleteAFAttribute(string webID)
        {
            var request = new RestRequest("/attributes/{webId}", Method.DELETE);
            request.AddUrlSegment("webId", webID);

            var statusCode = _client.Execute(request).StatusCode;

            return ((int)statusCode == 204 ? true : false);
        }

        /// <summary>
        /// Creates a new AFAttribute in the parent element referenced by parentWID.
        /// </summary>
        /// <param name="parentWID"></param>
        /// <param name="attr">The definition of the new attribute.</param>
        /// <returns>Returns true if create completed.</returns>
        public bool CreateAFAttribute(string parentWID, AFAtrribute attr)
        {
            var request = new RestRequest("/attributes/{webId}", Method.POST);
            request.AddUrlSegment("webId", attr.WebID);
            request.AddBody(attr);

            var statusCode = _client.Execute(request).StatusCode;

            return ((int)statusCode == 201 ? true : false);
        }

        //TODO: Implement GetAttributes

        //TODO: Implement GetCategories

        /// <summary>
        /// Get the attribute's value. This call is intended for use with attributes that have no data reference only. For attributes with a data reference, consult the documentation for Streams.
        /// </summary>
        /// <param name="attrWID">The WebID of the AF Attribute to be read.</param>
        /// <returns></returns>
        public AFValue GetValue(string attrWID)
        {
            var request = new RestRequest("/attributes/{webId}/value");

            request.AddUrlSegment("webId", attrWID);

            return _client.Execute<AFValue>(request).Data;
        }

        /// <summary>
        /// Set the value of a configuration item attribute. For attributes with a data reference or non-configuration item attributes, consult the documentation for streams.
        /// </summary>
        /// <param name="attrWID">The WebID of the AF Attribute to be updated.</param>
        /// <param name="value">The AFValue to be applied to the Attribute.</param>
        /// <returns>Returns true if the value update completes.</returns>
        /// <remarks>Users must be aware of the value type that the attribute takes before changing the value. If a value entered by the user does not match the value type expressed in the attribute, it will not work or it will return an error.</remarks>
        public bool SetValue(string attrWID, AFValue value)
        {
            var request = new RestRequest("/attributes/{webId}/value", Method.PUT);

            request.AddUrlSegment("webId", attrWID);
            request.AddBody(value);

            var statusCode = _client.Execute(request).StatusCode;

            return ((int)statusCode == 204 ? true : false);
        }
        #endregion

        #region "AttributeCategory"

            public AttributeCategory GetAttributeCategory(string webID)
            {
                var request = new RestRequest("/attributecategories/{webId}");
                request.AddUrlSegment("webId", webID);

                return _client.Execute<AttributeCategory>(request).Data;
            }

            public AttributeCategory GetAttributeCategoryByPath(string path)
            {
                var request = new RestRequest("/attributecategories");
                request.AddParameter("path", path);

                return _client.Execute<AttributeCategory>(request).Data;
            }

            public bool UpdateAttributeCategory(AttributeCategory attrCat)
            {
                var request = new RestRequest("/attributecategories/{webId}", Method.PATCH);
                request.AddUrlSegment("webId", attrCat.WebID);
                request.AddBody(attrCat);

                var statusCode = _client.Execute(request).StatusCode;

                return ((int)statusCode == 204 ? true : false);
            }

            public bool DeleteAttributeCategory(AttributeCategory attrCat)
            {
                var request = new RestRequest("/attributecategories/{webId}", Method.DELETE);
                request.AddUrlSegment("webId", attrCat.WebID);

                var statusCode = _client.Execute(attrCat).StatusCode;

                return ((int)statusCode == 204 ? true : false);
            }
        #endregion

        #region "AttributeTemplate"

            public AttributeTemplate GetAttributeTemplate(string webID)
            {
                var request = new RestRequest("/attributetemplates/{webId}");
                request.AddUrlSegment("webId", webId);

                return _client.Execute<AttributeTemplate>(request).Data;
            }

            public AttributeTemplate GetAttributeTemplateByPath(string path)
            {
                var request = new RestRequest("/attributetemplates");
                request.AddParameter("path", path);

                return _client.Execute<AttributeTemplate>(request).Data;
            }

            public bool UpdateAttributeTemplate(AttributeTemplate attrTemp)
            {
                var request = new RestRequest("/attributetemplates/{webId}", Method.PATCH);
                request.AddUrlSegment("webId", webId);
                request.AddBody(attrTemp);

                var statusCode = _client.Execute(request).StatusCode;

                return ((int)statusCode == 204 ? true : false);
            }

            public bool DeleteAttributeTemplate(AttributeTemplate attrTemp)
            {
                var request = new RestRequest("/attributetemplates/{webId}");
                request.AddParameter("webId", webId);

                var statusCode = _client.Execute(request).StatusCode;

                return ((int)statusCode == 204 ? true : false);
            }

            //This really creates a childe attributetemplate
            public bool CreateAttributeTemplate(AttributeTemplate attrTemp)
            {
                var request = new RestRequest("/attributetemplates/{webId}/attributetemplates");
                request.AddUrlSegment("webId", attrTemp.WebID);
                request.AddBody(attrTemp);

                var statusCode = _client.Execute(request).StatusCode;
                return ((int)statusCode == 201 ? true : false);
            }

            public ResponseList<AttributeTemplate> GetChildAttributeTemplates(string webId)
            {
                var request = new RestRequest("/attributetemplates/{webId}/attributetemplates");
                request.AddUrlSegment("webId", webId);

                return _client.Execute<ResponseList<AttributeTemplate>>(request).Data;
            }       
        #endregion

        #region "Calculation"
            public ResponseList<AFValue> GetAtIntervals(string objWID, string expression, DateTime startTime, DateTime endTime, TimeSpan sampleInterval)
            {
                var request = new RestRequest("/calculation/intervals");
                request.AddParameter("webId", objWID);
                request.AddParameter("expression", expression);
                request.AddParameter("startTime", startTime);
                request.AddParameter("endTime", endTime);
                request.AddParameter("sampleInterval", sampleInterval);

                return _client.Execute<ResponseList<AFValue>>(request).Data;
            }

            public ResponseList<AFValue> GetAtRecorded(string objWID, string expression, DateTime startTime, DateTime endTime)
            {
                var request = new RestRequest("/calculation/recorded");
                request.AddParameter("webId", objWID);
                request.AddParameter("expression", expression);
                request.AddParameter("startTime", startTime);
                request.AddParameter("endTime", endTime);

                return _client.Execute<ResponseList<AFValue>>(request).Data;
            }

            public ResponseList<AFValue> GetAtTimes(string objWID, string expression, DateTime time, string sortOrder)
            {
                var request = new RestRequest("/calculation/times");
                request.AddParameter("webId", objWID);
                request.AddParameter("expression", expression);
                request.AddParameter("time", time);
                request.AddParameter("sortOrder", sortOrder);

                return _client.Execute<ResponseList<AFValue>>(request).Data;
            }

            public ResponseList<AFValue> GetSummary(string objWID, string expression, DateTime startTime, DateTime endTime, SummaryType summaryType, CalculationBasis calculationBasis, TimeType timeType, string summaryDuration, SampleType sampleType, TimeSpan sampleInterval)
            {
                var request = new RestRequest("/calculation/summary");
                request.AddParameter("webId", objWID);
                request.AddParameter("expression", expression);
                request.AddParameter("startTime", startTime;
                request.AddParameter("endTime", endTime);
                request.AddParameter("summaryType", summaryType);
                request.AddParameter("calculationBasis");
                request.AddParameter("timeType", timeType);
                request.AddParameter("summaryDuration", summaryDuration);
                request.AddParameter("sampleType", sampleType);
                request.AddParameter("sampleInterval", sampleInterval);

                return _client.Execute<ResponseList<AFValue>>(request).Data;
            }
        #endregion

        #region "Configuration"

            public SystemConfiguration ListConfiguration()
            {
                var request = new RestRequest("/system/configuration"); 

                return _client.Execute<SystemConfiguration>(request).Data;
            }

            public string GetConfigurationItemValue(string key)
            {
                var request = new RestRequest("/system/configuration/{key}");
                request.AddUrlSegment("key", key);

                return _client.Execute<string>(request).Data;
            }

            public bool DeleteConfigurationItem(string key)
            {
                var request = new RestRequest("/system/configuration/{key}");
                request.AddUrlSegment("key", key);

                var statusCode = _client.Execute(request).StatusCode;

                return ((int)statusCode == 202 ? true : false);

            }

            public bool PutConfiguration(string key)
            {
                var request = new RestRequest("/system/configuration/{key}");
                request.AddUrlSegment("key", key);

                var statusCode = _client.Execute(request).StatusCode;

                return ((int)statusCode == 202 ? true : false);
            }
        #endregion

        #region "DataServer"

            public ResponseList<DataServer> GetDataServers()
            {
                var request = new RestRequest("/dataservers");

                return _client.Execute<ResponseList<DataServer>>(request).Data;
            }

            public DataServer GetDataServer(string serverWID)
            {
                var request = new RestRequest("/dataservers/{webId}");
                request.AddUrlSegment("webId", webId);

                return _client.Execute<DataServer>(request);
            }

            public DataServer GetDataServerByPath(string path)
            {
                var request = new RestRequest("/dataservers");
                request.AddParameter("path", path);

                return _client.Execute<DataServer>(request).Data;
            }

            public bool CreateEnumerationSet(stirng dataServerWID, EnumerationSet enumSet)
            {
                var request = new RestRequest("/dataservers/{webId}/enumerationsets", Method.POST);
                request.AddUrlSegment("webId", dataServerWID);
                request.AddBody(enumSet);

                var statusCode = _client.Execute(request).StatusCode;

                return ((int)statusCode == 201 ? true : false);
            }

            public bool CreatePoint(string dataServerWID, DataPoint point)
            {
                var request = new RestRequest("/dataservers/{webId}/points");
                request.AddUrlSegment("webId", dataServerWID);
                request.AddBody(point);

                var statusCode = _client.Execute(request).StatusCode;

                return ((int)statusCode == 201 ? true : false);
            }   

            public DataServer GetDataServerByName(string name)
            {
                var request = new RestRequest("/dataservers");
                request.AddParameter("name", name);

                return _client.Execute<DataServer>(request).Data;
            }

            public ResponseList<EnumerationSet> GetEnumerationSets(string dataServerWID)
            {
                var request = new RestRequest("/dataservers/{webId}/enumerationssets");
                request.AddUrlSegment("webId", dataServerWID);

                return _client.Execute<ResponseList<EnumerationSet>>(request).Data;
            }

            public ResponseList<DataPoint> GetPoints(string dataServerWID, string nameFilter, int startIndex, int maxCount)
            {
                var request = new RestRequest("/dataservers/{webId}/points");
                request.AddUrlSegment("webId", dataServerWID);
                request.AddParameter("nameFilter", nameFilter);
                request.AddParameter("startIndex", startIndex);
                request.AddParameter("maxCount", maxCount);

                return _client.Execute<ResponseList<DataPoint>>(request).Data;

            }            
        #endregion

        #region "Element"
        
        #endregion
    }
}
