using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using RestSharp;
using LazyPI.Common;

namespace LazyPI.LazyObjects
{
	public class AFElement : LazyPI.BaseObject
	{
		private Lazy<AFElementTemplate> _Template;
		private Lazy<AFElement> _Parent;
		private IEnumerable<string> _CategoryNames;
		private Lazy<ObservableCollection<AFElementCategory>> _Categories;
		private Lazy<ObservableCollection<AFElement>> _Children;
		private Lazy<ObservableCollection<AFAttribute>> _Attributes;
		private static IAFElement _ElementLoader;

		#region "Properties"
			//TODO: To  be removed when category resolution code is written.
			public IEnumerable<string> CategoryNames
			{
				get
				{
					return _CategoryNames;
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

			public ObservableCollection<AFElement> Children
			{
				get
				{
					return _Children.Value; 
				}
			}

			public ObservableCollection<AFAttribute> Attributes
			{
				get
				{
					return _Attributes.Value;
				}
			}
		#endregion

		#region "Constructors"
			private AFElement(Connection Connection, string ID, string Name, string Description, string Path)
				: base(Connection, ID, Name, Description, Path)
			{
				Initialize();
			}

			/// <summary>
			/// Builds loader object and sets all callbacks for lazy loading 
			/// </summary>
			private void Initialize()
			{
				//Initialize Category List
				_CategoryNames = _ElementLoader.GetCategories(_Connection, _ID);

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
					return ElementFactory.CreateInstance(_Connection, _ElementLoader.FindByPath(_Connection, parentPath));
				}, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

				//Initialize Attributes Loader
				_Attributes = new Lazy<ObservableCollection<AFAttribute>>(() => 
				{
					List<BaseObject> resultList = _ElementLoader.GetAttributes(_Connection, this.ID).ToList();
					ObservableCollection<AFAttribute> obsList = new ObservableCollection<AFAttribute>();

					foreach (var attr in resultList)
					{
						obsList.Add(AFAttribute.Find(attr.ID));
					}
					
					obsList.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(AttributesChanged);
					return obsList;
				}, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

				//Initialize Children Loader
				_Children = new Lazy<ObservableCollection<AFElement>>(() =>
				{
					List<AFElement> resultList = ElementFactory.CreateList(_Connection, _ElementLoader.GetElements(_Connection, this.ID));
					ObservableCollection<AFElement> obsList = new ObservableCollection<AFElement>(resultList);
					obsList.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(ChildrenChanged);
					return obsList;
				}, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);
			}
		#endregion

		#region"Callbacks"
			private void AttributesChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
			{
				if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
				{
					AFAttribute.Create(this._ID, (AFAttribute)sender);
				}
				else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
				{
					AFAttribute obj = (AFAttribute)sender;
					AFAttribute.Delete(obj.ID);
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
			return ElementFactory.CreateInstance(Connection, _ElementLoader.Find(Connection, ID));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Path"></param>
		/// <returns></returns>
		public static AFElement FindByPath(Connection Connection, string Path)
		{
			return ElementFactory.CreateInstance(Connection, _ElementLoader.FindByPath(Connection, Path));
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
			var baseList = _ElementLoader.GetElements(Connection, RootID, "*", CategoryName, "*", ElementType.Any, false, "Name", "Ascending", 0, MaxCount);

			return ElementFactory.CreateList(Connection, baseList);
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
			var baseList = _ElementLoader.GetElements(Connection, RootID, "*", "*", TemplateName, ElementType.Any, false, "Name", "Ascending", 0, MaxCount);

			return ElementFactory.CreateList(Connection, baseList);
		}
		#endregion

		/// <summary>
		/// Uses the hidden constructor to create full instances of AFElement.
		/// </summary>
		public class ElementFactory
		{
			public static AFElement CreateInstance(Connection Connection, BaseObject bObj)
			{
				return new AFElement(Connection, bObj.ID, bObj.Name, bObj.Description, bObj.Path);
			}

			public static AFElement CreateInstance(Connection Connection, string ID, string Name, string Description, string Path)
			{
				return new AFElement(Connection, ID, Name, Description, Path);
			}

			public static List<AFElement> CreateList(Connection Connection, IEnumerable<BaseObject> Elements)
			{
				List<AFElement> elementList = new List<AFElement>();

				foreach(AFElement element in Elements)
				{
					elementList.Add(new AFElement(Connection, element.ID, element.Name, element.Description, element.Path));
				}

				return elementList;
			}
		}
	}
}