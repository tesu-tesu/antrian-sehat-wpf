using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWPPL.Admin.CreatePolyclinicSchedule
{
    public class Polyclinic
    {
        public int id { get; set; }
        public int poly_master_id { get; set; }
        public int health_agency_id { get; set; }
    }

    public class RootPolyclinic
    {
        public bool success { get; set; }
        public string message { get; set; }
        public Polyclinic data { get; set; }
    }
}
