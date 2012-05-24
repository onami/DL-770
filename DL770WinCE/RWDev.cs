using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace DL770
{
    public static class RWDev
    {
        [DllImport("DL770WinCE.dll")]
        public static extern int ModelPowerOn();

        [DllImport("DL770WinCE.dll")]
        public static extern int ModelPowerOff();

        [DllImport("DL770WinCE.dll")]
        public static extern int ModulePowerOn();

        [DllImport("DL770WinCE.dll")]
        public static extern int ModulePowerOff();

        [DllImport("DL770WinCE.dll")]
        public static extern int ConnectReader();

        [DllImport("DL770WinCE.dll")]
        public static extern int DisconnectReader();

        [DllImport("DL770WinCE.dll")]
        public static extern int Inventory_G2(ref byte address,                                         
                                            ref byte Len,
                                            byte[] EPCAndID,
                                            ref byte CardNum);

        [DllImport("DL770WinCE.dll")]
        public static extern int GetReaderInfo(ref byte address,			//读写器地址		
                                                byte[] versionInfo,			//软件版本
                                                ref byte model,				//读写器型号
                                                ref byte supProtocol,		//支持的协议
                                                ref byte dmaxfre,           //当前读写器使用的最高频率
                                                ref byte dminfre,           //当前读写器使用的最低频率
                                                ref byte power,             //读写器的输出功率
                                                ref byte inventoryScanTime);
        [DllImport("DL770WinCE.dll")]
        public static extern int ReadCard_G2(ref byte address,				//读写器地址		                                                                                            
                                                ref byte Len,		//支持的协议
                                                byte ENum,           //当前读写器使用的最高频率
                                                byte[] EPC,           //当前读写器使用的最低频率
                                                byte Mem,             //读写器的输出功率
                                                byte WordPtr,
                                                byte Num,
                                                byte[] Password,
                                                byte[] Data,
                                                ref byte Errorcode);
        [DllImport("DL770WinCE.dll")]
        public static extern int WriteCard_G2 (ref byte address,
									ref byte Len, 
									byte WNum,
									byte ENum, 
									byte[] EPC,
									byte Mem,
									byte WordPtr,
									byte[]Writedata,
									byte[] Password,
									ref byte Errorcode);

        [DllImport("DL770WinCE.dll")]
        public static extern int Writepower(ref byte address,
                                    ref byte power);

        [DllImport("DL770WinCE.dll")]
        public static extern int Writedfre(ref byte address,
                                           ref byte dmaxfre,
                                           ref byte dminfre);

        [DllImport("DL770WinCE.dll")]
        public static extern int Inventory_6B(ref byte Inventory_6B,
                                           byte[] ID_6B,
                                           ref byte errorcode);

        [DllImport("DL770WinCE.dll")]
        public static extern int ReadCard_6B(ref byte Address,
								             ref byte Len,
								             byte[] ID_6B ,
								             byte  StartAddress,
								             byte Num,
								             byte[] Data,
								  	         ref byte Errorcode);

        [DllImport("DL770WinCE.dll")]
        public static extern int WriteCard_6B(ref byte Address,
                                            ref byte Len,
                                            byte[] ID_6B,
                                            byte StartAddress,
                                            byte[] Writedata,
                                            byte Writedatalen,
                                            ref byte Errorcode);



    }
}
