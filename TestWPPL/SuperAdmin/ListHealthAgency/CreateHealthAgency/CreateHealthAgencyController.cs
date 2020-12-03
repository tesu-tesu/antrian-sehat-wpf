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
                .setEndpoint("admin/health-agency")
                .setRequestMethod(HttpMethod.Post);
            client.setOnSuccessRequest(setSuccessStoreHealthAgency);
            client.setOnFailedRequest(setErrorStoreHealthAgency);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setSuccessStoreHealthAgency(HttpResponseBundle _response)
        {
            string message = _response.getHttpResponseMessage().Content.ReadAsStringAsync().Result;
            Console.WriteLine("sukses: " + _response.getJObject());

            HealthAgency healthAgency = _response.getParsedObject<RootSingleHealthAgency>().data;
            getView().callMethod("successStore", healthAgency);
        }

        private void setErrorStoreHealthAgency(HttpResponseBundle _response)
        {
            string message = _response.getHttpResponseMessage().Content.ReadAsStringAsync().Result;
            Console.WriteLine("error: " + _response.getJObject());
            getView().callMethod("setErrorStore", message);
        }
    }
}
