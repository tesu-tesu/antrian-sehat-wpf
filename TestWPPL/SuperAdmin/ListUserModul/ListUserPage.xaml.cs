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
using TestWPPL.Service;
using TestWPPL.SuperAdmin.ListUserModul.CreateUser;
using TestWPPL.SuperAdmin.ListUserModul.EditUser;
using Velacro.UIElements.Basic;
using Velacro.UIElements.DataGrid;
using Velacro.UIElements.ListBox;

namespace TestWPPL.SuperAdmin.ListUserModul
{
    /// <summary>
    /// Interaction logic for ListUserPage.xaml
    /// </summary>
    public partial class ListUserPage : MyPage
    {
        private BuilderDataGrid builderDataGrid;
        private IMyDataGrid dataGridUser;
        private MyPage createUserPage;
        private MyPage editUserPage;

        public ListUserPage()
        {
            InitializeComponent();
            this.KeepAlive = true;
            createUserPage = new CreateUserPage();
            editUserPage = new EditUserPage();
            setController(new ListUserController(this));
            initUIBuilders();
            initUIElements();
        }

        private void initUIBuilders()
        {
            builderDataGrid = new BuilderDataGrid();
        }

        private void initUIElements()
        {
            dataGridUser = builderDataGrid.activate(this, "dgUsers");
        }

        public void fetchDataUser()
        {
            getController().callMethod("fetchDataUser");
        }

        public void setListView(List<User> users)
        {
            this.Dispatcher.Invoke(() =>
            {
                //Console.WriteLine("length: " + users.LongCount());
                dgUsers.ItemsSource = users;
                //dataGridUser.setItemsSource<User>((Velacro.Basic.MyList<User>)users);
            });
        }

        void edit_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            User dataObject = button.DataContext as User;
            //Console.WriteLine("index: " + dataObject.id);
            FrameService.frame.Navigate(editUserPage);
            ((EditUserPage)editUserPage).userId = dataObject.id;

            editUserPage.callMethod("fetchUserData");
        }

        void delete_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            getController().callMethod("deleteUser", button.DataContext as User);
            Console.WriteLine("Delete action");
        }

        void addUser(object sender, RoutedEventArgs e)
        {
            FrameService.frame.Navigate(createUserPage);
            createUserPage.callMethod("fetchDataHealthAgency");
        }

        public void setSuccessDelete(string message)
        {
            this.Dispatcher.Invoke(() =>
            {
                MessageBox.Show(message, "Success");
                fetchDataUser();
            });
        }
    }
}
