using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public static class AFElements
    {
        private IAFElement _ElementLoader;
 
        /// <summary>
        /// Returns element requested
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public AFElement Find(string ID)
        {
            return _ElementLoader.Find(ID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public AFElement Find(string Path)
        {
            return _ElementLoader.FindByPath(Path);
        }

        /// <summary>
        /// Removes specific element from AF Database
        /// </summary>
        /// <param name="ElementID">The ID of the element to be deleted</param>
        /// <returns></returns>
        public bool Delete(string ElementID)
        {
            return _ElementLoader.Delete(ElementID);
        }

        //TODO: Implement Find Element By Category
        public IEnumerable<AFElement> FindByCategory(string CategoryName)
        {
            throw new NotImplementedException();
        }

        //TODO: Implement Find Element By Template
        public IEnumerable<AFElement> FindByTemplate(string TemplateName)
        {
            throw new NotImplementedException();
        }
    }
}
