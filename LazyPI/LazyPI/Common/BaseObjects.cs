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
        protected bool _IsNew;
        protected bool _IsDirty;
        protected bool _IsDeleted;

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
                this._IsDirty = true;
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
                this._IsDirty = true;
            }
        }

        public string Path
        {
            get
            {
                return this._Path;
            }
        }

        public bool IsNew
        {
            get
            {
                return _IsNew;
            }
            internal set
            {
                _IsNew = value;
            }
        }

        public bool IsDirty
        {
            get
            {
                return _IsDirty;
            }
            internal set
            {
                _IsDirty = value;
            }
        }

        public bool IsDeleted
        {
            get
            {
                return _IsDeleted;
            }
            internal set
            {
                _IsDeleted = value;
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

        protected void ItemsChangedMethod(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (BaseObject item in e.NewItems)
                {
                    item.IsNew = true;
                    _IsDirty = true;
                }
            }
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (BaseObject item in e.OldItems)
                {
                    item.IsDeleted = true;
                    _IsDirty = true;
                }
            }
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
            {
                throw new NotImplementedException("Replace not implemented by LazyPI");
            }
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Move)
            {
                throw new NotImplementedException("Move not implemented by LazyPI");
            }
        }
    }
}
