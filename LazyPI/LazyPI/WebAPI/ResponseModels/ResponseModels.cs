using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazyPI.LazyObjects;

namespace LazyPI.WebAPI.ResponseModels
{
    /// <summary>
    /// A Base Object that all elements are derived from.
    /// </summary>
    [Serializable]
    public abstract class BaseResponse
    {
        public BaseResponse()
        {
        }

        public BaseResponse(string ID, string WebID, string Name, string Description, string Path)
        {
            this.WebId = WebID;
            this.Id = ID;
            this.Name = Name;
            this.Description = Description;
            this.Path = Path;
        }

        [Newtonsoft.Json.JsonIgnore]
        public string WebId { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public string Path { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public Dictionary<string, string> Links { get; set; }
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
        public AFServer()
        {
        }

        public AFServer(string ID, string WebID, string Name, string Description, string Path)
            : base(ID, WebID, Name, Description, Path)
        {
        }

        public bool IsConnected { get; set; }
        public string ServerVersion { get; set; }
    }

    [Serializable]
    public class AFDB : BaseResponse
    {
        public AFDB()
        {
        }

        public AFDB(string ID, string WebID, string Name, string Description, string Path)
            : base(ID, WebID, Name, Description, Path)
        {
        }
    }

    [Serializable]
    public class UnitClass : BaseResponse
    {
        public UnitClass(string ID, string WebID, string Name, string Description, string Path)
            : base(ID, WebID, Name, Description, Path)
        {
        }

        public string CanonicalUnitName { get; set; }
        public string CanonicalUnitAbbreviation { get; set; }
    }

    [Serializable]
    public class AttributeCategory : BaseResponse
    {
        public AttributeCategory()
        {
        }

        public AttributeCategory(string ID, string WebID, string Name, string Description, string Path)
            : base(ID, WebID, Name, Description, Path)
        {
        }
    }

    [Serializable]
    public class ElementCategory : BaseResponse
    {
        public ElementCategory()
        {
        }

        public ElementCategory(string ID, string WebID, string Name, string Description, string Path)
            : base(ID, WebID, Name, Description, Path)
        {
        }
    }

    [Serializable]
    public class AFElement : BaseResponse
    {
        public AFElement()
        {
        }

        public AFElement(string ID, string WebID, string Name, string Description, string Path)
            : base(ID, WebID, Name, Description, Path)
        {
        }


        public string TemplateName { get; set; }
        public List<string> CategoryNames { get; set; }
    }

    [Serializable]
    public class AFAttributeTemplate : BaseResponse
    {
        public AFAttributeTemplate()
        {
        }

        public AFAttributeTemplate(string ID, string WebID, string Name, string Description, string Path)
            : base(ID, WebID, Name, Description, Path)
        {
        }

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
        public AFAttribute()
        {
        }

        public AFAttribute(string ID, string WebID, string Name, string Description, string Path)
            : base(ID, WebID, Name, Description, Path)
        {
        }

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
        public AFValue()
        {
        }

        public DateTime Timestamp { get; set; }
        public string Value { get; set; }
        public string UnitsAbbreviation { get; set; }
        public bool Good { get; set; }
        public bool Questionable { get; set; }
        public bool Substituted { get; set; }
    }

    [Serializable]
    public class Stream
    {
        public Stream()
        {
        }

        public Dictionary<string, string> Links { get; set; }
        public string UnitAbbreviation { get; set; }
        public List<AFValue> Items { get; set; }
    }

    [Serializable]
    public class AFElementTemplate : BaseResponse
    {
        public AFElementTemplate()
        {
        }

        public AFElementTemplate(string ID, string WebID, string Name, string Description, string Path)
            : base(ID, WebID, Name, Description, Path)
        {
        }

        public bool AllowElementToExtend { get; set; }
        public List<string> CategoryNames { get; set; }
    }

    [Serializable]
    public class AFEventFrame : BaseResponse
    {
        public AFEventFrame()
        {
        }

        public AFEventFrame(string ID, string WebID, string Name, string Description, string Path)
            : base(ID, WebID, Name, Description, Path)
        {
        }

        public string TemplateName { get; set; }
        public List<string> CategoryNames { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool AreValuesCaptured { get; set; }
    }

    [Serializable]
    public class AFEnumerationSet : BaseResponse
    {
        public AFEnumerationSet()
        {
        }
    }

    [Serializable]
    public class AFTable : BaseResponse
    {
        public AFTable()
        {
        }

        public AFTable(string ID, string WebID, string Name, string Description, string Path)
            : base(ID, WebID, Name, Description, Path)
        {
        }

        public List<string> CategoryNames { get; set; }
    }

    [Serializable]
    public class AFTableCategory : BaseResponse
    {
        public AFTableCategory()
        {
        }

        public AFTableCategory(string ID, string WebID, string Name, string Description, string Path)
            : base(ID, WebID, Name, Description, Path)
        {
        }
    }

    [Serializable]
    public class DataPoint : BaseResponse
    {
        public DataPoint()
        {
        }

        public DataPoint(string ID, string WebID, string Name, string Description, string Path)
            : base(ID, WebID, Name, Description, Path)
        {
        }

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
        public Dictionary<string, string> Links { get; set; }
        public List<T> Items { get; set; }
    }
}
