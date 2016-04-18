using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LazyPI.Common;

namespace LazyPI.LazyObjects
{
	public class AFElements : ObservableCollection<AFElement>
	{
		public AFElement this[string Name]
		{
			get
			{
				return this.Single(x => x.Name == Name);
			}
		}

		internal AFElements(IEnumerable<AFElement> elements) : base(elements)
		{
		}

        protected override void InsertItem(int index, AFElement item)
        {
            base.InsertItem(index, item);


        }
	}

	public class AFElement : BaseObject
	{
		private Lazy<AFElementTemplate> _Template;
		private Lazy<AFElement> _Parent;
		private Lazy<ObservableCollection<string>> _Categories;
		private static IAFElementController _ElementLoader;

		#region "Properties"
			public ObservableCollection<string> Categories
			{
				get
				{
					return _Categories.Value;
				}
			}

			public AFElementTemplate Template
			{
				get
				{
					return _Template.Value;
				}
			}

			public AFElement Parent
			{
				get
				{
					return _Parent.Value;
				}
			}

			public AFElements Elements
			{
				get
				{
					return new AFElements(_ElementLoader.GetElements(_Connection, _ID)); 
				}
				set
				{
				}
			}

			public AFAttributes Attributes
			{
				get
				{
					return new AFAttributes(_ElementLoader.GetAttributes(_Connection, _ID));
				}
				set
				{
				}
			}
		#endregion

		#region "Constructors"
			public AFElement()
			{
			}

			internal AFElement(Connection Connection, string WebID, string ID, string Name, string Description, string Path)
				: base(Connection, WebID, ID, Name, Description, Path)
			{
				Initialize();
			}

			/// <summary>
			/// Builds loader object and sets all callbacks for lazy loading 
			/// </summary>
			private void Initialize()
			{
				CreateLoader();

				//Initialize Category List

				_Categories = new Lazy<ObservableCollection<string>>(() => {
					return new ObservableCollection<string>(_ElementLoader.GetCategories(_Connection, _ID));
				}, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

				//Initialize Template Loader
				_Template = new Lazy<AFElementTemplate>(() =>
				{
					string templateName = _ElementLoader.GetElementTemplate(_Connection, this._ID);
					return AFElementTemplate.Find(_Connection, templateName);
				}, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

				//Initialize Parent Loader
				string parentPath = Path.Substring(0, Path.LastIndexOf('\\')); 
				
				_Parent = new Lazy<AFElement>(() =>
				{
					return _ElementLoader.FindByPath(_Connection, parentPath);
				}, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);
			}

			private void CreateLoader()
			{
				if (_Connection is WebAPI.WebAPIConnection)
				{
					_ElementLoader = new WebAPI.AFElementController();
				}
			}
		#endregion

		#region"Callbacks"
			private void AttributesChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
			{
				if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
				{
					AFAttribute.Create(_Connection, this._ID, (AFAttribute)sender);
				}
				else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
				{
					AFAttribute obj = (AFAttribute)sender;
					AFAttribute.Delete(_Connection, obj.ID);
				}
				else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
				{
					throw new NotImplementedException("Replace is not supported by LazyPI.");
				}
				else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
				{
					throw new NotImplementedException("Reset is not supported by LazyPI.");
				}
				else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Move)
				{
					throw new NotImplementedException("Move is not supported by LazyPI.");
				}
			}

			/// <summary>
			/// Notifies when developer makes changes to list. This method makes call back to insure PI is up to date.
			/// </summary>
			/// <param name="sender">Object that triggered the change.</param>
			/// <param name="e">Arguments that define the event.</param>
			private void ChildrenChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
			{
				if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
				{
					_ElementLoader.CreateChildElement(_Connection, this._ID, (AFElement)sender);
				}
				else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
				{
					AFElement element = (AFElement)sender;
					Delete(_Connection, element._ID);
				}
				else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
				{
					throw new NotImplementedException("Replace is not supported by LazyPI.");
				}
				else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
				{
					throw new NotImplementedException("Reset is not supported by LazyPI.");
				}
				else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Move)
				{
					throw new NotImplementedException("Move is not supported by LazyPI.");
				}
			}
		#endregion

		#region "Interactions"
			public void CheckIn()
			{
				_ElementLoader.Update(_Connection, this);
			}
		#endregion

		#region "Static Methods"
		/// <summary>
		/// Returns element requested
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public static AFElement Find(Connection Connection, string ID)
		{
			return _ElementLoader.Find(Connection, ID);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Path"></param>
		/// <returns></returns>
		public static AFElement FindByPath(Connection Connection, string Path)
		{
			return _ElementLoader.FindByPath(Connection, Path);
		}

		/// <summary>
		/// Removes specific element from AF Database
		/// </summary>
		/// <param name="ElementID">The ID of the element to be deleted</param>
		/// <returns></returns>
		public static bool Delete(Connection Connection, string ElementID)
		{
			return _ElementLoader.Delete(Connection, ElementID);
		}

		/// <summary>
		/// Find all of the child elements with a specific category.
		/// </summary>
		/// <param name="RootID">The parent or root for the search.</param>
		/// <param name="CategoryName">Name of the category to be searched for.</param>
		/// <param name="MaxCount">Max number of elements that should be searched for.</param>
		/// <returns>A list of elements that have a specific category.</returns>
		public static IEnumerable<AFElement> FindByCategory(Connection Connection, string RootID, string CategoryName, int MaxCount = 1000)
		{
			return _ElementLoader.GetElements(Connection, RootID, "*", CategoryName, "*", ElementType.Any, false, "Name", "Ascending", 0, MaxCount);
		}

		/// <summary>
		/// Find all of the child elements with a specific template.
		/// </summary>
		/// <param name="RootID">The parent or root for the search</param>
		/// <param name="TemplateName">Name of the template to be searched for.</param>
		/// <param name="MaxCount">Max number of elements that should be searched for.</param>
		/// <returns>A list of elements that have a specific template.</returns>
		public static IEnumerable<AFElement> FindByTemplate(Connection Connection, string RootID, string TemplateName, int MaxCount = 1000)
		{
			return _ElementLoader.GetElements(Connection, RootID, "*", "*", TemplateName, ElementType.Any, false, "Name", "Ascending", 0, MaxCount);
		}
		#endregion
	}

}