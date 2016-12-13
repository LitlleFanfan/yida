using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yidascan;
using yidascan.DataAccess;

namespace yidascanTest
{
    /// <summary>
    /// 
    /// </summary>
    [NUnit.Framework.TestFixture]
    public class TestCase4
    {
        LableCodeBll lcb;
        List<LableCode> lcs = null;
        PanelInfo pinfo = null;

        [NUnit.Framework.SetUp]
        public void Init()
        {
            lcb = new LableCodeBll();
            pinfo = new PanelInfo() { PanelNo = "2016090701", CurrFloor = 1, OddStatus = false, EvenStatus = false, Status = 0 };
            lcs = new List<LableCode>() {
                new LableCode() {LCode="A0001",ToLocation="B01",PanelNo="2016090701",Status=0,Floor=1,FloorIndex=0,Diameter=213,Coordinates="" },

                new LableCode() {LCode="A0002",ToLocation="B01",PanelNo="2016090701",Status=0,Floor=1,FloorIndex=4,Diameter=130,Coordinates="30,90,561" },

                new LableCode() {LCode="A0003",ToLocation="B01",PanelNo="2016090701",Status=0,Floor=1,FloorIndex=3,Diameter=114,Coordinates="30,90,909" },

                new LableCode() {LCode="A0004",ToLocation="B01",PanelNo="2016090701",Status=0,Floor=1,FloorIndex=1,Diameter=109,Coordinates="30,90,800" },

                new LableCode() {LCode="A0005",ToLocation="B01",PanelNo="2016090701",Status=0,Floor=1,FloorIndex=2,Diameter=109,Coordinates="30,90,691" },

                new LableCode() {LCode="A0006",ToLocation="B01",PanelNo="2016090701",Status=0,Floor=1,FloorIndex=0,Diameter=201,Coordinates="" }
            };
        }
        [NUnit.Framework.Test]
        public void Test()
        {
            LableCode lc = new LableCode() { LCode = "A0007", ToLocation = "B01", PanelNo = "2016090701", Status = 0, Floor = 1, FloorIndex = 0, Diameter = 210, Coordinates = "" };
            LableCode lc2 = lcb.IsPanelFull(lcs, lc);
            NUnit.Framework.Assert.AreNotEqual(lc2, null);

            Console.WriteLine(lc2.LCode);
            lcb.CalculatePosition(lcs, lc, lc2);
            NUnit.Framework.Assert.AreNotEqual(lc.FloorIndex, 0);
            NUnit.Framework.Assert.AreNotEqual(lc2.FloorIndex, 0);
            NUnit.Framework.Assert.AreEqual(lc2.LCode, "A0001");
            NUnit.Framework.Assert.AreEqual(lc2.FloorIndex, 7);
            NUnit.Framework.Assert.AreEqual(lc.FloorIndex, 5);
        }
    }
}
