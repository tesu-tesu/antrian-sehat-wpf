using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using TestWPPL.Annotations;
using Velacro.Basic;
using Velacro.Chart.LineChart;
using Velacro.UIElements.Basic;

namespace TestWPPL.Dashboard {
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    ///
    ///
    public class TodoItem : INotifyPropertyChanged {
        public string Title { get; set; }
        public int Completion { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null){
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public partial class Dashboard : MyPage{
        private IMyLineChart lineChart;
        private BuilderLineChart builderLineChart;
        BindingList<TodoItem> items = new BindingList<TodoItem>();
        public Dashboard() {
            InitializeComponent();
            
            items.Add(new TodoItem() { Title = "Complete this WPF tutorial", Completion = 45 });
            items.Add(new TodoItem() { Title = "Learn C#", Completion = 80 });
            items.Add(new TodoItem() { Title = "Wash the car", Completion = 0 });

            lbTodoList.ItemsSource = items;

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e){
            Button button = sender as Button;
            TodoItem dataObject = button.DataContext as TodoItem;
            int index = items.IndexOf(dataObject);
            items.RemoveAt(index);
        }
    }
}
