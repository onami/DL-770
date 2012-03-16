using System;
using System.IO;
using System.Reflection;
using System.Data.SQLite;
using System.Data;

namespace DL770WinCE
{
    public class DataCollector
    {
        string tableName = "rfidTags";
        SQLiteConnection connection;

        int queryCnt; //количество сделанных записей

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

        void checkTransaction()
        {
            if (queryCnt == 0)
            {
                new SQLiteCommand("begin", connection).ExecuteNonQuery();
                queryCnt++;
            }
            else if (queryCnt > 20)
            {
                new SQLiteCommand("end", connection).ExecuteNonQuery();
                queryCnt = 0;
                checkTransaction();
            }    
        }

        bool isTableExist()
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = @"SELECT name FROM sqlite_master WHERE type='table' AND name='" + tableName + "'";
                command.CommandType = CommandType.Text;
                if (command.ExecuteScalar() != null) return true;

                return false;
            }
        }

        void createTable()
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
            checkTransaction();
            comInsert.ExecuteNonQuery();
        }

        ~DataCollector()
        {
            //if (connection != null && queryCnt != 0)
            //{
            //    new SQLiteCommand("end", connection).ExecuteNonQuery();       
            //}
            connection.Close();
        }

    }
}