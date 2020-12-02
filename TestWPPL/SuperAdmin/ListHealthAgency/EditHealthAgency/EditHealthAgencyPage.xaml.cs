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

namespace TestWPPL.SuperAdmin.ListHealthAgency.EditHealthAgency
{
    /// <summary>
    /// Interaction logic for EditHealthAgencyPage.xaml
    /// </summary>
    public partial class EditHealthAgencyPage : MyPage
    {
        public int idHA { get; set; }

        public EditHealthAgencyPage()
        {
            InitializeComponent();
        }
    }
}
