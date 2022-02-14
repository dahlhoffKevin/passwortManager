using System;
using System.Windows;
using System.Windows.Input;
using PasswordManagerLogger;

namespace PasswordManager
{
    public partial class LoginWindow : Window
    {   
        public LoginWindow()
        {
            InitializeComponent();
            Logger.WriteLog("Apllication started successfully", "INFO");

            PagesNavigation.Navigate(new Uri("Pages/LoginPage.xaml", UriKind.RelativeOrAbsolute));
            Logger.WriteLog("Switched to Login Page", "INFO");
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Logger.WriteLog("Application Shutdown", "INFO");
            App.Current.Shutdown();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
