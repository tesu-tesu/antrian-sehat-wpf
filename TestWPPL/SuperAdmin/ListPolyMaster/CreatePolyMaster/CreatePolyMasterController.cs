﻿using System;
using System.Net.Http;
using TestWPPL.Admin.CreatePolyclinicSchedule;
using TestWPPL.SuperAdmin.ListPolyMaster;
using Velacro.Api;
using Velacro.Basic;

namespace TestWPPL.SuperAdmin.ListPolyMaster.CreatePolyMaster
{
    public class CreatePolyMasterController : MyController
    {
        public CreatePolyMasterController(IMyView _myView) : base(_myView)
        {
        }
        
        public CreatePolyMasterController CreateInstance(IMyView _myView)
        {
            return new CreatePolyMasterController(_myView);
        }
        
        public async void storePolyMasterData(String name, String email, String address, String callCenter)
        {

            ApiClient client = ApiAntrianSehat.getInstance().GetApiClient();
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .addParameters("name", name)
                .setEndpoint("admin/poly-master")
                .setRequestMethod(HttpMethod.Post);
            client.setOnSuccessRequest(setSuccessStorePolyMaster);
            client.setOnFailedRequest(setErrorStorePolyMaster);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setSuccessStorePolyMaster(HttpResponseBundle _response)
        {
            string message = _response.getHttpResponseMessage().Content.ReadAsStringAsync().Result;
            Console.WriteLine("sukses: " + _response.getJObject());

            PolyMaster polyMaster = _response.getParsedObject<RootSinglePolyMaster>().data;
            getView().callMethod("successStore", polyMaster);
        }

        private void setErrorStorePolyMaster(HttpResponseBundle _response)
        {
            string message = _response.getHttpResponseMessage().Content.ReadAsStringAsync().Result;
            Console.WriteLine("error: " + _response.getJObject());
            getView().callMethod("setErrorStore", message);
        }
    }
}