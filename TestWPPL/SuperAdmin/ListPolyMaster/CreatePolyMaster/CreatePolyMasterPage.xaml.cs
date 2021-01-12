using System;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json.Linq;
using TestWPPL.Admin.ListPolyclinic;
using Velacro.LocalFile;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBox;
using PolyMaster = TestWPPL.Admin.CreatePolyclinicSchedule.PolyMaster;

namespace TestWPPL.SuperAdmin.ListPolyMaster.CreatePolyMaster
{
    public partial class CreatePolyMasterPage : MyPage
    {
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private IMyButton addButton;
        private IMyTextBox nameTxtBox, emailTxtBox, addressTxtBox, callCenterTxtBox;

        public CreatePolyMasterPage()
        {
            InitializeComponent();
            this.KeepAlive = true;
            setController(CreatePolyMasterController.CreateInstance(this));
            initUIBuilders();
            initUIElements();
        }

        private void initUIBuilders()
        {
            buttonBuilder = new BuilderButton();
            txtBoxBuilder = new BuilderTextBox();
        }

        private void initUIElements()
        {
            addButton = buttonBuilder.activate(this, "addHA_btn");
            nameTxtBox = txtBoxBuilder.activate(this, "name_txt");
        }

        private void btAddPolyMaster(object sender, RoutedEventArgs e)
        {
            getController().callMethod("storePolyMasterData",
                name_txt.Text);
        }
        
        public void successStore(PolyMaster polyMaster)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.NavigationService.GoBack();
                this.NavigationService.RemoveBackEntry();

                nameTxtBox.setText("");

                MessageBox.Show(
                    "Nama : " + polyMaster.name + Environment.NewLine
                );
            });
        }
        
        public void setErrorStore(string errorMessage)
        {
            JObject messageJSon = JObject.Parse(errorMessage);
            
            String nameError = "";
            String allError = "";
            
            if (messageJSon["name"] != null)
            {
                nameError += messageJSon["name"][0].ToString();
                allError += nameError + Environment.NewLine;
            }
           

            this.Dispatcher.Invoke(() =>
            {
                MessageBox.Show(
                    "Error : " + allError + Environment.NewLine 
                );
            });

        }

        private void upload_btn(object sender, RoutedEventArgs e)
        {
            OpenFile openFile = new OpenFile();
            openFile.openImageFile(false);
            
        }
    }
}