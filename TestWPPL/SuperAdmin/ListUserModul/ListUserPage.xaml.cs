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
using Velacro.UIElements.ListBox;
using Velacro.UIElements.ListView;

namespace TestWPPL.SuperAdmin.ListUserModul
{
    /// <summary>
    /// Interaction logic for ListUserPage.xaml
    /// </summary>
    public partial class ListUserPage : MyPage
    {
        private BuilderListBox builderListBox;
        private IMyListBox listBoxUser;

        public ListUserPage()
        {
            InitializeComponent();
            this.KeepAlive = true;
            setController(new ListUserController(this));
            initUIBuilders();
            initUIElements();
        }

        private void initUIBuilders()
        {
            builderListBox = new BuilderListBox();
        }


        private void initUIElements()
        {
            listBoxUser = builderListBox.activate(this, "lbUser");
        }
        public void fetchDataUser()
        {
            getController().callMethod("fetchDataUser");
        }

        public void setListView(List<User> users)
        {
            this.Dispatcher.Invoke(() =>
            {
                Console.WriteLine("length: " + users.LongCount());
                lbUser.ItemsSource = users;
                //listBoxUser.setItemsSource<User>((Velacro.Basic.MyList<User>)users);
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
