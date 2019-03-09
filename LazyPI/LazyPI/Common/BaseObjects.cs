using System;

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

        public bool IsNew { get; internal set; }

        public bool IsDirty { get; internal set; }

        public bool IsDeleted { get; internal set; }

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                IsDirty = true;
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
                IsDirty = true;
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

        public BaseObject(LazyPI.Common.Connection Connection, string webId, string id, string name, string description, string path)
        {
            _Connection = Connection;
            ID = id;
            Name = name;
            Description = description;
            Path = path;
            WebID = webId;
        }

        #endregion "Constructors"

        protected void ItemsChangedMethod(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (BaseObject item in e.NewItems)
                {
                    item.IsNew = true;
                    IsDirty = true;
                }
            }
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (BaseObject item in e.OldItems)
                {
                    item.IsDeleted = true;
                    IsDirty = true;
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