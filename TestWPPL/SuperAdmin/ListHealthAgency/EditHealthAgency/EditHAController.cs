using System;
using System.Net.Http;
using Velacro.Api;
using Velacro.Basic;

namespace TestWPPL.SuperAdmin.ListHealthAgency.EditHealthAgency
{
    public class EditHAController : MyController
    {
        private ApiClient client;
        public EditHAController CreateInstance(IMyView _myView)
        {
            return new EditHAController(_myView);
        }

        public EditHAController(IMyView _myView) : base(_myView)
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

        private void setViewSuccessFetchHA(HttpResponseBundle response)
        {
            HealthAgency healthAgency = response.getParsedObject<RootSingleHealthAgency>().data;
            getView().callMethod("setHAData");
        }

        public async void updateHA()
        {
            
        }
    }
}