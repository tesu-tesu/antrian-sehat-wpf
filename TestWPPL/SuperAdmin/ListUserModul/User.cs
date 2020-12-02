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
        public string residence_number { get; set; }
        public object profile_img { get; set; }
        public int health_agency_id { get; set; }
        public string role { get; set; }
        public HealthAgencyOfUser health_agency { get; set; }
        //public DateTime created_at { get; set; }
        //public DateTime updated_at { get; set; }
    }

    public class HealthAgencyOfUser
    {
        public int id { get; set; }
        public String name { get; set; }
        public String address { get; set; }
        public object image { get; set; }
        public String call_center { get; set; }
        public String email { get; set; }
        //public DateTimeOffset CreatedAt { get; set; }
        //public DateTimeOffset UpdatedAt { get; set; }
    }

    public class RootUser
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<User> data { get; set; }
    }

    public class RootSingleUser
    {
        public bool success { get; set; }
        public string message { get; set; }
        public User data { get; set; }
    }
}
