using System;
using System.Net.Http;
using System.Windows.Controls;
using TestWPPL.Admin.CreatePolyclinicSchedule;
using TestWPPL.SuperAdmin.ListHealthAgency;
using TestWPPL.SuperAdmin.ListHealthAgency.EditHealthAgency;
using TestWPPL.SuperAdmin.ListPolyMaster.EditPolyMaster;
using Velacro.Basic;
using Velacro.Api;

namespace TestWPPL.SuperAdmin.ListPolyMaster
{
    public class ListPolyMasterController : MyController
    {
        private ApiClient client;
        
        public ListPolyMasterController(IMyView _myView) : base(_myView)
        {
            
        }
        
        public async void fetchDataPolyMaster()
        {
            client = ApiAntrianSehat.getInstance().GetApiClient();
            var request = new ApiRequestBuilder();

            var req = request.buildHttpRequest()
                .setEndpoint("admin/poly-master/")
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewSuccessFetch);
            client.setOnFailedRequest(setViewFailedFetch);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setViewFailedFetch(HttpResponseBundle obj)
        {
            Console.WriteLine("err: " + obj.getHttpResponseMessage().Content.ReadAsStringAsync().Result);
        }

        private void setViewSuccessFetch(HttpResponseBundle _response)
        {
            Pagination polyMasterPagination = _response.getParsedObject<RootPolyMasterPagination>().data;
            getView().callMethod("setListView", polyMasterPagination);
        }
        
        public async void deleteProcess(PolyMaster polyMaster)
        {
            
            ApiClient client = ApiAntrianSehat.getInstance().GetApiClient();
            var request = new ApiRequestBuilder();

            var req = request.buildHttpRequest()
                .setEndpoint("admin/poly-master/" + polyMaster.id)
                .setRequestMethod(HttpMethod.Delete);
            client.setOnSuccessRequest(setViewSuccessDelete);
            client.setOnFailedRequest(setViewErrorDelete);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setViewErrorDelete(HttpResponseBundle obj)
        {
            Console.WriteLine("err: " + obj.getHttpResponseMessage().Content.ReadAsStringAsync().Result);
        }

        private void setViewSuccessDelete(HttpResponseBundle obj)
        {
            getView().callMethod("setSuccessDelete", "Sukses menghapus Poly Master");
        }
        
        public async void editProcess(Button btn)
        {
            PolyMaster dataObject = btn.DataContext as PolyMaster;
            Console.WriteLine("index: " + dataObject.id);

            EditPolyMasterPage editPage = new EditPolyMasterPage();
            editPage.idPolyMaster = dataObject.id;
            //FrameService.Frame.Navigate(editPage);
        }
    }
}