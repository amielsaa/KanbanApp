using IntroSE.Kanban.PresentationLayer.ViewModel;
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
using System.Windows.Shapes;

namespace IntroSE.Kanban.PresentationLayer.View
{
    /// <summary>
    /// Interaction logic for Board.xaml
    /// </summary>
    public partial class Board : Window
    {

        private BoardVM boardVM;

        public Board()
        {
            InitializeComponent();
            this.DataContext = boardVM;
        }

        private void Button_Add_Task(object sender, RoutedEventArgs e)
        {
            Task task = new Task();
            task.Show();

        }

        private void Button_Add_Column(object sender, RoutedEventArgs e)
        {

        }
    }
}
