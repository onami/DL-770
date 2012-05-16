using System;
using System.Xml.Serialization;
using System.IO;

namespace DL770.Rfid
{
    /// <summary>
    /// Класс, хранящий данные о соединении с сервером
    /// </summary>
    public class Configuration
    {
        public string Server;
        public string DeviceKey;
        public string Location;

        /// <summary>
        /// Сериализовать данные и сохранить
        /// </summary>
        /// <param name="path"></param>
        public void Serialize(string path)
        {
            var serializer = new XmlSerializer(typeof(Configuration));
            File.Delete(path);
            TextWriter writer = new StreamWriter(path);
            serializer.Serialize(writer, this);
            writer.Close();
        }

        /// <summary>
        /// Мэппинг конфигурации из файла в экземпляр класса
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Configuration Deserialize(string path)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(Configuration));
                var fs = new FileStream(path, FileMode.Open);
                var conf = (Configuration)serializer.Deserialize(fs);
                fs.Close();
                return conf;
            }

            catch
            {
                var conf = new Configuration();
                conf.Serialize("config.xml");
                return null;
            }
        }
    }
}