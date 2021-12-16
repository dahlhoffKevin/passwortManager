using System;
using System.IO;
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
using System.Windows.Navigation;
using PasswordManager;
using PasswordManager.EncryptionHelper;

namespace PasswordManager
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
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
        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Window loginWindow = new LoginWindow();
            Close();
            loginWindow.Show();
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

                    string encrypted_master_password = EncryptionHelper.EncryptionHelper.Encrypt(entry_master_password.ToString());

                    file.WriteLine(encrypted_master_password);
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
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                File.Delete(userdata_folder + masterpassword_file);
            }

        }
    }
}
