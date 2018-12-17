using System.IO;
using System.Reflection;
using NUnit.Framework;
using ZIndex.DNN.NBrightImport.Import;

namespace ZIndex.DNN.NBrightImport.UnitTests.NBStoreConnect.ZipFileGeneratorTests
{
    [TestFixture]
    public class GenerateZipFileTests : TestBase
    {
        private ZipFileGenerator _zipGenerator;
        private string _zipFile;


        #region Setup/Teardown

        [SetUp]
        public override void TestSetup()
        {
            base.TestSetup();

            _zipGenerator = new ZipFileGenerator();

            _zipFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"test.zip");

            _zipGenerator.Zip(_zipFile, StoreFiles, "*.jpg", true);

        }

        [TearDown]
        public override void TestTearDown()
        {
            base.TestTearDown();
//            File.Delete(_zipFile);
        }

        #endregion

        [Test]
        public void ZipFileExists()
        {
            Assert.IsTrue(File.Exists(_zipFile));
        }

    }
}