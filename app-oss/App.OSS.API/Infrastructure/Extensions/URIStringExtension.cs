using System;
using System.Linq;

namespace App.OSS.API.Infrastructure.Extensions
{
    /// <summary>
    /// URI字符串进行回退上级路由帮助类
    /// </summary>
    public static class URIStringExtension
    {
        /// <summary>
        /// 返回上一级Uri
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="lowercase"></param>
        /// <returns></returns>
        public static string URIUpperLevel(this string uri, bool lowercase = true)
        {
            return uri.URIUpperLevel(1, lowercase);
        }

        /// <summary>
        /// 返回指定的上级Uri
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="upperLevel"></param>
        /// <param name="lowercase"></param>
        /// <returns></returns>
        public static string URIUpperLevel(this string uri, int upperLevel, bool lowercase = true)
        {
            if (upperLevel < 1)
                throw new Exception($"\"upperLevel\" must greater than 1");
            var arr = uri.Split("/", StringSplitOptions.None).ToList();
            for (; upperLevel > 0; upperLevel--)
                arr.RemoveAt(arr.Count - 1);

            var uriStr = string.Join('/', arr);
            if (lowercase)
                return uriStr.ToLower();
            return uriStr;
        }
    }
}
