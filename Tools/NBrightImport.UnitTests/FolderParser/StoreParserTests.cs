using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Moq;
using NUnit.Framework;
using ZIndex.DNN.NBrightImport.Import;
using ZIndex.DNN.NBrightImport.Model.Store;

namespace ZIndex.DNN.NBrightImport.UnitTests.FolderParser
{
    [TestFixture]
    public class StoreParserTests : TestBase
    {
        [SetUp]
        public override void TestSetup()
        {
            base.TestSetup();

            _mockCategoriesParser = new Mock<ICategoriesParser>();
            _mockCategoriesParser.Setup(parser => parser.Parse(It.Is<string>(s => s == "root")))
                .Returns(new List<Category> {new Category {Name = "Root", Id = 0}});
            _mockProductParser = new Mock<IProductsParser>();
            _mockProductParser.Setup(parser => parser.Parse(It.Is<string>(s => s == "root"),
                    It.Is<List<Category>>(list => list.Any(item => item.Id == 0) && list.Count == 1)))
                .Returns(new List<Product> {new Product {Id = 1}});

            _storeParser = new StoreParser(_mockCategoriesParser.Object, _mockProductParser.Object);
            _actualStore = _storeParser.Parse("root", CultureInfo.InvariantCulture, "imageBasePath", "imageBaseUrl", 100);
        }

        [TearDown]
        public override void TestTearDown()
        {
            base.TestTearDown();
        }

        private Mock<ICategoriesParser> _mockCategoriesParser;
        private Mock<IProductsParser> _mockProductParser;
        private StoreParser _storeParser;
        private Store _actualStore;

        [Test]
        public void ProductsCountIsValid()
        {
            Assert.AreEqual(1, _actualStore.Products.Count);
        }

        [Test]
        public void CategoriesCountIsValid()
        {
            Assert.AreEqual(1, _actualStore.Categories.Count);
        }

        [Test]
        public void CultureIsValid()
        {
            Assert.AreEqual(CultureInfo.InvariantCulture, _actualStore.Culture);
        }

        [Test]
        public void ImageBasePathIsValid()
        {
            Assert.AreEqual("imageBasePath", _actualStore.ImageBasePath);
        }

        [Test]
        public void ImageBaseUrlIsValid()
        {
            Assert.AreEqual("imageBaseUrl", _actualStore.ImageBaseUrl);
        }

        [Test]
        public void ProductUnitCostIsValid()
        {
            Assert.AreEqual(100, _actualStore.ProductUnitCost);
        }

    }
}