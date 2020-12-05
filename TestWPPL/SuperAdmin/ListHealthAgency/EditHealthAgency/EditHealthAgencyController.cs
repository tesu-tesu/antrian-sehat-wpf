using System;
using System.Net.Http;
using Velacro.Api;
using Velacro.Basic;

namespace TestWPPL.SuperAdmin.ListHealthAgency.EditHealthAgency
{
    public class EditHealthAgencyController : MyController
    {
        private ApiClient client;
        public EditHealthAgencyController CreateInstance(IMyView _myView)
        {
            return new EditHealthAgencyController(_myView);
        }

        public EditHealthAgencyController(IMyView _myView) : base(_myView)
        {
        }
        
        public async void fetchHAData(int idHA)
        {
            client = ApiAntrianSehat.getInstance().GetApiClient();
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("admin/health-agency/" + idHA.ToString())
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewSuccessFetchHA);
            client.setOnFailedRequest(setViewErrorFetchHA);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setViewErrorFetchHA(HttpResponseBundle obj)
        {
            Console.WriteLine("err: " + obj.getHttpResponseMessage()
                .Content.ReadAsStringAsync().Result);
        }

        private void setViewSuccessFetchHA(HttpResponseBundle _response)
        {
            HealthAgency healthAgency = _response.getParsedObject<RootSingleHealthAgency>().data;
            getView().callMethod("setHAData");
        }

        public async void updateHA(int id, String name, String email, String address, String call_center)
        {
            var request = new ApiRequestBuilder();
            var req = request
                .buildHttpRequest()
                .addParameters("_method", "PUT")
                .addParameters("name", name)
                .addParameters("email", email)
                .addParameters("address", address)
                .addParameters("call_center", call_center)
                .addParameters("role", "Admin")
                .setEndpoint("admin/health-agency/" + id.ToString())
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