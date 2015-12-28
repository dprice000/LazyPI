using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIWebSharp
{
    /// <summary>
    /// The possible values for the type of an element
    /// </summary>
    public enum ElementType
    {
        None,
        Other,
        Node,
        Measurement,
        Flow,
        Transfer,
        Boundry,
        PIPoint,
        Any
    }

    public enum SearchMode
    {
        None,
        StartInclusive,
        EndInclusive,
        Inclusive,
        Overlapped,
        InProgress,
        BackwardFromStartTime,
        ForwardFromStartTime,
        BackwardFromEndTime,
        ForwardFromEndTime,
        ForwardInProgress
    }

    public class LinksResponse
    {
        public string Self { get; set; }
    }

    /// <summary>
    /// A Base Object that all elements are derived from.
    /// </summary>
    public abstract class BaseResponse
    {
        public string WebID { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public LinksResponse Links { get; set; }
    }

    public class HomeResponse
    {
        public string Self { get; set; }
        public string AssetServers { get; set; }
        public string DataServers { get; set; }
        public string System { get; set; }
    }

    public class AFServer : BaseResponse
    {
        public bool IsConnected { get; set; }
        public string ServerVersion { get; set; }
    }

    public class AFDB : BaseResponse
    {
    }

    public class UnitClass : BaseResponse
    {
        public string CanonicalUnitName { get; set; }
        public string CanonicalUnitAbbreviation { get; set; }
    }

    public class AttributeCategory : BaseResponse
    {
    }

    public class ElementCategory : BaseResponse
    {
    }

    public class AFElement : BaseResponse
    {
        public string TemplateName { get; set; }
        public List<string> CategoryNames { get; set; }
    }

    public class AFElementTemplate : BaseResponse
    {
        public bool AllowElementToExtend { get; set; }
        public List<string> CategoryNames { get; set; }
    }

    public class AFEventFrame : BaseResponse
    {
        public string TemplateName { get; set; }
        public List<string> CategoryNames { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool AreValuesCaptured { get; set; }
    }

    public class AFEnumerationSet : BaseResponse
    {
    }

    public class AFTable : BaseResponse
    {
        public List<string> CategoryNames { get; set; }
    }

    public class AFTableCategory : BaseResponse
    {
    }

    /// <summary>
    /// Used to handle any response that includes a list of items
    /// </summary>
    /// <typeparam name="T">The type to be held in the Items list</typeparam>
    public class ResponseList<T>
    {
        public LinksResponse Links { get; set; }
        public List<T> Items { get; set; }
    }
}
