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
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }

        public void CheckIn()
        {
            _CategoryLoader.Update(_Connection, this);
        }
    }
}
