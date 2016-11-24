using ProduceComm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yidascan.DataAccess;

namespace yidascan
{

    public class LableCodeBll
    {
        public LableCode CalculateCache(PanelInfo pinfo, LableCode lc, List<LableCode> lcs)
        {
            LableCode lc2 = null;
            int cachecount = (pinfo.OddStatus ? 0 : 1) + (pinfo.EvenStatus ? 0 : 1);
            var tmp = from s in lcs where s.FloorIndex == 0 orderby s.Diameter ascending select s;
            switch (tmp.Count())
            {
                case 0://当前层已没有了缓存。//当前布卷直接缓存起来。
                    break;
                case 1://当前层只有一卷缓存。
                case 2://当前层有两卷缓存。
                    if (tmp.Count() == cachecount)//缓存卷数与需要缓存卷数相等
                    {
                        List<LableCode> lcObjs = new List<LableCode>();
                        foreach (LableCode l in tmp)
                        {
                            if (l.Diameter + clsSetting.DiameterDiff < lc.Diameter)
                            { lcObjs.Add(l); }
                        }
                        if (lcObjs.Count > 0)
                        {
                            lc2 = lcObjs[0];
                            lc.GetOutLCode = lc2.LCode;//换掉的标签
                            CalculatePosition(lcs, lc2);//当前布卷直接缓存起来。缓存的两卷中小的拿出来并计算位置。
                        }
                        else
                        {
                            CalculatePosition(lcs, lc);//当前布卷不需要缓存，计算位置。
                        }
                    }
                    break;
            }
            return lc2;
        }

        public void CalculatePosition(List<LableCode> lcs, LableCode lc)
        {
            decimal sumlen = 0;
            lc.FloorIndex = CalculateFloorIndex(lcs, lc);
            var len = (from s in lcs where s.FloorIndex != 0 && s.FloorIndex % 2 == lc.FloorIndex % 2 select s.Diameter).Sum();
            decimal z = lc.Floor == 1 ? 30 : LableCode.GetFloorMaxDiameter(lc.PanelNo, lc.Floor);
            decimal r = clsSetting.OddTurn && lc.Floor % 2 == 1 ? 90 : 0;
            decimal xory =
                lc.FloorIndex % 2 == 0 ?
                (sumlen - len - lc.Diameter) : //双数加
                (sumlen +//单数减
                (lc.FloorIndex == 1 ? 0 : len));//第1个不用加
            //z,r,x/y
            lc.Coordinates = string.Format("{0},{1},{2}", z, r, xory);
            //CalculateFinish(lcs, lc);//, ref result, z, r, ref xory return result;
        }

        private int CalculateFloorIndex(List<LableCode> lcs, LableCode lc)
        {
            var odd = from s in lcs where s.FloorIndex != 0 && s.FloorIndex % 2 == 1 select s.FloorIndex;
            var even = from s in lcs where s.FloorIndex != 0 && s.FloorIndex % 2 == 0 select s.FloorIndex;
            return odd.Count() == 0 ? 1 :
                    (odd.Count() > even.Count() ?
                    (even.Count() == 0 ? 2 :
                    (even.Max() + 2)) :
                (odd.Max() + 2));
        }

        public void CalculatePosition(List<LableCode> lcs, LableCode lc, LableCode lc2)
        {
            lc.Floor = lc2.Floor;
            if (lc.Diameter > lc2.Diameter)
            {
                LableCode tmp = lc2;
                lc2 = lc;
                lc = tmp;
            }
            CalculatePosition(lcs, lc);
            lc2.FloorIndex = lc.FloorIndex + 2;
            string[] coor = lc.Coordinates.Split(',');

            decimal xory =
               lc.FloorIndex % 2 == 0 ?
               (decimal.Parse(coor[2]) - lc2.Diameter) :
               (decimal.Parse(coor[2]) + lc.Diameter);
            lc2.Coordinates = string.Format("{0},{1},{2}", coor[0], coor[1], xory);
        }

        /// <summary>
        /// 当前边是否满
        /// </summary>
        /// <param name="lcs">当前层所有标签</param>
        /// <param name="lc">当前标签</param>
        /// <returns></returns>
        public LableCode CalculateFinish(List<LableCode> lcs, LableCode lc)
        {
            const decimal MAX_LEN = 800;

            int floorindex = lcs.Count - 1;            
            
            var cache = from s in lcs where s.FloorIndex == 0 orderby s.Diameter ascending select s;
            var len = (from s in lcs where s.FloorIndex != 0 && s.FloorIndex % 2 == floorindex % 2 select s.Diameter).Sum();
            var width = len + lc.Diameter;

            LableCode result = null;
            foreach (LableCode item in cache)
            {
                decimal tmp = Math.Abs(MAX_LEN - (item.Diameter + width));
                if (result == null && tmp < clsSetting.EdgeObligate)
                {
                    result = item;
                }
                else if (tmp < clsSetting.EdgeObligate)
                {
                    decimal re = Math.Abs(MAX_LEN - (result.Diameter + width));
                    if (tmp < re)
                        result = item;
                }
            }

            Console.WriteLine(string.Format("lc.floorindex {1}, calindex {2}, finish {3}, width {0}, result.diameter {4}",
                width, lc.FloorIndex, floorindex, result != null, (result != null ? result.Diameter : 0)));

            return result;
        }

        public void GetPanelNo(LableCode lc, DateTime dtime, int shiftNo)
        {
            string panelNo = NewPanelNo(dtime, shiftNo);
            lc.PanelNo = panelNo;
            lc.FloorIndex = 0;
            lc.Floor = 1;
            lc.Coordinates = "";
        }

        private string NewPanelNo(DateTime dtime, int shiftNo)
        {
            string panelNo = LableCode.GetLastPanelNo(string.Format("{0}", dtime.ToString(clsSetting.LABEL_CODE_DATE_FORMAT)));
            panelNo = string.IsNullOrEmpty(panelNo) 
                ? string.Format("{0}{1}", dtime.ToString(clsSetting.LABEL_CODE_DATE_FORMAT), "0001") 
                : (decimal.Parse(panelNo) + 1).ToString();
            return panelNo;
        }
    }
}
