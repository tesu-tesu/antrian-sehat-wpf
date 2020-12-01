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
        public CreateUserController(IMyView _myView) : base(_myView)
        {

        }
        public async void fetchDataHealthAgency()
        {
            ApiClient client = ApiAntrianSehat.getInstance().GetApiClient();
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
            Console.WriteLine("DATAAAA : " + _response.getJObject());
            List<HealthAgency> healthAgencies = _response.getParsedObject<RootHealthAgency2>().data;

            getView().callMethod("setComboBox", healthAgencies);
        }

        private void setViewErrorFetchHealthAgency(HttpResponseBundle _response)
        {
            Console.WriteLine("err: " + _response.getHttpResponseMessage().Content.ReadAsStringAsync().Result);
        }

        public async void store(String name, String email, String phone)
        {

        }
    }
}
