using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazyPI.Common;

namespace LazyPI.LazyObjects
{
    public class AFValue : BaseObject
    {
        private DateTime _Timestamp;
        private string _Value;
        private string _UnitsAbbreviation;
        private bool _Good;
        private bool _Questionable;
        private bool _Substituted;

        #region "Properties"
        public DateTimeOffset Timestamp
        {
            get
            {
                return _Timestamp;
            }
        }

        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
                _Timestamp = DateTime.Now;
            }
        }

        public string UnitsAbbreviation
        {
            get
            {
                return _UnitsAbbreviation;
            }
        }

        public bool Good
        {
            get
            {
                return _Good;
            }
        }

        public bool Questionable
        {
            get
            {
                return _Questionable;
            }
        }

        public bool Substituted
        {
            get
            {
                return _Substituted;
            }
        }
        #endregion

        #region "Constructors"
        public AFValue(DateTime TimeStamp, string Value, string Units, bool Good, bool Questionable, bool Substituted)
        {
            _Timestamp = TimeStamp;
            _Value = Value;
            _UnitsAbbreviation = Units;
            _Good = Good;
            _Questionable = Questionable;
            _Substituted = Substituted;
        }
        #endregion
    }
}
