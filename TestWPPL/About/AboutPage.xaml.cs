using System.Windows.Controls;
using Velacro.UIElements.Basic;

namespace TestWPPL.About
{
    public partial class About : MyPage
    {
        public About()
        {
            InitializeComponent();
            setController(new AboutController(this));
        }
    }
}