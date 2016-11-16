using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using WebApiSample.Filters;

namespace WebApiSample.Controllers
{
    /// <summary>
    ///     FI接口
    /// </summary>
    [FormateResult]
    //[TracePerformance]
    [RoutePrefix("api/FI")]
    public class FIController : BaseApiController
    {

        /// <summary>
        ///     接口 1 获取板位，写入数据
        /// </summary>
        /// <param name="Bar_Code">条码编号</param>
        [Route("GetLocation")]
        [HttpPost]
        public DataTable GetLocation(string Bar_Code)  //接口 1 获取板位，写入数据
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("LOCATION"));
            DataRow dr = dt.NewRow();
            dr["LOCATION"] = location[new Random().Next(0, location.Count - 1)];
            dt.Rows.Add(dr);
            return dt;
        }

        List<string> location = new List<string>() { "B03", "B02", "B01", "A01", "A02", "A03", "C01", "C02", "C03" };//, "A04", "A05", "A06", "A07", "A08", "A09", "A10", "A11", "B02","B01"
        /// <summary>
        ///     接口 3 完成码垛
        /// </summary>
        /// <param name="Board_No">板号</param>
        ///  <param name="AllBarCode">条码列表</param>
        [Route("getDataForFinish")]
        [HttpPost]
        public DataTable getDataForFinish(string Board_No, string AllBarCode)//接口 3 完成码垛
        {
            //string sql = " Exec QCMDB.dbo.USP_FISTACK_FINISH '" + Board_No + "'," + "'" + AllBarCode + "'";

            //SqlConnection con = new SqlConnection(sqlcon);
            //SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            //DataTable dt = new DataTable();
            //ad.Fill(dt);
            //return dt;
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("result"));
            DataRow dr = dt.NewRow();
            dr["result"] = "OK";
            dt.Rows.Add(dr);
            return dt;
        }

        /// <summary>
        ///     接口2 计算重量
        /// </summary>
        [Route("getDataForWeight")]
        [HttpPost]
        public DataTable getDataForWeight()//接口2 计算重量
        {
            //string sql = " Exec QCMDB.dbo.USP_FISTACK_Weight ";

            //SqlConnection con = new SqlConnection(sqlcon);
            //SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            //DataTable dt = new DataTable();
            //ad.Fill(dt);
            //return dt;
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("result"));
            DataRow dr = dt.NewRow();
            dr["result"] = "OK";
            dt.Rows.Add(dr);
            return dt;
        }

        string sqlcon
        {
            get
            {
                if (string.IsNullOrEmpty(_sqlcon))
                {
                    _sqlcon = GetConfigString("sqlcon");
                }
                return _sqlcon;
            }
        }
        private static string _sqlcon;

    }
}
