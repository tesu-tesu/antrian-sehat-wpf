using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Velacro.Basic;

namespace TestWPPL.SuperAdmin.ListHealthAgency
{
    public class HealthAgency
    {
        public int id { get; set; }
        public String name { get; set; }
        public String address { get; set; }
        public object image { get; set; }
        public String call_center { get; set; }
        public String email { get; set; }
        //public DateTime created_at { get; set; }
        //public DateTime updated_at { get; set; }
    }

    public class Pagination
    {
        public int current_page { get; set; }
        public List<HealthAgency> data { get; set; }
        public string first_page_url { get; set; }
        public int from { get; set; }
        public int last_page { get; set; }
        public string last_page_url { get; set; }
        public object next_page_url { get; set; }
        public string path { get; set; }
        public int per_page { get; set; }
        public string prev_page_url { get; set; }
        public int to { get; set; }
        public int total { get; set; }
    }

    public class RootHealthAgency
    {
        public bool success { get; set; }
        public string message { get; set; }
        public Pagination data { get; set; }
    }

    public class RootHealthAgency2
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<HealthAgency> data { get; set; }
    }

    public class RootSingleHealthAgency
    {
        public bool success { get; set; }
        public string message { get; set; }
        public HealthAgency data { get; set; }
    }
}
