using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace PIWebSharp.LazyObjects
{
	public class AFElement
	{
		private PIWebSharp.WebAPI.AFElement _Element;
		private AFElementTemplate _Template;
		private AFElement _Parent;
		private IEnumerable<AFElement> _Children;
		private IEnumerable<Attributes> _Attributes;
		private IAFElementLoader _ElementLoader;

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

				}
				set
				{

				}
			}

			public AFElement Parent
			{
				get
				{

				}
				set
				{

				}
			}

			public List<AFElement> Children
			{
				get
				{

				}
				set
				{

				}
			}

			public List<Attributes> Attributes
			{
				get
				{

				}
				set
				{

				}
			}
		#endregion


		#region "Constructors"

			public AFElement(string webID)
			{
				_ElementLoader.Find(webID);

				BuildReferences();
			}

			// Initialized all basic references
			private BuildReferences()
			{
				//Strips element name from path to get parent path
				string parentPath = Path.SubString(0, Path.LastIndexOf('\\');
				_Parent = _ElementLoader.FindByPath(parentPath);
			}
		#endregion

		#region "Interactions"
			public void CheckIn()
			{
				AFElements.Update(this)
			}

		#endregion
	}
}