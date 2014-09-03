using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishLib
{
    public class SupportCookies
    {
        public static string K4Cookie
        {
            get
            {
                return File.ReadAllLines(@"X:\db\cookies.txt")[1].Split('|')[0];
            }
            private set
            {
                ;
            }
        }

        public static string WfPvtGmtool
        {
            get
            {
                return File.ReadAllLines(@"X:\db\cookies.txt")[0].Split('|')[0];
            }
            private set
            {
                ;
            }
        }
    }
}
