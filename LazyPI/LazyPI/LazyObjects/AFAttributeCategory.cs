using LazyPI.Common;

namespace LazyPI.LazyObjects
{
    public class AFAttributeCategory : CheckInAble
    {
        private IAFAttributeCategoryController _CategoryController;

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