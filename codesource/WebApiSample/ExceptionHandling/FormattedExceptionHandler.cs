using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using WebApiSample.Models;

namespace WebApiSample.ExceptionHandling
{
    /// <summary>
    ///  自定义异常处理类
    /// </summary>
    public class FormattedExceptionHandler : ExceptionHandler
    {
        /// <summary>
        /// 在派生类中重写时，将异步处理异常。
        /// </summary>
        /// <returns>
        /// 表示异步异常处理操作的任务。
        /// </returns>
        /// <param name="context">异常处理程序上下文。</param><param name="cancellationToken">要监视的取消请求标记。</param>
        public override async Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            await base.HandleAsync(context, cancellationToken);

            await Task.Run(() =>
            {
                TextPlainErrorResult result = new TextPlainErrorResult
                {
                    Request = context.Request,
                    Exception = context.Exception
                };
                context.Result = result;
            }, cancellationToken);
        }
    }

    /// <summary>
    ///  自定义异常返回类型
    /// </summary>
    public class TextPlainErrorResult : IHttpActionResult
    {
        /// <summary>
        ///  请求对象
        /// </summary>
        public HttpRequestMessage Request { get; set; }

        /// <summary>
        ///  异常对象
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        ///  执行格式化异常结果
        /// </summary>
        /// <param name="cancellationToken">取消参数</param>
        /// <returns>
        ///  格式化后的Response
        /// </returns>
        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response =
                             new HttpResponseMessage(HttpStatusCode.InternalServerError);

            var result = new ApiResultMsgModel()
            {
                State = CommonValue.WebApiReusltState.失败.ToString(),
                ContinueCount = 0,
                Msg = Exception.InnerException == null ? Exception.Message : Exception.InnerException.Message
            };
            var content = Json.Encode(result);
            response.Content = new StringContent(content);
            response.RequestMessage = Request;
            return await Task.FromResult(response);
        }
    }

}