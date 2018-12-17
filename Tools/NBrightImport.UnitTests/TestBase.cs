using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using ZIndex.DNN.NBrightImport.Logger;

namespace ZIndex.DNN.NBrightImport.UnitTests
{
    public class TestBase
    {
        protected ILog Logger { get; private set; }
        private DateTime _startAt;

        /// <summary>
        /// The store files
        /// </summary>
        protected string StoreFiles;

        public TestBase()
        {
            Logger = new LoggerBase(GetType()).Logger;
        }

        #region NUnit Section

        [TestFixtureSetUp]
        public virtual void Initialize()
        {
        }

        [SetUp]
        public virtual void TestSetup()
        {
            _startAt = DateTime.Now;
            Logger.Info("--- Begin test -------------------------------------------------");
            Logger.Debug("--- Initializing test at {0}", _startAt.ToLongTimeString());

            StoreFiles = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"NBrightImport\StoreSamples\Root");
            Logger.Debug("Store files located @ {0}", StoreFiles);

            Logger.Info("----------- End Initialize Tests -----------");
        }

        [TearDown]
        public virtual void TestTearDown()
        {
            var endAt = DateTime.Now;
            Logger.Debug("--- Cleanup test at {0}, test duration is {1}", endAt.ToLongTimeString(), endAt.Subtract(_startAt).ToString());
            Logger.Info("--- End Test -------------------------------------------------");
        }

        #endregion
    }
}
