using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace PIWebSharp.LazyObjects
{
	public class AFElement : PIWebSharp.AFElement
	{
		private PIWebSharp.WebAPI.AFElement _Element;
		private Lazy<AFElementTemplate> _Template;
		private Lazy<AFElement> _Parent;
		private Lazy<List<AFElement>> _Children;
		private Lazy<List<AFAttribute>> _Attributes;
		private IAFElementLoader _Loader;

		#region "Properties"

			public string Name
			{
				get
				{
					return _Element.Name;
				}
				set
				{
					_Element.Name = value;
				}
			}

			public string Description
			{
				get
				{
					return _Element.Description;
				}
				set
				{
					_Element.Description = value;
				}
			}

			public string Path
			{
				get
				{
					return _Element.Path;
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
			public AFElement()
			{

			}

			public AFElement(PIWebSharp.WebAPI.AFElement rawElement)
			{
				_Element = rawElement;
			}

			public AFElement(string webID)
			{
				_Element = _Loader.Find(webID);




			}

			private void InitializeLoaders()
			{
				string parentPath = Path.Substring(0, Path.LastIndexOf('\\'));

				//Load Parent
				_Parent = new Lazy<AFElement>(() =>
				{
					var ele = _Loader.FindByPath(parentPath);
					AFElement element = new AFElement(ele);
					return element;
				}, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);


			}
			// Initialized all basic references
			//Strips element name from path to get parent path

		#endregion

		#region "Interactions"
			public void CheckIn()
			{
				_Loader.Update(this);
			}

		#endregion
	}
}