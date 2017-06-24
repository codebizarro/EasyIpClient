using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.EasyIp.Enums;
using System.Net.EasyIp.Interfaces;

namespace EasyIpClientTest
{
    [TestClass]
    public class ClientTest: BaseClientTest
    {
        private IEasyIpClient _client;
        private const byte LENGTH = 2;
        private const short POINT = 0;
        
        public ClientTest()
        {
            _client = GetClientInstance();
        }

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Дополнительные атрибуты тестирования
        //
        // При написании тестов можно использовать следующие дополнительные атрибуты:
        //
        // ClassInitialize используется для выполнения кода до запуска первого теста в классе
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // TestInitialize используется для выполнения кода перед запуском каждого теста 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // TestCleanup используется для выполнения кода после завершения каждого теста
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void BatchReadTest()
        {
            var result = _client.BatchReadWord<short>(POINT, DataTypeEnum.FlagWord, LENGTH);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length == LENGTH);
        }

        [TestMethod]
        public void BatchWriteTest()
        {
            var val = new short[] { 254, 201 };
            _client.BatchWriteWord<short>(POINT, val, DataTypeEnum.FlagWord);
        }

        [TestMethod]
        public void BatchShortReadWriteTest()
        {
            var val = new short[LENGTH] { 254, 201 };
            _client.BatchWriteWord<short>(POINT, val, DataTypeEnum.FlagWord);
            var result = _client.BatchReadWord<short>(POINT, DataTypeEnum.FlagWord, LENGTH);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length == LENGTH);
            for (int i = 0; i < LENGTH; i++)
            {
                Assert.AreEqual(val[i], result[i]);
            }
        }

        [TestMethod]
        public void BatchIntReadWriteTest()
        {
            var val = new int[LENGTH] { int.MaxValue - 1, int.MaxValue - 2 };
            _client.BatchWriteWord<int>(POINT, val, DataTypeEnum.FlagWord);
            var result = _client.BatchReadWord<int>(POINT, DataTypeEnum.FlagWord, LENGTH);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length == LENGTH);
            for (int i = 0; i < LENGTH; i++)
            {
                Assert.AreEqual(val[i], result[i]);
            }
        }

        [TestMethod]
        public void BatchLongReadWriteTest()
        {
            var val = new long[LENGTH] { long.MaxValue-1, long.MaxValue-2 };
            _client.BatchWriteWord<long>(POINT, val, DataTypeEnum.FlagWord);
            var result = _client.BatchReadWord<long>(POINT, DataTypeEnum.FlagWord, LENGTH);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length == LENGTH);
            for (int i = 0; i < LENGTH; i++)
            {
                Assert.AreEqual(val[i], result[i]);
            }
        }

        [TestMethod]
        public void BatchDoubleReadWriteTest()
        {
            var val = new double[LENGTH] { double.MaxValue - 0.1, double.MaxValue - 0.2 };
            _client.BatchWriteWord<double>(POINT, val, DataTypeEnum.FlagWord);
            var result = _client.BatchReadWord<double>(POINT, DataTypeEnum.FlagWord, LENGTH);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length == LENGTH);
            for (int i = 0; i < LENGTH; i++)
            {
                Assert.AreEqual(val[i], result[i]);
            }
        }
    }
}
