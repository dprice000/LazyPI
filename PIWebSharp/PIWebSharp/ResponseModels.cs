using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIWebSharp
{
    /// <summary>
    /// The possible values for the type of an element
    /// </summary>
    public enum ElementType
    {
        None,
        Other,
        Node,
        Measurement,
        Flow,
        Transfer,
        Boundry,
        PIPoint,
        Any
    }

    public class LinksResponse
    {
        public string Self { get; set; }
    }

    public class HomeResponse
    {
        public string Self { get; set; }
        public string AssetServers { get; set; }
        public string DataServers { get; set; }
        public string System { get; set; }
    }

    public class AFServer
    {
        public string WebID { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public bool IsConnected { get; set; }
        public string ServerVersion { get; set; }
        public LinksResponse Links { get; set; }
    }

    public class AFDB
    {
        public string WebID { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public LinksResponse Links { get; set; }
    }

    public class UnitClass
    {
        public string WebID { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CanonicalUnitName { get; set; }
        public string CanonicalUnitAbbreviation { get; set; }
        public string Path { get; set; }
        public LinksResponse Links { get; set; }
    }

    public class AttributeCategory
    {
        public string WebID { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public LinksResponse Links { get; set; }
    }

    public class ElementCategory
    {
        public string WebID { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public LinksResponse Links { get; set; }
    }

    public class AFElement
    {
        public string WebID { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public string TemplateName { get; set; }
        public List<string> CategoryNames { get; set; }
        public LinksResponse Links { get; set; } 
    }

    public class AFElementList
    {
        public LinksResponse Links { get; set; }
        public List<AFElement> Items { get; set; }
    }

    public class ElementCategoryList
    {
        public LinksResponse Links { get; set; }
        public List<ElementCategory> Items { get; set; }
    }

    public class AttributeCategoryList
    {
        public LinksResponse Links { get; set; }
        public List<AttributeCategory> List { get; set; }
    }

    public class UnitClassList
    {
        public LinksResponse Links { get; set; }
        public List<UnitClass> Items { get; set; }
    }

    public class AFDBList
    {
        public LinksResponse Links { get; set; }
        public List<AFDB> Items { get; set; } 
    }

    public class AFServerList
    {
        public LinksResponse Links { get; set; }
        public List<AFServer> Items { get; set; }
    }
}
