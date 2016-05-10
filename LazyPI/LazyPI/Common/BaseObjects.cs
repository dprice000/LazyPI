using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.Common
{
    public class BaseObject
    {
        protected LazyPI.Common.Connection _Connection;
        protected string _ID;
        protected string _WebID;
        protected string _Name;
        protected string _Path;
        protected string _Description;

        #region "Properties"
        public string ID
        {
            get
            {
                return _ID;
            }
        }

        public string WebID
        {
            get
            {
                return _WebID;
            }
        }

        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }

        public string Path
        {
            get
            {
                return this._Path;
            }
        }
        #endregion

        #region "Constructors"
        public BaseObject()
        { 
        }

        public BaseObject(string Name, string Description)
        {
            _Name = Name;
            _Description = Description;
        }

        public BaseObject(LazyPI.Common.Connection Connection, string webId, string id, string name, string description, string path)
        {
            _Connection = Connection;
            _ID = id;
            _Name = name;
            _Description = description;
            _Path = path;
            _WebID = webId;
        }
        #endregion
    }
}
