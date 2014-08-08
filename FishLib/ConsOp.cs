using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishLib
{
    public static class ConsOp
    {

        static ConsOp()
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public static void initConsOp(int consoleWidth, string title)
        {
            Console.WindowWidth = consoleWidth;
            Console.Title = title;
        }

        private static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public static void log(string txt, ConsoleColor fcolor = ConsoleColor.Green)
        {
            Console.ForegroundColor = fcolor;
            string log = string.Format("{0}: {1}", DateTime.Now.ToString(), txt);
            Console.WriteLine(log);
        }

        public static void logFile(string txt, string path)
        {
            string log = string.Format("{0}: {1}", DateTime.Now.ToString(), txt);
            File.AppendAllText(path, log + Environment.NewLine);
        }

        public static void readLine()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string command = Console.ReadLine().ToLower();
            switch (command)
            {
                case "exit":
                    Environment.Exit(0);
                    break;
                case "":
                    break;
                default:
                    Console.WriteLine(string.Format("Неизвестная команда «{0}».", command));
                    break;
            }

            readLine();
        }

    }
}
