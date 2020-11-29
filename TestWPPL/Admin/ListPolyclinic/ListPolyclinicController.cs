using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Velacro.Api;
using Velacro.Basic;

namespace TestWPPL.Admin.ListPolyclinic
{
    class ListPolyclinicController : MyController
    {
        public ListPolyclinicController(IMyView _myView) : base(_myView)
        {

        }

        public async void fetchDataPolyclinic()
        {
            ApiClient client = ApiAntrianSehat.getInstance().GetApiClient();
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("admin/health-agency/6/polyclinic")
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

            List<Polyclinic> polyclinics= _response.getParsedObject<Root>().data;
            foreach(Polyclinic polyclinic in polyclinics)
            {
                List<Schedule> schedules = new List<Schedule>();
                for(int i = 0; i < 7; i++)
                {
                    Schedule schedule = new Schedule();
                    var isExist = polyclinic.sorted.Find(x => x.day == i);
                    if (isExist == null)
                    {
                        schedule.id = -1;
                        schedule.polyclinic_id = -1;
                        schedule.day = i;
                        schedule.time_open = "-";
                        schedule.time_close = "-";
                        schedule.charOfDay = "-";
                        schedule.date = "-";
                    }
                    else
                        schedule = isExist;
                    schedules.Add(schedule);
                }
                polyclinic.sorted = schedules;
            }
            getView().callMethod("setListView", polyclinics);
        }
    }
}
