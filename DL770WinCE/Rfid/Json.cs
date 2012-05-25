using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using CodeBetter.Json;

namespace DL770.Rfid
{
    class RfidJson
    {
        public static RpcResponse Deserialize(string s)
        {
            var response = new RpcResponse();
            var match = new Regex("{\"result\":\"?(.*?)\"?,\"error\":(.*?)}").Match(s);
            if (match.Success)
            {
                if (match.Groups[1].Value == "null")
                    response.result = null;
                else
                    response.result = match.Groups[1].Value;

                if (match.Groups[2].Value == "null")
                    response.status = RpcResponse.StatusCode.Ok;
                else
                {
                    try
                    {
                        response.status = (RpcResponse.StatusCode)Int32.Parse(match.Groups[2].Value);
                    }
                    catch (Exception e)
                    {
                        response.result = e.Message;
                        response.status = RpcResponse.StatusCode.InternalServerError;
                        return response;
                    }
                }
            }

            return response;
        }
    }
}
