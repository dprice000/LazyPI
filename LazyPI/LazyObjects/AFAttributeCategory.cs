using LazyPI.Common;

namespace LazyPI.LazyObjects
{
    public class AFAttributeCategory : CheckInAble
    {
        private IAFAttributeCategoryController _CategoryController;

        /// <summary>
        /// Only for testing purposes.
        /// </summary>
        internal IAFAttributeCategoryController CategoryController
        {
            get
            {
                return _CategoryController;
            }
            set
            {
                _CategoryController = value;
            }
        }


        private void Initialize()
        {
        }

        public override void CheckIn()
        {
            if (IsDirty && !IsDeleted)
            {
                _CategoryController.Update(_Connection, this);
                ResetState();
            }
        }

        protected override void ResetState()
        {
            IsNew = false;
            IsDirty = false;
        }
    }
}