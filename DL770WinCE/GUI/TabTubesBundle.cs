using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DL770.Rfid;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

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
                var tag = reader.GetFirstTag();
                var data = new byte[TubesBundle.GetSize()];
                var result = reader.ReadBytes(tag, ref data, RfidReader.MemorySection.User);

                if (data != null)
                {
                    var session = new TubesBundleSession()
                    {
                        time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        sessionMode = TubesBundleSession.Mode.Reading,
                        tag = ByteArrayToHexString(tag),
                    };

                    var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
                    session.bundle = (TubesBundle)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(TubesBundle));
                    handle.Free();

                    TagEpcLabel.Text = ByteArrayToHexString(tag);
                    dateTimePicker.Value = new DateTime(1970, 1, 1).AddSeconds(session.bundle.time).ToLocalTime();
                    wellNumberInput.Text = session.bundle.districtId.ToString();
                    tubesLengthInput.Text = session.bundle.bundleLength.ToString();

                    collector.WriteSession(session);
                    new Thread(new ThreadStart(this.SendBundleSessions)).Start();
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
                var tag = reader.GetFirstTag();
                var data = new byte[TubesBundle.GetSize()];

                var session = new TubesBundleSession()
                {
                    sessionMode = TubesBundleSession.Mode.Writing,
                    tag = ByteArrayToHexString(tag),
                    time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    bundle = new TubesBundle()
                    {
                        time = (int)(dateTimePicker.Value - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds,
                        districtId = Convert.ToUInt16(wellNumberInput.Text),
                        bundleLength = Convert.ToUInt16(tubesLengthInput.Text),                    
                    }
                };

                IntPtr pnt = Marshal.AllocHGlobal(Marshal.SizeOf(data));
                var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
                Marshal.StructureToPtr(session.bundle, handle.AddrOfPinnedObject(), false);
                handle.Free();

                if (reader.WriteBytes(tag, data, RfidReader.MemorySection.User) != (int)RfidReader.ResultCode.Ok)
                {
                    MessageBox.Show("Не удалось записать данные на метку", "Ошибка");
                }
                else
                {
                    collector.WriteSession(session);
                    new Thread(new ThreadStart(this.SendBundleSessions)).Start();
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
