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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PasswordManager.Pages
{
    public partial class PasswordsPage : Page
    {
        public PasswordsPage()
        {
            InitializeComponent();
            display_passwords();
        }
        private void btn_create_password(object sender, RoutedEventArgs e)
        {
            Window addPasswordWindow = new addPassword();
            addPasswordWindow.Show();
        }
        private void display_passwords()
        {
            string password_folder = System.IO.Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
                    @"PasswordManager\userdata\passwords\"; // C:\PasswordManager\userdata\

            foreach (string filename in Directory.GetFiles(password_folder))
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    string Line;
                    while ((Line = sr.ReadLine()) != null)
                    {
                        String tmp = Line;

                        if (tmp.Substring(tmp.Length - 2) == "==")
                        {
                            String decrypted_string = EncryptionHelper.EncryptionHelper.Decrypt(tmp);
                            txtPasswords.Text += "\nPassword: " + decrypted_string;
                        }
                        else
                        {
                            txtPasswords.Text += "\n";
                            txtPasswords.Text += "Password Use: " + Line + "\n";
                        }
                    }
                }
            }
        }

        private void btn_remove_all_passwords_click(object sender, RoutedEventArgs e)
        {
            string password_folder = System.IO.Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
                    @"PasswordManager\userdata\passwords\"; // C:\PasswordManager\userdata\

            if (MessageBox.Show("Do You Really Want To Delete All Your Passwords?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (string filename in Directory.GetFiles(password_folder))
                {
                    File.Delete(filename);
                }
            }
            else
            {
                return;
            }
        }
    }
}
