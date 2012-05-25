using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using CodeBetter.Json;

namespace DL770.Rfid
{
    /// <summary>
    /// Используется модификация стандарта JSON-RPC:
    ///     В result записывается некие данные, которые возвращает сервер в ответ на запрос; 
    ///     В error записывается либо null, если всё прошло нормально, либо же код ошибки.
    ///     В последнем случае в result записывается возможное словесное описание ошибки. 
    /// </summary>
    class RpcResponse
    {
        public string result;
        public StatusCode status;

        /// <summary>
        /// Перечисление определяет возможный статус запроса, возвращаемый сервером.
        /// InternalServerError и ConnectionFail могут
        /// быть установлены самим клиентом в SendPostData
        /// </summary>
        public enum StatusCode
        {
            Ok = 0,
            InternalServerError = 1,
            InvalidDeviceKey = 2,
            CorruptedChecksum = 3,
            CorruptedFormat = 4,
            DuplicatedMessage = 5,
            EmptyRequest = 6,
            ConnectionFail = 7
        };

        public RpcResponse()
        {

        }
    }

    /// <summary>
    /// Класс, отвечающий за посылку данных считывания на сервер
    /// </summary>
    public class RfidWebClient
    {
        /// <summary>
        /// Определяет настройки подключения: сервер, ключ устройства.
        /// Смотри файл config.xml.
        /// </summary>
        readonly Configuration Configuration;

        readonly SHA1CryptoServiceProvider Sha1 = new SHA1CryptoServiceProvider();
        readonly UTF8Encoding UniEncoding = new UTF8Encoding();

        /// <summary>
        ///  Инициализация вебклиента
        /// </summary>
        /// <param name="conf"></param>
        public RfidWebClient(Configuration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Посылка сессий считывания
        /// </summary>
        /// <param name="unshippedSessions">Список сессий считывания со статусом Session.DeliveryStatus.Unshipped.
        /// Список заполняется TagsCollector.GetUnshippedTags()</param>
        public void SendRfidReports(List<TubesSession> unshippedSessions)
        {
            var url = "post/" + Configuration.DeviceKey + "/";

            foreach (var session in unshippedSessions)
            {
                var jsonString = Converter.Serialize(session);
                var jsonHashString = String.Empty;

                //Получается, что конвертация производится и в этом методе
                //и в SendPostData. Возможно проще переписать метод
                var inHashBytes = UniEncoding.GetBytes(jsonString);
                var outHashBytes = Sha1.ComputeHash(inHashBytes);

                foreach (var b in outHashBytes)
                {
                    jsonHashString += b.ToString("x2");
                }

                var response = SendPostData(url, "json=" + jsonString + "&checksum=" + jsonHashString);

                //Если дубликат, тоже не отправлять.
                if (response.status == RpcResponse.StatusCode.Ok || response.status == RpcResponse.StatusCode.DuplicatedMessage)
                {
                    session.deliveryStatus = TubesSession.DeliveryStatus.Shipped;
                }
            }
        }

        /// <summary>
        /// Отправка и обработка данных. Обёртка над UploadString.
        /// </summary>
        /// <param name="url">Метод</param>
        /// <param name="data">Параметры</param>
        RpcResponse SendPostData(string url, string data)
        {
            try
            {
                var byteArray = UniEncoding.GetBytes(data);
                var webRequest = (HttpWebRequest)WebRequest.Create(Configuration.Server + url);
                //ставим в null; в противном случае webRequest начинает поиск прокси и тратит кучу времени
                //На DL770 падает
                //webRequest.Proxy = null; 
                webRequest.Method = "POST";
                webRequest.AllowAutoRedirect = false;
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = byteArray.Length;
                webRequest.Timeout = 5000;
                var webpageStream = webRequest.GetRequestStream();
                webpageStream.Write(byteArray, 0, byteArray.Length);
                webpageStream.Close();
                      
                //обработка
                using (var response = webRequest.GetResponse())
                {
                    var responseStr = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    //Использовал самописный десериализатор, т.к. CodeBetter, цитирую:
                    //On the whole I found it works; From memory it does not deal with nulls nicely,
                    //and I think I had to tweak datetime serialisation to make it work the way
                    //other json serialisers do.
                    //
                    //Иными словами, Converter.Deserialize<RpcResponse>(responseStr) валится на "result":"null"
                    return RfidJson.Deserialize(responseStr);
                }
            }

            catch (Exception e)
            {
                var response = new RpcResponse();
                response.result = e.Message;
                response.status = RpcResponse.StatusCode.InternalServerError;
                return response;
            }
        }
    }
}
