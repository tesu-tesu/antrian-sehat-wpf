using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWPPL.SuperAdmin.ListHealthAgency
{
    public class HealthAgency
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public object image { get; set; }
        public string call_center { get; set; }
        public string email { get; set; }
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

    public class Root
    {
        public bool success { get; set; }
        public string message { get; set; }
        public Pagination data { get; set; }
    }
}
