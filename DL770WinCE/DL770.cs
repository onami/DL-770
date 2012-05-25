using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DL770
{
    class RfidReader
    {
        private byte address; //Некий адрес считывателя
        byte[] password = new byte[4] { 0, 0, 0, 0 }; //Пароль доступа к метке
        public ResultCode connectionStatus { get; private set; }

        public enum MemorySection : byte
        {
            Password = 0x00,
            Epc = 0x01,
            Tid = 0x02,
            User = 0x03
        }

        public enum ErrorCode : byte
        {
            Unknown = 0x0f,
            CmdDenialOfService = 0x04,
            UltraLightTagAnticollisionFailed = 0x33,
            MF1UtraLightAnticollision = 0x32,
            MultipleTagsInRfField = 0x31,
            AnticollisionFailed = 0x30
        };

        public enum ResultCode
        {
            Ok = 0x00,
            WrongLength = 0x01,
            NotSupportedOperation = 0x0b,
            DataRangeError = 0x03,
            CmdDenialOfService = 0x04,
            E2PromOperationError = 0x06,
            TimeoutExceeded = 0x02,
            UnknownError = 0x0f,
            CommunicationError = 0x30,
            CrcChecksumError = 0x31,
            ComPortOpened = 0x35,
            ComPostClosed = 0x36
        };

        public RfidReader()
        {
            RWDev.ModulePowerOff();
            RWDev.ModulePowerOn();
            RWDev.DisconnectReader();
            connectionStatus = (ResultCode)RWDev.ConnectReader();
        }

        public void Off()
        {
            RWDev.ModulePowerOff();
            connectionStatus = (ResultCode)RWDev.DisconnectReader();
        }

        static public string GetErrorCodeDescription(ErrorCode c)
        {
            switch (c)
            {
                case ErrorCode.Unknown:
                    return "Unknown error";
                case ErrorCode.CmdDenialOfService:
                    return "CMD can't execute at current";
                case ErrorCode.UltraLightTagAnticollisionFailed:
                    return "Utralight tag anticollision failed";
                case ErrorCode.MF1UtraLightAnticollision:
                    return "MF1 and Utralight tag collision";
                case ErrorCode.MultipleTagsInRfField:
                    return "Unallow more than one tag in RF field";
                case ErrorCode.AnticollisionFailed:
                    return "Anticollision failed";
                default:
                    return ((int)c).ToString("X");
            }
        }

        static public string GetResultCodeDescription(ResultCode c)
        {
            switch (c)
            {
                case ResultCode.Ok: return "success";
                case ResultCode.WrongLength: return "Command operand length error";
                case ResultCode.NotSupportedOperation: return "Command not supported";
                case ResultCode.E2PromOperationError: return "E2PROM operation error";
                case ResultCode.TimeoutExceeded: return "InventoryScanTime overflow,no UID collected";
                case ResultCode.CommunicationError: return "Communication Error";
                case ResultCode.CrcChecksumError: return "CRC checksummat Error";
                case ResultCode.CmdDenialOfService: return "CMD can't execute at current";
                case ResultCode.DataRangeError: return "Operation data range error";
                default:
                    return ((int)c).ToString("X");
            }
        }

        /// <summary>
        /// Читает байты с метки с определенной секции данных.
        /// </summary>
        /// <remarks></remarks>
        /// <param name="tag"></param>
        /// <param name="data">Внимание! RWDev.ReadCard_G2 возвращает Len = 5, если метка считана не была.
        /// В этом случае данный метод присваивает data пустое значение null</param>
        /// <param name="memorySection"></param>
        /// <returns></returns>
        public ResultCode ReadBytes(byte[] tag, ref byte[] data, MemorySection memorySection)
        {
            byte totalLength = 0; //Некая общая длина всех передаваемых данных
            byte offset = 0; //Смещение, с которого считать данные
            var result = ResultCode.UnknownError;
            byte errorCode = 0;

            totalLength = (byte)(12 + tag.Length); //12 - это китайская магия

            result = (ResultCode)RWDev.ReadCard_G2(
                ref address,
                ref totalLength,
                (byte)(tag.Length/2),
                tag,
                (byte)memorySection,
                offset,
                (byte)(data.Length/2),
                password,
                data,
                ref errorCode);

            if (totalLength == 5)
            {
                data = null;
            }

            return result;
        }


        public ResultCode WriteBytes(byte[] tag, byte[] data, MemorySection memorySection)
        {
            byte totalLength = 0; //Некая общая длина всех передаваемых данных
            byte offset = 0; //Смещение, с которого записать данные
            var result = ResultCode.UnknownError;
            byte errorCode = 0;

            totalLength = Convert.ToByte(12 + tag.Length + data.Length); //12 - это китайская магия

            return result = (ResultCode)RWDev.WriteCard_G2(
                ref address,
                ref totalLength,
                (byte)(data.Length/2),
                (byte)(tag.Length/2),
                tag,
                (byte)memorySection,
                offset,
                data,
                password,
                ref errorCode);
        }

        /// <summary>
        /// Даёт инфу о первом попавшемся теге
        /// </summary>
        /// <returns></returns>
        public byte[] GetFirstTag()
        {
            var cnt = 5;
            var data_ = new byte[160];
            byte length = 0;
            byte cardsCount = 0;

            //На случае, если с первого раза метка считана не была
            while (cardsCount == 0 && cnt-- > 0)
            {
                Thread.Sleep(50);
            }

            if (RWDev.Inventory_G2(ref address, ref length, data_, ref cardsCount) == 0)
            {
                var data = new byte[length - 6]; //Почему -6?
                Array.Copy(data_, data, length - 6);

                //Формат записи:
                //1 байт - длина метки
                //далее тело метки
                //затем последовательность повторяется

                //Следовательно, в daw[0] лежит длина первой метки
                byte[] firstTagData = new byte[data[0]];
                //1 - это смещение; мы не копируем байт с указанием длины метки
                for (var i = 0; i < firstTagData.Length; i++)
                {
                    firstTagData[i] = data[i + 1];
                }                
                
                return firstTagData;
            }

            else
            {
                throw new Exception("Метка не обнаружена");
            }
        }

        /// <summary>
        /// Обновляет мощность считывателя
        /// </summary>
        /// <param name="power"></param>
        /// <param name="minFreq"></param>
        /// <param name="maxFreq"></param>
        /// <returns></returns>
        public ResultCode UpdatePower(byte power)
        {
            return (ResultCode)RWDev.Writepower(ref address, ref power);
        }

        /// <summary>
        /// Обновляет диапазон рабочих частот считывателя
        /// </summary>
        /// <param name="maxFreq"></param>
        /// <param name="minFreq"></param>
        /// <returns></returns>
        public ResultCode UpdateFrequency(byte maxFreq, byte minFreq)
        {
            return (ResultCode)RWDev.Writedfre(ref address, ref maxFreq, ref minFreq);
        }         
    }
}