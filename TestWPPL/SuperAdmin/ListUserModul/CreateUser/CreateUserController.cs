using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Velacro.Api;
using Velacro.Basic;
using TestWPPL.SuperAdmin.ListHealthAgency;

namespace TestWPPL.SuperAdmin.ListUserModul.CreateUser
{
    public class CreateUserController : MyController
    {
        private ApiClient client;
        private static Random random = new Random();
        String randomPassword;
        public CreateUserController(IMyView _myView) : base(_myView)
        {

        }
        public async void fetchDataHealthAgency()
        {
            client = ApiAntrianSehat.getInstance().GetApiClient();
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

        /*
         * @note : method store
         */
        private string getRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async void store(String name, String email, String phone, String residence_number, int health_agency_id)
        {
            var request = new ApiRequestBuilder();
            randomPassword = getRandomString(6);

            var req = request
                .buildHttpRequest()
                .addParameters("name", name)
                .addParameters("email", email)
                .addParameters("phone", phone)
                .addParameters("residence_number", residence_number)
                .addParameters("health_agency", health_agency_id.ToString())
                .addParameters("role", "Admin")
                .addParameters("password", randomPassword)
                .setEndpoint("user/")
                .setRequestMethod(HttpMethod.Post);
            client.setOnSuccessRequest(setSuccessStoreUser);
            client.setOnFailedRequest(setErrorStoreUser);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setSuccessStoreUser(HttpResponseBundle _response)
        {
            User user = _response.getParsedObject<RootSingleUser>().data;

            getView().callMethod("successStore", user, randomPassword);
        }

        private void setErrorStoreUser(HttpResponseBundle _response)
        {
            Console.WriteLine("err: " + _response.getHttpResponseMessage().Content.ReadAsStringAsync().Result);
        }
    }
}
