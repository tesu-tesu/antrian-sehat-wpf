using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using TestWPPL.Admin.CreatePolyclinicSchedule;
using Velacro.Api;
using Velacro.Basic;

namespace TestWPPL.SuperAdmin.ListPolyMaster.EditPolyMaster
{
    public class EditPolyMasterController : MyController
    {
        private ApiClient client;
        
        public EditPolyMasterController(IMyView _myView) : base(_myView)
        {
            
        }
        
        public EditPolyMasterController CreateInstance(IMyView _myView)
        {
            return new EditPolyMasterController(_myView);
        }
        
        public async void fetchPolyMasterData(int idPolyMaster)
        {
            client = ApiAntrianSehat.getInstance().GetApiClient();
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("admin/poly-master/" + idPolyMaster.ToString())
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewSuccessFetchPolyMaster);
            client.setOnFailedRequest(setViewErrorFetchPolyMaster);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setViewErrorFetchPolyMaster(HttpResponseBundle obj)
        {
            Console.WriteLine("err: " + obj.getHttpResponseMessage()
                .Content.ReadAsStringAsync().Result);
        }

        private void setViewSuccessFetchPolyMaster(HttpResponseBundle _response)
        {
            PolyMaster polyMaster = _response.getParsedObject<RootSinglePolyMaster>().data;
            getView().callMethod("setPolyMasterData", polyMaster);
        }

        public async void updatePolyMaster(int id, String name)
        {
            var request = new ApiRequestBuilder();
            var req = request.buildHttpRequest()
                .addParameters("_method", "PUT")
                .addParameters("name", name)
                .setEndpoint("admin/poly-master/" + id.ToString())
                .setRequestMethod(HttpMethod.Post);
            client.setOnSuccessRequest(setSuccessStorePolyMaster);
            client.setOnFailedRequest(setErrorStorePolyMaster);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setErrorStorePolyMaster(HttpResponseBundle _response)
        {
            string message = _response.getHttpResponseMessage().Content.ReadAsStringAsync().Result;
            Console.WriteLine("error: " + _response.getJObject());
            getView().callMethod("setErrorStore", message);
        }

        private void setSuccessStorePolyMaster(HttpResponseBundle _response)
        {
            string message = _response.getHttpResponseMessage().Content.ReadAsStringAsync().Result;
            Console.WriteLine("sukses: " + _response.getJObject());

            PolyMaster polyMaster = _response.getParsedObject<RootSinglePolyMaster>().data;
            getView().callMethod("successStore", polyMaster);
        }
    }
}