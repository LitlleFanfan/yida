using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebApiSample.Models;

namespace WebApiSample.Filters
{
    /// <summary>
    ///  跟踪记录接口性能的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true)]
    public class TracePerformanceAttribute : ActionFilterAttribute
    {
        // 计时器对象在参数字典中对应的key
        private const string KeyName = "performanceTimeKey";

        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            // 启动一个计时器，并将计时器对象保存在执行上下文的参数字典中
            actionContext.ActionArguments[KeyName] = Stopwatch.StartNew();

            await base.OnActionExecutingAsync(actionContext, cancellationToken);
        }

        public override async Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            await base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);

            object performanceStopwatchObj;
            if (actionExecutedContext.ActionContext.ActionArguments.TryGetValue(KeyName, out performanceStopwatchObj))
            {
                // 从执行上下文的参数字典中获取计时器对象
                var performanceStopwatch = performanceStopwatchObj as Stopwatch;

                if (performanceStopwatch == null)
                {
                    return;
                }

                // 停止计时器
                performanceStopwatch.Stop();

                // 获取请求的api名称  controler / action
                var interfaceName = string.Format("{0}:{1}", GetClientIp(actionExecutedContext.Request), actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName + @"/" + actionExecutedContext.ActionContext.ActionDescriptor.ActionName);

                // 获取请求参数
                string paramStr = "";

                if (actionExecutedContext.Request.Method == HttpMethod.Get)
                {
                    // Get请求方式，从Url中获得参数
                    paramStr = actionExecutedContext.Request.RequestUri.Query;
                }
                else if (actionExecutedContext.Request.Method == HttpMethod.Post)
                {
                    // Post请求方式，从Body中获得参数
                    paramStr = GetBodyFromRequest(actionExecutedContext);
                }

                // 将执行时间记录到数据库
                await Task.Run(() => WritePerformace(interfaceName, performanceStopwatch.ElapsedMilliseconds, paramStr), cancellationToken);
            }
        }

        /// <summary>
        ///  从请求中解析Post提交的信息
        /// </summary>
        /// <param name="actionExecutedContext">执行上下文</param>
        /// <returns>
        ///  body参数内容
        /// </returns>
        private string GetBodyFromRequest(HttpActionExecutedContext actionExecutedContext)
        {
            string data;
            using (var stream = actionExecutedContext.Request.Content.ReadAsStreamAsync().Result)
            {
                if (stream.CanSeek)
                {
                    stream.Position = 0;
                }
                data = actionExecutedContext.Request.Content.ReadAsStringAsync().Result;
            }
            return data;
        }

        /// <summary>
        ///  将执行耗时记录到数据库
        /// </summary>
        private void WritePerformace(string interfaceName, decimal elapsedMilliseconds, string paramStr)
        {
            WebApiPerformanceLog log = new WebApiPerformanceLog();
            log.InterfaceName = interfaceName;
            log.InterfaceTotalElapsedMilliseconds = elapsedMilliseconds;
            log.ParamStr = paramStr;
            log.RequestDateTime = DateTime.Now;
            log.RequestId = Guid.NewGuid().ToString();
            log.Type = "WebApi";
            WritePerformanceLog(log);
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

        /// <summary>
        ///  将性能日志记录到数据库
        /// </summary>
        /// <param name="log">性能日志实体</param>
        public void WritePerformanceLog(WebApiPerformanceLog log)
        {
            using (DbContext dbContext = new DbContext("DefaultConnection"))
            {
                SqlParameter requestIdSqlParameter = string.IsNullOrEmpty(log.RequestId) ? new SqlParameter("@requestId", DBNull.Value) : new SqlParameter("@requestId", log.RequestId);
                SqlParameter requestDateTimeSqlParameter = new SqlParameter("@requestDateTime", log.RequestDateTime);
                SqlParameter interfaceNameSqlParameter = string.IsNullOrEmpty(log.InterfaceName) ? new SqlParameter("@interfaceName", DBNull.Value) : new SqlParameter("@interfaceName", log.InterfaceName);
                SqlParameter paramStrSqlParameter = string.IsNullOrEmpty(log.ParamStr) ? new SqlParameter("@paramStr", DBNull.Value) : new SqlParameter("@paramStr", log.ParamStr);
                SqlParameter interfaceTotalElapsedMillisecondsSqlParameter = new SqlParameter("@interfaceTotalElapsedMilliseconds", log.InterfaceTotalElapsedMilliseconds);
                SqlParameter sqlTextSqlParameter = string.IsNullOrEmpty(log.SqlText) ? new SqlParameter("@sqlText", DBNull.Value) : new SqlParameter("@sqlText", log.SqlText);
                SqlParameter sqlElapsedMillisecondsSqlParameter = new SqlParameter("@sqlElapsedMilliseconds", log.SqlElapsedMilliseconds);
                SqlParameter uIElapsedMilisencondsSqlParameter = new SqlParameter("@uIElapsedMilisenconds", log.UiElapsedMilisenconds);
                SqlParameter typeSqlParameter = string.IsNullOrEmpty(log.Type) ? new SqlParameter("@type", DBNull.Value) : new SqlParameter("@type", log.Type);

                object[] paramObjects = {
                    requestIdSqlParameter, requestDateTimeSqlParameter, interfaceNameSqlParameter, paramStrSqlParameter,
                    interfaceTotalElapsedMillisecondsSqlParameter,
                    sqlTextSqlParameter, sqlElapsedMillisecondsSqlParameter, uIElapsedMilisencondsSqlParameter,
                    typeSqlParameter
                 };

                dbContext.Database.ExecuteSqlCommand(
                    "EXEC Usp_WriteWebApiPerformanceLog @requestId,@requestDateTime,@interfaceName,@paramStr,@interfaceTotalElapsedMilliseconds,@sqlText,@sqlElapsedMilliseconds,@uIElapsedMilisenconds,@type",
                    paramObjects);
            }
        }
    }
}