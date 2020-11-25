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
        public LoginController(IMyView _myView) : base(_myView){
            
        }

        public async void login(string _email, string _password) {
            var client = new ApiClient("http://127.0.0.1:8000/");
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .addParameters("email", _email)
                .addParameters("password", _password)
                .setEndpoint("api/login/")
                .setRequestMethod(HttpMethod.Post);
            client.setOnSuccessRequest(setViewLoginStatus);
            var response = await client.sendRequest(request.getApiRequestBundle());
            Console.WriteLine(response.getJObject()["access_token"]);
            client.setAuthorizationToken(response.getJObject()["access_token"].ToString());
        }

        //method yg dijalankan saat request success harus memiliki parameter bertipe HttpResponseBundle
        private void setViewLoginStatus(HttpResponseBundle _response){
            if (_response.getHttpResponseMessage().Content != null) {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setLoginStatus", status);
            }
        }
    }
}
