using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace LazyPI.LazyObjects
{
	public class AFElement : LazyPI.BaseObject
	{
		private AFElementTemplate _Template;
		private Lazy<AFElement> _Parent;
		private Lazy<IEnumerable<AFElement>> _Children;
		private Lazy<IEnumerable<AFAttribute>> _Attributes;
		private static IAFElement _ElementLoader;

		#region "Properties"
			public AFElementTemplate Template
			{
				get
				{
					return _Template;
				}
			}

			public AFElement Parent
			{
				get
				{
					return _Parent.Value;
				}
			}

			public IEnumerable<AFElement> Children
			{
				get
				{
					return _Children.Value; 
				}
			}

			public IEnumerable<AFAttribute> Attributes
			{
				get
				{
					return _Attributes.Value;
				}
			}
		#endregion


		#region "Constructors"
			private AFElement(string id, string name, string description, string path)
			{
				this._ID = id;
				this._Name = name;
				this._Description = description;
				this._Path = path;

				Initialize();
			}

			public AFElement(string ID)
			{
				BaseObject baseObj = _ElementLoader.Find(ID);
				this._ID = baseObj.ID;
				this._Name = baseObj.Name;
				this._Description = baseObj.Description;
				this._Path = baseObj.Path;

				Initialize();
			}

			/// <summary>
			/// Builds loader object and sets all callbacks for lazy loading 
			/// </summary>
			private void Initialize()
			{
				string parentPath = Path.Substring(0, Path.LastIndexOf('\\')); 

				//Load Parent
				_Parent = new Lazy<AFElement>(() =>
				{
					AFElement ele = _ElementLoader.FindByPath(parentPath);
					return ele;
				}, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

				//Load Attributes
				_Attributes = new Lazy<IEnumerable<AFAttribute>>(() => 
				{
					return _ElementLoader.GetAttributes(this.ID).Cast<AFAttribute>().ToList();
				}, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

				//Load Children
				_Children = new Lazy<IEnumerable<AFElement>>(() =>
				{
					return _ElementLoader.GetElements(this.ID);
				}, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);
			}
			// Initialized all basic references
			//Strips element name from path to get parent path

		#endregion

		#region "Interactions"
			public void CheckIn()
			{
				_ElementLoader.Update(this);
			}

		#endregion

		#region "Static Methods"
		/// <summary>
		/// Returns element requested
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public static AFElement Find(string ID)
		{
			return ElementFactory.CreateInstance(_ElementLoader.Find(ID));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Path"></param>
		/// <returns></returns>
		public static AFElement FindByPath(string Path)
		{
			return ElementFactory.CreateInstance(_ElementLoader.FindByPath(Path));
		}

		/// <summary>
		/// Removes specific element from AF Database
		/// </summary>
		/// <param name="ElementID">The ID of the element to be deleted</param>
		/// <returns></returns>
		public static bool Delete(string ElementID)
		{
			return _ElementLoader.Delete(ElementID);
		}

		//TODO: Implement Find Element By Category
		public static IEnumerable<AFElement> FindByCategory(string CategoryName)
		{
			throw new NotImplementedException();
		}

		//TODO: Implement Find Element By Template
		public static IEnumerable<AFElement> FindByTemplate(string TemplateName)
		{
			throw new NotImplementedException();
		}
		#endregion

		public class ElementFactory
		{
			public static AFElement CreateInstance(BaseObject bObj)
			{
				return new AFElement(bObj.ID, bObj.Name, bObj.Description, bObj.Path);
			}

			public static AFElement CreateInstance(string ID, string Name, string Description, string Path)
			{
				return new AFElement(ID, Name, Description, Path);
			}
		}
	}
}