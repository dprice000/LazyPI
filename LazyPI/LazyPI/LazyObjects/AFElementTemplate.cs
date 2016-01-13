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
        private static IAFElementTemplate _templateLoader;
        private IEnumerable<string> _CategoryList;
        private Lazy<ObservableCollection<AFElementCategory>> _Categories;
        private bool _IsExtendable;

        #region "Properties"
            public ObservableCollection<AFElementCategory> Categories
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
            private AFElementTemplate(string ID, string Name, string Description, string Path)
                : base(ID, Name, Description, Path)
            {
                Initialize();
            }

            public AFElementTemplate(string ID)
            {
                BaseObject result = _templateLoader.Find(ID);
                this._ID = result.ID;
                this._Name = result.Name;
                this._Description = result.Description;
                this._Path = result.Path;

                Initialize();
            }

            public AFElementTemplate(string Name, string Path)
            {
                //TODO: Should do something fancy with Path and Name to find template by path
            }

            private void Initialize()
            {
                // Load Categories
                _Categories = new Lazy<ObservableCollection<AFElementCategory>>(() => {
                   ObservableCollection<AFElementCategory> collection = new ObservableCollection<AFElementCategory>(_templateLoader.GetCategories(this._ID));
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
            public static AFElementTemplate Find(string ID)
            {
                return _templateLoader.Find(ID);
            }

            public static AFElementTemplate FindByPath(string path)
            {
                return _templateLoader.FindByPath(path);
            }

            public static bool Delete(string ID)
            {
                return _templateLoader.Delete(ID);
            }

            public static bool CreateElementTemplate(string parentID, AFElementTemplate template)
            {
                return _templateLoader.CreateElementTemplate(parentID, template);
            }

            public static IEnumerable<AFAttributeTemplate> GetAttributeTemplates(string elementID)
            {
                return _templateLoader.GetAttributeTemplates(elementID);
            }

            public class ElementTemplateFactory
            {
                public static AFElementTemplate CreateInstance(BaseObject bObj)
                {
                    return new AFElementTemplate(bObj.ID, bObj.Name, bObj.Description, bObj.Path);
                }

                public static AFElementTemplate CreateInstance(string ID, string Name, string Description, string Path)
                {
                    return new AFElementTemplate(ID, Name, Description, Path);
                }
            }
        #endregion
    }
}
