using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIWebSharp.WebAPI
{

    /// <summary>
    /// Values to indicate which summary type calculation(s) should be performed. 
    /// </summary>
    public enum SummaryType
    {
        None,
        Total,
        Average,
        Minimum,
        Maximum,
        Range,
        StdDev,
        PopulationStdDev,
        Count,
        PercentGood,
        All,
        AllForNonNumeric
    }

    /// <summary>
    /// Defines the timestamp returned for a value when a summary calculation is done. 
    /// </summary>
    public enum TimeType
    {
        Auto,
        EarliestTime,
        MostRecentTime
    }

    /// <summary>
    /// Defines the evaluation of an expression over a time range.
    /// </summary>
    public enum SampleType
    {
        ExpressionRecordedValues,
        Interval
    }

    /// <summary>
    /// Defines the possible calculation options when performing summary calculations over time-series data.
    /// </summary>
    public enum CalculationBasis
    {
        TimeWeighted,
        EventWeighted,
        TimeWeightedContinuous,
        TimeWeightedDiscrete,
        EventWeightedExcludeMostRecentEvent,
        EventWeightedExcludeEarliestEvent,
        EventWeightedIncludeBothEnds
    }

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

    public class HomeResponse
    {
        public string Self { get; set; }
        public string AssetServers { get; set; }
        public string DataServers { get; set; }
        public string System { get; set; }
    }

    public class DataServer : BaseResponse
    {
        public bool IsConnected { get; set; }
        public string ServerVersion { get; set; }
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

    public class AFAtrribute : BaseResponse
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

    public class AFValue
    {
        public DateTime Timestamp { get; set; }
        public string Value { get; set; }
        public string UnitsAbbreviation { get; set; }
        public bool Good { get; set; }
        public bool Questionable { get; set; }
        public bool Substituted { get; set; }
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

    public class DataPoint :BaseResponse
    {
        public string PointClass { get; set; }
        public string PointType { get; set; }
        public bool Future { get; set; }
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
