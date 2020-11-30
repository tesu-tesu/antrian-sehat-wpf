using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Velacro.UIElements.Basic;
using Velacro.UIElements.TextBox;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.CheckBox;
using Velacro.UIElements.ComboBox;

namespace TestWPPL.Admin.Polyclinic
{
    /// <summary>
    /// Interaction logic for CreatePolyclinicSchedule.xaml
    /// </summary>
    public partial class CreatePolyclinicSchedule : MyPage
    {
        private BuilderCheckBox builderCheckBox;
        private BuilderTextBox builderTextBox;
        private BuilderComboBox builderComboBox;
        private BuilderTextBlock builderTextBlock;
        private Dictionary<String, ScheduleTime> schedules;
        private IMyComboBox polyMaster_CombB;

        public CreatePolyclinicSchedule()
        {
            InitializeComponent();
            this.KeepAlive = true;
            setController(new CreatePolyclinicScheduleController(this));
            builderComboBox = new BuilderComboBox();
            polyMaster_CombB = builderComboBox.activate(this, "polyMaster_CombB");
        }

        void Checked_Handler(object sender, RoutedEventArgs e)
        {
            string name = (sender as CheckBox).Name.ToString();
            ScheduleTime schedule = getValueTimePickers();

            if (name.Equals("Senin_Cb"))
                schedule.setDay("Senin");
            else if (name.Equals("Selasa_Cb"))
                schedule.setDay("Selasa");
            else if (name.Equals("Rabu_Cb"))
                schedule.setDay("Rabu");
            else if (name.Equals("Kamis_Cb"))
                schedule.setDay("Kamis");
            else if (name.Equals("Jumat_Cb"))
                schedule.setDay("Jumat");
            else if (name.Equals("Sabtu_Cb"))
                schedule.setDay("Sabtu");
            else if (name.Equals("Minggu_Cb"))
                schedule.setDay("Minggu");

            schedules.Add(schedule.getDay(), schedule);
        }

        ScheduleTime getValueTimePickers()
        {
            String timeOpen, timeClose;
            DateTime selectedTime;

            selectedTime = TimeOpen_Tp.SelectedTime.Value;
            timeOpen = selectedTime.ToString("HH:mm");
            selectedTime = TimeClose_Tp.SelectedTime.Value;
            timeClose = selectedTime.ToString("HH:mm");

            return new ScheduleTime(timeOpen, timeClose);
        }

        void Unchecked_Handler(object sender, RoutedEventArgs e)
        {
            string name = (sender as CheckBox).Name.ToString();
            if (name.Equals("Senin_Cb"))
                name = "Senin";
            else if (name.Equals("Selasa_Cb"))
                name = "Selasa";
            else if (name.Equals("Rabu_Cb"))
                name = "Rabu";
            else if (name.Equals("Kamis_Cb"))
                name = "Kamis";
            else if (name.Equals("Jumat_Cb"))
                name = "Jumat";
            else if (name.Equals("Sabtu_Cb"))
                name = "Sabtu";
            else if (name.Equals("Minggu_Cb"))
                name = "Minggu";

            schedules.Remove(name);
        }
    }
}
