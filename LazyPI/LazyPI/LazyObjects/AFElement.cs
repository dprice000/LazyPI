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

			private AFElement(LazyPI.WebAPI.ResponseModels.AFElement responseEle)
			{

				Initialize(responseEle.TemplateName);
			}

			//private AFElement(PIWebSharp.WebAPI.AFElement rawElement)
			//{
			//    this._Name = rawElement.Name;
			//    this._Description = rawElement.Description;
			//    this._ID = rawElement.ID;

			//    Initialize(rawElement.Template.Name);
			//}   

			private void Initialize(string templateName)
			{
				string parentPath = Path.Substring(0, Path.LastIndexOf('\\'));

				//Load Template 
				

				//Load Parent
				_Parent = new Lazy<AFElement>(() =>
				{
					AFElement ele = _ElementLoader.FindByPath(parentPath);
					return ele;
				}, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

				_Attributes = new Lazy<IEnumerable<AFAttribute>>(() => 
				{
					return _ElementLoader.GetAttributes(this.ID).Cast<AFAttribute>().ToList();
				}, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

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