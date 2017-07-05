using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mirle;
using clsLifterMPLC;

namespace Mirle
{
    public class clsLC_Def
    {
        /// <summary>
        /// Define Stocker Unique ID
        /// </summary>
        public static string gstrControllerID = "L10STK";

        /// <summary>
        /// Ini檔名稱
        /// </summary>
        /// <remarks></remarks>
        public static string gstrIniFileName = "LCS.ini";

        /// <summary>
        /// MPLC Connection Station Number
        /// </summary>
        public static int gintPLCStationNo = 33;

        /// <summary>
        /// 記錄Ini檔的相對路徑
        /// </summary>
        /// <remarks></remarks>
        public static string gstrIniPath = string.Empty;

        /// <summary>
        /// Define TRU Qty
        /// </summary>
        public static int gintTRUQty = 2;


        /// <summary>
        /// Define Conveyor Qty
        /// </summary>
        public static int gintCONQty = 6;

        /// <summary>
        /// For TRU
        /// </summary>
        public static Dictionary<int, clsLC_Def.PortDef> dicTRUDef = new Dictionary<int, clsLC_Def.PortDef>();
        /// <summary>
        /// For Conveyor
        /// </summary>
        public static Dictionary<int, clsLC_Def.PortDef> dicCONDef = new Dictionary<int, clsLC_Def.PortDef>();

        public struct PortDef
        {
            public int PLCindex;
            public int PLCPortID;
            public int PortType;
            public int Floor;
            public string HostEQPortID;
            public int Stage;
        }
    }
}