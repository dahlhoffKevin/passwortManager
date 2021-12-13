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

namespace PasswordManager
{
    /// <summary>
    /// Interaktionslogik für RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            String username = txtUsername.Text;
            String password = txtPassword.Text;
            String passwordRepetition = txtPasswordRepetition.Text;

            #pragma warning disable CS8600 // Das NULL-Literal oder ein möglicher NULL-Wert wird in einen Non-Nullable-Typ konvertiert.
            String winDir = System.IO.Path.GetPathRoot(Environment.SystemDirectory);
            #pragma warning restore CS8600 // Das NULL-Literal oder ein möglicher NULL-Wert wird in einen Non-Nullable-Typ konvertiert.

            String path = winDir + @"\PasswordManager";

            String notMatchingPasswords = "Deine Passwörter stimmen nicht überein";
            String notAllFieldsFilledIn = "Alle Felder müssen ausgefüllt sein";
            String anErrorOccured = "Ein Fehler ist aufgetretten";


            if (! String.IsNullOrWhiteSpace(username) || 
                ! String.IsNullOrWhiteSpace(password) || 
                ! String.IsNullOrWhiteSpace(passwordRepetition))
            {
                if (password == passwordRepetition)
                {
                    try
                    {
                        if (Directory.Exists(path))
                        {
                            MessageBox.Show("Ordner bereits vorhanden", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                        } else
                        {
                            Directory.CreateDirectory(path);
                            MessageBox.Show("Ordner erstellt", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    } 
                    catch (Exception ex)
                    {
                        MessageBox.Show(anErrorOccured + ": " + ex.ToString(), "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                } 
                else
                {
                    MessageBox.Show(notMatchingPasswords, "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show(notAllFieldsFilledIn, "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Window mainWindow = new MainWindow();
            Close();
            mainWindow.Show();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Window mainWindow = new MainWindow();
            Close();
            mainWindow.Show();
        }
    }
}
