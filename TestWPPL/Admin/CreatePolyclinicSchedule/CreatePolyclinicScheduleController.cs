using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Velacro.Api;
using Velacro.Basic;

namespace TestWPPL.Admin.Polyclinic
{
    public class CreatePolyclinicScheduleController : MyController
    {
        ApiClient client;

        public CreatePolyclinicScheduleController(IMyView _myView) : base(_myView)
        {

        }

        public async void fetchDataPolyMaster()
        {
            client = ApiAntrianSehat.getInstance().GetApiClient();
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("admin/poly-master/all")
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

            List<PolyMaster> polyMasters = _response.getParsedObject<RootPoly>().data;

            getView().callMethod("setComboBox", polyMasters);
        }

        public async void CreatePolyclinic(int _polyMasterId)
        {
            var request = new ApiRequestBuilder();
            
            var req = request
                .buildHttpRequest()
                .addParameters("poly_master_id", _polyMasterId.ToString())
                .addParameters("health_agency_id", "1")
                .setEndpoint("admin/polyclinic/")
                .setRequestMethod(HttpMethod.Post);
            client.setOnSuccessRequest(setSuccessCreatePolyclinic);
            client.setOnFailedRequest(setErrorCreatePolyclinic);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        public void SaveAllSchedules(int _polyclinicId, Dictionary<String, Schedule> _schedules)
        {
            foreach (Schedule schedule in _schedules.Values)
                SaveSchedule(_polyclinicId, schedule);
        }

        public async void SaveSchedule(int _polyclinicId, Schedule _schedules)
        {
            client = ApiAntrianSehat.getInstance().GetApiClient();
            Console.WriteLine("client: " + client.GetType().ToString());
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .addParameters("day", _schedules.getDay())
                .addParameters("time_open", _schedules.getTimeOpen())
                .addParameters("time_close", _schedules.getTimeClose())
                .addParameters("polyclinic", _polyclinicId.ToString())
                .setEndpoint("admin/schedule/")
                .setRequestMethod(HttpMethod.Post);
            client.setOnSuccessRequest(setSuccessCreateSchedule);
            client.setOnFailedRequest(setErrorCreateSchedule);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setSuccessCreateSchedule(HttpResponseBundle _response)
        {
            Console.WriteLine("sukses: " + _response.getJObject());

            Schedule schedule = _response.getParsedObject<RootSchedule>().data;

            getView().callMethod("setSuccessCreateSchedule", schedule);
        }

        private void setErrorCreateSchedule(HttpResponseBundle _response)
        {
            Console.WriteLine("error: " + _response.getJObject());
        }

        private void setErrorCreatePolyclinic(HttpResponseBundle _response)
        {
            Console.WriteLine("error: " + _response.getJObject());
        }

        private void setSuccessCreatePolyclinic(HttpResponseBundle _response)        
        {
            Console.WriteLine("sukses: " + _response.getJObject());

            Polyclinic polyclinic = _response.getParsedObject<RootPolyclinic>().data;

            getView().callMethod("setSuccessCreatePolyclinic", polyclinic);
        }

    }
}
