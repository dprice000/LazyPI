using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazyPI.Common;

namespace LazyPI.LazyObjects
{
    public interface IAFDatabase
    {
        AFDatabase Find(Connection Connection, string ID);
        AFDatabase FindByPath(Connection Connection, string Path);
        bool Update(Connection Connection, AFDatabase AFDB);
        bool Delete(Connection Connection, string DatabaseID);
        bool CreateElement(Connection Connection, AFElement Element);
        bool CreateEventFrame(Connection Connection, AFEventFrame EventFrame);
        IEnumerable<AFElement> GetElements();
        IEnumerable<AFEventFrame> GetEventFrames();
    }
}
