using LazyPI.Common;

namespace LazyPI.LazyObjects
{
    public interface IAFAttributeCategoryController
    {
        AFAttributeCategory Find(Connection Connection, string ID);

        AFAttributeCategory FindByPath(Connection Connection, string Path);

        bool Update(Connection Connection, AFAttributeCategory Category);

        bool Delete(Connection Connection, string ID);
    }
}