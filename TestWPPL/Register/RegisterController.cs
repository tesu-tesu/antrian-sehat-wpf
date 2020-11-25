using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Velacro.Api;
using Velacro.Basic;

namespace TestWPPL.Register {
    public class RegisterController : MyController{
        public RegisterController(IMyView _myView) : base(_myView) { }

        public async void register(
            string _name, 
            string _email, 
            string _password, 
            string _passwordc) {
            var client = new ApiClient("http://127.0.0.1:8000/");
            var request = new ApiRequestBuilder();

            string token = "";
            var req = request
                .buildHttpRequest()
                .addParameters("name", _name)
                .addParameters("email", _email)
                .addParameters("password", _password)
                .addParameters("password_confirmation", _passwordc)
                .setEndpoint("api/register/")
                .setRequestMethod(HttpMethod.Post);
            client.setOnSuccessRequest(setViewRegisterStatus);
            var response = await client.sendRequest(request.getApiRequestBundle());

        }

        private void setViewRegisterStatus(HttpResponseBundle _response) {
            if (_response.getHttpResponseMessage().Content != null) {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setRegisterStatus", status);
            }
        }

        
    }
}
