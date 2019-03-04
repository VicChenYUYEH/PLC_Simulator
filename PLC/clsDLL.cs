using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Mirle;

namespace Component
{
    public class clsDB
    {

        public static SqlConnection SqlDBConn;
        public static SqlTransaction SqlDBTran;

        public static bool bConnDB;

        public static DB gobjDB1 = new DB();//MSSQL
        public static DB gobjDB2 = new DB();//SQLite

        public static void subGetDBConfig(string strIniPathName, string strAppName = "Data Base")
        {
            string strKeyName = string.Empty;
            string strDefault = string.Empty;
            string strTemp = string.Empty;
            string strEM = string.Empty;

            #region Database
            strKeyName = "DBMS";
            strDefault = "MSSQL";
            strTemp = clsLC_SubFun.funReadParam(strIniPathName, strAppName, strKeyName, strDefault);
            //把列舉型別常數名稱轉回列舉
            gobjDB1.DBType = (DB.enuDatabaseType)Enum.Parse(typeof(DB.enuDatabaseType), strTemp);

            strKeyName = "DbServer";
            strDefault = "127.0.0.1";
            gobjDB1.DBServer = clsLC_SubFun.funReadParam(strIniPathName, strAppName, strKeyName, strDefault);


            strKeyName = "FODbServer";
            strDefault = "";
            gobjDB1.FODBServer = clsLC_SubFun.funReadParam(strIniPathName, strAppName, strKeyName, strDefault);


            strKeyName = "DataBase";
            strDefault = "ERCS";
            gobjDB1.DBName = clsLC_SubFun.funReadParam(strIniPathName, strAppName, strKeyName, strDefault);

            strKeyName = "DbUser";
            strDefault = "ERCS";
            gobjDB1.DBUser = clsLC_SubFun.funReadParam(strIniPathName, strAppName, strKeyName, strDefault);

            strKeyName = "DbPswd";
            strDefault = "ERCS";
            gobjDB1.DBPassword = clsLC_SubFun.funReadParam(strIniPathName, strAppName, strKeyName, strDefault);

            if (gobjDB1.DBType == DB.enuDatabaseType.MSSQL)
            {
                if (gobjDB1.funOpenDB_SQLMARS(ref strEM) != ErrDef.ProcSuccess)
                {
                    MessageBox.Show("Open Database#1 Fail !");
                    Environment.Exit(0);
                }
            }
            else if (gobjDB1.DBType == DB.enuDatabaseType.Oracle_OracleClient)
            {
                if (gobjDB1.funOpenDB(ref strEM) != ErrDef.ProcSuccess)
                {
                    MessageBox.Show("Open Database#1 Fail !");
                    Environment.Exit(0);
                }
            }

            #endregion

            #region SQLite
            strKeyName = "DBMS";
            strDefault = "SQLITE";
            gobjDB2.DBType = DB.enuDatabaseType.SQLite;


            strKeyName = "DataBase";
            strDefault = "ERCS";
            gobjDB2.DBName = @"C:\深超\LIFTERCODE.DB";

            gobjDB2.funOpenDB(ref strEM);
            #endregion
        }

    }
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
    public class clsLC_SubFun
    {
        //API
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString
            (string section, string key, string def, StringBuilder retVal, int size, string filePath);


        public static void funInitSystem(string strIniFileName = "")
        {
            string strIniPathName = string.Empty;
            string sIniFileName = string.Empty;
            string strRet = string.Empty;

            subGetCmdLine();

            if (strIniFileName == "")
                sIniFileName = clsLC_Def.gstrIniFileName;
            else
                sIniFileName = strIniFileName;

            if (clsLC_Def.gstrIniPath == "")
                clsLC_Def.gstrIniPath = Application.StartupPath + "\\" + sIniFileName;
            else
                clsLC_Def.gstrIniPath = clsLC_Def.gstrIniPath + sIniFileName;

            subGetSysConfig(clsLC_Def.gstrIniPath);
        }

        public static void subGetSysConfig(string strIniPathName)
        {
            string strAppName = string.Empty;
            string strKeyName = string.Empty;
            string strDefault = string.Empty;
            string strTemp = string.Empty;
            bool bolRet = false;

            strAppName = "SystemConfig";

            strKeyName = "ControllerID";
            strDefault = "1ALFT010";
            clsLC_Def.gstrControllerID = funReadParam(strIniPathName, strAppName, strKeyName, strDefault);


            strKeyName = "TRUQty";
            strDefault = "2";
            strTemp = funReadParam(strIniPathName, strAppName, strKeyName, strDefault);
            bolRet = Mirle.Tools.ComSubFun.IsNumeric(strTemp, out clsLC_Def.gintTRUQty);

            strKeyName = "CONQty";
            strDefault = "4";
            strTemp = funReadParam(strIniPathName, strAppName, strKeyName, strDefault);
            bolRet = Mirle.Tools.ComSubFun.IsNumeric(strTemp, out clsLC_Def.gintCONQty);



            strAppName = "PLC";

            strKeyName = "ActLogicalStationNo";
            strDefault = "0";
            strTemp = funReadParam(strIniPathName, strAppName, strKeyName, strDefault);
            bolRet = Mirle.Tools.ComSubFun.IsNumeric(strTemp, out clsLC_Def.gintPLCStationNo);



            strAppName = "PLCR";

        }


        public static void subGetCmdLine()
        {
            string[] straComArgs = Environment.GetCommandLineArgs();
            for (int i = 1; i < straComArgs.Length; i++)
            {
                if (straComArgs[i].IndexOf("/PATH", 0) >= 0)
                {
                    clsLC_Def.gstrIniPath = straComArgs[i].Substring(straComArgs[i].ToUpper().IndexOf("=", 0) + 1).Trim();
                }
            }
        }




        /// <summary>
        /// 讀取ini檔的單一欄位
        /// </summary>
        /// <param name="sFileName">INI檔檔名</param>
        /// <param name="sAppName">區段名</param>
        /// <param name="sKeyName">KEY名稱</param>
        /// <param name="strDefault">Default</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string funReadParam(string sFileName, string sAppName, string sKeyName, string strDefault = "")
        {
            StringBuilder sResult = new StringBuilder(255);
            int intResult;
            intResult = GetPrivateProfileString(sAppName, sKeyName, strDefault, sResult, sResult.Capacity, sFileName);
            return sResult.ToString().Trim();
        }




        public static void funStr2Array_ByArrayLength(string strInput, int[] Length, ref string[] strOutput)
        {
            int intStart = 0;
            if (Length.Length == strOutput.Length)
            {
                for (int I = 0; I < strOutput.Length; I++)
                {
                    strOutput[I] = strInput.Substring(intStart, Length[I]).Trim();
                    intStart = intStart + Length[I];
                }
            }
        }
    }
}

