using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProduceComm;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace yidascan.DataAccess
{
    public class OPCParam
    {

        /// <summary>
        /// 直径
        /// </summary>
        public string Diameter { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public string Length { get; set; }

        /// <summary>
        /// 交地
        /// </summary>
        public string ToLocationArea { get; set; }

        /// <summary>
        /// 交地
        /// </summary>
        public string ToLocationNo { get; set; }

        /// <summary>
        /// 层
        /// </summary>
        public string Floor { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 相机编号
        /// </summary>
        public string CameraNo { get; set; }

        /// <summary>
        /// 称重
        /// </summary>
        public string GetWeigh { get; set; }

        /// <summary>
        /// 缓存前信号
        /// </summary>
        public string BeforCacheStatus { get; set; }
        /// <summary>
        /// 缓存前标签 
        /// </summary>
        public string BeforCacheLable1 { get; set; }
        /// <summary>
        /// 缓存前标签 
        /// </summary>
        public string BeforCacheLable2 { get; set; }

        /// <summary>
        /// 是否缓存
        /// </summary>
        public string IsCache { get; set; }

        /// <summary>
        /// 从缓存取出的标签
        /// </summary>
        public string GetOutLable1 { get; set; }
        /// <summary>
        /// 从缓存取出的标签
        /// </summary>
        public string GetOutLable2 { get; set; }

        /// <summary>
        /// 是否最后一个
        /// </summary>
        public string IsLastOftPanel { get; set; }

        /// <summary>
        /// 机器人处标签1
        /// </summary>
        public string RobotLable11 { get; set; }

        /// <summary>
        /// 机器人处标签1
        /// </summary>
        public string RobotLable12 { get; set; }
        /// <summary>
        /// 机器人处标签2
        /// </summary>
        public string RobotLable21 { get; set; }
        /// <summary>
        /// 机器人处标签2
        /// </summary>
        public string RobotLable22 { get; set; }

        public string ScanState { get; set; }

        public string ScanLable1 { get; set; }

        public string ScanLable2 { get; set; }

        public string ACAreaPanelFinish { get; set; }

        public static DataTable Query()
        {
            string sql = "select Name,Code,Remark from OPCParam order by [Index] DESC";
            return DataAccess.CreateDataAccess.sa.Query(sql);
        }

        public static OPCParam GetCfg()
        {
            DataTable dt = Query();
            if (dt == null || dt.Rows.Count < 1)
            {
                return null;
            }
            OPCParam opcparam = new OPCParam();

            foreach (DataRow dr in dt.Rows)
            {
                foreach (PropertyInfo property in typeof(OPCParam).GetProperties())
                {
                    if (property.Name == dr["Name"].ToString())
                    {
                        property.SetValue(opcparam, dr["Code"].ToString());
                    }
                }
            }
            return opcparam;
        }
    }
}
