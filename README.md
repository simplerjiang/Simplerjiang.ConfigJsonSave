# Simplerjiang.ConfigJsonSave


Nuget:[![Simplerjiang.ConfigJsonSave](https://img.shields.io/nuget/v/Simplerjiang.ConfigJsonSave.svg)](https://www.nuget.org/packages/Simplerjiang.ConfigJsonSave/)
[![Simplerjiang.ConfigJsonSave](https://img.shields.io/nuget/dt/Simplerjiang.ConfigJsonSave.svg)](https://www.nuget.org/packages/Simplerjiang.ConfigJsonSave/)



基于.net standard，用于.net 与 .net core的配置文件Json 及Json转换的类库

A Json Worker Class library, most easy way for write config file and read, base on .Net Standard. 

---

使用非常简单

It's pretty easy to use.


为你的模型加上[DataContract] 和 [DataMember] 特性，不参与json的加上[IgnoreDataMember]

Add [DataContract]  to your class,  [DataMember] to your property. to ingnore property should use [IgnoreDataMember]

```c#
using System.Runtime.Serialization; 

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
```

加上类库后

after using library

```c#
        [TestMethod]
        public void StrTest()
        {
            TestClass testClass = new TestClass()
            {
                Name = "test",
                Info = "aaa",
                Balance = 1m,
            };
            //将模型转换为json字符串 convert json string to model
            var convertStr = Simplerjiang.ConfigJsonSave.JsonWorker.Out<TestClass>(testClass); 
            
            Trace.WriteLine(convertStr);
            
            //将json字符串转换为模型 convert json string to model
            var convertModel = Simplerjiang.ConfigJsonSave.JsonWorker.Get<TestClass>(convertStr);
            
            Assert.AreEqual(convertModel.Balance, testClass.Balance);
        }
        
        [TestMethod]
        public void WriteAndReadTest()
        {
            TestClass testClass = new TestClass()
            {
                Name = "test",
                Info = "aaa",
                Balance = 1m,
            };
            //保存路径 Save Path
            var convertPath = System.IO.Path.Combine(Environment.CurrentDirectory, "test.json");
            
            //保存json文件到路径 Save json file to path
            Simplerjiang.ConfigJsonSave.JsonWorker.Write(testClass, convertPath);
            
            //读取json文件从路径 Read json file from path
            var convertModel = Simplerjiang.ConfigJsonSave.JsonWorker.Read<TestClass>(convertPath);
            
            Assert.AreEqual(convertModel.Balance, testClass.Balance);
        }
        
```
---

### 支持环境 Support Framework

.net core >= 2.0

.net >= 4.6.1

其他平台请看 Others Platfrom(without test)：https://docs.microsoft.com/zh-cn/dotnet/standard/net-standard#net-implementation-support
