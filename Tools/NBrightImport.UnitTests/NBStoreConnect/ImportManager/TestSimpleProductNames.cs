using System.Globalization;
using System.Linq;
using NUnit.Framework;
using ZIndex.DNN.NBrightImport.Extensions;
using ZIndex.DNN.NBrightImport.Import;

namespace ZIndex.DNN.NBrightImport.UnitTests.NBStoreConnect.ImportManager
{
    [TestFixture]
    public class TestSimpleProductNames : TestBase
    {
        private ISimpleProduct _simpleProduct;

        #region Setup/Teardown

        [SetUp]
        public override void TestSetup()
        {
            base.TestSetup();
            _simpleProduct = new SimpleProduct(@".\NBrightImport\Pictures", "category", "image.jpg", "/imageBaseUrl/", @"c:\imageBasePath\", 1, CultureInfo.GetCultureInfo("fr-BE"));
        }

        [TearDown]
        public override void TestTearDown()
        {
            base.TestTearDown();
        }

        #endregion

        [Test]
        public void TheListOfProductNamesContains2Products()
        {
            Assert.AreEqual(2, _simpleProduct.ProductNames.Count);
        }

        [Test]
        public void TheFirstProductNameIsValid()
        {
            Assert.AreEqual("100_1116", _simpleProduct.ProductNames.First());
        }

        [Test]
        public void TheCategoryImageUrlIsValid()
        {
            Assert.AreEqual("/imageBaseUrl/image..jpg", _simpleProduct.ToCategoryImageUrl());
        }

        [Test]
        public void TheProductImagePathIsValid()
        {
            Assert.AreEqual(@"c:\imageBasePath\image.jpg", _simpleProduct.ToProductImagePath("image"));
        }

        [Test]
        public void FilenameWithDotReturnsProductImagePathIsValid()
        {
            Assert.AreEqual(@"c:\imageBasePath\P.A.2017_BEB_3694.jpg", _simpleProduct.ToProductImagePath("P.A.2017_BEB_3694"));
        }

    }
}
