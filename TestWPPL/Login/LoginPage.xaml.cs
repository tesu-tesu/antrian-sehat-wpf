using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using TestWPPL.Register;
using Velacro.Api;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.PasswordBox;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.TextBox;

namespace TestWPPL.Login {

    public partial class LoginPage : MyWindow {
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private BuilderTextBlock txtBlockBuilder;
        private BuilderPasswordBox passBoxBuilder;
        private IMyButton loginButton;
        private IMyTextBox emailTxtBox;
        private IMyPasswordBox passwordTxtBox;
        //private IMyTextBlock loginStatusTxtBlock;
        private IMyTextBlock emailTxtBlock, passTxtBlock;
        private ApiClient client;

        public LoginPage() {
            InitializeComponent();
            setController(new LoginController(this));
            initUIBuilders();
            initUIElements();
            checkAuth();
        }

        private void checkAuth()
        {
            client = ApiAntrianSehat.getInstance().GetApiClient();
            String path = System.AppDomain.CurrentDomain.BaseDirectory + "../../assets/user.txt";
            if (File.Exists(path))
            {
                String strFile = File.ReadAllText("../../assets/user.txt");
                string[] userFile = strFile.Split(',');
                
                Application.Current.Resources["email"] = userFile[0];
                if(userFile[3] != null)
                    Application.Current.Resources["ha_id"] = userFile[3];
                client.setAuthorizationToken(userFile[1]);
                Console.WriteLine(userFile[2]);
                redirectBasedOnRole(userFile[2]);
            }
        }

        private void initUIBuilders(){
            buttonBuilder = new BuilderButton();
            txtBoxBuilder = new BuilderTextBox();
            txtBlockBuilder = new BuilderTextBlock();
            passBoxBuilder = new BuilderPasswordBox();
        }

        private void initUIElements(){
            loginButton = buttonBuilder
                .activate(this, "loginButton_btn")
                .addOnClick(this, "onLoginButtonClick");
            emailTxtBox = txtBoxBuilder.activate(this, "email_txt");
            passwordTxtBox = passBoxBuilder.activate(this, "password_box");
            //loginStatusTxtBlock = txtBlockBuilder.activate(this, "loginStatus");
            emailTxtBlock = txtBlockBuilder.activate(this, "email_error_txt");
            passTxtBlock = txtBlockBuilder.activate(this, "password_error_txt");
        }


        public void onLoginButtonClick() {
            getController().callMethod("login", email_txt.Text, password_box.Password.ToString());
        }

        public void setLoginSuccess(string _status, string role){
            this.Dispatcher.Invoke(() =>    //jika method ini dipanggil scr async maka pakai dispatcher
            {
                loginButton.setText(_status);
                emailTxtBlock.setText("");
                passTxtBlock.setText("");

                redirectBasedOnRole(role);
            });
        }

        private void redirectBasedOnRole(String role)
        {
            if (role == "Admin")
                new AdminWindow().Show();
            else if (role == "Super Admin")
                new SuperAdminWindow().Show();
            this.Close();
        }

        public void restrictNoAuthentication(string _status)
        {
            this.Dispatcher.Invoke(() =>   
            {
                emailTxtBlock.setText("You're not an Admin nor Super Admin");
            });
        }

        public void setErrorLogin(string errorMessage)
        {
            JObject messageJSon = JObject.Parse(errorMessage);
            String emailError = "";
            String passError = "";

            if (messageJSon["email"] != null) {
                emailError += messageJSon["email"][0].ToString();
            } else {
                emailError += "username or email incorrect";
            }
            if (messageJSon["password"] != null) {
                passError += messageJSon["password"][0].ToString();
            }

            Console.WriteLine("msg: " + emailError + " " + passError);
            
            this.Dispatcher.Invoke(() =>
            {
                emailTxtBlock.setText(emailError);
                passTxtBlock.setText(passError);
            });
            
        }

        private void password_box_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {

        }
    }
}
