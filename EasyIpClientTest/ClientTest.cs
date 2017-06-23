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
            var result = _client.BatchReadWord<short>(0, DataTypeEnum.FlagWord, LENGTH);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length == LENGTH);
        }
    }
}
