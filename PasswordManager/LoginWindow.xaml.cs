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
using MaterialDesignThemes.Wpf;

namespace PasswordManager
{
    public partial class LoginWindow : Window
    {   
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            Window mainWindow = new MainWindow();
            Close();
            mainWindow.Show();
        }

        private void btn_signup_Click(object sender, RoutedEventArgs e)
        {
            Window registerWindow = new RegisterWindow();
            Close();
            registerWindow.Show();
        }
    }
}
