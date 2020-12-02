using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Velacro.Api;
using Velacro.Basic;

namespace TestWPPL.SuperAdmin.ListHealthAgency.CreateHealthAgency
{
    class CreateHealthAgencyController : MyController
    {
        public CreateHealthAgencyController(IMyView _myView) : base(_myView)
        {
        }

        public async void storeHealthAgencyData(String name, String email, String address, String callCenter)
        {
            ApiClient client = ApiAntrianSehat.getInstance().GetApiClient();
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .addParameters("name", name)
                .addParameters("email", email)
                .addParameters("address", address)
                .addParameters("call_center", callCenter)
                .setEndpoint("api/register/")
                .setRequestMethod(HttpMethod.Post);
            client.setOnSuccessRequest(setSuccessStoreHealthAgency);
            client.setOnFailedRequest(setErrorStoreHealthAgency);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setSuccessStoreHealthAgency(HttpResponseBundle _response)
        {

        }

        private void setErrorStoreHealthAgency(HttpResponseBundle _response)
        {
            Console.WriteLine("err: " + _response.getHttpResponseMessage().Content.ReadAsStringAsync().Result);
        }
    }
}
