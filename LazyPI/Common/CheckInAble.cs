using System;

namespace LazyPI.Common
{
    public abstract class CheckInAble : BaseObject
    {

        #region "Properties"

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

        #endregion

        #region "Constructors"

        public CheckInAble() : base()
        {
            IsNew = true;
            IsDirty = false;
            IsDeleted = false;
        }

        public CheckInAble(string Name, string Description) : base(Name, Description)
        {
            _Name = Name;
            _Description = Description;

            IsNew = true;
            IsDirty = false;
            IsDeleted = false;
        }

        public CheckInAble(Connection connection, string webId, string id, string name, string description, string path) : base(connection, webId, id, name, description, path)
        {
            IsNew = true;
            IsDirty = false;
            IsDeleted = false;
        }

        #endregion

        public abstract void CheckIn();

        protected abstract void ResetState();

        protected void ItemsChangedMethod(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (CheckInAble item in e.NewItems)
                {
                    item.IsNew = true;
                    IsDirty = true;
                }
            }
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (CheckInAble item in e.OldItems)
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