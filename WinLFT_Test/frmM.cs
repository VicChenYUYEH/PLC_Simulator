#region V1.0.0.0_New Version
/// V2.4.1709.0    20170918    VicChen 
///      V2.4.1709.0-1  Add        ->   將寫入位址偏移值，及第一、二樓層初始以Setting方式設定，可整合Lifter及TRU Simulator
///      
/// V2.3.1706.0    20170626    VicChen 
///      V2.3.1706.0-1  Add        ->   將Roller Type的各個Stage On/OFF移至Fork Type版
///      
/// V2.2.1703.0    20170309    VicChen 
///      V2.2.1703.0-1  Add        ->   新增Oracle連線。
///      
/// V2.1.1702.0    20170217    VicChen 
///      V2.1.1702.0-1  Add        ->   新增Abnormal測試情境及手動觸發Alarm。
///      
/// V2.0.1702.0    20170209    VicChen 
///      V2.0.1702.0-1  Add        ->   TM Fork Type I/O Mapping。
///      
/// V1.1.1701.0    20170118    VicChen 
///      V0.1.1701.0-1  Add        ->   於PLC.cs新增讀取LCS.ini及DB流程並於LoadPortDef中確認是否為PNP。
///      V0.1.1701.0-2  Modify     ->   修改UI Port功能，改由下拉式選單選擇Port再進行動作(Wait In、Wait out等)
///      
/// V1.0.1612.3    20161228    VicChen 
///      V0.0.1612.3-1  Modify     ->   新增選項for TM I/O mapping Ver.2.2(LFT to LFC handshake變 2Word)。
///      
/// V0.0.1612.2    20161221    VicChen 
///      V0.0.1612.2-1  Modify     ->   修改初始化只清空值，不做Remove動作
///      V0.0.1612.2-2  Add        ->   新增PortModeChange事件及Port Auto/Manual切換(由外部不由Simulator寫入)
///      
/// V0.0.1612.1    20161215    VicChen 
///      V0.0.1612.1-1  Modify     ->   更改Address for VDT I/O mapping Ver.2.1
///      V0.0.1612.1-2  Modify     ->   新增接受到Command時，寫入Speed至MPLC的動作
///
/// V0.0.1612.0    20161206    VicChen 初版(Address for深超)
///
///(V0.xxxx:For MDT  V1.xxx:For TM)
#endregion V1.0.0.0

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WinLFT_Test
{
    public partial class frmMain : Form
    {
        #region Define Varible

        int TRU1step = 0;
        int TRU2step = 0;
        int LFTstep = 0;
        //用來決定模擬的步驟

        int LFTalarmcode = 41254;//切換成16進制，即DB的Alarmcode
        int TRU1alarmcode = 41984;
        int TRU2alarmcode = 41985;
        int PortAlarmcode = 4365;
        int LFTErrorIndex = 0;
        int TRU1ErrorIndex = 0;
        int TRU2ErrorIndex = 0;
        int PortErrorIndex = 0;

        int TRUnum = 2;

        PLC.MPLC LFC = new PLC.MPLC();

        public clsLifterMPLC.MPLC objLFC = new clsLifterMPLC.MPLC();

        private Color Color_SignOn = Color.Blue;
        private Color Color_SignOff = Color.Red;

        private static int[] iTRU1_Step = new int[2];
        private static int iLFT_Step = 0;

        #endregion

        public frmMain()
        {
            InitializeComponent();
            objLFC.CONV_PortModeChangeTO += CONV_PortModeChangeTO;
        }


        #region Event Method

        private void CONV_PortModeChangeTO(int CONVIdx, clsLifterMPLC.MPLC.enuPortMode PortMode)
        {
            switch (PortMode)
            {
                //Request InMode
                case clsLifterMPLC.MPLC.enuPortMode.InMode:
                    
                    LFC.FunWriPLC_Bit("D" + LFC.iAddrPortSts1[CONVIdx] + ".3", 1);  // input mode
                    LFC.FunWriPLC_Bit("D" + LFC.iAddrPortSts1[CONVIdx] + ".4", 0);  // output mode
                    LFC.FunWriPLC_Bit("D" + LFC.iAddrPortCmd[CONVIdx] + ".E", 0);  // input mode
                    LFC.FunWriPLC_Bit("D" + LFC.iAddrPortCmd[CONVIdx] + ".F", 0);  // output mode                 
                    LFC.FunWriPLC_Bit("D" + LFC.iAddrPortSts1[CONVIdx] + ".6", 0);  // Input request 
                    break;
                //Request OutMode
                case clsLifterMPLC.MPLC.enuPortMode.OutMode:
                    LFC.FunWriPLC_Bit("D" + LFC.iAddrPortSts1[CONVIdx] + ".3", 0);  // input mode
                    LFC.FunWriPLC_Bit("D" + LFC.iAddrPortSts1[CONVIdx] + ".4", 1);  // output mode
                    LFC.FunWriPLC_Bit("D" + LFC.iAddrPortCmd[CONVIdx] + ".E", 0);  // input mode
                    LFC.FunWriPLC_Bit("D" + LFC.iAddrPortCmd[CONVIdx] + ".F", 0);  // output mode           
                    LFC.FunWriPLC_Bit("D" + LFC.iAddrPortSts1[CONVIdx] + ".7", 0);  // output request 
                    break;
            }
        }

        #endregion

        private void frmM_Load(object sender, EventArgs e)
        {
            clsLifterMPLC.MPLC.LFCConfig Config = new clsLifterMPLC.MPLC.LFCConfig();
            Config.CONV_QTY = 4;
            Config.TRU_QTY = 2;
            Config.WordAddress_QTY = 601;
            objLFC.subStart(Config);

            ChkAppIsAlreadyRunning();
            this.Text = "Mirle Lifter_Simulator (V." + Application.ProductVersion + ")";

            cboPortSelect.Items.Clear();
            cboStageSelect.Items.Clear();
            for (int i = 0; i < Mirle.clsLC_Def.dicCONDef.Count; i++) 
            {
                if (Mirle.clsLC_Def.dicCONDef[i].PortType == 3)
                {
                    cboPortSelect.Items.Add(Mirle.clsLC_Def.dicCONDef[i].HostEQPortID);
                }
            }
            cboPortSelect.SelectedIndex = -1;
            TRU1._iTRU = 0; 
            TRU2._iTRU = 1;
            tmMainProc.Enabled = true;
        }

        private void tmMainProc_Tick(object sender, EventArgs e)
        {
            try
            {
                tmMainProc.Enabled = false;

                lblConn_PLC1.BackColor = (LFC.bConnectPLC == true ? Color_SignOn : Color_SignOff);

                if (LFC.bConnectPLC == true)
                {
                    TRU1._iTRU = 0; 
                    TRU2._iTRU = 1; 
                    MainProcess();       
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                tmMainProc.Enabled = true;
            }
        }


        /// <summary>
        /// PLC訊號主流程
        /// </summary>           
        private void MainProcess()
        {
            txtTRU1.Text = iTRU1_Step[0].ToString();
            txtTRU2.Text = iTRU1_Step[1].ToString();
            txtLFT.Text = iLFT_Step.ToString();

            #region Abnormal Control

            if (rdbtnAbnormal.Checked == true && cboAbnormal.Text != "" && cboPortSelect.Text != "" && cboSourceDest.Text != "")  
            {
                switch (cboAbnormal.SelectedIndex) //異常流程選擇
                {
                    case 0:
                        if(TRU1step < 99)
                        {
                            if (TRU_PosSelect(cboWhere.SelectedIndex, cboSourceDest.SelectedIndex, PLC.MPLC.TRU.L10))
                                TRU1step = 99;  //interlock error
                        }
                        break;

                    case 1:
                        if(TRU1step < 199)
                        {
                            if (TRU_PosSelect(cboWhere.SelectedIndex, cboSourceDest.SelectedIndex, PLC.MPLC.TRU.L10))
                                TRU1step = 199; // alarm
                        }
                        break;

                    case 2:
                        if (TRU2step < 99)
                        {
                            if (TRU_PosSelect(cboWhere.SelectedIndex, cboSourceDest.SelectedIndex, PLC.MPLC.TRU.L30))
                                TRU2step = 99;  //interlock error
                        }
                        break;

                    case 3:
                        if (TRU2step < 199)
                        {
                            if (TRU_PosSelect(cboWhere.SelectedIndex, cboSourceDest.SelectedIndex, PLC.MPLC.TRU.L30))
                                TRU2step = 199; // alarm
                        }
                        break;

                    default:
                        break;
                }
            }
            #endregion

            #region LFC Proc

                #region 初始化 &  Check LFT's command Mode
                if (LFTstep == 0)
                {
                    if (clsLifterMPLC.MPLC.LFC_C.L2LFC.Sts_1.Online == clsLifterMPLC.MPLC.enuSignal.ON &&
                        clsLifterMPLC.MPLC.LFC_C.L2LFC.Sts_1.Ready == clsLifterMPLC.MPLC.enuSignal.ON)
                    {
                        CheckLFTCommandMode();
                    }
                }
                #endregion

                switch (LFTstep)
                {
                    #region Normal_Proc
                    case 1:     //T1 流程
                        {
                            LFTstep = LFC.LFT_T1_Proc();
                            break;
                        }
                    case 2:     //LFT取物流程
                        {
                            LFTstep = LFC.LFT_SelectFrom();
                            break;
                        }
                    case 7:     //TRU 1 pick up 交握流程
                        {
                            LFTstep = LFC.LFT_Pick_up_TRU(PLC.MPLC.TRU.L10);
                            break;
                        }
                    case 8:     //TRU 2 pick up 交握流程
                        {
                            LFTstep = LFC.LFT_Pick_up_TRU(PLC.MPLC.TRU.L30);
                            break;
                        }
                    case 10:    //LFT 置物
                        {
                            LFTstep = LFC.LFT_SelectTo();
                            break;
                        }
                    case 15:    //LFT Deposite TRU1
                        {
                            LFTstep = LFC.LFT_Deposite_TRU(PLC.MPLC.TRU.L10);
                            break;
                        }
                    case 16:    //LFT Deposite TRU2
                        {
                            LFTstep = LFC.LFT_Deposite_TRU(PLC.MPLC.TRU.L30);
                            break;
                        }
                    case 18:    //transfer command T3 move 
                        {
                            LFTstep = LFC.LFT_TransferMove();
                            break;
                        }
                    case 20:    //Normal complete、ready on、busy off
                        {
                            LFTstep = LFC.LFT_Normal_Fin();
                            break;
                        }
                    #endregion

                    #region interlock alarm 流程
                    case 99:
                        {
                            string sData = "";
                            string sAddr = "";

                            sAddr = "D" + LFC.iAddrLFT_Sts1 + ".9";
                            bool bRet = LFC.FunWriPLC_Bit(sAddr, 0);  // Normal complete
                            sAddr = "D" + LFC.iAddrLFT_Sts1 + ".1";
                            bRet = LFC.FunWriPLC_Bit(sAddr, 0); // ready off

                            if (clsLifterMPLC.MPLC.LFC_C.L2LFC.ErrorIdx == clsLifterMPLC.MPLC.LFC_C.LFC2L.PCErrorIdx)
                            {
                                LFTErrorIndex = clsLifterMPLC.MPLC.LFC_C.L2LFC.ErrorIdx;
                                LFTErrorIndex++;

                                string temp = LFTalarmcode.ToString();

                                sData = temp;
                                sAddr = "D" + LFC.iAddrLFT_ErrorCode; //LFT  error code
                                bRet = LFC.FunWriPLC_Word(sAddr, sData);

                                sData = LFTErrorIndex.ToString();
                                sAddr = "D" + LFC.iAddrLFT_ErrorIdx; //LFT  error index
                                bRet = LFC.FunWriPLC_Word(sAddr, sData);

                                sAddr = "D" + LFC.iAddrLFT_Sts2 + ".3";
                                bRet = LFC.FunWriPLC_Bit(sAddr, 1); //LFT interlock error on

                                LFTstep = 100;
                                SpinWait.SpinUntil(() => false, 500);
                            }
                            break;
                        }
                    case 100:
                        {
                            if (clsLifterMPLC.MPLC.LFC_C.LFC2L.LFCCmd.InterlockError == clsLifterMPLC.MPLC.enuSignal.ON)
                            {
                                string sAddr = "";
                                sAddr = "D" + LFC.iAddrLFT_Sts2 + ".3";
                                bool bRet = LFC.FunWriPLC_Bit(sAddr, 0); //LFT interlock error off

                                LFTstep = 101;
                            }
                            else
                                LFTstep = 100;
                            break;
                        }
                    case 101:
                        {
                            if (clsLifterMPLC.MPLC.LFC_C.LFC2L.LFCCmd.InterlockError == clsLifterMPLC.MPLC.enuSignal.OFF)
                            {
                                string sData = "";
                                string sAddr = "";
                                sAddr = "D" + LFC.iAddrLFT_Sts1 + ".2";
                                bool bRet = LFC.FunWriPLC_Bit(sAddr, 0);  // busy off

                                sData = "0,0,0,0,0,0,0,0,0,0,0";
                                sAddr = "D" + LFC.iAddrLFT_ClearCmd;
                                bRet = LFC.FunWriPLC_Word(sAddr, sData);

                                sAddr = "D" + LFC.iAddrLFT_Sts1 + ".A";
                                bRet = LFC.FunWriPLC_Bit(sAddr, 1);  // abnormal complete on


                                sAddr = "D" + LFC.iAddrLFT_Sts1 + ".1";
                                bRet = LFC.FunWriPLC_Bit(sAddr, 1); // ready on

                                sAddr = "D" + LFC.iAddrLFT_ErrorCode;
                                bRet = LFC.FunWriPLC_Word(sAddr, 0); // Clear error code

                                LFTErrorIndex++;
                                sData = LFTErrorIndex.ToString();
                                sAddr = "D" + LFC.iAddrLFT_ErrorIdx; //LFT  error index
                                bRet = LFC.FunWriPLC_Word(sAddr, sData);

                                SpinWait.SpinUntil(() => false, 500);

                                sAddr = "D" + LFC.iAddrLFT_Sts1 + ".A";
                                bRet = LFC.FunWriPLC_Bit(sAddr, 0);  // abnormal complete 0ff
                                LFTstep = 0;
                            }
                            break;
                        }
                    #endregion

                    #region alarm 流程
                    case 199:
                        {
                            string sData = "";
                            string sAddr = "";

                            sAddr = "D" + LFC.iAddrLFT_Sts1 + ".9";
                            bool bRet = LFC.FunWriPLC_Bit(sAddr, 0);  // Normal complete
                            sAddr = "D" + LFC.iAddrLFT_Sts1 + ".1";
                            bRet = LFC.FunWriPLC_Bit(sAddr, 0); // ready off
                            sAddr = "D" + LFC.iAddrLFT_Sts1 + ".2";
                            bRet = LFC.FunWriPLC_Bit(sAddr, 0);  // busy off
                            sAddr = "D" + LFC.iAddrLFT_IFSts + ".0";
                            bRet = LFC.FunWriPLC_Bit(sAddr, 0); // LDRQ off                                
                            sAddr = "D" + LFC.iAddrLFT_IFSts + ".1";
                            bRet = LFC.FunWriPLC_Bit(sAddr, 0); // UDRQ off

                            if (clsLifterMPLC.MPLC.LFC_C.L2LFC.ErrorIdx == clsLifterMPLC.MPLC.LFC_C.LFC2L.PCErrorIdx)
                            {
                                LFTErrorIndex = clsLifterMPLC.MPLC.LFC_C.L2LFC.ErrorIdx;
                                LFTErrorIndex++;

                                string temp = LFTalarmcode.ToString();

                                sData = temp;
                                sAddr = "D" + LFC.iAddrLFT_ErrorCode; //LFT  error code
                                bRet = LFC.FunWriPLC_Word(sAddr, sData);

                                sData = LFTErrorIndex.ToString();
                                sAddr = "D" + LFC.iAddrLFT_ErrorIdx; //LFT  error index
                                bRet = LFC.FunWriPLC_Word(sAddr, sData);

                                sAddr = "D" + LFC.iAddrLFT_Sts1 + ".4";
                                bRet = LFC.FunWriPLC_Bit(sAddr, 1); //LFT error on
                                LFTstep = 200;
                                SpinWait.SpinUntil(() => false, 500);
                            }

                            sData = "0,0,0,0,0,0,0,0,0,0,0";
                            sAddr = "D" + LFC.iAddrLFT_ClearCmd;
                            bRet = LFC.FunWriPLC_Word(sAddr, sData); //命令清空                               
                            break;
                        }
                    case 200:
                        {
                            string sAddr = "";
                            sAddr = "D" + LFC.iAddrLFT_Sts1 + ".A";
                            bool bRet = LFC.FunWriPLC_Bit(sAddr, 1);  // abnormal complete on                               
                            LFTstep = 201;
                            SpinWait.SpinUntil(() => false, 500);
                            break;
                        }
                    case 201:
                        {
                            string sAddr = "";

                            if (clsLifterMPLC.MPLC.LFC_C.LFC2L.LFCCmd.AlarmReset == clsLifterMPLC.MPLC.enuSignal.ON)
                            {
                                sAddr = "D" + LFC.iAddrLFT_Sts1 + ".4";
                                bool bRet = LFC.FunWriPLC_Bit(sAddr, 0); //LFT error off

                                sAddr = "D" + LFC.iAddrLFT_Sts1 + ".1";
                                bRet = LFC.FunWriPLC_Bit(sAddr, 1); // ready on

                                sAddr = "D" + LFC.iAddrLFT_ErrorCode;
                                bRet = LFC.FunWriPLC_Word(sAddr, 0); // Clear error code
                                LFC.FunWriPLC_Bit("D" + LFC.iAddrLFT_Reset + ".1", 0);

                                LFTErrorIndex++;
                                string sData = LFTErrorIndex.ToString();
                                sAddr = "D" + LFC.iAddrLFT_ErrorIdx; //LFT  error index
                                bRet = LFC.FunWriPLC_Word(sAddr, sData);

                                LFTstep = 0;
                            }
                            break;
                        }
                    #endregion

                    default:
                        break;
                }
                #endregion

            #region TRU_1 Proc

            TRUnum = 0;  //TRU -1
            switch (TRU1step)
            {
                #region Normal Proc
                case 1:   //T1 流程
                    {
                        TRU1step = LFC.TRU_T1_Proc(PLC.MPLC.TRU.L10);
                        break;
                    }

                case 2: // T2-1 -0   pick up的取物流程
                    {
                        TRU1step = LFC.TRU_T2_Select(PLC.MPLC.TRU.L10);
                        break;
                    }

                case 3: // T2 (免交握)     pick up 執行
                    {
                        TRU1step = LFC.TRU_T2_PickUp(PLC.MPLC.TRU.L10);
                        break;
                    }

                case 7: // T2-2-0   pick up  (交握)
                    {
                        TRU1step = LFC.TRU_T2_Handshake(PLC.MPLC.TRU.L10);
                        break;
                    }

                case 8: //T2 Finish
                    {
                        TRU1step = LFC.TRU_T2_Fin(PLC.MPLC.TRU.L10);
                        break;
                    }


                case 10: //T3-1-0
                    {
                        TRU1step = LFC.TRU_T3_Select(PLC.MPLC.TRU.L10);
                        break;
                    }

                case 11: //T3-1 deposite  沒交握
                    {
                        TRU1step = LFC.TRU_T3_Deposite(PLC.MPLC.TRU.L10);
                        break;
                    }

                case 15:
                    {
                        TRU1step = LFC.TRU_T3_Fin(PLC.MPLC.TRU.L10);
                        break;
                    }

                case 18: //transfer command T3 move 
                    {
                        TRU1step = LFC.TRU_T3_TransferMove(PLC.MPLC.TRU.L10);
                        break;
                    }

                case 20:
                    {
                        TRU1step = LFC.TRU_NormalFin(PLC.MPLC.TRU.L10);
                        break;
                    }
                #endregion

                #region Interlock alarm 流程
                case 99:
                    {
                        string sData = "";
                        string sAddr = "";

                        sAddr = "D" + LFC.iAddrTRU1_Sts1 + ".9";
                        LFC.FunWriPLC_Bit(sAddr, 0);  // Normal complete off
                        if (clsLifterMPLC.MPLC.LFC_C.T2LFC[0].ErrorIdx == clsLifterMPLC.MPLC.LFC_C.LFC2T[0].PCErrorIdx)
                        {
                            TRU1ErrorIndex = clsLifterMPLC.MPLC.LFC_C.T2LFC[0].ErrorIdx;
                            TRU1ErrorIndex++;

                            string temp = TRU1alarmcode.ToString();


                            sData = temp;
                            sAddr = "D" + LFC.iAddrTRU1_ErrorCode; //TRU  error code
                            bool bRet = LFC.FunWriPLC_Word(sAddr, sData);

                            sData = TRU1ErrorIndex.ToString();
                            sAddr = "D" + LFC.iAddrTRU1_ErrorIdx; //TRU  error index
                            bRet = LFC.FunWriPLC_Word(sAddr, sData);

                            sAddr = "D" + LFC.iAddrTRU1_Sts1 + ".1";
                            bRet = LFC.FunWriPLC_Bit(sAddr, 0); // ready off

                            sAddr = "D" + LFC.iAddrTRU1_Sts2 + ".3";
                            bRet = LFC.FunWriPLC_Bit(sAddr, 1); //LFT interlock error on
                            TRU1step = 100;
                            SpinWait.SpinUntil(() => false, 500);
                        }
                        break;
                    }
                case 100:
                    {
                        if (clsLifterMPLC.MPLC.LFC_C.LFC2T[0].TRUCCmd.InterlockAck == clsLifterMPLC.MPLC.enuSignal.ON)
                        {

                            string sAddr = "";
                            sAddr = "D" + LFC.iAddrTRU1_Sts2 + ".3";
                            bool bRet = LFC.FunWriPLC_Bit(sAddr, 0); //LFT interlock error off

                            TRU1step = 101;
                        }
                        break;
                    }
                case 101:
                    {
                        if (clsLifterMPLC.MPLC.LFC_C.LFC2T[0].TRUCCmd.InterlockAck == clsLifterMPLC.MPLC.enuSignal.OFF)
                        {
                            string sData = "";
                            string sAddr = "";
                            sAddr = "D" + LFC.iAddrTRU1_Sts1 + ".2";
                            bool bRet = LFC.FunWriPLC_Bit(sAddr, 0);  // busy off

                            sData = "0,0,0,0,0,0,0,0,0,0,0";
                            sAddr = "D" + LFC.iAddrLFT_ClearCmd;
                            bRet = LFC.FunWriPLC_Word(sAddr, sData);

                            sAddr = "D" + LFC.iAddrTRU1_Sts1 + ".A";
                            bRet = LFC.FunWriPLC_Bit(sAddr, 1);  // abnormal complete on


                            sAddr = "D" + LFC.iAddrTRU1_Sts1 + ".1";
                            bRet = LFC.FunWriPLC_Bit(sAddr, 1); // ready on

                            sAddr = "D" + LFC.iAddrLFT_ErrorCode;
                            bRet = LFC.FunWriPLC_Word(sAddr, 0); // Clear error code

                            TRU1ErrorIndex++;
                            sData = TRU1ErrorIndex.ToString();
                            sAddr = "D" + LFC.iAddrTRU1_ErrorIdx; //TRU  error index
                            bRet = LFC.FunWriPLC_Word(sAddr, sData);
                            SpinWait.SpinUntil(() => false, 1000);

                            sAddr = "D" + LFC.iAddrTRU1_Sts1 + ".A";
                            bRet = LFC.FunWriPLC_Bit(sAddr, 0);  // abnormal complete off
                            TRU1step = 0;
                        }
                        break;
                    }
                #endregion

                #region Alarm 流程
                case 199:
                    {
                        string sData = "";
                        string sAddr = "";

                        sAddr = "D" + LFC.iAddrTRU1_Sts1 + ".1";
                        bool bRet = LFC.FunWriPLC_Bit(sAddr, 0); // ready off

                        sAddr = "D" + LFC.iAddrTRU1_Sts1 + ".2";
                        bRet = LFC.FunWriPLC_Bit(sAddr, 0);  // busy off

                        sAddr = "D" + LFC.iAddrTRU1_IFSts + ".0";
                        bRet = LFC.FunWriPLC_Bit(sAddr, 0); // LDRQ off

                        sAddr = "D" + LFC.iAddrTRU1_IFSts + ".1";
                        bRet = LFC.FunWriPLC_Bit(sAddr, 0); // UDRQ off

                        sAddr = "D" + LFC.iAddrTRU1_Sts1 + ".9";
                        bRet = LFC.FunWriPLC_Bit(sAddr, 0);  // Normal complete off

                        if (clsLifterMPLC.MPLC.LFC_C.T2LFC[0].ErrorIdx == clsLifterMPLC.MPLC.LFC_C.LFC2T[0].PCErrorIdx)
                        {
                            TRU1ErrorIndex = clsLifterMPLC.MPLC.LFC_C.T2LFC[0].ErrorIdx;
                            TRU1ErrorIndex++;

                            string temp = TRU1alarmcode.ToString();


                            sData = temp;
                            sAddr = "D" + LFC.iAddrTRU1_ErrorCode; //LFT  error code
                            bRet = LFC.FunWriPLC_Word(sAddr, sData);

                            sData = TRU1ErrorIndex.ToString();
                            sAddr = "D" + LFC.iAddrTRU1_ErrorIdx; //LFT  error index
                            bRet = LFC.FunWriPLC_Word(sAddr, sData);

                            sAddr = "D" + LFC.iAddrTRU1_Sts1 + ".4";
                            bRet = LFC.FunWriPLC_Bit(sAddr, 1); //LFT error on
                            TRU1step = 200;
                            SpinWait.SpinUntil(() => false, 500);
                        }
                        sData = "0,0,0,0,0,0,0,0,0,0,0";
                        sAddr = "D" + LFC.iAddrTRU1_ClearCmd;
                        bRet = LFC.FunWriPLC_Word(sAddr, sData); //命令清空

                        break;
                    }
                case 200:
                    {
                        string sAddr = "";

                        sAddr = "D" + LFC.iAddrTRU1_Sts1 + ".A";
                        bool bRet = LFC.FunWriPLC_Bit(sAddr, 1);  // abnormal complete on

                        TRU1step = 201;
                        SpinWait.SpinUntil(() => false, 1000);

                        break;
                    }
                case 201:
                    {

                        string sAddr = "";

                        if (clsLifterMPLC.MPLC.LFC_C.LFC2T[0].TRUCCmd.AlarmReset == clsLifterMPLC.MPLC.enuSignal.ON)
                        {
                            sAddr = "D" + LFC.iAddrTRU1_Sts1 + ".4";
                            bool bRet = LFC.FunWriPLC_Bit(sAddr, 0); //LFT error off

                            sAddr = "D" + LFC.iAddrTRU1_Sts1 + ".1";
                            bRet = LFC.FunWriPLC_Bit(sAddr, 1); // ready on
                            sAddr = "D" + LFC.iAddrTRU1_ErrorCode;
                            bRet = LFC.FunWriPLC_Word(sAddr, 0); // Clear error code

                            TRU1ErrorIndex++;
                            string sData = TRU1ErrorIndex.ToString();
                            sAddr = "D" + LFC.iAddrTRU1_ErrorIdx; //TRU  error index
                            bRet = LFC.FunWriPLC_Word(sAddr, sData);

                            LFC.FunWriPLC_Bit("D" + LFC.iAddrTRU1_Reset + ".1", 0);
                            TRU1step = 0;
                        }

                        break;
                    }
                #endregion

                default:
                    break;
            }

            #region 初始化 &  Check TRU-1's command Mode
            if (TRUnum == 0 && TRU1step == 0) //TRU-1 目前沒有執行的方案
            {
                if (clsLifterMPLC.MPLC.LFC_C.T2LFC[TRUnum].Sts_1.Online == clsLifterMPLC.MPLC.enuSignal.ON &&
                    clsLifterMPLC.MPLC.LFC_C.T2LFC[TRUnum].Sts_1.Ready == clsLifterMPLC.MPLC.enuSignal.ON)//檢查 TRU-1 是否 online & ready
                {
                    CheckTRUCommandMode(TRUnum);
                }
            }
            #endregion

            #endregion

            #region TRU_2 Proc

            TRUnum = 1;  //TRU -2
            switch (TRU2step)
            {
                #region Normal Proc
                case 1:   //T1 流程
                    {
                        TRU2step = LFC.TRU_T1_Proc(PLC.MPLC.TRU.L30);
                        break;
                    }
                case 2: // T2-1 -0   pick up的取物流程
                    {
                        TRU2step = LFC.TRU_T2_Select(PLC.MPLC.TRU.L30);
                        break;
                    }
                case 3: // T2 -1(免交握) -1     pick up 執行
                    {
                        TRU2step = LFC.TRU_T2_PickUp(PLC.MPLC.TRU.L30);

                        break;
                    }
                case 7: // T2-2-0   pick up  (交握)
                    {

                        TRU2step = LFC.TRU_T2_Handshake(PLC.MPLC.TRU.L30);
                        break;
                    }
                case 8:     //T2 Finish
                    {
                        TRU2step = LFC.TRU_T2_Fin(PLC.MPLC.TRU.L30);
                        break;
                    }
                case 10: //T3-1-0
                    {
                        TRU2step = LFC.TRU_T3_Select(PLC.MPLC.TRU.L30);
                        break;
                    }

                case 11: //T3-1 deposite  沒交握
                    {
                        TRU2step = LFC.TRU_T3_Deposite(PLC.MPLC.TRU.L30);
                        break;
                    }

                case 15:
                    {
                        TRU2step = LFC.TRU_T3_Fin(PLC.MPLC.TRU.L30);
                        break;
                    }
                case 18: //transfer command T3 move 
                    {
                        TRU2step = LFC.TRU_T3_TransferMove(PLC.MPLC.TRU.L30);
                        break;
                    }
                case 20:
                    {
                        TRU2step = LFC.TRU_NormalFin(PLC.MPLC.TRU.L30);
                        break;
                    }

                #endregion

                #region Interlock alarm 流程
                case 99:
                    {
                        string sData = "";
                        string sAddr = "";

                        sAddr = "D" + LFC.iAddrTRU2_Sts1 + ".9";
                        LFC.FunWriPLC_Bit(sAddr, 0);  // Normal complete off
                        if (clsLifterMPLC.MPLC.LFC_C.T2LFC[1].ErrorIdx == clsLifterMPLC.MPLC.LFC_C.LFC2T[1].PCErrorIdx)
                        {
                            TRU2ErrorIndex = clsLifterMPLC.MPLC.LFC_C.T2LFC[1].ErrorIdx;
                            TRU2ErrorIndex++;

                            string temp = TRU2alarmcode.ToString();

                            sData = temp;
                            sAddr = "D" + LFC.iAddrTRU2_ErrorCode; //LFT  error code
                            bool bRet = LFC.FunWriPLC_Word(sAddr, sData);

                            sData = TRU2ErrorIndex.ToString();
                            sAddr = "D" + LFC.iAddrTRU2_ErrorIdx; //LFT  error index
                            bRet = LFC.FunWriPLC_Word(sAddr, sData);

                            sAddr = "D" + LFC.iAddrTRU2_Sts1 + ".1";
                            bRet = LFC.FunWriPLC_Bit(sAddr, 0); // ready off


                            sAddr = "D" + LFC.iAddrTRU2_Sts2 + ".3";
                            bRet = LFC.FunWriPLC_Bit(sAddr, 1); //LFT interlock error on

                            TRU2step = 100;
                            SpinWait.SpinUntil(() => false, 500);
                        }
                        break;
                    }
                case 100:
                    {
                        if (clsLifterMPLC.MPLC.LFC_C.LFC2T[1].TRUCCmd.InterlockAck == clsLifterMPLC.MPLC.enuSignal.ON)
                        {
                            string sAddr = "";
                            sAddr = "D" + LFC.iAddrTRU2_Sts2 + ".3";
                            bool bRet = LFC.FunWriPLC_Bit(sAddr, 0); //LFT interlock error off

                            TRU2step = 101;
                        }
                        else
                            TRU2step = 100;
                        break;
                    }
                case 101:
                    {
                        if (clsLifterMPLC.MPLC.LFC_C.LFC2T[1].TRUCCmd.InterlockAck == clsLifterMPLC.MPLC.enuSignal.OFF)
                        {
                            string sData = "";
                            string sAddr = "";
                            sAddr = "D" + LFC.iAddrTRU2_Sts1 + ".2";
                            bool bRet = LFC.FunWriPLC_Bit(sAddr, 0);  // busy off

                            sData = "0,0,0,0,0,0,0,0,0,0,0";
                            sAddr = "D" + LFC.iAddrTRU2_ClearCmd;
                            bRet = LFC.FunWriPLC_Word(sAddr, sData);

                            sAddr = "D" + LFC.iAddrTRU2_Sts1 + ".A";
                            bRet = LFC.FunWriPLC_Bit(sAddr, 1);  // abnormal complete on


                            sAddr = "D" + LFC.iAddrTRU2_Sts1 + ".1";
                            bRet = LFC.FunWriPLC_Bit(sAddr, 1); // ready on

                            sAddr = "D" + LFC.iAddrTRU2_ErrorCode;
                            bRet = LFC.FunWriPLC_Word(sAddr, 0); // Clear error code

                            TRU2ErrorIndex++;
                            sData = TRU2ErrorIndex.ToString();
                            sAddr = "D" + LFC.iAddrTRU2_ErrorIdx; //TRU  error index
                            bRet = LFC.FunWriPLC_Word(sAddr, sData);

                            SpinWait.SpinUntil(() => false, 1000);

                            sAddr = "D" + LFC.iAddrTRU2_Sts1 + ".A";
                            bRet = LFC.FunWriPLC_Bit(sAddr, 0);  // abnormal complete off
                            TRU2step = 0;
                        }
                        break;
                    }
                #endregion

                #region Alarm 流程
                case 199:
                    {
                        string sData = "";
                        string sAddr = "";


                        sAddr = "D" + LFC.iAddrTRU2_Sts1 + ".1";
                        bool bRet = LFC.FunWriPLC_Bit(sAddr, 0); // ready off
                        sAddr = "D" + LFC.iAddrTRU2_Sts1 + ".2";
                        bRet = LFC.FunWriPLC_Bit(sAddr, 0);  // busy off


                        sAddr = "D" + LFC.iAddrTRU2_IFSts + ".0";
                        bRet = LFC.FunWriPLC_Bit(sAddr, 0); // LDRQ off

                        sAddr = "D" + LFC.iAddrTRU2_IFSts + ".1";
                        bRet = LFC.FunWriPLC_Bit(sAddr, 0); // UDRQ off

                        sAddr = "D" + LFC.iAddrTRU2_Sts1 + ".9";
                        LFC.FunWriPLC_Bit(sAddr, 0);  // Normal complete off
                        if (clsLifterMPLC.MPLC.LFC_C.T2LFC[1].ErrorIdx == clsLifterMPLC.MPLC.LFC_C.LFC2T[1].PCErrorIdx)
                        {
                            TRU2ErrorIndex = clsLifterMPLC.MPLC.LFC_C.T2LFC[1].ErrorIdx;
                            TRU2ErrorIndex++;

                            string temp = TRU2alarmcode.ToString();

                            sData = temp;
                            sAddr = "D" + LFC.iAddrTRU2_ErrorCode; //LFT  error code
                            bRet = LFC.FunWriPLC_Word(sAddr, sData);

                            sData = TRU2ErrorIndex.ToString();
                            sAddr = "D" + LFC.iAddrTRU2_ErrorIdx; //LFT  error index
                            bRet = LFC.FunWriPLC_Word(sAddr, sData);

                            sAddr = "D" + LFC.iAddrTRU2_Sts1 + ".4";
                            bRet = LFC.FunWriPLC_Bit(sAddr, 1); //LFT error on

                            TRU2step = 200;
                            SpinWait.SpinUntil(() => false, 500);
                        }

                        sData = "0,0,0,0,0,0,0,0,0,0,0";
                        sAddr = "D" + LFC.iAddrTRU2_ClearCmd;
                        bRet = LFC.FunWriPLC_Word(sAddr, sData); //命令清空
                        break;
                    }

                case 200:
                    {
                        string sAddr = "";
                        sAddr = "D" + LFC.iAddrTRU2_Sts1 + ".A";
                        bool bRet = LFC.FunWriPLC_Bit(sAddr, 1);  // abnormal complete on
                        TRU2step = 201;
                        SpinWait.SpinUntil(() => false, 1000);
                        break;
                    }

                case 201:
                    {
                        string sAddr = "";

                        if (clsLifterMPLC.MPLC.LFC_C.LFC2T[1].TRUCCmd.AlarmReset == clsLifterMPLC.MPLC.enuSignal.ON)
                        {
                            sAddr = "D" + LFC.iAddrTRU2_Sts1 + ".4";
                            bool bRet = LFC.FunWriPLC_Bit(sAddr, 0); //LFT error off

                            sAddr = "D" + LFC.iAddrTRU2_Sts1 + ".1";
                            bRet = LFC.FunWriPLC_Bit(sAddr, 1); // ready on

                            sAddr = "D" + LFC.iAddrTRU2_ErrorCode;
                            bRet = LFC.FunWriPLC_Word(sAddr, 0); // Clear error code

                            TRU2ErrorIndex++;
                            string sData = TRU2ErrorIndex.ToString();
                            sAddr = "D" + LFC.iAddrTRU2_ErrorIdx; //TRU  error index
                            bRet = LFC.FunWriPLC_Word(sAddr, sData);

                            LFC.FunWriPLC_Bit("D" + LFC.iAddrTRU2_Reset + "1", 0);
                            TRU2step = 0;
                        }
                        break;
                    }
                #endregion

                default:
                    break;
            }
            #region 初始化 &  Check TRU-2's command Mode
            if (TRUnum == 1 && TRU2step == 0) //TRU-2 目前沒有執行的方案
            {
                if (clsLifterMPLC.MPLC.LFC_C.T2LFC[TRUnum].Sts_1.Online == clsLifterMPLC.MPLC.enuSignal.ON &&
                       clsLifterMPLC.MPLC.LFC_C.T2LFC[TRUnum].Sts_1.Ready == clsLifterMPLC.MPLC.enuSignal.ON)//檢查 TRU-2 是否 online & ready
                {
                    CheckTRUCommandMode(TRUnum);
                }
            }
            #endregion

            #endregion

            #region Port event Check

            LFC.Run2Stop2Run(PLC.MPLC.Conveyor.L10L1);
            LFC.Run2Stop2Run(PLC.MPLC.Conveyor.L10L2);
            LFC.Run2Stop2Run(PLC.MPLC.Conveyor.L30L1);
            LFC.Run2Stop2Run(PLC.MPLC.Conveyor.L30L2);

            #endregion
        }

        #region Method

        /// <summary>
        /// TRU 檢查步驟是否到位
        /// </summary>
        /// <param name="iFunc">Pick or Deposite</param>
        /// <returns>是否到達設定位置</returns>
        private bool TRU_PosSelect(int iFunc, int iTarget, PLC.MPLC.TRU iTRU)
        {
            bool bReachPos = false;

            switch (iFunc)
            {
                case 0: //Pick up
                    {
                        if ((int)iTRU == 1)
                        {
                            if (iTarget == 1 && TRU1step >= 3) //Dest.
                            {
                                if (TRU1step != 0 && clsLifterMPLC.MPLC.LFC_C.T2LFC[0].Sts_2.Stock == clsLifterMPLC.MPLC.enuSignal.ON)
                                {
                                    bReachPos = true;
                                    if (clsLifterMPLC.MPLC.LFC_C.LFC2L.LFCmdMode != 0)
                                    {
                                        LFTstep = 99;
                                    }
                                }
                            }
                            else if(iTarget == 0)
                            {
                                if (TRU1step == 3 || TRU1step == 7) //3表示Port、7表示Lifter
                                bReachPos = true;
                            }
                                    break;
                        }
                        else if ((int)iTRU == 2)
                        {
                            if (iTarget == 1 && TRU2step >= 3) //Dest.
                            {
                                if (TRU2step != 0 && clsLifterMPLC.MPLC.LFC_C.T2LFC[1].Sts_2.Stock == clsLifterMPLC.MPLC.enuSignal.ON)
                                {
                                    bReachPos = true;
                                    if (clsLifterMPLC.MPLC.LFC_C.LFC2L.LFCmdMode != 0)
                                    {
                                        LFTstep = 99;
                                    }
                                }
                            }
                            else if (iTarget == 0)
                            {
                                if (TRU2step == 3 || TRU2step == 7)
                                bReachPos = true;
                            }
                            break;
                        }
                        else
                            break;
                    }

                case 1: //Deposite
                    {
                        if ((int)iTRU == 1)
                        {
                            if (iTarget == 1 )//Dest
                            {
                                if(LFTstep == 0)
                                {
                                    if (TRU1step == 20)
                                        bReachPos = true;
                                }
                                else
                                {
                                    if (clsLifterMPLC.MPLC.LFC_C.LFC2T[1].LFCmdMode != 0 && clsLifterMPLC.MPLC.LFC_C.L2LFC.Sts_2.CSTIn == clsLifterMPLC.MPLC.enuSignal.ON)
                                    {
                                        bReachPos = true;
                                        TRU2step = 99;
                                    }
                                }
                            }
                            else if(iTarget == 0)
                            {
                                if (TRU1step == 11 || TRU1step == 15) //11表示Port、15表示Lifter
                                    bReachPos = true;
                            }
                            break;
                        }
                        else if ((int)iTRU == 2)
                        {

                            if (iTarget == 1)//Dest
                            {
                                if (LFTstep == 0)
                                {
                                    if (TRU2step == 20)
                                        bReachPos = true;
                                }
                                else
                                {
                                    if (clsLifterMPLC.MPLC.LFC_C.LFC2T[0].LFCmdMode != 0 && clsLifterMPLC.MPLC.LFC_C.L2LFC.Sts_2.CSTIn == clsLifterMPLC.MPLC.enuSignal.ON)
                                    {
                                        bReachPos = true;
                                        TRU1step = 99;
                                    }
                                }
                            }
                            else if(iTarget == 0)
                            {
                                if (TRU2step == 11 || TRU2step == 15)
                                    bReachPos = true;
                            }
                            break;
                        }
                        else
                            break;
                    }
                default:
                    break;
            }
            if (bReachPos)
            {
                if (iTRU == PLC.MPLC.TRU.L10)
                {
                    LFC.FunWriPLC_Word("D" + LFC.iAddrTRU1_IFSts, 0); //Clear handshake data
                    if (clsLifterMPLC.MPLC.LFC_C.LFC2L.LFCmdMode != 0 && clsLifterMPLC.MPLC.LFC_C.L2LFC.CurrentLV.CurrentLoc == LFC.iFirstFloor )
                    LFTstep = 99;
                }
                else if (iTRU == PLC.MPLC.TRU.L30)
                {
                    LFC.FunWriPLC_Word("D" + LFC.iAddrTRU2_IFSts, 0);
                    if (clsLifterMPLC.MPLC.LFC_C.LFC2L.LFCmdMode != 0 && clsLifterMPLC.MPLC.LFC_C.L2LFC.CurrentLV.CurrentLoc == LFC.iSecondFloor)
                    LFTstep = 99;
                }
            }
            return bReachPos;
        }

        /// <summary>
        /// TRU command Mode check 
        /// </summary>
        /// <param name="i">TRU編號</param>
        private void CheckTRUCommandMode(int i) //0= false , 1= true;
        {
            if (clsLifterMPLC.MPLC.LFC_C.LFC2T[i].LFCmdMode.ToString() != "0")
            {
                string sData = "";
                string sAddr = "";
                // i = TRU編號
                bool bRet = false;

                if (i == 0)
                {
                    sAddr = "D" + LFC.iAddrTRU1_Sts1 + ".9";
                    bRet = LFC.FunWriPLC_Bit(sAddr, 0);  // Normal complete off

                    SpinWait.SpinUntil(() => false, 700);
                    TRU1step = 1;
                }

                if (i == 1)
                {
                    sAddr = "D" + LFC.iAddrTRU2_Sts1 + ".9";
                    bRet = LFC.FunWriPLC_Bit(sAddr, 0);  // Normal complete off

                    SpinWait.SpinUntil(() => false, 700);
                    TRU2step = 1;
                }

                sAddr = (i == 0 ? "D" + LFC.iAddrTRU1_Sts1 + ".1" : "D" + LFC.iAddrTRU2_Sts1 + ".1");
                bRet = LFC.FunWriPLC_Bit(sAddr, 0); // ready off
                sAddr = (i == 0 ? "D" + LFC.iAddrTRU1_Sts1 + ".2" : "D" + LFC.iAddrTRU2_Sts1 + ".2");
                bRet = LFC.FunWriPLC_Bit(sAddr, 1);  // busy on

                sData = clsLifterMPLC.MPLC.LFC_C.LFC2T[i].SeqNo.ToString();
                sAddr = (i == 0 ? "D" + LFC.iAddrTRU1_SeqNo : "D" + LFC.iAddrTRU2_SeqNo);
                bRet = LFC.FunWriPLC_Word(sAddr, sData); //SeqNo

                string[] sArrayData = new string[4];
                sArrayData[0] = clsLifterMPLC.MPLC.LFC_C.LFC2T[i].LifterSpeed.ToString();
                sArrayData[1] = clsLifterMPLC.MPLC.LFC_C.LFC2T[i].TravelSpeed.ToString();
                sArrayData[2] = clsLifterMPLC.MPLC.LFC_C.LFC2T[i].RotateSpeed.ToString();
                sArrayData[3] = clsLifterMPLC.MPLC.LFC_C.LFC2T[i].ForkSpeed.ToString();

                sAddr = (i == 0 ? "D" + LFC.iAddrTRU1_Speed : "D" + LFC.iAddrTRU2_Speed);
                bRet = LFC.FunWriPLC_Word(sAddr, sArrayData[0] + "," + sArrayData[1] + "," + sArrayData[2] + "," + sArrayData[3]); //Speed
            }
        }

        /// <summary>
        /// LFT command Mode check
        /// </summary>
        private void CheckLFTCommandMode()
        {
            if (clsLifterMPLC.MPLC.LFC_C.LFC2L.LFCmdMode.ToString() != "0")
            {
                string sData = "";
                string sAddr = "";

                sAddr = "D" + LFC.iAddrLFT_Sts1 + ".9";
                bool bRet = LFC.FunWriPLC_Bit(sAddr, 0);  // Normal complete off

                sAddr = "D" + LFC.iAddrLFT_Sts1 + ".1";
                bRet = LFC.FunWriPLC_Bit(sAddr, 0); // ready off
                sAddr = "D" + LFC.iAddrLFT_Sts1 + ".2";
                bRet = LFC.FunWriPLC_Bit(sAddr, 1);  // busy on

                sData = clsLifterMPLC.MPLC.LFC_C.LFC2L.SeqNo.ToString();
                sAddr = "D" + LFC.iAddrLFT_SeqNo;
                bRet = LFC.FunWriPLC_Word(sAddr, sData); //SeqNo

                sAddr = "D" + LFC.iAddrLFT_Speed;
                sData = clsLifterMPLC.MPLC.LFC_C.LFC2L.Speed.ToString();
                bRet = LFC.FunWriPLC_Word(sAddr, sData); //Lifter Speed

                LFTstep = 1;
                SpinWait.SpinUntil(() => false, 800);
            }
        }

        private void Init_LFT()
        {
            string sData = "0,0,0,0,0,0,0,515,4096,0"; //等補
            bool bRet = LFC.FunWriPLC_Word("D" + LFC.iAddrLFT_Init, sData);
            sData = "0,0,0,0,0";  //T1~T4
            bRet = LFC.FunWriPLC_Word("D" + LFC.iAddrLFT_ClearSts, sData);

            string sData1 = "0,0,0,0,0,0,0,0,0,0,0";
            bool bRet1 = LFC.FunWriPLC_Word("D" + LFC.iAddrLFT_ClearCmd, sData1);
            LFTstep = 0;
        }

        private void Init_TRU1()
        {
            string sData = "0,0,0,0,0,0,0,515,1792";
            bool bRet = LFC.FunWriPLC_Word("D" + LFC.iAddrTRU1_Init, sData);
            sData = "0,0,0,0,0";  //T1~T4, IF Status
            bRet = LFC.FunWriPLC_Word("D" + LFC.iAddrTRU1_ClearSts, sData);

            string sData1 = "0,0,0,0,0,0,0,0,0,0,0";
            bool bRet1 = LFC.FunWriPLC_Word("D" + LFC.iAddrTRU1_ClearCmd, sData1);
            TRU1step = 0;
        }

        private void Init_TRU2()
        {
            string sData = "0,0,0,0,0,0,0,515,1792";
            bool bRet = LFC.FunWriPLC_Word("D" + LFC.iAddrTRU2_Init, sData);
            sData = "0,0,0,0,0";  //T1~T4, IF Status
            bRet = LFC.FunWriPLC_Word("D" + LFC.iAddrTRU2_ClearSts, sData);

            string sData1 = "0,0,0,0,0,0,0,0,0,0,0";
            bool bRet1 = LFC.FunWriPLC_Word("D" + LFC.iAddrTRU2_ClearCmd, sData1);
            TRU1step = 0;
        }

        private void InitCONV()
        {
            for (int i = 0; i < 4; i++)
            {
                LFC.FunWriPLC_Bit("D" + LFC.iAddrPortSts1[i] + ".0", 1);
                if (i % 2 == 0)
                {
                    LFC.FunWriPLC_Bit("D" + LFC.iAddrPortSts1[i] + ".3", 1);
                    LFC.FunWriPLC_Bit("D" + LFC.iAddrPortSts1[i] + ".4", 0);

                }
                else
                {
                    LFC.FunWriPLC_Bit("D" + LFC.iAddrPortSts1[i] + ".4", 1);
                    LFC.FunWriPLC_Bit("D" + LFC.iAddrPortSts1[i] + ".3", 0);
                }
                LFC.FunWriPLC_Bit("D" + LFC.iAddrPortSts1[i] + ".5", 1);
                LFC.FunWriPLC_Bit("D" + LFC.iAddrPortSts1[i] + ".B", 1);
                LFC.FunWriPLC_Bit("D" + LFC.iAddrPortSts1[i] + ".C", 1);
                LFC.FunWriPLC_Bit("D" + LFC.iAddrPortSts1[i] + ".D", 0);
            }
            for (int i = 0; i < 4; i++)
            {
                LFC.FunWriPLC_Bit("D" + LFC.iAddrPortSts2[i] + ".0", 0);
            }
            for (int i = 0; i < 4; i++)
            {
                LFC.FunWriPLC_Word("D" + LFC.iAddrPortClearSts[i], "0,0,0,0,0,0,0");
            }
            for (int i = 0; i < 4; i++)
            {
                LFC.FunWriPLC_Word("D" + LFC.iAddrPortCmd[i], "0,0");
            }
        }

        public static void ChkAppIsAlreadyRunning()
        {
            string aFormName = System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName;
            string aProcName = System.IO.Path.GetFileNameWithoutExtension(aFormName);
            if (System.Diagnostics.Process.GetProcessesByName(aProcName).Length > 1)
            {
                MessageBox.Show("程式己開啓", "模擬程式", MessageBoxButtons.OK);
                Application.Exit();
                System.Environment.Exit(0);
            }
        }

        #endregion

        #region On_Click_Event

        private void btnStart_Click(object sender, EventArgs e)
        {
            LFC.StationNumber = int.Parse(textBox1.Text);
            LFC.subStart();
        }

        private void btnInitTRU1_Click(object sender, EventArgs e)
        {
            Init_TRU1();
        }

        private void btnInitTRU2_Click(object sender, EventArgs e)
        {
            Init_TRU2();
        }

        private void btnInitAll_Click(object sender, EventArgs e)
        {
            Init_LFT();
            Init_TRU1();
            Init_TRU2();
            InitCONV();
        }

        private void btnInitLFT_Click(object sender, EventArgs e)
        {
            Init_LFT();
        }

        private void InitCONV_Click(object sender, EventArgs e)
        {
            InitCONV();
        }

        private void rdbtnAbnormal_Click(object sender, EventArgs e)
        {
            cboAbnormal.Visible = (rdbtnAbnormal.Checked == true) ? true : false;
            cboWhere.Visible = (rdbtnAbnormal.Checked == true) ? true : false;
            cboSourceDest.Visible = (rdbtnAbnormal.Checked == true) ? true : false;
        }

        private void rdbtnNormal_Click(object sender, EventArgs e)
        {
            cboAbnormal.Visible = (rdbtnAbnormal.Checked == true) ? true : false;
            cboWhere.Visible = (rdbtnAbnormal.Checked == true) ? true : false;
            cboSourceDest.Visible = (rdbtnAbnormal.Checked == true) ? true : false;
        }

        private void btnLFTReset_Click(object sender, EventArgs e)
        {
            LFC.FunWriPLC_Bit("D" + LFC.iAddrLFT_Reset + ".1", 1);
        }

        private void btnTRU1Reset_Click(object sender, EventArgs e)
        {
            LFC.FunWriPLC_Bit("D" + LFC.iAddrTRU1_Reset + ".1", 1);
        }

        private void btnTRU2Reset_Click(object sender, EventArgs e)
        {
            LFC.FunWriPLC_Bit("D" + LFC.iAddrTRU2_Reset + ".1", 1);
        }

        private void btnLFTAlarm_Click(object sender, EventArgs e)
        {
            LFTstep = 199;
        }

        private void btnTRU1Alarm_Click(object sender, EventArgs e)
        {
            TRU1step = 199;
        }

        private void btnTRU2Alarm_Click(object sender, EventArgs e)
        {
            TRU2step = 199;
        }

        private void btnLFTInterlock_Click(object sender, EventArgs e)
        {
            LFTstep = 99;
        }

        private void btnTRU1Interlock_Click(object sender, EventArgs e)
        {
            TRU1step = 99;
        }

        private void btnTRU2Interlock_Click(object sender, EventArgs e)
        {
            TRU2step = 99;
        }

        private void btnPortAlarm_Click(object sender, EventArgs e)
        {
            if (clsLifterMPLC.MPLC.LFC_C.C2LFC[0].ErrorIdx == clsLifterMPLC.MPLC.LFC_C.LFC2C[0].PCErrorIdx)
            {
                PortErrorIndex = clsLifterMPLC.MPLC.LFC_C.C2LFC[0].ErrorIdx;
                PortErrorIndex++;

                string temp = PortAlarmcode.ToString();

                string sData = temp;
                string sAddr = "D" + LFC.iAddrPortErrorCode; //Port  error code
                LFC.FunWriPLC_Word(sAddr, sData);

                sData = PortErrorIndex.ToString();
                sAddr = "D" + LFC.iAddrPortErrorIndex; //Port  error index
                LFC.FunWriPLC_Word(sAddr, sData);

                //sAddr = "D" + LFC.iAddrPortErrorBit + ".2";
                //LFC.FunWriPLC_Bit(sAddr, 1); //Port error on
            }
        }

        private void btnResetPort_Click(object sender, EventArgs e)
        {
            string sAddr = "D" + LFC.iAddrPortErrorBit + ".2";
            bool bRet = LFC.FunWriPLC_Bit(sAddr, 0); //Port error off

            sAddr = "D" + LFC.iAddrPortErrorCode;
            bRet = LFC.FunWriPLC_Word(sAddr, 0); // Clear error code

            PortErrorIndex++;
            string sData = PortErrorIndex.ToString();
            sAddr = "D" + LFC.iAddrPortErrorIndex; //Port  error index
            bRet = LFC.FunWriPLC_Word(sAddr, sData);
        }

        private void btnPresentOn_Click(object sender, EventArgs e)
        {
            if (txtCSTID.Text.Trim() != "" && cboPortSelect.SelectedIndex != -1 && cboStageSelect.SelectedIndex != -1)
            {
                int[] sData = new int[7];
                sData = LFC.funString2int(txtCSTID.Text, 7);
                LFC.PresentOnOff(Mirle.clsLC_Def.dicCONDef[cboPortSelect.SelectedIndex].PLCindex, Convert.ToInt32(cboStageSelect.Text), sData);
            }
        }

        private void btnLFTPresentOnOff_Click(object sender, EventArgs e)
        {
            if (txtCSTID.Text.Trim() != "")
            {
                int[] sData = new int[7];
                sData = LFC.funString2int(txtCSTID.Text, 7);
                LFC.EQPresentOnOff(5, sData);
            }
        }

        private void btnTRU1PresentOnOff_Click(object sender, EventArgs e)
        {
            if (txtCSTID.Text.Trim() != "")
            {
                int[] sData = new int[7];
                sData = LFC.funString2int(txtCSTID.Text, 7);
                LFC.EQPresentOnOff(0, sData);
            }
        }

        private void btnTRU2PresentOnOff_Click(object sender, EventArgs e)
        {
            if (txtCSTID.Text.Trim() != "")
            {
                int[] sData = new int[7];
                sData = LFC.funString2int(txtCSTID.Text, 7);
                LFC.EQPresentOnOff(1, sData);
            }
        }

        private void cboPortSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboStageSelect.Items.Clear();
            for (int i = 1; i <= Mirle.clsLC_Def.dicCONDef[cboPortSelect.SelectedIndex].Stage; i++)
            {
                cboStageSelect.Items.Add(i);
            }
        }

        #endregion

        #region Port/Conveyor 狀態變更

        private void btnBCRReadDown_Click(object sender, EventArgs e)
        {
            if (txtCSTID.Text.Trim() != "")
            {
                int[] sData = new int[7];
                sData = LFC.funString2int(txtCSTID.Text, 7);

                LFC.Wait_In(Mirle.clsLC_Def.dicCONDef[cboPortSelect.SelectedIndex].PLCindex, sData);
            }
        }

        private void btnRemoved_Click(object sender, EventArgs e)
        {
            LFC.Remove(Mirle.clsLC_Def.dicCONDef[cboPortSelect.SelectedIndex].PLCindex);
        }

        private void btnWaitOut_Click_1(object sender, EventArgs e)
        {
            int[] sData = new int[7];
            sData = LFC.funString2int(txtCSTID.Text, 7);

            LFC.Wait_Out(Mirle.clsLC_Def.dicCONDef[cboPortSelect.SelectedIndex].PLCindex, sData);
        }

        private void btnModeChange_Click_1(object sender, EventArgs e)
        {
            LFC.ModeChange(Mirle.clsLC_Def.dicCONDef[cboPortSelect.SelectedIndex].PLCindex);
        }

        #endregion

    }
}