using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ZIndex.DNN.NBrightImport.Model.Store;

namespace ZIndex.DNN.NBrightImport.UnitTests.FolderParser
{
    [TestFixture]
    public class CategoriesParserTests : TestBase
    {
        /*
        * Typical directory structure (with generated id + idlang)
        * Root(1+2) | Path1(2+3)
        * Root(1) | Path1(2+3) | Path11(4+5)
        * Root(1) | Path1(2+3) | Path12(6+7)
        * Root(1) | Path2(8+9)
        */



        private global::ZIndex.DNN.NBrightImport.Import.CategoriesParser _parser;
        private List<Category> _actualCategories;

        #region Setup/Teardown

        [SetUp]
        public override void TestSetup()
        {
            base.TestSetup();

            _parser = new global::ZIndex.DNN.NBrightImport.Import.CategoriesParser();

            _actualCategories = _parser.Parse(StoreFiles);// Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"FolderParser\Root"));
        }

        [TearDown]
        public override void TestTearDown()
        {
            base.TestTearDown();
        }

        #endregion

        [Test]
        public void CategoriesCountIsValid() => Assert.AreEqual(5, _actualCategories.Count);

        [Test]
        public void CategoryRootChildCountIsValid() => Assert.AreEqual(2, _actualCategories.Count(category => category.Parent != null && category.Parent.Name == "Root"));

        [Test]
        public void CategoryPath1ChildCountIsValid() => Assert.AreEqual(2, _actualCategories.Count(category => category.Parent != null && category.Parent.Name == "Path1"));

        [Test]
        public void CategoryPath11ParentIsValid() => Assert.AreEqual("Path1", _actualCategories.Single(category => category.Name == "Path11").Parent.Name);

        [Test]
        public void CategoryRootIdIsValid() => Assert.AreEqual(1, _actualCategories.Single(category => category.Name == "Root").Id);
        [Test]
        public void CategoryRootIdLangIsValid() => Assert.AreEqual(2, _actualCategories.Single(category => category.Name == "Root").IdLang);

        [Test]
        public void CategoryPath1IdIsValid() => Assert.AreEqual(3, _actualCategories.Single(category => category.Name == "Path1").Id);
        [Test]
        public void CategoryPath1IdLangIsValid() => Assert.AreEqual(4, _actualCategories.Single(category => category.Name == "Path1").IdLang);

        [Test]
        public void CategoryPath2IdIsValid() => Assert.AreEqual(9, _actualCategories.Single(category => category.Name == "Path2").Id);
        [Test]
        public void CategoryPath2IdLangIsValid() => Assert.AreEqual(10, _actualCategories.Single(category => category.Name == "Path2").IdLang);

        [Test]
        public void CategoryIdsAreValid() => Assert.AreEqual(55, _actualCategories.Sum(category => category.Id + category.IdLang));


    }
}