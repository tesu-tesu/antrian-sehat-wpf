﻿using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Windows.Controls;
using TestWPPL.Register;
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

        public LoginPage() {
            InitializeComponent();
            setController(new LoginController(this));
            initUIBuilders();
            initUIElements();
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

                if (role == "Admin")
                    new AdminWindow().Show();
                else if (role == "Super Admin")
                    new SuperAdminWindow().Show();
                this.Close();
            });
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
