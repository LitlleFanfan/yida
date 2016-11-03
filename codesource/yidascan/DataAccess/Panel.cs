using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yidascan.DataAccess
{
    public class PanelInfo
    {
        int sequenceNo;
        string panelNo;
        int status;
        int currFloor;
        bool oddStatus;
        bool evenStatus;
        DateTime createDate;
        DateTime updateDate;
        string remark;

        public string Remark
        {
            get
            {
                return remark;
            }

            set
            {
                remark = value;
            }
        }

        public DateTime UpdateDate
        {
            get
            {
                return updateDate;
            }

            set
            {
                updateDate = value;
            }
        }

        public DateTime CreateDate
        {
            get
            {
                return createDate;
            }

            set
            {
                createDate = value;
            }
        }

        public bool EvenStatus
        {
            get
            {
                return evenStatus;
            }

            set
            {
                evenStatus = value;
            }
        }

        public bool OddStatus
        {
            get
            {
                return oddStatus;
            }

            set
            {
                oddStatus = value;
            }
        }

        public int CurrFloor
        {
            get
            {
                return currFloor;
            }

            set
            {
                currFloor = value;
            }
        }

        public int Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }

        public string PanelNo
        {
            get
            {
                return panelNo;
            }

            set
            {
                panelNo = value;
            }
        }

        public int SequenceNo
        {
            get
            {
                return sequenceNo;
            }

            set
            {
                sequenceNo = value;
            }
        }
    }
}
