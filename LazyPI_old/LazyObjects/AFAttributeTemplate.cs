using LazyPI.Common;

namespace LazyPI.LazyObjects
{
    public class AFAttributeTemplate : BaseObject
    {
        internal AFAttributeTemplate(Connection Connection, string WebID, string ID, string Name, string Description, string Path)
            : base(Connection, WebID, ID, Name, Description, Path)
        {
        }
    }
}