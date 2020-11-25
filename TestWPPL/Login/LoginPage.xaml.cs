using System;
using System.Net.Http;
using System.Windows.Controls;
using TestWPPL.Register;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.TextBox;

namespace TestWPPL.Login {

    public partial class LoginPage : MyPage {
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private BuilderTextBlock txtBlockBuilder;
        private IMyButton loginButton;
        private IMyTextBox emailTxtBox;
        private IMyTextBox passwordTxtBox;
        private IMyTextBlock loginStatusTxtBlock;

        public LoginPage() {
            InitializeComponent();
            this.KeepAlive = true;
            setController(new LoginController(this));
            initUIBuilders();
            initUIElements();
        }

        private void initUIBuilders(){
            buttonBuilder = new BuilderButton();
            txtBoxBuilder = new BuilderTextBox();
            txtBlockBuilder = new BuilderTextBlock();
        }

        private void initUIElements(){
            loginButton = buttonBuilder
                .activate(this, "loginButton_btn")
                .addOnClick(this, "onLoginButtonClick");
            emailTxtBox = txtBoxBuilder.activate(this, "email_txt");
            passwordTxtBox = txtBoxBuilder.activate(this, "password_txt");
            loginStatusTxtBlock = txtBlockBuilder.activate(this, "loginStatus");
        }

        public void onLoginButtonClick() {
            getController().callMethod("login", email_txt.Text, password_txt.Text);
        }


        public void setLoginStatus(string _status){
            this.Dispatcher.Invoke(() =>
            {
                loginButton.setText(_status);
            });
        }
    }
}
