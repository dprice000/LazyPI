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
        public AFAttributeTemplate(LazyPI.Common.Connection Connection, string ID, string Name, string Description, string Path)
            : base(Connection, ID, Name, Description, Path)
        {
        }
    }
}
