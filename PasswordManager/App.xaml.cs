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
                // checks if the main folder exists
                if (!Directory.Exists(main_folder_path))
                {
                    // creates the main folder if it not exists
                    Directory.CreateDirectory(main_folder_path);

                    // creates the passwords folder if it not exists
                    Directory.CreateDirectory(passwords_folder_path);
                }
                else if (!Directory.Exists(passwords_folder_path))
                {
                    // creates the passwords folder if it not exists
                    Directory.CreateDirectory(passwords_folder_path);
                }
            } 
            catch (Exception ex)
            {
                //shutsdown the app if an error occures
                Current.Shutdown();
                Console.WriteLine("an error occured: " + ex.ToString());
            }
        }

    }
}
