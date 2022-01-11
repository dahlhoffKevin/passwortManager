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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PasswordManager.Pages
{
    /// <summary>
    /// Interaktionslogik für GeneratePasswordPage.xaml
    /// </summary>
    public partial class GeneratePasswordPage : Page
    {
        public GeneratePasswordPage()
        {
            InitializeComponent();
        }

        private void btn_password_generate(object sender, RoutedEventArgs e)
        {

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
