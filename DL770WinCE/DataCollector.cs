using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;

namespace DL770WinCE
{
    public class DataCollector
    {
        private SqlCeConnection connection;

        public DataCollector()
        {
            connection = new SqlCeConnection("Data Source=RFID_DB.sdf");
            connection.Open();        
        }

        public void write(String rfidTag)
        {
            var cmd = connection.CreateCommand();
            var insertString = "INSERT INTO rfid(tagName) VALUES("+rfidTag+")";
            var comInsert = new SqlCeCommand(insertString, connection);
            comInsert.ExecuteNonQuery();
        }

        ~DataCollector()
        {
            connection.Close();
        }

    }
}