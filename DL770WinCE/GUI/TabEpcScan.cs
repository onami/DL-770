using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DL770.Rfid;
using System.Threading;

namespace DL770
{
    public partial class MainForm : Form
    {
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

        private void Timer1_Tick(object sender, EventArgs e)
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

                            if (session.tags.Contains(temp) == false)
                            {
                                session.tags.Add(temp);
                            }

                            for (n = 0; n < epcTagsListView.Items.Count; n++)     //判断是否在Listview列表内
                            {
                                if (temp == epcTagsListView.Items[n].SubItems[1].Text)
                                {
                                    aListItem = epcTagsListView.Items[n];
                                    ChangeSubItem(aListItem, 1, temp);
                                    isonlistview = true;
                                }
                            }
                            if (!isonlistview)
                            {
                                aListItem = new ListViewItem();
                                aListItem.SubItems.Add("");
                                aListItem.SubItems.Add("");
                                aListItem.SubItems.Add("");
                                epcTagsListView.Items.Add(aListItem);
                                s = temp;
                                aListItem.SubItems[0].Text = (epcTagsListView.Items.Count).ToString();
                                ChangeSubItem(aListItem, 1, s);
                            }
                            MessageBeep(5);
                        }
                    }
                }
            }
            isStopInventory = false;
        }

        private void ScanEpcTagsButton_Click(object sender, EventArgs e)
        {
            scanGen2Timer.Enabled = !scanGen2Timer.Enabled;

            if (!scanGen2Timer.Enabled)
            {
                scanEpcTagsButton.Text = "Сканировать";
                collector.WriteSession(session);

                if (isRfidSessionSending == false)
                {
                    new Thread(new ThreadStart(this.SendSessions)).Start();
                }
            }
            else
            {
                epcTagsListView.Items.Clear();
                list.Clear();

                session = new TubesSession
                    {
                    sessionMode = TubesSession.Mode.Reading,
                    time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };

                scanEpcTagsButton.Text = "Остановить";
            }
        }

    }
}
