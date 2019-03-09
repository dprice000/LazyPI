namespace LazyPI.Common
{
    public abstract class Connection
    {
        protected string _Hostname;
        protected string _Username;

        #region "Properties"

        public string Hostname
        {
            get
            {
                return _Hostname;
            }
        }

        public string Username
        {
            get
            {
                return _Username;
            }
        }

        #endregion "Properties"
    }
}