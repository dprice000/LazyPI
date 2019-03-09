using LazyPI.Common;

namespace LazyPI.LazyObjects
{
    public interface IAFUnitController
    {
        AFUnit Find(Connection Connection, string ID);

        AFUnit FindByPath(Connection Connection, string Path);

        bool Update(Connection Connection, AFUnit unit);

        bool Delete(Connection Connection, string ID);
    }
}