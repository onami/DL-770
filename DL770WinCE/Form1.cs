using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DL770WinCE;
using System.Collections;
using System.Resources;
using System.Reflection;
using System.IO.Ports;
using System.IO;
using System.Runtime.InteropServices;

namespace DL770WinCE
{
    public partial class Form1 : Form
    {
        public const int MB_ICONEXCLAMATION = 48;

        private DataCollector collector = new DataCollector();

        [DllImport("coredll.dll")]
        public static extern bool MessageBeep(int uType);

        private byte readerAddr = 0xff;
        private bool fAppClose = false;
        private int fCmdRet = 0x30;
        private const byte OK = 0x00;
        private const byte lengthError = 0x01;
        private const byte operationNotSupport = 0x0b;
        private const byte dataRangError = 0x03;
        private const byte cmdNotOperation = 0x04;
        private const byte EEPROM = 0x06;
        private const byte timeOut = 0x02;
        private const byte unknownError = 0x0f;
        private const byte communicationErr = 0x30;
        private const byte retCRCErr = 0x31;
        private const byte comPortOpened = 0x35;
        private const byte comPortClose = 0x36;
        private bool isStopInventory = false;
        private int CardNum1 = 0;
        ArrayList list = new ArrayList();
        private string fInventory_EPC_List; //存贮询查列表（如果读取的数据没有变化，则不进行刷新）
        private byte[] EPCAndID = new byte[160];
        public Form1()
        {
            InitializeComponent();
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
            radioButton4.Checked = true;
        }
        private string RR9086GetErrorCodeDesc(byte errorCode)
        {
            switch (errorCode)
            {
                case unknownError:
                    return "Unknown error";
                case cmdNotOperation:
                    return "CMD can't execute at current";
                case 0x33:
                    return "Utralight tag anticollision failed";
                case 0x32:
                    return "MF1 and Utralight tag collision";
                case 0x31:
                    return "Unallow more than one tag in RF field";
                case 0x30:
                    return "Anticollision failed";
                default:
                    return "";
            }
        }

        private string RR9086GetReturnCodeDesc(int retCode)
        {
            switch (retCode)
            {
                case OK: return "success";
                case lengthError: return "Command operand length error";
                case operationNotSupport: return "Command not supported";
                case EEPROM: return "E2PROM operation error";
                case timeOut: return "InventoryScanTime overflow,no UID collected";
                case communicationErr: return "Communication Error";
                case retCRCErr: return "CRC checksummat Error";
                case cmdNotOperation: return "CMD can't execute at current";
                case dataRangError: return "Operation data range error";
                default:
                    return "";
            }
        }

        private void ShowResultDialog(string cmdStr, int cmdRet, byte errorCode)
        {
            string s = "";
            try
            {
                if (!fAppClose)
                {
                    if (cmdRet == 0x10)
                    {
                        if (errorCode == 0)
                            s = cmdStr + ": \r\n" + RR9086GetReturnCodeDesc(cmdRet);
                        else
                            s = cmdStr + ": \r\n" + RR9086GetErrorCodeDesc(errorCode);
                    }
                    else
                    {
                        s = cmdStr + ": " + RR9086GetReturnCodeDesc(cmdRet) + "\r\n" + RR9086GetErrorCodeDesc(errorCode);
                    }
                    MessageBox.Show(s, "Information");
                }
            }
            finally
            {
                ;
            }
        }

        private void ShowResultDialog(string cmdStr, int cmdRet)
        {
            string tp;
            try
            {
                tp = RR9086GetReturnCodeDesc(cmdRet);
                if (tp != "")
                    MessageBox.Show(cmdStr + ": " + tp, "Information");
                else
                    MessageBox.Show(cmdStr + ": \r\n return " + "0x" + cmdRet.ToString("X2"), "Information"); ;
            }
            finally
            {
                ;
            }
        }

        private byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        private string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            return sb.ToString().ToUpper();
        }
        public void ChangeSubItem(ListViewItem ListItem, int subItemIndex, string ItemText)
        {
            if (subItemIndex == 1)
            {
                if (ListItem.SubItems[subItemIndex].Text != ItemText)
                {
                    ListItem.SubItems[subItemIndex].Text = ItemText;
                    ListItem.SubItems[2].Text = "1";
                }
                else
                {
                    ListItem.SubItems[2].Text = Convert.ToString(Convert.ToUInt32(ListItem.SubItems[2].Text) + 1);
                    if ((Convert.ToUInt32(ListItem.SubItems[2].Text) > 9999))
                        ListItem.SubItems[2].Text = "1";
                }

            }

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isStopInventory)
                return;
            isStopInventory = true;
            byte Len = 0;
            int i, EPClen, m, n;
            n = 0;
            byte CardNum = 0;
            string  temp = "", temps = "" ,s="";
            byte[] PUPI = new byte[4];
            byte[] AppData = new byte[4];
            byte[] ProtocolInfo = new byte[3];
            bool isonlistview;
            ListViewItem aListItem = new ListViewItem();
            if (tabControl1.SelectedIndex == 1)
            {
                fCmdRet = RWDev.Inventory_G2(ref readerAddr, ref Len, EPCAndID, ref CardNum);

                if (fCmdRet == 0)
                {
                    byte[] daw = new byte[Len-6];
                    Array.Copy(EPCAndID, daw, Len-6);
                    temps = ByteArrayToHexString(daw);
                    fInventory_EPC_List = temps;            //存贮记录
                    m = 0;
                    if (CardNum > 0)
                    {
                        for (i = 0; i < CardNum; i++)
                        {

                            EPClen = daw[m];
                            temp = temps.Substring(m * 2 + 2, EPClen * 2);
                            list.Add(temp);
                            m = m + EPClen + 1;
                            isonlistview = false;
                            for (n = 0;n< ListView1_EPC.Items.Count; n++)     //判断是否在Listview列表内
                            {
                              if (temp == ListView1_EPC.Items[n].SubItems[1].Text)
                                {
                                    aListItem = ListView1_EPC.Items[n];
                                    ChangeSubItem(aListItem, 1, temp);
                                    collector.write(temp);
                                    isonlistview = true;
                                }
                            }
                            if (!isonlistview)
                            {
                                aListItem = new ListViewItem();
                                aListItem.SubItems.Add("");
                                aListItem.SubItems.Add("");
                                aListItem.SubItems.Add("");
                                ListView1_EPC.Items.Add(aListItem);
                                s = temp;
                                aListItem.SubItems[0].Text = (ListView1_EPC.Items.Count).ToString();
                                ChangeSubItem(aListItem, 1, s);
                                if (comboBox1.Items.IndexOf(temp) == -1)
                                {
                                    comboBox1.Items.Add(temp);
                                }
                            }
                           // MessageBeep(10);
                        }                        
                    }
               }
               if ((comboBox1.Items.Count != 0))
               {
                   comboBox1.SelectedIndex = 0;
               }
               
            }
            isStopInventory = false;
        }

      

        private void Form1_Load(object sender, EventArgs e)
        {
            int i = 0;
            RWDev.ModulePowerOff();
            RWDev.ModulePowerOn();
            button1.Enabled = false;
            button2.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            radioButton6.Checked = true;
            radioButton5.Enabled = false;
            ComboBox_PowerDbm.SelectedIndex = 0;
            for (i = 0; i < 63; i++)
            {
                ComboBox_dminfre.Items.Add(Convert.ToString(902.6 + i * 0.4) + " MHz");
                ComboBox_dmaxfre.Items.Add(Convert.ToString(902.6 + i * 0.4) + " MHz");
            }
            ComboBox_dminfre.SelectedIndex = 0;
            ComboBox_dmaxfre.SelectedIndex = 62;
            i = 40;
            while (i <= 300)
            {
                comboBox2.Items.Add(Convert.ToString(i) + "ms");
                i = i + 10;
            }
            comboBox2.SelectedIndex = 1;
            
        }

        private void Form1_Closing(object sender, CancelEventArgs e)
        {
            RWDev.ModulePowerOff();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
           // readerAddr = 0xFF;
            byte[] verion = new byte[2];	    //软件版本
            byte model = 0;				        //读写器型号
            byte supProtocol = 0;	//支持的协议
            byte dmaxfre = 0;   //当前读写器使用的最高频率
            byte dminfre = 0;  //当前读写器使用的最低频率
            byte power = 0;  //读写器的输出功率
            byte inventoryScanTime = 0; //询查时间
            int result = 0x30;

            result = RWDev.ConnectReader();
            if (result == OK)
            {
                RWDev.GetReaderInfo(ref readerAddr, verion, ref model, ref supProtocol, ref dmaxfre, ref dminfre, ref power, ref inventoryScanTime);
               tbVersion.Text = verion[0].ToString().PadLeft(2, '0') + "." + verion[1].ToString().PadLeft(2, '0');
               if (power < 20)
                   power = 20;
                ComboBox_PowerDbm.SelectedIndex = power - 20;
                ComboBox_dminfre.SelectedIndex = dminfre;
                ComboBox_dmaxfre.SelectedIndex = dmaxfre;
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
                button8.Enabled = true;
            }
            else
            {
                MessageBox.Show("ConnectReader failed.", "Communication");
                btnConnect.Enabled = true;
                btnDisconnect.Enabled = false;
            }
        }


        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (RWDev.DisconnectReader() == OK)
            {
                MessageBox.Show("DisconnectReader success.", "Communication");
                btnConnect.Enabled = true;
                btnDisconnect.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button8.Enabled = false;
                tbVersion.Text = "";
            }
            else
            {
                MessageBox.Show("DisconnectReader failed.", "Communication");
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer2.Enabled = !timer2.Enabled;
            if (!timer2.Enabled)
            {
                button1.Text = "Read";
            }
            else
            {
                listBox1.Items.Clear();
                button1.Text = "Stop";
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            int result = 0x30;
            byte Len = 0;
            byte ENum = 0;
            byte Mem = 0;
            byte Num = 0;
            byte WordPtr = 0;
            byte Errorcode = 0;
            byte[] Password = new byte[4];
            byte[] Data = new byte[440];
            string str;
            if (comboBox1.Items.Count == 0)
                return;
            str = comboBox1.SelectedItem.ToString();
            if (str == "")
                return;
            ENum = Convert.ToByte(str.Length / 4);
            byte[] EPC = new byte[ENum];
            EPC = HexStringToByteArray(str);
            if (radioButton1.Checked)
                Mem = 0;
            if (radioButton2.Checked)
                Mem = 1;
            if (radioButton3.Checked)
                Mem = 2;
            if (radioButton4.Checked)
                Mem = 3;
            if (textBox1.Text == "")
                return;
            if (textBox2.Text == "")
                return;
            if (textBox3.Text == "" | textBox3.Text.Length!=8)
                return;
            WordPtr = Convert.ToByte(textBox1.Text, 16);
            Num = Convert.ToByte(textBox2.Text);
            Password = HexStringToByteArray(textBox3.Text);
            Len = Convert.ToByte(12 + ENum * 2);
         //   listBox1.Items.Clear();
            result = RWDev.ReadCard_G2(ref readerAddr, ref Len, ENum, EPC, Mem, WordPtr, Num, Password, Data, ref Errorcode);
            if (Len == 5)
            {
                return;
            }
            if (result == 0)
            {
                byte[] daw = new byte[Len - 5];
                Array.Copy(Data, daw, Len - 5);
                if (listBox1.Items.IndexOf(ByteArrayToHexString(daw)) == -1)
                {
                    listBox1.Items.Add(ByteArrayToHexString(daw));
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int result = 0x30;
            byte Len = 0;
            byte WNum = 0;
            byte ENum = 0;
            byte Mem = 0;
            byte Num = 0;
            byte WordPtr = 0;
            byte Errorcode = 0;
            byte[] Password = new byte[4];
            byte[] Data = new byte[440];
            string str,str1;
            if (comboBox1.Items.Count == 0)
                return;
            str = comboBox1.SelectedItem.ToString();
            if (str == "")
                return;
            ENum = Convert.ToByte(str.Length / 4);
            byte[] EPC = new byte[ENum*2];
            EPC = HexStringToByteArray(str);
            if (radioButton1.Checked)
                Mem = 0;
            if (radioButton2.Checked)
                Mem = 1;
            if (radioButton3.Checked)
                Mem = 2;
            if (radioButton4.Checked)
                Mem = 3;
            if (textBox1.Text == "")
                return;
            if (textBox2.Text == "")
                return;
            if (textBox3.Text == "" | textBox3.Text.Length != 8)
                return;
            if (textBox4.Text == "" | textBox4.Text.Length %4 !=0)
                return;
            str1 = textBox4.Text;
            WNum = Convert.ToByte(str1.Length / 4);
            byte[] writedata = new byte[WNum * 2];
            writedata = HexStringToByteArray(str1);
            WordPtr = Convert.ToByte(textBox1.Text, 16);
            Num = Convert.ToByte(textBox2.Text);
            Password = HexStringToByteArray(textBox3.Text);
            //   listBox1.Items.Clear();
            Len = Convert.ToByte(12 + (ENum+WNum) * 2);
            result = RWDev.WriteCard_G2(ref readerAddr, ref Len, WNum, ENum, EPC, Mem, WordPtr, writedata, Password,ref Errorcode);
            if (result == 0)
            {
                MessageBox.Show("Write success.", "Information");
            }
            else 
            {
                MessageBox.Show("Write Failed.", "Information");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            byte powerDbm, dminfre, dmaxfre;
            dminfre = Convert.ToByte(ComboBox_dminfre.SelectedIndex);
            dmaxfre = Convert.ToByte(ComboBox_dmaxfre.SelectedIndex);
            if (dminfre > dmaxfre)
            {
                MessageBox.Show("dminfre > dmaxfre!", "Information");  
            }
            powerDbm = Convert.ToByte(ComboBox_PowerDbm.SelectedIndex + 20);
            fCmdRet = RWDev.Writepower(ref readerAddr,ref powerDbm);
            fCmdRet = RWDev.Writedfre(ref readerAddr, ref dmaxfre, ref dminfre);
            if (fCmdRet==0)
                MessageBox.Show("Update Success!", "Information");
            return;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            if (!timer1.Enabled)
            {
                button5.Text = "Scan";
            }
            else
            {
                ListView1_EPC.Items.Clear();
                list.Clear();
                comboBox1.Items.Clear();
                button5.Text = "Stop";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            timer3.Enabled = !timer3.Enabled;
            if (!timer3.Enabled)
            {
                button6.Text = "List Tag ID";
            }
            else
            {
                ListView_ID_6B.Items.Clear();
                comboBox3.Items.Clear();
                button6.Text = "Stop";
                CardNum1 = 0;
            }


        }
        private void Inventory6b()
        {
            byte[] ID_6B = new byte[2000];
            byte[] ID2_6B = new byte[5000];
            byte errorcode = 30;
            bool isonlistview;
            string temps;
            string s;
            ListViewItem aListItem = new ListViewItem();
            int i;
            byte[] ConditionContent = new byte[300];
            fCmdRet = RWDev.Inventory_6B(ref readerAddr , ID_6B,ref errorcode);
            if (fCmdRet == 0)
            {
                byte[] daw = new byte[8];
                Array.Copy(ID_6B, daw, 8);
                temps = ByteArrayToHexString(daw);
                // comboBox3.Items.Add(temps);
                if (!list.Contains(temps))
                {
                    CardNum1 = CardNum1 + 1;
                    list.Add(temps);
                }
                isonlistview = false;
                for (i = 0; i < ListView_ID_6B.Items.Count; i++)     //判断是否在Listview列表内
                {
                    if (temps == ListView_ID_6B.Items[i].SubItems[1].Text)
                    {
                        aListItem = ListView_ID_6B.Items[i];
                        ChangeSubItem(aListItem, 1, temps);
                        isonlistview = true;
                    }
                }
                if (!isonlistview)
                {
                    // CardNum1 = Convert.ToByte(ListView_ID_6B.Items.Count+1);
                    aListItem = new ListViewItem();
                    aListItem.SubItems.Add("");
                    aListItem.SubItems.Add("");
                    aListItem.SubItems.Add("");
                    ListView_ID_6B.Items.Add(aListItem);
                    s = temps;
                    aListItem.SubItems[0].Text = (ListView_ID_6B.Items.Count).ToString();
                    ChangeSubItem(aListItem, 1, s);
                    if (comboBox3.Items.IndexOf(s) == -1)
                    {
                        comboBox3.Items.Add(temps);
                    }
                }
                MessageBeep(10);

                if (comboBox3.Items.Count != 0)
                    comboBox3.SelectedIndex = 0;
            }
            
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            if (isStopInventory)
                return;
            isStopInventory = true;
            Inventory6b();
            isStopInventory = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            timer4.Enabled = !timer4.Enabled;
            if (!timer4.Enabled)
            {
                button7.Text = "Read";
            }
            else
            {
                listBox2.Items.Clear();
                button7.Text = "Stop";
            }
        }
        private void Read6B()
        {
            string temp, temps;
            byte[] CardData = new byte[320];
            byte[] ID_6B = new byte[8];
            byte Num, StartAddress;
            byte Len = 0, ferrorcode=30;
            if (comboBox3.Items.Count == 0)
                return;
            if (comboBox3.SelectedItem == null)
                return;
            temp = comboBox3.SelectedItem.ToString();
            if (temp == "")
                return;
            ID_6B = HexStringToByteArray(temp);
            if (textBox5.Text == "")
                return;
            StartAddress = Convert.ToByte(textBox5.Text);
            if (textBox6.Text == "")
                return;
            Num = Convert.ToByte(textBox6.Text);
            fCmdRet = RWDev.ReadCard_6B(ref readerAddr,ref Len, ID_6B, StartAddress, Num, CardData, ref ferrorcode);
            if (fCmdRet == 0)
            {
                byte[] data = new byte[Num];
                Array.Copy(CardData, data, Num);
                temps = ByteArrayToHexString(data);
                listBox2.Items.Add(temps);
            }
            if (fAppClose)
                Close();
        }
        private void timer4_Tick(object sender, EventArgs e)
        {
            if (isStopInventory)
                return;
            isStopInventory = true;
            Read6B();
            isStopInventory = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string temp;
            byte[] CardData = new byte[320];
            byte[] ID_6B = new byte[8];
            byte StartAddress;
            byte Writedatalen;

            byte errorcode = 30,Len=0;
            if ((textBox7.Text == "") | ((textBox7.Text.Length % 2) != 0))
            {
                MessageBox.Show("Please input in bytes in hexadecimal form!", "Information");
                return;
            }
            if ((textBox5.Text == "") | (textBox6.Text == ""))
            {
                MessageBox.Show("Start address or length is empty!Please input!", "Information");
                return;
            }
            if (comboBox3.Items.Count == 0)
                return;
            if (comboBox3.SelectedItem == null)
                return;
            temp = comboBox3.SelectedItem.ToString();
            if (temp == "")
                return;
            ID_6B = HexStringToByteArray(temp);
            if (textBox5.Text == "")
                return;
            StartAddress = Convert.ToByte(textBox5.Text);
            Writedatalen = Convert.ToByte(textBox7.Text.Length / 2);
            byte[] Writedata = new byte[Writedatalen];
            Writedata = HexStringToByteArray(textBox7.Text);
            Len =Convert.ToByte(14 + Writedatalen);
            fCmdRet = RWDev.WriteCard_6B(ref readerAddr,ref Len, ID_6B, StartAddress, Writedata, Writedatalen,ref errorcode);
            if(fCmdRet==0)
                MessageBox.Show("Write Success!!", "Information");
            else
                MessageBox.Show("Write Failed!", "Information");

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex < 6)
                timer3.Interval = 100;
            else
                timer3.Interval = (comboBox2.SelectedIndex + 4) * 10;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer2.Enabled = false;
            timer3.Enabled = false;
            timer4.Enabled = false;
            button5.Text="Scan";
            button1.Text = "Read";
            button6.Text = "List Tag ID";
            button7.Text = "Read";
        }


      
    }
}