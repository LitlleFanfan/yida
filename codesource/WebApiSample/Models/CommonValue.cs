using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSample.Models
{
    public class CommonValue
    {
        /// <summary>
        ///  webapi控制器与过滤器传递参数的key
        /// </summary>
        public const string WebApiResultKey = "ApiResultMsgModel";

        /// <summary>
        ///  接口返回的状态信息
        /// </summary>
        public enum WebApiReusltState
        {
            成功,
            失败,
            警告,
            错误
        }
    }
}