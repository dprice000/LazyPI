using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.LazyObjects
{
    public class AFUnit : BaseObject
    {
        private string _Abbreviation;
        private double _Factor;
        private double _Offset;
        private double _ReferenceFactor;
        private double _ReferenceOffset;
        private string _ReferenceUnitAbbreviation;
        private static IAFUnit _UnitLoader;

        #region "Properties"

        public string Abbreviation
        {
            get
            {
                return _Abbreviation;
            }
            //TODO: Implement Setter
        }

        public double Factor
        {
            get
            {
                return _Factor;
            }
            //TODO: Implement Setter
        }

        public double Offset
        {
            get
            {
                return _Offset;
            }
            //TODO: Implement Setter
        }

        public double ReferenceFactor
        {
            get
            {
                return _ReferenceFactor;
            }
            //TODO: Implment Setter
        }

        public double ReferenceOffset
        {
            get
            {
                return _ReferenceOffset;
            }
            //TODO: Implement Setter
        }

        public string ReferenceUnitAbbreviation
        {
            get
            {
                return _ReferenceUnitAbbreviation;
            }
            //TODO: Implement Setter
        }
        #endregion

        #region "Constructors"
        private AFUnit(string ID, string Name, string Description, string Path) : base(ID, Name, Description, Path)
        {
            Initialize();
        }

        private void Initialize()
        {
        }
        #endregion

        #region "Static Methods"
        public static bool Delete(string ID)
        {
           return _UnitLoader.Delete(ID);
        }
        #endregion
    }
}
