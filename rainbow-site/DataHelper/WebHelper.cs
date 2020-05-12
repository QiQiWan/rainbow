using System.Net;
using System.IO;

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
        private static string ReadWebStream(Stream stream) {
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
