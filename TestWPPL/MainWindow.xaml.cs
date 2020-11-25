using System.Windows;
using TestWPPL.Login;
using TestWPPL.Register;
using Velacro.DataStructures;
using Velacro.UIElements.Basic;

namespace TestWPPL {
    public partial class MainWindow : MyWindow {
        private MyPage loginPage;
        private MyPage registerPage;
        private MyPage dashboardPage;
        public MainWindow() {
            InitializeComponent();
            registerPage = new RegisterPage();
            loginPage = new LoginPage();
            dashboardPage = new Dashboard.Dashboard();
        }

        private void loginButton_btn_Click(object sender, RoutedEventArgs e){
            mainFrame.Navigate(loginPage);
        }

        private void registerButton_btn_Click(object sender, RoutedEventArgs e){
            mainFrame.Navigate(registerPage);
        }

        private void dashboardButton_btn_Click(object sender, RoutedEventArgs e){
            mainFrame.Navigate(dashboardPage);
        }
    }
}
