using System.Windows.Controls;
using TestWPPL.SuperAdmin.ListPolyMaster.EditPolyMaster;
using TestWPPL.SuperAdmin.ListUserModul.CreateUser;
using Velacro.UIElements.Basic;

namespace TestWPPL.SuperAdmin.ListPolyMaster
{
    public partial class ListPolyMasterPage : MyPage
    {
        private MyPage createPolyMasterPage;
        private MyPage editPolyMasterPage;
        public ListPolyMasterPage()
        {
            InitializeComponent();
            this.KeepAlive = true;
            createPolyMasterPage = new CreateUserPage();
            editPolyMasterPage = new EditPolyMasterPage();
            setController(new ListPolyMasterController(this));
        }
    }
}