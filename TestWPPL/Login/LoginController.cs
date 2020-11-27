using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Velacro.Api;
using Velacro.Basic;

namespace TestWPPL.Login {
    public class LoginController : MyController{
        ApiClient client;
        public LoginController(IMyView _myView) : base(_myView){
            
        }

        public async void login(string _email, string _password) {
            client = new ApiClient("http://127.0.0.1:8000/api/");
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
                Console.WriteLine("role: " + _response.getJObject()["user"]["role"]);
                String role = _response.getJObject()["user"]["role"].ToString();
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
