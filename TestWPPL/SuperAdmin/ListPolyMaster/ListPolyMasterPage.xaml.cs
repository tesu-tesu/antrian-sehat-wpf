using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TestWPPL.Admin.CreatePolyclinicSchedule;
using TestWPPL.Service;
using TestWPPL.SuperAdmin.ListPolyMaster.CreatePolyMaster;
using TestWPPL.SuperAdmin.ListPolyMaster.EditPolyMaster;
using TestWPPL.SuperAdmin.ListUserModul.CreateUser;
using Velacro.UIElements.Basic;
using Velacro.UIElements.DataGrid;

namespace TestWPPL.SuperAdmin.ListPolyMaster
{
    public partial class ListPolyMasterPage : MyPage
    {
        private MyPage createPolyMasterPage;
        private MyPage editPolyMasterPage;
        List<PolyMaster> polyMasters;
        private BuilderDataGrid builderDataGrid;
        private IMyDataGrid dataGridPolyMaster;
        
        public ListPolyMasterPage()
        {
            InitializeComponent();
            this.KeepAlive = true;
            createPolyMasterPage = new CreatePolyMasterPage();
            editPolyMasterPage = new EditPolyMasterPage();
            setController(new ListPolyMasterController(this));
            initUIBuilders();
            initUIElements();
        }

        private void initUIElements()
        {
            dataGridPolyMaster = builderDataGrid.activate(this, "DgPolyMasters");
        }

        private void initUIBuilders()
        {
            builderDataGrid = new BuilderDataGrid();
        }

        private void edit_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void delete_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            getController().callMethod("deleteProcess", button.DataContext as PolyMaster);
        }
        
        public void setSuccessDelete(string message)
        {
            this.Dispatcher.Invoke(() =>
            {
                MessageBox.Show(message, "Success");
                fetchDataPolyMaster();
            });
        }
        

        private void addPolyMaster(object sender, RoutedEventArgs e)
        {
            FrameService.frame.Navigate(createPolyMasterPage);
        }
        
        public void fetchDataPolyMaster()
        {
            getController().callMethod("fetchDataPolyMaster");
        }

        public void setListView(Pagination pagination)
        {
            this.Dispatcher.Invoke(() =>
            {
                polyMasters = pagination.data;
                DgPolyMasters.ItemsSource = null;
                DgPolyMasters.ItemsSource = polyMasters;
            });
        }
        
    }
}