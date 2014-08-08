using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FishLib
{
    public class Browser
    {
        private bool useProxy;
        private bool attachCookieContainer;
        private bool useCredentials;
        private CookieContainer cookies = new CookieContainer();
        public Browser(bool _useProxy = false, bool _attachCookieContainer = false, bool _useCredentials = false)
        {
            this.useProxy = _useProxy;
            this.attachCookieContainer = _attachCookieContainer;
            this.useCredentials = _useCredentials;
        }

        public void addCookie(string cookieName, string value, string path, string domain)
        {
            Cookie cookie = new Cookie(cookieName, value, path, domain);
            cookies.Add(cookie);
        }

        public string navigate(string URL, string proxyAddr = "127.0.0.1", int proxyPort = 8118)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);

            if (useProxy)
            {
                request.Proxy = new WebProxy(proxyAddr, proxyPort);
            }

            if (attachCookieContainer)
            {
                request.CookieContainer = cookies;
            }

                    if (useCredentials)
	        {
		         request.Credentials = new NetworkCredential("i.kopylov", "06dF8c2b@963852");
	        }

            


            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:27.0) Gecko/20100101 Firefox/27.0";
            HttpWebResponse result = (HttpWebResponse)request.GetResponse();
            Stream ReceiveStream = result.GetResponseStream();
            //StreamReader sr1 = new StreamReader(ReceiveStream, Encoding.UTF8);
            StreamReader sr1 = new StreamReader(ReceiveStream, Encoding.UTF8);
            string html = sr1.ReadToEnd();
            result.Close();
            return html;
        }

        public async Task<string> NavigateAsync(string URL, string proxyAddr = "127.0.0.1", int proxyPort = 8118)
        {
            return await Task<string>.Factory.StartNew(() =>
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);

                if (useProxy)
                {
                    request.Proxy = new WebProxy(proxyAddr, proxyPort);
                }

                if (attachCookieContainer)
                {
                    request.CookieContainer = cookies;
                }


                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:27.0) Gecko/20100101 Firefox/27.0";
                HttpWebResponse result = (HttpWebResponse)request.GetResponse();
                Stream ReceiveStream = result.GetResponseStream();
                //StreamReader sr1 = new StreamReader(ReceiveStream, Encoding.UTF8);
                StreamReader sr1 = new StreamReader(ReceiveStream, Encoding.UTF8);
                string html = sr1.ReadToEnd();
                result.Close();
                return html;
            });
        }

        public async Task<string> DoPostAsync(string Url, string Data, string proxyAddr = "127.0.0.1", int proxyPort = 8118)
        {
            return await Task<string>.Factory.StartNew(() =>
            {
                var request = (HttpWebRequest)WebRequest.Create(Url);
                request.AllowAutoRedirect = false;

                if (useProxy)
                {
                    request.Proxy = new WebProxy(proxyAddr, proxyPort);
                }

                if (attachCookieContainer)
                {
                    request.CookieContainer = cookies;
                }

                request.Method = "POST";
                request.Timeout = 5000;
                request.ContentType = "application/x-www-form-urlencoded";
                byte[] sentData = Encoding.GetEncoding(1251).GetBytes(Data);
                request.ContentLength = sentData.Length;
                Stream sendStream = request.GetRequestStream();

                sendStream.Write(sentData, 0, sentData.Length);
                sendStream.Close();
                WebResponse res = request.GetResponse();

                Stream ReceiveStream = res.GetResponseStream();
                StreamReader sr = new StreamReader(ReceiveStream, Encoding.UTF8);
                //Кодировка указывается в зависимости от кодировки ответа сервера
                char[] read = new char[256];
                int count = sr.Read(read, 0, 256);
                string _out = string.Empty;
                while (count > 0)
                {
                    string str = new string(read, 0, count);
                    _out += str;
                    count = sr.Read(read, 0, 256);
                }
                return _out;
            });
        }

        public string DoPost(string Url, string Data, string proxyAddr = "127.0.0.1", int proxyPort = 8118)
        {
            var request = (HttpWebRequest)WebRequest.Create(Url);
            request.AllowAutoRedirect = false;

            if (useProxy)
            {
                request.Proxy = new WebProxy(proxyAddr, proxyPort);
            }

            if (attachCookieContainer)
            {
                request.CookieContainer = cookies;
            }

            request.Method = "POST";
            request.Timeout = 5000;
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] sentData = Encoding.GetEncoding(1251).GetBytes(Data);
            request.ContentLength = sentData.Length;
            Stream sendStream = request.GetRequestStream();

            sendStream.Write(sentData, 0, sentData.Length);
            sendStream.Close();
            WebResponse res = request.GetResponse();

            Stream ReceiveStream = res.GetResponseStream();
            StreamReader sr = new StreamReader(ReceiveStream, Encoding.UTF8);
            //Кодировка указывается в зависимости от кодировки ответа сервера
            char[] read = new char[256];
            int count = sr.Read(read, 0, 256);
            string _out = string.Empty;
            while (count > 0)
            {
                string str = new string(read, 0, count);
                _out += str;
                count = sr.Read(read, 0, 256);
            }
            return _out;
        }
    }
}
