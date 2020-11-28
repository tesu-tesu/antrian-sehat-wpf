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

        public ListHealthAgencyPage()
        {
            InitializeComponent();
            this.KeepAlive = true;
            setController(new ListHealthAgencyController(this));
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
                List<HealthAgency> healthAgencies = paginationHA.data;
                Console.WriteLine("length: " + healthAgencies.LongCount());
                dgHealthAgencies.ItemsSource = healthAgencies;
            });
        }

        void edit_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Console.WriteLine("Edit action");
        }

        void delete_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Console.WriteLine("Delete action");
        }

    }
}
