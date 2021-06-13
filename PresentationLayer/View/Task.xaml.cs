using IntroSE.Kanban.PresentationLayer.Model;
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
    /// Interaction logic for Task.xaml
    /// </summary>
    public partial class Task : Window
    {
        private Board board;
        private TaskVM taskVM;
        public Task()
        {
            InitializeComponent();
        }
        public Task(ColumnModel columnModel, UserModel user)
        {
            InitializeComponent();
            taskVM = new TaskVM(columnModel, user);
            this.DataContext = taskVM;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            taskVM.AddTask();
            this.Close();
        }
    }
}
