using System;
using System.Windows;
using System.Windows.Input;

namespace PasswordManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PagesNavigation.Navigate(new Uri("Pages/HomePage.xaml", UriKind.RelativeOrAbsolute));
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
        // Page Navigation Methods
        private void rdHome_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new Uri("Pages/HomePage.xaml", UriKind.RelativeOrAbsolute));
        }
        private void rdPasswords_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new Uri("Pages/PasswordsPage.xaml", UriKind.RelativeOrAbsolute));
        }
        private void rdNotes_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new Uri("Pages/NotesPage.xaml", UriKind.RelativeOrAbsolute));
        }
        private void rdPayment_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new Uri("Pages/PaymentPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
