using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.IO;
using System.Configuration;
using PasswordManagerLogger;

namespace PasswordManager.Pages
{
    public partial class addPasswordPage : Page
    {

        // all necessary variables for this segment
        #pragma warning disable CS8602
        public string app_name = ConfigurationManager.AppSettings["app_name"].ToString();

        public string userdata_folder = Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
                    ConfigurationManager.AppSettings["userdata_folder_path"].ToString();
        public string password_folder = Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
                ConfigurationManager.AppSettings["pwd_folder_path"].ToString();
        public string file_ending = ConfigurationManager.AppSettings["pwd_file_ending"].ToString();

        // initializes the "add password" page
        public addPasswordPage()
        {
            InitializeComponent();
        }

        // checks if all necessary folders have been created
        private static bool check_folders()
        {
            if (!Directory.Exists(Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
                ConfigurationManager.AppSettings["pwd_folder_path"].ToString()))
            {
                return false;
            }
            if (!Directory.Exists(Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
                    ConfigurationManager.AppSettings["pwd_manager_folder"].ToString() +
                    ConfigurationManager.AppSettings["userdata_folder_path"].ToString()))
            {
                return false;
            }
            return true;
        }

        // main function for "add password" page
        private void btn_addpassword(object sender, RoutedEventArgs e)
        {
            string entry_password = txtPassword.Password.ToString();
            string entry_password_confirm = txtPasswordConfirm.Password.ToString();
            string entry_password_use = txt_password_use.Text.ToString();
            string entry_password_url = txt_password_url.Text.ToString();

            // checks if all necessary fields are filled in
            if (entry_password == "" || entry_password_confirm == "" || entry_password_use == "")
            {
                MessageBox.Show("All fields must been filled in", app_name, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // checks if the entered passwords are the same
            if (!(entry_password == entry_password_confirm))
            {
                MessageBox.Show("Your passwords are not machting", app_name, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // calls the check_folders methode in order to confirm that all folders were created
            if (!check_folders())
            {
                MessageBox.Show("Ops! An error occured!\nPlease restart the application\nSee the logs for more information", app_name, MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.WriteLog("Not all folders were created", "ERROR");
                return;
            }

            // checks if the entered password has a minimum length of 4 characters
            if (!(txtPassword.Password.Length >= 4))
            {
                MessageBox.Show("Your password should not be shorter than 4 characters", app_name, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // checks if the entered password use has a minimum length of 2 characters
            if (!(txt_password_use.Text.Length > 2))
            {
                MessageBox.Show("The password use is too short", app_name, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // checks if the entered password already exists
                if (File.Exists(password_folder + entry_password_use + file_ending))
                {
                    MessageBox.Show("Password does already exists", app_name, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (entry_password_url == "")
                {
                    File.Create(password_folder + entry_password_use + file_ending).Dispose();
                    using StreamWriter file = new(password_folder + entry_password_use + ConfigurationManager.AppSettings["pwd_file_ending"].ToString(), append: true);
                    string encrypted_password = EncryptionHelper.EncryptionHelper.Encrypt(entry_password, false);
                    file.WriteLine(encrypted_password + "\n" + entry_password_use);
                    file.Close();
                }
                else
                {
                    // will be executed if the entered password url equals empty string
                    try
                    {
                        // checks if the entered url start with https oder http
                        if (!(entry_password_url.Substring(0, 5) == "https" || entry_password_url.Substring(0, 4) == "http"))
                        {
                            MessageBox.Show("The entered URL must start with 'https' or 'http'!", app_name, MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    } catch
                    {
                        MessageBox.Show("The entered URL must start with 'https' or 'http'!", app_name, MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // creates a file with the passwort as name to save the entered informations in it
                    File.Create(password_folder + entry_password_use + file_ending).Dispose();
                    using StreamWriter file = new(password_folder + entry_password_use + file_ending, append: true);
                    string encrypted_password = EncryptionHelper.EncryptionHelper.Encrypt(entry_password, false);
                    file.WriteLine(encrypted_password + "\n" + entry_password_use + "\n" + entry_password_url);
                    file.Close();

                }

                txtPassword.Password = "";
                txtPasswordConfirm.Password = "";
                txt_password_use.Text = "";
                txt_password_url.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ops! An error occured!\nPlease restart the application\nSee the logs for more information", app_name, MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.WriteLog(ex.ToString(), "ERROR");
                return;
            }
        }

        // navigates to home page
        private void btn_backhome_click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("Pages/PasswordsPage.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }
    }
}
