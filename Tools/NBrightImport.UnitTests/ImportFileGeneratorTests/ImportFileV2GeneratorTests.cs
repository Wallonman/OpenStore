using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Moq;
using NUnit.Framework;
using ZIndex.DNN.NBrightImport.Import;
using ZIndex.DNN.NBrightImport.Model.Store;

namespace ZIndex.DNN.NBrightImport.UnitTests.ImportFileGeneratorTests
{
    [TestFixture]
    public class ImportFileV2GeneratorTests : TestBase
    {
        private XDocument _actual;
        private ImportV2FileGenerator _generator;
        private Store _store;
        private List<Category> _categories;
        private Category _rootCategory;
        private Category _childCategory;
        private XElement _firstProduct;
        private Mock<IConverter> _converter;

        #region Setup/Teardown

        [SetUp]
        public override void TestSetup()
        {
            base.TestSetup();

            //prepare the store
            _rootCategory = new Category {Id = 100, Name = "root"};
            _childCategory = new Category {Id = 101, Name = "child", Parent = _rootCategory};
            _categories = new List<Category>
            {
                _rootCategory,
                _childCategory,
            };

            _store = new Store
            {
                Products = new List<Product>
                {
                    new Product{Id = 1, ImageFilename = "1.jpg", Name = "prod1", Category = _rootCategory},
                    new Product{Id = 2, ImageFilename = "2.jpg", Name = "prod2", Category = _childCategory},
                },
                Categories = _categories,
                Culture = new CultureInfo("fr-BE"),
                ImageBasePath = "c:\\temp",
                ImageBaseUrl = "/url/",
                ProductUnitCost = 10,
            };

            // mock the converter
            _converter = new Mock<IConverter>();
            _converter.Setup(converter => converter.ToImagePath(It.IsAny<Product>(), It.IsAny<string>()))
                .Returns(@"c:\temp\image.jpg");
            _converter.Setup(converter => converter.ToImageBaseUrl(It.IsAny<Product>(), It.IsAny<string>()))
                .Returns("/url/image.jpg");

            TextWriter writer = new StringWriter();

            // generate using the generator
            _generator = new ImportV2FileGenerator(_converter.Object);
            _generator.Generate(writer, _store);

            // load the actual result
            _actual = XDocument.Load(new StringReader(writer.ToString()));

            Assert.IsNotNull(_actual);
            Logger.Debug(_actual.ToString());

            _firstProduct = _actual.Descendants("NB_Store_ProductsInfo").FirstOrDefault();

        }

        [TearDown]
        public override void TestTearDown()
        {
            base.TestTearDown();
        }

        #endregion

        [Test]
        public void ImportRootIsValid()
        {
            Assert.AreEqual("root", _actual.Root.Name.LocalName);
        }

        [Test]
        public void ImportCategoriesCountIsValid()
        {
            Assert.AreEqual(2, _actual.Descendants("NB_Store_CategoriesInfo").Count());
        }

        [Test]
        public void ImportRootCategoryNameIsValid()
        {
            Assert.AreEqual("root", _actual.Descendants("NB_Store_CategoriesInfo").First().Element("CategoryName").Value);
        }

        [Test]
        public void ImportRootCategoryHasNoParent()
        {
            Assert.AreEqual("0", _actual.Descendants("NB_Store_CategoriesInfo").First().Element("ParentCategoryID").Value);
        }

        [Test]
        public void ImportChildCategoryParentIsValid()
        {
            Assert.AreEqual("100", _actual.Descendants("NB_Store_CategoriesInfo").Last().Element("ParentCategoryID").Value);
        }

/*
        [Test]
        public void ImportCategoryImageURLIsValid()
        {
            Assert.AreEqual("/ImageBaseUrl/CategoryImage..jpg", _actual.Descendants("NB_Store_CategoriesInfo").First().Element("ImageURL").Value);
        }
*/

        [Test]
        public void ImportContainsTwoProducts()
        {
            Assert.AreEqual(2, _actual.Descendants("NB_Store_ProductsInfo").Count());
        }

        [Test]
        public void ImportProductExists()
        {
            Assert.IsNotNull(_firstProduct);
        }

        [Test]
        public void ImportProductNameIsValid()
        {
            Assert.AreEqual("prod1", _firstProduct.Element("ProductName").Value);
        }

        [Test]
        public void ImportProductLangIsValid()
        {
            Assert.AreEqual("fr-BE", _firstProduct.Element("Lang").Value);
        }

        [Test]
        public void ImportProductIsDeletedIsFalse()
        {
            Assert.AreEqual("false", _firstProduct.Element("IsDeleted").Value);
        }

        [Test]
        public void ImportProductIsHiddenIsFalse()
        {
            Assert.AreEqual("false", _firstProduct.Element("IsHidden").Value);
        }

        [Test]
        public void ImportProductModelHasValidId()
        {
            Assert.AreEqual("103", _actual.Descendants("ModelID").Min(element => element.Value));
        }

        [Test]
        public void ImportProductModelHasValidProductId()
        {
            Assert.AreEqual("1", _actual.Descendants("NB_Store_ModelInfo").First().Element("ProductID").Value);
        }

        [Test]
        public void ImportProductModelHasValidUnitCost()
        {
            Assert.AreEqual("10", _actual.Descendants("NB_Store_ModelInfo").First().Element("UnitCost").Value);
        }

        [Test]
        public void ImportProductModelHasValidModelRef()
        {
            Assert.AreEqual("prod1", _actual.Descendants("NB_Store_ModelInfo").First().Element("ModelRef").Value);
        }

        [Test]
        public void ImportProductModelHasValidLang()
        {
            Assert.AreEqual("fr-BE", _actual.Descendants("NB_Store_ModelInfo").First().Element("Lang").Value);
        }

        [Test]
        public void ImportProductImageHasValidProductID()
        {
            Assert.AreEqual("1", _actual.Descendants("NB_Store_ProductImageInfo").First().Element("ProductID").Value);
        }

        [Test]
        public void ImportProductImageHasValidImagePath()
        {
            Assert.AreEqual(@"c:\temp\image.jpg", _actual.Descendants("NB_Store_ProductImageInfo").First().Element("ImagePath").Value);
        }

        [Test]
        public void ImportProductImageHasValidLang()
        {
            Assert.AreEqual("fr-BE", _actual.Descendants("NB_Store_ProductImageInfo").First().Element("Lang").Value);
        }

        [Test]
        public void ImportProductImageHasValidImageURL()
        {
            Assert.AreEqual("/url/image.jpg", _actual.Descendants("NB_Store_ProductImageInfo").First().Element("ImageURL").Value);
        }

        [Test]
        public void ImportProductCategoryHasValidProductID()
        {
            Assert.AreEqual("1", _firstProduct.Element("ProductID").Value);
        }

        [Test]
        public void ImportProductCategoryHasValidCategoryID()
        {
            Assert.AreEqual("100", _actual.Descendants("P").FirstOrDefault().Descendants("CategoryID").First().Value);
        }


        [Test]
        public void ImportCultureCountIsValid()
        {
            var langs = _actual.Descendants("Lang");
            Assert.AreEqual(8, langs.Count()); // 2 products with 3 lang elements + 2 categories

        }

        [Test]
        public void ImportCulturesAreValid()
        {
            var langs = _actual.Descendants("Lang");
            // all Lang element must be fr-BE
            langs.ToList().ForEach(element => Assert.AreEqual("fr-BE", element.Value));
        }


    }
}