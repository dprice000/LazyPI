namespace LazyPI.Common
{
    public abstract class Connection
    {
        #region "Properties"

        public string Hostname { get; protected set; }

        public string Username { get; protected set; }

        #endregion "Properties"
    }
}