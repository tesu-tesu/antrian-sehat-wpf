using System;
using System.Windows;
using System.Windows.Controls;
using TestWPPL.Admin.CreatePolyclinicSchedule;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBox;

namespace TestWPPL.SuperAdmin.ListPolyMaster.EditPolyMaster
{
    public partial class EditPolyMasterPage : MyPage
    {
        private BuilderButton buttonBuilder;
        private IMyTextBox nameTxtBox;
        private BuilderTextBox txtBoxBuilder;
        private IMyButton updateButton;

        public EditPolyMasterPage()
        {
            InitializeComponent();
            KeepAlive = true;
            initUIBuilders();
            initUIElements();
            setController(new EditPolyMasterController(this));
        }

        public TextBox NameTxt
        {
            get => name_txt;
            set => name_txt = value;
        }
        
        public int idPolyMaster { get; set; }

        private void initUIBuilders()
        {
            buttonBuilder = new BuilderButton();
            txtBoxBuilder = new BuilderTextBox();
        }

        private void initUIElements()
        {
            updateButton = buttonBuilder.activate(this, "updateHA_btn");
            nameTxtBox = txtBoxBuilder.activate(this, "name_txt");
        }

        public void fetchPolyMasterData()
        {
            getController().callMethod("fetchPolyMasterData", idPolyMaster);
        }
        
        public void setPolyMasterData(PolyMaster polyMaster)
        {
            this.Dispatcher.Invoke(() =>
            {
                name_txt.Text = polyMaster.name;
            });
        }
        
        private void btnUpdatePolyMaster(object sender, RoutedEventArgs e)
        {
            if (
                name_txt.Text == ""
            )
            {
                MessageBox.Show("Nama Master Poli harus diisi");
            }
            else
            {
                getController().callMethod("updatePolyMaster", idPolyMaster, name_txt.Text);
            }
        }

        public void successStore(PolyMaster polyMaster)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.NavigationService.GoBack();
                this.NavigationService.RemoveBackEntry();

                MessageBox.Show(
                    "Nama : " + polyMaster.name + Environment.NewLine
                );
            });
        }

        private void upload_btn(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}