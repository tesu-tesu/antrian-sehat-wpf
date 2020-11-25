using System.Windows.Controls;
using TestWPPL.Login;
using Velacro.DataStructures;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.TextBox;

namespace TestWPPL.Register {
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : MyPage {
        public RegisterPage() {
            InitializeComponent();
            this.KeepAlive = true;
            setController(new RegisterController(this));
            initUIBuilders();
            initUIElements();
        }

        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private BuilderTextBlock txtBlockBuilder;
        
        private void initUIBuilders() {
            buttonBuilder = new BuilderButton();
            txtBoxBuilder = new BuilderTextBox();
            txtBlockBuilder = new BuilderTextBlock();
        }

        private IMyButton registerButton;
        private IMyTextBox emailTxtBox;
        private IMyTextBox nameTxtBox;
        private IMyTextBox passwordTxtBox;
        private IMyTextBox passwordcTxtBox;
        private IMyTextBlock registerStatusTxtBlock;

        private void initUIElements() {
            registerButton = buttonBuilder.activate(this, "register_btn")
                .addOnClick(this, "onRegisterButtonClick");
            nameTxtBox = txtBoxBuilder.activate(this, "name_txt");
            emailTxtBox = txtBoxBuilder.activate(this, "email_txt");
            passwordTxtBox = txtBoxBuilder.activate(this, "password_txt");
            passwordcTxtBox = txtBoxBuilder.activate(this, "passwordc_txt");
            registerStatusTxtBlock = txtBlockBuilder.activate(this, "registerStatus");
        }

        public void onRegisterButtonClick() {
            getController().callMethod("register",  
                nameTxtBox.getText(), 
                emailTxtBox.getText(), 
                passwordTxtBox.getText(), 
                passwordcTxtBox.getText());
        }

        public void setRegisterStatus(string _status) {
            this.Dispatcher.Invoke(() => {
                registerButton.setText(_status);
            });

        }
    }
}
