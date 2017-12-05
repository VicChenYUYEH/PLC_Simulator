using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Mirle;


namespace PLC
{
    public class MPLC
    {
        #region Setting
        public string _sConn = "";
        private System.Timers.Timer timRead = new System.Timers.Timer();
        private clsLC_Def.PortDef[] objLFCDef;
        int iLFTPortID ;
        List<int> iConvPortID = new List<int>();
        private string _sTimer = "";
        public string sTimer
        {
            get { return _sTimer; }
        }

        private int giCraneNo = 0;
        public int CraneNo
        {
            get { return giCraneNo; }
            set { giCraneNo = value; }
        }
        public ACTMULTILib.ActEasyIF objPLC = new ACTMULTILib.ActEasyIF();
        public bool bConnectPLC;

        private int iStationNumber = 0;
        public int StationNumber
        {
            get { return iStationNumber; }
            set { iStationNumber = value; }
        }

        private bool _bReConn = false;
        public bool bReConn
        {
            set { _bReConn = value; }
        }

        public int iFirstFloor = Properties.Settings.Default.First_Floor;
        public int iSecondFloor = Properties.Settings.Default.Second_Floor;

        # region MPLC Start Addr

        public int iAddrLFT_Reset = Properties.Settings.Default.MPLC_StartAddr + 25;
        public int iAddrLFT_Sts1 = Properties.Settings.Default.MPLC_StartAddr + 125;
        public int iAddrLFT_Sts2 = Properties.Settings.Default.MPLC_StartAddr + 126;
        public int iAddrLFT_IFSts = Properties.Settings.Default.MPLC_StartAddr + 145;
        public int iAddrLFT_SeqNo = Properties.Settings.Default.MPLC_StartAddr + 110;
        public int iAddrLFT_Speed = Properties.Settings.Default.MPLC_StartAddr + 114;
        public int iAddrLFT_Init = Properties.Settings.Default.MPLC_StartAddr + 118;
        public int iAddrLFT_ClearSts = Properties.Settings.Default.MPLC_StartAddr + 134;
        public int iAddrLFT_ClearCmd = Properties.Settings.Default.MPLC_StartAddr + 10;
        public int iAddrLFT_ErrorCode = Properties.Settings.Default.MPLC_StartAddr + 128;
        public int iAddrLFT_ErrorIdx = Properties.Settings.Default.MPLC_StartAddr + 129;

        public int iAddrTRU1_Reset = Properties.Settings.Default.MPLC_StartAddr + 45;
        public int iAddrTRU1_Sts1 = Properties.Settings.Default.MPLC_StartAddr + 175;
        public int iAddrTRU1_Sts2 = Properties.Settings.Default.MPLC_StartAddr + 176;
        public int iAddrTRU1_IFSts = Properties.Settings.Default.MPLC_StartAddr + 187;
        public int iAddrTRU1_SeqNo = Properties.Settings.Default.MPLC_StartAddr + 160;
        public int iAddrTRU1_Speed = Properties.Settings.Default.MPLC_StartAddr + 164;
        public int iAddrTRU1_Init = Properties.Settings.Default.MPLC_StartAddr + 168;
        public int iAddrTRU1_ClearSts = Properties.Settings.Default.MPLC_StartAddr + 183;
        public int iAddrTRU1_ClearCmd = Properties.Settings.Default.MPLC_StartAddr + 30;
        public int iAddrTRU1_ErrorCode = Properties.Settings.Default.MPLC_StartAddr + 177;
        public int iAddrTRU1_ErrorIdx = Properties.Settings.Default.MPLC_StartAddr + 178;

        public int iAddrTRU2_Reset = Properties.Settings.Default.MPLC_StartAddr + 65;
        public int iAddrTRU2_Sts1 = Properties.Settings.Default.MPLC_StartAddr + 215;
        public int iAddrTRU2_Sts2 = Properties.Settings.Default.MPLC_StartAddr + 216;
        public int iAddrTRU2_IFSts = Properties.Settings.Default.MPLC_StartAddr + 227;
        public int iAddrTRU2_SeqNo = Properties.Settings.Default.MPLC_StartAddr + 200;
        public int iAddrTRU2_Speed = Properties.Settings.Default.MPLC_StartAddr + 204;
        public int iAddrTRU2_Init = Properties.Settings.Default.MPLC_StartAddr + 208;
        public int iAddrTRU2_ClearSts = Properties.Settings.Default.MPLC_StartAddr + 223;
        public int iAddrTRU2_ClearCmd = Properties.Settings.Default.MPLC_StartAddr + 50;
        public int iAddrTRU2_ErrorCode = Properties.Settings.Default.MPLC_StartAddr + 217;
        public int iAddrTRU2_ErrorIdx = Properties.Settings.Default.MPLC_StartAddr + 218;

        public int iAddrPortErrorCode = Properties.Settings.Default.MPLC_StartAddr + 244;
        public int iAddrPortErrorIndex = Properties.Settings.Default.MPLC_StartAddr + 291;
        public int iAddrPortErrorBit = Properties.Settings.Default.MPLC_StartAddr + 240;
        public int[] iAddrPortSts1 = new int[] { Properties.Settings.Default.MPLC_StartAddr + 240, Properties.Settings.Default.MPLC_StartAddr + 300,
                                                Properties.Settings.Default.MPLC_StartAddr + 360, Properties.Settings.Default.MPLC_StartAddr + 420 };
        public int[] iAddrPortSts2 = new int[] { Properties.Settings.Default.MPLC_StartAddr + 241, Properties.Settings.Default.MPLC_StartAddr + 301,
                                                Properties.Settings.Default.MPLC_StartAddr + 361, Properties.Settings.Default.MPLC_StartAddr + 421 };
        public int[] iAddrPortClearSts = new int[] { Properties.Settings.Default.MPLC_StartAddr + 246, Properties.Settings.Default.MPLC_StartAddr + 306,
                                                    Properties.Settings.Default.MPLC_StartAddr + 366, Properties.Settings.Default.MPLC_StartAddr + 426 };
        public int[] iAddrPortCmd = new int[] { Properties.Settings.Default.MPLC_StartAddr + 70, Properties.Settings.Default.MPLC_StartAddr + 72,
                                                    Properties.Settings.Default.MPLC_StartAddr + 74, Properties.Settings.Default.MPLC_StartAddr + 76 };

        #endregion

        #endregion

        public MPLC()
        {   
            timRead.Elapsed += new System.Timers.ElapsedEventHandler(timRead_Elapsed);
            timRead.Enabled = false; timRead.Interval = 350;
            LoadPortDef();
        }

        private void LoadPortDef()
        {           
            if (clsLC_Def.gstrIniPath == "")
                clsLC_Def.gstrIniPath = Application.StartupPath + "\\LCS.ini";
            else
                clsLC_Def.gstrIniPath = clsLC_Def.gstrIniPath + "LCS.ini";

            clsDB.subGetDBConfig(clsLC_Def.gstrIniPath);  //路徑搜尋

            string strEM = string.Empty;
            DataTable DT = new DataTable();
            string strSQL = "select Stage,HostEQPortID,PortType,PLCIndex,PLCPortID,Floor from LifterPortDef group by Stage,HostEQPortID,PortType,PLCIndex,PLCPortID,Floor order by Floor,HostEQPortID";

            if (clsDB.gobjDB1.funGetDT(strSQL, ref DT, ref strEM) == 0)
            {
                objLFCDef = new clsLC_Def.PortDef[DT.Rows.Count];
                for (int i = 0; i < DT.Rows.Count; i++) //將獲得資料寫入Dictionary，索引以PLC Index排列
                {
                    objLFCDef[i].PLCindex = Convert.ToInt32(DT.Rows[i]["PLCIndex"]);
                    objLFCDef[i].PLCPortID = Convert.ToInt32(DT.Rows[i]["PLCPortID"]);
                    objLFCDef[i].PortType = Convert.ToInt32(DT.Rows[i]["PortType"]);
                    objLFCDef[i].Floor = Convert.ToInt32(DT.Rows[i]["Floor"]);
                    objLFCDef[i].HostEQPortID = Convert.ToString(DT.Rows[i]["HostEQPortID"]);
                    objLFCDef[i].Stage = Convert.ToInt32(DT.Rows[i]["Stage"]);

                    switch (objLFCDef[i].PortType)
                    {
                        case 1: //LFT                   
                            iLFTPortID = objLFCDef[i].PLCPortID;
                            break;
                        case 2: //TRU
                            clsLC_Def.dicTRUDef.Add(objLFCDef[i].PLCindex, objLFCDef[i]);
                            break;
                        case 3: //Conveyor
                            clsLC_Def.dicCONDef.Add(objLFCDef[i].PLCindex, objLFCDef[i]);
                            break;
                        default:
                            break;
                    }
                }
                objLFCDef = null;
            }
            foreach (clsLC_Def.PortDef Value in clsLC_Def.dicCONDef.Values) //歸納Conveyor PortID範圍
            {
                if (!iConvPortID.Contains(Value.PLCPortID))
                iConvPortID.Add(Value.PLCPortID);
            }
            DT = null;
        }

        public void subStart()
        {
            SubOpenPLC();
            timRead.Enabled = true;
        }

        private void timRead_Elapsed(object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                timRead.Enabled = false;
                _sTimer = System.DateTime.Now.ToString("HH:mm:ss");

                int iTestAddr = Properties.Settings.Default.MPLC_StartAddr + 30;
                int bRet = FunReadPLC("D" + iTestAddr);
                if (!bConnectPLC || bRet == -1)
                {
                    SubOpenPLC();//重新連線
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                timRead.Enabled = true;
            }
        }

        private void SubOpenPLC()
        {
            if (FunOpenPLC(ref objPLC, iStationNumber) == true)
            {
                bConnectPLC = true;
            }
            else
            {
                bConnectPLC = false;
            }
        }

        public int[] funString2int(string sValue, int iLength)
        {
            int[] iData = new int[iLength];

            if (sValue.Length % 2 == 0)
            {
                int j = 0;
                for (int i = 0; i < sValue.Length; i = i + 2)
                {
                    iData[j] = Convert.ToInt32(sValue[i]) + (Convert.ToInt32(sValue[i + 1]) * 256);
                    j = j + 1;
                }

            }
            else
            {
                int j = 0;
                for (int i = 0; i < sValue.Length - 1; i = i + 2)
                {
                    iData[j] = Convert.ToInt32(sValue[i]) + (Convert.ToInt32(sValue[i + 1]) * 256);
                    j = j + 1;
                }
                iData[j] = Convert.ToInt32(sValue[sValue.Length - 1]);

            }
            return iData;
        }

        #region Main_Scenario_Proc

        #region Lifter Normal_Proc

        /// <summary>
        /// T1流程
        /// </summary>
        /// <returns>流程步驟編號</returns>
        public int LFT_T1_Proc()
        {
            int iAddr = Properties.Settings.Default.MPLC_StartAddr + 125;
            bool bRet = FunWriPLC_Bit("D" + iAddr + ".A", 0);  // abnormal complete off    
            iAddr = Properties.Settings.Default.MPLC_StartAddr + 111;

            switch (clsLifterMPLC.MPLC.LFC_C.LFC2L.LFCmdMode)
            {
                case clsLifterMPLC.MPLC.enuCommandMode.Move:   // 如果是move的話 行動完結進行 normal complete  &  清值
                    {
                        int iData = (int)clsLifterMPLC.MPLC.LFC_C.LFC2L.To;
                        bRet = FunWriPLC_Word("D" + iAddr, iData);

                        bRet = FunWriPLC_Bit("D" + iAddr + ".F", 1); // move on

                        SpinWait.SpinUntil(() => false, 500);

                        bRet = FunWriPLC_Bit("D" + iAddr + ".F", 0); // move off   

                        return 20;
                    }

                case clsLifterMPLC.MPLC.enuCommandMode.PickUp:
                case clsLifterMPLC.MPLC.enuCommandMode.Transfer:  // 如果是pick up 或 transfer command的話 移動到location from 
                    {
                        int iData = (int)clsLifterMPLC.MPLC.LFC_C.LFC2L.Form; //current location  = From
                        bRet = FunWriPLC_Word("D" + iAddr, iData);

                        bRet = FunWriPLC_Bit("D" + iAddr + ".F", 1); // move on

                        SpinWait.SpinUntil(() => false, 500);

                        bRet = FunWriPLC_Bit("D" + iAddr + ".F", 0); // move off

                        return 2;
                    }

                case clsLifterMPLC.MPLC.enuCommandMode.Deposite:  // 如果是deposite 的話 移動到location to  
                    {
                        int iData = (int)clsLifterMPLC.MPLC.LFC_C.LFC2L.To; //current location  = To
                        bRet = FunWriPLC_Word("D" + iAddr, iData);

                        bRet = FunWriPLC_Bit("D" + iAddr + ".F", 1); // move on

                        SpinWait.SpinUntil(() => false, 500);

                        bRet = FunWriPLC_Bit("D" + iAddr + ".F", 0); // move off

                        return 10;
                    }

                default:
                    return 0;
            }
        }

        /// <summary>
        /// 選擇取物流程
        /// </summary>
        /// <returns>流程步驟編號</returns>
        public int LFT_SelectFrom()
        {
            SpinWait.SpinUntil(() => false, 300);

            if (clsLifterMPLC.MPLC.LFC_C.L2LFC.CurrentLV.CurrentLoc == clsLC_Def.dicTRUDef[0].Floor)   //LFT 取 TRU-1 交握
            {
                SpinWait.SpinUntil(() => false, 500);
                return 7;
            }
            else if (clsLifterMPLC.MPLC.LFC_C.L2LFC.CurrentLV.CurrentLoc == clsLC_Def.dicTRUDef[1].Floor)   //LFT 取 TRU -2 交握
            {
                SpinWait.SpinUntil(() => false, 500);
                return 8;
            }
            else
                return 2;
        }

        /// <summary>
        /// LFT 從TRU取物
        /// </summary>
        /// <param name="iTRU">TRU編號</param>
        /// <returns>流程步驟編號</returns>
        public int LFT_Pick_up_TRU(TRU iTRU)
        {
            int[] sData = new int[7];
            int inum = (int)iTRU - 1;

            SpinWait.SpinUntil(() => false, 300);

            if (clsLifterMPLC.MPLC.LFC_C.T2LFC[inum].CSTID == "")
            {
                sData = funString2int(clsLifterMPLC.MPLC.LFC_C.LFC2L.CSTID, 7);

                int iAddr = Properties.Settings.Default.MPLC_StartAddr + 118;
                bool bRet = FunWriPLC_Word("D" + iAddr, sData[0]);
                iAddr = Properties.Settings.Default.MPLC_StartAddr + 119;
                bRet = FunWriPLC_Word("D" + iAddr, sData[1]);
                iAddr = Properties.Settings.Default.MPLC_StartAddr + 120;
                bRet = FunWriPLC_Word("D" + iAddr, sData[2]);
                iAddr = Properties.Settings.Default.MPLC_StartAddr + 121;
                bRet = FunWriPLC_Word("D" + iAddr, sData[3]);
                iAddr = Properties.Settings.Default.MPLC_StartAddr + 122;
                bRet = FunWriPLC_Word("D" + iAddr, sData[4]);
                iAddr = Properties.Settings.Default.MPLC_StartAddr + 123;
                bRet = FunWriPLC_Word("D" + iAddr, sData[5]);
                iAddr = Properties.Settings.Default.MPLC_StartAddr + 124;
                bRet = FunWriPLC_Word("D" + iAddr, sData[6]);

                iAddr = Properties.Settings.Default.MPLC_StartAddr + 126;
                bRet = FunWriPLC_Bit("D" + iAddr + ".F", 1); // CST in cage

                if (clsLifterMPLC.MPLC.LFC_C.LFC2L.LFCmdMode == clsLifterMPLC.MPLC.enuCommandMode.Transfer)
                {
                    SpinWait.SpinUntil(() => false, 500);
                    return 18;    //transfer 交握 中繼
                }
                else // 單純pick up 流程結束
                {
                    SpinWait.SpinUntil(() => false, 500);
                    return 20;
                }
            }
            else
                return (iTRU == MPLC.TRU.L10)? 7:8;
        }

        /// <summary>
        /// 選擇置物流程
        /// </summary>
        /// <returns>流程步驟編號</returns>
        public int LFT_SelectTo()
        {
            SpinWait.SpinUntil(() => false, 300);

            if (clsLifterMPLC.MPLC.LFC_C.L2LFC.CurrentLV.CurrentLoc == clsLC_Def.dicTRUDef[0].Floor)   //LFT 取 TRU-1 交握
            {
                SpinWait.SpinUntil(() => false, 500);
                return 15;
            }
            else if (clsLifterMPLC.MPLC.LFC_C.L2LFC.CurrentLV.CurrentLoc  == clsLC_Def.dicTRUDef[1].Floor)   //LFT 取 TRU -2 交握
            {
                SpinWait.SpinUntil(() => false, 500);
                return 16;
            }
            else
                return 10;
        }

        /// <summary>
        /// LFT To TRU流程
        /// </summary>
        /// <param name="iTRU">TRU編號</param>
        /// <returns>流程步驟編號</returns>
        public int LFT_Deposite_TRU(TRU iTRU)
        {
            int inum = (int)iTRU - 1;

            SpinWait.SpinUntil(() => false, 500);
            string sData = "";
            sData = "0,0,0,0,0,0,0";
            int iAddr = Properties.Settings.Default.MPLC_StartAddr + 118; //CSTID
            FunWriPLC_Word("D" + iAddr, sData);

            iAddr = Properties.Settings.Default.MPLC_StartAddr + 126;
            FunWriPLC_Bit("D" + iAddr +".F", 0); // CST in cage off                                      

            SpinWait.SpinUntil(() => false, 500);
            return 20;
        }

        /// <summary>
        /// LFT_TransferMove
        /// </summary>
        /// <returns>流程步驟編號</returns>
        public int LFT_TransferMove()
        {
            int iData = 0;
            int iAddr = Properties.Settings.Default.MPLC_StartAddr + 111;
            iData = (int)clsLifterMPLC.MPLC.LFC_C.LFC2L.To; //current location  = To

            bool bRet = FunWriPLC_Word("D" + iAddr, iData);

            SpinWait.SpinUntil(() => false, 500);
            return 10;
        }

        /// <summary>
        /// LFT Normal完成訊號
        /// </summary>
        /// <returns>流程步驟編號</returns>
        public int LFT_Normal_Fin()
        {
            int iAddr = Properties.Settings.Default.MPLC_StartAddr + 125;
            bool bRet = FunWriPLC_Bit("D" + iAddr +".9", 1);  // Normal complete

            string sData = "";
            sData = "0,0,0,0,0,0,0,0,0,0,0";
            iAddr = Properties.Settings.Default.MPLC_StartAddr + 10;
            bRet = FunWriPLC_Word("D" + iAddr, sData);
            
            iAddr = Properties.Settings.Default.MPLC_StartAddr + 125;
            bRet = FunWriPLC_Bit("D" + iAddr + ".1", 1);  // ready on
            bRet = FunWriPLC_Bit("D" + iAddr + ".2", 0);  // busy off

            return 0;
        }

        #endregion

        #region TRU Normal_Proc

        /// <summary>
        /// T1 流程
        /// </summary>
        /// <param name="iTRU">TRU編號</param>
        /// <returns>流程步驟編號</returns>
        public int TRU_T1_Proc(TRU iTRU)
        {
            int inum = (int)iTRU - 1;
            int iAddr1 = Properties.Settings.Default.MPLC_StartAddr + (161 + (inum * 40));
            int iAddr2 = Properties.Settings.Default.MPLC_StartAddr + (175 + (inum * 40));

            bool bRet = FunWriPLC_Bit("D" + iAddr2 + ".A", 0);  // abnormal complete off

            switch (clsLifterMPLC.MPLC.LFC_C.LFC2T[inum].LFCmdMode)
            {
                case clsLifterMPLC.MPLC.enuCommandMode.Move: // 如果是move的話 行動完結進行 normal complete  &  清值
                    {
                        string sData = "";
                        sData = clsLifterMPLC.MPLC.LFC_C.LFC2T[inum].To.ToString(); //current location  = To
                        bRet = FunWriPLC_Word("D" + iAddr1, sData);

                        bRet = FunWriPLC_Bit("D" + iAddr1 + ".F", 1); // move on
                        SpinWait.SpinUntil(() => false, 700);

                        bRet = FunWriPLC_Bit("D" + iAddr1 + ".F", 0); // move off
                        SpinWait.SpinUntil(() => false, 700);
                        return 20;
                    }
                case clsLifterMPLC.MPLC.enuCommandMode.PickUp:
                case clsLifterMPLC.MPLC.enuCommandMode.Transfer:  // 如果是pick up或Transfer cmd的話 移動到location from , 並判定location 是否是conveyor 
                    {
                        string sData = "";
                        sData = clsLifterMPLC.MPLC.LFC_C.LFC2T[inum].Form.ToString(); //current location  = From
                        bRet = FunWriPLC_Word("D" + iAddr1, sData);

                        bRet = FunWriPLC_Bit("D" + iAddr1 + ".F", 1); // move on
                        SpinWait.SpinUntil(() => false, 700);

                        bRet = FunWriPLC_Bit("D" + iAddr1 + ".F", 0); // move off
                        SpinWait.SpinUntil(() => false, 700);
                        return 2;
                    }

                case clsLifterMPLC.MPLC.enuCommandMode.Deposite: //如果是deposite 的話 移動到location to  
                    {
                        string sData = "";
                        sData = clsLifterMPLC.MPLC.LFC_C.LFC2T[inum].To.ToString(); //current location  = To
                        bRet = FunWriPLC_Word("D" + iAddr1, sData);

                        bRet = FunWriPLC_Bit("D" + iAddr1 + ".F", 1); // move on
                        SpinWait.SpinUntil(() => false, 700);

                        bRet = FunWriPLC_Bit("D" + iAddr1 + ".F", 0); // move off
                        SpinWait.SpinUntil(() => false, 700);
                        return 10;
                    }
                default:
                    return 1;
            }
        }

        /// <summary>
        /// TRU1_T2 選擇目的地
        /// </summary>
        /// <param name="iTRU">TRU編號</param>
        /// <returns>流程步驟編號</returns>
        public int TRU_T2_Select(TRU iTRU)
        {
            int inum = (int)iTRU - 1;

            SpinWait.SpinUntil(() => false, 500);

            if (clsLifterMPLC.MPLC.LFC_C.LFC2L.LFCmdMode != 0)  // 代表取物目標是 Lifter
            {
                SpinWait.SpinUntil(() => false, 500);
                return 7;
            }
            else //代表取物目標是convery
            {
                SpinWait.SpinUntil(() => false, 500);
                return 3;
            }
        }

        /// <summary>
        /// T2 Pick up動作流程
        /// </summary>
        /// <param name="iTRU">TRU編號</param>
        /// <returns>流程步驟編號</returns>
        public int TRU_T2_PickUp(TRU iTRU)
        {
            int inum = (int)iTRU - 1;
            int[] sData = new int[7]{0,0,0,0,0,0,0};

            int iAddr = Properties.Settings.Default.MPLC_StartAddr + (176 + (inum * 40));
            FunWriPLC_Bit("D" + iAddr + ".E", 1); // forking on

            SpinWait.SpinUntil(() => false, 500);

            FunWriPLC_Bit("D" + iAddr + ".E", 0); // forking off

            switch ((int)iTRU)
            {
                case 1:
                    for (int i = 0; i < clsLC_Def.dicCONDef.Count; i++)
                    {
                        if (clsLifterMPLC.MPLC.LFC_C.LFC2T[inum].Form == clsLC_Def.dicCONDef[i].PLCPortID)
                        {
                            if(clsLC_Def.dicCONDef[i].Floor == Properties.Settings.Default.First_Floor)
                            {
                                PortSignalSelect(clsLC_Def.dicCONDef[i].PLCindex, false, sData);
                                break;
                            }
                        }
                    }
                    break;

                case 2:
                    for (int i = 0; i < clsLC_Def.dicCONDef.Count; i++)
                    {
                        if (clsLifterMPLC.MPLC.LFC_C.LFC2T[inum].Form == clsLC_Def.dicCONDef[i].PLCPortID)
                        {
                            if (clsLC_Def.dicCONDef[i].Floor == Properties.Settings.Default.Second_Floor)
                            {
                                PortSignalSelect(clsLC_Def.dicCONDef[i].PLCindex, false, sData);
                                break;
                            }
                        }
                    }
                    break;
            }


            sData = funString2int(clsLifterMPLC.MPLC.LFC_C.LFC2T[inum].CSTID, 7);//CSTID
            iAddr = Properties.Settings.Default.MPLC_StartAddr + (168 + (inum * 40));

            for (int i = 0; i < 7; i++)
            {
                FunWriPLC_Word("D" + iAddr, sData[i]);
                iAddr++;
            }

            iAddr = Properties.Settings.Default.MPLC_StartAddr + (176 + (inum * 40));

            FunWriPLC_Bit("D" + iAddr + ".F", 1); // stock on fork on

            if (clsLifterMPLC.MPLC.LFC_C.LFC2T[inum].LFCmdMode == clsLifterMPLC.MPLC.enuCommandMode.Transfer)
            {
                SpinWait.SpinUntil(() => false, 500);
                return 18;
            }
            else
                return 20;// normal pick 流程結束
        }

        /// <summary>
        /// T2 Pick up交握流程
        /// </summary>
        /// <param name="iTRU">TRU編號</param>
        /// <returns>流程步驟編號</returns>
        public int TRU_T2_Handshake(TRU iTRU)
        {
            int iAddr = 0;
            int inum = (int)iTRU - 1;

            if (clsLifterMPLC.MPLC.LFC_C.LFC2L.CSTID == "")
            {
                iAddr = Properties.Settings.Default.MPLC_StartAddr + (176 + inum * 40);
                bool bRet = FunWriPLC_Bit("D" + iAddr + ".E", 1); // forking on

                SpinWait.SpinUntil(() => false, 500);
                return 8;
            }
            else
                return 7;
        }

        /// <summary>
        /// T2 完成訊號建立
        /// </summary>
        /// <param name="iTRU">TRU編號</param>
        /// <returns>流程步驟編號</returns>
        public int TRU_T2_Fin(TRU iTRU)
        {
            int[] sData = new int[7];
            int inum = (int)iTRU - 1;
            int iAddr = Properties.Settings.Default.MPLC_StartAddr + (176 + (inum * 40));

            bool bRet = FunWriPLC_Bit("D" + iAddr + ".E", 0); // forking off

            sData = funString2int(clsLifterMPLC.MPLC.LFC_C.LFC2T[inum].CSTID, 7);//CSTID

            iAddr = Properties.Settings.Default.MPLC_StartAddr + (168 + (inum * 40));

            for (int i = 0; i < 7; i++)
            {
                bRet = FunWriPLC_Word("D" + iAddr, sData[i]);
                iAddr++;
            }

            iAddr = Properties.Settings.Default.MPLC_StartAddr + (176 + (inum * 40));
            bRet = FunWriPLC_Bit("D" + iAddr + ".F", 1); // stock on fork on


            if (clsLifterMPLC.MPLC.LFC_C.LFC2T[inum].LFCmdMode == clsLifterMPLC.MPLC.enuCommandMode.Transfer)
            {
                SpinWait.SpinUntil(() => false, 600);
                return 18;    //transfer 交握 中繼
            }
            else // 單純pick up 交握流程結束
            {
                SpinWait.SpinUntil(() => false, 600);
                return 20;
            }
        }

        /// <summary>
        /// TRU1_T3 選擇目標
        /// </summary>
        /// <param name="iTRU">TRU編號</param>
        /// <returns>流程步驟編號</returns>
        public int TRU_T3_Select(TRU iTRU)
        {
            int inum = (int)iTRU - 1;

            SpinWait.SpinUntil(() => false, 500);

            if (clsLifterMPLC.MPLC.LFC_C.LFC2L.LFCmdMode != 0)  // 代表放物目標是 Lifter
            {
                SpinWait.SpinUntil(() => false, 500);
                return 15;
            }
            else  //代表放物目標是convery 或 PNP
            {
                SpinWait.SpinUntil(() => false, 500);
                return 11;
            }
        }

        /// <summary>
        /// T3 Deposite 流程
        /// </summary>
        /// <param name="iTRU">TRU編號</param>
        /// <returns>流程步驟編號</returns>
        public int TRU_T3_Deposite(TRU iTRU)
        {
            int inum = (int)iTRU - 1;
            string sData = "";
            int[] CSTID_Data = new int[7];

            int iAddr = Properties.Settings.Default.MPLC_StartAddr + (176 + (inum * 40));
            FunWriPLC_Bit("D" + iAddr + ".E", 1); // forking on

            SpinWait.SpinUntil(() => false, 500);

            FunWriPLC_Bit("D" + iAddr + ".E", 0); // forking off

            CSTID_Data = funString2int(clsLifterMPLC.MPLC.LFC_C.LFC2T[inum].CSTID, 7);//CSTID

            sData = "0,0,0,0,0,0,0"; //CSTID off
            iAddr = Properties.Settings.Default.MPLC_StartAddr + (168 + (inum * 40));
            FunWriPLC_Word("D" + iAddr, sData);

            iAddr = Properties.Settings.Default.MPLC_StartAddr + (176 + (inum * 40));
            FunWriPLC_Bit("D" + iAddr + ".F", 0); // stock on fork off

            switch ((int)iTRU)
            {
                case 1:                   
                    for (int i = 0; i < clsLC_Def.dicCONDef.Count; i++)
                    {
                        if (clsLifterMPLC.MPLC.LFC_C.LFC2T[inum].To == clsLC_Def.dicCONDef[i].PLCPortID)
                        {
                            if(clsLC_Def.dicCONDef[i].Floor == Properties.Settings.Default.First_Floor)
                            {
                                PortSignalSelect(clsLC_Def.dicCONDef[i].PLCindex, true, CSTID_Data);
                                break;
                            }
                        }
                    }
                    break;

                case 2:
                    for (int i = 0; i < clsLC_Def.dicCONDef.Count; i++)
                    {
                        if (clsLifterMPLC.MPLC.LFC_C.LFC2T[inum].To == clsLC_Def.dicCONDef[i].PLCPortID)
                        {
                            if(clsLC_Def.dicCONDef[i].Floor == Properties.Settings.Default.Second_Floor)
                            {
                                PortSignalSelect(clsLC_Def.dicCONDef[i].PLCindex, true, CSTID_Data);
                                break;
                            }
                        }
                    }
                    break;
            }

            return 20;
        }

        /// <summary>
        /// T3 完成 流程
        /// </summary>
        /// <param name="iTRU">TRU編號</param>
        /// <returns>流程步驟編號</returns>
        public int TRU_T3_Fin(TRU iTRU)
        {
            int inum = (int)iTRU - 1;

            SpinWait.SpinUntil(() => false, 500);
            int iAddr = Properties.Settings.Default.MPLC_StartAddr + (176 + (40 * inum));
            FunWriPLC_Bit("D" + iAddr + ".E", 1); // forking on                                

            string sData = "0,0,0,0,0,0,0"; //CSTID off
            iAddr = Properties.Settings.Default.MPLC_StartAddr + (168 + (inum * 40));
            FunWriPLC_Word("D" + iAddr, sData);

            iAddr = Properties.Settings.Default.MPLC_StartAddr + (176 + (40 * inum));
            FunWriPLC_Bit("D" + iAddr + ".F", 0); // stock on fork off  
            FunWriPLC_Bit("D" + iAddr + ".E", 0); // forking off

            SpinWait.SpinUntil(() => false, 500);
            return 20;
        }

        /// <summary>
        /// T3 Move訊號
        /// </summary>
        /// <param name="iTRU">TRU編號</param>
        /// <returns>流程步驟編號</returns>
        public int TRU_T3_TransferMove(TRU iTRU)
        {
            int inum = (int)iTRU - 1;
            string sData = "";
            int iAddr = Properties.Settings.Default.MPLC_StartAddr + (161 + (inum * 40));
            sData = clsLifterMPLC.MPLC.LFC_C.LFC2T[inum].To.ToString(); //current location  = To
            bool bRet = FunWriPLC_Word("D" + iAddr, sData);

            SpinWait.SpinUntil(() => false, 500);
            return 10;
        }

        /// <summary>
        /// TRU動作完成訊號
        /// </summary>
        /// <param name="iTRU">TRU編號</param>
        /// <returns>流程步驟編號</returns>
        public int TRU_NormalFin(TRU iTRU)
        {
            int inum = (int)iTRU - 1;
            int iAddr = Properties.Settings.Default.MPLC_StartAddr + (175 + (inum * 40));
            string sData = "";

            SpinWait.SpinUntil(() => false, 500);
            bool bRet = FunWriPLC_Bit("D" + iAddr + ".9", 1);  // Normal complete
            sData = "0,0,0,0,0,0,0,0,0,0,0";

            iAddr = Properties.Settings.Default.MPLC_StartAddr + (30 + (20 * inum));
            bRet = FunWriPLC_Word("D" + iAddr, sData);

            iAddr = Properties.Settings.Default.MPLC_StartAddr + (175 + (inum * 40));
            bRet = FunWriPLC_Bit("D" + iAddr + ".1", 1); // ready on
            bRet = FunWriPLC_Bit("D" + iAddr + ".2", 0);  // busy off

            SpinWait.SpinUntil(() => false, 500);
            return 0;
        }

        /// <summary>
        /// 根據Port的PLCIdx更改荷有及L/UL訊號
        /// </summary>
        /// <param name="iPLCIdx">PLC的Index</param>
        /// <param name="bOn_Off">選擇On還是Off</param>
        public void PortSignalSelect(int iPLCIdx, bool bOn_Off, int[] sData)
        {
            int iAddr = Properties.Settings.Default.MPLC_StartAddr + (241 + 60 * iPLCIdx);

            if (bOn_Off)
            {
                FunWriPLC_Bit("D" + iAddr + ".0", 1); //荷有
                iAddr = Properties.Settings.Default.MPLC_StartAddr + (240 + 60 * iPLCIdx);
                FunWriPLC_Bit("D" + iAddr + ".C", 0); //load ok
                iAddr = Properties.Settings.Default.MPLC_StartAddr + (246 + 60 * iPLCIdx);
                for (int i = 0; i <= 6; i++)
                {
                    FunWriPLC_Word("D" + iAddr, sData[i].ToString());
                    iAddr++;
                }
            }
            else
            {
                FunWriPLC_Bit("D" + iAddr + ".0", 0); //荷有
                iAddr = Properties.Settings.Default.MPLC_StartAddr + (240 + 60 * iPLCIdx);
                FunWriPLC_Bit("D" + iAddr + ".C", 1); //load ok
                FunWriPLC_Bit("D" + iAddr + ".D", 0); //unload
                iAddr = Properties.Settings.Default.MPLC_StartAddr + (246 + 60 * iPLCIdx);
                FunWriPLC_Word("D" + iAddr, "0,0,0,0,0,0,0");

            }
        }

        #endregion

        #endregion

        #region Device_Singal_Function

        #region TRU

        public enum TRU : byte
        {
            L10 = 1,
            L30 = 2
        }
        #endregion

        #region Conveyor

        public enum Conveyor : byte
        {
            L10L1 = 1,
            L10L2 = 2,
            L30L1 = 3,
            L30L2 = 4
        }

        /// <summary>
        /// 移除Port的Carrier訊號
        /// </summary>
        /// <param name="iConv"></param>
        public void Remove(int PLCIdx)
        {
            int iAddr = Properties.Settings.Default.MPLC_StartAddr + (241 + 60 * PLCIdx);

            FunWriPLC_Bit("D" + iAddr + ".F", 1);
            FunWriPLC_Bit("D" + iAddr + ".0", 0); //荷有
            SpinWait.SpinUntil(() => false, 500);
            FunWriPLC_Bit("D" + iAddr + ".F", 0);
            iAddr = Properties.Settings.Default.MPLC_StartAddr + (240 + 60 * PLCIdx);
            FunWriPLC_Bit("D" + iAddr + ".C", 1); //load ok
            FunWriPLC_Bit("D" + iAddr + ".D", 0); //unload ok
            iAddr = Properties.Settings.Default.MPLC_StartAddr + (246 + 60 * PLCIdx);
            FunWriPLC_Word("D" + iAddr , "0,0,0,0,0,0,0");
        }

        /// <summary>
        /// BRC Read done Carrier Wait In
        /// </summary>
        /// <param name="iConv">Port 編號</param>
        /// <param name="sData">寫入ID序號</param>
        public void Wait_In(int PLCIdx, int[] sData)
        {
            int iAddr = Properties.Settings.Default.MPLC_StartAddr + (240 + 60 * PLCIdx);
            int iAddr2 = 28 + 6 * PLCIdx;

            if (Convert.ToString(FunReadPLC("D" + iAddr), 2).PadLeft(16, Convert.ToChar("0")).Substring(12, 1) == "1")
            {
                iAddr = Properties.Settings.Default.MPLC_StartAddr + (281 + 60 * PLCIdx);
                for (int i = 0; i < 7; i++)
                {
                    FunWriPLC_Word("D" + iAddr, sData[i].ToString());
                    iAddr++;
                }

                iAddr = Properties.Settings.Default.MPLC_StartAddr + (241 + 60 * PLCIdx);
                FunWriPLC_Bit("D" + iAddr + ".0", 1); //荷有
                FunWriPLC_Bit("D" + iAddr + ".9", 1);

                SpinWait.SpinUntil(() => false, 200);

                iAddr = Properties.Settings.Default.MPLC_StartAddr + (246 + 60 * PLCIdx);
                for (int i = 0; i < 7; i++)
                {
                    FunWriPLC_Word("D" + iAddr , sData[i].ToString());
                    iAddr++;
                }

                iAddr = Properties.Settings.Default.MPLC_StartAddr + (241 + 60 * PLCIdx);
                FunWriPLC_Bit("D" + iAddr + ".9", 0);
                iAddr = Properties.Settings.Default.MPLC_StartAddr + (240 + 60 * PLCIdx);
                FunWriPLC_Bit("D" + iAddr + ".8", 1);
                FunWriPLC_Bit("D" + iAddr + ".D", 1); //unload ok
                FunWriPLC_Bit("D" + iAddr + ".C", 0); //load

                SpinWait.SpinUntil(() => false, 500);
                FunWriPLC_Bit("D" + iAddr + ".8", 0);
            }
        }

        /// <summary>
        /// Carrier Wait Out
        /// </summary>
        /// <param name="iConv">Port 編號</param>
        /// <param name="sData">寫入ID序號</param>
        public void Wait_Out(int PLCIdx, int[] sData)
        {
            int iAddr = Properties.Settings.Default.MPLC_StartAddr + (246 + 60 * PLCIdx);

            for (int i = 0; i < 7; i++)
            {
                FunWriPLC_Word("D" + iAddr, sData[i].ToString());  // 位置 1
                iAddr++;
            }

            SpinWait.SpinUntil(() => false, 500);
            iAddr = Properties.Settings.Default.MPLC_StartAddr + (240 + 60 * PLCIdx);
            FunWriPLC_Bit("D" + iAddr + ".9", 1);//Wait out 
            FunWriPLC_Bit("D" + iAddr + ".C", 1); //Unload ok
            FunWriPLC_Bit("D" + iAddr + ".D", 0); //load

            SpinWait.SpinUntil(() => false, 500);

            FunWriPLC_Bit("D" + iAddr + ".9", 0);
        }

        /// <summary>
        /// Port In/Out Mode改變
        /// </summary>
        /// <param name="iConv">Port 編號</param>
        public void ModeChange(int PLCIdx)
        {
            int iPLCInfo = 0;
            int iAddr = Properties.Settings.Default.MPLC_StartAddr + (241 + 60 * PLCIdx);

            objPLC.GetDevice("D" + iAddr + ".0", out iPLCInfo);

            if (iPLCInfo == 1) //確認無荷有
            {
                MessageBox.Show("You must remove carrier before port mode change.");
                return;
            }
            iAddr = Properties.Settings.Default.MPLC_StartAddr + (240 + 60 * PLCIdx);
            objPLC.GetDevice("D" + iAddr + ".3", out iPLCInfo); //讀取當前In/out狀態

            if (iPLCInfo == 1) //Input->Output
            {
                FunWriPLC_Bit("D" + iAddr + ".3", 0);  // input mode
                FunWriPLC_Bit("D" + iAddr + ".4", 1);  // output mode
                iAddr = Properties.Settings.Default.MPLC_StartAddr + (70 + PLCIdx * 2);
                FunWriPLC_Bit("D" + iAddr + ".E", 0);  // input mode
                FunWriPLC_Bit("D" + iAddr + ".F", 0);  // output mode   
            }
            else    //Output->Input
            {
                FunWriPLC_Bit("D" + iAddr + ".3", 1);  // input mode
                FunWriPLC_Bit("D" + iAddr + ".4", 0);  // output mode
                iAddr = Properties.Settings.Default.MPLC_StartAddr + (70 + PLCIdx * 2);
                FunWriPLC_Bit("D" + iAddr + ".E", 0);  // input mode
                FunWriPLC_Bit("D" + iAddr + ".F", 0);  // output mode   
            }
        }

        /// <summary>
        /// Port to Stop to Run
        /// </summary>
        /// <param name="iConv">Port 編號</param>
        public void Run2Stop2Run(Conveyor iConv)
        {
            int iAddr = Properties.Settings.Default.MPLC_StartAddr + (240 + 60 * ((int)iConv - 1));
            int iAddrPLC2LFC = 24 + 6 * ((int)iConv - 1);
            if (clsLifterMPLC.MPLC.LFC_C.LFC2C[(int)iConv - 1].CMDMode.Stop == clsLifterMPLC.MPLC.enuSignal.ON)
            {
                FunWriPLC_Bit("D" + iAddr + ".0", 0);
                FunWriPLC_Bit("D" + iAddr + ".1", 1);
                SpinWait.SpinUntil(() => false, 300);

                FunWriPLC_Bit("D" + iAddr + ".B", 0);
                iAddr = Properties.Settings.Default.MPLC_StartAddr + (243 + 60 * ((int)iConv - 1));
                FunWriPLC_Bit("D" + iAddr + ".0", 1); //Run Enable
                SpinWait.SpinUntil(() => false, 500);
            }
            if (clsLifterMPLC.MPLC.LFC_C.LFC2C[(int)iConv - 1].CMDMode.Run == clsLifterMPLC.MPLC.enuSignal.ON)
            {
                if (clsLifterMPLC.MPLC.LFC_C.C2LFC[(int)iConv - 1].Sts_4.RunEnable == clsLifterMPLC.MPLC.enuSignal.ON)
                {
                    iAddr = Properties.Settings.Default.MPLC_StartAddr + (243 + 60 * ((int)iConv - 1));
                    FunWriPLC_Bit("D" + iAddr + ".0", 0);
                    iAddr = Properties.Settings.Default.MPLC_StartAddr + (240 + 60 * ((int)iConv - 1));
                    FunWriPLC_Bit("D" + iAddr + ".B", 1);
                    SpinWait.SpinUntil(() => false, 300);

                    FunWriPLC_Bit("D" + iAddr + ".0", 1);
                    FunWriPLC_Bit("D" + iAddr + ".1", 0);
                    SpinWait.SpinUntil(() => false, 500);
                }
            }
        }

        /// <summary>
        /// Conveyor 荷有 & CSTID建立
        /// </summary>
        /// <param name="PLCIdx"></param>
        /// <param name="StageNo"></param>
        /// <param name="sData"></param>
        public void PresentOnOff(int PLCIdx, int StageNo, int[] sData)
        {
            int iPLCInfo = 0;

            int iAddr = Properties.Settings.Default.MPLC_StartAddr + (241 + 60 * PLCIdx);
            objPLC.GetDevice("D" + iAddr + "." + (StageNo - 1).ToString(), out iPLCInfo);

            if (iPLCInfo == 1) //確認無荷有
            {
                FunWriPLC_Bit("D" + iAddr + "." + (StageNo - 1).ToString(), 0); //荷有
                iAddr = Properties.Settings.Default.MPLC_StartAddr + (240 + 60 * PLCIdx);
                FunWriPLC_Bit("D" + iAddr + ".C", 1); //load ok
                FunWriPLC_Bit("D" + iAddr + ".D", 0); //unload ok
                iAddr = Properties.Settings.Default.MPLC_StartAddr + (246 + 60 * PLCIdx) + ((StageNo - 1) * 7);
                FunWriPLC_Word("D" + iAddr, "0,0,0,0,0,0,0");
            }
            else
            {
                FunWriPLC_Bit("D" + iAddr + "." + (StageNo - 1).ToString(), 1); //荷有
                iAddr = Properties.Settings.Default.MPLC_StartAddr + (281 + 60 * PLCIdx);
                for (int i = 0; i < 7; i++)
                {
                    FunWriPLC_Word("D" + iAddr , sData[i].ToString()); //BCR Readdone
                    iAddr++;
                }

                SpinWait.SpinUntil(() => false, 200);

                iAddr = Properties.Settings.Default.MPLC_StartAddr + (246 + 60 * PLCIdx) + ((StageNo - 1) * 7);
                for (int i = 0; i < 7; i++)
                {
                    FunWriPLC_Word("D" + iAddr, sData[i].ToString());
                    iAddr++;
                }

                iAddr = Properties.Settings.Default.MPLC_StartAddr + (240 + 60 * PLCIdx);
                FunWriPLC_Bit("D" + iAddr + ".D", 1); //unload ok
                FunWriPLC_Bit("D" + iAddr + ".C", 0); //load
            }
        }

        /// <summary>
        /// LFT&TRU 荷有 & CSTID建立
        /// </summary>
        /// <param name="iConv">Port 編號</param>
        /// <param name="sData">寫入ID序號</param>
        public void EQPresentOnOff(int PLCIdx, int[] sData)
        {
            int iPLCInfo = 0;
            int iAddr = 0;

            if (PLCIdx == 5)
            {
                iAddr = Properties.Settings.Default.MPLC_StartAddr + 126;
                objPLC.GetDevice("D" + iAddr + ".F", out iPLCInfo); //判斷LFT
                if (iPLCInfo == 0)
                {
                    iAddr = Properties.Settings.Default.MPLC_StartAddr + 118;
                    for (int i = 0; i < 7; i++)
                    {
                        FunWriPLC_Word("D" + iAddr, sData[i].ToString());
                        iAddr++;
                    }

                    iAddr = Properties.Settings.Default.MPLC_StartAddr + 126;
                    FunWriPLC_Bit("D" + iAddr +".F", 1); // CST in cage
                }
                else
                {
                    string sData2 = "0,0,0,0,0,0,0";
                    iAddr = Properties.Settings.Default.MPLC_StartAddr + 118;
                    FunWriPLC_Word("D" + iAddr, sData2);

                    iAddr = Properties.Settings.Default.MPLC_StartAddr + 126;
                    FunWriPLC_Bit("D" + iAddr + ".F", 0); // CST in cage off 
                }
            }
            else
            {
                iAddr = Properties.Settings.Default.MPLC_StartAddr + (176 + (PLCIdx * 40));
                objPLC.GetDevice("D" + iAddr + ".F", out iPLCInfo); //判斷TRU
                if (iPLCInfo == 0)
                {
                    iAddr = Properties.Settings.Default.MPLC_StartAddr + (168 + (PLCIdx * 40));
                    for (int i = 0; i < 7; i++)
                    {
                        FunWriPLC_Word("D" + iAddr, sData[i].ToString());
                        iAddr++;
                    }

                    iAddr = Properties.Settings.Default.MPLC_StartAddr + (176 + (PLCIdx * 40));
                    FunWriPLC_Bit("D" + iAddr + ".F", 1); // stock on fork on
                }
                else
                {
                    string sData1 = "0,0,0,0,0,0,0"; //CSTID off
                    iAddr = Properties.Settings.Default.MPLC_StartAddr + (168 + (PLCIdx * 40));
                    FunWriPLC_Word("D" + iAddr, sData1);

                    iAddr = Properties.Settings.Default.MPLC_StartAddr + (176 + (PLCIdx * 40));
                    FunWriPLC_Bit("D" + iAddr + ".F", 0); // stock on fork off
                }
            }
        }

        #endregion

        #endregion

        #region PLC_Basic_Function

        // Open to Connection PLC
        public bool FunOpenPLC(ref ACTMULTILib.ActEasyIF objPLC, int iPlcStationNumber)
        {
            try
            {
                objPLC.ActLogicalStationNumber = iPlcStationNumber;
                objPLC.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Read PLC Data
        public bool FunReadPLC(string sAddr, ref int[] iRetData)
        {
            try
            {
                int iLength = 1;
                int iRtn;
                iLength = iRetData.Length;
                iRtn = objPLC.ReadDeviceBlock(sAddr, iLength, out iRetData[0]);
                if (iRtn == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public int FunReadPLC(string sAddr)
        {
            try
            {
                int[] PLCData = new int[1];
                bool bRet = FunReadPLC(sAddr, ref PLCData);
                return PLCData[0];
            }
            catch
            {
                return -1;
            }
        }

        // Write PLC Data (Wrod值) (單一值或多值)
        // 如果是連續的值,使用逗號做區分
        public bool FunWriPLC_Word(string sAddr, string sSetData)
        {
            try
            {
                int iLength;

                string[] sRetData = sSetData.Split(',');
                iLength = sRetData.Length;
                int[] iRetData = new int[iLength];

                for (int i = 0; i < iLength; i++)
                {
                    iRetData[i] = int.Parse(sRetData[i]);
                }

                int iRtn;
                iRtn = objPLC.WriteDeviceBlock(sAddr, iLength, ref iRetData[0]);
                if (iRtn == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        // Write PLC Data (Wrod值) 
        public bool FunWriPLC_Word(string sAddr, int iSetData)
        {
            try
            {
                int iLength = 1;
                int[] iRetData = new int[iLength];
                iRetData[0] = iSetData;

                int iRtn;
                iRtn = objPLC.WriteDeviceBlock(sAddr, iLength, ref iRetData[0]);
                if (iRtn == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }

        // Write PLC Data (bit值)
        public bool FunWriPLC_Bit(string sAddr, string sSetData)
        {
            try
            {
                int iRetData;
                iRetData = int.Parse(sSetData);

                int iRtn;
                iRtn = objPLC.SetDevice(sAddr, iRetData);
                if (iRtn == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        // Write PLC Data (bit值)
        public bool FunWriPLC_Bit(string sAddr, int iSetData)
        {
            try
            {
                int iRtn;
                iRtn = objPLC.SetDevice(sAddr, iSetData);
                if (iRtn == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion       

    }
}
