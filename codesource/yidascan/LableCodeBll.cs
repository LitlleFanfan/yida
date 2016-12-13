﻿using Newtonsoft.Json;
using ProduceComm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yidascan.DataAccess;

namespace yidascan {

    public class LableCodeBll {
        public LableCode CalculateCache(PanelInfo pinfo, LableCode lc, List<LableCode> lcs) {
            LableCode lc2 = null;
            int cachecount = (pinfo.OddStatus ? 0 : 1) + (pinfo.EvenStatus ? 0 : 1);
            var tmp = from s in lcs where s.FloorIndex == 0 orderby s.Diameter ascending select s;
            switch (tmp.Count()) {
                case 0://当前层已没有了缓存。//当前布卷直接缓存起来。
                    break;
                case 1://当前层只有一卷缓存。
                case 2://当前层有两卷缓存。
                    if (tmp.Count() == cachecount)//缓存卷数与需要缓存卷数相等
                    {
                        List<LableCode> lcObjs = new List<LableCode>();
                        foreach (LableCode l in tmp) {
                            if (l.Diameter + clsSetting.DiameterDiff < lc.Diameter) { lcObjs.Add(l); }
                        }
                        if (lcObjs.Count > 0) {
                            lc2 = lcObjs[0];
                            lc.GetOutLCode = lc2.LCode;//换掉的标签
                            CalculatePosition(lcs, lc2);//当前布卷直接缓存起来。缓存的两卷中小的拿出来并计算位置。
                        } else {
                            CalculatePosition(lcs, lc);//当前布卷不需要缓存，计算位置。
                        }
                    }
                    break;
            }
            return lc2;
        }

        private bool IsRollInSameSide(LableCode lc, int flindex) {
            return lc.FloorIndex > 0 && lc.FloorIndex % 2 == flindex % 2;
        }

        public void CalculatePosition(List<LableCode> lcs, LableCode lc) {
            lc.FloorIndex = CalculateFloorIndex(lcs);

            decimal z = lc.Floor == 1 ? 0 : LableCode.GetFloorMaxDiameter(lc.PanelNo, lc.Floor);
            decimal r = clsSetting.OddTurn ?
                (lc.Floor % 2 == 1 ? 0 : 90) : //奇数层横放
                (lc.Floor % 2 == 1 ? 90 : 0); //偶数层横放

            decimal xory = CalculateXory(lcs, lc);

            //z,r,x/y
            lc.Coordinates = string.Format("{0},{1},{2}", z, r, xory);
            lc.Cx = r == 0 ? 0 : xory;
            lc.Cy = r == 0 ? xory : 0;
            lc.Cz = z;
            lc.Crz = r;
        }

        private decimal CalculateXory(List<LableCode> lcs, LableCode lc) {
            int index = CalculateFloorIndex(lcs);
            decimal xory;
            if (index <= 2) {
                xory = lc.FloorIndex % 2 == 1 ? lc.Diameter : -clsSetting.RollSep;
            } else {
                var lastRoll = (from s in lcs where IsRollInSameSide(s, lc.FloorIndex)
                                orderby s.FloorIndex descending select s).First();
                xory = (Math.Abs(lastRoll.Cx + lastRoll.Cz) + lastRoll.Diameter + clsSetting.RollSep)
                    * (lc.FloorIndex % 2 == 1 ? 1 : -1);
            }

            return xory;
        }

        private int CalculateFloorIndex(List<LableCode> lcs) {
            var oddcount = lcs.Count(x => { return x.isOddSide(); });
            var evencount = lcs.Count(x => { return x.isEvenSide(); });

            if (oddcount == 0) { return 1; }
            if (evencount == 0) { return 2; }
            return oddcount > evencount ? 2 * evencount + 2 : 2 * oddcount + 1;
        }

        public void CalculatePosition(List<LableCode> lcs, LableCode lc, LableCode lc2) {
            lc.Floor = lc2.Floor;

            //if (lc.Diameter > lc2.Diameter) {
            //    LableCode tmp = lc2;
            //    lc2 = lc;
            //    lc = tmp;
            //}

            CalculatePosition(lcs, lc);
            lc2.FloorIndex = lc.FloorIndex + 2;

            var d = Math.Abs(lc.Cx + lc.Cz) + lc.Diameter + clsSetting.RollSep;
            var xory = d * (lc.FloorIndex % 2 == 1 ? 1 : -1);

            lc2.Coordinates = string.Format("{0},{1},{2}", lc.Cz, lc.Crz, xory);

            lc2.Cx = lc2.Crz == 0 ? 0 : xory;
            lc2.Cy = lc2.Crz == 0 ? xory : 0;
            lc2.Cz = lc.Cz;
            lc2.Crz = lc.Crz;
        }

        /// <summary>
        /// 当前边是否满
        /// </summary>
        /// <param name="lcs">当前层所有标签</param>
        /// <param name="lc">当前标签</param>
        /// <returns></returns>
        public LableCode IsPanelFull(List<LableCode> lcs, LableCode lc) {
            LableCode result = null;
            const decimal MAX_LEN = 800;

            var cache = from s in lcs where s.FloorIndex == 0 select s;
            decimal xory = CalculateXory(lcs, lc);//计算lc的Xory

            foreach (LableCode item in cache) {
                // 当前卷的坐标。
                decimal itemXory = Math.Abs(xory) + lc.Diameter + clsSetting.RollSep;
                // 当前对应的边界余量。
                decimal tmp = MAX_LEN - (item.Diameter + Math.Abs(itemXory));

                if (tmp > clsSetting.EdgeSpace) { continue; }

                if (result == null || (item.Diameter < result.Diameter)) {
                    result = item;
                }
            }
            return result;
        }

        public void GetPanelNo(LableCode lc) {
            // string panelNo = NewPanelNo(dtime, shiftNo);
            string panelNo = PanelGen.NewPanelNo();
            lc.PanelNo = panelNo;
            lc.FloorIndex = 0;
            lc.Floor = 1;
            lc.Coordinates = "";
        }

        public CacheState AreaBCalculate(LableCode lc, out string outCacheLable, List<LableCode> lcs, out string msg) {
            CacheState cState = CacheState.Error;
            outCacheLable = string.Empty;
            msg = string.Empty;
            LableCode lc2 = null;

            if (lcs == null || lcs.Count == 0) {
                // 产生新板号赋予当前标签。
                GetPanelNo(lc);
                LableCode.Update(lc);
                cState = CacheState.Cache;
            } else {
                FloorPerformance fp = FloorPerformance.None;
                PanelInfo pinfo = LableCode.GetPanel(lcs[0].PanelNo);

                Console.WriteLine(pinfo != null); // 这里显示pinfo是null.

                lc.SetupPanelInfo(pinfo);

                if (pinfo.CurrFloor == lcs[0].Floor) {
                    // 最近一层没满。
                    lc2 = IsPanelFull(lcs, lc);

                    if (lc2 != null)//不为NULL，表示满
                    {
                        CalculatePosition(lcs, lc, lc2);//计算位置坐标

                        if (lc.FloorIndex % 2 == 0) {
                            pinfo.EvenStatus = true;
                            fp = FloorPerformance.EvenFinish;
                        } else {
                            pinfo.OddStatus = true;
                            fp = FloorPerformance.OddFinish;
                        }
                    } else {
                        lc2 = CalculateCache(pinfo, lc, lcs);//计算缓存，lc2不为NULL需要缓存
                    }
                }

                if (pinfo.EvenStatus && pinfo.OddStatus)
                    fp = FloorPerformance.BothFinish;

                if (lc2 != null) {
                    if (LableCode.Update(fp, lc, lc2))
                        outCacheLable = lc2.LCode;
                    cState = lc.FloorIndex == 0 ? CacheState.GetThenCache : CacheState.GoThenGet;
                } else {
                    if (LableCode.Update(fp, lc))
                        cState = lc.FloorIndex == 0 ? CacheState.Cache : CacheState.Go;
                }

                if (fp == FloorPerformance.BothFinish && lc.Floor == pinfo.MaxFloor) {
                    PanelEnd(lc.PanelNo, out msg);
                }
            }
            msg = string.Format(@"交地:{0};当前标签:{1};直径:{2};长:{3};缓存状态:{4};取出标签:{5};直径:{6};长:{7};",
                   lc.ToLocation, lc.LCode, lc.Diameter, lc.Length, cState, outCacheLable,
                   (string.IsNullOrEmpty(outCacheLable) ? 0 : lc2.Diameter),
                   (string.IsNullOrEmpty(outCacheLable) ? 0 : lc2.Length));
            return cState;
        }

        public bool PanelEnd(string panelNo, out string msg, bool handwork = false) {
            if (!string.IsNullOrEmpty(panelNo)) {
                // 这个从数据库取似更合理。                
                var data = LableCode.QueryLabelcodeByPanelNo(panelNo);

                if (data == null) {
                    msg = "板号完成失败，未能查到数据库的标签。";
                    return false;
                }

                Dictionary<string, string> erpParam = new Dictionary<string, string>() {
                        { "Board_No", panelNo },  // first item.
                        { "AllBarCode", string.Join(",", data.ToArray()) } // second item.
                    };
                Dictionary<string, string> re = CallWebApi.Post(clsSetting.PanelFinish, erpParam);

                // show result.
                if (re["State"] == "Fail") {
                    msg = string.Format("{0}板号{1}完成失败。{2}", (handwork ? "手工" : "自动"),
                        JsonConvert.SerializeObject(erpParam), re["ERR"]);
                } else {
                    msg = string.Format("{0}板号{1}完成成功。{2}", (handwork ? "手工" : "自动"),
                        JsonConvert.SerializeObject(erpParam), re["Data"]);
                    return true;
                }
            }
            msg = "板号完成失败，板号为空。";
            return false;
        }
    }
}
