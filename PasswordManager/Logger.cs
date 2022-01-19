using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PasswordManager
{
    public static class Logger
    {
        public static void WriteLog(string message, string status)
        {
            string date = DateTime.Today.ToString("d");
            string log_file_path = Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
                    @"PasswordManager\logs\log_" + date + ".txt";

            using (StreamWriter sw = new StreamWriter(log_file_path, true))
            {
                if (status == "ERROR")
                {
                    sw.WriteLine($"[ERROR] - {DateTime.Now} : {message}");
                    sw.Close();
                }
                if (status == "WARNING")
                {
                    sw.WriteLine($"[WARNING] - {DateTime.Now} : {message}");
                    sw.Close();
                }
                if (status == "INFO")
                {
                    sw.WriteLine($"[INFO] - {DateTime.Now} : {message}");
                    sw.Close();
                }
            }
        }
    }
}
