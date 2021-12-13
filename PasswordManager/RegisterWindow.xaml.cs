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
            Window mainWindow = new MainWindow();
            Close();
            mainWindow.Show();
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
