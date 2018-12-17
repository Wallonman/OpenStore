using NUnit.Framework;
using ZIndex.DNN.NBrightImport.Import;
using ZIndex.DNN.NBrightImport.Model.Store;

namespace ZIndex.DNN.NBrightImport.UnitTests.NBStoreConnect.ImportFileGeneratorTests
{
    [TestFixture]
    public class ConverterTests : TestBase
    {
        private Converter _converter;
        private Product _product;

        [SetUp]
        public override void TestSetup()
        {
            base.TestSetup();

            _product = new Product
            {
                ImageFilename = "image.jpg",
            };
            _converter = new Converter();
        }

        [TearDown]
        public override void TestTearDown()
        {
            base.TestTearDown();
        }

        [Test]
        public void ImagePathIsValid()
        {
            Assert.AreEqual(@"c:\temp\image.jpg", _converter.ToImagePath(_product, @"c:\temp"));
        }

        [Test]
        public void ImageBaseUrlIsValid()
        {
            Assert.AreEqual(@"/temp/image.jpg", _converter.ToImageBaseUrl(_product, @"/temp"));
        }

        [Test]
        public void ImageBaseUrlWithTrailingSlashIsValid()
        {
            Assert.AreEqual(@"/temp/image.jpg", _converter.ToImageBaseUrl(_product, @"/temp/"));
        }

        [Test]
        public void ImageBaseUrlWithoutSlashIsValid()
        {
            Assert.AreEqual(@"temp/image.jpg", _converter.ToImageBaseUrl(_product, @"temp"));
        }


    }
}