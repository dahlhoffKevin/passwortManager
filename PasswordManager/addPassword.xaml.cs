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
using System.IO;

namespace PasswordManager
{
    public partial class addPassword : Window
    {
        public addPassword()
        {
            InitializeComponent();
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private static bool check_folders()
        {
            string userdata_folder = System.IO.Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
                    @"PasswordManager\userdata\"; // C:\PasswordManager\userdata\
            string password_folder = System.IO.Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
                    @"PasswordManager\userdata\passwords"; // C:\PasswordManager\userdata\passwords

            if (!Directory.Exists(userdata_folder))
            {
                return false;
            }

            if (!Directory.Exists(password_folder))
            {
                return false;
            }
            return true;
        }

        private void btn_addpassword(object sender, RoutedEventArgs e)
        {
            String entry_password = txtPassword.Password.ToString();
            String entry_password_confirm = txtPasswordConfirm.Password.ToString();
            String entry_password_use = txt_password_use.Text.ToString();
            string password_folder = System.IO.Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
                    @"PasswordManager\userdata\passwords\"; // C:\PasswordManager\userdata\

            if (entry_password == "" || entry_password_confirm == "" || entry_password_use == "")
            {
                MessageBox.Show("All Fields Must Been Filled In", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!(entry_password == entry_password_confirm))
            {
                MessageBox.Show("Passwords Are Not Machting", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!check_folders())
            {
                MessageBox.Show("Ops! An Error Occured!\nPlease Restart The Application", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!(txtPassword.Password.Length >= 4))
            {
                MessageBox.Show("Your Password Should Not Be Shorter Than 4 Characters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!(txt_password_use.Text.Length > 2))
            {
                MessageBox.Show("The Password Use Is Too Short", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (File.Exists(password_folder + entry_password_use + ".txt"))
                {
                    MessageBox.Show("Password Already Exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Creates a File in Folder Passwords with the name of the entered Password Use
                // Dispose = Closes the Thread where the File is used to make File useable again
                File.Create(password_folder + entry_password_use + ".txt").Dispose();
                using StreamWriter file = new(password_folder + entry_password_use + ".txt", append: true);
                string encrypted_password = EncryptionHelper.EncryptionHelper.Encrypt(entry_password);

                file.WriteLine(encrypted_password + "\n" + entry_password_use);
                file.Close();

                txtPassword.Password = "";
                txtPasswordConfirm.Password = "";
                txt_password_use.Text = "";

                MessageBox.Show("Password Added", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            } catch
            {
                MessageBox.Show("Ops! An Error Occured!\nPlease Restart The Application", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void btn_backhome_click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
