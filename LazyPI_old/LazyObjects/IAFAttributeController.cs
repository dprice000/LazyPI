using LazyPI.Common;

namespace LazyPI.LazyObjects
{
    public interface IAFAttributeController
    {
        AFAttribute Find(Connection Connection, string ID);

        AFAttribute FindByPath(Connection Connection, string Path);

        bool Update(Connection Connection, AFAttribute Attr);

        bool Delete(Connection Connection, string ID);

        bool Create(Connection Connection, string ParentID, AFAttribute Attr);

        bool CreateChild(Connection Connection, string AttrID, AFAttribute Attr);

        PIPoint GetPoint(Connection Connection, string AttrID);

        AFValue GetValue(Connection Connection, string AttrID);

        bool SetValue(Connection Connection, string AttrID, AFValue Value);
    }
}