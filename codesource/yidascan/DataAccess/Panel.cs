using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yidascan.DataAccess {
    public class PanelInfo {
        public string Remark { get;set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool EvenStatus { get; set; }
        public bool OddStatus { get; set; }
        public int CurrFloor { get; set; }
        public int Status { get; set; }
        public string PanelNo { get; set; }
        public int SequenceNo { get; set; }
    }
}
