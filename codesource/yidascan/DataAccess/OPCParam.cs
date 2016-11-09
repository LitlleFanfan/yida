using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProduceComm;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using Newtonsoft.Json;

namespace yidascan.DataAccess
{
    public class LCodeSignal
    {
        public string LCode1 { get; set; }
        public string LCode2 { get; set; }
        public string Signal { get; set; }

        public LCodeSignal(string cls)
        {
            DataTable dt = OPCParam.Query(string.Format("where Class='{0}'", cls));
            if (dt == null || dt.Rows.Count < 1)
            {
                return;
            }
            foreach (DataRow dr in dt.Rows)
            {
                foreach (PropertyInfo property in typeof(LCodeSignal).GetProperties())
                {
                    if (property.Name == dr["Name"].ToString())
                    {
                        property.SetValue(this, dr["Code"].ToString());
                    }
                }
            }
        }
    }

    public class OPCScanParam
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

        public string ScanLable1 { get; set; }

        public string ScanLable2 { get; set; }

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

        public OPCScanParam()
        {
            DataTable dt = OPCParam.Query("where Class='Scan'");
            if (dt == null || dt.Rows.Count < 1)
            {
                return;
            }
            foreach (DataRow dr in dt.Rows)
            {
                foreach (PropertyInfo property in typeof(OPCScanParam).GetProperties())
                {
                    if (property.Name == dr["Name"].ToString())
                    {
                        property.SetValue(this, dr["Code"].ToString());
                    }
                }
            }
        }

    }

    public class OPCBeforCacheParam
    {
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

        public OPCBeforCacheParam()
        {
            DataTable dt = OPCParam.Query("where Class='Cache'");
            if (dt == null || dt.Rows.Count < 1)
            {
                return;
            }
            foreach (DataRow dr in dt.Rows)
            {
                foreach (PropertyInfo property in typeof(OPCBeforCacheParam).GetProperties())
                {
                    if (property.Name == dr["Name"].ToString())
                    {
                        property.SetValue(this, dr["Code"].ToString());
                    }
                }
            }
        }

    }

    public class OPCParam
    {
        public OPCScanParam ScanParam;

        public OPCBeforCacheParam CacheParam;

        public LCodeSignal RobotCarryA;

        public LCodeSignal RobotCarryB;

        public LCodeSignal DeleteLCode;

        public string RobotWorkState;

        public string RobotRunState;

        public Dictionary<string, LCodeSignal> ACAreaPanelFinish;

        public Dictionary<string, string> BAreaPanelFinish;

        public Dictionary<string, string> BAreaFloorFinish;

        public Dictionary<string, string> BAreaPanelState;

        public static DataTable Query(string where = "")
        {
            string sql = string.Format("select Name,Code,Class,Remark from OPCParam {0} order by IndexNo DESC", where);
            return DataAccess.CreateDataAccess.sa.Query(sql);
        }

        public void Init()
        {
            InitNone();
            InitBAreaPanelFinish();
            //clsSetting.loger.Info(string.Format("BAreaPanelFinish {0}", JsonConvert.SerializeObject(BAreaPanelFinish)));
            InitBAreaFloorFinish();
            //clsSetting.loger.Info(string.Format("BAreaFloorFinish {0}", JsonConvert.SerializeObject(BAreaFloorFinish)));
            InitBAreaPanelState();
            //clsSetting.loger.Info(string.Format("BAreaPanelState {0}", JsonConvert.SerializeObject(BAreaPanelState)));

            ScanParam = new OPCScanParam();
            //clsSetting.loger.Info(string.Format("ScanParam {0}", JsonConvert.SerializeObject(ScanParam)));
            CacheParam = new OPCBeforCacheParam();
            //clsSetting.loger.Info(string.Format("CacheParam {0}", JsonConvert.SerializeObject(CacheParam)));

            RobotCarryA = new LCodeSignal("RobotCarryA");
            //clsSetting.loger.Info(string.Format("CacheParam {0}", JsonConvert.SerializeObject(CacheParam)));

            RobotCarryB = new LCodeSignal("RobotCarryB");
            //clsSetting.loger.Info(string.Format("CacheParam {0}", JsonConvert.SerializeObject(CacheParam)));

            DeleteLCode = new LCodeSignal("DeleteLCode");
            clsSetting.loger.Info(string.Format("DeleteLCode {0}", JsonConvert.SerializeObject(DeleteLCode)));

            GetACAreaFinishCfg();
            //clsSetting.loger.Info(string.Format("ACAreaPanelFinish {0}", JsonConvert.SerializeObject(ACAreaPanelFinish)));
        }

        private bool InitNone()
        {
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
                        property.SetValue(this, dr["Code"].ToString());
                    }
                }
            }
            return true;
        }

        private bool InitBAreaPanelFinish()
        {
            DataTable dt = Query(string.Format("where Class='BAreaPanelFinish'"));
            if (dt == null || dt.Rows.Count < 1)
            {
                return false;
            }
            foreach (DataRow dr in dt.Rows)
            {
                BAreaPanelFinish.Add(dr["Name"].ToString(), dr["Code"].ToString());
            }
            return true;
        }
        private bool InitBAreaFloorFinish()
        {
            DataTable dt = Query(string.Format("where Class='BAreaFloorFinish'"));
            if (dt == null || dt.Rows.Count < 1)
            {
                return false;
            }
            foreach (DataRow dr in dt.Rows)
            {
                BAreaFloorFinish.Add(dr["Name"].ToString(), dr["Code"].ToString());
            }
            return true;
        }
        private bool InitBAreaPanelState()
        {
            DataTable dt = Query(string.Format("where Class='BAreaPanelState'"));
            if (dt == null || dt.Rows.Count < 1)
            {
                return false;
            }
            foreach (DataRow dr in dt.Rows)
            {
                BAreaPanelState.Add(dr["Name"].ToString(), dr["Code"].ToString());
            }
            return true;
        }

        private bool GetACAreaFinishCfg()
        {
            DataTable dt = Query("where Class like 'AArea%' or Class like 'CArea%'");
            if (dt == null || dt.Rows.Count < 1)
            {
                return false;
            }
            ACAreaPanelFinish = new Dictionary<string, yidascan.DataAccess.LCodeSignal>();
            string tmp;
            foreach (DataRow dr in dt.Rows)
            {
                tmp = dr["Class"].ToString();
                if (!ACAreaPanelFinish.ContainsKey(tmp))
                {
                    LCodeSignal p = new LCodeSignal(tmp);
                    ACAreaPanelFinish.Add(tmp, p);
                }
            }
            return true;
        }
    }
}
