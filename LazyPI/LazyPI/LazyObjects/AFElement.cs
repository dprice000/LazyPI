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
		private IAFElement _ElementLoader;

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
			private AFElement()
			{
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
	}
}