using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DL770.Rfid
{
    class RfidJson
    {
        public static string Serialise(RfidSession session)
        {
            //TODO::unescape
            var ret = new StringBuilder();
            ret.Append("{\"time\":\"").Append(session.time).Append("\",");
            ret.Append("\"location\":\"").Append(session.location).Append("\",");
            ret.Append("\"readingStatus\":\"").Append((int)session.readingStatus).Append("\",");
            ret.Append("\"sessionMode\":\"").Append((int)session.sessionMode).Append("\",");
            ret.Append("\"tags\":[");

            for (var i = 0; i < session.tags.Count; i++)
            {
                ret.Append("\"").Append(session.tags[i]).Append("\"");
                if (i + 1 < session.tags.Count)
                {
                    ret.Append(",");
                }
            }

            ret.Append("]");

            ret.Append("}");
            return ret.ToString();
        }

        public static RpcResponse Desialise(string s)
        {
            var response = new RpcResponse();
            var match = new Regex("{\"result\":\"?(.*?)\"?,\"error\":(.*?)}").Match(s);
            if (match.Success)
            {
                if (match.Groups[1].Value == "null")
                    response.Result = null;
                else
                    response.Result = match.Groups[1].Value;

                if (match.Groups[2].Value == "null")
                    response.Status = RpcResponse.StatusCode.Ok;
                else
                {
                    try
                    {
                        response.Status = (RpcResponse.StatusCode)Int32.Parse(match.Groups[2].Value);
                    }
                    catch (Exception e)
                    {
                        response.Result = e.Message;
                        response.Status = RpcResponse.StatusCode.InternalServerError;
                        return response;
                    }
                }
            }

            return response;
        }
    }
}
