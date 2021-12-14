using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace PasswordManager
{
    public partial class App : Application
    {
        String main_folder_path = Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) + 
            "PasswordManager";
        String passwords_folder_path = Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) + 
            @"PasswordManager\Passwords";

        void startup_check_files(object sender, StartupEventArgs e)
        {
            try
            {
                if (!Directory.Exists(main_folder_path))
                {
                    Directory.CreateDirectory(main_folder_path);
                    Directory.CreateDirectory(passwords_folder_path);
                    File.Create(main_folder_path + @"\config.yaml");
                }
                else if (!Directory.Exists(passwords_folder_path))
                {
                    Directory.CreateDirectory(passwords_folder_path);
                }
            } 
            catch (Exception ex)
            {
                Current.Shutdown();
                Console.WriteLine("an error occured: " + ex.ToString());
            }
        }

    }
}
