using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Security.Cryptography;
using System.Configuration;

namespace PasswordManager.Pages
{
    public partial class GeneratePasswordPage : Page
    {
        #pragma warning disable CS8602
        public string app_name = ConfigurationManager.AppSettings["app_name"].ToString();
        public int max_pwd_length = Convert.ToInt32(ConfigurationManager.AppSettings["max_pwd_length"].ToString());

        public GeneratePasswordPage()
        {
            InitializeComponent();
        }

        #pragma warning disable SYSLIB0023
        static RNGCryptoServiceProvider provider = new();
        private void btn_password_generate(object sender, RoutedEventArgs e)
        {
            if (txtPasswordLength.Text == "")
            {
                MessageBox.Show("Password Length Field Must Be Filled Out", app_name, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!(checkBox_normal_characters.IsChecked == true || checkBox_special_characters.IsChecked == true || checkBox_digits.IsChecked == true))
            {
                MessageBox.Show("At Least One Check Box Must Be Checked", app_name, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int PasswordLength = Convert.ToInt32(txtPasswordLength.Text.ToString());

            if (PasswordLength > max_pwd_length)
            {
                MessageBox.Show("Password Manger: 'How long should the password be?'\nUser: 'Yes!'\n\nBro Chill! I don't think anyone needs such a long password!", "WTF", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            string CapitalLetters = ConfigurationManager.AppSettings["capital_letters"].ToString();
            string SmallLetters = ConfigurationManager.AppSettings["small_letters"].ToString(); ;
            string Digits = ConfigurationManager.AppSettings["digits"].ToString();
            string SpecialCharacters = "!@#$%^&*()-_=+<,>.";
            string AllChar = "";

            // checks if all checkboxes are checked
            if (checkBox_normal_characters.IsChecked == true && checkBox_special_characters.IsChecked == true && checkBox_digits.IsChecked == true)
            {
                AllChar = CapitalLetters + SmallLetters + Digits + SpecialCharacters;
            }
            // checks if normal character and digits checkboxs are checked
            else if (checkBox_normal_characters.IsChecked == true && checkBox_digits.IsChecked == true) 
            {
                AllChar = CapitalLetters + SmallLetters + Digits;
            }
            // checks if normal characters and special character checkboxs are checked
            else if (checkBox_normal_characters.IsChecked == true && checkBox_special_characters.IsChecked == true)
            {
                AllChar = CapitalLetters + SmallLetters + SpecialCharacters;
            }
            // checks if digits and special characters checkboxes are checked
            else if (checkBox_digits.IsChecked == true && checkBox_special_characters.IsChecked == true)
            {
                AllChar = Digits + SpecialCharacters;
            }
            // checks if the normal character checkbox is checked
            else if (checkBox_normal_characters.IsChecked == true)
            {
                AllChar = CapitalLetters + SmallLetters;
            }
            // checks if the digits checkbox is checked
            else if (checkBox_digits.IsChecked == true)
            {
                AllChar = Digits;
            }
            // checks if the special character checkbox is checked
            else if (checkBox_special_characters.IsChecked == true)
            {
                AllChar = SpecialCharacters;
            }

            string Password;
            
            StringBuilder sb = new StringBuilder();
            for (int n = 0; n < PasswordLength; n++)
            {
                sb = sb.Append(GenerateChar(AllChar));
            }

            Password = sb.ToString();
            txtGeneratedPassword.Text = Password;
            
        }
        private static char GenerateChar(string availableChars)
        {
            var byteArray = new byte[1];
            char c;
            do
            {
                provider.GetBytes(byteArray);
                c = (char)byteArray[0];

            } while (!availableChars.Any(x => x == c));
            return c;
        }
        private void btn_copy_to_clipboard(object sender, RoutedEventArgs e)
        {
            string generated_password = txtGeneratedPassword.Text;
            if (generated_password == "")
            {
                MessageBox.Show("Nothing To Copy", app_name, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Clipboard.SetText(generated_password);
        }
        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("Pages/PasswordsPage.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }
    }
}
