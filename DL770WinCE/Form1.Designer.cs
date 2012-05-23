

namespace DL770WinCE
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.TubesLengthText = new System.Windows.Forms.Label();
            this.WellNumberText = new System.Windows.Forms.Label();
            this.TubesLength = new System.Windows.Forms.TextBox();
            this.WellNumber = new System.Windows.Forms.TextBox();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPageReader = new System.Windows.Forms.TabPage();
            this.button4 = new System.Windows.Forms.Button();
            this.ComboBox_PowerDbm = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ComboBox_dmaxfre = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ComboBox_dminfre = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.tbVersion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tabPageReadTag = new System.Windows.Forms.TabPage();
            this.ListView1_EPC = new System.Windows.Forms.ListView();
            this.listViewCol_Number = new System.Windows.Forms.ColumnHeader();
            this.listViewCol_ID = new System.Windows.Forms.ColumnHeader();
            this.listViewCol_Times = new System.Windows.Forms.ColumnHeader();
            this.button5 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button6 = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ListView_ID_6B = new System.Windows.Forms.ListView();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer();
            this.timer3 = new System.Windows.Forms.Timer();
            this.timer4 = new System.Windows.Forms.Timer();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPageReader.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPageReadTag.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPageReader);
            this.tabControl1.Controls.Add(this.tabPageReadTag);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(238, 265);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.TubesLengthText);
            this.tabPage1.Controls.Add(this.WellNumberText);
            this.tabPage1.Controls.Add(this.TubesLength);
            this.tabPage1.Controls.Add(this.WellNumber);
            this.tabPage1.Controls.Add(this.dateTimePicker);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(230, 236);
            this.tabPage1.Text = "EPCG2_R/W";
            // 
            // TubesLengthText
            // 
            this.TubesLengthText.Location = new System.Drawing.Point(3, 83);
            this.TubesLengthText.Name = "TubesLengthText";
            this.TubesLengthText.Size = new System.Drawing.Size(109, 20);
            this.TubesLengthText.Text = "Длина подвески";
            // 
            // WellNumberText
            // 
            this.WellNumberText.Location = new System.Drawing.Point(3, 30);
            this.WellNumberText.Name = "WellNumberText";
            this.WellNumberText.Size = new System.Drawing.Size(118, 20);
            this.WellNumberText.Text = "Номер скважины";
            // 
            // TubesLength
            // 
            this.TubesLength.Location = new System.Drawing.Point(3, 106);
            this.TubesLength.Name = "TubesLength";
            this.TubesLength.Size = new System.Drawing.Size(109, 23);
            this.TubesLength.TabIndex = 20;
            this.TubesLength.Text = "0";
            // 
            // WellNumber
            // 
            this.WellNumber.Location = new System.Drawing.Point(3, 53);
            this.WellNumber.Name = "WellNumber";
            this.WellNumber.Size = new System.Drawing.Size(109, 23);
            this.WellNumber.TabIndex = 19;
            this.WellNumber.Text = "0";
            this.WellNumber.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.CustomFormat = "yyyy";
            this.dateTimePicker.Location = new System.Drawing.Point(4, 3);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(217, 24);
            this.dateTimePicker.TabIndex = 18;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(118, 213);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 20);
            this.button2.TabIndex = 15;
            this.button2.Text = "Write Data";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 213);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 20);
            this.button1.TabIndex = 14;
            this.button1.Text = "Read Data";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPageReader
            // 
            this.tabPageReader.Controls.Add(this.button4);
            this.tabPageReader.Controls.Add(this.ComboBox_PowerDbm);
            this.tabPageReader.Controls.Add(this.label10);
            this.tabPageReader.Controls.Add(this.ComboBox_dmaxfre);
            this.tabPageReader.Controls.Add(this.label9);
            this.tabPageReader.Controls.Add(this.ComboBox_dminfre);
            this.tabPageReader.Controls.Add(this.label3);
            this.tabPageReader.Controls.Add(this.panel1);
            this.tabPageReader.Controls.Add(this.tbVersion);
            this.tabPageReader.Controls.Add(this.label1);
            this.tabPageReader.Controls.Add(this.btnDisconnect);
            this.tabPageReader.Controls.Add(this.btnConnect);
            this.tabPageReader.Location = new System.Drawing.Point(4, 25);
            this.tabPageReader.Name = "tabPageReader";
            this.tabPageReader.Size = new System.Drawing.Size(230, 236);
            this.tabPageReader.Text = "Parameter";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(51, 194);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(121, 30);
            this.button4.TabIndex = 12;
            this.button4.Text = "Update";
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // ComboBox_PowerDbm
            // 
            this.ComboBox_PowerDbm.Items.Add("20");
            this.ComboBox_PowerDbm.Items.Add("21");
            this.ComboBox_PowerDbm.Items.Add("22");
            this.ComboBox_PowerDbm.Items.Add("23");
            this.ComboBox_PowerDbm.Items.Add("24");
            this.ComboBox_PowerDbm.Items.Add("25");
            this.ComboBox_PowerDbm.Items.Add("26");
            this.ComboBox_PowerDbm.Items.Add("27");
            this.ComboBox_PowerDbm.Items.Add("28");
            this.ComboBox_PowerDbm.Items.Add("29");
            this.ComboBox_PowerDbm.Items.Add("30");
            this.ComboBox_PowerDbm.Location = new System.Drawing.Point(119, 159);
            this.ComboBox_PowerDbm.Name = "ComboBox_PowerDbm";
            this.ComboBox_PowerDbm.Size = new System.Drawing.Size(100, 23);
            this.ComboBox_PowerDbm.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(12, 162);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 20);
            this.label10.Text = "RF Power:";
            // 
            // ComboBox_dmaxfre
            // 
            this.ComboBox_dmaxfre.Location = new System.Drawing.Point(119, 129);
            this.ComboBox_dmaxfre.Name = "ComboBox_dmaxfre";
            this.ComboBox_dmaxfre.Size = new System.Drawing.Size(100, 23);
            this.ComboBox_dmaxfre.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(12, 132);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 20);
            this.label9.Text = "Max.Frequency:";
            // 
            // ComboBox_dminfre
            // 
            this.ComboBox_dminfre.Location = new System.Drawing.Point(119, 100);
            this.ComboBox_dminfre.Name = "ComboBox_dminfre";
            this.ComboBox_dminfre.Size = new System.Drawing.Size(100, 23);
            this.ComboBox_dminfre.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "Min.Frequency:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton6);
            this.panel1.Controls.Add(this.radioButton5);
            this.panel1.Location = new System.Drawing.Point(12, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(207, 37);
            // 
            // radioButton6
            // 
            this.radioButton6.Location = new System.Drawing.Point(111, 8);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(92, 20);
            this.radioButton6.TabIndex = 1;
            this.radioButton6.Text = "Command";
            // 
            // radioButton5
            // 
            this.radioButton5.Location = new System.Drawing.Point(3, 8);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(102, 20);
            this.radioButton5.TabIndex = 0;
            this.radioButton5.Text = "Continuing";
            // 
            // tbVersion
            // 
            this.tbVersion.BackColor = System.Drawing.SystemColors.Control;
            this.tbVersion.Location = new System.Drawing.Point(119, 71);
            this.tbVersion.Name = "tbVersion";
            this.tbVersion.ReadOnly = true;
            this.tbVersion.Size = new System.Drawing.Size(100, 23);
            this.tbVersion.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 20);
            this.label1.Text = "Version:";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(129, 45);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(90, 20);
            this.btnDisconnect.TabIndex = 0;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(12, 45);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(90, 20);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tabPageReadTag
            // 
            this.tabPageReadTag.Controls.Add(this.ListView1_EPC);
            this.tabPageReadTag.Controls.Add(this.button5);
            this.tabPageReadTag.Location = new System.Drawing.Point(4, 25);
            this.tabPageReadTag.Name = "tabPageReadTag";
            this.tabPageReadTag.Size = new System.Drawing.Size(230, 236);
            this.tabPageReadTag.Text = "EPCG2_ReadTag";
            // 
            // ListView1_EPC
            // 
            this.ListView1_EPC.Columns.Add(this.listViewCol_Number);
            this.ListView1_EPC.Columns.Add(this.listViewCol_ID);
            this.ListView1_EPC.Columns.Add(this.listViewCol_Times);
            this.ListView1_EPC.FullRowSelect = true;
            this.ListView1_EPC.Location = new System.Drawing.Point(1, 3);
            this.ListView1_EPC.Name = "ListView1_EPC";
            this.ListView1_EPC.Size = new System.Drawing.Size(227, 202);
            this.ListView1_EPC.TabIndex = 4;
            this.ListView1_EPC.View = System.Windows.Forms.View.Details;
            // 
            // listViewCol_Number
            // 
            this.listViewCol_Number.Text = "No.";
            this.listViewCol_Number.Width = 40;
            // 
            // listViewCol_ID
            // 
            this.listViewCol_ID.Text = "ID";
            this.listViewCol_ID.Width = 130;
            // 
            // listViewCol_Times
            // 
            this.listViewCol_Times.Text = "Times";
            this.listViewCol_Times.Width = 80;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(46, 211);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(140, 20);
            this.button5.TabIndex = 3;
            this.button5.Text = "Scan";
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button6);
            this.tabPage2.Controls.Add(this.comboBox2);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.ListView_ID_6B);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(230, 236);
            this.tabPage2.Text = "ISO6B_ReadTag";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(44, 204);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(140, 26);
            this.button6.TabIndex = 3;
            this.button6.Text = "List Tag ID";
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.Location = new System.Drawing.Point(91, 175);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(136, 23);
            this.comboBox2.TabIndex = 2;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "Read Interval:";
            // 
            // ListView_ID_6B
            // 
            this.ListView_ID_6B.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.ListView_ID_6B.Columns.Add(this.columnHeader5);
            this.ListView_ID_6B.Columns.Add(this.columnHeader6);
            this.ListView_ID_6B.Columns.Add(this.columnHeader7);
            this.ListView_ID_6B.FullRowSelect = true;
            this.ListView_ID_6B.Location = new System.Drawing.Point(1, 3);
            this.ListView_ID_6B.Name = "ListView_ID_6B";
            this.ListView_ID_6B.Size = new System.Drawing.Size(227, 166);
            this.ListView_ID_6B.TabIndex = 1;
            this.ListView_ID_6B.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "No.";
            this.columnHeader5.Width = 50;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "ID";
            this.columnHeader6.Width = 130;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Times";
            this.columnHeader7.Width = 90;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.listBox2);
            this.tabPage3.Controls.Add(this.button8);
            this.tabPage3.Controls.Add(this.button7);
            this.tabPage3.Controls.Add(this.textBox7);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.textBox6);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.textBox5);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.comboBox3);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(230, 236);
            this.tabPage3.Text = "ISO6B_R/W";
            // 
            // listBox2
            // 
            this.listBox2.Location = new System.Drawing.Point(3, 167);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(224, 66);
            this.listBox2.TabIndex = 11;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(131, 144);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(90, 20);
            this.button8.TabIndex = 10;
            this.button8.Text = "Write";
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(3, 144);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(100, 20);
            this.button7.TabIndex = 9;
            this.button7.Text = "Read";
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(3, 115);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(224, 23);
            this.textBox7.TabIndex = 8;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(3, 97);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(134, 20);
            this.label13.Text = "Written Data(Hex):";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(121, 70);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 23);
            this.textBox6.TabIndex = 4;
            this.textBox6.Text = "1";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(3, 64);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 31);
            this.label12.Text = "Length of Tag\r\nData(1-32/16):";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(121, 36);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 23);
            this.textBox5.TabIndex = 2;
            this.textBox5.Text = "0";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(3, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(112, 34);
            this.label11.Text = "Address of Tag\r\nData(0/8-223):";
            // 
            // comboBox3
            // 
            this.comboBox3.Location = new System.Drawing.Point(3, 3);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(218, 23);
            this.comboBox3.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer3
            // 
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // timer4
            // 
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 265);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "UHF-RFID demo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPageReader.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabPageReadTag.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageReader;
        private System.Windows.Forms.TextBox tbVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TabPage tabPageReadTag;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.ComboBox ComboBox_dminfre;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ComboBox ComboBox_PowerDbm;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox ComboBox_dmaxfre;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ListView ListView1_EPC;
        private System.Windows.Forms.ColumnHeader listViewCol_Number;
        private System.Windows.Forms.ColumnHeader listViewCol_ID;
        private System.Windows.Forms.ColumnHeader listViewCol_Times;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView ListView_ID_6B;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Timer timer4;
        private System.Windows.Forms.Label TubesLengthText;
        private System.Windows.Forms.Label WellNumberText;
        private System.Windows.Forms.TextBox TubesLength;
        private System.Windows.Forms.TextBox WellNumber;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
    }
}

