using LazyPI.Common;

namespace LazyPI.LazyObjects
{
    public class AFUnit : CheckInAble
    {
        private string _Abbreviation;
        private double _Factor;
        private double _Offset;
        private double _ReferenceFactor;
        private double _ReferenceOffset;
        private string _ReferenceUnitAbbreviation;
        private static IAFUnitController _UnitController;

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

        #endregion "Properties"

        #region "Constructors"

        internal AFUnit(Connection Connection, string WebID, string ID, string Name, string Description, string Path) : base(Connection, WebID, ID, Name, Description, Path)
        {
            Initialize();
        }

        private void Initialize()
        {
        }

        private static IAFUnitController GetController(Connection Connection)
        {
            IAFUnitController result = null;

            if(Connection is WebAPI.WebAPIConnection)
            {
                throw new System.NotImplementedException("AFUnitController has not been implemented for WebAPI.");
            }

            return result;
        }

        #endregion "Constructors"

        #region "Static Methods"

        public AFUnit Find(Connection Connection, string ID)
        {
            BaseObject baseObj = _UnitController.Find(Connection, ID);
            return new AFUnit(Connection, baseObj.WebID, baseObj.ID, baseObj.Name, baseObj.Description, baseObj.Path);
        }

        public AFUnit FindByPath(Connection Connection, string Path)
        {
            BaseObject baseObj = _UnitController.FindByPath(Connection, Path);
            return new AFUnit(Connection, baseObj.WebID, baseObj.ID, baseObj.Name, baseObj.Description, baseObj.Path);
        }

        public static bool Delete(Connection Connection, string ID)
        {
            return GetController(Connection).Delete(Connection, ID);
        }

        #endregion "Static Methods"

        #region "Interactions"

        public override void CheckIn()
        {
            if (IsDirty && !IsDeleted)
            {
                _UnitController.Update(_Connection, this);

                ResetState();
            }
        }

        protected override void ResetState()
        {
            IsNew = false;
            IsDirty = false;
        }

        #endregion "Interactions"
    }
}