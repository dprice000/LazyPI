using LazyPI.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LazyPI.LazyObjects
{
    public class AFElementTemplate : BaseObject
    {
        private IEnumerable<string> _CategoryList;
        private Lazy<ObservableCollection<string>> _Categories;
        private static IAFElementTemplateContoller _templateController;

        #region "Properties"

        public bool IsExtendable { get; }

        public ObservableCollection<string> Categories
        {
            get
            {
                return _Categories.Value;
            }
        }

        #endregion "Properties"

        #region "Constructors"

        public AFElementTemplate()
        {
        }

        internal AFElementTemplate(Connection Connection, string WebID, string ID, string Name, string Description, string Path)
            : base(Connection, WebID, ID, Name, Description, Path)
        {
            Initialize();
        }

        private void Initialize()
        {
            _templateController = GetController(_Connection);

            // Load Categories
            _Categories = new Lazy<ObservableCollection<string>>(() =>
            {
                ObservableCollection<string> collection = new ObservableCollection<string>(_templateController.GetCategories(_Connection, ID));
                collection.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(CategoriesChanged);
                return collection;
            }, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);
        }

        private static IAFElementTemplateContoller GetController(Connection Connection)
        {
            IAFElementTemplateContoller result = null;

            if (Connection is WebAPI.WebAPIConnection)
            {
                result = new WebAPI.AFElementTemplateController();
            }

            return result;
        }

        /// <summary>
        /// Handles when changes are made to the categories list. Notifies and updates WebAPI.
        /// </summary>
        /// <param name="sender">The object that triggered notify.</param>
        /// <param name="e">Arguments describing the event.</param>
        private void CategoriesChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //TODO: Implement
            throw new NotImplementedException("Needs to be implemented");
        }

        #endregion "Constructors"

        #region "Static Methods"

        public static AFElementTemplate Find(Connection Connection, string ID)
        {
            return _templateController.Find(Connection, ID);
        }

        public static AFElementTemplate FindByName(string Name)
        {
            throw new NotImplementedException("Needs to be Implemented");
        }

        public static AFElementTemplate FindByPath(Connection Connection, string Path)
        {
            return _templateController.FindByPath(Connection, Path);
        }

        public static bool Delete(Connection Connection, string ID)
        {
            return _templateController.Delete(Connection, ID);
        }

        public static bool CreateElementTemplate(Connection Connection, string ParentID, AFElementTemplate Template)
        {
            return _templateController.CreateElementTemplate(Connection, ParentID, Template);
        }

        public static IEnumerable<AFAttributeTemplate> GetAttributeTemplates(Connection Connection, string ElementID)
        {
            return _templateController.GetAttributeTemplates(Connection, ElementID);
        }

        public class ElementTemplateFactory
        {
            public static AFElementTemplate CreateInstance(Connection Connection, BaseObject bObj)
            {
                return new AFElementTemplate(Connection, bObj.WebID, bObj.ID, bObj.Name, bObj.Description, bObj.Path);
            }

            public static AFElementTemplate CreateInstance(Connection Connection, string WebID, string ID, string Name, string Description, string Path)
            {
                return new AFElementTemplate(Connection, WebID, ID, Name, Description, Path);
            }
        }

        #endregion "Static Methods"
    }
}