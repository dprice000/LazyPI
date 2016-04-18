using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazyPI.Common;

namespace LazyPI.LazyObjects
{
    public class AFAttributeTemplate : BaseObject
    {
        internal AFAttributeTemplate(LazyPI.Common.Connection Connection, string WebID, string ID, string Name, string Description, string Path)
            : base(Connection, WebID, ID, Name, Description, Path)
        {
        }
    }
}
