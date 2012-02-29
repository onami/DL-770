using System;
using System.IO;
using System.Reflection;
using System.Data.SQLite;
using System.Data;

namespace DL770WinCE
{
    public class DataCollector
    {
        private string tableName = "rfidTags";
        private SQLiteConnection connection;

        public DataCollector()
        {
            string databasePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\rfidDb.sqlit";
            connection = new SQLiteConnection("data source=" + databasePath);
          
            connection.Open();

            if (isTableExist() == false)
            {
                createTable();
            }
        }

        private bool isTableExist()
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = @"SELECT name FROM sqlite_master WHERE type='table' AND name='" + tableName + "'";
                command.CommandType = CommandType.Text;
                if (command.ExecuteScalar() != null) return true;

                return false;
            }
        }

        private void createTable()
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = @"CREATE TABLE if not exists [rfidTags] (
                    [id] integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                    [data] char(32) NOT NULL,
                    [time] char(23) NOT NULL
                    );";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
        }

        public void write(String rfidTag)
        {
            var insertString = "INSERT INTO rfidTags (data, time) VALUES('" + rfidTag + "', '" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            var comInsert = new SQLiteCommand(insertString, connection);
            comInsert.ExecuteNonQuery();
            comInsert.Dispose();
        }

        ~DataCollector()
        {
            connection.Close();
        }

    }
}