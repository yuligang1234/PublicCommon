using System;

namespace Napoleon.PublicCommon.Base
{
    public static class CustomId
    {

        /// <summary>
        /// 获取字符串类型的主键
        /// </summary>
        /// <returns></returns>
        public static string GetCustomId()
        {
            string guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
            string id = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
            return id + guid;
        }

    }
}
