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
using TestWPPL.Admin.CreatePolyclinicSchedule;
using Velacro.UIElements.Basic;
using TestWPPL.Login;
using TestWPPL.Admin.ListPolyclinic;
using Velacro.Api;
using System.IO;

namespace TestWPPL
{
    /// <summary>
    /// Interaction logic for AuthenticatedWindow.xaml
    /// </summary>
    public partial class AdminWindow : MyWindow
    {
        private MyPage listPolyclinicPage;
        private MyPage createPolyclinicSchedulePage;

        public AdminWindow()
        {
            InitializeComponent();
            listPolyclinicPage = new ListPolyclinicPage();
            createPolyclinicSchedulePage = new CreatePolyclinicSchedulePage();
        }

        private void logoutBtnClick(object sender, RoutedEventArgs e)
        {
            ApiClient client = ApiAntrianSehat.getInstance().GetApiClient();
            client.clearAuthorizationToken();
            File.Delete("../../assets/user.txt");
            new LoginPage().Show();
            this.Close();
        }

        private void listPolyclinicBtnClick(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(listPolyclinicPage);
            listPolyclinicPage.callMethod("fetchDataPolyclinic");
        }

        private void managePolyclinicBtnClick(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(createPolyclinicSchedulePage);
            createPolyclinicSchedulePage.callMethod("fetchDataPolyMaster");
        }
    }
}
