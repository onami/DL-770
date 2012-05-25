using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DL770.Rfid;

namespace DL770
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Читает данные о подвеске с ближайшей метки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TubesPackReadButton_Click(object sender, EventArgs e)
        {
            try
            {
                var data = new byte[TubesBundle.GetSize()];
                var result = reader.ReadBytes(reader.GetFirstTag(), ref data, RfidReader.MemorySection.User);

                if (data != null)
                {
                    var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
                    var pack = (TubesBundle)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(TubesBundle));

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

        /// <summary>
        /// Записывает данные о подвеске на ближайшую метку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TubesPackWriteButton_Click(object sender, EventArgs e)
        {
            try
            {
                var pack = new TubesBundle()
                {
                    time = (int)(dateTimePicker.Value - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds,
                    disrictId = Convert.ToUInt16(wellNumberInput.Text),
                    tubesLength = Convert.ToUInt16(tubesLengthInput.Text),
                    test1 = 0xA0AAAAAAFFFFFFFF,
                    test2 = 0xA1AAAAAAFFFFFFFF,
                    test3 = 0xA2AAAAAAFFFFFFFF,
                    test4 = 0xA3AAAAAAFFFFFFFF,
                    test5 = 0xA4AAAAAAFFFFFFFF,
                };

                var data = new byte[TubesBundle.GetSize()];

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

        /// <summary>
        /// Выставляет в dateTimePicker текущую дату
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SyncTimeButton_Click(object sender, EventArgs e)
        {
            dateTimePicker.Value = DateTime.Now;
        }
    }
}
