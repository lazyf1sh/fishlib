﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishLib
{
    public class FilePaths
    {
        private static string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static string Desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public static string AppDataMGT
        {
            get
            {
                return AppData + @"\MGT\";
            }
            private set
            {
                ;
            }

        }

        public static string Mgt3Sqlite
        {
            get
            {
                return AppDataMGT + @"mgt3.sqlite";
            }
            private set
            {
                ;
            }
        }
    }
}
