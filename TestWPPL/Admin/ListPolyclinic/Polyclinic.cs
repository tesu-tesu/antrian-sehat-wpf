using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWPPL.Admin.ListPolyclinic
{
    public class Schedule
    {
        public int id { get; set; }
        public int polyclinic_id { get; set; }
        public int day { get; set; }
        public string time_open { get; set; }
        public string time_close { get; set; }
        //public DateTime? created_at { get; set; }
        //public DateTime? updated_at { get; set; }
        public string charOfDay { get; set; }
        public string date { get; set; }
    }

    public class PolyMaster
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Polyclinic
    {
        public int id { get; set; }
        public int poly_master_id { get; set; }
        public int health_agency_id { get; set; }
        public object created_at { get; set; }
        public object updated_at { get; set; }
        public List<Schedule> sorted { get; set; }
        public PolyMaster poly_master { get; set; }
    }

    public class Root
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<Polyclinic> data { get; set; }
    }
}
