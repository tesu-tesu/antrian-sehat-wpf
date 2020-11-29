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
using Velacro.UIElements.Basic;
using TestWPPL.Login;
using TestWPPL.Admin.ListPolyclinic;
using Velacro.Api;

namespace TestWPPL
{
    /// <summary>
    /// Interaction logic for AuthenticatedWindow.xaml
    /// </summary>
    public partial class AdminWindow : MyWindow
    {
        private MyPage listPolyclinicPage;
        public AdminWindow()
        {
            InitializeComponent();
            listPolyclinicPage = new ListPolyclinicPage();
        }

        private void logoutBtnClick(object sender, RoutedEventArgs e)
        {
            ApiClient client = ApiAntrianSehat.getInstance().GetApiClient();
            client.clearAuthorizationToken();

            new LoginPage().Show();
            this.Close();
        }

        private void listPolyclinicBtnClick(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(listPolyclinicPage);
            listPolyclinicPage.callMethod("fetchDataPolyclinic");
        }
    }
}
