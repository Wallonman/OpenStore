using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ZIndex.DNN.NBrightImport.Model.Store;

namespace ZIndex.DNN.NBrightImport.UnitTests.NBStoreConnect.FolderParser
{
    [TestFixture]
    public class ProductsParserTests : TestBase
    {
        /*
        * Typical directory structure (with generated id's)
        * Root | 0.jpg (0)
        * Root | Path1 | 1.jpg (1)
        * Root | Path1 | Path11 | 111.jpg (2)
        * Root | Path1 | Path11 | 112.jpg (3)
        * Root | Path1 | Path12 | 121.jpg (4)
        * Root | Path1 | Path12 | 122.jpg (5)
        * Root | Path2 | 21.jpg (6)
        * Root | Path2 | 27.jpg (7)
        */



        private global::ZIndex.DNN.NBrightImport.Import.ProductsParser _parser;
        private List<Product> _actualProducts;
        private List<Category> _categories;

        #region Setup/Teardown

        [SetUp]
        public override void TestSetup()
        {
            base.TestSetup();

            _parser = new global::ZIndex.DNN.NBrightImport.Import.ProductsParser();
            _categories = new List<Category>
            {
                new Category {Name = "Root", Id = 0},
                new Category {Name = "Path1", Id = 1},
                new Category {Name = "Path11", Id = 11},
                new Category {Name = "Path12", Id = 12},
                new Category {Name = "Path2", Id = 2},
            };

            _actualProducts = _parser.Parse(StoreFiles, _categories);//Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"NBrightImport\FolderParser\Root"), _categories);
        }

        [TearDown]
        public override void TestTearDown()
        {
            base.TestTearDown();
        }

        #endregion

        [Test]
        public void ProductsCountIsValid()
        {
            Assert.AreEqual(8, _actualProducts.Count);
        }

        [Test]
        public void ProductRootIdIsValid()
        {
            Assert.AreEqual(1, _actualProducts.Single(category => category.Name == "0").Id);
        }

        [Test]
        public void Product111IdIsValid()
        {
            Assert.AreEqual(3, _actualProducts.Single(category => category.Name == "111").Id);
        }

        [Test]
        public void ProductIdsAreValid()
        {
            // ID's : 1+2+3+4+5+6+7+8 = 36
            Assert.AreEqual(36, _actualProducts.Sum(product => product.Id));
        }

        [Test]
        public void ProductNameIsValid()
        {
            Assert.AreEqual("111", _actualProducts.Single(category => category.Name == "111").Name);
        }

        [Test]
        public void ProductImageFilenameIsValid()
        {
            Assert.AreEqual("111.JPG", _actualProducts.Single(category => category.Name == "111").ImageFilename);
        }

        [Test]
        public void ProductCategoryIsNotNull()
        {
            Assert.IsNotNull(_actualProducts.Single(category => category.Name == "111").Category);
        }

        [Test]
        public void ProductCategoryIsValid()
        {
            Assert.AreEqual(11, _actualProducts.Single(category => category.Name == "111").Category.Id);
        }

    }
}