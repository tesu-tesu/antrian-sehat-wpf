using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWPPL.Admin.Polyclinic
{
    class ScheduleTime
    {
        private String day;
        private String timeOpen;
        private String timeClose;

        public ScheduleTime(String day, String timeOpen, String timeClose)
        {
            this.day = day;
            this.timeOpen = timeOpen;
            this.timeClose = timeClose;
        }

        public ScheduleTime(string timeOpen, string timeClose)
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
}
