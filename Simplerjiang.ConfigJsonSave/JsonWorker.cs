                                using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Simplerjiang.ConfigJsonSave
{
    public static class JsonWorker
    {
        /// <summary>
        /// 文件锁
        /// </summary>
        private static object File_Lock = new object();


        /// <summary>
        /// 输入JSON字符串，返回特定的类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T Get<T>(string jsonString)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream mStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                return (T)serializer.ReadObject(mStream);
            }

        }

        /// <summary>
        /// 输入特定类型，返回JSON字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string Out<T>(T model)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.WriteObject(memoryStream, model);
                memoryStream.Position = 0;
                StreamReader reader = new StreamReader(memoryStream);
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// 将类写入到特定的地点
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="data">数据类</param>
        /// <param name="path">文件地址</param>
        /// <returns></returns>
        public static Exception Write<T>(T data, string path) where T : new()
        {
            lock (File_Lock)
            {
                FileStream file;
                try
                {
                    file = new FileStream(path, FileMode.Create);
                }
                catch (Exception ex)
                {
                    // IO错误，以后写到日志里
                    return ex;
                }
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                try
                {
                    serializer.WriteObject(file, data);
                }
                catch (Exception ex)
                {
                    //写入失败
                    return ex;
                }
                finally
                {
                    file.Close();
                }
                return null;
            }
        }


        /// <summary>
        /// 读取特定路径的文件。如果有任何错误都是返回全新的类
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>返回类</returns>
        public static T Read<T>(string path) where T : new()
        {
            lock (File_Lock)
            {
                T t = new T();
                FileStream file;
                try
                {
                    using (file = new FileStream(path, FileMode.Open))
                    {
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                        t = (T)serializer.ReadObject(file);
                    }

                }
                catch (System.Security.SecurityException)
                {
                    //文件权限错误，以后写到日志里
                    Write(new T(), path);
                    return new T();
                }
                catch (FileNotFoundException)
                {
                    Write(new T(), path);
                    return new T();
                }
                catch (IOException)
                {
                    // IO错误，代表所有的file异常，以后写到日志里
                    Write(new T(), path);
                    return new T();
                }
                catch (InvalidOperationException)
                {
                    //读取失败
                    Write(new T(), path);
                    return new T();
                }
                return t;
            }
        }


    }
}
