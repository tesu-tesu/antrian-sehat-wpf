using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestWPPL.SuperAdmin.ListHealthAgency;
using Velacro.Api;
using Velacro.Basic;

namespace TestWPPL.SuperAdmin.ListUserModul.EditUser
{
    public class EditUserController : MyController
    {
        private ApiClient client;
        
        public EditUserController(IMyView _myView) : base(_myView)
        {

        }

        public async void fetchUserData(int idUser)
        {
            client = ApiAntrianSehat.getInstance().GetApiClient();
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("user/" + idUser.ToString())
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewSuccessFetchUser);
            client.setOnFailedRequest(setViewErrorFetchUser);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setViewErrorFetchUser(HttpResponseBundle _response)
        {
            Console.WriteLine("err: " + _response.getHttpResponseMessage()
                .Content.ReadAsStringAsync().Result);
        }

        private void setViewSuccessFetchUser(HttpResponseBundle _response)
        {
            User user = _response.getParsedObject<RootSingleUser>().data;

            getView().callMethod("setUserData", user);
        }

        public async void fetchDataHealthAgency()
        {
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("admin/health-agency/all")
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewSuccessFetchHealthAgency);
            client.setOnFailedRequest(setViewErrorFetchHealthAgency);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setViewSuccessFetchHealthAgency(HttpResponseBundle _response)
        {
            List<HealthAgency> healthAgencies = _response.getParsedObject<RootHealthAgency2>().data;

            getView().callMethod("setComboBox", healthAgencies);
        }

        private void setViewErrorFetchHealthAgency(HttpResponseBundle _response)
        {
            Console.WriteLine("err: " + _response.getHttpResponseMessage().Content.ReadAsStringAsync().Result);
        }

        public async void update(int id, String name, String email, String phone, String residence_number, int health_agency_id)
        {
            var request = new ApiRequestBuilder();
            var req = request
                .buildHttpRequest()
                .addParameters("_method", "PUT")
                .addParameters("name", name)
                .addParameters("email", email)
                .addParameters("phone", phone)
                .addParameters("residence_number", residence_number)
                .addParameters("health_agency", health_agency_id.ToString())
                .addParameters("role", "Admin")
                .setEndpoint("user/" + id.ToString())
                .setRequestMethod(HttpMethod.Post);
            client.setOnSuccessRequest(setSuccessUpdateUser);
            client.setOnFailedRequest(setErrorUpdateUser);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setSuccessUpdateUser(HttpResponseBundle _response)
        {
            User user = _response.getParsedObject<RootSingleUser>().data;

            getView().callMethod("successUpdate", user);
        }

        private void setErrorUpdateUser(HttpResponseBundle _response)
        {
            Console.WriteLine("err: " + _response.getHttpResponseMessage().Content.ReadAsStringAsync().Result);
        }
    }
}