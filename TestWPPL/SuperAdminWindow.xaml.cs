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
using TestWPPL.Login;
using TestWPPL.Service;
using TestWPPL.SuperAdmin.ListHealthAgency;
using TestWPPL.SuperAdmin.ListHealthAgency.CreateHealthAgency;
using TestWPPL.SuperAdmin.ListUserModul;
using TestWPPL.SuperAdmin.ListUserModul.CreateUser;
using Velacro.Api;
using Velacro.UIElements.Basic;

namespace TestWPPL
{
    /// <summary>
    /// Interaction logic for SuperAdminWindow.xaml
    /// </summary>
    public partial class SuperAdminWindow : MyWindow
    {
        private MyPage listUserPage;
        private MyPage listHealthAgencyPage;
        private MyPage createUserPage;
        private MyPage createHealthAgencyPage;

        public SuperAdminWindow()
        {
            InitializeComponent();
            FrameService.frame = mainFrame;
            listUserPage = new ListUserPage();
            listHealthAgencyPage = new ListHealthAgencyPage();
            createUserPage = new CreateUserPage();
            createHealthAgencyPage = new CreateHealthAgencyPage();
        }

        private void logoutBtnClick(object sender, RoutedEventArgs e)
        {
            ApiClient client = ApiAntrianSehat.getInstance().GetApiClient();
            client.clearAuthorizationToken();

            new LoginPage().Show();
            this.Close();
        }

        private void listHealthAgencyBtnClick(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(listHealthAgencyPage);
            listHealthAgencyPage.callMethod("fetchDataHealthAgency");
            add_ha_btn.Visibility = Visibility.Visible;
            Console.WriteLine(add_ha_btn.Content);
            add_user_btn.Visibility = Visibility.Hidden;
        }

        private void listUserBtnClick(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(listUserPage);
            listUserPage.callMethod("fetchDataUser");

            add_user_btn.Visibility = Visibility.Visible;
            add_ha_btn.Visibility = Visibility.Hidden;
        }

        void addHA_OnClick(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(createHealthAgencyPage);
        }

        void addUser_OnClick(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(createUserPage);
            createUserPage.callMethod("fetchDataHealthAgency");
        }
    }
}
