using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace ZIndex.DNN.NBrightImport.UnitTests.NBStoreConnect.ImportManager
{
    [TestFixture]
    public class TestGenerateMultiImportFiles : TestBase
    {
        private global::ZIndex.DNN.NBrightImport.Import.ImportManagerOld _importManager;
        private string _path;

        #region Setup/Teardown

        [SetUp]
        public override void TestSetup()
        {
            base.TestSetup();

            _importManager = global::ZIndex.DNN.NBrightImport.Import.ImportManagerOld.CreateInstance();

            _path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"NBrightImport\ImportManager\Root");
            Logger.Debug("Working folder {0}", _path);

            _importManager.GenerateMultiImportFiles(_path, "/mcpaquot/Portals/0/productimages/", @"c:\temp", 2, CultureInfo.CurrentCulture);

        }

        [TearDown]
        public override void TestTearDown()
        {
            base.TestTearDown();
        }

        #endregion

        [Test]
        public void GeneratedXmlFileCountIsValid()
        {
            Assert.AreEqual(3, Directory.EnumerateFiles(_path, "*.xml", SearchOption.AllDirectories).Count());
        }

        [Test]
        public void GeneratedZipFileCountIsValid()
        {
            Assert.AreEqual(3, Directory.EnumerateFiles(_path, "*.zip", SearchOption.AllDirectories).Count());
        }


    }
}