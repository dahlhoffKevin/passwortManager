using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.IO;
using System.Configuration;
using PasswordManagerLogger;

namespace PasswordManager.Pages
{
    public partial class RegisterPage : Page
    {
        #pragma warning disable CS8602
        public string pwd_folder = Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
                ConfigurationManager.AppSettings["pwd_folder_path"].ToString();

        #pragma warning disable CS8602
        public string pwd_file_ending = ConfigurationManager.AppSettings["pwd_file_ending"].ToString();

        #pragma warning disable CS8602
        public string app_name = ConfigurationManager.AppSettings["app_name"].ToString();

        #pragma warning disable CS8602
        public string userdata_folder = Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) + 
            ConfigurationManager.AppSettings["userdata_folder_path"].ToString();
        public RegisterPage()
        {
            InitializeComponent();
        }
        private void btn_register_Click(object sender, RoutedEventArgs e)
        {
            string entry_master_password = txtMasterPassword.Password.ToString();
            string entry_master_password_repeat = txtMasterPasswordRepeat.Password.ToString();
            string userdata_folder = Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
                    @"PasswordManager\userdata\";
            string masterpassword_file = ConfigurationManager.AppSettings["master_password_file"].ToString() + 
                pwd_file_ending;

            if (!(entry_master_password == entry_master_password_repeat))
            {
                MessageBox.Show("Passwords not machting!", app_name, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                if (!File.Exists(userdata_folder + masterpassword_file))
                {
                    File.Create(userdata_folder + masterpassword_file).Dispose();
                    using StreamWriter file = new(userdata_folder + masterpassword_file, append: true);

                    string encrypted_master_password = EncryptionHelper.EncryptionHelper.Encrypt(entry_master_password);

                    file.WriteLine(encrypted_master_password);
                    file.Close();
                    MessageBox.Show("Master Password created", app_name, MessageBoxButton.OK, MessageBoxImage.Information);

                    txtMasterPassword.Password = "";
                    txtMasterPasswordRepeat.Password = "";
                }
                else
                {
                    MessageBox.Show("Master Password already exists!", app_name, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ups. An Error occured:\n" + ex, app_name, MessageBoxButton.OK, MessageBoxImage.Error);
                File.Delete(userdata_folder + masterpassword_file);
            }

        }
        private void btn_back_Click(object sender, RoutedEventArgs e)
        { 
            Uri uri = new Uri("Pages/LoginPage.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
            Logger.WriteLog("Switched Back To Login Page", "INFO");
        }
    }
}
