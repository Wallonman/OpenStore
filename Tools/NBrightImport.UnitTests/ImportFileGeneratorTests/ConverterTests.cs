using NUnit.Framework;
using ZIndex.DNN.NBrightImport.Import;
using ZIndex.DNN.NBrightImport.Model.Store;

namespace ZIndex.DNN.NBrightImport.UnitTests.ImportFileGeneratorTests
{
    [TestFixture]
    public class ConverterTests : TestBase
    {
        private Converter _converter;
        private Product _product;
        private Store _store;

        [SetUp]
        public override void TestSetup()
        {
            base.TestSetup();

            _product = new Product
            {
                ImageFilename = "image.jpg",
                FullPath = @"c:\temp\test",
            };

            _store = new Store
            {
                StoreRootPath = @"c:\temp",
                ImageBasePath = @"c:\inetpub",
                StoreName = @"temp",
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
            Assert.AreEqual(@"c:\inetpub\image.jpg", _converter.ToImagePath(_product, _store));
//            Assert.AreEqual(@"c:\inetpub\temp\test\image.jpg", _converter.ToImagePath(_product, _store));
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