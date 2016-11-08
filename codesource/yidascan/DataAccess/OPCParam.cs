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
    public struct LCodeSignal
    {
        public string LCode1 { get; set; }
        public string LCode2 { get; set; }
        public string Signal { get; set; }
    }
    public class OPCParam
    {
        /// <summary>
        /// 采集处信号
        /// </summary>
        public string ScanState { get; set; }

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

        public string ScanLable1 { get; set; }

        public string ScanLable2 { get; set; }

        public Dictionary<string, LCodeSignal> ACAreaPanelFinish;

        public static DataTable Query(string where = "")
        {
            string sql = string.Format("select Name,Code,Class,Remark from OPCParam {0} order by IndexNo DESC", where);
            return DataAccess.CreateDataAccess.sa.Query(sql);
        }

        public static bool GetNoneCfg(OPCParam param)
        {
            if (param == null)
            {
                param = new OPCParam();
            }
            DataTable dt = Query("where Class='None'");
            if (dt == null || dt.Rows.Count < 1)
            {
                return false;
            }
            foreach (DataRow dr in dt.Rows)
            {
                foreach (PropertyInfo property in typeof(OPCParam).GetProperties())
                {
                    if (property.Name == dr["Name"].ToString())
                    {
                        property.SetValue(param, dr["Code"].ToString());
                    }
                }
            }
            return true;
        }

        private static LCodeSignal GetClassCfg(string cls)
        {
            LCodeSignal param = new LCodeSignal();
            DataTable dt = Query(string.Format("where Class='{0}'", cls));
            if (dt == null || dt.Rows.Count < 1)
            {
                return param;
            }
            foreach (DataRow dr in dt.Rows)
            {
                foreach (PropertyInfo property in typeof(OPCParam).GetProperties())
                {
                    if (property.Name == dr["Name"].ToString())
                    {
                        property.SetValue(param, dr["Code"].ToString());
                    }
                }
            }
            return param;
        }

        public static bool GetACAreaFinishCfg(OPCParam param)
        {
            if (param == null)
            {
                param = new OPCParam();
            }
            DataTable dt = Query("where Class like 'AArea%' or Class like 'CArea%'");
            if (dt == null || dt.Rows.Count < 1)
            {
                return false;
            }
            param.ACAreaPanelFinish = new Dictionary<string, yidascan.DataAccess.LCodeSignal>();
            string tmp;
            foreach (DataRow dr in dt.Rows)
            {
                tmp = dr["Class"].ToString();
                if (!param.ACAreaPanelFinish.ContainsKey(tmp))
                {
                    LCodeSignal p = GetClassCfg(tmp);
                    param.ACAreaPanelFinish.Add(tmp, p);
                }
            }
            return true;
        }
    }
}
