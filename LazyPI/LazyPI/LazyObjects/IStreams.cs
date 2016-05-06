using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazyPI.Common;

namespace LazyPI.LazyObjects
{
    interface IStreams
    {
        AFValue GetEnd(Connection Connection, string PointID, string DesiredUnits);
        IEnumerable<AFValue> GetInterpolated(Connection Connection, string PointID, DateTime StartTime, DateTime EndTime, string Interval, string DesiredUnits, string FilterExpression, bool IncludeFilteredValues);
        AFValue GetInterpolatedAtTimes(Connection Connection, string PointID, string DesiredUnits, string FilterExpression, bool IncludeFilteredValues, string SortOrder);
        IEnumerable<AFValue> GetPlot(Connection Connection, string PointID, string StartTime, string EndTime, int Intervals, string DesiredUnits);
        IEnumerable<AFValue> GetRecorded(Connection Connection, string PointID, string StartTime, string EndTime, BoundryType BoundryType, string DesiredUnits, string FilterExpression, bool IncludeFilteredValues, int MaxCount);
        AFValue GetRecordedAtTime(Connection Connection, DateTime Time, RetreivalMode RetreivalMode, string DesiredUnits);
        IEnumerable<AFValue> GetSummary(Connection Connection, string PointID, DateTime StartTime, DateTime EndTime, SummaryType SummaryType, CalculationBasis CalculationBasis, TimeType TimeType, string SummaryDuration, SampleType SampleType, string SampleInterval, string FilterExpression);
        AFValue GetValue(Connection Connection, string PointID, DateTime Time, string DesiredUnits);
        void UpdateValue(Connection Connection, string PointID, UpdateOption UpdateOption, BufferOption BufferOption);
        void UpdateValues(Connection Connection, string PointID, UpdateOption UpdateOption, BufferOption BufferOption);
    }
}
