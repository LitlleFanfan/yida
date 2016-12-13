using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yidascan;
using yidascan.DataAccess;

using NUnit.Framework;
using ProduceComm;

namespace yidascanTest {
    /// <summary>
    /// 
    /// </summary>
    [NUnit.Framework.TestFixture]
    public class TestCase1 {
        LableCodeBll lcb;
        List<LableCode> lcs = null;
        PanelInfo pinfo = null;

        Random r = new Random();

        [NUnit.Framework.SetUp]
        public void Init() {
            lcb = new LableCodeBll();
            pinfo = new PanelInfo() { PanelNo = "2016090701", CurrFloor = 1, MaxFloor = 8, OddStatus = false, EvenStatus = false, Status = 0 };
            lcs = CreateLabelList();
            PanelGen.Init(DateTime.Now);
        }

        private List<LableCode> CreateLabelList() {
            var l = new List<LableCode>() {
            };
            return l;
        }

        [NUnit.Framework.Test]
        public void Test() {
            for (var i = 0; i < 100; i++) {
                var d = r.Next() % 100 + 100;
                LableCode lc = new LableCode() { LCode = "A00" + d.ToString().PadLeft(4, '0'), ToLocation = "B01", PanelNo = "2016090701", Status = 0, Floor = 1, FloorIndex = 0, Diameter = d, Length = d, Coordinates = "" };
                
                string code = "";
                string msg;
                CacheState cState = lcb.AreaBCalculate(lc, out code, lcs, out msg);
                Console.WriteLine(msg);
                Console.WriteLine(string.Format("\n{0}\n{1}\n", Newtonsoft.Json.JsonConvert.SerializeObject(lc), Newtonsoft.Json.JsonConvert.SerializeObject(lcs)));

                lcs.Add(lc);
            }
        }
    }
}
