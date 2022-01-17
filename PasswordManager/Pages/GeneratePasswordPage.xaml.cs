﻿using System;
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
using System.Security.Cryptography;


namespace PasswordManager.Pages
{
    public partial class GeneratePasswordPage : Page
    {
        public GeneratePasswordPage()
        {
            InitializeComponent();
        }

        static RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();

        private void btn_password_generate(object sender, RoutedEventArgs e)
        {
            if (txtPasswordLength.Text == "")
            {
                MessageBox.Show("Password Length Field Must Be Filled Out", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!(checkBox_normal_characters.IsChecked == true || checkBox_special_characters.IsChecked == true || checkBox_digits.IsChecked == true))
            {
                MessageBox.Show("At Least One Check Box Must Be Checked", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int PasswordLength = Convert.ToInt32(txtPasswordLength.Text.ToString());

            if (PasswordLength > 256)
            {
                MessageBox.Show("Password Manger: 'How long should the password be?'\nUser: 'Yes!'\n\nBro Chill! I don't think anyone needs such a long password!", "WTF", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            int PasswordAmount = 1;
            string CapitalLetters = "QWERTYUIOPASDFGHJKLZXCVBNM";
            string SmallLetters = "qwertyuiopasdfghjklzxcvbnm";
            string Digits = "0123456789";
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

            string[] AllPasswords = new string[PasswordAmount];
            
            for (int i = 0; i < PasswordAmount; i++)
            {
                StringBuilder sb = new StringBuilder();
                for (int n = 0; n < PasswordLength; n++)
                {
                    sb = sb.Append(GenerateChar(AllChar));
                }

                AllPasswords[i] = sb.ToString();
            }

            foreach (string singlePassword in AllPasswords)
            {
                txtGeneratedPassword.Text = singlePassword;
            }
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

        }
        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("Pages/PasswordsPage.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }
    }
}
