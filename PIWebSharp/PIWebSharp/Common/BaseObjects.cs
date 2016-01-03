using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIWebSharp
{
    abstract class BaseObject
    {
        protected string _ID;
        protected string _Name;
        protected string _Path;
        protected string _Description;

        public abstract string ID
        {
            get;
        }

        public abstract string Name
        {
            get;
            set;
        }

        public abstract string Path
        {
            get;
        }

        public abstract string Description
        {
            get;
            set;
        }
    }

    abstract class AFDB : BaseObject
    {
    }

    abstract class AFServer : BaseObject
    {
    }

    abstract class AFAttribute : BaseObject
    {
    }

    abstract class AFElement : BaseObject
    {
    }


    abstract class AFEventFrame : BaseObject
    {
    }

    abstract class AFAttributeTemplate : BaseObject
    {
    }

    abstract class AFElementTemplate : BaseObject
    {
    }

    abstract class AttributeCategory : BaseObject
    {
    }

    abstract class ElementCategory : BaseObject
    {
    }

    abstract class AFValue : BaseObject
    {
    }

    abstract class DataPoint : BaseObject
    {
    }

    abstract class AFEnumerationSet : BaseObject
    {
    }

    abstract class AFTable : BaseObject
    {
    }

    abstract class AFTableCategory : BaseObject
    {
    }
}
