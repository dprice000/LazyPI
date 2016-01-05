using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public class AFEventFrame : BaseObject
    {
        private DateTimeOffset _StartTime;
        private DateTimeOffset _EndTime;
        //TODO: Should handle event frame template
        private Lazy<IEnumerable<AFEventFrame>> _EventFrames;
        private Lazy<IEnumerable<AFAttribute>> _Attributes;
        private IEnumerable<string> _CategoryNames;

        #region "Properties"

        public DateTimeOffset StartTime
        {
            get
            {
                return _StartTime;
            }
            set
            {
                _StartTime = value;
            }
        }

        public DateTimeOffset EndTime
        {
            get
            {
                return _EndTime;
            }
            set
            {
                _EndTime = value;
            }
        }

        public IEnumerable<string> CategoryNames
        {
            get
            {
                return _CategoryNames;
            }
            set
            {
                _CategoryNames = value;
            }
        }
        #endregion
    }
}
