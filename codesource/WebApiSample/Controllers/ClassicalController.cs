using System.Collections.Generic;
using System.Web.Http;
using WebApiSample.Filters;

namespace WebApiSample.Controllers
{
    /// <summary>
    ///     传统风格接口
    /// </summary>
    [FormateResult]
    [TracePerformance]
    [RoutePrefix("api/Classical")]
    public class ClassicalController : BaseApiController
    {
        /// <summary>
        ///     查询所有数据接口
        /// </summary>
        /// <returns>
        ///     字符串数组
        /// </returns>
        [Route("Get")]
        [HttpGet]
        public IEnumerable<string> GetAllData()
        {
            
            return new[] { "value1", "value2" };
        }

        /// <summary>
        ///     通过Id查询数据接口
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>
        ///     字符串
        /// </returns>
        [Route("GetDataById")]
        [HttpGet]
        public string GetDataById(int id)
        {
            return "value";
        }

        /// <summary>
        ///     新增数据接口
        /// </summary>
        /// <param name="value">数据内容</param>
        [Route("AddData")]
        [HttpPost]
        public void AddData([FromBody]string value)
        {
        }

        /// <summary>
        ///     更新数据接口
        /// </summary>
        /// <param name="param">Json格式对象，包含id，value两个属性。例如：{id:3,value:"test"}</param>
        [Route("UpdateById")]
        [HttpPost]
        public void UpdateById([FromBody] dynamic param)
        {
            // 按照如下方式使用
            // int id = param.id;
            // string value = param.value;
        }

        /// <summary>
        ///     删除数据接口
        /// </summary>
        /// <param name="id">主键</param>
        [Route("DeleteById")]
        [HttpPost]
        public void DeleteById(int id)
        {
        }

    }
}
