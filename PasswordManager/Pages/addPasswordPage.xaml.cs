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
    public partial class addPasswordPage : Page
    {
        public addPasswordPage()
        {
            InitializeComponent();
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
            string entry_password = txtPassword.Password.ToString();
            string entry_password_confirm = txtPasswordConfirm.Password.ToString();
            string entry_password_use = txt_password_use.Text.ToString();
            string entry_password_url = txt_password_url.Text.ToString();
            string password_folder = System.IO.Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
                    @"PasswordManager\userdata\passwords\"; // C:\PasswordManager\userdata\

            if (entry_password == "" || entry_password_confirm == "" || entry_password_use == "")
            {
                MessageBox.Show("All Fields Must Been Filled In", "ITAPass", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!(entry_password == entry_password_confirm))
            {
                MessageBox.Show("Passwords Are Not Machting", "ITAPass", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!check_folders())
            {
                MessageBox.Show("Ops! An Error Occured!\nPlease Restart The Application\nSee The Logs For More Information", "ITAPass", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.WriteLog("Not All Folders Were Created", "ERROR");
                return;
            }
            if (!(txtPassword.Password.Length >= 4))
            {
                MessageBox.Show("Your Password Should Not Be Shorter Than 4 Characters", "ITAPass", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!(txt_password_use.Text.Length > 2))
            {
                MessageBox.Show("The Password Use Is Too Short", "ITAPass", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (File.Exists(password_folder + entry_password_use + ".txt"))
                {
                    MessageBox.Show("Password Already Exists", "ITAPass", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (entry_password_url == "")
                {
                    File.Create(password_folder + entry_password_use + ".txt").Dispose();
                    using StreamWriter file = new(password_folder + entry_password_use + ".txt", append: true);
                    string encrypted_password = EncryptionHelper.EncryptionHelper.Encrypt(entry_password);
                    file.WriteLine(encrypted_password + "\n" + entry_password_use);
                    file.Close();
                }
                else
                {
                    try
                    {
                        if (!(entry_password_url.Substring(0, 5) == "https" || entry_password_url.Substring(0, 4) == "http"))
                        {
                            MessageBox.Show("The Entered URL Must Start With 'https' Or 'http'!", "ITAPass", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    } catch
                    {
                        MessageBox.Show("The Entered URL Must Start With 'https' Or 'http'!", "ITAPass", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    File.Create(password_folder + entry_password_use + ".txt").Dispose();
                    using StreamWriter file = new(password_folder + entry_password_use + ".txt", append: true);
                    string encrypted_password = EncryptionHelper.EncryptionHelper.Encrypt(entry_password);
                    file.WriteLine(encrypted_password + "\n" + entry_password_use + "\n" + entry_password_url);
                    file.Close();

                }

                txtPassword.Password = "";
                txtPasswordConfirm.Password = "";
                txt_password_use.Text = "";
                txt_password_url.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ops! An Error Occured!\nPlease Restart The Application\nSee The Logs For More Information", "ITAPass", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.WriteLog(ex.ToString(), "ERROR");
                return;
            }
        }
        private void btn_backhome_click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("Pages/PasswordsPage.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }
    }
}
