using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace netFrameworkUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void StrTest()
        {
            TestClass testClass = new TestClass()
            {
                Name = "测试",
                Info = "aaa",
                Balance = 1m,
            };
            var convertStr = Simplerjiang.ConfigJsonSave.JsonWorker.Out<TestClass>(testClass);
            Trace.WriteLine(convertStr);
            var convertModel = Simplerjiang.ConfigJsonSave.JsonWorker.Get<TestClass>(convertStr);
            Assert.AreEqual(convertModel.Balance, testClass.Balance);
        }

        [TestMethod]
        public void WriteAndReadTest()
        {
            TestClass testClass = new TestClass()
            {
                Name = "测试",
                Info = "aaa",
                Balance = 1m,
            };
            var convertPath = System.IO.Path.Combine(Environment.CurrentDirectory, "test.json");
            Simplerjiang.ConfigJsonSave.JsonWorker.Write(testClass, convertPath);

            var convertModel = Simplerjiang.ConfigJsonSave.JsonWorker.Read<TestClass>(convertPath);
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
