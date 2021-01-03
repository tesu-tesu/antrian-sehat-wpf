using System.Windows;
using System.Windows.Controls;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBox;

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
    }
}