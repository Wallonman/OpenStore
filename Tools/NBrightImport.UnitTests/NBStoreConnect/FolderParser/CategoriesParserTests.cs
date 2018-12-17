using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ZIndex.DNN.NBrightImport.Model.Store;

namespace ZIndex.DNN.NBrightImport.UnitTests.NBStoreConnect.FolderParser
{
    [TestFixture]
    public class CategoriesParserTests : TestBase
    {
        /*
        * Typical directory structure (with generated id's)
        * Root(0) | Path1(1)
        * Root(0) | Path1(1) | Path11(2)
        * Root(0) | Path1(1) | Path12(3)
        * Root(0) | Path2(4)
        */



        private global::ZIndex.DNN.NBrightImport.Import.CategoriesParser _parser;
        private List<Category> _actualCategories;

        #region Setup/Teardown

        [SetUp]
        public override void TestSetup()
        {
            base.TestSetup();

            _parser = new global::ZIndex.DNN.NBrightImport.Import.CategoriesParser();

            _actualCategories = _parser.Parse(StoreFiles);// Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"NBrightImport\FolderParser\Root"));
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
        public void CategoryRootChildCountIsValid()
        {
            Assert.AreEqual(2, _actualCategories.Count(category => category.Parent != null && category.Parent.Name == "Root"));
        }

        [Test]
        public void CategoryPath1ChildCountIsValid()
        {
            Assert.AreEqual(2, _actualCategories.Count(category => category.Parent != null && category.Parent.Name == "Path1"));
        }

        [Test]
        public void CategoryPath11ParentIsValid()
        {
            Assert.AreEqual("Path1", _actualCategories.Single(category => category.Name == "Path11").Parent.Name);
        }

        [Test]
        public void CategoryRootIdIsValid()
        {
            Assert.AreEqual(1, _actualCategories.Single(category => category.Name == "Root").Id);
        }

        [Test]
        public void CategoryPath1IdIsValid()
        {
            Assert.AreEqual(2, _actualCategories.Single(category => category.Name == "Path1").Id);
        }

        [Test]
        public void CategoryPath2IdIsValid()
        {
            Assert.AreEqual(5, _actualCategories.Single(category => category.Name == "Path2").Id);
        }

        [Test]
        public void CategoryIdsAreValid()
        {
            // ID's : 1+2+3+4+5 = 15
            Assert.AreEqual(15, _actualCategories.Sum(category => category.Id));
        }


    }
}