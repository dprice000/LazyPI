using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.WebAPI.ResponseModels
{
    public class LinksResponse
    {
        public string Self { get; set; }
    }

    /// <summary>
    /// A Base Object that all elements are derived from.
    /// </summary>
    [Serializable]
    public abstract class BaseResponse
    {
        public string WebID { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public LinksResponse Links { get; set; }
    }

    [Serializable]
    public class SystemConfiguration
    {
        public List<string> AuthenticationMethods { get; set; }
        public string CorsHeaders { get; set; }
        public string CorsMethods { get; set; }
        public string CorsOrigins { get; set; }
        public bool CorsSupportsCredentials { get; set; }
        public DateTime Timestamp { get; set; }
        public bool VerifyExtensionAssemblySignature { get; set; }
    }

    [Serializable]
    public class HomeResponse
    {
        public string Self { get; set; }
        public string AssetServers { get; set; }
        public string DataServers { get; set; }
        public string System { get; set; }
    }

    [Serializable]
    public class DataServer : BaseResponse
    {
        public bool IsConnected { get; set; }
        public string ServerVersion { get; set; }
    }

    [Serializable]
    public class AFServer : BaseResponse
    {
        public bool IsConnected { get; set; }
        public string ServerVersion { get; set; }
    }

    [Serializable]
    public class AFDB : BaseResponse
    {
    }

    [Serializable]
    public class UnitClass : BaseResponse
    {
        public string CanonicalUnitName { get; set; }
        public string CanonicalUnitAbbreviation { get; set; }
    }

    [Serializable]
    public class AttributeCategory : BaseResponse
    {
    }

    [Serializable]
    public class ElementCategory : BaseResponse
    {
    }

    [Serializable]
    public class AFElement : BaseResponse
    {
        public string TemplateName { get; set; }
        public List<string> CategoryNames { get; set; }
    }

    [Serializable]
    public class AFAttributeTemplate : BaseResponse
    {
        public string Type { get; set; }
        public string TypeQualifier { get; set; }
        public string DefaultUnitsName { get; set; }
        public string DataReferencePlugIn { get; set; }
        public string ConfigString { get; set; }
        public bool IsConfigurationItem { get; set; }
        public bool IsExcluded { get; set; }
        public bool IsHidden { get; set; }
        public List<string> CategoryNames { get; set; }
    }

    [Serializable]
    public class AFAttribute : BaseResponse
    {
        public string Type { get; set; }
        public string TypeQualifier { get; set; }
        public string DefaultUnitsName { get; set; }
        public string DataReferencePlugIn { get; set; }
        public string ConfigString { get; set; }
        public bool IsConfigurationItem { get; set; }
        public bool IsExcluded { get; set; }
        public bool IsHidden { get; set; }
        public List<string> CategoryNames { get; set; }
        public bool Step { get; set; }
    }

    [Serializable]
    public class AFValue
    {
        public DateTime Timestamp { get; set; }
        public string Value { get; set; }
        public string UnitsAbbreviation { get; set; }
        public bool Good { get; set; }
        public bool Questionable { get; set; }
        public bool Substituted { get; set; }
    }

    [Serializable]
    public class AFElementTemplate : BaseResponse
    {
        public bool AllowElementToExtend { get; set; }
        public List<string> CategoryNames { get; set; }
    }

    [Serializable]
    public class AFEventFrame : BaseResponse
    {
        public string TemplateName { get; set; }
        public List<string> CategoryNames { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool AreValuesCaptured { get; set; }
    }

    [Serializable]
    public class AFEnumerationSet : BaseResponse
    {
    }

    [Serializable]
    public class AFTable : BaseResponse
    {
        public List<string> CategoryNames { get; set; }
    }

    [Serializable]
    public class AFTableCategory : BaseResponse
    {
    }

    [Serializable]
    public class DataPoint : BaseResponse
    {
        public string PointClass { get; set; }
        public string PointType { get; set; }
        public bool Future { get; set; }
    }

    /// <summary>
    /// Used to handle any response that includes a list of items
    /// </summary>
    /// <typeparam name="T">The type to be held in the Items list</typeparam>
    [Serializable]
    public class ResponseList<T>
    {
        public LinksResponse Links { get; set; }
        public List<T> Items { get; set; }
    }
}
