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
		private bool _IsNew;
		private bool _IsDirty;
		private bool _IsDeleted;
		private Lazy<AFElementTemplate> _Template;
		private Lazy<AFElement> _Parent;
		private Lazy<ObservableCollection<string>> _Categories;
		private static IAFElementController _ElementLoader;

		#region "Properties"
			public bool IsNew
			{
				get
				{
					return _IsNew;
				}
			}

			public bool IsDirty
			{
				get
				{
					return _IsDirty;
				}
			}

			public bool IsDeleted
			{
				get
				{
					return _IsDeleted;
				}
			}

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
					return new AFElements(_ElementLoader.GetElements(_Connection, _WebID)); 
				}
				set
				{
				}
			}

			public AFAttributes Attributes
			{
				get
				{
					return new AFAttributes(_ElementLoader.GetAttributes(_Connection, _WebID));
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
					return new ObservableCollection<string>(_ElementLoader.GetCategories(_Connection, _WebID));
				}, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

				//Initialize Template Loader
				_Template = new Lazy<AFElementTemplate>(() =>
				{
					string templateName = _ElementLoader.GetElementTemplate(_Connection, _WebID);
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

		#region "Interactions"
			public void CheckIn()
			{
				if (_IsDeleted)
					_ElementLoader.Delete(_Connection, _WebID);
				else if (_IsDirty)
					_ElementLoader.Update(_Connection, this);
			}

			public void Delete()
			{
				_IsDeleted = true;
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