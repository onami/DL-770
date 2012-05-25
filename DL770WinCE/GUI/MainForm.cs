using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DL770.Rfid;

namespace DL770
{
    public partial class MainForm : Form
    {
        private RfidReader reader;
        private RfidTagsCollector collector;
        private RfidSession session;
        private RfidWebClient webclient;

        [DllImport("coredll.dll")]
        public static extern bool MessageBeep(int uType);

        private byte readerAddr = 0xff;

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
        }

        /// <summary>
        /// Блокирует и разблокирует кнопки управления при включении/выключении модуля считывания.
        /// </summary>
        private void ReverseButtonsStatus()
        {
            connectButton.Enabled           = !connectButton.Enabled;
            disconnectButton.Enabled        = !disconnectButton.Enabled;

            tubesPackWriteButton.Enabled    = !tubesPackWriteButton.Enabled;
            tubesPackReadButton.Enabled     = !tubesPackReadButton.Enabled;
            updateButton.Enabled            = !updateButton.Enabled;
            scanEpcTagsButton.Enabled       = !scanEpcTagsButton.Enabled;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var i = 0;

            powerDbmComboBox.SelectedIndex = 0;

            for (i = 0; i < 63; i++)
            {
                minFrequencyComboBox.Items.Add(Convert.ToString(902.6 + i * 0.4) + " MHz");
                maxFrequencyComboBox.Items.Add(Convert.ToString(902.6 + i * 0.4) + " MHz");
            }

            minFrequencyComboBox.SelectedIndex = 0;
            maxFrequencyComboBox.SelectedIndex = 62;
            i = 40;

            ConnectButton_Click(this, null);
        }

        private void MainForm_Closing(object sender, CancelEventArgs e)
        {
            collector.Close();
            RWDev.ModulePowerOff();
        } 

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            scanGen2Timer.Enabled   = false;
            scanEpcTagsButton.Text  = "Сканировать";
        }
    }
}