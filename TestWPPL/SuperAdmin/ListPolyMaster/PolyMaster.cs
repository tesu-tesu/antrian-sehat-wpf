using System;
using System.Collections.Generic;
using TestWPPL.Admin.CreatePolyclinicSchedule;
using TestWPPL.SuperAdmin.ListHealthAgency;

namespace TestWPPL.SuperAdmin.ListPolyMaster
{
    /*public class PolyMaster
    {
        public int id { get; set; }
        public String name { get; set; }
    }*/


    public class Pagination
    {
        public int current_page { get; set; }
        public List<PolyMaster> data { get; set; }
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

    public class RootPolyMasterPagination
    {
        public bool success { get; set; }
        public string message { get; set; }
        public Pagination data { get; set; }
    }
    
    public class RootSinglePolyMaster
    {
        public bool success { get; set; }
        public string message { get; set; }
        public PolyMaster data { get; set; }
    }
    
    public class RootPolyMasterList
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<PolyMaster> data { get; set; }
    }

}