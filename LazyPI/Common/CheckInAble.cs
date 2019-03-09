namespace LazyPI.Common
{
    public abstract class CheckInAble : BaseObject
    {
        public CheckInAble()
        {
        }

        public CheckInAble(Connection connection, string webId, string id, string name, string description, string path) : base(connection, webId, id, name, description, path)
        {
        }

        public abstract void CheckIn();

        protected abstract void ResetState();
    }
}