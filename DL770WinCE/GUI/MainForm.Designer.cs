﻿

using System;
namespace DL770
{
    partial class MainForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.TubesPack = new System.Windows.Forms.TabPage();
            this.syncTimeButton = new System.Windows.Forms.Button();
            this.tubesPackWriteButton = new System.Windows.Forms.Button();
            this.tubesLengthText = new System.Windows.Forms.Label();
            this.wellNumberText = new System.Windows.Forms.Label();
            this.tubesLengthInput = new System.Windows.Forms.TextBox();
            this.wellNumberInput = new System.Windows.Forms.TextBox();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.tubesPackReadButton = new System.Windows.Forms.Button();
            this.EPCG2_ReadTags = new System.Windows.Forms.TabPage();
            this.epcTagsListView = new System.Windows.Forms.ListView();
            this.listViewCol_Number = new System.Windows.Forms.ColumnHeader();
            this.listViewCol_ID = new System.Windows.Forms.ColumnHeader();
            this.listViewCol_Times = new System.Windows.Forms.ColumnHeader();
            this.scanEpcTagsButton = new System.Windows.Forms.Button();
            this.Parameters = new System.Windows.Forms.TabPage();
            this.updateButton = new System.Windows.Forms.Button();
            this.powerDbmComboBox = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.maxFrequencyComboBox = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.minFrequencyComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.connectButton = new System.Windows.Forms.Button();
            this.scanGen2Timer = new System.Windows.Forms.Timer();
            this.TagEpcLabel = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.TubesPack.SuspendLayout();
            this.EPCG2_ReadTags.SuspendLayout();
            this.Parameters.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.TubesPack);
            this.tabControl.Controls.Add(this.EPCG2_ReadTags);
            this.tabControl.Controls.Add(this.Parameters);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(238, 265);
            this.tabControl.TabIndex = 1;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.TabControl_SelectedIndexChanged);
            // 
            // TubesPack
            // 
            this.TubesPack.Controls.Add(this.TagEpcLabel);
            this.TubesPack.Controls.Add(this.syncTimeButton);
            this.TubesPack.Controls.Add(this.tubesPackWriteButton);
            this.TubesPack.Controls.Add(this.tubesLengthText);
            this.TubesPack.Controls.Add(this.wellNumberText);
            this.TubesPack.Controls.Add(this.tubesLengthInput);
            this.TubesPack.Controls.Add(this.wellNumberInput);
            this.TubesPack.Controls.Add(this.dateTimePicker);
            this.TubesPack.Controls.Add(this.tubesPackReadButton);
            this.TubesPack.Location = new System.Drawing.Point(4, 25);
            this.TubesPack.Name = "TubesPack";
            this.TubesPack.Size = new System.Drawing.Size(230, 236);
            this.TubesPack.Text = "Подвеска ";
            // 
            // syncTimeButton
            // 
            this.syncTimeButton.Location = new System.Drawing.Point(159, 3);
            this.syncTimeButton.Name = "syncTimeButton";
            this.syncTimeButton.Size = new System.Drawing.Size(68, 24);
            this.syncTimeButton.TabIndex = 24;
            this.syncTimeButton.Text = "Синхро";
            this.syncTimeButton.Click += new System.EventHandler(this.SyncTimeButton_Click);
            // 
            // tubesPackWriteButton
            // 
            this.tubesPackWriteButton.Enabled = false;
            this.tubesPackWriteButton.Location = new System.Drawing.Point(3, 91);
            this.tubesPackWriteButton.Name = "tubesPackWriteButton";
            this.tubesPackWriteButton.Size = new System.Drawing.Size(103, 20);
            this.tubesPackWriteButton.TabIndex = 21;
            this.tubesPackWriteButton.Text = "Записать";
            this.tubesPackWriteButton.Click += new System.EventHandler(this.TubesPackWriteButton_Click);
            // 
            // tubesLengthText
            // 
            this.tubesLengthText.Location = new System.Drawing.Point(118, 30);
            this.tubesLengthText.Name = "tubesLengthText";
            this.tubesLengthText.Size = new System.Drawing.Size(109, 20);
            this.tubesLengthText.Text = "Подвеска, м";
            // 
            // wellNumberText
            // 
            this.wellNumberText.Location = new System.Drawing.Point(3, 30);
            this.wellNumberText.Name = "wellNumberText";
            this.wellNumberText.Size = new System.Drawing.Size(104, 20);
            this.wellNumberText.Text = "№ скважины";
            // 
            // tubesLengthInput
            // 
            this.tubesLengthInput.Location = new System.Drawing.Point(118, 53);
            this.tubesLengthInput.Name = "tubesLengthInput";
            this.tubesLengthInput.Size = new System.Drawing.Size(109, 23);
            this.tubesLengthInput.TabIndex = 20;
            this.tubesLengthInput.Text = "0";
            // 
            // wellNumberInput
            // 
            this.wellNumberInput.Location = new System.Drawing.Point(3, 53);
            this.wellNumberInput.Name = "wellNumberInput";
            this.wellNumberInput.Size = new System.Drawing.Size(104, 23);
            this.wellNumberInput.TabIndex = 19;
            this.wellNumberInput.Text = "0";
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker.Location = new System.Drawing.Point(4, 3);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(149, 24);
            this.dateTimePicker.TabIndex = 18;
            this.dateTimePicker.Value = new System.DateTime(2012, 5, 25, 11, 36, 49, 103);
            // 
            // tubesPackReadButton
            // 
            this.tubesPackReadButton.Enabled = false;
            this.tubesPackReadButton.Location = new System.Drawing.Point(118, 91);
            this.tubesPackReadButton.Name = "tubesPackReadButton";
            this.tubesPackReadButton.Size = new System.Drawing.Size(109, 20);
            this.tubesPackReadButton.TabIndex = 15;
            this.tubesPackReadButton.Text = "Считать";
            this.tubesPackReadButton.Click += new System.EventHandler(this.TubesPackReadButton_Click);
            // 
            // EPCG2_ReadTags
            // 
            this.EPCG2_ReadTags.Controls.Add(this.epcTagsListView);
            this.EPCG2_ReadTags.Controls.Add(this.scanEpcTagsButton);
            this.EPCG2_ReadTags.Location = new System.Drawing.Point(4, 25);
            this.EPCG2_ReadTags.Name = "EPCG2_ReadTags";
            this.EPCG2_ReadTags.Size = new System.Drawing.Size(230, 236);
            this.EPCG2_ReadTags.Text = "Метки ";
            // 
            // epcTagsListView
            // 
            this.epcTagsListView.Columns.Add(this.listViewCol_Number);
            this.epcTagsListView.Columns.Add(this.listViewCol_ID);
            this.epcTagsListView.Columns.Add(this.listViewCol_Times);
            this.epcTagsListView.FullRowSelect = true;
            this.epcTagsListView.Location = new System.Drawing.Point(1, 3);
            this.epcTagsListView.Name = "epcTagsListView";
            this.epcTagsListView.Size = new System.Drawing.Size(227, 202);
            this.epcTagsListView.TabIndex = 4;
            this.epcTagsListView.View = System.Windows.Forms.View.Details;
            // 
            // listViewCol_Number
            // 
            this.listViewCol_Number.Text = "No.";
            this.listViewCol_Number.Width = 35;
            // 
            // listViewCol_ID
            // 
            this.listViewCol_ID.Text = "ID";
            this.listViewCol_ID.Width = 190;
            // 
            // listViewCol_Times
            // 
            this.listViewCol_Times.Text = "";
            this.listViewCol_Times.Width = 34;
            // 
            // scanEpcTagsButton
            // 
            this.scanEpcTagsButton.Enabled = false;
            this.scanEpcTagsButton.Location = new System.Drawing.Point(1, 211);
            this.scanEpcTagsButton.Name = "scanEpcTagsButton";
            this.scanEpcTagsButton.Size = new System.Drawing.Size(227, 20);
            this.scanEpcTagsButton.TabIndex = 3;
            this.scanEpcTagsButton.Text = "Сканировать";
            this.scanEpcTagsButton.Click += new System.EventHandler(this.ScanEpcTagsButton_Click);
            // 
            // Parameters
            // 
            this.Parameters.Controls.Add(this.updateButton);
            this.Parameters.Controls.Add(this.powerDbmComboBox);
            this.Parameters.Controls.Add(this.label10);
            this.Parameters.Controls.Add(this.maxFrequencyComboBox);
            this.Parameters.Controls.Add(this.label9);
            this.Parameters.Controls.Add(this.minFrequencyComboBox);
            this.Parameters.Controls.Add(this.label3);
            this.Parameters.Controls.Add(this.disconnectButton);
            this.Parameters.Controls.Add(this.connectButton);
            this.Parameters.Location = new System.Drawing.Point(4, 25);
            this.Parameters.Name = "Parameters";
            this.Parameters.Size = new System.Drawing.Size(230, 236);
            this.Parameters.Text = "Настройки ";
            // 
            // updateButton
            // 
            this.updateButton.Enabled = false;
            this.updateButton.Location = new System.Drawing.Point(1, 211);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(227, 20);
            this.updateButton.TabIndex = 12;
            this.updateButton.Text = "Обновить настройки";
            this.updateButton.Click += new System.EventHandler(this.UpdateSettingsButton_Click);
            // 
            // powerDbmComboBox
            // 
            this.powerDbmComboBox.Items.Add("20");
            this.powerDbmComboBox.Items.Add("21");
            this.powerDbmComboBox.Items.Add("22");
            this.powerDbmComboBox.Items.Add("23");
            this.powerDbmComboBox.Items.Add("24");
            this.powerDbmComboBox.Items.Add("25");
            this.powerDbmComboBox.Items.Add("26");
            this.powerDbmComboBox.Items.Add("27");
            this.powerDbmComboBox.Items.Add("28");
            this.powerDbmComboBox.Items.Add("29");
            this.powerDbmComboBox.Items.Add("30");
            this.powerDbmComboBox.Location = new System.Drawing.Point(117, 92);
            this.powerDbmComboBox.Name = "powerDbmComboBox";
            this.powerDbmComboBox.Size = new System.Drawing.Size(110, 23);
            this.powerDbmComboBox.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(3, 95);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 20);
            this.label10.Text = "Мощность";
            // 
            // maxFrequencyComboBox
            // 
            this.maxFrequencyComboBox.Location = new System.Drawing.Point(117, 62);
            this.maxFrequencyComboBox.Name = "maxFrequencyComboBox";
            this.maxFrequencyComboBox.Size = new System.Drawing.Size(110, 23);
            this.maxFrequencyComboBox.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(3, 65);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 20);
            this.label9.Text = "Макс. частота";
            // 
            // minFrequencyComboBox
            // 
            this.minFrequencyComboBox.Location = new System.Drawing.Point(117, 33);
            this.minFrequencyComboBox.Name = "minFrequencyComboBox";
            this.minFrequencyComboBox.Size = new System.Drawing.Size(110, 23);
            this.minFrequencyComboBox.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "Мин. частота";
            // 
            // disconnectButton
            // 
            this.disconnectButton.Enabled = false;
            this.disconnectButton.Location = new System.Drawing.Point(117, 3);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(110, 20);
            this.disconnectButton.TabIndex = 0;
            this.disconnectButton.Text = "Выключить";
            this.disconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(3, 3);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(100, 20);
            this.connectButton.TabIndex = 0;
            this.connectButton.Text = "Включить";
            this.connectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // scanGen2Timer
            // 
            this.scanGen2Timer.Interval = 50;
            this.scanGen2Timer.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // TagEpcLabel
            // 
            this.TagEpcLabel.Location = new System.Drawing.Point(6, 127);
            this.TagEpcLabel.Name = "TagEpcLabel";
            this.TagEpcLabel.Size = new System.Drawing.Size(182, 20);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 265);
            this.Controls.Add(this.tabControl);
            this.Name = "MainForm";
            this.Text = "UHF-RFID";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.tabControl.ResumeLayout(false);
            this.TubesPack.ResumeLayout(false);
            this.EPCG2_ReadTags.ResumeLayout(false);
            this.Parameters.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage Parameters;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.TabPage EPCG2_ReadTags;
        private System.Windows.Forms.Timer scanGen2Timer;
        private System.Windows.Forms.TabPage TubesPack;
        private System.Windows.Forms.Button tubesPackReadButton;
        private System.Windows.Forms.ComboBox minFrequencyComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.ComboBox powerDbmComboBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox maxFrequencyComboBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button scanEpcTagsButton;
        private System.Windows.Forms.ListView epcTagsListView;
        private System.Windows.Forms.ColumnHeader listViewCol_Number;
        private System.Windows.Forms.ColumnHeader listViewCol_ID;
        private System.Windows.Forms.ColumnHeader listViewCol_Times;
        private System.Windows.Forms.Label tubesLengthText;
        private System.Windows.Forms.Label wellNumberText;
        private System.Windows.Forms.TextBox tubesLengthInput;
        private System.Windows.Forms.TextBox wellNumberInput;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Button tubesPackWriteButton;
        private System.Windows.Forms.Button syncTimeButton;
        private System.Windows.Forms.Label TagEpcLabel;
    }
}

