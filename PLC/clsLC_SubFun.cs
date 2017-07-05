using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using Mirle;

namespace Mirle
{
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

