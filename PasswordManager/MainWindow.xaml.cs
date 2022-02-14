using System;
using System.Windows;
using System.Windows.Input;
using PasswordManagerLogger;

namespace PasswordManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PagesNavigation.Navigate(new Uri("Pages/HomePage.xaml", UriKind.RelativeOrAbsolute));
            Application.Current.MainWindow.Close();
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Logger.WriteLog("Application Shutdown", "INFO");
            Application.Current.Shutdown();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        // Page Navigation Methods
        private void rdHome_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new Uri("Pages/HomePage.xaml", UriKind.RelativeOrAbsolute));
            Logger.WriteLog("Switched to Home Page", "INFO");
        }
        private void rdPasswords_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new Uri("Pages/PasswordsPage.xaml", UriKind.RelativeOrAbsolute));
            Logger.WriteLog("Switched to Password Page", "INFO");
        }
    }
}
