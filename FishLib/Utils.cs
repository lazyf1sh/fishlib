using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FishLib
{
    public static class Utils
    {

        public static string StripHTML(string inputString)
        {
            string HTML_TAG_PATTERN = "<.*?>";
            return Regex.Replace(inputString, HTML_TAG_PATTERN, string.Empty);
        }

        public static string StripHTMLBySpace(string inputString)
        {
            string HTML_TAG_PATTERN = "<.*?>";
            return Regex.Replace(inputString, HTML_TAG_PATTERN, " ");
        }

        public static void removeDuplicateLinesInTxt(string path)
        {
            string[] lines = File.ReadAllLines(path);
            File.WriteAllLines(path, lines.Distinct().ToArray());
        }

        public static List<string> getEnuqueEmailsFromTxt(string html)
        {
            List<string> emails = new List<string>();

            RegexOptions regex_options = RegexOptions.Multiline | RegexOptions.IgnoreCase;
            Regex regex_email = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", regex_options);
            MatchCollection emails_found = regex_email.Matches(html);
            foreach (Match email_found in emails_found)
            {
                string email = email_found.Value.ToLower();
                if (!emails.Contains(email))
                {
                    emails.Add(email);
                }
            }
            return emails;
        }

        public static bool isAppExpirated(DateTime expirationDate)
        {
            if (expirationDate < DateTime.Now)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
