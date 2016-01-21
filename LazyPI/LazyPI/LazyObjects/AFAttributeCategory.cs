using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public class AFAttributeCategory : BaseObject
    {
        private IAFAttributeCategory _CategoryLoader;

        private void Initialize()
        { 
        }

        public override string Name
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

        public override string Description
        {
        }

        public void CheckIn()
        {
            _CategoryLoader.Update(this);
        }
    }
}
