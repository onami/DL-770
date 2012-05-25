using System;
using System.Windows.Forms;

namespace DL770
{
    public partial class MainForm : Form
    {
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            byte[] version = new byte[2];	    //软件版本
            byte model = 0;				        //读写器型号
            byte supProtocol = 0;	//支持的协议
            byte maxFreq = 0;   //当前读写器使用的最高频率
            byte minFreq = 0;  //当前读写器使用的最低频率
            byte power = 0;  //读写器的输出功率
            byte inventoryScanTime = 0; //询查时间

            reader = new RfidReader();

            if (reader.connectionStatus == RfidReader.ResultCode.Ok)
            {
                RWDev.GetReaderInfo(
                    ref readerAddr,
                    version,
                    ref model,
                    ref supProtocol,
                    ref maxFreq,
                    ref minFreq,
                    ref power,
                    ref inventoryScanTime
                    );

                if (power < 20) power = 20;

                powerDbmComboBox.SelectedIndex = power - 20;

                minFrequencyComboBox.SelectedIndex = minFreq;
                maxFrequencyComboBox.SelectedIndex = maxFreq;

                ReverseButtonsStatus();
            }

            else
            {
                MessageBox.Show("Не удалось включить модуль считывания", "Ошибка инициализации");
            }
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            reader.Off();

            if (reader.connectionStatus == RfidReader.ResultCode.Ok)
            {
                ReverseButtonsStatus();
            }
            else
            {
                MessageBox.Show("Не удалось отключить модуль считывания", "Ошибка инициализации");
            }
        }

        private void UpdateSettingsButton_Click(object sender, EventArgs e)
        {
            var minFreq = (byte)(minFrequencyComboBox.SelectedIndex);
            var maxFreq = (byte)(maxFrequencyComboBox.SelectedIndex);

            if (minFreq > maxFreq)
            {
                MessageBox.Show("Вы выставили минимальное значение больше максимального!", "Ошибка");
            }

            var result = reader.UpdatePower((byte)(powerDbmComboBox.SelectedIndex + 20)) | reader.UpdateFrequency(maxFreq, minFreq);

            if (result != RfidReader.ResultCode.Ok)
            {
                MessageBox.Show("Не удалось обновить настройки");
            }

            return;
        }
    }
}
