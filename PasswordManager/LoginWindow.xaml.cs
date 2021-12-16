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
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using PasswordManager.EncryptionHelper;

namespace PasswordManager
{
    public partial class LoginWindow : Window
    {   
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            string userdata_folder_path = System.IO.Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
            @"PasswordManager\userdata\";
            string masterpassword_file = 
                "master_password.yaml";
            string master_password_entry = 
                txtMasterPassword.Password.ToString();
            string decrypted_master_password = 
                EncryptionHelper.EncryptionHelper.Decrypt(master_password_entry);
            string encrypted_master_password = 
                System.IO.File.ReadAllText(userdata_folder_path + masterpassword_file);
            string decrypted_master_password_in_file = 
                EncryptionHelper.EncryptionHelper.Decrypt(userdata_folder_path + masterpassword_file);

            if (encrypted_master_password == decrypted_master_password_in_file)
            {
                MessageBox.Show("Passwords Matching", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void btn_signup_Click(object sender, RoutedEventArgs e)
        {
            Window registerWindow = new RegisterWindow();
            Close();
            registerWindow.Show();
        }
    }
}
