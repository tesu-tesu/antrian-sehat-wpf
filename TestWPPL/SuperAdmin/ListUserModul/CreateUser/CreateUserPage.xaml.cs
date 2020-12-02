using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TestWPPL.Service;
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

        public void store(object sender, RoutedEventArgs e)
        {
            HealthAgency healthAgency = (HealthAgency)health_agency_cbx.SelectedItem;
            if (name.Text == "" || email.Text == "" || phone.Text == "" || healthAgency == null)
                MessageBox.Show("Nama, Email, Nomor HP, dan Puskesmas wajib diisi", "ERROR");
            else
            {
                String res_number = residence_number.Text;
                if (residence_number.Text == "")
                    res_number = "0";
                getController().callMethod("store", name.Text, email.Text, phone.Text, res_number, healthAgency.id);
            }
        }

        public void successStore(User user, String password)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.NavigationService.GoBack();
                this.NavigationService.RemoveBackEntry();
                MessageBox.Show(
                    "Name : " + user.name + Environment.NewLine +
                    "Email : " + user.email + Environment.NewLine +
                    "Phone : " + user.phone + Environment.NewLine +
                    "No. KTP : " + user.residence_number + Environment.NewLine +
                    "Puskesmas : " + user.health_agency.name + Environment.NewLine +
                    "Password : " + password, "Success");
            });
        }

    }
}
