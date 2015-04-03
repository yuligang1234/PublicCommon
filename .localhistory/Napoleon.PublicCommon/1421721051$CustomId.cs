
using System;

namespace Napoleon.PublicCommon
{
    public static class CustomId
    {

        /// <summary>
        /// 获取字符串类型的主键
        /// </summary>
        /// <returns></returns>
        public static string GetCustomId()
        {
            string id = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
            string guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
            id += guid.Substring(0, 10);
            return id;
        }

    }
}
