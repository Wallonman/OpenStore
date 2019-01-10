using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using ZIndex.DNN.NBrightImport.Import;
using ZIndex.DNN.NBrightImport.Model.Store;

namespace ZIndex.DNN.NBrightImport.UnitTests.FolderParser
{
    [TestFixture]
    public class McpaquotStoreParserTests : TestBase
    {
        private global::ZIndex.DNN.NBrightImport.Import.ProductsParser _productParser;
        private List<Product> _actualProducts;
        private List<Category> _actualCategories;
        private CategoriesParser _categoriesParser;

        #region Setup/Teardown

        [SetUp]
        public override void TestSetup()
        {
            base.TestSetup();

            StoreFiles = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"StoreSamples\Dance's Passion 2017");

            _productParser = new ProductsParser();
            _categoriesParser = new CategoriesParser();

            _actualCategories = _categoriesParser.Parse(StoreFiles);
            _actualProducts = _productParser.Parse(StoreFiles, _actualCategories);//Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"FolderParser\Root"), _categories);
        }

        [TearDown]
        public override void TestTearDown()
        {
            base.TestTearDown();
        }

        #endregion

        [Test]
        public void CategoriesCountIsValid()
        {
            Assert.AreEqual(5, _actualCategories.Count);
        }

        [Test]
        public void CategoryRootNameIsValid()
        {
            Assert.AreEqual("Dance\'s Passion 2017", _actualCategories.Single(category => category.Id == 1).Name);
        }

        [Test]
        public void ProductsCountIsValid()
        {
            Assert.AreEqual(10, _actualProducts.Count);
        }

        [Test]
        public void Product1IdIsValid()
        {
            Assert.AreEqual(11, _actualProducts.Single(category => category.Name == "DP 2017_BEB_4083").Id);
        }

        [Test]
        public void ProductNameIsValid()
        {
            Assert.IsTrue(_actualProducts.Any(category => category.Name == "DP 2017_BEB_4083"));
        }

        [Test]
        public void ProductImageFilenameIsValid()
        {
            Assert.AreEqual("DP 2017_BEB_4083.JPG", _actualProducts.Single(category => category.Name == "DP 2017_BEB_4083").ImageFilename);
        }

        [Test]
        public void ProductCategoryIsNotNull()
        {
            Assert.IsNotNull(_actualProducts.Single(category => category.Name == "DP 2017_BEB_4083").Category);
        }

        [Test]
        public void ProductCategoryIsValid()
        {
            Assert.AreEqual(7, _actualProducts.Single(category => category.Name == "DP 2017_BEB_4083").Category.Id);
        }

    }
}