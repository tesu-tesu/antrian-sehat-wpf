﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TestWPPL.SuperAdmin.ListHealthAgency.CreateHealthAgency;
using TestWPPL.SuperAdmin.ListHealthAgency.EditHealthAgency;
using Velacro.Api;
using Velacro.Basic;

namespace TestWPPL.SuperAdmin.ListHealthAgency
{
    class ListHealthAgencyController : MyController
    {
        public ListHealthAgencyController(IMyView _myView) : base(_myView)
        {

        }

        public async void fetchDataHealthAgency()
        {
            ApiClient client = ApiAntrianSehat.getInstance().GetApiClient();
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("admin/health-agency")
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
            Pagination healthAgencyPagination = _response.getParsedObject<RootHealthAgency>().data;

            getView().callMethod("setListView", healthAgencyPagination);
        }

        public void deleteProcess(Button btn)
        {
            HealthAgency dataObject = btn.DataContext as HealthAgency;

            Console.WriteLine("index: " + dataObject.id);

        }

        public void editProcess(Button btn)
        {
            HealthAgency dataObject = btn.DataContext as HealthAgency;
            Console.WriteLine("index: " + dataObject.id);

            EditHealthAgencyPage editPage = new EditHealthAgencyPage();
            editPage.idHA = dataObject.id;
            //FrameService.Frame.Navigate(editPage);
        }

    }
}
