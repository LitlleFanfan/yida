using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using yidascan.DataAccess;
using yidascan;

namespace RobotTest {
    public partial class Form1 : Form {
        List<LableCode> rolls;

        RobotHelper robot;

        string jobname = "NUMBER9"; // LJE2存于机器人本身的程序名。MAN

        decimal zStart = -1600;

        int index = 0;

        bool isrun = false;

        bool hold = false;

        MessageCenter msgCtr = new MessageCenter();

        public Form1() {
            InitializeComponent();

            DataBind();
            //initbtn();
            timer1.Interval = 500;
        }

        private void DataBind() {
            rolls = new List<LableCode>() {

            //new LableCode() {LCode="122381265003",ToLocation="B10",PanelNo="1611300003",Status=5,Floor=1,FloorIndex=1,Diameter=70,Length=214,Coordinates="30,90,0"},
            //new LableCode() { LCode = "122392404003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 1, FloorIndex = 2, Diameter = 145, Length = 1517, Coordinates = "30,90,-290" },
            //new LableCode() { LCode = "122391115003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 1, FloorIndex = 3, Diameter = 180, Length = 1560, Coordinates = "30,90,250" },
            //new LableCode() { LCode = "122381263003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 1, FloorIndex = 4, Diameter = 215, Length = 1512, Coordinates = "30,90,-360" },
            //new LableCode() { LCode = "122381264003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 1, FloorIndex = 6, Diameter = 215, Length = 1508, Coordinates = "30,90,-575" },
            //new LableCode() { LCode = "122381262003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 2, FloorIndex = 1, Diameter = 210, Length = 1520, Coordinates = "215,0,0" },
            //new LableCode() { LCode = "122393110003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 2, FloorIndex = 2, Diameter = 215, Length = 1496, Coordinates = "215,0,-215" },
            //new LableCode() { LCode = "122393109003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 2, FloorIndex = 3, Diameter = 215, Length = 1500, Coordinates = "215,0,425" },
            //new LableCode() { LCode = "122381009003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 2, FloorIndex = 6, Diameter = 225, Length = 1516, Coordinates = "215,0,-665" },
            //new LableCode() { LCode = "122381171003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 3, FloorIndex = 1, Diameter = 80, Length = 1514, Coordinates = "440,90,0" },
            //new LableCode() { LCode = "122381173003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 3, FloorIndex = 2, Diameter = 205, Length = 1521, Coordinates = "440,90,-410" },
            //new LableCode() { LCode = "122381170003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 3, FloorIndex = 4, Diameter = 230, Length = 1515, Coordinates = "440,90,-435" },
            //new LableCode() { LCode = "122393107003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 3, FloorIndex = 5, Diameter = 230, Length = 1506, Coordinates = "440,90,310" },
            //new LableCode() { LCode = "122381172003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 3, FloorIndex = 6, Diameter = 235, Length = 1521, Coordinates = "440,90,-670" },
            //new LableCode() { LCode = "122381007003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 3, FloorIndex = 7, Diameter = 235, Length = 1511, Coordinates = "440,90,540" },
            //new LableCode() { LCode = "122393108003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 4, FloorIndex = 1, Diameter = 85, Length = 1518, Coordinates = "675,0,0" },
            //new LableCode() { LCode = "122381260003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 4, FloorIndex = 2, Diameter = 125, Length = 1644, Coordinates = "675,0,-125" },
            //new LableCode() { LCode = "122381261003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 4, FloorIndex = 3, Diameter = 190, Length = 1505, Coordinates = "675,0,275" },
            //new LableCode() { LCode = "122381257003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 4, FloorIndex = 4, Diameter = 190, Length = 1516, Coordinates = "675,0,-315" },
            //new LableCode() { LCode = "122380830003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 4, FloorIndex = 7, Diameter = 215, Length = 1511, Coordinates = "675,0,345" },
            //new LableCode() { LCode = "122381259003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 4, FloorIndex = 8, Diameter = 200, Length = 1516, Coordinates = "675,0,-585" },
            //new LableCode() { LCode = "122387601003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 5, FloorIndex = 1, Diameter = 70, Length = 1563, Coordinates = "890,90,0" },
            //new LableCode() { LCode = "122393621003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 5, FloorIndex = 2, Diameter = 230, Length = 1511, Coordinates = "890,90,-230" },
            //new LableCode() { LCode = "122393622003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 5, FloorIndex = 5, Diameter = 230, Length = 1519, Coordinates = "890,90,300" },
            //new LableCode() { LCode = "122393620003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 5, FloorIndex = 6, Diameter = 230, Length = 1520, Coordinates = "890,90,-690" },
            //new LableCode() { LCode = "122385878003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 6, FloorIndex = 1, Diameter = 230, Length = 1519, Coordinates = "1120,0,0" },
            //new LableCode() { LCode = "122393619003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 6, FloorIndex = 2, Diameter = 230, Length = 1518, Coordinates = "1120,0,-460" },
            //new LableCode() { LCode = "122385876003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 6, FloorIndex = 3, Diameter = 115, Length = 1518, Coordinates = "1120,0,345" },
            //new LableCode() { LCode = "122385877003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 6, FloorIndex = 4, Diameter = 235, Length = 1523, Coordinates = "1120,0,-695" },
            //new LableCode() { LCode = "122385875003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 6, FloorIndex = 5, Diameter = 235, Length = 1514, Coordinates = "1120,0,460" },
            //new LableCode() { LCode = "122385873003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 7, FloorIndex = 1, Diameter = 235, Length = 1515, Coordinates = "1355,90,0" },
            //new LableCode() { LCode = "122393862003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 7, FloorIndex = 2, Diameter = 225, Length = 1509, Coordinates = "1355,90,-225" },
            //new LableCode() { LCode = "122393861003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 7, FloorIndex = 3, Diameter = 225, Length = 1510, Coordinates = "1355,90,460" },
            //new LableCode() { LCode = "122385874003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 7, FloorIndex = 4, Diameter = 235, Length = 1518, Coordinates = "1355,90,-460" },
            //new LableCode() { LCode = "122393859003", ToLocation = "B10", PanelNo = "1611300003", Status = 5, Floor = 7, FloorIndex = 5, Diameter = 230, Length = 1506, Coordinates = "1355,90,685" }//,
            //new LableCode() { LCode = "122391989003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 1, FloorIndex = 1, Diameter = 115, Length = 1501, Coordinates = "30,90,0" },
            //new LableCode() { LCode = "122390628003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 1, FloorIndex = 2, Diameter = 65, Length = 1510, Coordinates = "30,90,-65" },
            //new LableCode() { LCode = "122389654003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 1, FloorIndex = 3, Diameter = 140, Length = 1518, Coordinates = "30,90,115" },
            //new LableCode() { LCode = "122392036000", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 1, FloorIndex = 4, Diameter = 145, Length = 1516, Coordinates = "30,90,-335" },
            //new LableCode() { LCode = "122381238003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 1, FloorIndex = 6, Diameter = 205, Length = 1459, Coordinates = "30,90,-560" },
            //new LableCode() { LCode = "122391532003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 2, FloorIndex = 1, Diameter = 110, Length = 1505, Coordinates = "205,0,0" },
            //new LableCode() { LCode = "122391199003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 2, FloorIndex = 2, Diameter = 115, Length = 988, Coordinates = "205,0,-115" },
            //new LableCode() { LCode = "122389571003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 2, FloorIndex = 3, Diameter = 115, Length = 1498, Coordinates = "205,0,110" },
            //new LableCode() { LCode = "122390626003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 2, FloorIndex = 6, Diameter = 240, Length = 1528, Coordinates = "205,0,-520" },
            //new LableCode() { LCode = "122390623003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 2, FloorIndex = 7, Diameter = 165, Length = 1518, Coordinates = "205,0,390" },
            //new LableCode() { LCode = "122390516003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 3, FloorIndex = 1, Diameter = 60, Length = 1465, Coordinates = "445,90,0" },
            //new LableCode() { LCode = "122390618003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 3, FloorIndex = 2, Diameter = 110, Length = 1513, Coordinates = "445,90,-220" },
            //new LableCode() { LCode = "122390511003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 3, FloorIndex = 3, Diameter = 110, Length = 1496, Coordinates = "445,90,60" },
            //new LableCode() { LCode = "122390513003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 3, FloorIndex = 4, Diameter = 180, Length = 1475, Coordinates = "445,90,-470" },
            //new LableCode() { LCode = "122393822003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 3, FloorIndex = 7, Diameter = 240, Length = 1522, Coordinates = "445,90,400" },
            //new LableCode() { LCode = "122393820003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 4, FloorIndex = 1, Diameter = 230, Length = 1520, Coordinates = "685,0,0" },
            //new LableCode() { LCode = "122393819003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 4, FloorIndex = 2, Diameter = 230, Length = 1516, Coordinates = "685,0,-230" },
            //new LableCode() { LCode = "122393625003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 4, FloorIndex = 3, Diameter = 90, Length = 1499, Coordinates = "685,0,230" },
            //new LableCode() { LCode = "122393821003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 4, FloorIndex = 4, Diameter = 230, Length = 111, Coordinates = "685,0,-460" },
            //new LableCode() { LCode = "122393624003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 5, FloorIndex = 1, Diameter = 115, Length = 1503, Coordinates = "915,90,0" },
            //new LableCode() { LCode = "122379225003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 5, FloorIndex = 2, Diameter = 165, Length = 1478, Coordinates = "915,90,-330" },
            //new LableCode() { LCode = "122379223003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 5, FloorIndex = 3, Diameter = 55, Length = 243, Coordinates = "915,90,115" },
            //new LableCode() { LCode = "122379224003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 5, FloorIndex = 4, Diameter = 170, Length = 1467, Coordinates = "915,90,-505" },
            //new LableCode() { LCode = "122379218003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 5, FloorIndex = 5, Diameter = 190, Length = 1482, Coordinates = "915,90,170" },
            //new LableCode() { LCode = "122393809003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 5, FloorIndex = 6, Diameter = 80, Length = 1524, Coordinates = "915,90,-415" },
            //new LableCode() { LCode = "122379219003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 5, FloorIndex = 7, Diameter = 190, Length = 1471, Coordinates = "915,90,360" },
            //new LableCode() { LCode = "122379215003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 5, FloorIndex = 8, Diameter = 190, Length = 1472, Coordinates = "915,90,-605" },
            //new LableCode() { LCode = "122393662003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 6, FloorIndex = 1, Diameter = 180, Length = 1511, Coordinates = "1105,0,0" },
            //new LableCode() { LCode = "122393661003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 6, FloorIndex = 2, Diameter = 175, Length = 1522, Coordinates = "1105,0,-350" },
            //new LableCode() { LCode = "122393660003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 6, FloorIndex = 3, Diameter = 190, Length = 1518, Coordinates = "1105,0,180" },
            //new LableCode() { LCode = "122393711003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 6, FloorIndex = 4, Diameter = 125, Length = 1515, Coordinates = "1105,0,-300" },
            //new LableCode() { LCode = "122393313003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 6, FloorIndex = 5, Diameter = 205, Length = 1513, Coordinates = "1105,0,370" },
            //new LableCode() { LCode = "122393807003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 6, FloorIndex = 8, Diameter = 195, Length = 1519, Coordinates = "1105,0,-680" },
            //new LableCode() { LCode = "122393709003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 7, FloorIndex = 1, Diameter = 185, Length = 1514, Coordinates = "1310,90,0" },
            //new LableCode() { LCode = "122393707003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 7, FloorIndex = 2, Diameter = 190, Length = 1506, Coordinates = "1310,90,-380" },
            //new LableCode() { LCode = "122393393003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 7, FloorIndex = 4, Diameter = 115, Length = 1516, Coordinates = "1310,90,-305" },
            //new LableCode() { LCode = "122393710003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 7, FloorIndex = 5, Diameter = 180, Length = 1510, Coordinates = "1310,90,365" },
            //new LableCode() { LCode = "122393706003", ToLocation = "B09", PanelNo = "1611300002", Status = 5, Floor = 7, FloorIndex = 6, Diameter = 200, Length = 1507, Coordinates = "1310,90,-505" }











            //new LableCode() { LCode = "122361081003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 1, FloorIndex = 1, Diameter = 180, Length = 1566, Coordinates = "30,90,0" },
            //new LableCode() { LCode = "122361080003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 1, FloorIndex = 2, Diameter = 130, Length = 1539, Coordinates = "30,90,-130" },
            //new LableCode() { LCode = "122362060003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 1, FloorIndex = 3, Diameter = 160, Length = 1513, Coordinates = "30,90,180" },
            //new LableCode() { LCode = "122362059003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 1, FloorIndex = 4, Diameter = 160, Length = 1574, Coordinates = "30,90,-290" },
            //new LableCode() { LCode = "122362058003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 1, FloorIndex = 5, Diameter = 155, Length = 1584, Coordinates = "30,90,340" },
            //new LableCode() { LCode = "122355585003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 1, FloorIndex = 6, Diameter = 115, Length = 1506, Coordinates = "30,90,-405" },
            new LableCode() { LCode = "122361083003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 1, FloorIndex = 7, Diameter = 185, Length = 1570, Coordinates = "30,0,495" },
            new LableCode() { LCode = "122361082003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 1, FloorIndex = 8, Diameter = 180, Length = 1553, Coordinates = "30,0,-585" },
            //new LableCode() { LCode = "122356531003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 2, FloorIndex = 1, Diameter = 105, Length = 1529, Coordinates = "185,0,0" },
            //new LableCode() { LCode = "122362055003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 2, FloorIndex = 2, Diameter = 175, Length = 1521, Coordinates = "185,0,-175" },
            //new LableCode() { LCode = "122362054003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 2, FloorIndex = 3, Diameter = 190, Length = 1525, Coordinates = "185,0,105" },
            //new LableCode() { LCode = "122362053003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 2, FloorIndex = 4, Diameter = 185, Length = 1517, Coordinates = "185,0,-360" },
            //new LableCode() { LCode = "122362324003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 2, FloorIndex = 5, Diameter = 115, Length = 231, Coordinates = "185,0,295" },
            //new LableCode() { LCode = "122362317003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 2, FloorIndex = 6, Diameter = 165, Length = 1517, Coordinates = "185,0,-525" },
            new LableCode() { LCode = "122361079003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 2, FloorIndex = 7, Diameter = 190, Length = 1518, Coordinates = "185,90,410" },
            new LableCode() { LCode = "122362056003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 2, FloorIndex = 8, Diameter = 190, Length = 115, Coordinates = "185,90,-715" },
            //new LableCode() { LCode = "122362323003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 3, FloorIndex = 1, Diameter = 110, Length = 1507, Coordinates = "375,90,0" },
            //new LableCode() { LCode = "122362314003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 3, FloorIndex = 2, Diameter = 100, Length = 1511, Coordinates = "375,90,-100" },
            //new LableCode() { LCode = "122362313003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 3, FloorIndex = 3, Diameter = 160, Length = 1518, Coordinates = "375,90,110" },
            //new LableCode() { LCode = "122348048003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 3, FloorIndex = 4, Diameter = 175, Length = 1515, Coordinates = "375,90,-275" },
            //new LableCode() { LCode = "122362316003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 3, FloorIndex = 5, Diameter = 195, Length = 1520, Coordinates = "375,90,465" },
            new LableCode() { LCode = "122362315003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 3, FloorIndex = 6, Diameter = 195, Length = 1520, Coordinates = "375,0,-665" },
            new LableCode() { LCode = "122345184003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 3, FloorIndex = 7, Diameter = 200, Length = 1524, Coordinates = "375,0,660" },
            //new LableCode() { LCode = "122347962003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 4, FloorIndex = 1, Diameter = 160, Length = 1506, Coordinates = "575,0,0" },
            //new LableCode() { LCode = "122361103003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 4, FloorIndex = 2, Diameter = 190, Length = 1511, Coordinates = "575,0,-190" },
            //new LableCode() { LCode = "122361102003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 4, FloorIndex = 3, Diameter = 75, Length = 2027, Coordinates = "575,0,160" },
            //new LableCode() { LCode = "122361101003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 4, FloorIndex = 4, Diameter = 165, Length = 1568, Coordinates = "575,0,-355" },
            //new LableCode() { LCode = "122361100003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 4, FloorIndex = 5, Diameter = 165, Length = 1514, Coordinates = "575,0,235" },
            //new LableCode() { LCode = "122354748003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 4, FloorIndex = 6, Diameter = 205, Length = 1530, Coordinates = "575,0,-560" },
            new LableCode() { LCode = "122357519003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 4, FloorIndex = 7, Diameter = 150, Length = 1496, Coordinates = "575,90,295" },
            new LableCode() { LCode = "122361104003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 4, FloorIndex = 9, Diameter = 195, Length = 1521, Coordinates = "575,90,445" }//,
            //new LableCode() { LCode = "122363112003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 5, FloorIndex = 1, Diameter = 120, Length = 1515, Coordinates = "780,90,0" },
            //new LableCode() { LCode = "122363315003", ToLocation = "B10", PanelNo = "1611240008", Status = 0, Floor = 5, FloorIndex = 2, Diameter = 70, Length = 1504, Coordinates = "780,90,-70" }
        };
            dataGridView1.DataSource = rolls;
        }

        private void btnStart_Click(object sender, EventArgs e) {
            isrun = true;
            initbtn();
            StartRobotTask();
        }

        void initbtn() {
            btnStart.Enabled = !isrun;
            txtIP.Enabled = !isrun;
            btnAddRoll.Enabled = isrun;
            btnStop.Enabled = isrun;
            btnRun.Enabled = hold;
            btnHold.Enabled = isrun;
        }

        private void btnStop_Click(object sender, EventArgs e) {
            isrun = false;
            initbtn();
            robot.Dispose();
        }

        private void btnAddRoll_Click(object sender, EventArgs e) {
            if (index < rolls.Count) {
                PushInQueue(rolls[index], "A");
                index++;
            } else {
                msgCtr.Push("Havn't othar roll");
            }
        }

        private void StartRobotTask() {
            Task.Factory.StartNew(() => {
                robot = new RobotHelper(txtIP.Text, jobname, msgCtr);
                robot.JobLoop(ref isrun, ref hold);
                robot.AlarmTask(ref isrun, ref hold);
            });
        }

        private void PushInQueue(LableCode label, string side) {
            var state = PanelState.LessHalf;
            if (label.Floor < 6) { state = PanelState.HalfFull; }

            // lc.Coordinates = string.Format("{0},{1},{2}", z, r, xory);
            var ar_s = label.Coordinates.Split(new char[] { ',' });
            var ar = (from item in ar_s select decimal.Parse(item)).ToList();
            decimal x = 0; decimal y = 0;

            if (ar[1] == 0) {
                x = ar[2];
                y = 0;
            } else {
                x = 0;
                y = ar[2];
            }

            var z = ar[0] + zStart;
            var rz = ar[1];
            if (ar[2] > 0) {
                rz = rz == 0 ? 180 : (rz * -1);
            }
            msgCtr.Push(string.Format("Add roll {0}; x:{1}; y:{2}; z:{3}; rz:{4};", rolls[index].LCode, x, y, x, rz));
            var roll = new RollPosition(side, label.ToLocation, state, x, y, z, rz);
            lock (RobotHelper.robotJobs) {
                RobotHelper.robotJobs.AddRoll(roll);
            }
        }

        public void ViewMsg() {
            foreach (string s in msgCtr.GetAll()) {
                txtLog.Text = string.Format("{0}\r\n{1}", s, txtLog.Text);
            }
        }

        private void btnReset_Click(object sender, EventArgs e) {
            index = 0;
            DataBind();
        }

        private void btnHold_Click(object sender, EventArgs e) {
            robot.rCtrl.Hold(hold);
            hold = !hold;
            btnRun.Enabled = hold;
        }

        private void btnRun_Click(object sender, EventArgs e) {
            robot.rCtrl.StartJob(jobname);
        }

        private void timer1_Tick(object sender, EventArgs e) {
            ViewMsg();
        }
    }
}
