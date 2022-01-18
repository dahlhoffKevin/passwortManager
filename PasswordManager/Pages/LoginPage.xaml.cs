using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PasswordManager;

namespace PasswordManager.Pages
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            // path to user data folder
            string userdata_folder_path = System.IO.Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
            @"PasswordManager\userdata\";

            // name of the file which contains the master password
            string masterpassword_file = "master_password.yaml";

            // the original master password that was enterd
            string master_password_entry = txtMasterPassword.Password.ToString();

            // encrypted master password from master password file
            string encrypted_master_password = "";
            try
            {
                encrypted_master_password = System.IO.File.ReadAllText(userdata_folder_path + masterpassword_file);
            }
            catch
            {
                MessageBox.Show("Master Password Not Set", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // the encrypted master password from file encrypted
            string decrypted_master_password_in_file = "";
            try
            {
                decrypted_master_password_in_file = EncryptionHelper.EncryptionHelper.Decrypt(encrypted_master_password);
            }
            catch
            {
                MessageBox.Show("Could Not Decrypt Master Password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // checks if the passwords are machting
            if (master_password_entry == "")
            {
                MessageBox.Show("All Fields Must Been Filled In", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("Passwords Not Matching", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Window mainWindow = new MainWindow();
            mainWindow.Show();
        }
        private void btn_signup_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("Pages/RegisterPage.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }

        private void btn_forgot_password_Click(object sender, RoutedEventArgs e)
        {
            // path to user data folder
            string master_password_file_path = System.IO.Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
            @"PasswordManager\userdata\master_password.yaml";
            
            if (System.IO.File.Exists(master_password_file_path))
            {
                Uri uri = new Uri("Pages/ForgotPasswordPage.xaml", UriKind.Relative);
                NavigationService.Navigate(uri);
            } else
            {
                MessageBox.Show("No Master Password Created!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }
    }
}
