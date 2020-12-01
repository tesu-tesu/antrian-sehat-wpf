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
using Velacro.UIElements.DataGrid;

namespace TestWPPL.SuperAdmin.ListHealthAgency
{
    /// <summary>
    /// Interaction logic for ListHealthAgencyPage.xaml
    /// </summary>
    public partial class ListHealthAgencyPage : MyPage
    {
        private BuilderDataGrid builderDataGrid;
        private IMyDataGrid dataGridHA;
        List<HealthAgency> healthAgencies;

        public ListHealthAgencyPage()
        {
            InitializeComponent();
            this.KeepAlive = true;
            setController(new HealthAgencyController(this));
            initUIBuilders();
            initUIElements();
        }

        private void initUIBuilders()
        {
            builderDataGrid = new BuilderDataGrid();
        }

        private void initUIElements()
        {
            dataGridHA = builderDataGrid.activate(this, "dgHealthAgencies");
        }

        public void fetchDataHealthAgency()
        {
            getController().callMethod("fetchDataHealthAgency");
        }

        public void setListView(Pagination paginationHA)
        {
            this.Dispatcher.Invoke(() =>
            {
                healthAgencies = paginationHA.data;
                Console.WriteLine("length: " + healthAgencies.LongCount());
                dgHealthAgencies.ItemsSource = healthAgencies;
            });
        }

        void edit_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            getController().callMethod("editProcess", button);

            dgHealthAgencies.ItemsSource = new List<HealthAgency>();
            //navigate ke halaman edit dgn mengirimkan id HA

        }

        void delete_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            getController().callMethod("deleteProcess", button);

            dgHealthAgencies.ItemsSource = new List<HealthAgency>();
            //dgHealthAgencies.ItemsSource = itemlist baru
        }

    }
}
