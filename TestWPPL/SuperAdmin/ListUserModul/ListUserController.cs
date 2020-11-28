using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestWPPL.SuperAdmin;
using Velacro.Api;
using Velacro.Basic;
using TestWPPL.SuperAdmin;

namespace TestWPPL.SuperAdmin
{
    public class ListUserController : MyController
    {
        public ListUserController(IMyView _myView) : base(_myView)
        {
            
        }

        public async void fetchDataUser()
        {
            ApiClient client = ApiAntrianSehat.getInstance().GetApiClient();
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("admin/get-admin-user")
                .setRequestMethod(HttpMethod.Get);
            client.setOnSuccessRequest(setViewSuccessFetch);
            client.setOnFailedRequest(setViewErrorFetch);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setViewSuccessFetch(HttpResponseBundle _response)
        {
            Console.WriteLine("sukses: " + _response.getJObject());
            List<User> users = _response.getParsedObject<Root>().data;

            getView().callMethod("setListView", users);
        }

        private void setViewErrorFetch(HttpResponseBundle _response)
        {
            Console.WriteLine("err: " + _response.getHttpResponseMessage().Content.ReadAsStringAsync().Result);
        }
    }

}
