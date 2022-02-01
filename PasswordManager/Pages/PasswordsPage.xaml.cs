using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Diagnostics;
using System.Configuration;

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
        #pragma warning disable CS8602
        public string pwd_folder = System.IO.Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
                ConfigurationManager.AppSettings["pwd_folder_path"].ToString();

        #pragma warning disable CS8602
        public string pwd_file_ending = ConfigurationManager.AppSettings["pwd_file_ending"].ToString();

        #pragma warning disable CS8602
        public string pwd_replacement = ConfigurationManager.AppSettings["pwd_replacement"].ToString();

        #pragma warning disable CS8602
        public string app_name = ConfigurationManager.AppSettings["app_name"].ToString();

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
            foreach (string filename in Directory.GetFiles(pwd_folder))
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
                            password = pwd_replacement;
                        }
                        else if (tmp.Substring(0, 4) == "http")
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
            if (MessageBox.Show("Do You Really Want To Delete All Your Passwords?", app_name, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (string filename in Directory.GetFiles(pwd_folder))
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
                #pragma warning disable CS8600
                selectedPassword = (PasswordItem)ListViewPasswords.SelectedItems[0];
            } catch
            {
                MessageBox.Show("Please Select A Password First", app_name, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            foreach (string filename in Directory.GetFiles(pwd_folder))
            {
                #pragma warning disable CS8602
                if (filename.ToString() == pwd_folder + selectedPassword.Use + pwd_file_ending)
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
                                MessageBox.Show("Password copied to clipboard", app_name, MessageBoxButton.OK, MessageBoxImage.Information);
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
            catch
            {
                MessageBox.Show("Please Select A Password First", app_name, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (string filename in Directory.GetFiles(pwd_folder))
            {
                if (filename.ToString() == pwd_folder + selectedPassword.Use + pwd_file_ending)
                {
                    if (MessageBox.Show("Do You Really Want To Delete This Password?", app_name, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
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
        void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement)e.OriginalSource).DataContext;
            if (item != null)
            {
                PasswordItem selectedPassword = (PasswordItem)ListViewPasswords.SelectedItems[0];

                foreach (string filename in Directory.GetFiles(pwd_folder))
                {
                    if (filename == pwd_folder + selectedPassword.Use + pwd_file_ending)
                    {
                        using (StreamReader sr = new StreamReader(filename))
                        {
                            string? Line;
                            string url = "";

                            while ((Line = sr.ReadLine()) != null)
                            {
                                string tmp = Line;
                                if (tmp.Substring(0, 4) == "http")
                                {
                                    url = Line;

                                    var uri = url;
                                    var psi = new ProcessStartInfo();

                                    psi.UseShellExecute = true;
                                    psi.FileName = uri;

                                    Process.Start(psi);
                                    e.Handled = true;
                                    Logger.WriteLog("Opend Weblink: " + url, "INFO");
                                }
                            }
                        }
                    }
                }

            }
        }
    }
}
