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

        private void display_passwords()
        {
            string password_folder = System.IO.Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
                    @"PasswordManager\userdata\passwords\"; // C:\PasswordManager\userdata\

            foreach (string filename in Directory.GetFiles(password_folder))
            {
                string fileToRead = filename;
                using (StreamReader sr = new StreamReader(fileToRead))
                {
                    string Line;
                    while ((Line = sr.ReadLine()) != null)
                    {
                        String tmp = Line;

                        if (!(tmp.Substring(tmp.Length - 2) == "=="))
                        {
                            txtPasswords.Text += "\n";
                            txtPasswords.Text += "Password Use: " + Line;
                        }
                        else
                        {
                            String decrypted_string = EncryptionHelper.EncryptionHelper.Decrypt(tmp);
                            txtPasswords.Text += "\nPassword: " + decrypted_string;
                        }
                    }
                    return;
                }
            }
        }
    }
}
