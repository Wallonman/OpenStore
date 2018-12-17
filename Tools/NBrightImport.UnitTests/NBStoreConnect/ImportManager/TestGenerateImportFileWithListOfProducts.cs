/*using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Moq;
using NBrightImport.Import;
using NUnit.Framework;

namespace McPaquot.UnitTests.NBrightImport.ImportManager
{
    [TestFixture]
    public class TestGenerateImportFileWithListOfProducts : TestBase
    {
        private List<ISimpleProduct> _simpleProduct;
        private global::ZIndex.DNN.NBrightImport.Import.ImportManagerOld _importManager;
        private XDocument _actual;

        #region Setup/Teardown

        [SetUp]
        public override void TestSetup()
        {
            base.TestSetup();

            var p1 = new Mock<ISimpleProduct>();
            p1.Setup(p => p.ProductNames).Returns(new List<string> { "TestPicture11", "TestPicture12" });
            p1.Setup(p => p.ProductsImageExtension).Returns("ProductsImageExtension");
            p1.Setup(p => p.CategoryName).Returns("CategoryName1");
            p1.Setup(p => p.ImageBaseUrl).Returns("/ImageBaseUrl/");
            p1.Setup(p => p.ImageBasePath).Returns(@"ImageBasePath");
            p1.Setup(p => p.CategoryImage).Returns("CategoryImage.jpg");
            p1.Setup(p => p.UnitCost).Returns(111);
            p1.Setup(p => p.Culture).Returns(CultureInfo.GetCultureInfo("fr-BE"));

            var p2 = new Mock<ISimpleProduct>();
            p2.Setup(p => p.ProductNames).Returns(new List<string> { "TestPicture21", "TestPicture22" });
            p2.Setup(p => p.ProductsImageExtension).Returns("ProductsImageExtension");
            p2.Setup(p => p.CategoryName).Returns("CategoryName1"); // same cat as p1
            p2.Setup(p => p.ImageBaseUrl).Returns("/ImageBaseUrl/");
            p2.Setup(p => p.ImageBasePath).Returns(@"ImageBasePath");
            p2.Setup(p => p.CategoryImage).Returns("CategoryImage.jpg");
            p2.Setup(p => p.UnitCost).Returns(111);
            p2.Setup(p => p.Culture).Returns(CultureInfo.GetCultureInfo("fr-BE"));

            var p3 = new Mock<ISimpleProduct>();
            p3.Setup(p => p.ProductNames).Returns(new List<string> { "TestPicture31", "TestPicture32" });
            p3.Setup(p => p.ProductsImageExtension).Returns("ProductsImageExtension");
            p3.Setup(p => p.CategoryName).Returns("CategoryName3");
            p3.Setup(p => p.ImageBaseUrl).Returns("/ImageBaseUrl/");
            p3.Setup(p => p.ImageBasePath).Returns(@"ImageBasePath");
            p3.Setup(p => p.CategoryImage).Returns("CategoryImage.jpg");
            p3.Setup(p => p.UnitCost).Returns(111);
            p3.Setup(p => p.Culture).Returns(CultureInfo.GetCultureInfo("fr-BE"));
            

            _simpleProduct = new List<ISimpleProduct>()
            {
                p1.Object,
                p2.Object,
                p3.Object,
            };

            _importManager = global::ZIndex.DNN.NBrightImport.Import.ImportManagerOld.CreateInstance();

            TextWriter writer = new StringWriter();

            _importManager.GenerateImportFile(writer, _simpleProduct);

            _actual = XDocument.Load(new StringReader(writer.ToString()));

            Logger.Debug(_actual.ToString());
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
        public void ImportContainsProductElement()
        {
            Assert.AreEqual(1, _actual.Descendants("products").Count());
        }

        [Test]
        public void ImportContainsCategoriesElement()
        {
            Assert.AreEqual(1, _actual.Descendants("categories").Count());
        }

        [Test]
        public void ImportContainsCultureElement()
        {
            // 2 culture elements, 1 under products and 1 under categories
            Assert.AreEqual(2, _actual.Descendants("fr-BE").Count());
        }

        [Test]
        public void ImportContainsTwoCategories()
        {
            Assert.AreEqual(2, _actual.Descendants("NB_Store_CategoriesInfo").Count());
        }

        [Test]
        public void ImportCategoryNameIsValid()
        {
            Assert.AreEqual("CategoryName1", _actual.Descendants("NB_Store_CategoriesInfo").First().Element("CategoryName").Value);
            Assert.AreEqual("CategoryName2", _actual.Descendants("NB_Store_CategoriesInfo").Last().Element("CategoryName").Value);
        }

        [Test]
        public void ImportCategoryImageURLIsValid()
        {
            Assert.AreEqual("/ImageBaseUrl/CategoryImage..jpg", _actual.Descendants("NB_Store_CategoriesInfo").First().Element("ImageURL").Value);
        }

        [Test]
        public void ImportContainsProducts()
        {
            Assert.AreEqual(6, _actual.Descendants("NB_Store_ProductsInfo").Count());
        }

        [Test]
        public void ImportProductIsValid()
        {
            Assert.AreEqual("TestPicture1", _actual.Descendants("NB_Store_ProductsInfo").First().Element("ProductRef").Value);
            Assert.AreEqual("TestPicture1", _actual.Descendants("NB_Store_ProductsInfo").First().Element("ProductName").Value);
        }

        [Test]
        public void ImportProductHasValidId()
        {
            Assert.AreEqual("0", _actual.Descendants("NB_Store_ProductsInfo").First().Element("ProductID").Value);
            Assert.AreEqual("3", _actual.Descendants("NB_Store_ProductsInfo").Last().Element("ProductID").Value);
            Assert.AreEqual("3", _actual.Descendants("NB_Store_ModelInfo").Last().Element("ProductID").Value);
            Assert.AreEqual("3", _actual.Descendants("NB_Store_ProductImageInfo").Last().Element("ProductID").Value);
            Assert.AreEqual("3", _actual.Descendants("NB_Store_ProductCategoryInfo").Last().Element("ProductID").Value);
        }

        [Test]
        public void ImportProductModelHasValidId()
        {
            Assert.AreEqual("1", _actual.Descendants("NB_Store_ModelInfo").First().Element("ModelID").Value);
        }

        [Test]
        public void ImportProductImageHasValidId()
        {
            Assert.AreEqual("2", _actual.Descendants("NB_Store_ProductImageInfo").First().Element("ImageID").Value);
        }

        [Test]
        public void ImportCultureIsValid()
        {
            var langs = _actual.Descendants("Lang");
            Assert.AreEqual(7, langs.Count()); // 2 products with 3 lang elements + 1 category

            // all Lang element must be fr-BE
            langs.ToList().ForEach(element => Assert.AreEqual("fr-BE", element.Value));
        }


    }
}*/