using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Moq;
using NBrightDNN;
using NUnit.Framework;
using ZIndex.DNN.NBrightImport.Import;
using ZIndex.DNN.NBrightImport.Model.Store;

namespace ZIndex.DNN.NBrightImport.UnitTests.ImportFileGeneratorTests
{
    [TestFixture]
    public class ImportFileV4GeneratorTests : TestBase
    {
        private XDocument _actual;
        private ImportV4FileGenerator _generator;
        private Store _store;
        private List<Category> _categories;
        private Category _rootCategory;
        private Category _childCategory;
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
                    new Product {Id = 1, ImageFilename = "1.jpg", Name = "prod1", Category = _rootCategory},
                    new Product {Id = 2, ImageFilename = "2.jpg", Name = "prod2", Category = _childCategory},
                },
                Categories = _categories,
                Culture = new CultureInfo("fr-BE"),
                ImageBasePath = "c:\\temp",
                ImageBaseUrl = "/url/",
                ProductUnitCost = 10,
            };


            // mock the converter
            _converter = new Mock<IConverter>();
            _converter.Setup(converter => converter.ToImagePath(It.IsAny<Product>(), It.IsAny<Store>()))
                .Returns(@"c:\temp\image.jpg");
            _converter.Setup(converter => converter.ToImageBaseUrl(It.IsAny<Product>(), It.IsAny<string>()))
                .Returns("/url/image.jpg");
            _converter.Setup(converter => converter.CreateCategoryElements(It.IsAny<Category>(), It.IsAny<Store>()))
                .Returns(() => new List<NBrightInfo> {CreateNBrightInfo("CATEGORY"), CreateNBrightInfo("CATEGORYLANG")});
            _converter.Setup(converter => converter.CreateProductElements(It.IsAny<Product>(), It.IsAny<Store>()))
                .Returns(() => new List<NBrightInfo> { CreateNBrightInfo("PRD"), CreateNBrightInfo("PRDLANG"), CreateNBrightInfo("CATXREF") });

            TextWriter writer = new StringWriter();

            // generate using the generator
            _generator = new ImportV4FileGenerator(_converter.Object);
            _generator.Generate(writer, _store);

            // load the actual result
            _actual = XDocument.Load(new StringReader(writer.ToString()));

            Assert.IsNotNull(_actual);
            Logger.Debug(_actual.ToString());
        }

        private NBrightInfo CreateNBrightInfo(string typeCode)
        {
            return new NBrightInfo(true)
            {
                PortalId = 0, //todo: add portalId in store
                GUIDKey = "",
                Lang = _store.Culture.ToString(),
                ModifiedDate = DateTime.Now,
                ModuleId = -1,
                ParentItemId = 0,
                RowCount = 0,
                TextData = "",
                TypeCode = typeCode,
                UserId = 0,
                XMLData = "", // ?????
                //XMLDoc =  ????
                XrefItemId = 0,
                ItemID = 1,
                
            };
        }

        [TearDown]
        public override void TestTearDown()
        {
            base.TestTearDown();
        }

        #endregion

        [Test]
        public void ImportRootIsValid() => Assert.AreEqual("root", _actual.Root.Name.LocalName);

        [Test] // 2 cat * 2 items + 2 PRD * 3 items = 10 items
        public void NBrightInfoAreSerialized() => Assert.AreEqual(10, _actual.Root.Descendants().Count(element => element.Name == "item"));

        [Test]
        public void CreateProductCountIsValid()
        {
            _converter.Verify(converter => converter.CreateProductElements(It.IsAny<Product>(), It.IsAny<Store>()),
                Times.Exactly(2));
        }

        [Test]
        public void CreateCategoryCountIsValid()
        {
            _converter.Verify(converter => converter.CreateCategoryElements(It.IsAny<Category>(), It.IsAny<Store>()),
                Times.Exactly(2));
        }



    }
}