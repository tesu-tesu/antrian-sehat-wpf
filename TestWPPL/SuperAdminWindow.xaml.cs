using System;
using System.Collections.Generic;
using System.IO;
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
using TestWPPL.SuperAdmin.ListPolyMaster;
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
        private MyPage listPolyMasterPage;
        private MyPage aboutPage;

        public SuperAdminWindow()
        {
            InitializeComponent();
            FrameService.frame = mainFrame;
            listUserPage = new ListUserPage();
            listHealthAgencyPage = new ListHealthAgencyPage();
            listPolyMasterPage = new ListPolyMasterPage();
            aboutPage = new About.About();
            mainFrame.Navigate(aboutPage);
        }

        private void logoutBtnClick(object sender, RoutedEventArgs e)
        {
            ApiClient client = ApiAntrianSehat.getInstance().GetApiClient();
            client.clearAuthorizationToken();
            File.Delete("../../assets/user.txt");
            new LoginPage().Show();
            this.Close();
        }

        private void listHealthAgencyBtnClick(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(listHealthAgencyPage);
            listHealthAgencyPage.callMethod("fetchDataHealthAgency");
        }

        private void listUserBtnClick(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(listUserPage);
            listUserPage.callMethod("fetchDataUser");
        }

        private void listPolyMasterBtnClick(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(listPolyMasterPage);
            listPolyMasterPage.callMethod("fetchDataPolyMaster");
        }
    }
}
