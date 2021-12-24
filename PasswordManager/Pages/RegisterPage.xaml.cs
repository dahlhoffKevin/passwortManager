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
using System.IO;

namespace PasswordManager.Pages
{
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }
        private void btn_register_Click(object sender, RoutedEventArgs e)
        {
            string entry_master_password = txtMasterPassword.Password.ToString();
            string entry_master_password_repeat = txtMasterPasswordRepeat.Password.ToString();
            string userdata_folder = System.IO.Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
                    @"PasswordManager\userdata\"; // C:\PasswordManager\userdata\
            string masterpassword_file = "master_password.yaml";

            if (!(entry_master_password == entry_master_password_repeat))
            {
                MessageBox.Show("Passwords not machting!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    MessageBox.Show("Master Password created", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                    txtMasterPassword.Password = "";
                    txtMasterPasswordRepeat.Password = "";
                }
                else
                {
                    MessageBox.Show("Master Password already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ups. An Error occured:\n" + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                File.Delete(userdata_folder + masterpassword_file);
            }

        }
        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("Pages/LoginPage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }
    }
}
