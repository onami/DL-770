using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DL770.Rfid;

namespace DL770
{
    public partial class MainForm : Form
    {
        public const int MB_ICONEXCLAMATION = 48;

        private RfidReader reader;
        private RfidTagsCollector collector;
        private RfidSession session;
        private RfidWebClient webclient;

        [DllImport("coredll.dll")]
        public static extern bool MessageBeep(int uType);

        private byte readerAddr = 0xff;
        private bool fAppClose = false;

        private int fCmdRet = 0x30;

        private bool isStopInventory = false;

        ArrayList list = new ArrayList();
        private string fInventory_EPC_List; //存贮询查列表（如果读取的数据没有变化，则不进行刷新）
        private byte[] EPCAndID = new byte[160];

        public MainForm()
        {
            InitializeComponent();

            connectButton.Enabled = true;
            disconnectButton.Enabled = false;

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            collector = new RfidTagsCollector(path + @"\rfid.db");
            webclient = new RfidWebClient(Configuration.Deserialize(path + @"\config.xml"));

            reader = new RfidReader();
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
                            s = cmdStr + ": \r\n" + RfidReader.GetResultCodeDescription((RfidReader.ResultCode)cmdRet);
                        else
                            s = cmdStr + ": \r\n" + RfidReader.GetErrorCodeDescription((RfidReader.ErrorCode)errorCode);
                    }
                    else
                    {
                        s = cmdStr + ": " + RfidReader.GetResultCodeDescription((RfidReader.ResultCode)cmdRet) + "\r\n" + RfidReader.GetErrorCodeDescription((RfidReader.ErrorCode)errorCode);
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
                tp = RfidReader.GetResultCodeDescription((RfidReader.ResultCode)cmdRet);
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
            string temp = "", temps = "", s = "";
            byte[] PUPI = new byte[4];
            byte[] AppData = new byte[4];
            byte[] ProtocolInfo = new byte[3];
            bool isonlistview;
            ListViewItem aListItem = new ListViewItem();
            if (tabControl.SelectedIndex == 1)
            {
                fCmdRet = RWDev.Inventory_G2(ref readerAddr, ref Len, EPCAndID, ref CardNum);

                if (fCmdRet == 0)
                {
                    byte[] daw = new byte[Len - 6]; //Почему -6
                    Array.Copy(EPCAndID, daw, Len - 6);

                    temps = ByteArrayToHexString(daw);
                    fInventory_EPC_List = temps;            //存贮记录
                    m = 0;
                    if (CardNum > 0)
                    {
                        for (i = 0; i < CardNum; i++)
                        {

                            EPClen = daw[m];
                            temp = temps.Substring(m * 2 + 2, EPClen * 2);
                            // byte[] epcData = new byte[EPClen];

                            list.Add(temp);
                            m = m + EPClen + 1;
                            isonlistview = false;

                            for (n = 0; n < EpcTagsListView.Items.Count; n++)     //判断是否在Listview列表内
                            {
                                if (temp == EpcTagsListView.Items[n].SubItems[1].Text)
                                {
                                    aListItem = EpcTagsListView.Items[n];
                                    ChangeSubItem(aListItem, 1, temp);

                                    if (session.tags.Contains(temp) == false)
                                    {
                                        session.tags.Add(temp);
                                    }

                                    isonlistview = true;
                                }
                            }
                            if (!isonlistview)
                            {
                                aListItem = new ListViewItem();
                                aListItem.SubItems.Add("");
                                aListItem.SubItems.Add("");
                                aListItem.SubItems.Add("");
                                EpcTagsListView.Items.Add(aListItem);
                                s = temp;
                                aListItem.SubItems[0].Text = (EpcTagsListView.Items.Count).ToString();
                                ChangeSubItem(aListItem, 1, s);
                            }
                            MessageBeep(10);
                        }
                    }
                }
            }
            isStopInventory = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int i = 0;

            TubesPackWriteButton.Enabled = false;
            TubesPackReadButton.Enabled = false;
            updateButton.Enabled = false;
            ScanEpcTagsButton.Enabled = false;

            powerDbmComboBox.SelectedIndex = 0;
            for (i = 0; i < 63; i++)
            {
                minFrequencyComboBox.Items.Add(Convert.ToString(902.6 + i * 0.4) + " MHz");
                maxFrequencyComboBox.Items.Add(Convert.ToString(902.6 + i * 0.4) + " MHz");
            }
            minFrequencyComboBox.SelectedIndex = 0;
            maxFrequencyComboBox.SelectedIndex = 62;
            i = 40;

            btnConnect_Click(this, null);
        }

        private void Form1_Closing(object sender, CancelEventArgs e)
        {
            collector.Close();
            RWDev.ModulePowerOff();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            byte[] verion = new byte[2];	    //软件版本
            byte model = 0;				        //读写器型号
            byte supProtocol = 0;	//支持的协议
            byte dmaxfre = 0;   //当前读写器使用的最高频率
            byte dminfre = 0;  //当前读写器使用的最低频率
            byte power = 0;  //读写器的输出功率
            byte inventoryScanTime = 0; //询查时间

            if (reader.connectionStatus == RfidReader.ResultCode.Ok)
            {
                RWDev.GetReaderInfo(ref readerAddr, verion, ref model, ref supProtocol, ref dmaxfre, ref dminfre, ref power, ref inventoryScanTime);

                if (power < 20) power = 20;

                powerDbmComboBox.SelectedIndex = power - 20;

                minFrequencyComboBox.SelectedIndex = dminfre;
                maxFrequencyComboBox.SelectedIndex = dmaxfre;

                connectButton.Enabled = false;
                disconnectButton.Enabled = true;
                TubesPackWriteButton.Enabled = true;
                TubesPackReadButton.Enabled = true;
                updateButton.Enabled = true;
                ScanEpcTagsButton.Enabled = true;
            }

            else
            {
                MessageBox.Show("Не удалось включить модуль считывания", "Ошибка инициализации");
                connectButton.Enabled = true;
                disconnectButton.Enabled = false;
            }
        }

        private void syncTimeButton_Click(object sender, EventArgs e)
        {
            dateTimePicker.Value = DateTime.Now;
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (RWDev.DisconnectReader() == (int)RfidReader.ResultCode.Ok)
            {
                connectButton.Enabled = true;
                disconnectButton.Enabled = false;
                TubesPackWriteButton.Enabled = false;
                TubesPackReadButton.Enabled = false;
                updateButton.Enabled = false;
                ScanEpcTagsButton.Enabled = false;
            }
            else
            {
                MessageBox.Show("Не удалось отключить модуль считывания", "Ошибка инициализации");
                connectButton.Enabled = false;
                disconnectButton.Enabled = true;
            }
        }

        private void TubesPackReadButton_Click(object sender, EventArgs e)
        {
            try
            {
                int result = 0x30;
                byte Len = 0;
                byte ENum = 0;
                byte memorySection = 3;
                byte Num = 0;
                byte WordPtr = 0;
                byte Errorcode = 0;
                byte[] Password = new byte[4];
                byte[] Data = new byte[440];

                var EPC = reader.GetFirstTag();
                ENum = (byte)(EPC.Length / 2);

                WordPtr = 0;
                Num = 4;
                Len = Convert.ToByte(12 + ENum * 2);

                result = RWDev.ReadCard_G2(ref readerAddr, ref Len, ENum, EPC, memorySection, WordPtr, Num, Password, Data, ref Errorcode);
                if (Len == 5)
                {
                    return;
                }
                if (result == 0)
                {
                    byte[] daw = new byte[Len - 5];
                    Array.Copy(Data, daw, Len - 5);

                    var handle = GCHandle.Alloc(daw, GCHandleType.Pinned);
                    var pack = (TubesPack)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(TubesPack));

                    dateTimePicker.Value = new DateTime(1970, 1, 1).AddSeconds(pack.time);
                    wellNumberInput.Text = pack.disrictId.ToString();
                    tubesLengthInput.Text = pack.tubesLength.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TubesPackWriteButton_Click(object sender, EventArgs e)
        {
            try
            {
                var pack = new TubesPack()
                {
                    time = (int)(dateTimePicker.Value - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds,
                    disrictId = Convert.ToUInt16(wellNumberInput.Text),
                    tubesLength = Convert.ToUInt16(tubesLengthInput.Text)
                };

                var data = new byte[8];

                IntPtr pnt = Marshal.AllocHGlobal(Marshal.SizeOf(data));
                var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
                Marshal.StructureToPtr(pack, handle.AddrOfPinnedObject(), false);
                handle.Free();

                if (reader.WriteBytes(reader.GetFirstTag(), data, RfidReader.MemorySection.User) != (int)RfidReader.ResultCode.Ok)
                {
                    MessageBox.Show("Не удалось записать данные на метку", "Ошибка");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            byte powerDbm, dminfre, dmaxfre;
            dminfre = Convert.ToByte(minFrequencyComboBox.SelectedIndex);
            dmaxfre = Convert.ToByte(maxFrequencyComboBox.SelectedIndex);
            if (dminfre > dmaxfre)
            {
                MessageBox.Show("Минимальное значение больше максимального!", "Ошибка");
            }
            powerDbm = Convert.ToByte(powerDbmComboBox.SelectedIndex + 20);
            fCmdRet = RWDev.Writepower(ref readerAddr, ref powerDbm);
            fCmdRet = RWDev.Writedfre(ref readerAddr, ref dmaxfre, ref dminfre);
            
            if(fCmdRet != 0)
            {
                MessageBox.Show("Не удалось обновить настройки.\nКод ошибки: " + RfidReader.GetResultCodeDescription((RfidReader.ResultCode)fCmdRet), "Ошибка");
            }
            return;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            scanGen2Timer.Enabled = !scanGen2Timer.Enabled;

            if (!scanGen2Timer.Enabled)
            {
                ScanEpcTagsButton.Text = "Сканировать";
                collector.Write(session);
                var sessions = collector.GetUnshippedTags();
                //webclient.SendRfidReports(sessions);
                //collector.SetDeliveryStatus(sessions);
            }
            else
            {
                EpcTagsListView.Items.Clear();
                list.Clear();

                session = new RfidSession { sessionMode = RfidSession.SessionMode.Reading };
                session.time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                ScanEpcTagsButton.Text = "Остановить";
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            scanGen2Timer.Enabled = false;
            ScanEpcTagsButton.Text = "Сканировать";
        }
    }
}