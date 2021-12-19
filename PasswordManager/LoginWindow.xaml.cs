using System;
using System.Windows;
using System.Windows.Input;

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
            } catch
            {
                MessageBox.Show("Master Password Not Set", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // the encrypted master password from file encrypted
            string decrypted_master_password_in_file = "";
            try
            {
                decrypted_master_password_in_file = EncryptionHelper.EncryptionHelper.Decrypt(encrypted_master_password);
            } catch
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
            MessageBox.Show("Passwords Matching", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void btn_signup_Click(object sender, RoutedEventArgs e)
        {
            Window registerWindow = new RegisterWindow();
            Close();
            registerWindow.Show();
        }
    }
}
