using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
        private IList<NBrightInfo> _actual;

        [SetUp]
        public override void TestSetup()
        {
            base.TestSetup();

            _converter = new Converter();
            _actual = _converter.CreateCategoryElements(new Category {Id = 1, Name = "name", Parent = new Category {Id = 2}},
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
            Assert.AreEqual(1, _actual.First().ItemID);
        }

        [Test]
        public void PortalIdIsValid()
        {
            Assert.AreEqual(0, _actual.First().PortalId);
        }
        [Test]
        public void LangIsValid()
        {
            Assert.AreEqual("fr-BE", _actual.First().Lang);
        }
        [Test]
        public void ModuleIdIsValid()
        {
            Assert.AreEqual(-1, _actual.First().ModuleId);
        }
        [Test]
        public void GUIDKeyIsValid()
        {
            Assert.AreEqual("", _actual.First().GUIDKey);
        }
        [Test]
        public void ModifiedDateIsValid()
        {
            Assert.AreEqual(DateTime.Now.Ticks, _actual.First().ModifiedDate.Ticks, 10);
        }
        [Test]
        public void ParentItemIdIsValid()
        {
            Assert.AreEqual(2, _actual.First().ParentItemId);
        }
        [Test]
        public void RowCountIsValid()
        {
            Assert.AreEqual(0, _actual.First().RowCount);
        }
        [Test]
        public void TextDataIsValid()
        {
            Assert.AreEqual("", _actual.First().TextData);
        }
        [Test]
        public void TypeCodeIsValid()
        {
            Assert.AreEqual("CATEGORY", _actual.First().TypeCode);
        }
        [Test]
        public void UserIdIsValid()
        {
            Assert.AreEqual(0, _actual.First().UserId);
        }
    }
}