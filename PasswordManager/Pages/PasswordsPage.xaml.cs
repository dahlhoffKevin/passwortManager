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
        public string? URL { get; set; }
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
            Uri uri = new Uri("Pages/addPasswordPage.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }
        private void display_passwords()
        {
            string password_folder = System.IO.Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
                    @"PasswordManager\userdata\passwords\"; // C:\PasswordManager\userdata\

            foreach (string filename in Directory.GetFiles(password_folder))
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    string? Line;
                    string use = "";
                    string password = "";
                    string url = "";
                    while ((Line = sr.ReadLine()) != null)
                    {
                        string tmp = Line;

                        if (tmp.Substring(tmp.Length - 2) == "==" || tmp.Substring(tmp.Length - 1) == "=")
                        {   
                            /**
                            string decrypted_string = EncryptionHelper.EncryptionHelper.Decrypt(tmp);
                            password = decrypted_string;
                            */
                            password = "*********";
                        }
                        else if (tmp.Substring(0, 5) == "https" || (tmp.Substring(0, 4) == "http")) 
                        {
                            url = Line;
                        }
                        else
                        {
                            use = Line;
                        }
                    }
                    ListViewPasswords.Items.Add(new PasswordItem { Use = use, Password = password, URL = url });
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
            NavigationService.Refresh();
        }
        private void btn_password_generate(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("Pages/GeneratePasswordPage.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }
        private void btn_password_copy(object sender, RoutedEventArgs e)
        {
            PasswordItem selectedPassword;
            try
            {
                selectedPassword = (PasswordItem)ListViewPasswords.SelectedItems[0];
            } catch (Exception ex)
            {
                MessageBox.Show("Please Select A Password First", "Password Manager", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string password_folder = System.IO.Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
                    @"PasswordManager\userdata\passwords\"; // C:\PasswordManager\userdata\

            foreach (string filename in Directory.GetFiles(password_folder))
            {
                if (filename.ToString() == password_folder + selectedPassword.Use + ".txt")
                {
                    using (StreamReader sr = new StreamReader(filename))
                    {
                        string? Line;
                        string password = "";
                        while ((Line = sr.ReadLine()) != null)
                        {
                            string tmp = Line;

                            if (tmp.Substring(tmp.Length - 2) == "==" || tmp.Substring(tmp.Length - 1) == "=")
                            {
                                string decrypted_string = EncryptionHelper.EncryptionHelper.Decrypt(tmp);
                                password = decrypted_string;
                                Clipboard.SetText(password);
                                MessageBox.Show("Password copied to clipboard", "Password Generator", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                    }
                }
            }
        }
        private void btn_remove_password(object sender, RoutedEventArgs e)
        {
            PasswordItem selectedPassword;
            try
            {
                selectedPassword = (PasswordItem)ListViewPasswords.SelectedItems[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Select A Password First", "Password Manager", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string password_folder = System.IO.Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
                    @"PasswordManager\userdata\passwords\"; // C:\PasswordManager\userdata\

            foreach (string filename in Directory.GetFiles(password_folder))
            {
                if (filename.ToString() == password_folder + selectedPassword.Use + ".txt")
                {
                    if (MessageBox.Show("Do You Really Want To Delete This Password?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        File.Delete(filename);
                        // reloads the site
                        NavigationService.Refresh();
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
    }
}
