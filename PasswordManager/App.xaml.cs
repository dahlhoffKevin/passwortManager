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
        string main_folder_path = Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
            "PasswordManager";
        string userdata_folder_path = Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
            @"PasswordManager\userdata\";
        string passwords_folder_path = Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
            @"PasswordManager\userdata\passwords\";
        string logs_folder_path = Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
            @"PasswordManager\logs\";
        string log_file_path = Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
            @"PasswordManager\logs\log_" + DateTime.Today.ToString("d") + ".txt";

        void startup_check_folders_and_files(object sender, StartupEventArgs e)
        {
            // Checking all Files
            try
            {
                if (File.Exists(log_file_path))
                {
                    return;
                }
                var log_file = File.Create(log_file_path);
                log_file.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ops An Error Occured:\n{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Current.Shutdown();
            }


            /**
             * This Method checks if all neccessary Folders and Files exists
             */

            // Checking all Folders
            try
            {
                if (!Directory.Exists(main_folder_path))
                {
                    Directory.CreateDirectory(main_folder_path);
                    Directory.CreateDirectory(userdata_folder_path);
                    Directory.CreateDirectory(passwords_folder_path);
                    Directory.CreateDirectory(logs_folder_path);
                }
                else if (!Directory.Exists(userdata_folder_path))
                {
                    Directory.CreateDirectory(userdata_folder_path);
                }
                else if (!Directory.Exists(passwords_folder_path))
                {
                    Directory.CreateDirectory(passwords_folder_path);
                }
                else if (!Directory.Exists(logs_folder_path))
                {
                    Directory.CreateDirectory(logs_folder_path);
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
