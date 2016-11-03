using System.Collections.Generic;
using System.Web.Http;
using WebApiSample.Filters;

namespace WebApiSample.Controllers
{
    /// <summary>
    ///  Restful 风格接口样例
    /// </summary>
    [FormateResult]
    [TracePerformance]
    public class ValuesController : BaseApiController
    {
        // GET api/values
        /// <summary>
        ///     查询所有数据接口
        /// </summary>
        /// <returns>
        ///     字符串数组
        /// </returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        /// <summary>
        ///     通过Id查询数据接口
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>
        ///     字符串
        /// </returns>
        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        /// <summary>
        ///     新增数据接口
        /// </summary>
        /// <param name="value">数据内容</param>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        /// <summary>
        ///     更新数据接口
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="value">更新后的值</param>
        [HttpPut]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        /// <summary>
        ///     删除数据接口
        /// </summary>
        /// <param name="id">主键</param>
        [HttpDelete]
        public void Delete(int id)
        {
        }


    }
}
