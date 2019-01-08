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
        protected internal NBrightInfo _nBrightInfo;

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

            _nBrightInfo = new NBrightInfo(true)
            {
                PortalId = 0, //todo: add portalId in store
                GUIDKey = "",
                Lang = _store.Culture.ToString(),
                ModifiedDate = DateTime.Now,
                ModuleId = -1,
                ParentItemId = 0,
                RowCount = 0,
                TextData = "",
//                TypeCode = typeCode,
                UserId = 0,
                XMLData = "", // ?????
                //XMLDoc =  ????
                XrefItemId = 0,
                ItemID = 1,
                
            };

            // mock the converter
            _converter = new Mock<IConverter>();
            _converter.Setup(converter => converter.ToImagePath(It.IsAny<Product>(), It.IsAny<string>()))
                .Returns(@"c:\temp\image.jpg");
            _converter.Setup(converter => converter.ToImageBaseUrl(It.IsAny<Product>(), It.IsAny<string>()))
                .Returns("/url/image.jpg");
            _converter.Setup(converter => converter.CreateCategory(It.IsAny<Category>(), It.IsAny<Store>()))
                .Returns(() =>
                {
                    _nBrightInfo.TypeCode = "CATEGORY";

                    return
                        _nBrightInfo;
                });
            _converter.Setup(converter => converter.CreateCategoryLang(It.IsAny<Category>(), It.IsAny<Store>()))
                .Returns(() =>
                {
                    _nBrightInfo.TypeCode = "CATEGORYLANG";

                    return
                        _nBrightInfo;
                });
            _converter.Setup(converter => converter.CreateProduct(It.IsAny<Product>(), It.IsAny<Store>()))
                .Returns(() =>
                {
                    _nBrightInfo.TypeCode = "PRD";

                    return
                        _nBrightInfo;
                });

            _converter.Setup(converter => converter.CreateProductLang(It.IsAny<Product>(), It.IsAny<Store>()))
                .Returns(() =>
                {
                    _nBrightInfo.TypeCode = "PRDLANG";

                    return
                        _nBrightInfo;
                });

            TextWriter writer = new StringWriter();

            // generate using the generator
            _generator = new ImportV4FileGenerator(_converter.Object);
            _generator.Generate(writer, _store);

            // load the actual result
            _actual = XDocument.Load(new StringReader(writer.ToString()));

            Assert.IsNotNull(_actual);
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
        public void CreateProductCountIsValid()
        {
            _converter.Verify(converter => converter.CreateProduct(It.IsAny<Product>(), It.IsAny<Store>()),
                Times.Exactly(2));
        }
        [Test]
        public void CreateProductLangCountIsValid()
        {
            _converter.Verify(converter => converter.CreateProductLang(It.IsAny<Product>(), It.IsAny<Store>()),
                Times.Exactly(2));
        }

        [Test]
        public void CreateCategoryCountIsValid()
        {
            _converter.Verify(converter => converter.CreateCategory(It.IsAny<Category>(), It.IsAny<Store>()),
                Times.Exactly(2));
        }
        [Test]
        public void CreateCategoryLangCountIsValid()
        {
            _converter.Verify(converter => converter.CreateCategoryLang(It.IsAny<Category>(), It.IsAny<Store>()),
                Times.Exactly(2));
        }


    }
}