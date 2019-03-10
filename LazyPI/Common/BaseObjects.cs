namespace LazyPI.Common
{
    public class BaseObject
    {
        protected Connection _Connection;
        protected string _Name;
        protected string _Description;

        #region "Properties"

        public string ID { get; protected set; }

        public string WebID { get; protected set; }

        public string Path { get; protected set; }

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }

        #endregion "Properties"

        #region "Constructors"

        public BaseObject()
        {
        }

        public BaseObject(string Name, string Description)
        {
            _Name = Name;
            _Description = Description;
        }

        public BaseObject(Connection Connection, string webId, string id, string name, string description, string path)
        {
            _Connection = Connection;
            ID = id;
            Name = name;
            Description = description;
            Path = path;
            WebID = webId;
        }

        #endregion "Constructors"
    }
}