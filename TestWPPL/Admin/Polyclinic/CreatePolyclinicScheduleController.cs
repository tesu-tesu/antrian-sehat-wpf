using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Velacro.Api;
using Velacro.Basic;

namespace TestWPPL.Admin.Polyclinic
{
    public class CreatePolyclinicScheduleController : MyController
    {
        ApiClient client;

        public CreatePolyclinicScheduleController(IMyView _myView) : base(_myView)
        {

        }

        public async void fetchDataPolyMaster()
        {
            ApiClient client = ApiAntrianSehat.getInstance().GetApiClient();
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("admin/poly-master/all")
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewSuccessFetch);
            client.setOnFailedRequest(setViewErrorFetch);
            var response = await client.sendRequest(request.getApiRequestBundle());

        }

        private void setViewErrorFetch(HttpResponseBundle _response)
        {
            Console.WriteLine("err: " + _response.getHttpResponseMessage().Content.ReadAsStringAsync().Result);
        }

        private void setViewSuccessFetch(HttpResponseBundle _response)
        {
            Console.WriteLine("sukses: " + _response.getJObject());

            List<PolyMaster> polyMasters = _response.getParsedObject<Root>().data;
            
            getView().callMethod("setDataPolyMaster", polyMasters);
        }

        public void SaveAllSchedules(List<ScheduleTime> _schedules)
        {
            foreach (ScheduleTime schedule in _schedules)
                SaveSchedule(sc);
        }
        public async void SaveSchedule(ScheduleTime schedules)
        {
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
        private void setViewSuccessLogin(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                Console.WriteLine("role: " + _response.getJObject()["user"]["role"]);
                String role = _response.getJObject()["user"]["role"].ToString();
                client.setAuthorizationToken(_response.getJObject()["access_token"].ToString());

                string status = _response.getHttpResponseMessage().ReasonPhrase;
                if (role == "Admin" || role == "Super Admin")
                {
                    getView().callMethod("setLoginSuccess", status, role);
                }
                else
                {
                    getView().callMethod("restrictNoAuthentication", status);
                }
            }
        }
    }
}
