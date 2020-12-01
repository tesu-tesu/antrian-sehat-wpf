using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TestWPPL.SuperAdmin.ListHealthAgency;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.ComboBox;
using Velacro.UIElements.TextBlock;

namespace TestWPPL.SuperAdmin.ListUserModul.CreateUser
{
    /// <summary>
    /// Interaction logic for CreateUserPage.xaml
    /// </summary>
    public partial class CreateUserPage : MyPage
    {
        private BuilderTextBlock builderTextBlock;
        private BuilderComboBox builderComboBox;
        private BuilderButton builderButton;
        private IMyComboBox healthAgencyCbx;

        public CreateUserPage()
        {
            InitializeComponent();
            this.KeepAlive = true;
            setController(new CreateUserController(this));
        }

        public void fetchDataHealthAgency()
        {
            getController().callMethod("fetchDataHealthAgency");
        }

        public void setComboBox(List<HealthAgency> healthAgencies)
        {
            this.Dispatcher.Invoke(() =>
            {
                health_agency_cbx.ItemsSource = healthAgencies;
                health_agency_cbx.DisplayMemberPath = "name";
                health_agency_cbx.SelectedValuePath = "id";
            });
        }

        public void btAddUser(object sender, RoutedEventArgs e)
        {
            //getController().callMethod
        }

    }
}
