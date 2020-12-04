using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBox;

namespace TestWPPL.SuperAdmin.ListHealthAgency.EditHealthAgency
{
    /// <summary>
    /// Interaction logic for EditHealthAgencyPage.xaml
    /// </summary>
    public partial class EditHealthAgencyPage : MyPage
    {
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private IMyButton updateButton;
        private IMyTextBox nameTxtBox, emailTxtBox, addressTxtBox, callCenterTxtBox;

        
        public int idHA { get; set; }

        public EditHealthAgencyPage()
        {
            InitializeComponent();
            initUIElements();
        }
        
        private void initUIElements()
        {
            updateButton = buttonBuilder.activate(this, "addHA_btn");
            nameTxtBox = txtBoxBuilder.activate(this, "name_txt");
            emailTxtBox = txtBoxBuilder.activate(this, "email_txt");
            addressTxtBox = txtBoxBuilder.activate(this, "address_txt");
            callCenterTxtBox = txtBoxBuilder.activate(this, "call_center_txt");
        }
        

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btUpdateHA(object sender, RoutedEventArgs e)
        {
            getController().callMethod("storeHealthAgencyData",
                name_txt.Text, email_txt.Text, address_txt.Text, call_center_txt.Text);
        }
        
        public void successStore(HealthAgency healthAgency)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.NavigationService.GoBack();
                this.NavigationService.RemoveBackEntry();

                nameTxtBox.setText("");
                emailTxtBox.setText("");
                addressTxtBox.setText("");
                callCenterTxtBox.setText("");

                MessageBox.Show(
                    "Nama : " + healthAgency.name + Environment.NewLine +
                    "Email : " + healthAgency.email + Environment.NewLine +
                    "Call center : " + healthAgency.call_center + Environment.NewLine +
                    "Alamat : " + healthAgency.address + Environment.NewLine 
                );
            });
        }
        
    }
}
