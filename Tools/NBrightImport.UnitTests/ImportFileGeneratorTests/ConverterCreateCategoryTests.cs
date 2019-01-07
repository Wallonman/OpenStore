using System;
using System.Globalization;
using NBrightDNN;
using NUnit.Framework;
using ZIndex.DNN.NBrightImport.Import;
using ZIndex.DNN.NBrightImport.Model.Store;

namespace ZIndex.DNN.NBrightImport.UnitTests.ImportFileGeneratorTests
{
    [TestFixture]
    public class ConverterCreateCategoryTests : TestBase
    {
        private Converter _converter;
        private NBrightInfo _actual;

        [SetUp]
        public override void TestSetup()
        {
            base.TestSetup();

            _converter = new Converter();
            _actual = _converter.CreateCategory(new Category {Id = 1, Name = "name", Parent = new Category {Id = 2}},
                new Store
                {
                    Culture = new CultureInfo("fr-BE"),
                    ImageBasePath = "ImageBasePath",
                    ImageBaseUrl = "ImageBaseUrl",
                    ProductUnitCost = 100,
                });
        }

        [Test]
        public void IdIsValid()
        {
            Assert.AreEqual(1, _actual.ItemID);
        }

        [Test]
        public void PortalIdIsValid()
        {
            Assert.AreEqual(0, _actual.PortalId);
        }
        [Test]
        public void LangIsValid()
        {
            Assert.AreEqual("fr-BE", _actual.Lang);
        }
        [Test]
        public void ModuleIdIsValid()
        {
            Assert.AreEqual(-1, _actual.ModuleId);
        }
        [Test]
        public void GUIDKeyIsValid()
        {
            Assert.AreEqual("", _actual.GUIDKey);
        }
        [Test]
        public void ModifiedDateIsValid()
        {
            Assert.AreEqual(DateTime.Now.Ticks, _actual.ModifiedDate.Ticks, 10);
        }
        [Test]
        public void ParentItemIdIsValid()
        {
            Assert.AreEqual(2, _actual.ParentItemId);
        }
        [Test]
        public void RowCountIsValid()
        {
            Assert.AreEqual(0, _actual.RowCount);
        }
        [Test]
        public void TextDataIsValid()
        {
            Assert.AreEqual("", _actual.TextData);
        }
        [Test]
        public void TypeCodeIsValid()
        {
            Assert.AreEqual("CATEGORY", _actual.TypeCode);
        }
        [Test]
        public void UserIdIsValid()
        {
            Assert.AreEqual(0, _actual.UserId);
        }
    }
}