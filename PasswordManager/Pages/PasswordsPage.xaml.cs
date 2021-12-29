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
    public class PasswordItem
    {
        public string? Use { get; set; }

        public string? Password { get; set; }
    }
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
                    string use = "";
                    string password = "";
                    while ((Line = sr.ReadLine()) != null)
                    {
                        string tmp = Line;

                        if (tmp.Substring(tmp.Length - 2) == "==" || tmp.Substring(tmp.Length - 1) == "=")
                        {
                            string decrypted_string = EncryptionHelper.EncryptionHelper.Decrypt(tmp);
                            password = decrypted_string;
                        }
                        else
                        {
                            use = Line;
                        }
                    }
                    ListViewPasswords.Items.Add(new PasswordItem { Use = use, Password = password });
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

                    // reloads the site
                    NavigationService.Refresh();
                }
            }
            else
            {
                return;
            }
        }
        private void btn_update_list(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Refresh();
        }
    }
}
