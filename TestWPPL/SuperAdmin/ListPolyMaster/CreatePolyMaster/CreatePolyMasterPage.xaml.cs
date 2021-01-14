using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using TestWPPL.Admin.ListPolyclinic;
using Velacro.Basic;
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
        private IMyTextBox nameTxtBox;
        MyFile polyMasterImage;
        private Label imageInfoLabel;


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
            polyMasterImage = new MyFile();
        }

        private void btAddPolyMaster(object sender, RoutedEventArgs e)
        {
            getController().callMethod("storePolyMasterData", name_txt.Text, polyMasterImage);
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
            polyMasterImage = openFile.openFile(false)[0];            

            if (polyMasterImage != null)
            {
                if (polyMasterImage.extension.Equals(".png", StringComparison.InvariantCultureIgnoreCase) ||
                    polyMasterImage.extension.Equals(".jpg", StringComparison.InvariantCultureIgnoreCase) ||
                    polyMasterImage.extension.Equals(".jpeg", StringComparison.InvariantCultureIgnoreCase) ||
                    polyMasterImage.extension.Equals(".bmp", StringComparison.InvariantCultureIgnoreCase))
                {
                    //label_image_info = polyMasterImage.fullFileName;
                }
                else
                {
                    MessageBox.Show("File format not supported !", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    polyMasterImage = null;
                }
            }
            
            
            /*if (op != null)
            {
                polyMasterImage.Source = op.openImageFile(false)[0];
            }
            else
            {
                MessageBox.Show("Format file haruslah gambar!", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                op = null;
            }*/
            
            /*OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";  
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +  
                        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +  
                        "Portable Network Graphic (*.png)|*.png";  
            if (op.ShowDialog() == true)
            {
                Image imgPhoto = new Image();
                imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
                imageText = op.SafeFileName;
            }  */
        }
    }
}