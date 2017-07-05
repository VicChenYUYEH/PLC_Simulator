using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLC
{
    public partial class TRU : UserControl
    {
        private Color Color_SignOn = Color.LightGreen;
        private Color Color_SignOff = Color.LightGray;

        private int iTRU;
        public int _iTRU
        {
            get { return iTRU; }
            set { iTRU = value; }
        }

        public TRU()
        {
            InitializeComponent();
        }

        private void TRU_Load(object sender, EventArgs e)
        {
            iTRU = 0;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;

                ShowData();
            }
            catch
            {

            }
            finally
            {
                timer1.Enabled = true;
            }
        }


        private void ShowData()
        {
            Showlbl();
            ShowTFC2TRU();
            ShowTRU2TFC();
        }

        private void Showlbl()
        {
            if (iTRU >= 2) {return;}
            D30.Text = "D" + (30 + 20 * iTRU).ToString();
            D31.Text = "D" + (31 + 20 * iTRU).ToString();
            D32.Text = "D" + (32 + 20 * iTRU).ToString();
            D33.Text = "D" + (33 + 20 * iTRU).ToString();
            D40.Text = "D" + (40 + 20 * iTRU).ToString();
            D41.Text = "D" + (41 + 20 * iTRU).ToString();
            D42.Text = "D" + (42 + 20 * iTRU).ToString();
            D43.Text = "D" + (43 + 20 * iTRU).ToString();
            D44.Text = "D" + (44 + 20 * iTRU).ToString();
            D45.Text = "D" + (45 + 20 * iTRU).ToString();
            D46.Text = "D" + (46 + 20 * iTRU).ToString();
            D47.Text = "D" + (47 + 20 * iTRU).ToString();

            D160.Text = "D" + (160 + 40 * iTRU).ToString();
            D161.Text = "D" + (161 + 40 * iTRU).ToString();
            D162.Text = "D" + (162 + 40 * iTRU).ToString();
            D164.Text = "D" + (164 + 40 * iTRU).ToString();
            D165.Text = "D" + (165 + 40 * iTRU).ToString();
            D166.Text = "D" + (166 + 40 * iTRU).ToString();
            D167.Text = "D" + (167 + 40 * iTRU).ToString();
            D168.Text = "D" + (168 + 40 * iTRU).ToString();
            D175.Text = "D" + (175 + 40 * iTRU).ToString();
            D176.Text = "D" + (176 + 40 * iTRU).ToString();
            D177.Text = "D" + (177 + 40 * iTRU).ToString();
            D178.Text = "D" + (178 + 40 * iTRU).ToString();
            D179.Text = "D" + (179 + 40 * iTRU).ToString();
            D181.Text = "D" + (181 + 40 * iTRU).ToString();
            D183.Text = "D" + (183 + 40 * iTRU).ToString();
            D184.Text = "D" + (184 + 40 * iTRU).ToString();
            D185.Text = "D" + (185 + 40 * iTRU).ToString();
            D186.Text = "D" + (186 + 40 * iTRU).ToString();
            D187.Text = "D" + (187 + 40 * iTRU).ToString();
        }

        private void ShowTFC2TRU()
        {
            txtD30.Text = clsLifterMPLC.MPLC.LFC_C.LFC2T[iTRU].LFCmdMode.ToString();
            txtD31.Text = clsLifterMPLC.MPLC.LFC_C.LFC2T[iTRU].Form.ToString();
            txtD32.Text = clsLifterMPLC.MPLC.LFC_C.LFC2T[iTRU].To.ToString();
            txtD33.Text = clsLifterMPLC.MPLC.LFC_C.LFC2T[iTRU].CSTID.ToString();
            txtD40.Text = clsLifterMPLC.MPLC.LFC_C.LFC2T[iTRU].SeqNo.ToString();
            txtD41.Text = clsLifterMPLC.MPLC.LFC_C.LFC2T[iTRU].LifterSpeed.ToString();
            txtD42.Text = clsLifterMPLC.MPLC.LFC_C.LFC2T[iTRU].TravelSpeed.ToString();
            txtD43.Text = clsLifterMPLC.MPLC.LFC_C.LFC2T[iTRU].RotateSpeed.ToString();
            txtD44.Text = clsLifterMPLC.MPLC.LFC_C.LFC2T[iTRU].ForkSpeed.ToString();
            txtD46.Text = clsLifterMPLC.MPLC.LFC_C.LFC2T[iTRU].PCErrorIdx.ToString();
            txtD47.Text = clsLifterMPLC.MPLC.LFC_C.LFC2T[iTRU].TransferTimeOut.ToString();

            ShowColor(ref txtD45_0, clsLifterMPLC.MPLC.LFC_C.LFC2T[iTRU].TRUCCmd.TRUMode == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD45_1, clsLifterMPLC.MPLC.LFC_C.LFC2T[iTRU].TRUCCmd.AlarmReset == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD45_2, clsLifterMPLC.MPLC.LFC_C.LFC2T[iTRU].TRUCCmd.BuzzerStop == clsLifterMPLC.MPLC.enuSignal.ON);
        }

        private void ShowTRU2TFC()
        {
            txtD160.Text = clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].SeqNo.ToString();
            txtD161.Text = clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].CurrentLV.CurrentLoc.ToString();
            txtD162.Text = clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].ForkPos.ToString();
            txtD164.Text = clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].LifterSpeed.ToString();
            txtD165.Text = clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].TravelSpeed.ToString();
            txtD166.Text = clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].RotateSpeed.ToString();
            txtD167.Text = clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].ForkSpeed.ToString();
            txtD168.Text = clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].CSTID.ToString();
            txtD177.Text = clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].ErrorCode.ToString();
            txtD178.Text = clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].ErrorIdx.ToString();
            txtD179.Text = clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].MoveCount.ToString();
            txtD181.Text = clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].TransferCount.ToString();
            txtD183.Text = clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].TimerCount[0].ToString();
            txtD184.Text = clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].TimerCount[1].ToString();
            txtD185.Text = clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].TimerCount[2].ToString();
            txtD186.Text = clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].TimerCount[3].ToString();

            ShowColor(ref txtD175_0, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].Sts_1.Online == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD175_1, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].Sts_1.Ready == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD175_2, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].Sts_1.Busy == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD175_3, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].Sts_1.CmdError == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD175_4, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].Sts_1.Error == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD175_5, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].Sts_1.Waring == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD175_6, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].Sts_1.BatteryLow == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD175_9, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].Sts_1.CmdNormalComplete == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD175_A, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].Sts_1.CmdAbnormalComplete == clsLifterMPLC.MPLC.enuSignal.ON);

            ShowColor(ref txtD176_3, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].Sts_2.InterlockError == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD176_6, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].Sts_2.PickpuCycle == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD176_7, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].Sts_2.DepositCycle == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD176_8, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].Sts_2.TravelHP == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD176_9, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].Sts_2.LFHP == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD176_A, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].Sts_2.ForkHP == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD176_B, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].Sts_2.RotateHP == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD176_C, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].Sts_2.TravelMoving == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD176_D, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].Sts_2.LFActing == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD176_E, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].Sts_2.Forking == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD176_F, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].Sts_2.Stock == clsLifterMPLC.MPLC.enuSignal.ON);

            ShowColor(ref txtD187_0, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].IFSts.LDRQ == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD187_1, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].IFSts.UDRQ == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD187_2, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].IFSts.Ready == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD187_6, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].IFSts.NoInterupt == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD187_7, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].IFSts.EMO == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD187_8, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].IFSts.Valid == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD187_9, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].IFSts.TRRQ == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD187_A, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].IFSts.Busy == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD187_B, clsLifterMPLC.MPLC.LFC_C.T2LFC[iTRU].IFSts.Complete == clsLifterMPLC.MPLC.enuSignal.ON);
        }

        private void ShowColor(ref Label objLbl, bool bFlag)
        {
            objLbl.Text = bFlag.ToString();
            if (bFlag == true)
            {
                objLbl.BackColor = Color_SignOn;
            }
            else
            {
                objLbl.BackColor = Color_SignOff;
            }

        }
    }
}
