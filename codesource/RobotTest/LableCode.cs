using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace yidascan.DataAccess {
    public enum FloorPerformance {
        None,
        OddFinish,
        EvenFinish,
        BothFinish
    }

    public enum CacheState {
        Error,
        Go,
        Cache,
        GetThenCache,
        GoThenGet
    }

    public class LableCode {
        int sequenceNo;

        public int SequenceNo {
            get { return sequenceNo; }
            set { sequenceNo = value; }
        }

        string lCode;

        public string LCode {
            get { return lCode; }
            set { lCode = value; }
        }

        string toLocation;

        public string ToLocation {
            get { return toLocation; }
            set { toLocation = value; }
        }

        string panelNo;

        public string PanelNo {
            get { return panelNo; }
            set { panelNo = value; }
        }

        int status;

        public int Status {
            get { return status; }
            set { status = value; }
        }

        DateTime createDate;

        public DateTime CreateDate {
            get { return createDate; }
            set { createDate = value; }
        }

        DateTime updateDate;

        public DateTime UpdateDate {
            get { return updateDate; }
            set { updateDate = value; }
        }

        string remark;

        public string Remark {
            get { return remark; }
            set { remark = value; }
        }

        string coordinates;
        public string Coordinates {
            get { return coordinates; }
            set { coordinates = value; }
        }

        string getOutLCode;
        public string GetOutLCode {
            get { return getOutLCode; }
            set { getOutLCode = value; }
        }

        decimal diameter;
        public decimal Diameter {
            get { return diameter; }
            set { diameter = value; }
        }

        decimal length;
        public decimal Length {
            get { return length; }
            set { length = value; }
        }

        int floorIndex;
        public int FloorIndex {
            get { return floorIndex; }
            set { floorIndex = value; }
        }

        int floor;
        public int Floor {
            get { return floor; }
            set { floor = value; }
        }

        /// <summary>
        /// 从交地字符串中提取序号
        /// </summary>
        /// <returns></returns>
        public string ParseLocationNo() {
            return string.IsNullOrEmpty(ToLocation) || ToLocation.Length < 3
                ? string.Empty
                : ToLocation.Substring(1, 2);
        }

        /// <summary>
        /// 从交地字符串中提取区域字符。
        /// </summary>
        /// <returns></returns>
        public string ParseLocationArea() {
            return string.IsNullOrEmpty(ToLocation)
                ? string.Empty
                : ToLocation.Substring(0, 1);
        }
    }
}
