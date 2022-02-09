using System.Windows.Controls;
using System.Windows.Navigation;
using System.Diagnostics;

namespace PasswordManager.Pages
{
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }
        private void Hyperlink_RequestNavigate_Discord(object sender, RequestNavigateEventArgs e)
        {
            var uri = "https://discord.gg/TM35FKFB2G";
            var psi = new ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = uri;
            Process.Start(psi);
            e.Handled = true;
            Logger.WriteLog("Clicked Discord Link", "INFO");
        }
        private void Hyperlink_RequestNavigate_Github(object sender, RequestNavigateEventArgs e)
        {
            var uri = "https://github.com/dahlhoffKevin/passwortManager";
            var psi = new ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = uri;
            Process.Start(psi);
            e.Handled = true;
            Logger.WriteLog("Clicked Github Link", "INFO");
        }
    }
}
