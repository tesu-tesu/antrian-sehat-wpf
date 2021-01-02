using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TestWPPL.Admin.CreatePolyclinicSchedule;
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
            createPolyMasterPage = new CreateUserPage();
            editPolyMasterPage = new EditPolyMasterPage();
            setController(new ListPolyMasterController(this));
            initUIBuilders();
            initUIElements();
        }

        private void initUIElements()
        {
            dataGridPolyMaster = builderDataGrid.activate(this, "DgPolyMaster");
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
            throw new System.NotImplementedException();
        }

        private void addPolyMaster(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
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
                DgPolyMaster.ItemsSource = null;
                DgPolyMaster.ItemsSource = polyMasters;
            });
        }
        
    }
}