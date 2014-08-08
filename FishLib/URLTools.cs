using System;
using System.Linq;
using System.Text.RegularExpressions;
using FishLib;

namespace FishLib
{
    public class URLTools
    {
        public string original_url { get; private set; }
        public string host;
        public string firstleveldomain;
        public string secondLevelDomain;
        public string http;
        public string sourceUrl;

        public URLTools(string _sourceUrl)
        {
            this.sourceUrl = _sourceUrl;
            this.original_url = _sourceUrl.ToLower();

            this.host = this.getHostFromInputUrl(original_url);


            if (host != null)
            {
                //ещё одна доп. проверка на правильность значения host - вытаскиваем регэкспом хост
                Regex hostRegex = new Regex(@"([a-zA-Z0-9]([a-zA-Z0-9\-]{0,61}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,6}");
                host = hostRegex.Match(host).Value;

                host = host.ToLower().Replace("www.", "").Trim();
                this.firstleveldomain = this.getFirstLevelDomain(host);
                this.secondLevelDomain = this.getSecondLevelDomain(host);
                this.http = getHttpLink(host);
            }

            /* избавляемся от http:// в original_url уже после определения хоста,
             * т.к. хост извлекается нкорректно только из полной ссылки с http://
             */
            this.original_url = this.original_url.Replace(@"http://", "").Replace(@"https://", "");
        }

        private string getSecondLevelDomain(string domain)
        {
            string[] splittedDomain = domain.Split('.');
            if (splittedDomain.Count() < 2)
            {
                return null;
            }
            else
            {
                string secondLevelDomain = splittedDomain[splittedDomain.Count() - 2] + "." + splittedDomain[splittedDomain.Count() - 1];
                return secondLevelDomain;
            }
        }

        private string getFirstLevelDomain(string domain)
        {
            string[] splittedDomain = domain.Split('.');
            if (splittedDomain.Count() < 2)
            {
                return null;
            }
            else
            {
                string secondLevelDomain = splittedDomain[splittedDomain.Count() - 1];
                return secondLevelDomain;
            }
        }

        private string getHostFromInputUrl(string inputUrl)
        {

            string httpUrlPattern = @"((http|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)";
            string withoutHttpUrlPattern = @"([\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)";

            Regex httpUrl = new Regex(httpUrlPattern, RegexOptions.IgnoreCase);
            Regex withoutHttpUrl = new Regex(withoutHttpUrlPattern, RegexOptions.IgnoreCase);



            if (httpUrl.IsMatch(inputUrl))
            {
                Uri uri = new Uri(httpUrl.Match(inputUrl).Value);
                return uri.Host;

            }
            else if (withoutHttpUrl.IsMatch(inputUrl))
            {
                return withoutHttpUrl.Match(inputUrl).Value.Replace("/", "");
            }
            else
            {
                return null;
            }
        }

        private string getHttpLink(string host)
        {
            return string.Format("http://{0}", host);
        }

    }
}
