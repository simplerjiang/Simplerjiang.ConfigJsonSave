using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace netCoreTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void StrTest()
        {
            TestClass testClass = new TestClass()
            {
                Name = "≤‚ ‘",
                Info = "aaa",
                Balance = 1m,
            };
            var convertStr = ConfigSaveByJson.JsonWorker.Out<TestClass>(testClass);
            Trace.WriteLine(convertStr);
            var convertModel = ConfigSaveByJson.JsonWorker.Get<TestClass>(convertStr);
            Assert.AreEqual(convertModel.Balance, testClass.Balance);
        }

        [Test]
        public void WriteAndReadTest()
        {
            TestClass testClass = new TestClass()
            {
                Name = "≤‚ ‘",
                Info = "aaa",
                Balance = 1m,
            };
            var convertPath = System.IO.Path.Combine(Environment.CurrentDirectory, "test.json");
            ConfigSaveByJson.JsonWorker.Write(testClass, convertPath);

            var convertModel = ConfigSaveByJson.JsonWorker.Read<TestClass>(convertPath);
            Assert.AreEqual(convertModel.Balance, testClass.Balance);
        }
    }
    [DataContract]
    public class TestClass
    {
        [DataMember]
        public string Name { get; set; } = "aaaaInfo";

        [DataMember]
        public string Info { get; set; } = "BBBBInfo";

        [DataMember]
        public decimal Balance { get; set; } = 100m;

        [DataMember]
        public bool Flag { get; set; } = false;

        [DataMember]
        public int Number { get; set; } = 12345;
    }
}