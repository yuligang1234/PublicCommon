using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

namespace Napoleon.PublicCommon
{
    public static class SerializeFunc
    {

        #region Json和实体类的相互转换

        /// <summary>
        ///  json序列化为实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">The json.</param>
        /// Author  : Napoleon
        /// Created : 2014-10-28 14:15:41
        public static T DeserializeJsonToList<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        ///  实体类序列化为json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">The t.</param>
        /// Author  : Napoleon
        /// Created : 2014-10-28 14:30:14
        public static string SerializeListToJson<T>(this T t)
        {
            return JsonConvert.SerializeObject(t);
        }

        #endregion

        #region 序列化和反序列化

        /// <summary>
        ///  序列化为字节流
        /// </summary>
        /// <param name="obj">The object.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-13 11:12:19
        public static string SerializeObject(this object obj)
        {
            string result;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                byte[] byt = ms.ToArray();
                result = Convert.ToBase64String(byt);
                ms.Flush();
            }
            return result;
        }

        /// <summary>
        ///  反序列化字节流
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str">The string.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-17 13:41:40
        public static T DSerializeToObject<T>(this string str) where T : class
        {
            T t;
            BinaryFormatter bf = new BinaryFormatter();
            byte[] byt = Convert.FromBase64String(str);
            using (MemoryStream ms = new MemoryStream(byt, 0, byt.Length))
            {
                t = bf.Deserialize(ms) as T;
            }
            return t;
        }

        #endregion

    }
}
