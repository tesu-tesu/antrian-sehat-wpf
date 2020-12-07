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

namespace TestWPPL.Admin.ListPolyclinic
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class ListPolyclinicPage : MyPage
    {
        private BuilderDataGrid builderDataGrid;
        private IMyDataGrid dataGridPoly;

        public ListPolyclinicPage()
        {
            InitializeComponent();
            this.KeepAlive = true;
            setController(new ListPolyclinicController(this));
            initUIBuilders();
            initUIElements();
        }

        private void initUIBuilders()
        {
            builderDataGrid = new BuilderDataGrid();
        }

        private void initUIElements()
        {
            dataGridPoly = builderDataGrid.activate(this, "dgPolyclinic");
        }

        public void fetchDataPolyclinic()
        {
            getController().callMethod("fetchDataPolyclinic");
        }

        public void setListView(List<Polyclinic> polyclinics)
        {
            this.Dispatcher.Invoke(() =>
            {
                //Console.WriteLine("length: " + healthAgencies.LongCount());
                dgPolyclinic.ItemsSource = polyclinics;
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

        public void Connect(int connectionId, object target)
        {
            throw new NotImplementedException();
        }
    }
}
