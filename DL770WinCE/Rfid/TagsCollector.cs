using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace DL770.Rfid
{
    /// <summary>
    /// Класс записи и чтения с sqlite-базы
    /// </summary>
    public class RfidTagsCollector
    {
        private readonly SQLiteConnection connection;

        public RfidTagsCollector(string connectionString)
        {
            connectionString = "data source=" + connectionString;

            //SQLite normally creates a new database without throwing any exeption.
            //So, if one wants to know whether a database exists or not,
            //he should add the 'FailIfMissing=True' option.
            connection = new SQLiteConnection(connectionString);
            connection.Open();
            CreateTables();
        }

        /// <summary>
        /// Возвращает список неотправленных на сервер сессий чтения
        /// </summary>
        public List<TubesSession> GetUnshippedTags()
        {
            var sessions = new List<TubesSession>();
            var sessionCmd = new SQLiteCommand(@"SELECT * from reading_sessions where delivery_status <> " + (int)TubesSession.DeliveryStatus.Shipped, connection);

            using (var sessionReader = sessionCmd.ExecuteReader())
            {
                while (sessionReader.Read())
                {
                    var session = new TubesSession
                        {
                            id = sessionReader.GetInt32(0),
                            time = sessionReader.GetString(1),
                            location = sessionReader.GetString(2),
                            deliveryStatus = (TubesSession.DeliveryStatus)sessionReader.GetInt32(3),
                            readingStatus = (TubesSession.ReadingStatus)sessionReader.GetInt32(4),
                            sessionMode = (TubesSession.Mode)sessionReader.GetInt32(5)
                        };

                    var tagCmd = new SQLiteCommand(@"SELECT * from tubes where session_id = " + session.id, connection);
                    using (var tagReader = tagCmd.ExecuteReader())
                    {
                        while (tagReader.Read())
                        {
                            session.tags.Add(tagReader.GetString(1));
                        }
                    }
                    sessions.Add(session);
                }
            }
            return sessions;
        }

        /// <summary>
        /// Возвращает список неотправленных на сервер сессий чтения
        /// </summary>
        public List<TubesBundleSession> GetUnshippedBundles()
        {
            var sessions = new List<TubesBundleSession>();
            var sessionCmd = new SQLiteCommand(@"SELECT * from tubes_bundles where delivery_status <> " + (int)TubesSession.DeliveryStatus.Shipped, connection);

            using (var sessionReader = sessionCmd.ExecuteReader())
            {
                while (sessionReader.Read())
                {
                    var session = new TubesBundleSession
                    {
                        id = sessionReader.GetInt32(0),
                        tag = sessionReader.GetString(1),
                        time = sessionReader.GetString(2),
                        bundle = new TubesBundle()
                        {
                            time = sessionReader.GetInt32(3),
                            districtId = (ushort)sessionReader.GetInt16(4),
                            bundleLength = (ushort)sessionReader.GetInt16(5),
                        },
                        deliveryStatus = (TubesBundleSession.DeliveryStatus)sessionReader.GetInt32(6),
                        sessionMode = (TubesBundleSession.Mode)sessionReader.GetInt32(7),
                    };

                    sessions.Add(session);
                }
            }
            return sessions;
        }


        /// <summary>
        /// Иниализация новой базы
        /// </summary>
        void CreateTables()
        {
            using (var cmd = new SQLiteCommand(connection))
            {
                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS tubes (
                    [session_id] integer NOT NULL,
                    [tag] char(60) NOT NULL,
                    PRIMARY KEY(session_id, tag)
                    );";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS reading_sessions (
                    [id] integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                    [time_marker] char(23) NOT NULL,
                    [location_id] char(32) DEFAULT NULL,
                    [delivery_status] integer NOT NULL,
                    [reading_status] integer NOT NULL,
                    [reading_mode] integer NOT NULL
                    );";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS tubes_bundles (
                    [id] integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                    [tag] char(60) NOT NULL,
                    [session_time] char(23) NOT NULL,
                    [bundle_time] integer NOT NULL,
                    [district_id] integer NOT NULL,
                    [bundle_length] integer NOT NULL,
                    [delivery_status] integer NOT NULL,
                    [session_mode] integer NOT NULL
                    );";
                cmd.ExecuteNonQuery();
            }
        }


        public void WriteSession(TubesBundleSession session)
        {
            var transaction = connection.BeginTransaction();

            //Register a new session
            var cmd = new SQLiteCommand(@"
            INSERT INTO tubes_bundles ( tag,  session_time,  bundle_time,  district_id,  bundle_length,  delivery_status,  session_mode)
                               VALUES(@tag, @session_time, @bundle_time, @district_id, @bundle_length, @delivery_status, @session_mode)", connection);
            cmd.Parameters.AddWithValue("@tag", session.tag);
            cmd.Parameters.AddWithValue("@session_time", session.time);
            cmd.Parameters.AddWithValue("@bundle_time", session.bundle.time);
            cmd.Parameters.AddWithValue("@district_id", session.bundle.districtId);
            cmd.Parameters.AddWithValue("@bundle_length", session.bundle.bundleLength);
            cmd.Parameters.AddWithValue("@delivery_status", session.deliveryStatus);
            cmd.Parameters.AddWithValue("@session_mode", session.sessionMode);
            cmd.ExecuteNonQuery();

            transaction.Commit();
        }

        public void WriteSession(TubesSession session)
        {
            if (session.tags.Count == 0)
                return;

            var transaction = connection.BeginTransaction();

            //Register a new session
            var cmd = new SQLiteCommand(@"
            INSERT INTO reading_sessions (time_marker,  location_id,  delivery_status,  reading_status,  reading_mode)
                                  VALUES(@time_marker, @location_id, @delivery_status, @reading_status, @reading_mode)", connection);
            cmd.Parameters.AddWithValue("@time_marker", session.time);
            cmd.Parameters.AddWithValue("@location_id", session.location);
            cmd.Parameters.AddWithValue("@delivery_status", session.deliveryStatus);
            cmd.Parameters.AddWithValue("@reading_status", session.readingStatus);
            cmd.Parameters.AddWithValue("@reading_mode", session.sessionMode);
            cmd.ExecuteNonQuery();

            //Look up the last session id
            cmd.CommandText = "SELECT last_insert_rowid()";
            var sessionId = Convert.ToInt32(cmd.ExecuteScalar());

            //Prepare for INSERT
            cmd = new SQLiteCommand("INSERT INTO tubes (session_id, tag) VALUES(@session_id, @tag)", connection);
            cmd.Parameters.AddWithValue("@session_id", sessionId);
            var tag_ = new SQLiteParameter("@tag");
            cmd.Parameters.Add(tag_);

            //Add new tags
            foreach (var tag in session.tags)
            {
                tag_.Value = tag;
                cmd.ExecuteNonQuery();
            }

            transaction.Commit();
        }

        /// <summary>
        /// Закрытие соединения
        /// </summary>
        /// <remarks>По каким-то причинам в Win CE закрытие не работает в деструкторе,
        /// поэтому был вынес его в этот метод</remarks>
        public void Close()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }

        /// <summary>
        /// Обновление состояния по сессиям. Применяется при успешной отправке данных.
        /// </summary>
        /// <param name="sessions"></param>
        public void SetDeliveryStatus(List<TubesSession> sessions)
        {
            foreach (var session in sessions)
            {
                var cmd = new SQLiteCommand("UPDATE reading_sessions SET delivery_status = @delivery_status where id = @id", connection);
                cmd.Parameters.AddWithValue("@delivery_status", (int)session.deliveryStatus);
                cmd.Parameters.AddWithValue("@id", session.id);
                cmd.ExecuteNonQuery();
            }
        }

        public void SetDeliveryStatus(List<TubesBundleSession> sessions)
        {
            foreach (var session in sessions)
            {
                var cmd = new SQLiteCommand("UPDATE tubes_bundles SET delivery_status = @delivery_status where id = @id", connection);
                cmd.Parameters.AddWithValue("@delivery_status", (int)session.deliveryStatus);
                cmd.Parameters.AddWithValue("@id", session.id);
                cmd.ExecuteNonQuery();
            }
        }


    }
}