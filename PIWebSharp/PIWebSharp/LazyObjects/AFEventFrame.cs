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

        #endregion
    }
}
