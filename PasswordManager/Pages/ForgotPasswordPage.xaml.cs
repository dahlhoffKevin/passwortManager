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
using EncryptionHelper;
using System.IO;

namespace PasswordManager
{
    public partial class ForgotPasswordPage : Page
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
        }
        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("Pages/LoginPage.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }
        private void btn_reset_password_Click(object sender, RoutedEventArgs e)
        {
            string oldMasterPassword = txtOldMasterPassword.Password;
            string newMasterPassword = txtNewMasterPassword.Password;
            string newMasterPasswordRepeate = txtNewMasterPasswordRepeate.Password;

            // path to user data folder
            string userdata_folder_path = System.IO.Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
            @"PasswordManager\userdata\";

            // name of the file which contains the master password
            string masterpassword_file = "master_password.yaml";

            string userdata_folder = System.IO.Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
                    @"PasswordManager\userdata\"; // C:\PasswordManager\userdata\

            if (oldMasterPassword == "" || newMasterPassword == "" || newMasterPasswordRepeate == "")
            {
                MessageBox.Show("All Fields Musst Been Filled In!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // encrypted master password from master password file
            string encrypted_master_password = "";
            try
            {
                encrypted_master_password = System.IO.File.ReadAllText(userdata_folder_path + masterpassword_file);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could Not Open Master Password File:\n" + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // the encrypted master password from file encrypted
            string decrypted_master_password = "";
            try
            {
                decrypted_master_password = EncryptionHelper.EncryptionHelper.Decrypt(encrypted_master_password);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could Not Decrypt Master Password:\n" + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (oldMasterPassword != decrypted_master_password)
            {
                MessageBox.Show("Incorrect Old Master Password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (newMasterPassword != newMasterPasswordRepeate)
            {
                MessageBox.Show("New Passwords Are Not Matching!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                string new_encrypted_master_password = EncryptionHelper.EncryptionHelper.Encrypt(newMasterPassword);
                using StreamWriter file = new(userdata_folder + masterpassword_file, append: false);

                file.WriteLine(new_encrypted_master_password + " test223342323244534q");
                file.Close();
                MessageBox.Show("Master Password Successfully Reseted", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                
            } catch (Exception ex)
            {
                MessageBox.Show("Ops. An Error Occurred:\n" + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }
    }
}
