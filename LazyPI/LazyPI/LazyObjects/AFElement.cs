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
				return this.SingleOrDefault(x => x.Name == Name);
			}
		}

		internal AFElements(IEnumerable<AFElement> elements) : base(elements.ToList())
		{
		}

	}

	public class AFElement : BaseObject
	{
		private bool _IsNew;
		private bool _IsDirty;
		private bool _IsDeleted;
		private AFElementTemplate _Template;
		private AFElement _Parent;
		private AFElements _Elements;
		private AFAttributes _Attributes;
		private List<string> _Categories;
		private static IAFElementController _ElementController;

		#region "Properties"
			public string Name
			{
				get
				{
					return base.Name;
				}
				set
				{
					base.Name = value;
					_IsDirty = true;
				}
			}

			public string Description
			{
				get
				{
					return base.Description;
				}
				set
				{
					base.Description = value;
					_IsDirty = true;
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

			public List<string> Categories
			{
				get
				{
					if (_Categories == null)
					{
						_Categories = new List<string>(_ElementController.GetCategories(_Connection, _WebID));
					}

					return _Categories;
				}
			}

			public AFElementTemplate Template
			{
				get
				{
					if (_Template == null)
					{
						string templateName = _ElementController.GetElementTemplate(_Connection, _WebID);
						_Template = AFElementTemplate.Find(_Connection, templateName);
					}

					return _Template;
				}
			}

			public AFElement Parent
			{
				get
				{
					if (_Parent == null)
					{
						string parentPath = _Path.Substring(0, _Path.LastIndexOf('\\'));
						_Parent = _ElementController.FindByPath(_Connection, parentPath);
					}

					return _Parent;
				}
			}

			public AFElements Elements
			{
				get
				{
                    if (_Elements == null)
                    {
                        _Elements = new AFElements(_ElementController.GetElements(_Connection, _WebID).ToList());
                        _Elements.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(ElementsChangedMethod);
                    }

					return _Elements;
				}
				set
				{
					_Elements = value;
					_IsDirty = true;
				}
			}

			public AFAttributes Attributes
			{
				get
				{
                    if (_Attributes == null)
                    {
                        _Attributes = new AFAttributes(_ElementController.GetAttributes(_Connection, _WebID).ToList());
                        _Attributes.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(AttributesChangedMethod);
                    }

					return _Attributes;
				}
				set
				{
					_Attributes = value;
					_IsDirty = true;
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
				_ElementController = GetController(_Connection);

				//Initialize Category List
			}

            private void ElementsChangedMethod(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    AFElement ele = (AFElement)sender;
                    ele.IsNew = true;
                    _IsDirty = true;
                }
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                {
                    AFElement ele = (AFElement)sender;
                    ele.Delete();
                    _IsDirty = true;
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

            private void AttributesChangedMethod(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    AFAttribute ele = (AFAttribute)sender;
                    ele.IsNew = true;
                    _IsDirty = true;
                }
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                {
                    AFAttribute ele = (AFAttribute)sender;
                    ele.Delete();
                    _IsDirty = true;
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

			private static IAFElementController GetController(Connection Connection)
			{
				IAFElementController result = null;

				if (Connection is WebAPI.WebAPIConnection)
				{
					result = new WebAPI.AFElementController();
				}

				return result;
			}
		#endregion

		#region "Interactions"
			public void CheckIn()
			{
				if (_IsDirty)
				{
					_ElementController.Update(_Connection, this);

					if (_Elements != null)
					{
						foreach (AFElement ele in _Elements.Where(x => x.IsNew))
						{
                            _ElementController.CreateChildElement(_Connection, _WebID, ele);
						}
					}

					if (_Attributes != null)
					{
						foreach (AFAttribute attr in _Attributes.Where(x => x.IsNew))
						{
                            AFAttribute.Create(_Connection, _WebID, attr);
						}
					}
				}

				ResetState();
			}

			public void Delete()
			{
                _IsDeleted = true;
				_ElementController.Delete(_Connection, _WebID);
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
			return GetController(Connection).Find(Connection, ID);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Path"></param>
		/// <returns></returns>
		public static AFElement FindByPath(Connection Connection, string Path)
		{
			return GetController(Connection).FindByPath(Connection, Path);
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
			return GetController(Connection).GetElements(Connection, RootID, "*", CategoryName, "*", ElementType.Any, false, "Name", "Ascending", 0, MaxCount);
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
			return GetController(Connection).GetElements(Connection, RootID, "*", "*", TemplateName, ElementType.Any, false, "Name", "Ascending", 0, MaxCount);
		}
		#endregion

		private void ResetState()
		{
			_IsNew = false;
			_IsDirty = false;
			_IsDeleted = false;
			_Elements = null;
			_Attributes = null;
			_Template = null;
			_Categories = null;
			_Parent = null;
		}
	}

}