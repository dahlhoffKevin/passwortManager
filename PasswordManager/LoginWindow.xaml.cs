using System;
using System.Windows;
using System.Windows.Input;

namespace PasswordManager
{
    public partial class LoginWindow : Window
    {   
        public LoginWindow()
        {
            InitializeComponent();
            PagesNavigation.Navigate(new Uri("Pages/LoginPage.xaml", UriKind.RelativeOrAbsolute));
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
