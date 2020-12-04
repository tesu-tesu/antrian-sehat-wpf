using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Velacro.Api;
using Velacro.Basic;
using Velacro.LocalFile;

namespace TestWPPL.Login {
    public class LoginController : MyController{
        ApiClient client;
        public LoginController(IMyView _myView) : base(_myView){
            
        }

        public async void login(string _email, string _password) {
            client = ApiAntrianSehat.getInstance().GetApiClient();
            Console.WriteLine("client: " + client.GetType().ToString());
            var request = new ApiRequestBuilder();
            
            var req = request
                .buildHttpRequest()
                .addParameters("email", _email)
                .addParameters("password", _password)
                .setEndpoint("auth/login/")
                .setRequestMethod(HttpMethod.Post);
            client.setOnSuccessRequest(setViewSuccessLogin);
            client.setOnFailedRequest(setViewErrorLogin);
            var response = await client.sendRequest(request.getApiRequestBundle());
            //string content = await response.getHttpResponseMessage().Content.ReadAsStringAsync();

        }

        private void setViewErrorLogin(HttpResponseBundle _response)
        {
            string message = _response.getHttpResponseMessage().Content.ReadAsStringAsync().Result;
            Console.WriteLine("error: " + _response.getJObject());
            getView().callMethod("setErrorLogin", message);
        }

        //method yg dijalankan saat request success harus memiliki parameter bertipe HttpResponseBundle
        private void setViewSuccessLogin(HttpResponseBundle _response){
            if (_response.getHttpResponseMessage().Content != null) {
                Application.Current.Resources["email"] = _response.getJObject()["user"]["email"];
                Application.Current.Resources["ha_id"] = _response.getJObject()["user"]["health_agency_id"];
                String role = _response.getJObject()["user"]["role"].ToString();

                String saveStr = _response.getJObject()["user"]["email"] + " "
                                + _response.getJObject()["access_token"].ToString() + " "
                                + _response.getJObject()["user"]["role"] + " "
                                + _response.getJObject()["user"]["health_agency_id"];

                TextOperation.writeToFile("../../assets/file/user.txt", saveStr);
                client.setAuthorizationToken(_response.getJObject()["access_token"].ToString());

                string status = _response.getHttpResponseMessage().ReasonPhrase;
                if (role == "Admin" || role == "Super Admin") {
                    getView().callMethod("setLoginSuccess", status, role);
                } else {
                    getView().callMethod("restrictNoAuthentication", status);
                }                
            }
        }
    }
}
