using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public class PIPoint : LazyPI.Common.BaseObject
    {
        private string _PointClass;
        private string _PointType;
        private bool _Future;

        #region"Properties"
        public string PointClass
        {
            get
            {
                return _PointClass;
            }
        }

        public string PointType
        {
            get
            {
                return _PointType;
            }
        }

        public bool Future
        {
            get 
            {
                return _Future;
            }
        }
        #endregion

        #region "Constructors"
        public PIPoint(LazyPI.Common.Connection Connection, string WebID, string ID, string Name, string Description, string Path) :
            base(Connection, WebID, ID, Name, Description, Path)
        {
        }

        public PIPoint(LazyPI.Common.Connection Connection, string WebID, string ID, string Name, string Description, string Path, string PointType, string PointClass, bool Future) :
            base(Connection, WebID, ID, Name, Description, Path)
        {
            _PointClass = PointClass;
            _PointType = PointType;
            _Future = Future;
        }
        #endregion

        #region "Interactions"

        #endregion
    }
}
