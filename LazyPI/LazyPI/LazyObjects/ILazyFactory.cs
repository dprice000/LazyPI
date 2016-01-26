using LazyPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    interface ILazyFactory
    {
        BaseObject CreateInstance(Connection Connection, BaseObject bObj);
        BaseObject CreateInstance(Connection Connection, string ID, string Name, string Description, string Path);
        IEnumerable<BaseObject> CreateList(Connection Connection, IEnumerable<BaseObject> Elements);
    }
}
