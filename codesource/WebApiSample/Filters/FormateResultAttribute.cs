using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using WebApiSample.Models;

namespace WebApiSample.Filters
{

    /// <summary>
    ///  格式化API结果的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true)]
    public class FormateResultAttribute : ActionFilterAttribute
    {
        /// <summary>
        ///     方法执行完成事件
        /// </summary>
        /// <param name="actionExecutedContext">当前上下文</param>
        /// <param name="cancellationToken">异步取消对象</param>
        /// <returns></returns>
        public override async Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext,
            CancellationToken cancellationToken)
        {
            await base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);

            if (actionExecutedContext.Exception != null)
            {
                // 发生异常时，转到全局异常处理
                throw actionExecutedContext.Exception;
            }
            await FormaterResultAsync(actionExecutedContext);
        }

        /// <summary>
        ///  使用自定义消息格式封装api结果
        /// </summary>
        /// <param name="actionExecutedContext">
        ///  接口执行的上下文
        /// </param>
        public async Task FormaterResultAsync(HttpActionExecutedContext actionExecutedContext)
        {
            object msg;
            ApiResultMsgModel resultModel;

            // 替换掉NoContent状态，使用自定义的消息格式
            var statusCode = actionExecutedContext.Response.StatusCode == HttpStatusCode.NoContent
                ? HttpStatusCode.OK
                : actionExecutedContext.Response.StatusCode;

            if (actionExecutedContext.ActionContext.Request.Properties.TryGetValue(CommonValue.WebApiResultKey, out msg))
            {
                resultModel = msg as ApiResultMsgModel;
                if (resultModel == null)
                {
                    resultModel = new ApiResultMsgModel { State = CommonValue.WebApiReusltState.成功.ToString() };
                }
            }
            else if (actionExecutedContext.ActionContext != null &&
                     !actionExecutedContext.ActionContext.ModelState.IsValid && statusCode != HttpStatusCode.OK)
            {
                resultModel = new ApiResultMsgModel { State = CommonValue.WebApiReusltState.错误.ToString() };
            }
            else
            {
                resultModel = new ApiResultMsgModel { State = CommonValue.WebApiReusltState.成功.ToString() };
            }

            if (actionExecutedContext.ActionContext.Response.Content != null)
            {
                resultModel.Data = await actionExecutedContext.ActionContext.Response.Content.ReadAsStringAsync();
            }
            else
            {
                resultModel.Data = string.Empty;
            }

            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(statusCode, resultModel);
        }
    }
}