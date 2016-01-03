using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace PIWebSharp.WebAPI
{
	public class AFElement : PIWebSharp.AFElement
	{
		private Lazy<AFElementTemplate> _Template;
		private Lazy<AFElement> _Parent;
		private Lazy<List<AFElement>> _Children;
		private Lazy<List<AFAttribute>> _Attributes;
		private IAFElementLoader _ElementLoader;

		#region "Properties"

			public string Name
			{
				get
				{
					return this._Name;
				}
				set
				{
					this._Name = value;
				}
			}

			public string Description
			{
				get
				{
					return this._Description;
				}
				set
				{
					this._Description = value;
				}
			}

			public string Path
			{
				get
				{
					return this._Path;
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

			public List<AFElement> Children
			{
				get
				{
					return _Children.Value; 
				}
			}

			public List<AFAttribute> Attributes
			{
				get
				{
					return _Attributes.Value;
				}
			}
		#endregion


		#region "Constructors"

			private AFElement(PIWebSharp.WebAPI.AFElement rawElement)
			{
				this._Name = rawElement.Name;
				this._Description = rawElement.Description;
				this._ID = rawElement.ID;

				Initialize();
			}

			private void Initialize()
			{
				string parentPath = Path.Substring(0, Path.LastIndexOf('\\'));

				//Load Parent
				_Parent = new Lazy<AFElement>(() =>
				{
					var ele = (PIWebSharp.WebAPI.AFElement)_ElementLoader.FindByPath(parentPath);
					return ele;
				}, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

				_Attributes = new Lazy<List<AFAttribute>>(() => 
				{
					List<PIWebSharp.WebAPI.AFAttribute> attributeList = _ElementLoader.GetAttributes(this.ID);
					
					return attributeList;

				}, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

				_Children = new Lazy<List<AFElement>>(() =>
				{

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