using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using WebApiSample.Utility;

namespace WebApiSample.ExceptionHandling
{
    /// <summary>
    ///     将异常信息记录到sqlserver数据库中
    /// </summary>
    public class SqlServerExceptionLogger : ExceptionLogger
    {
        public override async Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            await base.LogAsync(context, cancellationToken);
            await Task.Run(() => WriteLog(context), cancellationToken);
        }

        /// <summary>
        ///  记录日志方法
        /// </summary>
        /// <param name="context">
        ///  异常上下文
        /// </param>
        public void WriteLog(ExceptionLoggerContext context)
        {
            if (context != null && context.Exception != null)
            {
                var exceptionMessage = context.Exception.Message;
                var innerExceptionMessage = context.Exception.InnerException == null ? string.Empty : context.Exception.InnerException.Message;
                var url = context.Request.RequestUri.ToString();
                var log = LogHelper.GetInstance();
                string msg = string.Format("WebApi接口异常:url:{0};IP:{1};ExceptionMessage:{2};InnerExceptionMsg:{3}", url, GetClientIp(context.Request), exceptionMessage, innerExceptionMessage);
                log.WriteFatal(msg, context.Exception);
            }
        }

        /// <summary>
        ///  获取客户端IP
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string GetClientIp(HttpRequestMessage request)
        {
            return request.GetOwinContext().Request.RemoteIpAddress;
        }
    }
}