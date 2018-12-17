using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Moq;
using NUnit.Framework;
using ZIndex.DNN.NBrightImport.Import;

namespace ZIndex.DNN.NBrightImport.UnitTests.NBStoreConnect.ImportManager
{
    [TestFixture]
    public class TestGenerateImportFile : TestBase
    {
        private Mock<ISimpleProduct> _simpleProduct;
        private global::ZIndex.DNN.NBrightImport.Import.ImportManagerOld _importManager;
        private XDocument _actual;

        #region Setup/Teardown

        [SetUp]
        public override void TestSetup()
        {
            base.TestSetup();
            _simpleProduct = new Mock<ISimpleProduct>();
            _simpleProduct.Setup(p => p.ProductNames).Returns(new List<string> {"TestPicture1", "TestPicture2"});
            _simpleProduct.Setup(p => p.ProductsImageExtension).Returns("ProductsImageExtension");
            _simpleProduct.Setup(p => p.CategoryName).Returns("CategoryName");
            _simpleProduct.Setup(p => p.ImageBaseUrl).Returns("/ImageBaseUrl/");
            _simpleProduct.Setup(p => p.ImageBasePath).Returns(@"ImageBasePath");
            _simpleProduct.Setup(p => p.CategoryImage).Returns("CategoryImage.jpg");
            _simpleProduct.Setup(p => p.UnitCost).Returns(111);
            _simpleProduct.Setup(p => p.Culture).Returns(CultureInfo.GetCultureInfo("fr-BE"));

            _importManager = global::ZIndex.DNN.NBrightImport.Import.ImportManagerOld.CreateInstance();

            TextWriter writer = new StringWriter();

            _importManager.GenerateImportFile(writer, _simpleProduct.Object);

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
        public void ImportContainsOneCategory()
        {
            Assert.AreEqual(1, _actual.Descendants("NB_Store_CategoriesInfo").Count());
        }

        [Test]
        public void ImportCategoryNameIsValid()
        {
            Assert.AreEqual("CategoryName", _actual.Descendants("NB_Store_CategoriesInfo").First().Element("CategoryName").Value);
        }

        [Test]
        public void ImportCategoryImageURLIsValid()
        {
            Assert.AreEqual("/ImageBaseUrl/CategoryImage..jpg", _actual.Descendants("NB_Store_CategoriesInfo").First().Element("ImageURL").Value);
        }

        [Test]
        public void ImportContainsTwoProducts()
        {
            Assert.AreEqual(2, _actual.Descendants("NB_Store_ProductsInfo").Count());
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
}