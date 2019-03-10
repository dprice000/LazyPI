using LazyObjects = LazyPI.LazyObjects;
using LazyPI.WebAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResponseModels = LazyPI.WebAPI.ResponseModels;
using System.Linq;

namespace LazyPI_Test.WebAPI
{
    [TestClass]
    public class DataConversionTests
    {
        private static FixtureWrapper fixture;

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            fixture = new FixtureWrapper();
        }

        [TestMethod]
        public void ConvertElement()
        {
            var lazyElement = fixture.Create<LazyObjects.AFElement>();

            var responseElement = DataConversions.Convert(lazyElement);

            BaseAsserts(lazyElement, responseElement);
            Assert.IsNotNull(lazyElement.Template);
            Assert.AreEqual(lazyElement.Template?.Name, responseElement.TemplateName);
            Assert.IsNotNull(lazyElement.Categories);
            Assert.AreEqual(lazyElement.Categories, responseElement.CategoryNames.AsEnumerable());
        }

        [TestMethod]
        public void ConvertAttribute()
        {
            var lazyAttr = fixture.Create<LazyObjects.AFAttribute>();

            var responseAttr = DataConversions.Convert(lazyAttr);

            BaseAsserts(lazyAttr, responseAttr);
            Assert.AreEqual(lazyAttr.ConfigString, responseAttr.ConfigString);
            Assert.AreEqual(lazyAttr.DataReferencePlugIn, responseAttr.DataReferencePlugIn);
            Assert.AreEqual(lazyAttr.Type, responseAttr.Type);
            Assert.IsNotNull(lazyAttr.Categories);
            Assert.AreEqual(lazyAttr.Categories, responseAttr.CategoryNames.AsEnumerable());
        }

        [TestMethod]
        public void ConvertEventFrame()
        {
            var lazyFrame = fixture.Create<LazyObjects.AFEventFrame>();

            var responseFrame = DataConversions.Convert(lazyFrame);

            BaseAsserts(lazyFrame, responseFrame);
            Assert.AreEqual(lazyFrame.StartTime, responseFrame.StartTime);
            Assert.AreEqual(lazyFrame.EndTime, responseFrame.EndTime);
            Assert.IsNotNull(lazyFrame.Template);
            Assert.AreEqual(lazyFrame.Template.Name, responseFrame.TemplateName);
            Assert.IsNotNull(lazyFrame.CategoryNames);
            Assert.AreEqual(lazyFrame.CategoryNames, responseFrame.CategoryNames.AsEnumerable());
        }

        [TestMethod]
        public void ConvertDatabase()
        {
            var lazyDB = fixture.Create<LazyObjects.AFDatabase>();

            var responseDB = DataConversions.Convert(lazyDB);

            BaseAsserts(lazyDB, responseDB);
        }

        [TestMethod]
        public void ConvertUnit()
        {
            var lazyUnit = fixture.Create<LazyObjects.AFUnit>();

            var responseUnit = DataConversions.Convert(lazyUnit);

            BaseAsserts(lazyUnit, responseUnit);
            Assert.AreEqual(lazyUnit.Abbreviation, responseUnit.CanonicalUnitAbbreviation);
        }

        [TestMethod]
        public void ConverPoint()
        {
            var lazyPoint = fixture.Create<LazyObjects.PIPoint>();

            var responsePoint = DataConversions.Convert(lazyPoint);

            BaseAsserts(lazyPoint, responsePoint);
            Assert.AreEqual(lazyPoint.PointClass, responsePoint.PointClass);
            Assert.AreEqual(lazyPoint.PointType, responsePoint.PointType);
            Assert.AreEqual(lazyPoint.Future, responsePoint.Future);
        }

        private void BaseAsserts(LazyPI.Common.BaseObject lazyObject, ResponseModels.BaseResponse responseObject)
        {
            Assert.AreEqual(lazyObject.WebID, responseObject.WebId);
            Assert.AreEqual(lazyObject.ID, responseObject.Id);
            Assert.AreEqual(lazyObject.Name, responseObject.Name);
            Assert.AreEqual(lazyObject.Description, responseObject.Description);
            Assert.AreEqual(lazyObject.Path, responseObject.Path);
        }
    }
}
