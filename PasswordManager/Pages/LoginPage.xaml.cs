using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Configuration;
using System.IO;
using PasswordManagerLogger;

namespace PasswordManager.Pages
{
    public partial class LoginPage : Page
    {
        #pragma warning disable CS8602
        string masterpassword_file_path = Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) + 
                                          ConfigurationManager.AppSettings["pwd_manager_folder"].ToString() +
                                          ConfigurationManager.AppSettings["userdata_folder_path"].ToString() +
                                          ConfigurationManager.AppSettings["master_password_file"].ToString() +
                                          ConfigurationManager.AppSettings["pwd_file_ending"].ToString();

        string masterpassword_file = ConfigurationManager.AppSettings["master_password_file"].ToString() +
                                     ConfigurationManager.AppSettings["pwd_file_ending"].ToString();

        string app_name = ConfigurationManager.AppSettings["app_name"].ToString();

        public LoginPage()
        {
            InitializeComponent();
        }
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            // path to user data folder
            string userdata_folder_path = Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
            @"PasswordManager\userdata\";

            // the original master password that was enterd
            string master_password_entry = txtMasterPassword.Password.ToString();

            // encrypted master password from master password file
            string encrypted_master_password = "";
            try
            {
                encrypted_master_password = File.ReadAllText(userdata_folder_path + masterpassword_file);
            }
            catch
            {
                MessageBox.Show("Master Password not set", app_name, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // the encrypted master password from file encrypted
            string decrypted_master_password_in_file = "";
            try
            {
                decrypted_master_password_in_file = EncryptionHelper.EncryptionHelper.Decrypt(encrypted_master_password);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could Not Decrypt Master Password", app_name, MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.WriteLog(ex.ToString(), "ERROR");
                return;
            }

            // checks if the passwords are machting
            if (master_password_entry == "")
            {
                MessageBox.Show("All Fields Must Been Filled In", app_name, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // checks if the file is empty
            if (decrypted_master_password_in_file == "")
            {
                return;
            }

            // checks if the passwords are not matching
            if (!(master_password_entry == decrypted_master_password_in_file))
            {
                MessageBox.Show("Passwords Not Matching", app_name, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Window mainWindow = new MainWindow();
            mainWindow.Show();
            Logger.WriteLog("Switched to Main Window", "INFO");
        }
        private void btn_signup_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("Pages/RegisterPage.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
            Logger.WriteLog("Switched to Register Page", "INFO");
        }

        private void btn_forgot_password_Click(object sender, RoutedEventArgs e)
        {   
            if (File.Exists(masterpassword_file_path))
            {
                Uri uri = new Uri("Pages/ForgotPasswordPage.xaml", UriKind.Relative);
                NavigationService.Navigate(uri);
                Logger.WriteLog("Switched to Forgot Password Page", "INFO");
            } else
            {
                MessageBox.Show("No Master Password Created!\n" + masterpassword_file_path, app_name, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }
    }
}
