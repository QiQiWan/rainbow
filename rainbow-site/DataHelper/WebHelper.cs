using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System;

namespace rainbow_site
{
    public class WebHelper
    {
        /// <summary>
        /// 请求指定的地址，获取返回的字符串数据
        /// </summary>
        /// <returns></returns>
        public static string Request(string url)
        {
            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            return ReadWebStream(response.GetResponseStream());
        }

        public static string Post(string url,
            Dictionary<string, string> param,
            Dictionary<string, string> header){
                string paramStr = JsonConvert.SerializeObject(param);
                return Post(url, param, header);
            }
        public static string Post(string url,
            string param,
            Dictionary<string, string> header)
        {
            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            foreach (string key in header.Keys)
            {
                request.Headers.Add(key, header[key]);
            }
            request.ContentType = "application/json;charset=utf8";
            Console.WriteLine(param);
            byte[] databuff = Encoding.UTF8.GetBytes(param);
            request.ContentLength = databuff.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(databuff, 0, databuff.Length);
            }

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            return ReadWebStream(response.GetResponseStream());
        }

        private static string ReadWebStream(Stream stream)
        {
            string result;
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
                stream.Close();
                reader.Close();
            }
            return result;
        }
    }
}
