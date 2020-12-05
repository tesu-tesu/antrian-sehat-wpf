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
using Newtonsoft.Json.Linq;
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
            this.KeepAlive = true;
            initUIElements();
            initUIBuilders();
            setController(new EditHealthAgencyController(this));
        }

        public void fetchHAData()
        {
            getController().callMethod("fetchHAData", idHA);
        }
        
        private void initUIBuilders()
        {
            buttonBuilder = new BuilderButton();
            txtBoxBuilder = new BuilderTextBox();
        }
        
        
        private void initUIElements()
        {
            updateButton = buttonBuilder.activate(this, "updateHA_btn");
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
        
        public void setUserData(User user)
        {
            this.Dispatcher.Invoke(() =>
            {
                name_txt.Text = user.name;
                email_txt.Text = user.email;
                call_center_txt.Text = user.phone;
                address_txt.Text = user.residence_number;
            });
        }

        private void btnUpdateHA(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("UPDATE");
            /*getController().callMethod("storeHealthAgencyData",
                name_txt.Text, email_txt.Text, address_txt.Text, call_center_txt.Text);*/

            if (name_txt.Text == ""
                || call_center_txt.Text == ""
                || address_txt.Text == ""
                )
            {
                MessageBox.Show("Nama, Call Center, Alamat harus diisi");
            }
            else
            {
                getController().callMethod("updateHA", idHA, name_txt.Text, email_txt.Text, call_center_txt.Text);
            }
            
        }
        
        public void successStore(HealthAgency healthAgency)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.NavigationService.GoBack();
                this.NavigationService.RemoveBackEntry();

                /*
                nameTxtBox.setText("");
                emailTxtBox.setText("");
                addressTxtBox.setText("");
                callCenterTxtBox.setText("");
                */

                MessageBox.Show(
                    "Nama : " + healthAgency.name + Environment.NewLine +
                    "Email : " + healthAgency.email + Environment.NewLine +
                    "Call center : " + healthAgency.call_center + Environment.NewLine +
                    "Alamat : " + healthAgency.address + Environment.NewLine 
                );
            });
        }
        
        public void setErrorStore(string errorMessage)
        {
            JObject messageJSon = JObject.Parse(errorMessage);
            String emailError = "";
            String nameError = "";
            String addressError = "";
            String callCenterError = "";
            String allError = "";

            if (messageJSon["email"] != null)
            {
                emailError += messageJSon["email"][0].ToString();
                allError += emailError + Environment.NewLine;
            }
            if (messageJSon["name"] != null)
            {
                nameError += messageJSon["name"][0].ToString();
                allError += nameError + Environment.NewLine;
            }
            if (messageJSon["address"] != null)
            {
                addressError += messageJSon["address"][0].ToString();
                allError += addressError + Environment.NewLine;
            }
            if (messageJSon["call_center"] != null)
            {
                callCenterError += messageJSon["call_center"][0].ToString();
                allError += callCenterError + Environment.NewLine;
            }

            this.Dispatcher.Invoke(() =>
            {
                MessageBox.Show(
                    "Error : " + allError + Environment.NewLine 
                );
            });

        }
    }
}
