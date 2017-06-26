using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Net.EasyIp.Enums;
using System.Net.EasyIp.Interfaces;

namespace EasyIpClientTest
{
    [TestClass]
    public class ClientTest: BaseClientTest
    {
        private IEasyIpClient _client;
        private const byte LENGTH = 63;
        private const short POINT = 0;
        private List<int> _data = new List<int>();
        
        public ClientTest()
        {
            _client = GetClientInstance();
            var sequence = Enumerable.Range(1, LENGTH);
            _data.AddRange(sequence);
            _data.Shuffle();
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

        [ClassInitialize()]
        public static void ClientInitialize(TestContext testContext)
        {
            
        }


        [TestInitialize()]
        public void ClientTestInitialize()
        {
            _data.Shuffle();
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
            var result = _client.BlockRead<short>(POINT, DataTypeEnum.FlagWord, LENGTH);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length == LENGTH);
        }

        [TestMethod]
        public void BatchWriteTest()
        {
            var val = _data.ToShortArray();
            _client.BlockWrite<short>(POINT, val, DataTypeEnum.FlagWord);
        }

        [TestMethod]
        public void BatchShortReadWriteTest()
        {
            var val = _data.ToShortArray();
            _client.BlockWrite<short>(POINT, val, DataTypeEnum.FlagWord);
            var result = _client.BlockRead<short>(POINT, DataTypeEnum.FlagWord, LENGTH);

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
            var val = _data.ToArray();
            _client.BlockWrite<int>(POINT, val, DataTypeEnum.FlagWord);
            var result = _client.BlockRead<int>(POINT, DataTypeEnum.FlagWord, LENGTH);

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
            var val = _data.ToLongArray();
            _client.BlockWrite<long>(POINT, val, DataTypeEnum.FlagWord);
            var result = _client.BlockRead<long>(POINT, DataTypeEnum.FlagWord, LENGTH);

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
            var val = _data.ToDoubleArray();
            _client.BlockWrite<double>(POINT, val, DataTypeEnum.FlagWord);
            var result = _client.BlockRead<double>(POINT, DataTypeEnum.FlagWord, LENGTH);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length == LENGTH);
            for (int i = 0; i < LENGTH; i++)
            {
                Assert.AreEqual(val[i], result[i]);
            }
        }
    }
}
