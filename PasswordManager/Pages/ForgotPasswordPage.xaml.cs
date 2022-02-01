using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.IO;
using System.Configuration;

namespace PasswordManager
{
    public partial class ForgotPasswordPage : Page
    {
        #pragma warning disable CS8602
        public string userdata_folder = Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
                ConfigurationManager.AppSettings["userdata_folder_path"].ToString();

        #pragma warning disable CS8602
        public string app_name = ConfigurationManager.AppSettings["app_name"].ToString();

        public ForgotPasswordPage()
        {
            InitializeComponent();
        }
        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("Pages/LoginPage.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
            Logger.WriteLog("Switched Back To Login Page", "INFO");
        }
        private void btn_reset_password_Click(object sender, RoutedEventArgs e)
        {
            string oldMasterPassword = txtOldMasterPassword.Password;
            string newMasterPassword = txtNewMasterPassword.Password;
            string newMasterPasswordRepeate = txtNewMasterPasswordRepeate.Password;

            #pragma warning disable CS8602
            // name of the file which contains the master password
            string masterpassword_file = ConfigurationManager.AppSettings["master_password_file"].ToString() + 
                                         ConfigurationManager.AppSettings["master_password_file_ending"].ToString();

            if (oldMasterPassword == "" || newMasterPassword == "" || newMasterPasswordRepeate == "")
            {
                MessageBox.Show("All Fields Musst Been Filled In!", app_name, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // encrypted master password from master password file
            string encrypted_master_password;
            try
            {
                encrypted_master_password = File.ReadAllText(userdata_folder + masterpassword_file);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could Not Open Master Password File\nSee The Logs For More Information", app_name, MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.WriteLog(ex.ToString(), "ERROR");
                return;
            }

            // the encrypted master password from file encrypted
            string decrypted_master_password;
            try
            {
                decrypted_master_password = EncryptionHelper.EncryptionHelper.Decrypt(encrypted_master_password);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could Not Decrypt Master Password\nSee The Logs For More Information", app_name, MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.WriteLog(ex.ToString(), "ERROR");
                return;
            }

            if (oldMasterPassword != decrypted_master_password)
            {
                MessageBox.Show("Incorrect Old Master Password!", app_name, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (newMasterPassword != newMasterPasswordRepeate)
            {
                MessageBox.Show("New Passwords Are Not Matching!", app_name, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                string new_encrypted_master_password = EncryptionHelper.EncryptionHelper.Encrypt(newMasterPassword);
                using StreamWriter file = new(userdata_folder + masterpassword_file, append: false);

                file.WriteLine(new_encrypted_master_password);
                file.Close();
                MessageBox.Show("Master Password Successfully Reseted", app_name, MessageBoxButton.OK, MessageBoxImage.Information);
                
            } catch (Exception ex)
            {
                MessageBox.Show("Ops. An Error Occurred\nSee The Logs For More Information", app_name, MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.WriteLog(ex.ToString(), "ERROR");
                return;
            }

        }
    }
}
