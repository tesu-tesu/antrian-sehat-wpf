using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWPPL.Admin.CreatePolyclinicSchedule
{
    public class PolyMaster
    {
        public int id { get; set; }
        public String name { get; set; }
    }

    public class RootPoly
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<PolyMaster> data { get; set; }
    }
}
