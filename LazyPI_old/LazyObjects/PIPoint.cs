using System;
using System.Collections.Generic;

namespace LazyPI.LazyObjects
{
    public class PIPoint : Common.BaseObject
    {
        private IStreams _DataStream;

        #region"Properties"

        public string PointClass { get; }

        public string PointType { get; }

        public bool Future { get; }

        #endregion

        #region "Constructors"

        public PIPoint(Common.Connection Connection, string WebID, string ID, string Name, string Description, string Path) :
            base(Connection, WebID, ID, Name, Description, Path)
        {
        }

        public PIPoint(Common.Connection Connection, string WebID, string ID, string Name, string Description, string Path, string PointType, string PointClass, bool Future) :
            base(Connection, WebID, ID, Name, Description, Path)
        {
            this.PointClass = PointClass;
            this.PointType = PointType;
            this.Future = Future;
        }

        #endregion

        #region "Interactions"

        public AFValue ValueAtTime(DateTime Time, string DesiredUnits, Common.RetreivalMode RetreivalMode = Common.RetreivalMode.Auto)
        {
            return _DataStream.GetRecordedAtTime(_Connection, Time, RetreivalMode, DesiredUnits);
        }

        public IEnumerable<AFValue> RecordedValues(string StartTime, string EndTime, Common.BoundryType BoundryType, string DesiredUnits, string FilterExpression, bool IncludeFilters, int MaxCount = 1000)
        {
            return _DataStream.GetRecorded(_Connection, WebID, StartTime, EndTime, BoundryType, DesiredUnits, FilterExpression, IncludeFilters, MaxCount);
        }

        public IEnumerable<AFValue> Summary(DateTime StartTime, DateTime EndTime, Common.SummaryType SummaryType, Common.CalculationBasis CalcBasis, Common.TimeType TimeType, string SummaryDuration, Common.SampleType SampleType)
        {
            return _DataStream.GetSummary(_Connection, WebID, StartTime, EndTime, SummaryType, CalcBasis, TimeType, SummaryDuration, SampleType);
        }

        #endregion
    }
}