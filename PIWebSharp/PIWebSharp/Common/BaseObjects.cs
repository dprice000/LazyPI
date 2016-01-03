using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI
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

    abstract class BaseObject
    {
        protected string _ID;
        protected string _Name;
        protected string _Path;
        protected string _Description;

        #region "Properties"
        public string ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }

        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }

        public string Path
        {
            get
            {
                return this._Path;
            }
        }
        #endregion
    }
}
