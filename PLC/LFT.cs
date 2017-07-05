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
    public partial class LFT : UserControl
    {

        private Color Color_SignOn = Color.LightGreen;
        private Color Color_SignOff = Color.LightGray;

        public LFT()
        {
            InitializeComponent();
        }

        private void LFT_Load(object sender, EventArgs e)
        {
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
            ShowLFC2LF();
            ShowLF2LFC();
        }

       
        private void ShowLFC2LF()
        {
            txtD10.Text = clsLifterMPLC.MPLC.LFC_C.LFC2L.LFCmdMode.ToString();
            txtD11.Text = clsLifterMPLC.MPLC.LFC_C.LFC2L.Form.ToString();
            txtD12.Text = clsLifterMPLC.MPLC.LFC_C.LFC2L.To.ToString();
            txtD13.Text = clsLifterMPLC.MPLC.LFC_C.LFC2L.CSTID.ToString();
            txtD20.Text = clsLifterMPLC.MPLC.LFC_C.LFC2L.SeqNo.ToString();
            txtD21.Text = clsLifterMPLC.MPLC.LFC_C.LFC2L.Speed.ToString();
            txtD26.Text = clsLifterMPLC.MPLC.LFC_C.LFC2L.PCErrorIdx.ToString();
            txtD27.Text = clsLifterMPLC.MPLC.LFC_C.LFC2L.TransferTimeOut.ToString();

            ShowColor(ref txtD25_1, (clsLifterMPLC.MPLC.LFC_C.LFC2L.LFCCmd.AlarmReset == clsLifterMPLC.MPLC.enuSignal.ON));
            ShowColor(ref txtD25_2, (clsLifterMPLC.MPLC.LFC_C.LFC2L.LFCCmd.BuzzerStop == clsLifterMPLC.MPLC.enuSignal.ON));
            ShowColor(ref txtD25_5, (clsLifterMPLC.MPLC.LFC_C.LFC2L.LFCCmd.InterlockError == clsLifterMPLC.MPLC.enuSignal.ON));
        }

        private void ShowLF2LFC()
        {
            txtD110.Text = clsLifterMPLC.MPLC.LFC_C.L2LFC.SeqNo.ToString();
            txtD111.Text = clsLifterMPLC.MPLC.LFC_C.L2LFC.CurrentLV.CurrentLoc.ToString();
            txtD114.Text = clsLifterMPLC.MPLC.LFC_C.L2LFC.Speed.ToString();
            txtD118.Text = clsLifterMPLC.MPLC.LFC_C.L2LFC.CSTID.ToString();
            txtD128.Text = clsLifterMPLC.MPLC.LFC_C.L2LFC.ErrorCode.ToString();
            txtD129.Text = clsLifterMPLC.MPLC.LFC_C.L2LFC.ErrorIdx.ToString();
            txtD130.Text = clsLifterMPLC.MPLC.LFC_C.L2LFC.MoveCount.ToString();
            txtD132.Text = clsLifterMPLC.MPLC.LFC_C.L2LFC.TransferCount.ToString();


            ShowColor(ref txtD125_0, clsLifterMPLC.MPLC.LFC_C.L2LFC.Sts_1.Online == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD125_1, clsLifterMPLC.MPLC.LFC_C.L2LFC.Sts_1.Ready == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD125_2, clsLifterMPLC.MPLC.LFC_C.L2LFC.Sts_1.Busy == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD125_3, clsLifterMPLC.MPLC.LFC_C.L2LFC.Sts_1.CmdError == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD125_4, clsLifterMPLC.MPLC.LFC_C.L2LFC.Sts_1.Error == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD125_5, clsLifterMPLC.MPLC.LFC_C.L2LFC.Sts_1.Waring == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD125_9, clsLifterMPLC.MPLC.LFC_C.L2LFC.Sts_1.CmdNormalComplete == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD125_A, clsLifterMPLC.MPLC.LFC_C.L2LFC.Sts_1.CmdAbnormalComplete == clsLifterMPLC.MPLC.enuSignal.ON);

            ShowColor(ref txtD126_3, clsLifterMPLC.MPLC.LFC_C.L2LFC.Sts_2.InterlockError == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD126_5, clsLifterMPLC.MPLC.LFC_C.L2LFC.Sts_2.EMS == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD126_9, clsLifterMPLC.MPLC.LFC_C.L2LFC.Sts_2.Upward == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD126_A, clsLifterMPLC.MPLC.LFC_C.L2LFC.Sts_2.Downward == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD126_B, clsLifterMPLC.MPLC.LFC_C.L2LFC.Sts_2.DoorOpen == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD126_C, clsLifterMPLC.MPLC.LFC_C.L2LFC.Sts_2.DoorClose == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD126_F, clsLifterMPLC.MPLC.LFC_C.L2LFC.Sts_2.CSTIn == clsLifterMPLC.MPLC.enuSignal.ON);

            ShowColor(ref txtD127_0, clsLifterMPLC.MPLC.LFC_C.L2LFC.Sts_3.BatteryLow == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD127_1, clsLifterMPLC.MPLC.LFC_C.L2LFC.Sts_3.AirDecrease == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD127_2, clsLifterMPLC.MPLC.LFC_C.L2LFC.Sts_3.HEPAError == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD127_3, clsLifterMPLC.MPLC.LFC_C.L2LFC.Sts_3.ExhaustError == clsLifterMPLC.MPLC.enuSignal.ON);

            ShowColor(ref txtD145_0, clsLifterMPLC.MPLC.LFC_C.L2LFC.PortSts[0].LDRQ == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD145_1, clsLifterMPLC.MPLC.LFC_C.L2LFC.PortSts[0].UDRQ == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD145_2, clsLifterMPLC.MPLC.LFC_C.L2LFC.PortSts[0].Ready == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD145_4, clsLifterMPLC.MPLC.LFC_C.L2LFC.PortSts[0].DoorOpen == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD145_5, clsLifterMPLC.MPLC.LFC_C.L2LFC.PortSts[0].DoorClose == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD145_6, clsLifterMPLC.MPLC.LFC_C.L2LFC.PortSts[0].NoInterupt == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD145_7, clsLifterMPLC.MPLC.LFC_C.L2LFC.PortSts[0].TRUError == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD145_8, clsLifterMPLC.MPLC.LFC_C.L2LFC.PortSts[0].Valid == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD145_9, clsLifterMPLC.MPLC.LFC_C.L2LFC.PortSts[0].TRRQ == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD145_A, clsLifterMPLC.MPLC.LFC_C.L2LFC.PortSts[0].Busy == clsLifterMPLC.MPLC.enuSignal.ON);
            ShowColor(ref txtD145_B, clsLifterMPLC.MPLC.LFC_C.L2LFC.PortSts[0].Complete == clsLifterMPLC.MPLC.enuSignal.ON);
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
