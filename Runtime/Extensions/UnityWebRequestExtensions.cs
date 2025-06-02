using System.Collections.Generic;

namespace UnityEngine.Networking
{
    public static class UnityWebRequestExtensions
    {
        public static void SetRequestHeaders(this UnityWebRequest request, IDictionary<string, string> headers)
        {
            foreach (var header in headers)
                request.SetRequestHeader(header.Key, header.Value);
        }

        public static bool IsHttpError(this UnityWebRequest request)
        {
            return request.responseCode is < 200 or >= 300;
        }

        public static bool IsNetworkError(this UnityWebRequest request)
        {
            return request.result == UnityWebRequest.Result.ConnectionError;
        }
        
        public static string MakeHttpStatusCodeString(this UnityWebRequest request)
        {
            var code = request.responseCode;
            if (code is < 100 or > 599)
                return code.ToString();

            var httpStatusCode = (System.Net.HttpStatusCode)request.responseCode;
            return httpStatusCode.ToString();
        }
        
        public static string MakeStatusCodeString(this UnityWebRequest request)
        {
            return $"{request.responseCode} {request.MakeHttpStatusCodeString()}";
        }

        public static bool IsErrorExist(this UnityWebRequest request)
        {
            return request.IsNetworkError() || request.IsHttpError();
        }
        
        public static string GetExtErrorString(this UnityWebRequest request)
        {
            if (request.IsNetworkError())
            {
                return request.error ?? request.MakeStatusCodeString();
            }

            if (request.IsHttpError())
            {
                return request.MakeStatusCodeString();
            }

            return "NO ERROR";
        }
    }
}