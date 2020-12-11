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
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.ComboBox;
using Velacro.UIElements.Button;

namespace TestWPPL.Admin.CreatePolyclinicSchedule
{
    /// <summary>
    /// Interaction logic for CreatePolyclinicSchedule.xaml
    /// </summary>
    public partial class CreatePolyclinicSchedulePage : MyPage
    {
        private BuilderComboBox builderComboBox;
        private BuilderButton builderButton;
        private BuilderTextBlock builderTextBlock;
        private Dictionary<String, Schedule> schedules;
        private IMyComboBox polyMaster_CombB;
        private IMyButton savePoly_bt;
        private IMyButton saveSchedule_bt;
        private IMyTextBlock errorCreateSchedule_txt;
        private IMyTextBlock successCreatePolyclinic_txt;
        private int createdPoly = 0;
        private int polyclinicId = -1;
        private string polyName;

        public CreatePolyclinicSchedulePage()
        {
            InitializeComponent();
            this.KeepAlive = true;
            setController(new CreatePolyclinicScheduleController(this));

            schedules = new Dictionary<string, Schedule>();

            initUIBuilders();
            initUIElements();
        }

        private void initUIBuilders()
        {
            builderTextBlock = new BuilderTextBlock();
            builderComboBox = new BuilderComboBox();
            builderButton = new BuilderButton();
        }

        private void initUIElements()
        {
            errorCreateSchedule_txt = builderTextBlock.activate(this, "ErrorSchedule_txt");
            successCreatePolyclinic_txt = builderTextBlock.activate(this, "SuccessPoly_txt");
            polyMaster_CombB = builderComboBox.activate(this, "PolyMaster_CombB");
            savePoly_bt = builderButton.activate(this, "SavePoly_bt").addOnClick(this, "onClickSavePoly");
            saveSchedule_bt = builderButton.activate(this, "SaveSchedule_bt").addOnClick(this, "onClickSaveSchedule");
        }

        public void onClickSaveSchedule()
        {
            if(polyclinicId == -1)
            {
                ErrorSchedule_txt.Text = "Mohon buat Poli terlebih dahulu";
                return;
            }
            ErrorSchedule_txt.Text = "";
            createdPoly = 0;
            getController().callMethod("SaveAllSchedules", polyclinicId, schedules);
        }

        public void onClickSavePoly()
        {
            PolyMaster polyMaster = (PolyMaster) PolyMaster_CombB.SelectedItem;
            polyName = polyMaster.name;
            int selectedPoly = (int) PolyMaster_CombB.SelectedValue;
            getController().callMethod("CreatePolyclinic", selectedPoly);
        }

        public void setSuccessCreatePolyclinic(Polyclinic polyclinic)
        {
            this.Dispatcher.Invoke(() =>    
            {
                errorCreateSchedule_txt.setText("");
                successCreatePolyclinic_txt.setText("Sukses Membuat Poli " + polyName);
                polyclinicId = polyclinic.id;

                Day_Cb.Visibility = Visibility.Visible;
                SaveSchedule_bt.IsEnabled = true;
            });
        }

        public void setSuccessCreateSchedule(Schedule schedule)
        {
            this.Dispatcher.Invoke(() =>
            {
                createdPoly++;

                if (createdPoly == schedules.Count)
                {
                    Console.WriteLine(createdPoly + " " + schedules.Count);
                    MessageBoxResult result = MessageBox.Show(
                        "Berhasil membuat semua jadwal untuk poli " + polyName, "Success");
                    if(result == MessageBoxResult.OK)
                        clearAllConfig();
                        
                    return;
                }
                Console.WriteLine(createdPoly + " " + schedules.Count);
                Console.WriteLine("Sukses membuat jadwal di hari " + schedule.day + " " + schedule.timeOpen + "-" + schedule.timeClose);
            });
        }

        private void clearAllConfig()
        {
            polyclinicId = -1;
            polyName = "";
            createdPoly = 0;
            schedules = new Dictionary<string, Schedule>();
            ErrorSchedule_txt.Text = "";
            SuccessPoly_txt.Text = "";
            SaveSchedule_bt.IsEnabled = false;
        }

        void Checked_Handler(object sender, RoutedEventArgs e)
        {
            string name = (sender as CheckBox).Name.ToString();
            Schedule schedule = getValueTimePickers();

            if (schedule == null) {
                (sender as CheckBox).IsChecked = false;
                return;
            }

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
            setTimePicker();
        }

        private void setTimePicker()
        {
            TimeClose_Tp.SelectedTime = null;
            TimeOpen_Tp.SelectedTime = null;
        }

        Schedule getValueTimePickers()
        {
            String timeOpen, timeClose;

            if (TimeOpen_Tp.SelectedTime != null && TimeClose_Tp.SelectedTime != null) {
                ErrorSchedule_txt.Text = "";
                timeOpen = TimeOpen_Tp.SelectedTime.Value.ToString("HH:mm");
                timeClose = TimeClose_Tp.SelectedTime.Value.ToString("HH:mm");
            } else {
                ErrorSchedule_txt.Text = "Anda harus memilih waktu buka dan tutup terlebih dahulu";
                return null;
            }

            return new Schedule(timeOpen, timeClose);
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

        public void fetchDataPolyMaster()
        {
            getController().callMethod("fetchDataPolyMaster");
        }

        public void setComboBox(List<PolyMaster> polymasters)
        {
            this.Dispatcher.Invoke(() =>
            {
                PolyMaster_CombB.ItemsSource = polymasters;
                PolyMaster_CombB.DisplayMemberPath = "name";
                PolyMaster_CombB.SelectedValuePath = "id";
            });
        }
    }
}
