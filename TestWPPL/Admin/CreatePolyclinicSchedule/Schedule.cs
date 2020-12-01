using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWPPL.Admin.CreatePolyclinicSchedule
{
    public class Schedule
    {
        public int id { get; set; }
        public String day { get; set; }
        public String timeOpen { get; set; }
        public String timeClose { get; set; }
        
        public Schedule()
        {
        }

        public Schedule(string timeOpen, string timeClose)
        {
            this.timeOpen = timeOpen;
            this.timeClose = timeClose;
        }

        public void setDay(String day)
        {
            this.day = day;
        } 

        public String getDay()
        {
            return day;
        }

        public String getTimeOpen()
        {
            return timeOpen;
        }

        public String getTimeClose()
        {
            return timeClose;
        }
    }

    public class RootSchedule
    {
        public bool success { get; set; }
        public string message { get; set; }
        public Schedule data { get; set; }
    }
}
