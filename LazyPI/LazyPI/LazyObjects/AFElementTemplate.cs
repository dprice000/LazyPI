using LazyPI.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public class AFElementTemplate : BaseObject
    {
        private IEnumerable<string> _CategoryList;
        private Lazy<ObservableCollection<string>> _Categories;
        private bool _IsExtendable;
        private static IAFElementTemplate _templateLoader;

        #region "Properties"
            public ObservableCollection<string> Categories
            {
                get
                {
                    return _Categories.Value;
                }
            }

            public bool IsExtendable
            {
                get
                {
                    return _IsExtendable;
                }
            }
        #endregion

        #region "Constructors"
            public AFElementTemplate()
            {
            }

            internal AFElementTemplate(Connection Connection, string ID, string Name, string Description, string Path)
                : base(Connection, ID, Name, Description, Path)
            {
                Initialize();
            }

            private void Initialize()
            {
                // Load Categories
                _Categories = new Lazy<ObservableCollection<string>>(() => {
                   ObservableCollection<string> collection = new ObservableCollection<string>(_templateLoader.GetCategories(_Connection, this._ID));
                   collection.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(CategoriesChanged);
                   return collection;
                }, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);
            }

            /// <summary>
            /// Handles when changes are made to the categories list. Notifies and updates WebAPI.
            /// </summary>
            /// <param name="sender">The object that triggered notify.</param>
            /// <param name="e">Arguments describing the event.</param>
            private void CategoriesChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
            {
                //TODO: Implement
            }
        #endregion

        #region "Static Methods"
            public static AFElementTemplate Find(Connection Connection, string ID)
            {
                return _templateLoader.Find(Connection, ID);
            }

            public static AFElementTemplate FindByName(string Name)
            {
                throw new NotImplementedException("Needs to be Implemented");
            }

            public static AFElementTemplate FindByPath(Connection Connection, string Path)
            {
                return _templateLoader.FindByPath(Connection, Path);
            }

            public static bool Delete(Connection Connection, string ID)
            {
                return _templateLoader.Delete(Connection, ID);
            }

            public static bool CreateElementTemplate(Connection Connection, string ParentID, AFElementTemplate Template)
            {
                return _templateLoader.CreateElementTemplate(Connection, ParentID, Template);
            }

            public static IEnumerable<AFAttributeTemplate> GetAttributeTemplates(Connection Connection, string ElementID)
            {
                return _templateLoader.GetAttributeTemplates(Connection, ElementID);
            }

            public class ElementTemplateFactory
            {
                public static AFElementTemplate CreateInstance(Connection Connection, BaseObject bObj)
                {
                    return new AFElementTemplate(Connection, bObj.ID, bObj.Name, bObj.Description, bObj.Path);
                }

                public static AFElementTemplate CreateInstance(Connection Connection, string ID, string Name, string Description, string Path)
                {
                    return new AFElementTemplate(Connection, ID, Name, Description, Path);
                }
            }
        #endregion
    }
}
