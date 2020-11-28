using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWPPL.SuperAdmin
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        //public object residence_number { get; set; }
        public object profile_img { get; set; }
        public int health_agency_id { get; set; }
        public string role { get; set; }
        //public DateTime created_at { get; set; }
        //public DateTime updated_at { get; set; }
    }

    public class Root
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<User> data { get; set; }
    }
}
