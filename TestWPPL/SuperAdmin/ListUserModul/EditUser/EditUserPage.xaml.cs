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
using TestWPPL.SuperAdmin.ListHealthAgency;
using TestWPPL.SuperAdmin.ListUserModul.EditUser;
using Velacro.UIElements.Basic;

namespace TestWPPL.SuperAdmin.ListUserModul.EditUser
{
    /// <summary>
    /// Interaction logic for EditUserPage.xaml
    /// </summary>
    public partial class EditUserPage : MyPage
    {
        private int healthAgencyId;
        public int userId { get; set; }
       
        public EditUserPage()
        {
            InitializeComponent();
            this.KeepAlive = true;
            setController(new EditUserController(this));
        }

        public void fetchUserData()
        {
            getController().callMethod("fetchUserData", userId);
        }

        public void fetchHealthAgencies()
        {
            getController().callMethod("fetchDataHealthAgency");
        }

        public void setUserData(User user)
        {
            this.Dispatcher.Invoke(() =>
            {
                name.Text = user.name;
                email.Text = user.email;
                phone.Text = user.phone;
                residence_number.Text = user.residence_number;
                healthAgencyId = user.health_agency_id;

                fetchHealthAgencies();
            });
        }

        public void setComboBox(List<HealthAgency> healthAgencies)
        {
            this.Dispatcher.Invoke(() =>
            {
                health_agency_cbx.ItemsSource = healthAgencies;
                health_agency_cbx.DisplayMemberPath = "name";
                health_agency_cbx.SelectedValuePath = "id";
                health_agency_cbx.SelectedValue = healthAgencyId;
            });
        }



        public void successUpdate(User user)
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
                    "Puskesmas : " + user.health_agency.name + Environment.NewLine, 
                    "Success");
            });
        }

        private void update(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("UPDATE");
            HealthAgency healthAgency = (HealthAgency)health_agency_cbx.SelectedItem;
            if (name.Text == "" || email.Text == "" || phone.Text == "" || healthAgency == null)
                MessageBox.Show("Nama, Email, Nomor HP, dan Puskesmas wajib diisi", "ERROR");
            else
            {
                String res_number = residence_number.Text;
                if (residence_number.Text == "")
                    res_number = "0";
                getController().callMethod("update", userId, name.Text, email.Text, phone.Text, res_number, healthAgency.id);
            }
        }
    }
}
