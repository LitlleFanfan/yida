using System.Web.Http;
using System.Web.Http.Description;
using WebApiSample.Models;

namespace WebApiSample.Controllers
{
    /// <summary>
    ///  自定义WebApi 基类
    /// </summary>
    public class BaseApiController : ApiController
    {
        /// <summary>
        ///  公共基类方法,添加返回消息
        /// </summary>
        /// <param name="continueCount">强制提交计数器</param>
        /// <param name="data">数据</param>
        /// <param name="msg">消息</param>
        /// <param name="state">状态</param>
        [ApiExplorerSettings(IgnoreApi = true)]
        public void AddResultMsg(int continueCount, string data, string msg, string state)
        {
            ApiResultMsgModel model = new ApiResultMsgModel() { ContinueCount = continueCount, Data = data, Msg = msg, State = state };
            Request.Properties.Add(CommonValue.WebApiResultKey, model);
        }

        /// <summary>
        ///  添加验证错误信息
        /// </summary>
        /// <param name="msg">
        ///  信息内容
        /// </param>
        [ApiExplorerSettings(IgnoreApi = true)]
        public void AddErrorResultMsg(string msg)
        {
            AddResultMsg(0, string.Empty, msg, CommonValue.WebApiReusltState.错误.ToString());
        }

        /// <summary>
        ///  添加确认信息
        /// </summary>
        /// <param name="continueCount">强制提交计数器</param>
        /// <param name="msg">
        ///  确认信息内容
        /// </param>
        [ApiExplorerSettings(IgnoreApi = true)]
        public void AddConfirmResultMsg(int continueCount, string msg)
        {
            AddResultMsg(continueCount, string.Empty, msg, CommonValue.WebApiReusltState.警告.ToString());
        }

        /// <summary>
        ///  添加异常信息
        /// </summary>
        /// <param name="msg">
        ///  异常信息内容
        /// </param>
        [ApiExplorerSettings(IgnoreApi = true)]
        public void AddFaildResultMsg(string msg)
        {
            AddResultMsg(0, string.Empty, msg, CommonValue.WebApiReusltState.失败.ToString());
        }

        /// <summary>
        ///  添加成功信息
        /// </summary>
        /// <param name="msg">
        ///  成功信息内容
        /// </param>
        [ApiExplorerSettings(IgnoreApi = true)]
        public void AddSucessResultMsg(string msg)
        {
            AddResultMsg(0, string.Empty, msg, CommonValue.WebApiReusltState.成功.ToString());
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public  string GetConfigString(string keyName)
        {
            var config = System.Configuration.ConfigurationManager.AppSettings;
            string connectionString = config[keyName];

            //string connectionString = "server=GEK-MIS01uat;database=QualityDB;uid=test;pwd=ittest";
            return connectionString;
        }
    }
}