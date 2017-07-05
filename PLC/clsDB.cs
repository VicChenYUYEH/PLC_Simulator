using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;



namespace Mirle
{
    public class clsDB
    {

        public static SqlConnection SqlDBConn;
        public static SqlTransaction SqlDBTran;

        public static bool bConnDB;

        public static Mirle.DB gobjDB1 = new Mirle.DB();//MSSQL
        public static Mirle.DB gobjDB2 = new Mirle.DB();//SQLite

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
}

